namespace Intive.ConfR.Infrastructure.ApiClient
{
    public class GraphApiPostRequest<T> : GraphApiBaseRequest
    {
        public T Body { get; set; }
    }
}
