using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

using Granp.Services.Repositories;
using Granp.Services.Repositories.Interfaces;
using Granp.Services.Repositories.Extensions;

using System.Security.Claims;

namespace Granp.Services.SignalR
{
    [Authorize]
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
            
            // Get user from database using extension method
            var user = await _unitOfWork.GetUser(userId);

            // If the user is null, return bad request
            if (user == null)
            {
                // Error
                throw new HubException("No Identity");
            }

            // Add the connection id to the user group
            await Groups.AddToGroupAsync(Context.ConnectionId, user.Id.ToString());

            await base.OnConnectedAsync();
        }
    }
}
