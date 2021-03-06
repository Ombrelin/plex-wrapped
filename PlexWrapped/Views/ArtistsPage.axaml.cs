using System.Reactive.Disposables;
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
        private Button NextButton => this.FindControl<Button>("Next");

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
                
                // Next button
                this.BindCommand(
                    ViewModel,
                    vm => vm.NextCommand,
                    v => v.NextButton);
            });
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}