using AutoMapper;

using Granp.DTOs;
using Granp.Models.Entities;
using Granp.Models.Types;

namespace Granp.DTOs.Mappers
{
    public class TimeSlotMapperProfile : Profile
    {
        public TimeSlotMapperProfile()
        { 
            CreateMap<TimeSlotRequest, TimeSlot>()
                .BeforeMap((src, dest, ctx) => dest.ProfessionalId = Guid.Parse(ctx.Items["ProfessionalId"].ToString()));
            CreateMap<TimeSlot, TimeSlotResponse>();
        }
    }
}