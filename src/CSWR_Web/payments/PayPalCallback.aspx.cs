using System;
using BP.CheatSheetWarRoom.UI;

public partial class test_PayPalCallback : BasePage
{
  protected void Page_Load(object sender, EventArgs e)
  {

    var randomGuid = Guid.NewGuid();

    //string userId = Request.Form["custom"];

    Response.Write("count:" + Request.Form.Keys.Count.ToString() + "<br/>");
    foreach (string key in Request.Form.Keys)
    {
      Response.Write(key + ": " + Request.Form[key] + " ");
    }
    Response.Write("custom:" + this.Request["cn"] + "<br/>");

    Response.Write("headers:" + this.Request.Headers.Count.ToString());

    foreach (string key in Request.Headers.Keys)
    {
      Response.Write(key + ": " + Request.Headers[key] + "<br/>");
    }


    //Response.Redirect("~/payments/ThankYou.aspx?Guid=" + randomGuid + "&UserId=" + userId);


    //if (Guid.TryParse(Request.Form["custom"].ToString(), out userId))
    //{
    //  MembershipUser currentUser = Membership.GetUser(userId);

    //  // update
    //  if (currentUser != null)
    //  {
    //    UpgradeUser.ConfirmUpgradeUser(FOO.FOOString, FOO.CurrentSeason, userId);

    //    //MailMessage message = new MailMessage();
    //    //message.From = new MailAddress("no-reply@cheatsheetwarroom.com");
    //    //message.To.Add(new MailAddress("bperniciaro@gmail.com"));
    //    //message.Subject = "PayPal Validation";
    //    //message.Body = currentUser.UserName + " bought Cheat Sheet Pro";
    //    //SmtpClient client = new SmtpClient();

    //    //try
    //    //{
    //    //  client.Send(message);
    //    //}
    //    //catch { }
    //  }

    //}


  }
}