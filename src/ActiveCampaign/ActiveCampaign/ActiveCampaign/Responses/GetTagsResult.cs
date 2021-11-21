using System.Collections.Generic;
using Newtonsoft.Json;

namespace ActiveCampaign.Responses
{
    public class GetTagsResult : ResultBase
    {
        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        public GetTagsResult()
        {
            Tags = new List<Tag>();
        }
    }
}
