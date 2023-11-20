using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AutoMapper;

using Granp.Models.Entities;
using Granp.Services.Repositories.Interfaces;
using Granp.Services.SignalR;
using Granp.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace Granp.Controllers
{
    [ApiController, Route("chat")]
    public class ChatController : GenericController<ChatController>
    {
        private readonly IHubContext<ChatHub> hubContext;

        public ChatController(ILogger<ChatController> logger, IUnitOfWork unitOfWork, IMapper mapper, IHubContext<ChatHub> hubContext) : base(logger, unitOfWork, mapper) {
            this.hubContext = hubContext;
        }

        [HttpPost, Authorize(Roles = "Customer, Professional")]
        public async Task<IActionResult> SendMessage(ChatMessageRequest message)
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

                // If the customer profile is not in the database, return false
                if (customer == null)
                {
                    return BadRequest();
                }

                // Get the professional profile from the database
                var professionalProfile = await _unitOfWork.Professionals.GetById(Guid.Parse(message.To));

                // If the professional profile is not in the database, return false
                if (professionalProfile == null)
                {
                    return BadRequest();
                }

                // Send the message to the professional group
                await hubContext.Clients.Group("p-" + professionalProfile.Id.ToString()).SendAsync("ReceiveMessage", message);

                return Ok();
            }
            else if (User.IsInRole("Professional"))
            {
                // Get the professional profile from the database
                var professional = await _unitOfWork.Professionals.GetByUserId(userId);

                // Get the customer profile from the database
                var customerProfile = await _unitOfWork.Customers.GetByUserId(message.To);

                // If the customer profile is not in the database, return false
                if (customerProfile == null)
                {
                    return BadRequest();
                }

                // Send the message to the customer group
                await hubContext.Clients.Group("c-" + customerProfile.Id.ToString()).SendAsync("ReceiveMessage", message);

                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}