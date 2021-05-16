using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using AvaloniaProgressRing;
using PlexWrapped.ViewModels;
using ReactiveUI;

namespace PlexWrapped.Views
{
    public class UserProfilePage : ReactiveUserControl<UserProfileViewModel>
    {
        
        private TextBlock UsernameTextBlock => this.FindControl<TextBlock>("UsernameTextBlock");
        private TextBlock UserEmailTextBlock => this.FindControl<TextBlock>("UserEmailTextBlock");
        private Image ProfileImage => this.FindControl<Image>("ProfileImage");
        private ProgressRing Loader => this.FindControl<ProgressRing>("Loader");
        private Button StartButton => this.FindControl<Button>("StartButton");
        public UserProfilePage()
        {
            this.WhenActivated(disposables =>
            {
                // Username
                this.OneWayBind(
                    ViewModel,
                    vm => vm.UserFullname,
                    v => v.UsernameTextBlock.Text
                ).DisposeWith(disposables);
                this.OneWayBind(
                    ViewModel,
                    vm => vm.IsFetchingPlexUser,
                    v => v.UsernameTextBlock.IsVisible,
                    NotConverter
                ).DisposeWith(disposables);
                // Email
                this.OneWayBind(
                    ViewModel,
                    vm => vm.UserEmail,
                    v => v.UserEmailTextBlock.Text
                ).DisposeWith(disposables);
                this.OneWayBind(
                    ViewModel,
                    vm => vm.IsFetchingPlexUser,
                    v => v.UserEmailTextBlock.IsVisible,
                    NotConverter
                ).DisposeWith(disposables);
                
                // Image
                
                // this.OneWayBind(
                //     ViewModel,
                //     vm => vm.IsFetchingPlexUser,
                //     v => v.ProfileImage.IsVisible,
                //     NotConverter
                // ).DisposeWith(disposables);
                
                this.OneWayBind(
                    ViewModel,
                    vm => vm.UserProfileImage,
                    v => v.ProfileImage.Source
                ).DisposeWith(disposables);
                
                // Loader
                
                this.OneWayBind(
                    ViewModel,
                    vm => vm.IsFetchingPlexUser,
                    v => v.Loader.IsVisible
                ).DisposeWith(disposables);
                
                // Start Button
                this.OneWayBind(
                    ViewModel,
                    vm => vm.IsFetchingPlexUser,
                    v => v.StartButton.IsVisible,
                    NotConverter
                ).DisposeWith(disposables);
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