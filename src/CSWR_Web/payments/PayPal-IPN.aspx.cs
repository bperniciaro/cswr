using System;
using System.Web.Security;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class PayPal_IPN : BasePage
  {
    protected void Page_Load(object sender, EventArgs e)
    {

      Guid userId = new Guid();

      if (Guid.TryParse(Request.Form["custom"].ToString(), out userId))
      {
        MembershipUser currentUser = Membership.GetUser(userId);

        // update
        if (currentUser != null)
        {
          UpgradeUser.ConfirmUpgradeUser(FOO.FOOString, FOO.CurrentSeason, userId);

          //MailMessage message = new MailMessage();
          //message.From = new MailAddress("no-reply@cheatsheetwarroom.com");
          //message.To.Add(new MailAddress("bperniciaro@gmail.com"));
          //message.Subject = "PayPal Validation";
          //message.Body = currentUser.UserName + " bought Cheat Sheet Pro";
          //SmtpClient client = new SmtpClient();

          //try
          //{
          //  client.Send(message);
          //}
          //catch { }
        }

      }



    }
  }
}