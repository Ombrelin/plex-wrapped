using System;
using System.Reactive;
using System.Threading.Tasks;
using PlexApi;
using ReactiveUI;

namespace PlexWrapped.ViewModels
{
    public class PlexLoginViewModel: ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "plexlogin";
        public IScreen HostScreen { get; }
        public ReactiveCommand<Unit,Task> PlexLoginCommand { get; }
        private readonly IPlexApi plexApi;
        
        public PlexLoginViewModel(IScreen hostScreen, IPlexApi plexApi)
        {
            HostScreen = hostScreen;
            this.plexApi = plexApi;
            PlexLoginCommand = ReactiveCommand.Create(PlexLogin);
        }

        private async Task PlexLogin()
        {
            var plexAuth = await plexApi.Authenticate("PlexWrapped", "test");
        }
    }
}