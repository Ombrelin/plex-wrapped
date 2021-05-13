using System;
using System.Collections.Generic;
using System.Text;
using PlexApi;
using ReactiveUI;

namespace PlexWrapped.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        public string Greeting => "Welcome to Avalonia!";
        public RoutingState Router { get; } = new RoutingState();

        public MainWindowViewModel(IPlexApi plexApi)
        {
            this.Router.Navigate.Execute(new PlexLoginViewModel(this,plexApi));
        }
    }
}