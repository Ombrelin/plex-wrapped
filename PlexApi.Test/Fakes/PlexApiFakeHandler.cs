﻿using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using PlexApi.Model;
using Xunit;

namespace PlexApi.Test
{
    public class PlexApiFakeHandler : FakeHandler
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
            else if (method == HttpMethod.Get && url.Contains("/api/v2/user.json"))
            {
                return GetUser(url);
            }
            else if (method == HttpMethod.Get && url.Contains("pms/servers.xml"))
            {
                return GetServers(url);
            }

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        private HttpResponseMessage GetServers(string url)
        {
            Assert.Contains("X-Plex-Token=plexauthtoken", url);
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"
                    <?xml version=""1.0"" encoding=""utf-16""?>
                    <MediaContainer friendlyName=""myPlex"" identifier=""com.plexapp.plugins.myplex"" machineIdentifier=""c62be90a37d43ecd9596be8abd98a2e485979e31"" size=""2"">
                      <Server accessToken=""plexauthtoken"" name=""VOSTOK"" address=""ip address"" port=""1"" version=""1.23.0.4497-a1b1f3c10"" scheme=""http"" host=""ip address"" localAddresses=""ipaddress1,ipaddress2"" machineIdentifier=""machineid"" createdAt=""1590584258"" updatedAt=""1621708154"" owned=""1"" synced=""0"" />
                      </MediaContainer >
                ")
            };
        }

        private HttpResponseMessage GetUser(string url)
        {

            Assert.Contains("X-Plex-Token=plexauthtoken", url);
            
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    JsonSerializer.Serialize(new PlexUserProfile
                    {
                        Id = 123456789,
                        AuthToken = "plexauthtoken",
                        Confirmed = false,
                        Country = "FR",
                        Email = "user@provider.com",
                        Guest = false,
                        Home = true,
                        Pin = "pinid",
                        Protected = true,
                        Restricted = false,
                        Thumb = "thumbnail url",
                        Title = "User Full Name",
                        Username = "User name",
                        Uuid = Guid.NewGuid().ToString(),
                        CertificateVersion = 3,
                        HasPassword = true,
                        HomeAdmin = true,
                        HomeSize = 5,
                        ScrobbleTypes = "",
                        SubscriptionDescription = "Lifetime Plex Pass",
                        EmailOnlyAuth = false,
                        MailingListActive = true,
                        MailingListStatus = "active",
                        MaxHomeSize = 15,
                        RememberExpiresAt = 1622209087


                    }))
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
                        AuthToken = "plexauthtoken",
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