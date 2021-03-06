using System.Collections.Generic;
using System.Threading.Tasks;
using PlexApi.Model;

namespace PlexApi
{
    public interface IPlexApi
    {
        Task<PlexAuth> Authenticate(string productName, string clientId);
        Task<PlexUserProfile> GetProfile();
        Task<List<Server>> GetServers();
        string PlexToken { get; }
    }
}