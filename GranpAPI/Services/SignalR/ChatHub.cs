using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

using Granp.Services.Repositories;
using Granp.Services.Repositories.Interfaces;

using System.Security.Claims;

namespace Granp.Services.SignalR
{
    // [Authorize]
    public class ChatHub : Hub<IChatHub>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChatHub(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public override async Task OnConnectedAsync()
        {
            if (Context.User == null)
            {
                // Error
                throw new HubException("No Identity");
            }

            // Get user id from token
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Get role from token
            var role = Context.User.FindFirstValue(ClaimTypes.Role); 

            // If the user id or role is null, return bad request
            if (userId == null || role == null)
            {
                // Error
                throw new HubException("No Identity");
            }

            // If the user is a customer
            if (role == "Customer")
            {
                // Get the customer profile from the database
                var customer = await _unitOfWork.Customers.GetByUserId(userId);

                // If the customer profile is not in the database, return false
                if (customer == null)
                {
                    // Error
                    throw new HubException("No Identity");
                }

                // Add the connection id to the customer group
                await Groups.AddToGroupAsync(Context.ConnectionId, "c-" + customer.Id.ToString());
            } 
            else if (role == "Professional")
            {
                // Get the professional profile from the database
                var professional = await _unitOfWork.Professionals.GetByUserId(userId);

                // If the professional profile is not in the database, return false
                if (professional == null)
                {
                    // Error ?
                    throw new HubException("No Identity");
                }

                // Add the connection id to the professional group
                await Groups.AddToGroupAsync(Context.ConnectionId, "p-" + professional.Id.ToString());
            }

            await base.OnConnectedAsync();
        }
    }
}
