using Newtonsoft.Json;

namespace ActiveCampaign.Responses
{
    public class DeleteContactResult : ResultBase
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
