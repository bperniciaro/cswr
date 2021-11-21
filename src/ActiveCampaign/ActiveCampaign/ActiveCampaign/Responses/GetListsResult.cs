using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ActiveCampaign.Responses
{
    public class GetListsResult : ResultBase
    {
        [JsonProperty("lists")]
        public List<ACList> Lists { get; set; }

        public GetListsResult()
        {
            Lists = new List<ACList>();
        }
    }
}
