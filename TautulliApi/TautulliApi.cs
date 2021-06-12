using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using TautulliApi.Model;
using TautulliApi.Tautulli;

namespace TautulliApi
{
    public class TautulliApi : ITautulliApi
    {
        private readonly ITautulliStatsApi tautulliApi;
        private readonly string tautulliApiKey;

        public TautulliApi(HttpClient client, string tautulliApiKey, string tautulliUrl)
        {
            this.tautulliApiKey = tautulliApiKey;
            client.BaseAddress = new Uri(tautulliUrl);
            tautulliApi = RestService.For<ITautulliStatsApi>(client, new RefitSettings()
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer()
            });
        }


        public async Task<List<Play>> GetPlaysHistory(string username, int year, string mediaType)
        {
            var countResult = await tautulliApi.GetHistory(this.tautulliApiKey, username, 0, mediaType);
            var actualResult = await tautulliApi.GetHistory(
                this.tautulliApiKey,
                username,
                countResult.Response.Data.RecordsFiltered,
                mediaType
            );

            return actualResult.Response.Data.Data
                .Where(play => DateTimeOffset.FromUnixTimeSeconds(play.Date).UtcDateTime.Year == year)
                .ToList();
        }
    }
}