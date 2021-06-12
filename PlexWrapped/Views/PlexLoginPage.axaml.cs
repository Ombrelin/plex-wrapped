using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaProgressRing;
using PlexWrapped.ViewModels;
using ReactiveUI;

namespace PlexWrapped.Views
{
    public class PlexLoginPage : ReactiveUserControl<PlexLoginViewModel>
    {
        private Button SignInWithPlexButton => this.FindControl<Button>("SignInWithPlexButton");
        private ProgressRing Loader => this.FindControl<ProgressRing>("Loader");

        public PlexLoginPage()
        {
            this.WhenActivated(disposables =>
            {
                this.BindCommand(ViewModel,
                        vm => vm.PlexLoginCommand,
                        v => v.SignInWithPlexButton)
                    .DisposeWith(disposables);

                this.OneWayBind(
                    ViewModel,
                    vm => vm.IsLoggingIn,
                    v => v.SignInWithPlexButton.IsVisible,
                    NotConverter
                ).DisposeWith(disposables);

                this.OneWayBind(
                        ViewModel,
                        vm => vm.IsLoggingIn,
                        v => v.Loader.IsVisible)
                    .DisposeWith(disposables);
            });
            InitializeComponent();
        }

        private bool NotConverter(bool val) => !val;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}