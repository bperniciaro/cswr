using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace BP.CheatSheetWarRoom.UI.Admin.Users
{
  public partial class EditUser : BasePage
  {

    string userName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
      // retrieve the username from the querystring
      userName = this.Request.QueryString["UserName"];
      labRolesFeedbackOK.Visible = false;
      labProfileFeedbackOK.Visible = false;

      // if not postback, load all of the page controls
      if (!this.IsPostBack)
      {
        upUserProfile.UserName = userName;
        MembershipUser user = Membership.GetUser(userName);
        litUserName.Text = user.UserName;
        hypEmail.Text = user.Email;
        hypEmail.NavigateUrl = "mailto:" + user.Email;
        litRegistered.Text = user.CreationDate.ToString("f");
        litLastLogin.Text = user.LastLoginDate.ToString("f");
        litLastActivity.Text = user.LastActivityDate.ToString("f");
        cbOnlineNow.Checked = user.IsOnline;
        cbApproved.Checked = user.IsApproved;
        cbLockedOut.Checked = user.IsLockedOut;
        cbLockedOut.Enabled = user.IsLockedOut;
        BindRoles();
      }
      upUserProfile.MessageEvent += new Globals.MessageEventHandler(userControl_MessageHandler);
    }

    public void userControl_MessageHandler(object sender, Globals.MessageBoxEventArgs e)
    {
      mbStatus.MessageType = e.MessageType;
      mbStatus.Message = new StringBuilder(e.Text);
      mbStatus.SetMessage();
    }

    /// <summary>
    /// Load the available roles, then indicate if the user is a member of each
    /// </summary>
    protected void BindRoles()
    {
      cblRoles.DataSource = Roles.GetAllRoles();
      cblRoles.DataBind();
      foreach(string role in Roles.GetRolesForUser(userName))  
        cblRoles.Items.FindByText(role).Selected = true;
    }

    /// <summary>
    /// Update a user's roles based on the roles selected.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void butUpdateRoles_Click(object sender, EventArgs e)
    {
      // first remove the user from all roles
      string[] currRoles = Roles.GetRolesForUser(userName);
      if (currRoles.Length > 0)
        Roles.RemoveUserFromRoles(userName, currRoles);

      // then add the user to the selected roles
      List<string> newRoles = new List<string>();
      foreach(ListItem item in cblRoles.Items)  {
        if(item.Selected)
          newRoles.Add(item.Text);
      }
      if (newRoles.Count > 0)
      {
        Roles.AddUserToRoles(userName, newRoles.ToArray());
      }
      labRolesFeedbackOK.Visible = true;    
    }

    /// <summary>
    /// When the user clicks the button to create a new role, we first check to see if that role
    /// exists, if it doesn't we create it and remind the roles
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void butCreateRole_Click(object sender, EventArgs e)
    {
      if (!Roles.RoleExists(tbNewRole.Text.Trim()))
      {
        Roles.CreateRole(tbNewRole.Text.Trim());
        BindRoles();
      }
    }

    /// <summary>
    /// If the approved status gets changed, we generate a postback then update the user
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cbApproved_CheckChanged(object sender, EventArgs e)
    {
      MembershipUser user = Membership.GetUser(userName);
      user.IsApproved = cbApproved.Checked;
      Membership.UpdateUser(user);
    }


    protected void cbLockedOut_CheckChanged(object sender, EventArgs e)
    {
      if (!cbLockedOut.Checked)
      {
        MembershipUser user = Membership.GetUser(userName);
        user.UnlockUser();
        cbLockedOut.Enabled = false;
      }
    }

    protected void butUpdateProfile_Click(object sender, EventArgs e)
    {
      upUserProfile.SaveProfile();
      labProfileFeedbackOK.Visible = true;
    }

  }
}