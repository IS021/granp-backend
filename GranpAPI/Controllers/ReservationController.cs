using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AutoMapper;

using Granp.Models.Entities;
using Granp.Models.Enums;
using Granp.Services.Repositories.Interfaces;
using Granp.Services.Repositories.Extensions;
using Granp.DTOs;


/*
| Method | Path | Description | Roles |
| ------ | ---- | ----------- | ----- |
| POST | /reservations/request | Create a reservation | Customer |
| DELETE | /reservations/cancel/{id} | Cancel a reservation | Customer, Professional |
| GET | /reservations/get-all | Get all user reservations | Customer, Professional |
| POST | /reservations/accept/{id} | Accept a reservation | Professional |
| POST | /reservations/decline/{id} | Decline a reservation | Professional |
*/

namespace Granp.Controllers
{
    [ApiController, Route("reservations")]
    public class ReservationController: GenericController<ReservationController>
    {
        public ReservationController(ILogger<ReservationController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(logger, unitOfWork, mapper) { }

        [HttpPost("request"), Authorize(Roles = "Customer")]
        // Create a reservation
        public async Task<IActionResult> RequestReservation(ReservationRequest reservationRequest)
        {
            // Get User Id from the authentication token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user id is null, return bad request
            if (userId == null)
            {
                return BadRequest();
            }

            // Get the customer profile from the database
            var customer = await _unitOfWork.Customers.GetByUserId(userId);

            // If the customer profile is not in the database, return bad request
            if (customer == null)
            {
                return BadRequest();
            }

            // Create a new reservation
            var reservation = _mapper.Map<Reservation>(reservationRequest, opts => opts.Items["CustomerId"] = customer.Id);

            // Set the customer profile
            await _unitOfWork.Reservations.Add(reservation);

            // NOTIFY PROFESSIONAL

            // Save changes to the database
            await _unitOfWork.CompleteAsync();

            // Return ok
            return Ok();
        }

        [HttpDelete("cancel/{id}"), Authorize(Roles = "Customer, Professional")]
        // Cancel a reservation
        public async Task<IActionResult> CancelReservation(Guid id)
        {
            // Get User Id from the authentication token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user id is null, return bad request
            if (userId == null)
            {
                return BadRequest();
            }

            // Get the reservation from the database
            var reservation = await _unitOfWork.Reservations.GetById(id);

            // If the reservation is not in the database, return bad request
            if (reservation == null)
            {
                return BadRequest();
            }

            // Use the user lookup extension to get the user
            var user = await _unitOfWork.GetUser(userId);

            // If the user is not in the database, return bad request
            if (user == null)
            {
                return BadRequest();
            }

            // If the reservation does not belong to the user, return bad request
            if (reservation.Customer!.Id != user.Id && reservation.Professional!.Id != user.Id)
            {
                return BadRequest();
            }

            // Set the reservation status to cancelled
            reservation.Status = ReservationStatus.Cancelled;

            // NOTIFY CUSTOMER AND PROFESSIONAL

            // Save changes to the database
            await _unitOfWork.CompleteAsync();

            // Return ok
            return Ok();
        }

        [HttpGet("get-all"), Authorize(Roles = "Customer, Professional")]
        // Get all user reservations using authentication token as a filter
        public async Task<IActionResult> GetAll()
        {
            // Get User Id from the authentication token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user id is null, return bad request
            if (userId == null)
            {
                return BadRequest();
            }

            // Use the user lookup extension to get the user
            var user = await _unitOfWork.GetUser(userId);

            // If the user is not in the database, return bad request
            if (user == null)
            {
                return BadRequest();
            }

            // If the user is a customer
            if (user is Customer)
            {
                // Get all reservations from the database
                var reservations = await _unitOfWork.Reservations.GetByCustomerId(user.Id);

                // Map the reservations to a response
                var reservationsResponse = _mapper.Map<IEnumerable<ReservationResponse>>(reservations);

                // Return ok
                return Ok(reservationsResponse);
            } else if (user is Professional)
            {
                // Get all reservations from the database
                var reservations = await _unitOfWork.Reservations.GetByProfessionalId(user.Id);

                // Map the reservations to a response
                var reservationsResponse = _mapper.Map<IEnumerable<ReservationResponse>>(reservations);

                // Return ok
                return Ok(reservationsResponse);
            }

            // Return bad request
            return BadRequest();
        }

        [HttpPost("accept/{id}"), Authorize(Roles = "Professional")]
        // Accept a reservation
        public async Task<IActionResult> AcceptReservation(Guid id)
        {
            // Get User Id from the authentication token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user id is null, return bad request
            if (userId == null)
            {
                return BadRequest();
            }

            // Get the reservation from the database
            var reservation = await _unitOfWork.Reservations.GetById(id);

            // If the reservation is not in the database, return bad request
            if (reservation == null)
            {
                return BadRequest();
            }

            // Get the professional profile from the database
            var professional = await _unitOfWork.Professionals.GetByUserId(userId);

            // If the professional profile is not in the database, return bad request
            if (professional == null)
            {
                return BadRequest();
            }

            // If the reservation does not belong to the professional, return bad request
            if (reservation.Professional!.Id != professional.Id)
            {
                return BadRequest();
            }

            // Set the reservation status to accepted
            reservation.Status = ReservationStatus.Accepted;

            // Save changes to the database
            await _unitOfWork.CompleteAsync();

            // Return ok
            return Ok();
        }

        [HttpPost("decline/{id}"), Authorize(Roles = "Professional")]
        // Decline a reservation
        public async Task<IActionResult> DeclineReservation(Guid id)
        {
            // Get User Id from the authentication token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user id is null, return bad request
            if (userId == null)
            {
                return BadRequest();
            }

            // Get the reservation from the database
            var reservation = await _unitOfWork.Reservations.GetById(id);

            // If the reservation is not in the database, return bad request
            if (reservation == null)
            {
                return BadRequest();
            }

            // Get the professional profile from the database
            var professional = await _unitOfWork.Professionals.GetByUserId(userId);

            // If the professional profile is not in the database, return bad request
            if (professional == null)
            {
                return BadRequest();
            }

            // If the reservation does not belong to the professional, return bad request
            if (reservation.Professional!.Id != professional.Id)
            {
                return BadRequest();
            }

            // Set the reservation status to declined
            reservation.Status = ReservationStatus.Declined;

            // Save changes to the database
            await _unitOfWork.CompleteAsync();

            // Return ok
            return Ok();
        }

    }
}