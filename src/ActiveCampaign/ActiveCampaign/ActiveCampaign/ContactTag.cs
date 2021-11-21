using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ActiveCampaign
{
    public class ContactTag : EntityBase
    {
        [JsonProperty("contact")]
        public int ContactId { get; set; }

        [JsonProperty("tag")]
        public int TagId { get; set; }
    }
}
