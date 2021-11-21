using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ActiveCampaign
{
    /// <summary>
    /// Active Campaign Client
    /// </summary>
    public partial class ActiveCampaignClient : DisposableBase
    {
        #region 
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private HttpClient _httpClient = new HttpClient(); 
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

            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Creates URL for resource
        /// </summary>
        /// <param name="apiAction"></param>
        /// <returns></returns>
        private string CreateBaseUrl(string apiAction)
        {
            return $"{_baseUrl}/{apiAction}?api_key={_apiKey}&api_output=json";
        }
        
        /// <summary>
        /// Creates a contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public async Task<CreateContactResult> CreateContact(CreateContactRequest contact)
        {
            var uri = CreateBaseUrl("contacts");
            var result = new CreateContactResult();
            using (var postContent = new StringContent(JsonConvert.SerializeObject(contact)))
            {
                using (var response = await _httpClient.PostAsync(uri, postContent))
                {
                    try
                    {
                        using (HttpContent content = response.Content)
                        {
                            string rawData = await content.ReadAsStringAsync();
                            if (response.IsSuccessStatusCode)
                            {
                                result = JsonConvert.DeserializeObject<CreateContactResult>(rawData);
                                result.IsSuccessful = true;
                            }
                            else
                            {
                                result.Error = JsonConvert.DeserializeObject<ErrorResult>(rawData);
                                result.IsSuccessful = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        result.IsSuccessful = false;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Deletes a contact by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DeleteContactResult> DeleteContactById(int id)
        {
            var uri = CreateBaseUrl($"contacts/{id}");
            var result = new DeleteContactResult();
            using (var response = await _httpClient.DeleteAsync(uri))
            {
                try
                {
                    using (HttpContent content = response.Content)
                    {
                        string rawData = await content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<DeleteContactResult>(rawData);
                        result.IsSuccessful = response.IsSuccessStatusCode;
                    }
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    result.IsSuccessful = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Deletes a contact by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<DeleteContactResult> DeleteContactByEmail(string email)
        {
            var contact = await GetContactByEmail(email);

            if (contact.IsSuccessful)
                return await DeleteContactById(contact.Contacts.First().Id);
            
            return new DeleteContactResult(){IsSuccessful = contact.IsSuccessful};
        }

        /// <summary>
        /// Gets a contact by its email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private async Task<GetContactsResult> GetContactByEmail(string email)
        {
            var uri = CreateBaseUrl("contacts") + $"&email={email}";
            var result = new GetContactsResult();
            using (var response = await _httpClient.GetAsync(uri))
            {
                try
                {
                    using (HttpContent content = response.Content)
                    {
                        string rawData = await content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<GetContactsResult>(rawData);
                        result.IsSuccessful = response.IsSuccessStatusCode && result.Contacts.Any();
                    }
                }
                catch (Exception ex)
                {
                    result.IsSuccessful = false;
                }
            }

            return result;
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
