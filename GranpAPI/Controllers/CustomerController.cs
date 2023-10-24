using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AutoMapper;

using Granp.Models.Entities;
using Granp.Services.Repositories.Interfaces;
using Granp.DTOs;

namespace Granp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : GenericController<CustomerController> // ControllerBase is a base class for MVC controller without view support.
    {
        public CustomerController(ILogger<CustomerController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(logger, unitOfWork, mapper) { }

        // Create a new customer
        [HttpPost, Authorize]
        public async Task<IActionResult> CreateCustomer(CustomerRequest customerRequest) // 
        {
            if (ModelState.IsValid)
            {
                // Get User Id from the authentication token
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Map the request to a customer entity
                var customer = _mapper.Map<Customer>(customerRequest, opts => opts.Items["UserId"] = userId);

                await _unitOfWork.Customers.Add(customer); // add the customer to the database
                await _unitOfWork.CompleteAsync(); // save the changes to the database

                return CreatedAtAction("GetItem", new { id = customer.Id }, customer);
            }

            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        // Get a customer by id
        // NOTE: this will be replaced by the authentication system
        // Id will be automatically taken from the authentication token
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var customer = await _unitOfWork.Customers.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // Update a player by id
        // NOTE: this will be replaced by the authentication system
        // Id will be automatically taken from the authentication token
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            await _unitOfWork.Customers.Update(customer);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // Delete a customer
        // NOTE: this will be replaced by the authentication system
        // Id will be automatically taken from the authentication token
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var customer = await _unitOfWork.Customers.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            await _unitOfWork.Customers.Delete(id);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}