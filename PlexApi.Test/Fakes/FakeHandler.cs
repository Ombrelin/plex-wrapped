using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PlexApi.Test
{
    public abstract class FakeHandler : HttpClientHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(SendAsync(request.Method, request.RequestUri.PathAndQuery));
        }

        public abstract HttpResponseMessage SendAsync(HttpMethod method, string url);
    }
}