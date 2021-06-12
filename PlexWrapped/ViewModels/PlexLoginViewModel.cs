using System;
using System.Reactive;
using System.Threading.Tasks;
using PlexApi;
using PlexWrapped.Services;
using ReactiveUI;
using Splat;

namespace PlexWrapped.ViewModels
{
    public class PlexLoginViewModel: ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "plexlogin";
        public IScreen HostScreen { get; }
        public ReactiveCommand<Unit, Unit> PlexLoginCommand { get; }
        private readonly IPlexApi plexApi;

        private readonly ObservableAsPropertyHelper<bool> isLoggingIn;
        public bool IsLoggingIn => isLoggingIn.Value;
        
        public PlexLoginViewModel(IScreen hostScreen, IPlexApi plexApi)
        {
            HostScreen = hostScreen;
            this.plexApi = plexApi;
            PlexLoginCommand = ReactiveCommand.CreateFromTask(PlexLogin);
            PlexLoginCommand.IsExecuting.ToProperty(this, x => x.IsLoggingIn, out isLoggingIn);
        }

        private async Task PlexLogin()
        {
            await plexApi.Authenticate("PlexWrapped", "test");
            HostScreen.Router.Navigate.Execute(
                new UserProfileViewModel(HostScreen, plexApi, Locator.Current.GetService<IWrappedService>())
                );
        }
    }
}