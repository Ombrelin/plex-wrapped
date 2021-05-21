using System.Collections.Generic;

namespace TautulliApi.Model
{
    public class TautulliData
    {
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
        public List<Play> Data { get; set; }
        public int Draw { get; set; }
        public string FilterDuration { get; set; }
        public string TotalDuration { get; set; }
    }
}