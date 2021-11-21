using Newtonsoft.Json;

namespace ActiveCampaign.Requests
{
    public class CreateContactRequest
    {
        [JsonProperty("contact")]
        public Contact Contact { get; set; }

        public CreateContactRequest()
        {
            Contact = new Contact();
        }
    }
}
