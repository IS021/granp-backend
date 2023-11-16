using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AutoMapper;

using Granp.Models.Entities;
using Granp.Models.Enums;
using Granp.Services.Repositories.Interfaces;
using Granp.DTOs;


/*
| Method | Path | Description | Roles |
| ------ | ---- | ----------- | ----- |
| POST | /reservations/request | Request a reservation to a Professional | Customer |
| DELETE | /reservations/cancel/{id} | Cancel a reservation | All |
| GET | /reservations/get-all | Get all user reservations using authentication token as a filter | All |
| POST | /reservations/accept/{id} | Accept a reservation | Professional |
| POST | /reservations/decline/{id} | Decline a reservation | Professional |
*/

namespace Granp.Controllers
{
    [ApiController, Route("reservations")]
    public class ReservationController: GenericController<ReservationController>
    {
        public ReservationController(ILogger<ReservationController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(logger, unitOfWork, mapper) { }

        [HttpPost("create"), Authorize(Roles = "Customer")]
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
            var reservation = _mapper.Map<Reservation>(reservationRequest);

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

            // If the user is a customer
            if (User.IsInRole("Customer"))
            {
                // Get the customer profile from the database
                var customer = await _unitOfWork.Customers.GetByUserId(userId);

                // If the customer profile is not in the database, return bad request
                if (customer == null)
                {
                    return BadRequest();
                }

                // If the reservation does not belong to the customer, return bad request
                if (reservation.Customer!.Id != customer.Id)
                {
                    return BadRequest();
                }

                // Set the reservation status to cancelled
                reservation.Status = ReservationStatus.Cancelled;
            }

            // If the user is a professional
            if (User.IsInRole("Professional"))
            {
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

                // Set the reservation status to canceled
                reservation.Status = ReservationStatus.Cancelled;
            }

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

            // If the user is a customer
            if (User.IsInRole("Customer"))
            {
                // Get the customer profile from the database
                var customer = await _unitOfWork.Customers.GetByUserId(userId);

                // If the customer profile is not in the database, return bad request
                if (customer == null)
                {
                    return BadRequest();
                }

                // Get all reservations from the database
                var reservations = await _unitOfWork.Reservations.GetByCustomerId(customer.Id);

                // Map the reservations to a response
                var reservationsResponse = _mapper.Map<IEnumerable<ReservationResponse>>(reservations);

                // Return ok
                return Ok(reservationsResponse);
            }

            // If the user is a professional
            if (User.IsInRole("Professional"))
            {
                // Get the professional profile from the database
                var professional = await _unitOfWork.Professionals.GetByUserId(userId);

                // If the professional profile is not in the database, return bad request
                if (professional == null)
                {
                    return BadRequest();
                }

                // Get all reservations from the database
                var reservations = await _unitOfWork.Reservations.GetByProfessionalId(professional.Id);

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