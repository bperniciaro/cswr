using System;
using System.Text;
using BP.CheatSheetWarRoom.BLL.Sheets;
using BP.CheatSheetWarRoom.UI.UserControls;

namespace BP.CheatSheetWarRoom.UI.Admin.Sheets
{
  public partial class RACSheetSettings : System.Web.UI.Page
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
      SportSetting showSupplementalSourcesSetting = SportSetting.GetSportSetting("RAC", "SUPRNK");
      if (showSupplementalSourcesSetting != null)
      {
        if (showSupplementalSourcesSetting.SettingValue == "1")
        {
          cbShowSupps.Checked = true;
        }
      }

      SportSetting showAffiliateAdsSetting = SportSetting.GetSportSetting("RAC", "SHOAFF");
      if (showAffiliateAdsSetting != null)
      {
        if (showAffiliateAdsSetting.SettingValue == "1")
        {
          cbShowAffiliates.Checked = true;
        }
      }
    }

    protected void butSaveSheetSettings_Click(object sender, EventArgs e)
    {
      bool result = true;
      MessageBox mbStatus = (MessageBox)upCorruption.FindControl("mbStatus");

      // show supplementals
      string showSuppRankingsSetting = (cbShowSupps.Checked == true) ? "1" : "0";
      if (!SportSetting.UpdateSportSettingValue("RAC", "SUPRNK", showSuppRankingsSetting))
      {
        result = false;
      }

      // show affiliates
      string showAffiliateAdsSetting = (cbShowAffiliates.Checked == true) ? "1" : "0";
      if (!SportSetting.UpdateSportSettingValue("RAC", "SHOAFF", showAffiliateAdsSetting))
      {
        result = false;
      }

      // show message
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

    protected void butCalcADP_Click(object sender, EventArgs e)
    {
      //ADPManager.CalculateADP("RAC", "2012", "DR");
    }
  }
}