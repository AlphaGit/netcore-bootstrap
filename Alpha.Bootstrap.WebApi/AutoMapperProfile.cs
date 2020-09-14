using Alpha.Bootstrap.Logic.Models;
using Alpha.Bootstrap.WebApi.Dtos.v1.Posts;
using AutoMapper;

namespace Alpha.Bootstrap.WebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostDto>();
        }
    }
}
