using Newtonsoft.Json;

namespace ActiveCampaign.Responses
{
    public class AddTagToContactResult : ResultBase
    {
        [JsonProperty("contactTag")]
        public ContactTag ContactTag { get; set; }

        public AddTagToContactResult()
        {
            ContactTag = new ContactTag();
        }
    }
}
