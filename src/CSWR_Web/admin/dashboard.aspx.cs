using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.Admin.Users
{
  public partial class Dashboard : BasePage
  {

    private MembershipUserCollection AllUsers { get; set; }


    protected void Page_Load(object sender, EventArgs e)
    {
      if (!this.IsPostBack)
      {
        this.AllUsers = Membership.GetAllUsers();
        LoadUserData();
        LoadSheetTotals();
      }
    }

    private void LoadUserData()
    {
      int registrationsToday = 0;
      int registrationsYesterday = 0;
      int returnedCount = 0;

      foreach (MembershipUser currentUser in this.AllUsers)
      {
        // count today's registrations
        if (currentUser.CreationDate.Date == DateTime.Today.Date)
        {
          registrationsToday++;
        }
        // count yesterday's registrations
        if (currentUser.CreationDate.Date == DateTime.Today.Subtract(new TimeSpan(1, 0, 0, 0)))
        {
          registrationsYesterday++;
        }
        // calculate email subscription totals
        //ProfileCommon targetProfile = Profile.GetProfile(currentUser.UserName);
        //if (targetProfile.EmailNotifications == true)
        //{
        //  subscriptionCount++;
        //}
        // count users who have logged-in again after their first visit
        if (currentUser.CreationDate != currentUser.LastActivityDate)
        {
          returnedCount++;
        }
      }

      //litTotalUsers.Text = this.AllUsers.Count.ToString();
      //litOnlineUsers.Text = Membership.GetNumberOfUsersOnline().ToString();
      //litRegistrationsToday.Text = registrationsToday.ToString();
      //litRegistrationsYesterday.Text = registrationsYesterday.ToString();

      BindOnlineUsers();

      //double subscribedPercentage = (subscriptionCount * 100) / this.AllUsers.Count;
      //litPercentageSubscribed.Text = subscribedPercentage.ToString();
      double returnedPercentage = (returnedCount * 100) / this.AllUsers.Count;
      //litPercentageReturned.Text = returnedPercentage.ToString();
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
      //gvOnlineUsers.DataSource = onlineUsers;
      //gvOnlineUsers.DataBind();
    }


    private void LoadSheetTotals()
    {
      //litTotalUsers.Text = Membership.GetAllUsers().Count.ToString();

      int totalSheets = 0; // CheatSheet.GetCheatSheetCount(true);  
      int totalMemberSheets = CheatSheet.GetCheatSheetCount(UserCategory.MEMBER);
      int totalVisitorSheets = CheatSheet.GetCheatSheetCount(UserCategory.VISITOR);
      totalSheets = totalMemberSheets + totalVisitorSheets;

      // Load sheet summaries
      litTotalSheets.Text = totalSheets.ToString();
      litTotalVisitorSheets.Text = totalVisitorSheets.ToString();
      litTotalMemberSheets.Text = totalMemberSheets.ToString();

      // Load football sheet summaries
      int totalFootballMemberSheetCount = CheatSheet.GetCheatSheetCount(UserCategory.MEMBER, "FOO");
      int totalFootballVisitorSheetCount = CheatSheet.GetCheatSheetCount(UserCategory.VISITOR, "FOO");
      int totalFootballSheetCount = totalFootballMemberSheetCount + totalFootballVisitorSheetCount;
      litTotalFootballSheets.Text = totalFootballSheetCount.ToString();
      litTotalFootballMemberSheets.Text = totalFootballMemberSheetCount.ToString();
      litTotalFootballVisitorSheets.Text = totalFootballVisitorSheetCount.ToString();

      // Load racing sheet summaries
      int totalRacingMemberSheetCount = CheatSheet.GetCheatSheetCount(UserCategory.MEMBER, "RAC");
      int totalRacingVisitorSheetCount = CheatSheet.GetCheatSheetCount(UserCategory.VISITOR, "RAC");
      int totalRacingSheetCount = totalRacingMemberSheetCount + totalRacingVisitorSheetCount;
      litTotalRacingSheets.Text = totalRacingSheetCount.ToString();
      litTotalRacingMemberSheets.Text = totalRacingMemberSheetCount.ToString();
      litTotalRacingVisitorSheets.Text = totalRacingVisitorSheetCount.ToString();
    }

    private int GetCorruptSheetCount(string sportCode)
    {
      List<int> badSheets = new List<int>();
      int badSheetCounter = 0;

      List<CheatSheet> allCheatSheets = CheatSheet.GetCheatSheets("FOO");
      foreach (CheatSheet currentSheet in allCheatSheets)
      {
        if (currentSheet.Validate())
        {
          badSheetCounter++;
        }
      }
      return badSheetCounter;
    }

    protected void butCheckCorruption_Click(object sender, EventArgs e)
    {
      labFootballCorruptionCount.Text = GetCorruptSheetCount("FOO").ToString();
      butCorrectFootballCorruption.Visible = true;
    }

    protected void butCorrectFootballCorruption_Click(object sender, EventArgs e)
    {
      // correct all sheets
      List<CheatSheet> allCheatSheets = CheatSheet.GetCheatSheets("FOO");
      foreach (CheatSheet currentSheet in allCheatSheets)
      {
        currentSheet.Validate();
      }
      // re-check the counts
      labFootballCorruptionCount.Text = GetCorruptSheetCount("FOO").ToString();
    }


    protected void butCalcRacingADP_Click(object sender, EventArgs e)
    {

      ADPManager.CalculateADP("RAC", SportSeason.GetCurrentSportSeason("RAC").LastSeasonCode, "DR", 14);
    }


    protected void butClearCache_Click(object sender, EventArgs e)
    {
      List<string> keys = new List<string>();
      // retrieve application Cache enumerator
      IDictionaryEnumerator enumerator = Cache.GetEnumerator();
      // copy all keys that currently exist in Cache
      while (enumerator.MoveNext())
      {
        keys.Add(enumerator.Key.ToString());
      }
      // delete every key from cache
      for (int i = 0; i < keys.Count; i++)
      {
        Cache.Remove(keys[i]);
      }
    }


    protected void butClearSessions_Click(object sender, EventArgs e)
    {
      Session.Clear();
    }
  }
}