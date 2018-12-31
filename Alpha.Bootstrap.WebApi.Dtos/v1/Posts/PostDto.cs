using System;

namespace Alpha.Bootstrap.WebApi.Dtos.v1
{
    public class PostDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}