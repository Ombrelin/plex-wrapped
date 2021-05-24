using System.Net.Http;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using PlexApi;
using PlexApi.BrowserAuth;
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
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(new PlexClientApi(new HttpClient(), new BrowserOpener())),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}