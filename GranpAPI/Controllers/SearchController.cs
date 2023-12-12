using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AutoMapper;

using Granp.Models.Entities;
using Granp.Models.Types;
using Granp.Services.Repositories.Interfaces;
using Granp.DTOs;

/*
| Method | Path | Description | Roles |
| ------ | ---- | ----------- | ----- |
| GET | /search | Search for professionals by filter | Customer |
| GET | /search/professional/{id} | Get professional's info by id | Customer |
| GET | /search/customer/{id} | Get customer's info by id | Professional |
*/

namespace Granp.Controllers
{
    [ApiController, Route("search")]
    public class SearchController: GenericController<SearchController>
    {
        public SearchController(ILogger<SearchController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(logger, unitOfWork, mapper) { }


        // TODO - Add pagination
        [HttpPost(""), Authorize(Roles = "Customer")]
        // Search for professionals by filter
        public async Task<IActionResult> Search(SearchFilterRequest filter)
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

            // Map the filter request to a filter
            var searchFilter = _mapper.Map<SearchFilter>(filter, opts => opts.Items["Location"] = customer.ElderAddress.Location);

            // Get the professionals from the database
            var professionals = await _unitOfWork.Professionals.GetByFilter(searchFilter);

            // Map the professionals to a list of professional profile responses
            var professionalPublicResponses = _mapper.Map<List<ProfessionalPreviewResponse>>(professionals);

            // Return the list of professional profile responses
            return Ok(professionalPublicResponses);
        }

        [HttpGet("professional/{id}"), Authorize(Roles = "Customer")]
        // Get professional's info by id
        public async Task<IActionResult> GetProfessionalInfo(Guid id)
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

            // Get the professional profile from the database
            var professional = await _unitOfWork.Professionals.GetById(id);

            // If the professional profile is not in the database, return bad request
            if (professional == null)
            {
                return BadRequest();
            }

            // Map the professional to a professional profile response
            var professionalProfileResponse = _mapper.Map<ProfessionalPublicResponse>(professional);

            // Return the professional profile response
            return Ok(professionalProfileResponse);
        }

        [HttpGet("customer/{id}"), Authorize(Roles = "Professional")]
        // Get customer's info by id
        public async Task<IActionResult> GetCustomerInfo(Guid id)
        {
            // Get User Id from the authentication token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user id is null, return bad request
            if (userId == null)
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

            // Get the customer profile from the database
            var customer = await _unitOfWork.Customers.GetById(id);

            // If the customer profile is not in the database, return bad request
            if (customer == null)
            {
                return BadRequest();
            }

            // Map the customer to a customer profile response
            var customerProfileResponse = _mapper.Map<CustomerPublicResponse>(customer);

            // Return the customer profile response
            return Ok(customerProfileResponse);
        }

    }
}