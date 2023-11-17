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
| PUT | /timetable/update | Update a timetable | Professional |
| GET | /timetable/get | Get a timetable | Professional |
*/

namespace Granp.Controllers
{
    [ApiController, Route("timetable")]
    public class TimeTableController: GenericController<TimeTableController>
    {
        public TimeTableController(ILogger<TimeTableController> logger, IUnitOfWork unitOfWork, IMapper mapper) : base(logger, unitOfWork, mapper) { }

        [HttpPut("update"), Authorize(Roles = "Professional")]
        // Update a timetable
        public async Task<IActionResult> Update(TimeTableRequest timeTableRequest)
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

            // Map the new timetable adding the professional id to context
            // var timeTable = _mapper.Map<TimeTable>(timeTableRequest, opt => opt.Items["ProfessionalId"] = professional.Id);

            // Update the timetable
            professional.TimeTable.WeeksInAdvance = timeTableRequest.WeeksInAdvance;
            professional.TimeTable.TimeSlots = _mapper.Map<List<TimeSlot>>(timeTableRequest.TimeSlots, opt => opt.Items["ProfessionalId"] = professional.Id);

            // Save the changes to the database
            await _unitOfWork.CompleteAsync();

            // Return ok
            return Ok();
        }

        [HttpGet("get"), Authorize(Roles = "Professional")]
        // Get a timetable
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

            // If the professional profile is not in the database, return bad request
            if (professional == null)
            {
                return BadRequest();
            }

            // Map the timetable to a response
            var timeTableResponse = _mapper.Map<TimeTableResponse>(professional.TimeTable);

            // Return the timetable
            return Ok(timeTableResponse);
        }

    }
}



