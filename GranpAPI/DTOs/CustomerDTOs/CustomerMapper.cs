using AutoMapper;

using Granp.DTOs;
using Granp.Models.Entities;

namespace Granp.DTOs.Mappers
{
    public class CustomerMapperProfile : Profile
    {
        public CustomerMapperProfile()
        {
            CreateMap<Customer, CustomerResponse>();
            CreateMap<CustomerRequest, Customer>()
                .BeforeMap((src, dest, ctx) => dest.UserId = ctx.Items["UserId"].ToString());
        }
    }
}