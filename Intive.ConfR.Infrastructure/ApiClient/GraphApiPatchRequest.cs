namespace Intive.ConfR.Infrastructure.ApiClient
{
    public class GraphApiPatchRequest<T> : GraphApiBaseRequest
    {
        public string Id { get; set; }
        public T Body { get; set; }
    }
}
