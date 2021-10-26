using System.Collections.Generic;

namespace Intive.ConfR.Infrastructure.ApiClient
{
    public class GraphApiGetRequest : GraphApiBaseRequest
    {
        public Dictionary<string, string> QueryParameters { get; set; }
    }
}
