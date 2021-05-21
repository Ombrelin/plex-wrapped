using System.Collections.Generic;
using System.Threading.Tasks;
using TautulliApi.Model;

namespace TautulliApi
{
    public interface ITautulliApi
    {
        Task<List<Play>> GetPlaysHistory(string username, int year, string mediaType);
    }
}