using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AutoMapper;

using Granp.Models.Entities;
using Granp.Services.Repositories.Interfaces;
using Granp.DTOs;

/*
| Method | Path | Description | Roles |
| ------ | ---- | ----------- | ----- |
| GET | /customer/is-complete | Check if profile is complete | Customer |
| POST | /customer/complete | Complete customer profile | Customer |
| GET | /customer/get | Get customer profile | Customer |
| PUT | /customer/update | Update customer profile | Customer |
| DELETE | /customer/delete | Delete profile | Customer |
*/

namespace Granp.Controllers
{
    [ApiController, Route("customer")]
    public class CustomerProfileController : GenericController<CustomerProfileController> 
    {

        public CustomerProfileController(ILogger<CustomerProfileController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(logger, unitOfWork, mapper) { }

        [HttpGet("is-complete"), Authorize(Roles = "Customer")]
        // Check if the customer profile is in the database
        public async Task<IActionResult> IsComplete()
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

            // If the customer profile is not in the database, return false
            if (customer == null)
            {
                return Ok(false);
            }

            // If the customer profile is in the database, return true
            return Ok(true);
        }

        [HttpPost("complete"), Authorize(Roles = "Customer")]
        // Complete the customer profile
        public async Task<IActionResult> Complete(CustomerProfileRequest customerProfileRequest)
        {
            // Get User Id from the authentication token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user id is null, return bad request
            if (userId == null)
            {
                return BadRequest();
            }

            // Map the request to a customer entity
            var customer = _mapper.Map<Customer>(customerProfileRequest, opts => opts.Items["UserId"] = userId);

            // Add the customer profile to the database
            await _unitOfWork.Customers.Add(customer);

            // Save the changes to the database
            await _unitOfWork.CompleteAsync();

            // Return the customer profile
            return Ok();
        }

        [HttpGet("get"), Authorize(Roles = "Customer")]
        // Get the customer profile
        public async Task<IActionResult> Get()
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

            // If the customer profile is not in the database, return not found
            if (customer == null)
            {
                return NotFound();
            }

            // Map the customer profile to a response
            var customerProfileResponse = _mapper.Map<CustomerProfileResponse>(customer);

            // Return the customer profile
            return Ok(customerProfileResponse);
        }

        [HttpPut("update"), Authorize(Roles = "Customer")]
        // Update the customer profile
        public async Task<IActionResult> Update(CustomerProfileRequest customerProfileRequest)
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

            // If the customer profile is not in the database, return not found
            if (customer == null)
            {
                return NotFound();
            }

            // Map the request to the customer profile
            _mapper.Map(customerProfileRequest, customer);

            // Update the customer profile in the database
            await _unitOfWork.Customers.Update(customer);

            // Save the changes to the database
            await _unitOfWork.CompleteAsync();

            // Return the customer profile
            return Ok();
        }

        [HttpDelete("delete"), Authorize(Roles = "Customer")]
        // Delete the customer profile
        public async Task<IActionResult> Delete()
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

            // If the customer profile is not in the database, return not found
            if (customer == null)
            {
                return NotFound();
            }

            // Delete the customer profile from the database
            await _unitOfWork.Customers.Delete(customer.Id);

            // Save the changes to the database
            await _unitOfWork.CompleteAsync();

            // Return the customer profile
            return Ok();
        }
        
    }
}