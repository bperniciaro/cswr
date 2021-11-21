using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ActiveCampaign
{
    public class Contact : EntityBase
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("phone")]
        public int? Phone { get; set; }

        //[JsonProperty("organization")]
        //public string Organization { get; set; }

        //[JsonProperty("cdate")]
        //public DateTime CreationDate { get; set; }

        //[JsonProperty("udate")]
        //public DateTime UpdateDate { get; set; }

    }
}
