using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using PlexWrapped.ViewModels;
using ReactiveUI;

namespace PlexWrapped.Views
{
    public class MediasPage : ReactiveUserControl<MediaPageViewModel>
    {
        private ItemsControl Items => this.FindControl<ItemsControl>("Items");

        
        public MediasPage()
        {
            this.WhenActivated(disposables =>
            {
                // Items
                this.OneWayBind(
                    ViewModel,
                    vm => vm.Medias,
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