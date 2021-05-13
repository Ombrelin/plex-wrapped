namespace PlexApi.Model
{
    public class PlexUserProfile
    {
        public int Id { get; set; }
        public string Uuid { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public object Locale { get; set; }
        public bool Confirmed { get; set; }
        public bool EmailOnlyAuth { get; set; }
        public bool HasPassword { get; set; }
        public bool Protected { get; set; }
        public string Thumb { get; set; }
        public string AuthToken { get; set; }
        public string MailingListStatus { get; set; }
        public bool MailingListActive { get; set; }
        public string ScrobbleTypes { get; set; }
        public string Country { get; set; }
        public string Pin { get; set; }
        public string SubscriptionDescription { get; set; }
        public bool Restricted { get; set; }
        public object Anonymous { get; set; }
        public bool Home { get; set; }
        public bool Guest { get; set; }
        public int HomeSize { get; set; }
        public bool HomeAdmin { get; set; }
        public int MaxHomeSize { get; set; }
        public int CertificateVersion { get; set; }
        public int RememberExpiresAt { get; set; }
    }
}