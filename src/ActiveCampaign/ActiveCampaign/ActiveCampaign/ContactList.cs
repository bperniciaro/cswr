using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ActiveCampaign
{
    public class ContactList
    {
        [JsonProperty("list")]
        public int ListId { get; set; }

        [JsonProperty("contact")]
        public int ContactId { get; set; }

        [JsonProperty("status")]
        public int StatusId { get; set; }
    }
}
