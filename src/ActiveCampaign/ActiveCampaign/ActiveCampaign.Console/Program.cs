using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveCampaign.Requests;

namespace ActiveCampaign.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new ActiveCampaignClient())
            {
                var contact = new Contact()
                {
                    Email = "test1@gmail.com",
                    FirstName = "Smith",
                    LastName = "John"
                };

                //ContactTests(client, contact);
                ListTests(client, contact.Email, "Subscribers");
                TagTests(client, contact.Email, "originalimport");

            }
        }

        private static void ContactTests(ActiveCampaignClient client, Contact contact)
        {
            var result = client.CreateContact(new CreateContactRequest() { Contact = contact }).Result;
            var deleteByEmailResult = client.DeleteContactByEmail(contact.Email).Result;
            var deleteByIdResult = client.DeleteContactById(1).Result;
            //var resultContact = client.GetContactByEmail(contact.Email).Result;
        }

        private static void TagTests(ActiveCampaignClient client, string email, string tagName)
        {
            var addContactTagResult = client.AddTagToContact(email, tagName).Result;
            var removeContactTagResult = client.RemoveTagFromContact(email, tagName).Result;
        }

        private static void ListTests(ActiveCampaignClient client, string email, string listName)
        {
            var addContactToListResult = client.AddContactToList(email, listName).Result;
            var removeContactFromListResult = client.RemoveContactFromList(email, listName).Result;
        }
    }
}
