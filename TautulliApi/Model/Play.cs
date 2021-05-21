using Newtonsoft.Json;

namespace TautulliApi.Model
{
    public class Play
    {
        [JsonProperty("reference_id")] public int ReferenceId { get; set; }

        [JsonProperty("row_id")] public int RowId { get; set; }

        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("date")] public int Date { get; set; }

        [JsonProperty("started")] public int Started { get; set; }

        [JsonProperty("stopped")] public int Stopped { get; set; }

        [JsonProperty("duration")] public int Duration { get; set; }

        [JsonProperty("paused_counter")] public int PausedCounter { get; set; }

        [JsonProperty("user_id")] public int UserId { get; set; }

        [JsonProperty("user")] public string User { get; set; }

        [JsonProperty("friendly_name")] public string FriendlyName { get; set; }

        [JsonProperty("platform")] public string Platform { get; set; }

        [JsonProperty("product")] public string Product { get; set; }

        [JsonProperty("player")] public string Player { get; set; }

        [JsonProperty("ip_address")] public string IpAddress { get; set; }

        [JsonProperty("live")] public int Live { get; set; }

        [JsonProperty("machine_id")] public string MachineId { get; set; }

        [JsonProperty("media_type")] public string MediaType { get; set; }

        [JsonProperty("rating_key")] public int RatingKey { get; set; }

        [JsonProperty("parent_rating_key")] public int ParentRatingKey { get; set; }

        [JsonProperty("grandparent_rating_key")]
        public int GrandparentRatingKey { get; set; }

        [JsonProperty("full_title")] public string FullTitle { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("parent_title")] public string ParentTitle { get; set; }

        [JsonProperty("grandparent_title")] public string GrandparentTitle { get; set; }

        [JsonProperty("original_title")] public string OriginalTitle { get; set; }

        [JsonProperty("year")] public int Year { get; set; }

        [JsonProperty("media_index")] public int MediaIndex { get; set; }

        [JsonProperty("parent_media_index")] public int ParentMediaIndex { get; set; }

        [JsonProperty("thumb")] public string Thumb { get; set; }

        [JsonProperty("originally_available_at")]
        public string OriginallyAvailableAt { get; set; }

        [JsonProperty("guid")] public string Guid { get; set; }

        [JsonProperty("transcode_decision")] public string TranscodeDecision { get; set; }

        [JsonProperty("percent_complete")] public int PercentComplete { get; set; }

        [JsonProperty("watched_status")] public int WatchedStatus { get; set; }

        [JsonProperty("group_count")] public int GroupCount { get; set; }

        [JsonProperty("group_ids")] public string GroupIds { get; set; }

        [JsonProperty("state")] public object State { get; set; }

        [JsonProperty("session_key")] public object SessionKey { get; set; }
    }
}