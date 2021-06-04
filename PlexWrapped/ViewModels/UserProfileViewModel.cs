using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.Media.Imaging;
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
        public ReactiveCommand<Unit, Unit> StartWrappedCommand { get; }
        private readonly ObservableAsPropertyHelper<bool> isFetchingPlexUser;
        public bool IsFetchingPlexUser => isFetchingPlexUser.Value;

        [Reactive] public string UserFullname { get; set; } = "";
        [Reactive] public string UserEmail { get; set; } = "";
        [Reactive] public IImage UserProfileImage { get; set; }

        
        public UserProfileViewModel(IScreen hostScreen, IPlexApi plexApi)
        {
            this.plexApi = plexApi;
            HostScreen = hostScreen;
            FetchPlexUserCommand = ReactiveCommand.CreateFromTask(FetchPlexUser);
            StartWrappedCommand = ReactiveCommand.CreateFromTask(StartWrapped);
            FetchPlexUserCommand.IsExecuting.ToProperty(this, x => x.IsFetchingPlexUser, out isFetchingPlexUser);

            FetchPlexUserCommand.Execute();
        }

        private async Task FetchPlexUser()
        {
            var profile = await plexApi.GetProfile();
            this.UserFullname = profile.Username;
            this.UserEmail = profile.Email;

            using WebClient client = new();
            this.UserProfileImage = new Bitmap(new MemoryStream(client.DownloadData(profile.Thumb)));
        }

        private async Task StartWrapped()
        {
            var tautulliApi = new TautulliApi.TautulliApi(
                new HttpClient(),
                "7ecb76ff5b314acd89122fa7e93262ce",
                "https://tautulli.arsenelapostolet.fr/api/v2"
            );

            var servers = await plexApi.GetServers();
            Console.WriteLine(servers.Count);
            
            var plays = await tautulliApi.GetPlaysHistory(UserFullname, 2021, "track");
            Console.WriteLine(plays.Count);
            //HostScreen.Router.Navigate.Execute(new ArtistsViewModel(HostScreen,tautulliApi));
        }
    }
}