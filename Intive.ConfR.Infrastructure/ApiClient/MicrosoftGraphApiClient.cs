using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Intive.ConfR.Application.Exceptions;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Intive.ConfR.Infrastructure.ApiClient
{
    public class MicrosoftGraphApiClient
    {
        private string CreateQueryString(Dictionary<string, string> queryParameters)
        {
            var query = "?";

            var builder = new StringBuilder().Append(query);

            foreach (KeyValuePair<string, string> queryPair in queryParameters)
            {
                var queryConcatenate = string.Concat(queryPair.Key, "=", queryPair.Value, "&");
                query = builder.Append(queryConcatenate).ToString();
            }

            return query.Remove(query.Length - 1);
        }

        private string UrlConcatenate(GraphApiBaseRequest request)
        {
            var endpoint = string.Concat(request.BaseAddress, request.GraphVersion, request.Endpoint);
            return endpoint;
        }

        private string GetToken(string header)
        {
            return header.Replace("Bearer", string.Empty).Trim();
        }

        /// <summary>
        /// Send GET request to MS Graph API.
        /// </summary>
        /// <typeparam name="T">response model</typeparam>
        /// <param name="request">object of <see cref="GraphApiGetRequest"/> with request parameters</param>
        /// <param name="httpContext">http context with MS Graph access token</param>
        /// <returns>response from MS Graph</returns>
        /// <exception cref="GraphApiException"></exception> 
        public async Task<T> Get<T>(GraphApiGetRequest request, HttpContext httpContext)
        {
            string uri = UrlConcatenate(request);

            if (request.QueryParameters != null)
            {
                uri = string.Concat(uri, CreateQueryString(request.QueryParameters));
            }

            var accessToken = httpContext.Request.Headers["Access_token"].ToString();
            var prefer = httpContext.Request.Headers.SingleOrDefault(h => h.Key == "Prefer");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                if (!string.IsNullOrWhiteSpace(prefer.Key))
                {
                    httpClient.DefaultRequestHeaders.Add(prefer.Key, prefer.Value.ToString());
                }

                var getResult = await httpClient.GetAsync(uri);
                var content = await getResult.Content.ReadAsStringAsync();

                if (!getResult.IsSuccessStatusCode)
                {
                    var graphError = JsonConvert.DeserializeObject<GraphError>(content);

                    throw new GraphApiException(getResult.StatusCode, graphError.Error.Message);
                }

                var result = JsonConvert.DeserializeObject<T>(content);
                return result;
            }
        }

        /// <summary>
        /// Send POST request to MS Graph API.
        /// </summary>
        /// <typeparam name="T">response model</typeparam>
        /// <typeparam name="B">POST request body</typeparam>
        /// <param name="request">object of <see cref="GraphApiPostRequest{T}"/> with request parameters</param>
        /// <param name="httpContext">http context with MS graph access token</param>
        /// <returns>response from MS Graph</returns>
        /// <exception cref="GraphApiException"></exception>  
        public async Task<T> Post<T, B>(GraphApiPostRequest<B> request, HttpContext httpContext)
        {
            string uri = UrlConcatenate(request);

            var accessToken = httpContext.Request.Headers["Access_token"].ToString();

            using (var httpClient = new HttpClient())
            {
                var jsonBody = JsonConvert.SerializeObject(request.Body);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var postResult = await httpClient.PostAsync(uri, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
                var content = await postResult.Content.ReadAsStringAsync();

                if (!postResult.IsSuccessStatusCode)
                {
                    var graphError = JsonConvert.DeserializeObject<GraphError>(content);

                    throw new GraphApiException(postResult.StatusCode, graphError.Error.Message);
                }

                return JsonConvert.DeserializeObject<T>(content);
            }
        }

        /// <summary>
        /// Send DELETE request to MS Graph API
        /// </summary>
        /// <param name="request">object of <see cref="GraphApiDeleteRequest"/> with request parameters</param>
        /// <param name="httpContext">http context with MS graph access token</param>
        /// <exception cref="GraphApiException"></exception>  
        public async Task Delete(GraphApiDeleteRequest request, HttpContext httpContext)
        {
            string uri = UrlConcatenate(request);

            if (request.Id != null)
            {
                uri = string.Concat(uri, "/", request.Id);
            }

            var accessToken = httpContext.Request.Headers["Access_token"].ToString();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var deleteResult = await httpClient.DeleteAsync(uri);
                var content = await deleteResult.Content.ReadAsStringAsync();

                if (!deleteResult.IsSuccessStatusCode)
                {
                    var graphError = JsonConvert.DeserializeObject<GraphError>(content);

                    throw new GraphApiException(deleteResult.StatusCode, graphError.Error.Message);
                }
            }
        }

        /// <summary>
        /// Send PATCH request to MS Graph API
        /// </summary>
        /// <typeparam name="T">response model</typeparam>
        /// <typeparam name="B">PATCH request body</typeparam>
        /// <param name="request">object of <see cref="GraphApiPatchRequest{T}"/> with request parameters</param>
        /// <param name="httpContext">http context with MS graph access token</param>
        /// <returns>response from MS Graph</returns>
        /// <exception cref="GraphApiException"></exception>
        public async Task<T> Patch<T, B>(GraphApiPatchRequest<B> request, HttpContext httpContext)
        {
            string uri = UrlConcatenate(request);

            if (request.Id != null)
            {
                uri = string.Concat(uri, "/", request.Id);
            }

            var accessToken = httpContext.Request.Headers["Access_token"].ToString();

            using (var httpClient = new HttpClient())
            {
                var jsonBody = JsonConvert.SerializeObject(request.Body);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var patchResult = await httpClient.SendAsync(new HttpRequestMessage(new HttpMethod("PATCH"), uri)
                {
                    Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                });

                var content = await patchResult.Content.ReadAsStringAsync();

                if (!patchResult.IsSuccessStatusCode)
                {
                    var graphError = JsonConvert.DeserializeObject<GraphError>(content);

                    throw new GraphApiException(patchResult.StatusCode, graphError.Error.Message);
                }

                return JsonConvert.DeserializeObject<T>(content);
            }
        }
    }
}
