using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using PlexApi.Model;
using Xunit;

namespace PlexApi.Test
{
    public class CreatePinFakeHandler : FakeHandler
    {
        public override HttpResponseMessage SendAsync(HttpMethod method, string url)
        {
            if (method == HttpMethod.Post && url.Contains("/api/v2/pins.json"))
            {
                return CreatePin(url);
            }
            else if (method == HttpMethod.Get && url.Contains("/api/v2/pins/123456789.json"))
            {
                return ValidatePin(url);
            }

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        private HttpResponseMessage ValidatePin(string url)
        {
            Assert.Contains("X-Plex-Client-Identifier=clientUniqueId", url);

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Created,
                Content = new StringContent(
                    JsonSerializer.Serialize(new PlexAuth()
                    {
                        Id = 123456789,
                        Code = "azerty",
                        Product = "AppName",
                        Trusted = false,
                        ClientIdentifier = "clientUniqueId",
                        ExpiresIn = 1897,
                        CreatedAt = DateTime.Now,
                        ExpiresAt = DateTime.Now,
                        AuthToken = "plex auth token",
                        NewRegistration = null
                    }))
            };
        }

        private HttpResponseMessage CreatePin(string url)
        {
            Assert.Contains("strong=True", url);
            Assert.Contains("X-Plex-Product=", url);
            Assert.Contains("X-Plex-Client-Identifier=", url);

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Created,
                Content = new StringContent(
                    JsonSerializer.Serialize(new PlexAuth()
                    {
                        Id = 123456789,
                        Code = "azerty",
                        Product = "AppName",
                        Trusted = false,
                        ClientIdentifier = "clientUniqueId",
                        ExpiresIn = 1897,
                        CreatedAt = DateTime.Now,
                        ExpiresAt = DateTime.Now,
                        AuthToken = null,
                        NewRegistration = null
                    }))
            };
        }
    }
}