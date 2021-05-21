using System.Threading.Tasks;
using Refit;
using TautulliApi.Model;

namespace TautulliApi.Tautulli
{
    public interface ITautulliStatsApi
    {
        [Get("/api/v2?cmd=get_history")]
        Task<TautulliResult> GetHistory(
            [AliasAs("apikey")] string apikey, 
            [AliasAs("user")] string username, 
            [AliasAs("length")] int recordNumber,
            [AliasAs("media_type")] string mediaType
        );
    }
}