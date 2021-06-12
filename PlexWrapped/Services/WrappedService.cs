using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PlexApi;
using PlexApi.Model;
using PlexWrapped.Models;
using TautulliApi;
using TautulliApi.Model;

namespace PlexWrapped.Services
{
    public class WrappedService : IWrappedService
    {
        private readonly IPlexApi plexApi;
        private readonly ITautulliApi tautulliApi;
        public Server? SelectedServer { get; set; }

        private List<Play>? Data;
        
        public WrappedService(IPlexApi plexApi, ITautulliApi tautulliApi)
        {
            this.plexApi = plexApi;
            this.tautulliApi = tautulliApi;
        }

        public List<MediaElement> GetMostPlayedArtists(int number)
        {
            using WebClient client = new();
            return this.Data
                .Where(play => play.MediaType == "track")
                .GroupBy(g => new {Name = g.GrandparentTitle, ThumbId = g.GrandparentRatingKey})
                .Select(g =>  new MediaElement(g.Key.Name, GetArtistThumbnailAddress(g.Key.ThumbId),g.Count()))
                .OrderByDescending(artist => artist.Count)
                .Take(number)
                .ToList();
        }

        private string GetArtistThumbnailAddress(int thumbId) => $"http://{SelectedServer.Address}:{SelectedServer.Port}/library/metadata/{thumbId}/thumb?X-Plex-Token={plexApi.PlexToken}";
        private string GetMediaThumbnailAddress(string thumb) => $"https://{SelectedServer.Address}:{SelectedServer.Port}{thumb}";

        public List<MediaElement> GetMostPlayedMedias(int number)
        {
            throw new NotImplementedException();
        }

        public List<MediaElement> GetMostPlayedDecades(int number)
        {
            throw new NotImplementedException();
        }

        public List<DayOfWeek> GetDaysOfWeekWithMostPlays(int number)
        {
            throw new NotImplementedException();
        }

        public List<DayOfWeek> GetHourOfDayWithMostPlays(int number)
        {
            throw new NotImplementedException();
        }

        public async Task LoadData(string user, int year, string mediaType)
        {
            this.Data = await this.tautulliApi.GetPlaysHistory(user, year, mediaType);
        }

        public Task<List<Server>> GetServers()
        {
            return this.plexApi.GetServers();
        }
    }
}