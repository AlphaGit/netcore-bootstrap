using System;

namespace Alpha.Bootstrap.WebApi.Dtos.v1.Posts
{
    public class PostDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}