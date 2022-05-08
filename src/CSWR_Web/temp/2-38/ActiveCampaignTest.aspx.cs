using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ActiveCampaign;

public partial class temp_2_38_ActiveCampaignTest : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    //using (var client = new ActiveCampaignClient())
    //{
    //  var contact = new Contact()
    //  {
    //    Email = "george@aol.com",
    //    LastName = "GeorgyBoy"
    //  };

    //  var result = client.CreateContact(new CreateContactRequest() { Contact = contact }).Result;
    //  labResult.Text = result.ToString();
    //  //var deleteByEmailResult = client.DeleteContactByEmail(contact.Email).Result;
    //  //var deleteByIdResult = client.DeleteContactById(1).Result;
    //  //var resultContact = client.GetContactByEmail(contact.Email).Result;
    //  //var result = client.AddTagToContact("15pip@mchsi.com", "Registrant");
    //  //var addTagResult = client.AddTagToContact("15pip@mchsi.com", "Registrant").Result;
    //  //var removeTagResult = client.RemoveTagFromContact("15pip@mchsi.com", "Registrant").Result;
    //  //var addSubscribeResult = client.AddContactToList("15pip@mchsi.com", "Subscribers").Result;
    //  //var result = client.AddContactToList("15pip@mchsi.com", "Subscribers");
    //}

    using (var client = new ActiveCampaignClient())
    {
      var contact = new Contact()
      {
        Email = "george@aol.com",
        FirstName = "GeorgyBoy"
      };

      var result = client.CreateContact(new ActiveCampaign.Requests.CreateContactRequest() { Contact = contact }).Result;
      labResult.Text = result.ToString();

      var addTagResult = client.AddTagToContact(contact.Email, "Registrant").Result;
      var addSubscribeResult = client.AddContactToList(contact.Email, "Subscribers").Result;
    }
  }
}