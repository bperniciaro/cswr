using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_users_generateuserpassword : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (IsPostBack)
    {
      string newPassword = GenerateNewPassword(tbEmail.Text);
      labNewPassword.Text = newPassword;
      bool result = UpdateUserPassword(tbEmail.Text, newPassword);
      if(result)
      {
        labOperationResult.Text = "Success";
      }
      else
      {
        labOperationResult.Text = "Error";
      }
    }
  }

  public string GenerateNewPassword(string email)
  {
    var m_userName = Membership.GetUserNameByEmail(email);
    var m_user = Membership.GetUser(m_userName);
    return m_user.ResetPassword();
  }

  public bool UpdateUserPassword(string email, string newPassword)
  {
    bool success = false;
    //var resetPassword = GenerateNewPassword(email);
    //var currentUser = Membership.GetUser(Membership.GetUserNameByEmail(email), true);
    //bool changePasswordResult = currentUser.ChangePassword(resetPassword, newPassword);
    
    MailMessage message = new MailMessage();

    message.From = new MailAddress("accounts@cheatsheetwarroom.com");
    message.To.Add(new MailAddress(email));
    message.Subject = "Password Reset Request";
    message.Body = "Your new password: " + newPassword;
    SmtpClient client = new SmtpClient();

    try
    {
      client.Send(message);
      success = true;
    }
    catch { }

    if(success)
    {
      return true;
    }
    else
    {
      return false;
    }

  }



}