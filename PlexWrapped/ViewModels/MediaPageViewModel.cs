using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlexWrapped.Services;
using PlexWrapped.Views;
using ReactiveUI;

namespace PlexWrapped.ViewModels
{
    public class MediaPageViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly IWrappedService wrappedService;
        public string? UrlPathSegment => "medias";
        public IScreen HostScreen { get; }
        public List<MediaElementViewModel> Medias { get; set; }

        public MediaPageViewModel(IScreen hostScreen, IWrappedService wrappedService)
        {
            HostScreen = hostScreen;
            this.wrappedService = wrappedService;
            FetchMedias();
        }

        private async Task FetchMedias()
        {
            var artists = wrappedService.GetMostPlayedMedias(5);
            Medias = artists
                .Select(media => new MediaElementViewModel(media))
                .ToList();
        }
    }
    
    
}