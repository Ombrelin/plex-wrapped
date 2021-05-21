using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TautulliApi.Test
{
    public class TautulliApiTests
    {
        [Fact]
        public async Task GetHistory_ReturnsHitory()
        {
            // Given
            var fakeHandler = new TautulliApiFakeHandler();
            ITautulliApi api = new TautulliApi(new HttpClient(fakeHandler), "tautulliapikey", "https://fakeurl.com");
            
            // When
            var result = await api.GetPlaysHistory("Arsène Lapostolet", 2021,"track");
            
            // Then
            Assert.Single(result);
            Assert.Contains(result, play => play.Id == 11660);
        } 
    }
}