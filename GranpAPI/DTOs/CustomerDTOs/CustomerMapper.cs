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
            CreateMap<Customer, CustomerPublicResponse>();
            
            CreateMap<CustomerProfileRequest, Customer>()
                .BeforeMap((src, dest, ctx) => dest.UserId = ctx.Items["UserId"].ToString())
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.IsElder ? src.ElderFirstName : src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.IsElder ? src.ElderLastName : src.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.IsElder ? src.ElderPhoneNumber : src.PhoneNumber));
        }
    }
}