using Alpha.Bootstrap.WebApi.Dtos.v1.Posts;
using Bogus;

namespace Alpha.Bootstrap.ApiClient.Tests.Fakers
{
    class GetAllPostsResponseFaker
    {
        private readonly Faker<GetAllPostsResponse> _faker;

        public GetAllPostsResponseFaker()
        {
            _faker = new Faker<GetAllPostsResponse>()
                .Rules((f, r) =>
                {
                    var postFaker = new PostDtoFaker();
                    r.Posts = postFaker.Generate(10);
                });
        }

        public GetAllPostsResponse Generate()
            => _faker.Generate();
    }
}
