using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BP.CheatSheetWarRoom.BLL.Sheets;
using BP.CheatSheetWarRoom.UI.UserControls;
using Recaptcha;
using System.Web.Services;
using System.Net;
using ActiveCampaign;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class Register : BasePage
  {

    protected static string ReCaptcha_Key = ConfigurationManager.AppSettings["Google.ReCaptcha.SiteKey"];
    protected static string ReCaptcha_Secret = ConfigurationManager.AppSettings["Google.ReCaptcha.SecretKey"];

    protected void Page_Load(object sender, EventArgs e)
    {
      mbStatus.Visible = false;
      if (!IsPostBack)
      {
        if (this.User.Identity.IsAuthenticated)
        {
          Response.Redirect(this.BaseUrl);
        }
      }
    }


    /// <summary>
    /// This event is fired each time the user changes the stage 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cuwCreateUserWizard_ActiveStepChanged(object sender, EventArgs e)
    {
      // if we hit the 2nd step, redirect the user to registration confirmation
      if(CreateUserWizard1.ActiveStepIndex == 1)
      {
        Response.Redirect("~/access/registrationconfirmation.aspx");
      }
    }



    protected void cuwCreateUserWizard_OnCreatedUser(object sender, EventArgs e)
    {
      // determine if the user wants to subscribe
      ProfileCommon profile = this.Profile.GetProfile(CreateUserWizard1.UserName);
      // save the email preference
      CheckBox subscribeCheckbox = (CheckBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("cbSubscribe");
      profile.EmailNotifications = subscribeCheckbox.Checked;
      // save the first name
      TextBox tbFirstName = (TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("tbFirstName");
      profile.FirstName = tbFirstName.Text;
      profile.Save();

      // determine the email address
      TextBox tbEmailAddress = (TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Email");

      // add the user to Active Campaign if we're in prod
      if (Globals.CSWRSettings.ApplicationState == ApplicationState.Prod.ToString().ToLower())
      {
        if (subscribeCheckbox.Checked)
        {
          using (var client = new ActiveCampaignClient())
          {
            var contact = new Contact()
            {
              Email = tbEmailAddress.Text,
              FirstName = tbFirstName.Text
            };

            var result = client.CreateContact(new ActiveCampaign.Requests.CreateContactRequest() { Contact = contact }).Result;

            var addTagResult = client.AddTagToContact(contact.Email, "Registrant").Result;
            var addSubscribeResult = client.AddContactToList(contact.Email, "Subscribers").Result;
          }
        }

      }

    }


      /// <summary>
      /// This method is called after registration is complete, but before the validation email is sent to the new user.  
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      protected void CreateUserWizard1_OnSendingMail(object sender, MailMessageEventArgs e)
    {
      // if were running the application on a local machine, we should cancel the email, otherwise we'll
      // get some exceptions
      //if (Globals.CSWRSettings.ApplicationState == ApplicationState.Local.ToString().ToLower())
      //{
      //  e.Cancel = true;
      //}
    }


    protected void cuwCreateUserWizard_CreateUserError(object sender, CreateUserErrorEventArgs e)
    {

    }



    [WebMethod]
    public static string VerifyCaptcha(string response)
    {
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
      string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
      return (new WebClient()).DownloadString(url);
    }


  }
}