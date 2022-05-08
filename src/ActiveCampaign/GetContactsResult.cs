using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ActiveCampaign
{
    public class GetContactsResult : ResultBase
    {
        [JsonProperty("contacts")]
        public List<Contact> Contacts { get; set; }
    }
}
