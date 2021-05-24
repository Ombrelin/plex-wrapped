using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace TautulliApi.Test
{
    public class TautulliApiFakeHandler : HttpClientHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Assert.Contains("/api/v2", request.RequestUri.PathAndQuery);
            Assert.Contains("fakeurl.com", request.RequestUri.Host);
            Assert.Contains("apikey=tautulliapikey", request.RequestUri.PathAndQuery);
            Assert.Contains("cmd=get_history", request.RequestUri.PathAndQuery);
            Assert.Contains("cmd=get_history", request.RequestUri.PathAndQuery);
            Assert.Contains("media_type=track", request.RequestUri.PathAndQuery);
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"
                        {
                          ""response"": {
                            ""result"": ""success"",
                            ""message"": null,
                            ""data"": {
                              ""recordsFiltered"": 8353,
                              ""recordsTotal"": 11663,
                              ""data"": [
                                {
                                  ""reference_id"": 11660,
                                  ""row_id"": 11660,
                                  ""id"": 11660,
                                  ""date"": 1621528156,
                                  ""started"": 1621528156,
                                  ""stopped"": 1621528533,
                                  ""duration"": 182,
                                  ""paused_counter"": 195,
                                  ""user_id"": 17074161,
                                  ""user"": ""Arsène Lapostolet"",
                                  ""friendly_name"": ""Arsène Lapostolet"",
                                  ""platform"": ""Android"",
                                  ""product"": ""Plexamp"",
                                  ""player"": ""Android"",
                                  ""ip_address"": ""ip address"",
                                  ""live"": 0,
                                  ""machine_id"": ""machine id"",
                                  ""media_type"": ""track"",
                                  ""rating_key"": 13800,
                                  ""parent_rating_key"": 13799,
                                  ""grandparent_rating_key"": 13720,
                                  ""full_title"": ""Le Combat ordinaire - Les Fatals Picards"",
                                  ""title"": ""Le Combat ordinaire"",
                                  ""parent_title"": ""Le Sens de la gravité"",
                                  ""grandparent_title"": ""Les Fatals Picards"",
                                  ""original_title"": """",
                                  ""year"": 2009,
                                  ""media_index"": 1,
                                  ""parent_media_index"": 1,
                                  ""thumb"": ""/library/metadata/13799/thumb/1604342328"",
                                  ""originally_available_at"": """",
                                  ""guid"": ""plex://track/5d07d71f403c640290aaf2eb"",
                                  ""transcode_decision"": ""direct play"",
                                  ""percent_complete"": 81,
                                  ""watched_status"": 0.5,
                                  ""group_count"": 1,
                                  ""group_ids"": ""11660"",
                                  ""state"": null,
                                  ""session_key"": null
                                }
                           
                               ]
                            }
                        }
                    }
                    ")
            };
            response.Headers.Add("ContentType", "application/json");
            return Task.FromResult(response);
        }
    }
}