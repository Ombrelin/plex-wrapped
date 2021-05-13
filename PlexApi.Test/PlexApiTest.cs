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
        [Fact]
        public async Task Authenticate_ReturnsToken()
        {
            // Given
            var browserOpenerMock = new Mock<IBrowserOpener>();
            browserOpenerMock
                .Setup(mock => mock.OpenBrowser(It.IsAny<string>()))
                .Returns<string>(param => Task.CompletedTask);
            
            IPlexApi api = new PlexClientApi(new HttpClient(new CreatePinFakeHandler()), browserOpenerMock.Object);

            // When
            var result = await api.Authenticate("AppName","clientUniqueId");
            
            // Then
            Assert.Equal(123456789, result.Id);
            Assert.Equal("azerty", result.Code );
            Assert.Equal("AppName", result.Product);
            Assert.Equal(false, result.Trusted);
            Assert.Equal("clientUniqueId", result.ClientIdentifier);
            Assert.Equal(1897, result.ExpiresIn);
            Assert.Equal("plex auth token",result.AuthToken);
            Assert.Equal(null,result.NewRegistration);
        }
    }
}