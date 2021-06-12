using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using PlexWrapped.ViewModels;
using ReactiveUI;

namespace PlexWrapped.Views
{
    public class ArtistsPage : ReactiveUserControl<ArtistsViewModel>
    {
        private ItemsControl Items => this.FindControl<ItemsControl>("Items");

        public ArtistsPage()
        {
            // Items
            this.WhenActivated(disposables =>
            {
                // Title
                this.OneWayBind(
                    ViewModel,
                    vm => vm.Artists,
                    v => v.Items.Items
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