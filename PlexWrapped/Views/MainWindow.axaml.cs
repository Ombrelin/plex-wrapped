using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using PlexWrapped.ViewModels;
using ReactiveUI;

namespace PlexWrapped.Views
{
    public class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            this.WhenActivated(disposables => { });
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}