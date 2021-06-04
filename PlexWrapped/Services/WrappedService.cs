using System;
using System.Collections.Generic;
using System.Linq;
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
        public Server SelectedServer { get; set; }

        private List<Play> Data;
        
        public WrappedService(IPlexApi plexApi, ITautulliApi tautulliApi)
        {
            this.plexApi = plexApi;
            this.tautulliApi = tautulliApi;
        }

        public List<MediaElement> GetMostPlayedArtists(int number)
        {
            return this.Data
                .Where(play => play.MediaType == "track")
                .GroupBy(g => new {Name = g.GrandparentTitle, Thumb = g.Thumb})
                .Select(g => new MediaElement(g.Key.Name, GetThumbnailAddress(g.Key.Thumb),g.Count()))
                .OrderByDescending(artist => artist.Count)
                .Take(number)
                .ToList();
        }

        private string GetThumbnailAddress(string thumb) => $"https://{SelectedServer.Address}:{SelectedServer.Port}{thumb}";

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