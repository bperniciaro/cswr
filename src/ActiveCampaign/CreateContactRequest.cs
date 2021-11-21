using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ActiveCampaign
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
