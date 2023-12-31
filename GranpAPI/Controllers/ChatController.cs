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

/*
| Method | Path | Description | Roles |
| ------ | ---- | ----------- | ----- |
| POST | /chat/create | Create chat | Customer |
| GET | /chat/list | Get chat list | Customer, Professional |
| GET | /chat/messages/{chatId} | Get chat messages | Customer, Professional |
| POST | /chat/send | Send message | Customer, Professional |
| POST | /chat/read/{chatId} | Mark messages as read | Customer, Professional |
*/

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
                return BadRequest("User Id is null");
            }

            // Use extension method to get the user profile
            var customer = await _unitOfWork.Customers.GetByUserId(userId);

            // If the user profile is null, return bad request
            if (customer == null)
            {
                return BadRequest("Customer is null");
            }

            // Get the professional profile from the database
            var professional = await _unitOfWork.Professionals.GetById(professionalId);

            // If the professional profile is not in the database, return bad request
            if (professional == null)
            {
                return BadRequest("Professional is null");
            }

            var members = new List<Guid> { customer.Id, professional.Id };

            // Check if the chat already exists
            var existingChat = await _unitOfWork.Chats.GetByMembers(members);

            // If the chat already exists, return chat id
            if (existingChat != null)
            {
                return Ok(existingChat.Id);
            } else {
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
                    // BadRequest("Other user is null");
                    continue;
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
                    ProfileId = otherUser.Id,
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
        [HttpGet("messages/{chatId}"), Authorize(Roles = "Customer, Professional")]
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
                Time = message.Time
            };

            // Add the message to the database
            await _unitOfWork.Messages.Add(newMessage);

            // Save the changes to the database
            await _unitOfWork.CompleteAsync();

            // Map the message to the SignalRMessage DTO
            var signalRMessage = _mapper.Map<SignalRMessage>(newMessage);

            // Send the message to the chat group
            foreach (var member in chat.Members)
            {
                if (member == user.Id)
                {
                    continue;
                }
                await hubContext.Clients.Group(member.ToString()).SendAsync("ReceiveMessage", signalRMessage);
            }

            return Ok();
        }

        // Mark messages from the other user in a sepcific chat as read
        [HttpPost("read/{chatId}"), Authorize(Roles = "Customer, Professional")]
        public async Task<IActionResult> MarkMessagesAsRead(Guid chatId)
        {
            // Get User Id from the authentication token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Use extension method to get the user profile
            var user = await _unitOfWork.GetUser(userId);

            // Get the chat from the database
            var chat = await _unitOfWork.Chats.GetById(chatId);

            // If the chat is null, return bad request
            if (chat == null)
            {
                return BadRequest("Chat is null");
            }

            // If the user is not a member of the chat, return bad request
            if (!chat.Members.Contains(user.Id))
            {
                return BadRequest("User is not a member of the chat");
            }

            // Mark messages as read
            await _unitOfWork.Messages.MarkAsRead(chatId, user.Id);

            // Save the changes to the database
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

    }
}