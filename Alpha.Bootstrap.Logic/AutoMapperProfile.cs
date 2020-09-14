// ReSharper disable RedundantUsingDirective
// ReSharper disable RedundantNameQualifier
using Features = Alpha.Bootstrap.Logic.Features;
using Logic = Alpha.Bootstrap.Logic.Models;
using DAL = Alpha.Bootstrap.DAL.Models;
using AutoMapper;

namespace Alpha.Bootstrap.Logic
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DAL.Models.Post, Logic.Models.Post>()
                .ReverseMap();

            CreateMap<Features.Posts.Create.CreatePostRequest, DAL.Models.Post>()
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }
}