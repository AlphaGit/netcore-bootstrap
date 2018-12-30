using System;
using System.Collections.Generic;
using Alpha.Bootstrap.Logic.Models;
using Bogus;

namespace Alpha.Bootstrap.WebApi.Tests.Fakers
{
    public class LogicDtoFaker
    {
        private readonly Faker<Post> _faker;

        public LogicDtoFaker()
        {
            _faker = new Faker<Post>()
                .Rules((f, p) =>
                {
                    p.Id = Guid.NewGuid();
                    p.Content = f.Lorem.Paragraphs(1, 5);
                    p.Title = f.Lorem.Sentence();
                });
        }

        public Post Generate()
            => _faker.Generate();

        public ICollection<Post> Generate(int count)
            => _faker.Generate(count);
    }
}