using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ActiveCampaign
{
    public abstract class EntityBase
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
