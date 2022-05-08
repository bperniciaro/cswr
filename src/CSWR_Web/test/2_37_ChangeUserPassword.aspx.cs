using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test_ChangeUserPassword : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (IsPostBack)
    {
      string newPassword = ResetPassword(tbEmail.Text);
      labNewPassword.Text = newPassword;
      ChangeLostPassword(tbEmail.Text, newPassword);
    }
  }

  public string ResetPassword(string email)
  {
    var m_userName = Membership.GetUserNameByEmail(email);
    var m_user = Membership.GetUser(m_userName);
    return m_user.ResetPassword();
  }

  public bool ChangeLostPassword(string email, string newPassword)
  {
    var resetPassword = ResetPassword(email);
    var currentUser = Membership.GetUser(Membership.GetUserNameByEmail(email), true);
    return currentUser.ChangePassword(resetPassword, newPassword);

  }


}