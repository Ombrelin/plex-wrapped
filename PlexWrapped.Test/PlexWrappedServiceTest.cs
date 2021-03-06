using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using PlexApi;
using PlexApi.Model;
using PlexWrapped.Services;
using TautulliApi;
using TautulliApi.Model;
using Xunit;

namespace PlexWrapped.Test
{
    public class PlexWrappedServiceTest
    {
        private List<Play> plays;

        [Fact]
        public async Task GetServers_ReturnsPlexApiServers()
        {
            // Given
            var plexApiMock = new Mock<IPlexApi>();
            var tautulliApiMock = new Mock<ITautulliApi>();

            plexApiMock.Setup(x => x.GetServers())
                .Returns(() => Task.FromResult(
                    new List<Server>()
                    {
                        new Server() {Name = "Test Server 1"},
                        new Server() {Name = "Test Server 2"}
                    })
                );

            var plexWrappedService = new WrappedService(plexApiMock.Object, tautulliApiMock.Object);

            // When
            var result = await plexWrappedService.GetServers();

            // Then
            Assert.Equal(2, result.Count);
            Assert.Equal("Test Server 1", result[0].Name);
            Assert.Equal("Test Server 2", result[1].Name);
        }

        [Fact]
        public void SetServer_SetsSelectedServer()
        {
            // Given
            var plexApiMock = new Mock<IPlexApi>();
            var tautulliApiMock = new Mock<ITautulliApi>();

            var plexWrappedService = new WrappedService(plexApiMock.Object, tautulliApiMock.Object)
            {
                SelectedServer = new Server() {Name = "Test Server 1"}
            };

            // When

            // Then
            Assert.Equal("Test Server 1", plexWrappedService.SelectedServer.Name);
        }

        [Fact]
        public async Task GetMostPlayedArtists_ThreeArtists_ReturnsThreeMostPlayedArtistsOrderedByPlayCount()
        {
            // Given
            var plexApiMock = new Mock<IPlexApi>();
            var tautulliApiMock = new Mock<ITautulliApi>();
            plexApiMock.Setup(mock => mock.PlexToken).Returns(() => "token");
            tautulliApiMock.Setup(x => x.GetPlaysHistory("Ars??ne lapostolet", 2021, "track"))
                .Returns(() => Task.FromResult(GeneratePlaysArtists()));

            var plexWrappedService = new WrappedService(plexApiMock.Object, tautulliApiMock.Object)
            {
                SelectedServer = new Server() {Address = "192.192.192.192", Port = 8080}
            };
            await plexWrappedService.LoadData("Ars??ne lapostolet", 2021, "track");

            // When
            var result = plexWrappedService.GetMostPlayedArtists(3);

            // Then
            Assert.Equal(3, result.Count);

            Assert.Equal("Test Artist 1", result[0].Name);
            Assert.Equal("http://192.192.192.192:8080/library/metadata/1/thumb?X-Plex-Token=token",
                result[0].ThumbnailUrl);

            Assert.Equal("Test Artist 2", result[1].Name);
            Assert.Equal("http://192.192.192.192:8080/library/metadata/2/thumb?X-Plex-Token=token",
                result[1].ThumbnailUrl);

            Assert.Equal("Test Artist 3", result[2].Name);
            Assert.Equal("http://192.192.192.192:8080/library/metadata/3/thumb?X-Plex-Token=token",
                result[2].ThumbnailUrl);
        }

        [Fact]
        public async Task GetMostPlayedTitles_ThreeTitle_ReturnsThreeMostPlayedTitleOrderedByPlayCount()
        {
            // Given
            var plexApiMock = new Mock<IPlexApi>();
            var tautulliApiMock = new Mock<ITautulliApi>();
            plexApiMock.Setup(mock => mock.PlexToken).Returns(() => "token");
            tautulliApiMock.Setup(x => x.GetPlaysHistory("Ars??ne lapostolet", 2021, "track"))
                .Returns(() => Task.FromResult(GeneratePlaysTitles()));

            var plexWrappedService = new WrappedService(plexApiMock.Object, tautulliApiMock.Object)
            {
                SelectedServer = new Server() {Address = "192.192.192.192", Port = 8080}
            };
            await plexWrappedService.LoadData("Ars??ne lapostolet", 2021, "track");

            // When
            var result = plexWrappedService.GetMostPlayedMedias(3);

            // Then
            Assert.Equal(3, result.Count);

            Assert.Equal("Test Title 1", result[0].Name);
            Assert.Equal("http://192.192.192.192:8080/thumb/1?X-Plex-Token=token",
                result[0].ThumbnailUrl);

            Assert.Equal("Test Title 2", result[1].Name);
            Assert.Equal("http://192.192.192.192:8080/thumb/2?X-Plex-Token=token",
                result[1].ThumbnailUrl);

            Assert.Equal("Test Title 3", result[2].Name);
            Assert.Equal("http://192.192.192.192:8080/thumb/3?X-Plex-Token=token",
                result[2].ThumbnailUrl);
        }


        public List<Play> GeneratePlaysArtists()
        {
            return new List<Play>()
            {
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 1",
                    GrandparentTitle = "Test Artist 1", Thumb = "/thumb/1", GrandparentRatingKey = 1
                },
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 2",
                    GrandparentTitle = "Test Artist 2", Thumb = "/thumb/2", GrandparentRatingKey = 2
                },
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 3",
                    GrandparentTitle = "Test Artist 2", Thumb = "/thumb/2", GrandparentRatingKey = 2
                },
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 4",
                    GrandparentTitle = "Test Artist 2", Thumb = "/thumb/2", GrandparentRatingKey = 2
                },
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 5",
                    GrandparentTitle = "Test Artist 1", Thumb = "/thumb/1", GrandparentRatingKey = 1
                },
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 6",
                    GrandparentTitle = "Test Artist 1", Thumb = "/thumb/1", GrandparentRatingKey = 1
                },
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 7",
                    GrandparentTitle = "Test Artist 3", Thumb = "/thumb/3",
                    GrandparentRatingKey = 3
                },
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 8",
                    GrandparentTitle = "Test Artist 3", Thumb = "/thumb/3", GrandparentRatingKey = 3
                },
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 9",
                    GrandparentTitle = "Test Artist 4", Thumb = "/thumb/4", GrandparentRatingKey = 4
                },
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 10",
                    GrandparentTitle = "Test Artist 1", Thumb = "/thumb/1", GrandparentRatingKey = 1
                }
            };
        }

        public List<Play> GeneratePlaysTitles()
        {
            return new List<Play>()
            {
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 1",
                    GrandparentTitle = "Test Artist 1", Thumb = "/thumb/1", GrandparentRatingKey = 1
                },
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 1",
                    GrandparentTitle = "Test Artist 1", Thumb = "/thumb/1", GrandparentRatingKey = 1
                },
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 1",
                    GrandparentTitle = "Test Artist 1", Thumb = "/thumb/1", GrandparentRatingKey = 1
                },
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 2",
                    GrandparentTitle = "Test Artist 2", Thumb = "/thumb/2", GrandparentRatingKey = 2
                },
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 5",
                    GrandparentTitle = "Test Artist 1", Thumb = "/thumb/1", GrandparentRatingKey = 1
                },
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 3",
                    GrandparentTitle = "Test Artist 3", Thumb = "/thumb/3",
                    GrandparentRatingKey = 3
                },
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 3",
                    GrandparentTitle = "Test Artist 3", Thumb = "/thumb/3",
                    GrandparentRatingKey = 3
                },
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 2",
                    GrandparentTitle = "Test Artist 2", Thumb = "/thumb/2", GrandparentRatingKey = 2
                },
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 9",
                    GrandparentTitle = "Test Artist 4", Thumb = "/thumb/4", GrandparentRatingKey = 4
                },
                new Play()
                {
                    MediaType = "track", Date = 1622812884, Title = "Test Title 2",
                    GrandparentTitle = "Test Artist 2", Thumb = "/thumb/2", GrandparentRatingKey = 2
                },
            };
        }
    }
}