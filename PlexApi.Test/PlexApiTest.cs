using System;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using PlexApi.BrowserAuth;
using Xunit;

namespace PlexApi.Test
{
    public class PlexApiTest
    {
        private IBrowserOpener browserOpener;

        public PlexApiTest()
        {
            var browserOpenerMock  = new Mock<IBrowserOpener>();
            browserOpenerMock
                .Setup(mock => mock.OpenBrowser(It.IsAny<string>()))
                .Returns<string>(param => Task.CompletedTask);

            this.browserOpener = browserOpenerMock.Object;
        }
        
        [Fact]
        public async Task Authenticate_ReturnsToken()
        {
            // Given
            IPlexApi api = new PlexClientApi(new HttpClient(new PlexApiFakeHandler()), browserOpener);

            // When
            var result = await api.Authenticate("AppName","clientUniqueId");
            
            // Then
            Assert.Equal(123456789, result.Id);
            Assert.Equal("azerty", result.Code );
            Assert.Equal("AppName", result.Product);
            Assert.Equal(false, result.Trusted);
            Assert.Equal("clientUniqueId", result.ClientIdentifier);
            Assert.Equal(1897, result.ExpiresIn);
            Assert.Equal("plexauthtoken",result.AuthToken);
            Assert.Equal(null,result.NewRegistration);
        }

        [Fact]
        public async Task GetUserProfile()
        {
            // Given
            PlexClientApi api = new PlexClientApi(new HttpClient(new PlexApiFakeHandler()), browserOpener);
            await api.Authenticate("AppName","clientUniqueId");
            
            // When
            var result = await api.GetProfile();
            
            Assert.Equal(123456789, result.Id);
            Assert.Equal("plexauthtoken", result.AuthToken);
            Assert.Equal(false, result.Confirmed);
            Assert.Equal("FR",result.Country);
            Assert.Equal("user@provider.com", result.Email);
            Assert.Equal(false, result.Guest);
            Assert.Equal(true, result.Home);
            Assert.Equal("pinid", result.Pin);
            Assert.Equal(true,result.Protected);
            Assert.Equal(false, result.Restricted);
            Assert.Equal("thumbnail url", result.Thumb);
            Assert.Equal("User Full Name", result.Title);
            Assert.Equal("User name", result.Username);
            Assert.Equal(3, result.CertificateVersion);
            Assert.Equal(true, result.HasPassword);
            Assert.Equal(true, result.HomeAdmin);
            Assert.Equal(5,result.HomeSize);
            Assert.Equal("", result.ScrobbleTypes);
            Assert.Equal("Lifetime Plex Pass", result.SubscriptionDescription);
            Assert.Equal(false, result.EmailOnlyAuth);
            Assert.Equal(true, result.MailingListActive);
            Assert.Equal("active", result.MailingListStatus);
            Assert.Equal(15, result.MaxHomeSize);
            Assert.Equal(1622209087, result.RememberExpiresAt);
        }
    }
}