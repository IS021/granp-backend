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
            CreateMap<TimeSlotRequest, TimeSlot>();
            CreateMap<TimeSlot, TimeSlotResponse>();
        }
    }
}