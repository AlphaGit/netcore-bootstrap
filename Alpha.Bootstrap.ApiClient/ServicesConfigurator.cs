using System;
using System.Collections.Generic;
using Alpha.Bootstrap.ApiClient.Implementations;

namespace Alpha.Bootstrap.ApiClient
{
    /// <summary>
    /// Use this class to obtain the list of classes to register in your IoC container.
    /// </summary>
    public static class ServicesConfigurator
    {
        /// <summary>
        /// Obtain the list of classes to register in your IoC container.
        /// </summary>
        /// <returns>
        /// Dictionary with keys being the interfaces types and
        /// values being the implementation types.
        /// </returns>
        public static IDictionary<Type, Type> ServiceRegistrations = new Dictionary<Type, Type>()
        {
            { typeof(IApiClient), typeof(Implementations.ApiClient) },
            { typeof(IAuthenticationClient), typeof(AuthenticationClient) },
            { typeof(IPostsClient), typeof(PostsClient) },
            { typeof(IRestClient), typeof(RestClient) },
        };
    }
}
