using AutoMapper;

using Granp.DTOs;
using Granp.Models.Entities;
using Granp.Models.Types;

namespace Granp.DTOs.Mappers
{
    public class TimeTableMapperProfile : Profile
    {
        public TimeTableMapperProfile()
        {
            // Map Timetable to TimetableResponse 
            CreateMap<TimeTableRequest, TimeTable>()
                .BeforeMap((src, dest, ctx) => dest.ProfessionalId = (Guid) ctx.Items["ProfessionalId"]);
            CreateMap<TimeTable, TimeTableResponse>();
        }
    }
}