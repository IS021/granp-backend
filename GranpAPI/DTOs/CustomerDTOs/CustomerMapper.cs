using AutoMapper;

using Granp.DTOs;
using Granp.Models.Entities;

namespace Granp.DTOs.Mappers
{
    public class CustomerMapperProfile : Profile
    {
        public CustomerMapperProfile()
        {
            CreateMap<Customer, CustomerProfileResponse>();
            CreateMap<CustomerProfileRequest, Customer>()
                .BeforeMap((src, dest, ctx) => dest.UserId = ctx.Items["UserId"].ToString())
                // if src.isElder is true than ElderFirstName and ElderLastName and ElderPhoneNumber == src.FirstName and src.LastName and src.PhoneNumber
                .ForMember(dest => dest.ElderFirstName, opt => opt.MapFrom(src => src.IsElder ? src.FirstName : src.ElderFirstName))
                .ForMember(dest => dest.ElderLastName, opt => opt.MapFrom(src => src.IsElder ? src.LastName : src.ElderLastName))
                .ForMember(dest => dest.ElderPhoneNumber, opt => opt.MapFrom(src => src.IsElder ? src.PhoneNumber : src.ElderPhoneNumber));
        }
    }
}