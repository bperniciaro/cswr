using Newtonsoft.Json;

namespace ActiveCampaign.Requests
{
    public class AddTagToContactRequest
    {
        [JsonProperty("contactTag")]
        public ContactTag ContactTag { get; set; }

        public AddTagToContactRequest()
        {
            ContactTag = new ContactTag();
        }
    }
}
