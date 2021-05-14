using System;
using System.Net.Http;
using System.Threading.Tasks;
using PlexApi.BrowserAuth;
using PlexApi.Model;
using PlexApi.PlexTv;
using Refit;

namespace PlexApi
{
    public class PlexClientApi : IPlexApi
    {
        private readonly IBrowserOpener browserOpener;
        private readonly IPlexTvApi plexTvApi;
        private string plexAuthToken;
        
        public PlexClientApi(HttpClient client, IBrowserOpener browserOpener)
        {
            this.browserOpener = browserOpener;
            client.BaseAddress = new Uri("https://plex.tv/");
            plexTvApi = RestService.For<IPlexTvApi>(client);
        }

        public async Task<PlexAuth> Authenticate(string productName, string clientId)
        {
            var plexAuth = await plexTvApi.CreateAuthPin(true, productName, clientId);

            var loginUrl = $"https://app.plex.tv/auth#?clientID={clientId}&code={plexAuth.Code}";
            await browserOpener.OpenBrowser(loginUrl);

            var pinValidated = false;
            while (!pinValidated)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                plexAuth = await plexTvApi.ValidateAuthPin(plexAuth.Id, clientId);
                pinValidated = plexAuth.AuthToken != null;
            }

            this.plexAuthToken = plexAuth.AuthToken;
            return plexAuth;
        }

        public Task<PlexUserProfile> GetProfile() => this.plexTvApi.GetProfile(this.plexAuthToken);
    }
}