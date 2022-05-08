using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ActiveCampaign.Requests
{
    public class ContactListRequest
    {
        [JsonProperty("contactList")]
        public ContactList ContactList { get; set; }

        public ContactListRequest()
        {
            ContactList = new ContactList();
        }
    }
}
