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
| GET | /search/info/{id} | Get professional's info by id | Customer |
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
        public async Task<IActionResult> Search(SearchFilter filter)
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

            // Get the professionals from the database
            var professionals = await _unitOfWork.Professionals.GetByFilter(filter);

            // Map the professionals to a list of professional profile responses
            var professionalProfileResponses = _mapper.Map<List<ProfessionalProfileResponse>>(professionals);

            // Return the list of professional profile responses
            return Ok(professionalProfileResponses);
        }

        [HttpGet("info"), Authorize(Roles = "Customer")]
        // Get professional's info by id
        public async Task<IActionResult> Get(Guid id)
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
            var professionalProfileResponse = _mapper.Map<ProfessionalProfileResponse>(professional);

            // Return the professional profile response
            return Ok(professionalProfileResponse);
        }

    }
}