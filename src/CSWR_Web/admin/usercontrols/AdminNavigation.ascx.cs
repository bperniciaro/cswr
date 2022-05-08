using System;
using System.Collections;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class AdminNavigation : System.Web.UI.UserControl
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      ProcessMainNavigation();
      if (!this.Page.User.IsInRole("Administrator"))
      {
        panMainMenu.Visible = false;
        panFootballMenu.Visible = false;
        panFootballStatsSubMenu.Visible = false;
        panFootballSupplementalsSubMenu.Visible = false;
        panFootballUserSheetsSubMenu.Visible = false;
        panRacingMenu.Visible = false;
        panRacingStatsMenu.Visible = false;
        panRacingSupplementalsMenu.Visible = false;
      }
      if (!IsPostBack)
      {
        SupplementalSource cswrSource = SupplementalSource.GetSupplementalSource("CSWR");
        // QB Sheet
        SupplementalSheet qbSheet = SupplementalSheet.GetSupplementalSheet(FOO.CurrentSeason, cswrSource.SupplementalSourceID, FOO.FOOString, FOOPositionsOffense.QB.ToString());
        if (qbSheet != null)
        {
          hlQBSheet.NavigateUrl = "~/admin/sports/football/supplementals/ranksupplementalplayers.aspx?ID=" + qbSheet.SupplementalSheetID.ToString();
        }
        // RB Sheet
        SupplementalSheet rbSheet = SupplementalSheet.GetSupplementalSheet(FOO.CurrentSeason, cswrSource.SupplementalSourceID, FOO.FOOString, FOOPositionsOffense.RB.ToString());
        if (rbSheet != null)
        {
          hlRBSheet.NavigateUrl = "~/admin/sports/football/supplementals/ranksupplementalplayers.aspx?ID=" + rbSheet.SupplementalSheetID.ToString();
        }
        // WR Sheet
        SupplementalSheet wrSheet = SupplementalSheet.GetSupplementalSheet(FOO.CurrentSeason, cswrSource.SupplementalSourceID, FOO.FOOString, FOOPositionsOffense.WR.ToString());
        if (wrSheet != null)
        {
          hlWRSheet.NavigateUrl = "~/admin/sports/football/supplementals/ranksupplementalplayers.aspx?ID=" + wrSheet.SupplementalSheetID.ToString();
        }
        // TE Sheet
        SupplementalSheet teSheet = SupplementalSheet.GetSupplementalSheet(FOO.CurrentSeason, cswrSource.SupplementalSourceID, FOO.FOOString, FOOPositionsOffense.TE.ToString());
        if (teSheet != null)
        {
          hlTESheet.NavigateUrl = "~/admin/sports/football/supplementals/ranksupplementalplayers.aspx?ID=" + teSheet.SupplementalSheetID.ToString();
        }
        // K Sheet
        SupplementalSheet kSheet = SupplementalSheet.GetSupplementalSheet(FOO.CurrentSeason, cswrSource.SupplementalSourceID, FOO.FOOString, FOOPositionsOffense.K.ToString());
        if (kSheet != null)
        {
          hlKSheet.NavigateUrl = "~/admin/sports/football/supplementals/ranksupplementalplayers.aspx?ID=" + kSheet.SupplementalSheetID.ToString();
        }
        // DF Sheet
        SupplementalSheet dfSheet = SupplementalSheet.GetSupplementalSheet(FOO.CurrentSeason, cswrSource.SupplementalSourceID, FOO.FOOString, FOOPositionsOffense.DF.ToString());
        if (dfSheet != null)
        {
          hlDFSheet.NavigateUrl = "~/admin/sports/football/supplementals/ranksupplementalplayers.aspx?ID=" + dfSheet.SupplementalSheetID.ToString();
        }
      }
    }

    /// <summary>
    /// Determine top-level navigation
    /// </summary>
    private void ProcessMainNavigation()
    {
      // see if we're in the football branch
      if (Globals.isPageInSitemapTree("/football/"))
      {
        ProcessFootballSubNavigation();
        hl_Main_Football.Attributes["class"] = "current";
      }
      // see if we're in the racing branch
      else if (Globals.isPageInSitemapTree("/racing/"))
      {
        ProcessRacingSubNavigation();
        hl_Main_Racing.Attributes["class"] = "current";
      }
      // see if we're in the users branch
      else if (Globals.isPageInSitemapTree("/users/"))
      {
        ProcessUserSubNavigation();
        hl_Main_ManageUsers.Attributes["class"] = "current";
      }
      // see if we're in the health branch
      else if (Globals.isPageInSitemapTree("/health/"))
      {
        hl_Main_Health.Attributes["class"] = "current";
      }
      else
      {
        ProcessAdminSubNavigation();
        hl_Main_Admin.Attributes["class"] = "current";
      }
    }



    private void ProcessAdminSubNavigation()
    {
      panAdminMenu.Visible = true;

      switch (this.Page.AppRelativeVirtualPath.ToLower())
      {
        case "~/admin/dashboard.aspx":
          hl_Admin_Dashboard.Attributes["class"] = "current";
          break;
        case "~/admin/summarystats.aspx":
          hl_Admin_SummaryStats.Attributes["class"] = "current";
          break;
      }
    }


    private void ProcessUserSubNavigation()
    {
      panUsersMenu.Visible = true;

      switch (this.Page.AppRelativeVirtualPath.ToLower())
      {
        case "~/admin/users/manageusers.aspx":
          panUsersMenu.Visible = true;
          hl_Users_ManageUsers.Attributes["class"] = "current";
          break;
        case "~/admin/users/usersummary.aspx":
          panUsersMenu.Visible = true;
          hl_Users_UserSummary.Attributes["class"] = "current";
          break;
        case "~/admin/users/powerusers.aspx":
          panUsersMenu.Visible = true;
          hl_Users_PowerUsers.Attributes["class"] = "current";
          break;
        case "~/admin/users/email/makeemaillists.aspx":
          panUsersEmailSubMenu.Visible = true;
          hl_Users_Email.Attributes["class"] = "current";
          hl_Users_Email_MakeEmailLists.Attributes["class"] = "current";
          break;
        case "~/admin/users/email/generateuserpassword.aspx":
          panUsersMenu.Visible = true;
          hl_Users_GenerateNewPassword.Attributes["class"] = "current";
          break;
        case "~/admin/users/email/importinvalidemails.aspx":
          panUsersEmailSubMenu.Visible = true;
          hl_Users_Email.Attributes["class"] = "current";
          hl_Users_Email_ImportInvalidEmails.Attributes["class"] = "current";
          break;
        case "~/admin/users/upgrade/upgradedusers.aspx":
          panUsersUpgradeSubMenu.Visible = true;
          hl_Users_Upgrade.Attributes["class"] = "current";
          hl_Users_Upgrade_UpgradedUsers.Attributes["class"] = "current";
          break;
        case "~/admin/users/upgrade/managevouchers.aspx":
          panUsersUpgradeSubMenu.Visible = true;
          hl_Users_Upgrade.Attributes["class"] = "current";
          hl_Users_Upgrade_ManageVouchers.Attributes["class"] = "current";
          break;
        case "~/admin/users/upgrade/vouchergenerator.aspx":
          panUsersUpgradeSubMenu.Visible = true;
          hl_Users_Upgrade.Attributes["class"] = "current";
          hl_Users_Upgrade_VoucherGenerator.Attributes["class"] = "current";
          break;
        case "~/admin/users/upgrade/testpaypal.aspx":
          panUsersUpgradeSubMenu.Visible = true;
          hl_Users_Upgrade.Attributes["class"] = "current";
          hl_Users_Upgrade_TestPayPal.Attributes["class"] = "current";
          break;



          
      }
    }


    /// <summary>
    /// Process football navigation
    /// </summary>
    private void ProcessFootballSubNavigation()
    {
      panFootballMenu.Visible = true;
      panFootballUserSheetsSubMenu.Visible = false;
      panFootballPlayersSubMenu.Visible = false;
      panFootballStatsSubMenu.Visible = false;
      panFootballSupplementalsSubMenu.Visible = false;


      hl_Main_Football.Attributes["class"] = "current";
      
      switch (this.Page.AppRelativeVirtualPath)
      {
        case "~/admin/sports/football/players/manageplayers.aspx":
          panFootballPlayersSubMenu.Visible = true;
          hl_Football_Players_Manage.Attributes["class"] = "current";
          hl_Football_Players_ManagePlayers.Attributes["class"] = "current";
          break;
        case "~/admin/sports/football/players/manageplayerstatuses.aspx":
          panFootballPlayersSubMenu.Visible = true;
          hl_Football_Players_Manage.Attributes["class"] = "current";
          hl_Football_Players_ManagePlayerStatuses.Attributes["class"] = "current";
          break;
        case "~/admin/sports/football/players/likelyretired.aspx":
          panFootballPlayersSubMenu.Visible = true;
          hl_Football_Players_ManagePlayers.Attributes["class"] = "current";
          hl_Football_Players_LikelyRetired.Attributes["class"] = "current";
          break;
        case "~/admin/sports/football/players/stats/import/mapplayerids.aspx":
          panFootballStatsSubMenu.Visible = true;
          hl_Football_Players_SeasonStats.Attributes["class"] = "current";
          hl_Football_Stats_MapPlayerID.Attributes["class"] = "current";
          break;
        case "~/admin/sports/football/players/stats/manageplayerseasonstats.aspx":
          panFootballStatsSubMenu.Visible = true;
          hl_Football_Players_SeasonStats.Attributes["class"] = "current";
          hl_Football_Stats_Manage.Attributes["class"] = "current";
          break;
        case "~/admin/sports/football/players/stats/import/importweeklystats.aspx":
          panFootballStatsSubMenu.Visible = true;
          hl_Football_Players_SeasonStats.Attributes["class"] = "current";
          hl_Football_Stats_Weekly_Import.Attributes["class"] = "current";
          break;
        case "~/admin/sports/football/players/stats/import/importseasonstats.aspx":
          panFootballStatsSubMenu.Visible = true;
          hl_Football_Players_SeasonStats.Attributes["class"] = "current";
          hl_Football_Stats_Season_Import.Attributes["class"] = "current";
          break;
        case "~/admin/sports/football/supplementals/managesupplementalsheets.aspx":
          panFootballSupplementalsSubMenu.Visible = true;
          hl_Football_Players_Supplementals.Attributes["class"] = "current";
          hl_Football_Supplementals_Sheets.Attributes["class"] = "current";
          break;
        case "~/admin/sports/football/supplementals/editsupplementalsheet.aspx":
          panFootballSupplementalsSubMenu.Visible = true;
          hl_Football_Players_Supplementals.Attributes["class"] = "current";
          hl_Football_Supplementals_Sheets.Attributes["class"] = "current";
          break;
        case "~/admin/sports/football/supplementals/ranksupplementalplayers.aspx":
          panFootballSupplementalsSubMenu.Visible = true;
          hl_Football_Players_Supplementals.Attributes["class"] = "current";
          hl_Football_Supplementals_Sheets.Attributes["class"] = "current";
          break;
        case "~/admin/sports/football/supplementals/validatesuppsheet.aspx":
          panFootballSupplementalsSubMenu.Visible = true;
          hl_Football_Players_Supplementals.Attributes["class"] = "current";
          break;
        case "~/admin/sports/football/supplementals/managesupplementalsources.aspx":
          panFootballSupplementalsSubMenu.Visible = true;
          hl_Football_Supplementals_Sources.Attributes["class"] = "current";
          break;
        case "~/admin/sports/football/supplementals/scraperankings.aspx":
          panFootballSupplementalsSubMenu.Visible = true;
          hl_Football_Players_Supplementals.Attributes["class"] = "current";
          hl_Football_Supplementals_Scrape.Attributes["class"] = "current";
          break;
        case "~/admin/sports/football/usersheets/archiveusersheets.aspx":
          panFootballUserSheetsSubMenu.Visible = true;
          hl_Football_Players_UserSheets.Attributes["class"] = "current";
          hl_Football_UserSheets_ArchiveUserSheets.Attributes["class"] = "current";
          break;
        case "~/admin/sports/football/usersheets/gradeusersheets.aspx":
          panFootballUserSheetsSubMenu.Visible = true;
          hl_Football_Players_UserSheets.Attributes["class"] = "current";
          hl_Football_UserSheets_GradeUsers.Attributes["class"] = "current";
          break;
        case "~/admin/sports/football/adp/adpmonitor.aspx":
          hl_Football_Players_ADP.Attributes["class"] = "current";
          break;
        case "~/admin/sports/football/sheetsettings.aspx":
          hl_Football_Players_Settings.Attributes["class"] = "current";
          break;
      }

    }



    private void ProcessRacingSubNavigation()
    {
      panRacingMenu.Visible = true;
      hl_Main_Racing.Attributes["class"] = "current";

      switch (this.Page.AppRelativeVirtualPath.ToLower())
      {
        case "~/admin/sports/racing/drivers/managedrivers.aspx":
          hl_Racing_Drivers_ManageDrivers.Attributes["class"] = "current";
          break;
        case "~/admin/sports/racing/drivers/stats/managedriverseasonstats.aspx":
          hl_Racing_Drivers_SeasonStats.Attributes["class"] = "current";
          break;
        case "~/admin/sports/racing/supplementals/managesupplementalsheets.aspx":
          panRacingSupplementalsMenu.Visible = true;
          hl_Racing_Drivers_Supplementals.Attributes["class"] = "current";
          hl_Racing_Supplementals_Sheets.Attributes["class"] = "current";
          break;
        case "~/admin/sports/racing/supplementals/managesupplementalreviews.aspx":
          panRacingSupplementalsMenu.Visible = true;
          hl_Racing_Drivers_Supplementals.Attributes["class"] = "current";
          hl_Racing_Supplementals_Reviews.Attributes["class"] = "current";
          break;
      }

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

  }
}