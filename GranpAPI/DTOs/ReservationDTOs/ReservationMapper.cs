using AutoMapper;

using Granp.DTOs;
using Granp.Models.Entities;

namespace Granp.DTOs.Mappers
{
    public class ReservationMapperProfile : Profile
    {
        public ReservationMapperProfile()
        {
            // CreateMap<Reservation, ProfessionalProfileResponse>();

            // Map customer and professional ids to entities
            CreateMap<ReservationRequest, Reservation>();
        }
    }
}