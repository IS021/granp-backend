using AutoMapper;

using Granp.DTOs;
using Granp.Models.Entities;
using Granp.Models.Types;

namespace Granp.DTOs.Mappers
{
    public class SearchFilterProfile : Profile
    {
        public SearchFilterProfile()
        {
            CreateMap<SearchFilterRequest, SearchFilter>()
                .BeforeMap((src, dest, ctx) =>
                    {
                        dest.Location = ctx.Items["Location"] as Location;
                    });
        }
    }
}