using ReactiveUI;
using Splat;
using TautulliApi;

namespace PlexWrapped.ViewModels
{
    public class ArtistsViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "artists";
        public IScreen HostScreen { get; }
        private readonly ITautulliApi tautulliApi;
        
        public ArtistsViewModel(IScreen hostScreen, ITautulliApi tautulliApi)
        {
            HostScreen = hostScreen;
            this.tautulliApi = tautulliApi;
        }


    }
}