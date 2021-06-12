using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace PlexWrapped.Views
{
    public class MediaElementView : ReactiveUserControl<MediaElementViewModel>
    {
        private TextBlock TitleTextBlock => this.FindControl<TextBlock>("Title");
        private TextBlock CountTextBlock => this.FindControl<TextBlock>("Count");
        private Image Thumbnail => this.FindControl<Image>("Thumbnail");

        public MediaElementView()
        {
            this.WhenActivated(disposables =>
            {
                // Title
                this.OneWayBind(
                    ViewModel,
                    vm => vm.Title,
                    v => v.TitleTextBlock.Text
                ).DisposeWith(disposables);

                // Count
                this.OneWayBind(
                    ViewModel,
                    vm => vm.Count,
                    v => v.CountTextBlock.Text
                ).DisposeWith(disposables);


                // Thumbnail
                this.OneWayBind(
                    ViewModel,
                    vm => vm.Thumbnail,
                    v => v.Thumbnail.Source
                ).DisposeWith(disposables);
            });
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}