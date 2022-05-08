using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ActiveCampaign.Requests;
using ActiveCampaign.Responses;
using Newtonsoft.Json;

namespace ActiveCampaign
{
    public partial class ActiveCampaignClient
    {
        public async Task<AddTagToContactResult> AddTagToContact(string email, string tagName)
        {
            var result = new AddTagToContactResult();
            var resultContact = await GetContactByEmail(email);
            if (!resultContact.IsSuccessful)
            {
                result.IsSuccessful = false;
                return result;
            }

            var resultTag = await GetTagByName(tagName);
            if (!resultTag.IsSuccessful)
            {
                result.IsSuccessful = false;
                return result;
            }

            return await AddTagToContact(resultContact.Contacts.First().Id, resultTag.Tags.First().Id);
            
        }

        public async Task<DeleteContactTagResult> RemoveTagFromContact(string email, string tagName)
        {
            var result = new DeleteContactTagResult();

            var resultContact = await GetContactByEmail(email);
            if (!resultContact.IsSuccessful)
            {
                result.IsSuccessful = false;
                return result;
            }

            var resultTag = await GetTagByName(tagName);
            if (!resultTag.IsSuccessful)
            {
                result.IsSuccessful = false;
                return result;
            }

            var resultContactTag = await GetContactTags(resultContact.Contacts.First().Id, resultTag.Tags.First().Id);
            if (!resultContactTag.IsSuccessful)
            {
                result.IsSuccessful = false;
                return result;
            }
            
            return await DeleteContactTagById(resultContactTag.ContactTags.First().Id);
        }

        private async Task<AddTagToContactResult> AddTagToContact(int contactId, int tagId)
        {
            var result = new AddTagToContactResult();
            var uri = CreateBaseUrl("contactTags");

            var request = new AddTagToContactRequest()
            {
                ContactTag = new ContactTag()
                {
                    ContactId = contactId,
                    TagId = tagId
                }
            };

            using (var postContent = new StringContent(JsonConvert.SerializeObject(request)))
            {
                using (var response = await _httpClient.PostAsync(uri, postContent))
                {
                    try
                    {
                        using (HttpContent content = response.Content)
                        {
                            string rawData = await content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<AddTagToContactResult>(rawData);

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

        private async Task<GetTagsResult> GetTagByName(string tagName)
        {
            var uri = CreateBaseUrl("tags") + $"&search={tagName}";
            var result = new GetTagsResult();
            using (var response = await _httpClient.GetAsync(uri))
            {
                try
                {
                    using (HttpContent content = response.Content)
                    {
                        string rawData = await content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<GetTagsResult>(rawData);
                        result.IsSuccessful = response.IsSuccessStatusCode && result.Tags.Any();
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

        private async Task<DeleteContactTagResult> DeleteContactTagById(int contactTagId)
        {
            var uri = CreateBaseUrl($"contactTags/{contactTagId}");
            var result = new DeleteContactTagResult();
            using (var response = await _httpClient.DeleteAsync(uri))
            {
                try
                {
                    using (HttpContent content = response.Content)
                    {
                        string rawData = await content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<DeleteContactTagResult>(rawData);
                        result.IsSuccessful = response.IsSuccessStatusCode;
                    }
                }
                catch (Exception ex)
                {
                    //result.Message = ex.Message;
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

        private async Task<ContactTagResult> GetContactTags(int contactId, int tagId)
        {
            /*The filtering doesn't work*/
            int offset = 0;
            bool isFound = false;
            bool isLimitReached = false;

            
            string uri;
            var result = new ContactTagResult();

            while (!isFound && !isLimitReached)
            {
                uri = CreateBaseUrl("contactTags", offset);

                using (var response = await _httpClient.GetAsync(uri))
                {
                    try
                    {
                        using (HttpContent content = response.Content)
                        {
                            string rawData = await content.ReadAsStringAsync();
                            var contacts = JsonConvert.DeserializeObject<ContactTagResult>(rawData);
                            result.ContactTags = contacts.ContactTags
                                                        .Where(ct => ct.ContactId == contactId && ct.TagId == tagId)
                                                        .ToList();

                            if (result.ContactTags.Any())
                                isFound = true;

                            if (contacts.ContactTags.Count < ApiResources.Limit)
                                isLimitReached = true;

                            offset += contacts.ContactTags.Count;

                            result.IsSuccessful = response.IsSuccessStatusCode && result.ContactTags.Any();
                        }
                    }
                    catch (Exception ex)
                    {
                        //result.Message = ex.Message;
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
    }
}
