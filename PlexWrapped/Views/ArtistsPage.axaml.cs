using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using PlexWrapped.ViewModels;

namespace PlexWrapped.Views
{
    public class ArtistsPage : ReactiveUserControl<ArtistsViewModel>
    {
        public ArtistsPage()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}