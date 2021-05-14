using System;

namespace PlexApi.Model
{
    public class PlexAuth
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Product { get; set; }
        public bool Trusted { get; set; }
        public string ClientIdentifier { get; set; }
        public int ExpiresIn { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string AuthToken { get; set; }
        public object NewRegistration { get; set; }
    }
}