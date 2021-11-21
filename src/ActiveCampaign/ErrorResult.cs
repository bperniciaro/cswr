using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ActiveCampaign
{
    public class ErrorResult
    {
        [JsonProperty("errors")]
        public List<Error> Errors { get; set; }

        public ErrorResult()
        {
            Errors = new List<Error>();
        }
    }
}
