namespace Intive.ConfR.Infrastructure.ApiClient
{
    /// <summary>
    /// GraphApiBaseRequest is used to create a request
    /// </summary>
    public abstract class GraphApiBaseRequest
    {
        private const string Uri = "https://graph.microsoft.com/";

        /// <summary>
        /// Host address to which the request is sent
        /// </summary>
        public string BaseAddress => Uri;
        /// <summary>
        /// Version of MS Graph API e.g. beta/
        /// </summary> 
        public string GraphVersion { get; set; }
        /// <summary>
        /// Url endpoint
        /// </summary>
        public string Endpoint { get; set; }
    }
}
