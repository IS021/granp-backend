using AutoMapper;

using Granp.DTOs;
using Granp.Models.Entities;

namespace Granp.DTOs.Mappers
{
    public class ReservationMapperProfile : Profile
    {
        public ReservationMapperProfile()
        {

            // Map customer and professional ids to entities
            CreateMap<ReservationRequest, Reservation>()
                .BeforeMap((src, dest, ctx) => 
                    {
                        dest.CustomerId = (Guid) ctx.Items["CustomerId"];
                    });
            CreateMap<Reservation, ReservationResponse>();
        }
    }
}