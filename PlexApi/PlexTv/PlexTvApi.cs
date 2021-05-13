using System.Threading.Tasks;
using PlexApi.Model;
using Refit;

namespace PlexApi.PlexTv
{
    public interface IPlexTvApi
    {
        [Post("/api/v2/pins.json")]
        Task<PlexAuth> CreateAuthPin(
            bool strong,
            [AliasAs("X-Plex-Product")] string product,
            [AliasAs("X-Plex-Client-Identifier")] string clientId
        );
        
        [Get("/api/v2/pins/{pinId}.json")]
        Task<PlexAuth> ValidateAuthPin(
            [AliasAs("pinId")] int pinId,
            [AliasAs("X-Plex-Client-Identifier")] string clientId
        );
    }
}