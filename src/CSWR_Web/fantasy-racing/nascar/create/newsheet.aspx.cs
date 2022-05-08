using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.BLL.Sheets;
using BP.CheatSheetWarRoom.UI.UserControls;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class NewSheet : BasePage
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      // if there are no user sheets then we want to hide the navigation
      if (CheatSheet.GetCheatSheetCount(this.User.Identity.Name, SessionHandler.CurrentSportCode) == 0)
      {
        scmlnNavigation.Visible = false;
      }
      // load the appropriate controls
      if (!IsPostBack)
      {
        // load various controls based on sport
        ConfigureControls();
      }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        SessionHandler.CurrentSportCode = "RAC";
        scmlnNavigation.CheatSheetID = Profile.Racing.LastRacingCheatSheetID;
        scmlnNavigation.CurrentStage = SheetCreationManageLevelNavigation.CreationStage.NEWSHEET;
      }
    }


    private void ConfigureControls()
    {
      SportSeason currentSportSeason = SportSeason.GetCurrentSportSeason(SupportedSport.RAC.ToString());

      //trSupplementalSources.Visible = false;
      rblSortTypes.Items[0].Text = "Use CSWR Ranking";
      rblSortTypes.Items[0].Value = "SuppSource";


      rblSortTypes.Items[1].Text = currentSportSeason.LastSeasonCode + " Rankings";

      // load a default sheet name
      tbSheetName.Text = "My Drivers";
    }

    /// <summary>
    /// When the user clicks submit, we create the cheat sheet based on the confired parameters
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void butSubmit_Click(object sender, EventArgs e)
    {
      List<Position> cheatSheetPositions = new List<Position>();
      int newSheetID = 0;
      SportSeason currentSeason = SportSeason.GetCurrentSportSeason(SupportedSport.RAC.ToString());

      // Determine which statseason is relevant
      string statSeasonCode = currentSeason.LastSeasonCode;

      //simulate a collection of position choices (for when we eventually support multiple positions per sheet)
      cheatSheetPositions.Add(new Position("DR", String.Empty, String.Empty));

      // create the new sheet
      switch (rblSortTypes.SelectedValue)
      {
        case "Stats":
          newSheetID = CheatSheet.CreateCheatSheet(SupportedSport.RAC.ToString(), tbSheetName.Text, statSeasonCode, cheatSheetPositions,
                          Helpers.GetDefaultStatCodes("DR"), "PNTS", "ASC", null);
          break;
        case "SuppSource":
          newSheetID = CheatSheet.CreateCheatSheet(SupportedSport.RAC.ToString(), tbSheetName.Text, statSeasonCode, cheatSheetPositions,
                          Helpers.GetDefaultStatCodes("DR"), SupplementalSource.GetSupplementalSource("CSWR").SupplementalSourceID, null);
          break;
      }


      Response.Redirect("~/fantasy-racing/nascar/create/custom-sheet.aspx?Sheet=" + newSheetID.ToString());
    }


  }
}