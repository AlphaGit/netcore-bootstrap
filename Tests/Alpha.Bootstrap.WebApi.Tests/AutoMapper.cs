using Alpha.Bootstrap.WebApi.Tests.Fixtures;
using Xunit;

namespace Alpha.Bootstrap.WebApi.Tests
{
    [Collection(nameof(MapperFixture))]
    public class AutoMapper
    {
        private readonly MapperFixture _mapperFixture;

        public AutoMapper(MapperFixture mapperFixture)
        {
            _mapperFixture = mapperFixture;
        }

        [Fact]
        public void AllMappingsAreValid()
        {
            _mapperFixture.MapperConfiguration.AssertConfigurationIsValid();
        }
    }
}