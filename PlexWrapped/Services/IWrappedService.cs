using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlexWrapped.Models;
using TautulliApi.Model;

namespace PlexWrapped.Services
{
    public interface IWrappedService
    {
        Task<List<MediaElement>> GetMostPlayedArtists(int number);
        Task<List<MediaElement>> GetMostPlayedMedias(int number);
        Task<List<MediaElement>> GetMostPlayedDecades(int number);
        Task<List<DayOfWeek>> GetDaysOfWeekWithMostPlays(int number);
        Task<List<DayOfWeek>> GetHourOfDayWithMostPlays(int number);
    }
}