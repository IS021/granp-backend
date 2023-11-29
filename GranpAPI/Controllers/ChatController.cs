using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AutoMapper;

using Granp.Models.Entities;
using Granp.Services.Repositories.Interfaces;
using Granp.Services.Repositories.Extensions;
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

        // Create chat
        [HttpPost("create"), Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateChat(Guid professionalId)
        {
            // Get User Id from the authentication token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user id is null, return bad request
            if (userId == null)
            {
                return BadRequest();
            }

            // Use extension method to get the user profile
            var customer = await _unitOfWork.Customers.GetByUserId(userId);

            // If the user profile is null, return bad request
            if (customer == null)
            {
                return BadRequest();
            }

            // Get the professional profile from the database
            var professional = await _unitOfWork.Professionals.GetById(professionalId);

            // If the professional profile is not in the database, return bad request
            if (professional == null)
            {
                return BadRequest();
            }

            // Create a new chat
            var chat = new Chat();

            // Add the members to the chat
            chat.Members.Add(customer.Id);
            chat.Members.Add(professional.Id);

            // Add the chat to the database
            await _unitOfWork.Chats.Add(chat);

            // Save the changes to the database
            await _unitOfWork.CompleteAsync();

            // Return the chat id
            return Ok(chat.Id);
        }

        // Get chat list
        [HttpGet("list"), Authorize(Roles = "Customer, Professional")]
        public async Task<IActionResult> GetChatList()
        {
            // Get User Id from the authentication token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user id is null, return bad request
            if (userId == null)
            {
                return BadRequest("User Id is null");
            }

            // Use extension method to get the user profile
            var user = await _unitOfWork.GetUser(userId);

            // If the user profile is null, return bad request
            if (user == null)
            {
                return BadRequest("User is null");
            }

            // Get the chat list from the database
            var chatList = await _unitOfWork.Chats.GetByMemberId(user.Id);

            // If the chat list is null, return false
            if (chatList == null)
            {
                return BadRequest("Chat list is null");
            }
            

            // Map the chat list to the ChatListResponse DTO
            List<ChatInfoResponse> chatListResponse = new List<ChatInfoResponse>();

            for (int i = 0; i < chatList.Count; i++)
            {
                var chat = chatList[i];

                // Get other user profile
                var otherUser = await _unitOfWork.GetUser(chat.Members.Where(m => m != user.Id).FirstOrDefault());
                
                // GetLastMessage
                var lastMessage = await _unitOfWork.Messages.GetLastByChatId(chat.Id);

                // Get unread count
                var unreadCount = await _unitOfWork.Messages.GetUnreadCount(chat.Id, user.Id);

                // If the other user profile is null, return bad request
                if (otherUser == null)
                {
                    return BadRequest("Other user is null");
                }

                /*
                if (lastMessage == null)
                {
                    return BadRequest("Last message is null");
                }
                */

                // FOR TESTING PURPOSES
                if (lastMessage == null)
                {
                    lastMessage = new Message
                    {
                        Content = "",
                        Time = DateTime.Now
                    };
                }

                var chatInfo = new ChatInfoResponse
                {
                    Id = chat.Id,
                    ProfilePicture = otherUser.ProfilePicture,
                    Name = otherUser.FirstName + " " + otherUser.LastName,
                    LastMessage = lastMessage.Content,
                    Time = lastMessage.Time,
                    UnreadMessages = unreadCount
                };

                chatListResponse.Add(chatInfo);
            }

            // Return the chat list
            return Ok(chatListResponse);

        }

        // Get chat messages
        [HttpGet("messages"), Authorize(Roles = "Customer, Professional")]
        public async Task<IActionResult> GetChatMessages(Guid chatId)
        {
            // Get User Id from the authentication token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user id is null, return bad request
            if (userId == null)
            {
                return BadRequest();
            }

            // Use extension method to get the user profile
            var user = await _unitOfWork.GetUser(userId);

            // If the user profile is null, return bad request
            if (user == null)
            {
                return BadRequest();
            }

            // Get the chat from the database
            var chat = await _unitOfWork.Chats.GetById(chatId);

            // If the chat is null, return bad request
            if (chat == null)
            {
                return BadRequest();
            }

            // If the user is not a member of the chat, return bad request
            if (!chat.Members.Contains(user.Id))
            {
                return BadRequest();
            }

            // Get the messages from the database
            var messages = await _unitOfWork.Messages.GetByChatId(chatId);

            // If the messages are null, return bad request
            if (messages == null)
            {
                return BadRequest();
            }

            // Map the messages to the MessageResponse DTO
            var messagesResponse = _mapper.Map<List<ChatMessageResponse>>(messages, opt => opt.Items["UserId"] = user.Id);

            // Return the messages
            return Ok(messagesResponse);
        }

        [HttpPost("send"), Authorize(Roles = "Customer, Professional")]
        public async Task<IActionResult> SendMessage(ChatMessageRequest message)
        {
            // Get User Id from the authentication token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the user id is null, return bad request
            if (userId == null)
            {
                return BadRequest();
            }

            // Use extension method to get the user profile
            var user = await _unitOfWork.GetUser(userId);

            // If the user profile is null, return bad request
            if (user == null)
            {
                return BadRequest();
            }

            // Get the chat from the database
            var chat = await _unitOfWork.Chats.GetById(message.ChatId);

            // If the chat is null, return bad request
            if (chat == null)
            {
                return BadRequest();
            }

            // If the user is not a member of the chat, return bad request
            if (!chat.Members.Contains(user.Id))
            {
                return BadRequest();
            }

            // Map the message to the Message entity
            // TODO
            var newMessage = new Message
            {
                ChatId = message.ChatId,
                SenderId = user.Id,
                Content = message.Content,
                Time = DateTime.Now
            };

            // Add the message to the database
            await _unitOfWork.Messages.Add(newMessage);

            // Save the changes to the database
            await _unitOfWork.CompleteAsync();

            // Send the message to the chat group
            await hubContext.Clients.Group(chat.Id.ToString()).SendAsync("ReceiveMessage", message);

            return Ok();
        }
    }
}