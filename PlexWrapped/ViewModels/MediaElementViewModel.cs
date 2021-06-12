using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using PlexWrapped.Models;
using ReactiveUI;

namespace PlexWrapped.Views
{
    public class MediaElementViewModel : ReactiveObject
    {
        private readonly MediaElement element;

        public string Title => element.Name;
        public Bitmap Thumbnail;
        public string Count => element.Count.ToString();
        public MediaElementViewModel(MediaElement element)
        {
            this.element = element;
            DownloadThumbnail();
        }

        private async Task DownloadThumbnail()
        {
            using WebClient client = new();
            var imageStream = await client.DownloadDataTaskAsync(new Uri(element.ThumbnailUrl));
            this.Thumbnail = new Bitmap(new MemoryStream(imageStream));
        }
    }
}