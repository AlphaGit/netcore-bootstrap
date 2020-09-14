using AutoMapper;
using Xunit;

namespace Alpha.Bootstrap.WebApi.Tests.Fixtures
{
    public class MapperFixture
    {
        public Mapper Mapper { get; }

        public MapperConfiguration MapperConfiguration { get; }

        public MapperFixture()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
                cfg.AddProfiles(ServicesConfigurator.AutoMapperProfiles));
            Mapper = new Mapper(MapperConfiguration);
        }
    }

    [CollectionDefinition(nameof(MapperFixture))]
    public class MapperCollectionFixture : ICollectionFixture<MapperFixture> { }
}