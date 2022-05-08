using System;
using System.Web.Security;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class UserProfile : System.Web.UI.UserControl
  {

    /// <summary>
    /// Define an event handler
    /// </summary>
    public event Globals.MessageEventHandler MessageEvent;

    /// <summary>
    /// Define an event to handle the event
    /// </summary>
    /// <param name="e"></param>
    protected virtual void OnEvent(Globals.MessageBoxEventArgs e)
    {
      if (MessageEvent != null)
        MessageEvent(this, e);
    }

    /// <summary>
    /// If this property is populated we're editing a particular user either in the 'Edit Profile' page or 
    /// 'Edit User' administrative page.
    /// </summary>
    public string UserName
    {
      get 
      {
        return ViewState["UserName"] == null ? String.Empty : ViewState["UserName"].ToString();
      }
      set 
      { 
        ViewState["UserName"] = value; 
      }
    }

    /// <summary>
    /// Indicates if the user's email address should be shown.  During registration it is hidden
    /// </summary>
    private bool _hideEmailAddress = false;
    public bool HideEmailAddress
    {
      get
      {
        return _hideEmailAddress;
      }
      set
      {
        _hideEmailAddress = value;
      }
    }


    /// <summary>
    /// We load the controls on the profile page and then load the profile
    /// for either the logged-in user or the specified user (if the admin is editing
    /// a particular user
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

      if (!IsPostBack)
      {

        // if the username property is populated we're editing a particular user to load
        // their personal Profile properties
        if (this.UserName.Length > 0)
        {
          ProfileCommon profile =  this.Profile.GetProfile(this.UserName);
          tbFirstName.Text = profile.FirstName;
          ddlNewsletter.SelectedValue = (profile.EmailNotifications == true) ? "1" : "0";
          // interest in various sports
          //cbFantasyFootball.Checked = profile.Football.HasInterest;
          //cbFantasyRacing.Checked = profile.Racing.HasInterest;
          //cbFantasyBaseball.Checked = profile.Baseball.HasInterest;
          //cbFantasyBasketball.Checked = profile.Basketball.HasInterest;
          //cbFantasyHockey.Checked = profile.Hockey.HasInterest;
        }


        // we only show the email address if we're not in registration because the user already
        // provided it during the registration process
        if (!this.HideEmailAddress)
        {
          tbEmailAddress.Text = Membership.GetUser(this.UserName).Email;
        }
        else
        {
          // we're in administration, so ignore all email-related fields
          trEmailAddress.Visible = false;
          revEmailPattern.Enabled = false;
          rfvEmailRequired.Enabled = false;
          trEmailAddress2.Visible = false;
          revEmailPattern2.Enabled = false;
        }
      }

    }



    public void SaveProfile()
    {
      if (!this.HideEmailAddress)
      {
        MembershipUser targetUser = Membership.GetUser(this.UserName);
        // if the user tried to change their email..
        if (targetUser.Email != tbEmailAddress.Text)
        {
          // ensure that the email requested isn't already taken
          if (Membership.FindUsersByEmail(tbEmailAddress.Text).Count == 0)
          {
            if (tbEmailAddress.Text != tbEmailAddress2.Text)
            {
              OnEvent(new Globals.MessageBoxEventArgs("Your new email address does not match the confirmation email address.", MessageType.ERROR));
              tbEmailAddress.Text = String.Empty;
              tbEmailAddress2.Text = String.Empty;
              return;
            }
            else
            {
              targetUser.Email = tbEmailAddress.Text;
              // save the email address
              Membership.UpdateUser(targetUser);
            }
          }
          else
          {
            OnEvent(new Globals.MessageBoxEventArgs("Email already in use.  Please choose another email address.", MessageType.ERROR));
            return;
          }
        }
      }

      // if the username property contains an empty string, save the current user's
      // profile, otherwise save the profile for the specified user
      ProfileCommon profile;
      // if there is a username we're in administration, so get the profile for the specified user
      if (this.UserName.Length > 0)
      {
        profile = this.Profile.GetProfile(this.UserName);
      }
      // if the username is empty we're just saving ourselves
      else
      {
        profile = this.Profile;
      }

      // save all of the properties
      profile.FirstName = tbFirstName.Text;
      profile.EmailNotifications = (ddlNewsletter.SelectedValue == "1") ? true : false;
      // sport interest
      //profile.Football.HasInterest = cbFantasyFootball.Checked;
      //profile.Racing.HasInterest = cbFantasyRacing.Checked;
      //profile.Baseball.HasInterest = cbFantasyBaseball.Checked;
      //profile.Basketball.HasInterest = cbFantasyBasketball.Checked;
      //profile.Hockey.HasInterest = cbFantasyHockey.Checked;
      profile.Save();

      tbEmailAddress2.Text = String.Empty;

      OnEvent(new Globals.MessageBoxEventArgs("Profile Saved.", MessageType.SUCCESS));
    }
    

  
  }
}