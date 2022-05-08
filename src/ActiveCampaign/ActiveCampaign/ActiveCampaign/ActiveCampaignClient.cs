using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ActiveCampaign.Responses;

namespace ActiveCampaign
{
    public partial class ActiveCampaignClient : DisposableBase
    {
        #region 
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private HttpClient _httpClient;
        #endregion

        public ActiveCampaignClient()
        {
            if (string.IsNullOrEmpty(ApiResources.Key))
                throw new ArgumentNullException(nameof(ApiResources.Key));

            if (string.IsNullOrEmpty(ApiResources.BaseUrl))
                throw new ArgumentNullException(nameof(ApiResources.BaseUrl));

            _apiKey = ApiResources.Key;
            _baseUrl = ApiResources.BaseUrl;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            _httpClient = new HttpClient();
        }

        public ActiveCampaignClient(string apiKey, string baseUrl)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException(nameof(apiKey));

            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException(nameof(baseUrl));

            _apiKey = apiKey;
            _baseUrl = baseUrl;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Creates URL for resource
        /// </summary>
        /// <param name="apiAction"></param>
        /// <returns></returns>
        private string CreateBaseUrl(string apiAction, int offset = 0, int limit = ApiResources.Limit)
        {
            return $"{_baseUrl}/{apiAction}?api_key={_apiKey}&api_output=json&limit={limit}&offset={offset}";
        }
        
        /// <summary>
        /// Cleanup
        /// </summary>
        protected override void ReleaseResources()
        {
            _httpClient.Dispose();
        }
    }
}
