using System;
using System.Collections.Generic;
using System.Web.Security;

namespace BP.CheatSheetWarRoom.UI.Admin.Users
{
  public partial class UserSummary : BasePage
  {


    private MembershipUserCollection AllUsers { get; set; }


    /// <summary>
    /// Load the total users & total online users, then bind an alphabet to the repeater
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!this.IsPostBack)
      {
        int registrationsToday = 0;

        this.AllUsers = Membership.GetAllUsers();
        litTotalUsers.Text = this.AllUsers.Count.ToString();
        litOnlineUsers.Text = Membership.GetNumberOfUsersOnline().ToString();
        BindOnlineUsers();

        foreach (MembershipUser member in this.AllUsers)
        {
          if (member.CreationDate.Date == DateTime.Today.Date)
          {
            registrationsToday++;
          }
        }
        litRegistrationsToday.Text = registrationsToday.ToString();

      }
    }

    private void BindOnlineUsers()
    {
      List<MembershipUser> onlineUsers = new List<MembershipUser>();
      foreach (MembershipUser targetUser in this.AllUsers)
      {
        if (targetUser.IsOnline)
        {
          onlineUsers.Add(targetUser);
        }
      }
      gvOnlineUsers.DataSource = onlineUsers;
      gvOnlineUsers.DataBind();


    }

  
  }
}