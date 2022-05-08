using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.Security;
using System.Web.UI.WebControls;
using ActiveCampaign;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.Admin.Users
{
  public partial class ManageUsers : BasePage
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

        this.AllUsers = Membership.GetAllUsers();
        string[] alphabet = "A;B;C;D;E;F;G;H;I;J;K;L;M;N;O;P;Q;R;S;T;U;V;W;X;Y;Z;All".Split(';');
        repAlphabet.DataSource = alphabet;
        repAlphabet.DataBind();
        gvUsers.Attributes.Add("SearchText", "");
        BindUsers(false);
      }
    }



    /// <summary>
    /// When a letter is clicked, we need to perform the appropriate search
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void repAlphabet_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
      gvUsers.Attributes.Add("SearchByEmail", false.ToString());
      
      if (e.CommandArgument.ToString().Length == 1)
      {
        gvUsers.Attributes.Add("SearchText", e.CommandArgument.ToString() + "%");
        BindUsers(false);
      }
      else
      {
        gvUsers.Attributes.Add("SearchText", "");
        BindUsers(false);
      }
    }

    private void BindUsers(bool reloadAllUsers)
    {
      // after a delete, we need to remind the users to the allUsers collection so
      // that the deleted user is removed from the collection.
      
      if (reloadAllUsers)
      {
        this.AllUsers = Membership.GetAllUsers();
      }

      MembershipUserCollection users = null;
      
      // if a letter was clicked, load it as the search text
      string searchText = "";
      if (!string.IsNullOrEmpty(gvUsers.Attributes["SearchText"]))
      {
        searchText = gvUsers.Attributes["SearchText"];
      }

      // if email address was provied, search by email
      bool searchByEmail = false;
      if (!string.IsNullOrEmpty(gvUsers.Attributes["SearchByEmail"]))
      {
        searchByEmail = bool.Parse(gvUsers.Attributes["searchByEmail"]);
      }

      // grab the appropriate collection of users
      if (searchText.Length > 0)
      {
        if (searchByEmail)
        {
          users = Membership.FindUsersByEmail(searchText);
        }
        else
        {
          users = Membership.FindUsersByName(searchText);
        }
      }
      else
      {
        users = this.AllUsers;
      }

      // since the MembershipUserCollection does not support Linq we have to move
      // the users to a generic list
      List<MembershipUser> testUsers = new List<MembershipUser>();
      foreach (MembershipUser currentUser in users)
      {
        testUsers.Add(currentUser);
      }

      // create a new collection of users to bind to the gridview
      List<MembershipUser> bindUsers = new List<MembershipUser>();
      // sort the list of users using linq
      bindUsers = testUsers.OrderByDescending(x => x.CreationDate).ToList();
      // bind the sorted users to the gridview
      gvUsers.DataSource = bindUsers;
      gvUsers.DataBind();

    }

    protected void butSearch_Click(object sender, EventArgs e)
    {
      bool searchByEmail = (ddlSearchType.SelectedValue == "E-mail");
      gvUsers.Attributes.Add("SearchText", "%" + tbSearchText.Text + "%");
      gvUsers.Attributes.Add("SearchByEmail", searchByEmail.ToString());
      BindUsers(false);
    }

    protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      string userName = gvUsers.DataKeys[e.RowIndex].Value.ToString();
      MembershipUser targetUser = Membership.GetUser(userName);
      string email = targetUser.Email;

      // Delete the user's profile
      ProfileManager.DeleteProfile(userName);
      // Delete the user from the membership store
      Membership.DeleteUser(userName);
      // Delete the user's cheat shets
      CheatSheet.DeleteUserCheatSheets(userName);

      // delete user from Active Campaign
      using (var client = new ActiveCampaignClient())
      {
        var deleteByEmailResult = client.DeleteContactByEmail(email).Result;
      }

      BindUsers(true);
    }

    protected void gvUsers_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        ImageButton btn = e.Row.Cells[7].Controls[0] as ImageButton;
        if (btn != null)
        {
          btn.OnClientClick = "if (confirm('Are you sure you want to delete this user account?') == false) return false;";
        }
      }
    }

    protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      // if we're binding to a data row, manipulate the controls within
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        // get references to the appropriate controls
        MembershipUser boundUser = (MembershipUser)e.Row.DataItem;
        CheckBox cbSubscribed = (CheckBox)e.Row.FindControl("cbSubscribed");
        Label labCreated = (Label)e.Row.FindControl("labCreated");
        Label labLastActivity = (Label)e.Row.FindControl("labLastActivity");

        ProfileCommon userProfile = Profile.GetProfile(boundUser.UserName);
        if (userProfile != null)
        {
          if (userProfile.EmailNotifications)
          {
            cbSubscribed.Checked = true;
          }
        }

        DateTime createdCentralTime = boundUser.CreationDate.ToCentralStandardDateTime();
        labCreated.Text = createdCentralTime.ToShortDateString() + " " + createdCentralTime.ToShortTimeString();

        DateTime lastActivityCentralTime = boundUser.LastActivityDate.ToCentralStandardDateTime();
        labLastActivity.Text =  lastActivityCentralTime.ToShortDateString() + " " + lastActivityCentralTime.ToShortTimeString();

      }    
    }


    protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      gvUsers.PageIndex = e.NewPageIndex;
      BindUsers(true);
    }


}
}