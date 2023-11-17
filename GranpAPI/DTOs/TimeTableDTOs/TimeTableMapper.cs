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
                .BeforeMap((src, dest, ctx) => 
                    {
                        dest.TimeSlots.ForEach(slot => slot.ProfessionalId = Guid.Parse(ctx.Items["ProfessionalId"].ToString()));
                    });
            CreateMap<TimeTable, TimeTableResponse>();
        }
    }
}