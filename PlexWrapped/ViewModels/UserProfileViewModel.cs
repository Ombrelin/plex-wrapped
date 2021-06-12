using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using PlexApi;
using PlexApi.Model;
using PlexWrapped.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PlexWrapped.ViewModels
{
    public class UserProfileViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly IPlexApi plexApi;
        private readonly IWrappedService wrappedService;
        public IScreen HostScreen { get; }
        public string? UrlPathSegment => "userprofile";

        public ReactiveCommand<Unit, Unit> FetchPlexUserCommand { get; }
        public ReactiveCommand<Unit, Unit> StartWrappedCommand { get; }
        private readonly ObservableAsPropertyHelper<bool> isFetchingPlexUser;
        public bool IsFetchingPlexUser => isFetchingPlexUser.Value;

        private readonly ObservableAsPropertyHelper<bool> isAServerSelected;
        public bool IsAServerSelected => isAServerSelected.Value;

        [Reactive] public string UserFullname { get; set; } = "";
        [Reactive] public string UserEmail { get; set; } = "";
        [Reactive] public IImage UserProfileImage { get; set; }
        [Reactive] public List<Server> Servers { get; set; }
        [Reactive] public Server SelectedSever { get; set; }

        [Reactive] public bool MoviesEnabled { get; set; } = false;
        [Reactive] public bool TracksEnabled { get; set; } = true;

        public UserProfileViewModel(IScreen hostScreen, IPlexApi plexApi, IWrappedService wrappedService)
        {
            this.plexApi = plexApi;
            this.wrappedService = wrappedService;
            HostScreen = hostScreen;
            FetchPlexUserCommand = ReactiveCommand.CreateFromTask(FetchPlexUser);
            StartWrappedCommand = ReactiveCommand.CreateFromTask(StartWrapped);
            FetchPlexUserCommand.IsExecuting.ToProperty(this, x => x.IsFetchingPlexUser, out isFetchingPlexUser);

            this.isAServerSelected = this.WhenAnyValue(x => x.SelectedSever)
                .Select(v => v != null)
                .ToProperty(this, x => x.IsAServerSelected);

            FetchPlexUserCommand.Execute();
        }

        private async Task FetchPlexUser()
        {
            var profile = await FetchPlexUserProfile();
            FetchProfileImage(profile);
            await FetchServers();
        }

        private async Task<PlexUserProfile?> FetchPlexUserProfile()
        {
            var profile = await plexApi.GetProfile();
            this.UserFullname = profile.Username;
            this.UserEmail = profile.Email;
            return profile;
        }

        private async Task FetchProfileImage(PlexUserProfile? profile)
        {
            using WebClient client = new();
            var imageStream = await client.DownloadDataTaskAsync(new Uri(profile.Thumb));
            this.UserProfileImage = new Bitmap(new MemoryStream(imageStream));
        }

        private async Task FetchServers()
        {
            Servers = await plexApi.GetServers();
        }

        private async Task StartWrapped()
        {
            var mediaType = MoviesEnabled ? "movie" : "track";
            wrappedService.SelectedServer = this.SelectedSever;
            await wrappedService.LoadData(this.UserFullname, DateTime.Today.Year, mediaType);
            HostScreen.Router.Navigate.Execute(new ArtistsViewModel(HostScreen, wrappedService));
        }
    }
}