using AutoMapper;

using Granp.DTOs;
using Granp.Models.Entities;

namespace Granp.DTOs.Mappers
{
    public class ProfessionalMapperProfile : Profile
    {
        public ProfessionalMapperProfile()
        {
            CreateMap<Professional, ProfessionalProfileResponse>();
            CreateMap<ProfessionalProfileRequest, Professional>()
                .BeforeMap((src, dest, ctx) => dest.UserId = ctx.Items["UserId"].ToString());
        }
    }
}