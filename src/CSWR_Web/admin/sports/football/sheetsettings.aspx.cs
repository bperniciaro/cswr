using System;
using System.Collections.Generic;
using System.Text;
using BP.CheatSheetWarRoom.BLL.Sheets;
using BP.CheatSheetWarRoom.UI.UserControls;

namespace BP.CheatSheetWarRoom.UI.Admin.Sheets
{
  public partial class SheetSettings : BasePage
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        LoadControls();
      }
    }


    private void LoadControls()
    {
      List<SupplementalSource> allSupplmentalSources = SupplementalSource.GetSupplementalSources();

      /* Bind the supplemental source dropdowns */
      // Supp Source 1
      ddlSuppSource1.DataSource = allSupplmentalSources;
      ddlSuppSource1.DataBind();
      ddlSuppSource1.SelectedValue = Setting.GetSetting("DEFSS1").SettingValue;
      // Supp Source 2
      ddlSuppSource2.DataSource = allSupplmentalSources;
      ddlSuppSource2.DataBind();
      ddlSuppSource2.SelectedValue = Setting.GetSetting("DEFSS2").SettingValue;
      // Supp Source 3
      ddlSuppSource3.DataSource = allSupplmentalSources;
      ddlSuppSource3.DataBind();
      ddlSuppSource3.SelectedValue = Setting.GetSetting("DEFSS3").SettingValue;

      // Check 'show supplemental sources' sport setting       cbShowSupps.Checked = false;
      SportSetting showSupplementalSourcesSetting = SportSetting.GetSportSetting("FOO", "SUPRNK");
      if(showSupplementalSourcesSetting != null)  
      {
        if(showSupplementalSourcesSetting.SettingValue == "1")  
        {
          cbShowSupps.Checked = true;
        }
      }

      SportSetting showShowAffiliateAdsSetting = SportSetting.GetSportSetting("FOO", "SHOAFF");
      if (showShowAffiliateAdsSetting != null)
      {
        if (showShowAffiliateAdsSetting.SettingValue == "1")
        {
          cbShowAffiliates.Checked = true;
        }
      }

      // Calculate ADP
      SportSetting calculateADP = SportSetting.GetSportSetting("FOO", "CALADP");
      if (calculateADP != null)
      {
        if (calculateADP.SettingValue == "1")
        {
          cbCalculateADP.Checked = true;
        }
      }

      // Timespan in Days
      SportSetting timespanInDays = SportSetting.GetSportSetting("FOO", "TSNDAY");
      if (timespanInDays != null)
      {
        tbTimespanInDays.Text = timespanInDays.SettingValue;
      }

    }


    protected void butSaveSheetSettings_Click(object sender, EventArgs e)
    {
      bool result = true;
      MessageBox mbStatus = (MessageBox)upCorruption.FindControl("mbStatus");

      string showSuppRankingsSetting = (cbShowSupps.Checked == true) ? "1" : "0";
      if (!SportSetting.UpdateSportSettingValue("FOO", "SUPRNK", showSuppRankingsSetting))
      {
        result = false;
      }

      // show affiliates
      string showAffiliateAdsSetting = (cbShowAffiliates.Checked == true) ? "1" : "0";
      if (!SportSetting.UpdateSportSettingValue("FOO", "SHOAFF", showAffiliateAdsSetting))
      {
        result = false;
      }

      // supplemental source 1
      if (!Setting.UpdateSettingValue("DEFSS1", ddlSuppSource1.SelectedValue))
      {
        result = false;
      }

      // supplemental source 2
      if (!Setting.UpdateSettingValue("DEFSS2", ddlSuppSource2.SelectedValue))
      {
        result = false;
      }

      // supplemental source 3
      if (!Setting.UpdateSettingValue("DEFSS3", ddlSuppSource3.SelectedValue))
      {
        result = false;
      }

      // calculate ADP
      string calculateADP = (cbCalculateADP.Checked == true) ? "1" : "0";
      if (!SportSetting.UpdateSportSettingValue("FOO", "CALADP", calculateADP))
      {
        result = false;
      }

      // timespan in days
      int timespanInDays = 0;
      int.TryParse(tbTimespanInDays.Text, out timespanInDays);
      if (!SportSetting.UpdateSportSettingValue("FOO", "TSNDAY", timespanInDays.ToString()))
      {
        result = false;
      }


      if (result)
      {
        mbStatus.MessageType = MessageType.SUCCESS;
        mbStatus.Message = new StringBuilder("Success");
      }
      else
      {
        mbStatus.MessageType = MessageType.ERROR;
        mbStatus.Message = new StringBuilder("Error Updating");
      }


    }


    protected void butDeleteOldFOOSheets_Click(object sender, EventArgs e)
    {
      int recentSheets = 0;
      int oldSheets = 0;
      int recentUserSheets = 0;

      List<CheatSheet> allFooCheatSheets = CheatSheet.GetCheatSheets("FOO");
      foreach (CheatSheet currentSheet in allFooCheatSheets)
      {
        if ((DateTime.Now - currentSheet.LastUpdated) > new TimeSpan(15, 0, 0, 0))
        {
          oldSheets++;
          //currentSheet.Delete();
        }
        else
        {
          recentSheets++;
          if (currentSheet.Username != String.Empty)
          {
            recentUserSheets++;
          }
        }
      }
      int i = 0;
    }

    protected void butDeleteFOOVisitorSheets_Click(object sender, EventArgs e)
    {
      List<CheatSheet> allFooCheatSheets = CheatSheet.GetCheatSheets("FOO");
      foreach (CheatSheet currentSheet in allFooCheatSheets)
      {
        if (currentSheet.Username == String.Empty)
        {
          currentSheet.Delete();
        }
      }
    }
}
}