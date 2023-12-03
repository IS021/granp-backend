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
| GET | /professional/is-complete | Check if profile is complete | Professional |
| POST | /professional/complete | Complete professional profile | Professional |
| GET | /professional/get | Get professional profile | Professional |
| PUT | /professional/update | Update professional profile | Professional |
| DELETE | /professional/delete | Delete profile | Professional |
*/

namespace Granp.Controllers
{
    [ApiController, Route("professional")]
    public class ProfessionalProfileController : GenericController<ProfessionalProfileController> 
    {

        public ProfessionalProfileController(ILogger<ProfessionalProfileController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(logger, unitOfWork, mapper) { }

        [HttpGet("is-complete"), Authorize(Roles = "Professional")]
        // Check if the professional profile is in the database
        public async Task<IActionResult> IsComplete()
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

            // If the professional profile is not in the database, return false
            if (professional == null)
            {
                return Ok(false);
            }

            // If the professional profile is in the database, return true
            return Ok(true);
        }

        [HttpPost("complete"), Authorize(Roles = "Professional")]
        // Complete the professional profile
        public async Task<IActionResult> Complete(ProfessionalProfileRequest professionalProfileRequest)
        {
            // Get User Id from the authentication token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user id is null, return bad request
            if (userId == null)
            {
                return BadRequest();
            }

            // Map the request to a professional entity
            var professional = _mapper.Map<Professional>(professionalProfileRequest, opts => opts.Items["UserId"] = userId);

            // Add the professional profile to the database
            await _unitOfWork.Professionals.Add(professional);

            // Save the changes to the database
            await _unitOfWork.CompleteAsync();

            // Return ok
            return Ok();
        }

        [HttpGet("get"), Authorize(Roles = "Professional")]
        // Get the professional profile
        public async Task<IActionResult> Get()
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

            // If the professional profile is not in the database, return not found
            if (professional == null)
            {
                return NotFound();
            }

            // Map the professional profile to a response
            var professionalProfileResponse = _mapper.Map<ProfessionalProfileResponse>(professional);

            // Return ok
            return Ok(professionalProfileResponse);
        }

        [HttpPut("update"), Authorize(Roles = "Professional")]
        // Update the professional profile
        public async Task<IActionResult> Update(ProfessionalProfileRequest professionalProfileRequest)
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

            // If the professional profile is not in the database, return not found
            if (professional == null)
            {
                return NotFound();
            }

            // Map the request to the professional profile
            _mapper.Map(professionalProfileRequest, professional);

            // Update the professional profile in the database
            await _unitOfWork.Professionals.Update(professional);

            // Save the changes to the database
            await _unitOfWork.CompleteAsync();

            // Return ok
            return Ok();
        }

        [HttpDelete("delete"), Authorize(Roles = "Professional")]
        // Delete the professional profile
        public async Task<IActionResult> Delete()
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

            // If the professional profile is not in the database, return not found
            if (professional == null)
            {
                return NotFound();
            }

            // Delete the professional profile from the database
            await _unitOfWork.Professionals.Delete(professional.Id);

            // Save the changes to the database
            await _unitOfWork.CompleteAsync();

            // Return ok
            return Ok();
        }
        
    }
}