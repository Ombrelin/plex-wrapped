using Newtonsoft.Json;

namespace TautulliApi.Model
{
    public record Play
    {
        [JsonProperty("reference_id")] public int ReferenceId { get; set; }
        [JsonProperty("row_id")] public int RowId { get; set; }

        public int Id { get; set; }
        public int Date { get; set; }
        public int Started { get; set; }
        public int Stopped { get; set; }
        public int Duration { get; set; }
        [JsonProperty("paused_counter")] public int PausedCounter { get; set; }
        [JsonProperty("user_id")] public int UserId { get; set; }
        public string User { get; set; }
        [JsonProperty("friendly_name")] public string FriendlyName { get; set; }
        public string Platform { get; set; }
        public string Product { get; set; }
        public string Player { get; set; }
        public string IpAddress { get; set; }
        public int Live { get; set; }
        [JsonProperty("machine_id")] public string MachineId { get; set; }
        [JsonProperty("media_type")] public string MediaType { get; set; }

        [JsonProperty("rating_key")] public int RatingKey { get; set; }

        //  [JsonProperty("parent_rating_key")]
        //public int ParentRatingKey { get; set; }
        //[JsonProperty("grandparent_rating_key")]
        //public int GrandparentRatingKey { get; set; }
        [JsonProperty("full_title")] public string FullTitle { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("parent_title")] public string ParentTitle { get; set; }
        [JsonProperty("grandparent_title")] public string GrandparentTitle { get; set; }
        [JsonProperty("original_title")] public string OriginalTitle { get; set; }

        public int Year { get; set; }

        //[JsonProperty("media_index")]
        //public int MediaIndex { get; set; }
        //[JsonProperty("parent_media_index")]
        //public int ParentMediaIndex { get; set; }
        public string Thumb { get; set; }

        [JsonProperty("originally_available_at")]
        public string OriginallyAvailableAt { get; set; }

        public string Guid { get; set; }
        [JsonProperty("transcode_decision")]
        public string TranscodeDecision { get; set; }
        [JsonProperty("percent_complete")]
        public int PercentComplete { get; set; }
        //[JsonProperty("watched_status")]
        //public int WatchedStatus { get; set; }
        [JsonProperty("group_count")]
        public int GroupCount { get; set; }
        [JsonProperty("group_ids")]
        public string GroupIds { get; set; }
        public string State { get; set; }
        [JsonProperty("session_key")]
        public string SessionKey { get; set; }
    }
}