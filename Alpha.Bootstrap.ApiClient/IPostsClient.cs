﻿using System;
using System.Threading.Tasks;
using Alpha.Bootstrap.WebApi.Dtos.v1.Posts;

namespace Alpha.Bootstrap.ApiClient
{
    public interface IPostsClient
    {
        Task<RestResponse<GetAllPostsResponse>> GetAllPosts();

        Task<RestResponse> Delete(Guid id);

        Task<RestResponse> Create(CreatePostRequest newPost);

        Task<RestResponse> Update(Guid id, UpdatePostRequest postUpdate);
    }
}