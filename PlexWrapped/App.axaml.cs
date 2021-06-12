using System.Net.Http;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using PlexApi;
using PlexApi.BrowserAuth;
using PlexWrapped.Services;
using PlexWrapped.ViewModels;
using PlexWrapped.Views;
using ReactiveUI;
using Splat;
using TautulliApi;

namespace PlexWrapped
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            Locator.CurrentMutable.Register(() => new PlexLoginPage(), typeof(IViewFor<PlexLoginViewModel>));
            Locator.CurrentMutable.Register(() => new UserProfilePage(), typeof(IViewFor<UserProfileViewModel>));
            Locator.CurrentMutable.Register(() => new ArtistsPage(), typeof(IViewFor<ArtistsViewModel>));
            Locator.CurrentMutable.Register(() => new MediaElementView(), typeof(IViewFor<MediaElementViewModel>));
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var plexApi = new PlexClientApi(new HttpClient(), new BrowserOpener());
                var tautulliApi = new TautulliApi.TautulliApi(
                    new HttpClient(),
                    "7ecb76ff5b314acd89122fa7e93262ce",
                    "https://tautulli.arsenelapostolet.fr/api/v2"
                );
                Locator.CurrentMutable.RegisterLazySingleton(() => new WrappedService(plexApi, tautulliApi), typeof(IWrappedService));
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(plexApi),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}