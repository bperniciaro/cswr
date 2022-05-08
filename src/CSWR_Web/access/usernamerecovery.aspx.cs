using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Security;
using System.Web.Services;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class UsernameRecovery : BasePage
  {

    protected void Page_Init(object sender, EventArgs e)
    {
      if (this.User.Identity.IsAuthenticated)
      {
        Response.Redirect("~/");
      }
      if (!IsPostBack)
      {
        mbMessageBox.Visible = false;
      }
    }

    protected void butSubmit_Click(object sender, EventArgs e)
    {
      bool success = false;

      string targetUsername = Membership.GetUserNameByEmail(tbEmailAddress.Text);
      StringBuilder userMessage = new StringBuilder();

      mbMessageBox.Visible = true;
        
      if (targetUsername != null)
      {
        // check to see if the user is locked out
        if (Membership.GetUser(targetUsername).IsLockedOut)
        {
          panControls.Visible = false;
          userMessage.Append("Your account is locked due to failed login attempts.  Please <a href='mailto:admin@cheatsheetwarroom.com'>contact an administrator</a> for further assistance.");
          mbMessageBox.MessageType = MessageType.ERROR;
          mbMessageBox.Message = userMessage;
        }
        else
        {
          MailMessage message = new MailMessage();

          message.From = new MailAddress("accounts@cheatsheetwarroom.com");
          message.To.Add(new MailAddress(tbEmailAddress.Text));
          message.Subject = "Username Recovery - Cheatsheetwarroom.com";
          message.Body = "Your username is '" + targetUsername + "'";
          SmtpClient client = new SmtpClient();

          try
          {
            client.Send(message);
            success = true;
          }
          catch { }

          if (success)
          {
            panControls.Visible = false;
            userMessage.Append("Your username has been sent to the email address provided.  If you need further assistance, please <a href='mailto:admin@cheatsheetwarroom.com'>contact an administrator</a> for further assistance.");
            mbMessageBox.MessageType = MessageType.SUCCESS;
            mbMessageBox.Message = userMessage;
          }
          else
          {
            userMessage.Append("There was a problem sending your username to the email address you provided upon registration.  Please update your email address or <a href='mailto:admin@cheatsheetwarroom.com'>contact an administrator</a> for further assistance.");
            mbMessageBox.MessageType = MessageType.ERROR;
            mbMessageBox.Message = userMessage;
          }
        }
      }
      else  
      {
        userMessage.Append("The email address your provided cannot be found in our records.  Please <a href='mailto:admin@cheatsheetwarroom.com'>contact an administrator</a> for further assistance.");
        mbMessageBox.MessageType = MessageType.ERROR;
        mbMessageBox.Message = userMessage;
      }
    }

    protected static string ReCaptcha_Key = ConfigurationManager.AppSettings["Google.ReCaptcha.SiteKey"];
    protected static string ReCaptcha_Secret = ConfigurationManager.AppSettings["Google.ReCaptcha.SecretKey"];

    [WebMethod]
    public static string VerifyCaptcha(string response)
    {
      string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
      return (new WebClient()).DownloadString(url);
    }
  }
}