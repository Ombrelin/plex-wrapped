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
            var browserOpenerMock = new Mock<IBrowserOpener>();
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
            var result = await api.Authenticate("AppName", "clientUniqueId");

            // Then
            Assert.Equal(123456789, result.Id);
            Assert.Equal("azerty", result.Code);
            Assert.Equal("AppName", result.Product);
            Assert.Equal(false, result.Trusted);
            Assert.Equal("clientUniqueId", result.ClientIdentifier);
            Assert.Equal(1897, result.ExpiresIn);
            Assert.Equal("plexauthtoken", result.AuthToken);
            Assert.Equal(null, result.NewRegistration);
        }

        [Fact]
        public async Task GetUserProfile()
        {
            // Given
            PlexClientApi api = new PlexClientApi(new HttpClient(new PlexApiFakeHandler()), browserOpener);
            await api.Authenticate("AppName", "clientUniqueId");

            // When
            var result = await api.GetProfile();

            Assert.Equal(123456789, result.Id);
            Assert.Equal("plexauthtoken", result.AuthToken);
            Assert.Equal(false, result.Confirmed);
            Assert.Equal("FR", result.Country);
            Assert.Equal("user@provider.com", result.Email);
            Assert.Equal(false, result.Guest);
            Assert.Equal(true, result.Home);
            Assert.Equal("pinid", result.Pin);
            Assert.Equal(true, result.Protected);
            Assert.Equal(false, result.Restricted);
            Assert.Equal("thumbnail url", result.Thumb);
            Assert.Equal("User Full Name", result.Title);
            Assert.Equal("User name", result.Username);
            Assert.Equal(3, result.CertificateVersion);
            Assert.Equal(true, result.HasPassword);
            Assert.Equal(true, result.HomeAdmin);
            Assert.Equal(5, result.HomeSize);
            Assert.Equal("", result.ScrobbleTypes);
            Assert.Equal("Lifetime Plex Pass", result.SubscriptionDescription);
            Assert.Equal(false, result.EmailOnlyAuth);
            Assert.Equal(true, result.MailingListActive);
            Assert.Equal("active", result.MailingListStatus);
            Assert.Equal(15, result.MaxHomeSize);
            Assert.Equal(1622209087, result.RememberExpiresAt);
        }

        [Fact]
        public async Task GetServers()
        {
            // Given
            PlexClientApi api = new PlexClientApi(new HttpClient(new PlexApiFakeHandler()), browserOpener);
            await api.Authenticate("AppName", "clientUniqueId");

            // When
            var result = await api.GetServers();

            // Then
            Assert.Single(result);
            Assert.Equal("plexauthtoken", result[0].AccessToken);
            Assert.Equal("VOSTOK", result[0].Name);
            Assert.Equal("ip address", result[0].Address);
            Assert.Equal(1, result[0].Port);
            Assert.Equal("1.23.0.4497-a1b1f3c10", result[0].Version);
            Assert.Equal("http", result[0].Scheme);
            Assert.Equal("ip address", result[0].Host);
            Assert.Equal("ipaddress1,ipaddress2", result[0].LocalAddresses);
            Assert.Equal("machineid", result[0].MachineIdentifier);
            Assert.Equal(1590584258, result[0].CreatedAt);
            Assert.Equal(1621708154, result[0].UpdatedAt);
            Assert.True(result[0].IsOwned);
        }
    }
}