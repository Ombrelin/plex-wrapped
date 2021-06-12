using ReactiveUI;

namespace PlexWrapped.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, IRoutableViewModel
    {
        public abstract string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }
    }
}