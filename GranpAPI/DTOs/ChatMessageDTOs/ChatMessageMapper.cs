using AutoMapper;

using Granp.DTOs;
using Granp.Models.Entities;

namespace Granp.DTOs.Mappers
{
    public class ChatMessageMapperProfile : Profile
    {
        public ChatMessageMapperProfile()
        {
            // CreateMap<ChatMessage, ChatMessageProfileResponse>(); 
            CreateMap<Message, ChatMessageResponse>()
            // "user" if SenderId == userId, "other" otherwise
                .BeforeMap((src, dest, ctx) => dest.Sender = src.SenderId == (Guid)ctx.Items["UserId"] ? "user" : "other")
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time.ToLocalTime()));          
            
            CreateMap<Message, SignalRMessage>();

        }
    }
}