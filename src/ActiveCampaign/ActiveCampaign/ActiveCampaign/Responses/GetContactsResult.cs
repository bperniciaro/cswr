using System.Collections.Generic;
using Newtonsoft.Json;

namespace ActiveCampaign.Responses
{
    public class GetContactsResult : ResultBase
    {
        [JsonProperty("contacts")]
        public List<Contact> Contacts { get; set; }

        public GetContactsResult()
        {
            Contacts = new List<Contact>();
        }
    }
}
