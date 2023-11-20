using AutoMapper;

using Granp.DTOs;
using Granp.Models.Entities;
using Granp.Services.SignalR;

namespace Granp.DTOs.Mappers
{
    public class ChatMessageMapperProfile : Profile
    {
        public ChatMessageMapperProfile()
        {
            // CreateMap<ChatMessage, ChatMessageProfileResponse>();            
            CreateMap<ChatMessageRequest, ChatMessage>()
                .BeforeMap((src, dest, ctx) => dest.From = ctx.Items["From"].ToString());

        }
    }
}