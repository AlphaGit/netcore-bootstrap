﻿using System.Collections.Generic;
using Alpha.Bootstrap.Logic.Models;

namespace Alpha.Bootstrap.Logic.Features.Posts.GetAll
{
    public class Response
    {
        public ICollection<Post> Posts { get; set; }
    }
}
