using Newtonsoft.Json;

namespace ActiveCampaign.Responses
{
    public class DeleteContactTagResult : ResultBase
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
