using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ActiveCampaign
{
    public class Tag : EntityBase
    {
        [JsonProperty("tagType")]
        public string TagType { get; set; }

        [JsonProperty("tag")]
        public string TagName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
