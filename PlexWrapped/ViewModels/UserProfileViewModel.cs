using System.Reactive;
using System.Threading.Tasks;
using PlexApi;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PlexWrapped.ViewModels
{
    public class UserProfileViewModel: ReactiveObject, IRoutableViewModel
    {
        private readonly IPlexApi plexApi;
        public IScreen HostScreen { get; }
        public string? UrlPathSegment => "userprofile";
        
        public ReactiveCommand<Unit, Unit> FetchPlexUserCommand { get; }
        private readonly ObservableAsPropertyHelper<bool> isFetchingPlexUser;
        public bool IsFetchingPlexUser => isFetchingPlexUser.Value;

        [Reactive] public string UserFullname { get; set; } = "";
        [Reactive] public string UserEmail { get; set; } = "";
        [Reactive] public string UserProfileImageUrl { get; set; } = "";

        
        public UserProfileViewModel(IScreen hostScreen, IPlexApi plexApi)
        {
            this.plexApi = plexApi;
            HostScreen = hostScreen;
            FetchPlexUserCommand = ReactiveCommand.CreateFromTask(FetchPlexUser);
            FetchPlexUserCommand.IsExecuting.ToProperty(this, x => x.IsFetchingPlexUser, out isFetchingPlexUser);

            FetchPlexUserCommand.Execute();
        }

        private async Task FetchPlexUser()
        {
            var profile = await plexApi.GetProfile();
            this.UserFullname = profile.Username;
            this.UserEmail = profile.Email;
            this.UserProfileImageUrl = profile.Thumb;
        }

      

    }
}