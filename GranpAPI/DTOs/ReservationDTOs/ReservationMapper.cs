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
            CreateMap<ReservationRequest, Reservation>();
            CreateMap<Reservation, ReservationResponse>();
        }
    }
}