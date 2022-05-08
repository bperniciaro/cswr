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
        /// <summary>
        /// Adds contact to a list
        /// </summary>
        /// <param name="email">Email of the contact</param>
        /// <param name="listName">Name of the list</param>
        /// <returns></returns>
        public async Task<UpdateListStatusForContactResult> AddContactToList(string email, string listName)
        {
            return await UpdateListStatusForContact(email, listName, (int)ContactStatus.Active);
        }

        /// <summary>
        /// Removes a contact from a list
        /// </summary>
        /// <param name="email">Email of the contact</param>
        /// <param name="listName">Name of the list</param>
        /// <returns></returns>
        public async Task<UpdateListStatusForContactResult> RemoveContactFromList(string email, string listName)
        {
            return await UpdateListStatusForContact(email, listName, (int)ContactStatus.Unsubscribed);
        }

        /// <summary>
        /// Adds/Removes a contact from a certain list
        /// </summary>
        /// <param name="email"></param>
        /// <param name="listName"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        private async Task<UpdateListStatusForContactResult> UpdateListStatusForContact(string email, string listName, int statusId)
        {
            var result = new UpdateListStatusForContactResult();
            var uri = CreateBaseUrl("contactLists");
            
            /*Getting the contact's ID*/
            var contact = await GetContactByEmail(email);

            if (!contact.IsSuccessful)
            {
                result.IsSuccessful = false;
                return result;
            }

            /*Getting the list's ID*/
            var list = await GetListByName(listName);

            if (!list.IsSuccessful)
            {
                result.IsSuccessful = false;
                return result;
            }

            /*Building the request*/
            var request = new ContactListRequest()
            {
                ContactList = new ContactList()
                {
                    ContactId = contact.Contacts.First().Id,
                    ListId = list.Lists.First().Id,
                    StatusId = statusId
                }
            };

            /*Posting the data*/
            using (var postContent = new StringContent(JsonConvert.SerializeObject(request)))
            {
                using (var response = await _httpClient.PostAsync(uri, postContent))
                {
                    try
                    {
                        using (HttpContent content = response.Content)
                        {
                            string rawData = await content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<UpdateListStatusForContactResult>(rawData);

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
        /// Gets a list by its name
        /// </summary>
        /// <param name="listName"></param>
        /// <returns></returns>
        private async Task<GetListsResult> GetListByName(string listName)
        {
            /*The filtering doesn't work*/

            int offset = 0;
            bool isFound = false;
            bool isLimitReached = false;

            
            string uri;//+ $"&name={listName}";
            var result = new GetListsResult();

            while (!isFound && !isLimitReached)
            {
                uri = CreateBaseUrl("lists", offset);
                using (var response = await _httpClient.GetAsync(uri))
                {
                    try
                    {
                        using (HttpContent content = response.Content)
                        {
                            string rawData = await content.ReadAsStringAsync();
                            var lists = JsonConvert.DeserializeObject<GetListsResult>(rawData);
                            result.Lists = lists.Lists
                                                .Where(l => l.Name.Equals(listName, StringComparison.OrdinalIgnoreCase))
                                                .ToList();

                            if (result.Lists.Any())
                                isFound = true;

                            if (lists.Lists.Count < ApiResources.Limit)
                                isLimitReached = true;

                            offset += lists.Lists.Count;

                            result.IsSuccessful = response.IsSuccessStatusCode && result.Lists.Any();
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
    }
}
