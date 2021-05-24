using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlexApi;
using PlexWrapped.Models;
using TautulliApi;

namespace PlexWrapped.Services
{
    public class WrappedService : IWrappedService
    {
        private readonly IPlexApi plexApi;
        private readonly ITautulliApi tautulliApi;

        public WrappedService(IPlexApi plexApi, ITautulliApi tautulliApi)
        {
            this.plexApi = plexApi;
            this.tautulliApi = tautulliApi;
        }

        public Task<List<MediaElement>> GetMostPlayedArtists(int number)
        {
            throw new NotImplementedException();
        }

        public Task<List<MediaElement>> GetMostPlayedMedias(int number)
        {
            throw new NotImplementedException();
        }

        public Task<List<MediaElement>> GetMostPlayedDecades(int number)
        {
            throw new NotImplementedException();
        }

        public Task<List<DayOfWeek>> GetDaysOfWeekWithMostPlays(int number)
        {
            throw new NotImplementedException();
        }

        public Task<List<DayOfWeek>> GetHourOfDayWithMostPlays(int number)
        {
            throw new NotImplementedException();
        }
    }
}