using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using PlexApi.BrowserAuth;
using PlexApi.Model;
using PlexApi.PlexTv;
using Refit;

namespace PlexApi
{
    public class PlexClientApi : IPlexApi
    {
        private readonly IBrowserOpener browserOpener;
        private readonly IPlexTvApi plexTvApiJson;
        private readonly IPlexTvApi plexTvApiXml;
        private string plexAuthToken;

        public PlexClientApi(HttpClient client, IBrowserOpener browserOpener)
        {
            this.browserOpener = browserOpener;
            client.BaseAddress = new Uri("https://plex.tv");
            plexTvApiJson = RestService.For<IPlexTvApi>(client);
            plexTvApiXml = RestService.For<IPlexTvApi>(client, new RefitSettings
            {
                ContentSerializer = new XmlContentSerializer(
                    new XmlContentSerializerSettings
                    {
                        XmlReaderWriterSettings = new XmlReaderWriterSettings()
                        {
                            ReaderSettings = new XmlReaderSettings
                            {
                                IgnoreWhitespace = true
                            }
                        }
                    }
                )
            });
        }

        public async Task<PlexAuth> Authenticate(string productName, string clientId)
        {
            var plexAuth = await plexTvApiJson.CreateAuthPin(true, productName, clientId);

            var loginUrl = $"https://app.plex.tv/auth#?clientID={clientId}&code={plexAuth.Code}";
            await browserOpener.OpenBrowser(loginUrl);

            var pinValidated = false;
            while (!pinValidated)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                plexAuth = await plexTvApiJson.ValidateAuthPin(plexAuth.Id, clientId);
                pinValidated = plexAuth.AuthToken != null;
            }

            this.plexAuthToken = plexAuth.AuthToken;
            return plexAuth;
        }

        public Task<PlexUserProfile> GetProfile()
        {
            EnsureAuthenticated();
            return this.plexTvApiJson.GetProfile(this.plexAuthToken);
        }

        public async Task<List<Server>> GetServers()
        {
            EnsureAuthenticated();
            var serverList = await this.plexTvApiXml.GetServers(this.plexAuthToken);
            return serverList.Servers;
        }

        private void EnsureAuthenticated()
        {
            if (this.plexAuthToken is null)
            {
                throw new InvalidOperationException("Action requires authentication");
            }
        }
    }
}