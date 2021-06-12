using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlexApi.Model;
using PlexWrapped.Models;

namespace PlexWrapped.Services
{
    public interface IWrappedService
    {
        List<MediaElement> GetMostPlayedArtists(int number);
        List<MediaElement> GetMostPlayedMedias(int number);
        List<MediaElement> GetMostPlayedDecades(int number);
        List<DayOfWeek> GetDaysOfWeekWithMostPlays(int number);
        List<DayOfWeek> GetHourOfDayWithMostPlays(int number);
        Task LoadData(string user, int year, string mediaType);
        Task<List<Server>> GetServers();
        Server? SelectedServer { get; set; }
    }
}