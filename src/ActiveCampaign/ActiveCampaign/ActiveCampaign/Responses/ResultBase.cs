using System.Collections.Generic;
using Newtonsoft.Json;

namespace ActiveCampaign.Responses
{
    public abstract class ResultBase
    {
        public bool IsSuccessful { get; set; }

        [JsonProperty("errors")]
        public List<Error> Errors { get; set; }

        [JsonProperty("meta")]
        public ResponseMeta Meta { get; set; }
        public ResultBase()
        {
            Errors = new List<Error>();
            Meta = new ResponseMeta();
        }
    }
}
