using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ActiveCampaign.Responses
{
    public class ContactTagResult : ResultBase
    {
        [JsonProperty("contactTags")]
        public List<ContactTag> ContactTags { get; set; }

        public ContactTagResult()
        {
            ContactTags = new List<ContactTag>();
        }
    }
}
