using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using PlexWrapped.Services;
using PlexWrapped.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PlexWrapped.ViewModels
{
    public class ArtistsViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "artists";
        public IScreen HostScreen { get; }
        public ReactiveCommand<Unit, Unit> NextCommand;
        
        private readonly IWrappedService wrappedService;

        [Reactive] public List<MediaElementViewModel> Artists { get; set; } = new List<MediaElementViewModel>();

        public ArtistsViewModel(IScreen hostScreen, IWrappedService wrappedService)
        {
            HostScreen = hostScreen;
            this.wrappedService = wrappedService;
            NextCommand = ReactiveCommand.CreateFromTask(Next);
            FetchArtists();
        }

        private async Task Next()
        {
            this.HostScreen.Router.Navigate.Execute(new MediaPageViewModel(HostScreen, wrappedService));
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