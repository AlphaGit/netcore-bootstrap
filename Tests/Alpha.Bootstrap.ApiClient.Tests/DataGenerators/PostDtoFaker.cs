using System;
using System.Collections.Generic;
using Alpha.Bootstrap.WebApi.Dtos.v1;
using Bogus;

namespace Alpha.Bootstrap.ApiClient.Tests.DataGenerators
{
    class PostDtoFaker
    {
        private readonly Faker<PostDto> _faker;

        public PostDtoFaker()
        {
            _faker = new Faker<PostDto>()
                .Rules((f, p) =>
                {
                    p.Id = Guid.NewGuid();
                    p.Content = f.Lorem.Paragraphs(1, 5);
                    p.Title = f.Lorem.Sentence();
                });
        }

        public ICollection<PostDto> Generate(int count)
            => _faker.Generate(count);
    }
}
