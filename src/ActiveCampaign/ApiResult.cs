using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ActiveCampaign
{
    public abstract class ApiResult<T> : ResultBase
    {
        [JsonProperty("result_code")]
        public int Code { get; set; }

        [JsonProperty("result_message")]
        public string Message { get; set; }

        [JsonProperty("result_output")]
        public string Output { get; set; }

        public T Data { get; set; }
        public ErrorResult Error { get; set; }
    }
}
