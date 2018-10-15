using System.Threading.Tasks;

namespace Alpha.Bootstrap.ApiClient
{
    public interface IRestClient
    {
        Task<RestResponse<TResource>> GetAsync<TResource>(string relativeUrl);
        Task<RestResponse<TResource>> PostAsync<TResource>(string relativeUrl, object body);
        Task<RestResponse<TResource>> PutAsync<TResource>(string relativeUrl, object body);
        Task<RestResponse<TResource>> DeleteAsync<TResource>(string relativeUrl);

        Task<RestResponse> PostAsync(string relativeUrl, object body);
        Task<RestResponse> PutAsync(string relativeUrl, object body);
        Task<RestResponse> DeleteAsync(string relativeUrl);
    }
}