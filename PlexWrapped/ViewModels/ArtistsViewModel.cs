using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PlexWrapped.Models;
using PlexWrapped.Services;
using PlexWrapped.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using TautulliApi;

namespace PlexWrapped.ViewModels
{
    public class ArtistsViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "artists";
        public IScreen HostScreen { get; }
        private readonly IWrappedService wrappedService;

        [Reactive] public List<MediaElementViewModel> Artists { get; set; } = new List<MediaElementViewModel>();
        
        public ArtistsViewModel(IScreen hostScreen, IWrappedService wrappedService)
        {
            HostScreen = hostScreen;
            this.wrappedService = wrappedService;
            FetchArtists();
        }

        private async Task FetchArtists()
        {
            var artists = wrappedService.GetMostPlayedArtists(5);
            Artists = artists
                .Select(artist => new MediaElementViewModel(artist))
                .ToList();
        }
    }
}