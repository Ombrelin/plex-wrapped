using System.Threading.Tasks;

namespace PlexApi.BrowserAuth
{
    public interface IBrowserOpener
    {
        Task OpenBrowser(string url);
    }


}