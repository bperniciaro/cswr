using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ActiveCampaign.Requests;
using ActiveCampaign.Responses;
using Newtonsoft.Json;

namespace ActiveCampaign
{
    /// <summary>
    /// Active Campaign Client
    /// </summary>
    public partial class ActiveCampaignClient
    {
  
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
                            result = JsonConvert.DeserializeObject<CreateContactResult>(rawData);

                            if (response.IsSuccessStatusCode)
                            {
                                result.IsSuccessful = true;
                            }
                            else
                            {
                                result.IsSuccessful = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        result.IsSuccessful = false;
                        result.Errors.Add(new Error()
                        {
                            Title = "Error during API call",
                            Detail = ex.Message
                        });
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
                    result.Errors.Add(new Error()
                    {
                        Title = "Error during API call",
                        Detail = ex.Message
                    });
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
        public async Task<GetContactsResult> GetContactByEmail(string email)
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
                    result.Errors.Add(new Error()
                    {
                        Title = "Error during API call",
                        Detail = ex.Message
                    });
                }
            }

            return result;
        }
        
    }
}
