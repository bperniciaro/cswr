using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom;
using BP.CheatSheetWarRoom.BLL.Sheets;

public partial class temp_2_29_RemovePlayerFromSheets : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {

  }

  protected void butSubmit_Click(object sender, EventArgs e)
  {
    var keeperPlayerId = 0;
    if (!int.TryParse(tbKeeperPlayerId.Text, out keeperPlayerId))
    {
      return;
    }

    var gonerPlayerId = 0;
    if (!int.TryParse(tbGonerPlayerId.Text, out gonerPlayerId))
    {
      return;
    }

    // remove from cheat sheets
    if (cbCheatSheets.Checked)
    {
      //AdjustCheatSheets(keeperPlayerId, gonerPlayerId);
    }
    // remove from supp sheets
    if (cbSuppSheets.Checked)
    {
      //AdjustSuppSheets(keeperPlayerId, gonerPlayerId);
    }
    // remove from archived cheat sheets
    if (cbArchivedSheets.Checked)
    {
      AdjustArchivedCheatSheet(keeperPlayerId, gonerPlayerId);
    }

    // Now delete any stas related to the player to be momoved
    using (var cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString))
    {
      var cmd = new SqlCommand("DELETE FROM Sheets_SportSeasonPlayerWeeklyStats WHERE PlayerId= " + gonerPlayerId, cn);
      cn.Open();
      var deletedWeeklyecords = cmd.ExecuteNonQuery();
    }

    // Now delete any stas related to the player to be momoved
    using (var cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString))
    {
      var cmd = new SqlCommand("DELETE FROM Sheets_SportSeasonPlayerSeasonStats WHERE PlayerId= " + gonerPlayerId, cn);
      cn.Open();
      var deletedSeasonalRecords = cmd.ExecuteNonQuery();
    }

    // Now delete any stas related to the player to be momoved
    using (var cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString))
    {
      var cmd = new SqlCommand("DELETE FROM Sheets_ADPPlayerLogs WHERE PlayerId= " + gonerPlayerId, cn);
      cn.Open();
      var deletedSeasonalRecords = cmd.ExecuteNonQuery();
    }

    // Now delete any stas related to the player to be momoved
    using (var cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString))
    {
      var cmd = new SqlCommand("DELETE FROM Sheets_SportSeasonPlayerTeams WHERE PlayerId= " + gonerPlayerId, cn);
      cn.Open();
      var deletedSeasonalRecords = cmd.ExecuteNonQuery();
    }

    // Now delete any stas related to the player to be momoved
    using (var cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString))
    {
      var cmd = new SqlCommand("DELETE FROM Sheets_Players WHERE PlayerId= " + gonerPlayerId, cn);
      cn.Open();
      var deletedSeasonalRecords = cmd.ExecuteNonQuery();
    }

    // Now address ADP players


  }

  private void AdjustCheatSheets(int keeperPlayerId, int gonerPlayerId)
  {
    var allCheatSheets = CheatSheet.GetCheatSheets(FOO.FOOString);

    foreach (var currentCheatSheet in allCheatSheets)
    {
      var keeperPlayerInSheet = false;
      var keeperPlayerItem = currentCheatSheet.Items.Find(x => x.PlayerID == keeperPlayerId);
      if (keeperPlayerItem != null)
      {
        keeperPlayerInSheet = true;
      }

      var gonerPlayerInSheet = false;
      var gonerPlayerItem = currentCheatSheet.Items.Find(x => x.PlayerID == gonerPlayerId);
      if (gonerPlayerItem != null)
      {
        gonerPlayerInSheet = true;
      }

      // if both players are in the sheet, just remove the goner
      if (gonerPlayerInSheet && keeperPlayerInSheet)
      {
        CheatSheet.RemoveCheatSheetItem(currentCheatSheet.CheatSheetID, gonerPlayerId);
      }
      // if only the goner is in the sheet, remove it and insert the keepers in the goner's spot
      if (gonerPlayerInSheet && !keeperPlayerInSheet)
      {
        CheatSheet.RemoveCheatSheetItem(currentCheatSheet.CheatSheetID, gonerPlayerId);
        CheatSheetItem.InsertCheatSheetItem(new CheatSheetItem(currentCheatSheet.CheatSheetID, keeperPlayerId,
          gonerPlayerItem.Seqno, string.Empty, null));
      }
    }
  }

  private void AdjustSuppSheets(int keeperPlayerId, int gonerPlayerId)
  {
    var allSuppSheets = SupplementalSheet.GetSupplementalSheets(FOO.FOOString);

    foreach (var currentSuppSheet in allSuppSheets)
    {
      var keeperPlayerInSheet = false;
      var keeperPlayerItem = currentSuppSheet.Items.Find(x => x.PlayerID == keeperPlayerId);
      if (keeperPlayerItem != null)
      {
        keeperPlayerInSheet = true;
      }

      var gonerPlayerInSheet = false;
      var gonerPlayerItem = currentSuppSheet.Items.Find(x => x.PlayerID == gonerPlayerId);
      if (gonerPlayerItem != null)
      {
        gonerPlayerInSheet = true;
      }

      // if both players are in the sheet, just remove the goner
      if (gonerPlayerInSheet && keeperPlayerInSheet)
      {
        SupplementalSheet.RemoveSupplementalSheetItem(currentSuppSheet.SupplementalSheetID, gonerPlayerId);
      }
      // if only the goner is in the sheet, remove it and insert the keepers in the goner's spot
      if (gonerPlayerInSheet && !keeperPlayerInSheet)
      {
        SupplementalSheet.RemoveSupplementalSheetItem(currentSuppSheet.SupplementalSheetID, gonerPlayerId);

        var mappedProperties = new Dictionary<string, object>();

        SupplementalSheetItem.InsertSupplementalSheetItem(new SupplementalSheetItem(currentSuppSheet.SupplementalSheetID, keeperPlayerId,
          gonerPlayerItem.Seqno, string.Empty, mappedProperties));
      }
    }
  }

  private void AdjustArchivedCheatSheet(int keeperPlayerId, int gonerPlayerId)
  {

    var targetPlayerPosition = Player.GetPlayer(keeperPlayerId).PositionCode;

    var fooSportSeasons = SportSeason.GetSportSeasons(FOO.FOOString);

    foreach (var currentSeason in fooSportSeasons)
    {
      var allArchivedSheets = ArchivedCheatSheet.GetArchivedCheatSheets(FOO.FOOString, currentSeason.SeasonCode, targetPlayerPosition);

      foreach (var currentArchivedSheet in allArchivedSheets)
      {
        var keeperPlayerInSheet = false;
        var keeperPlayerItem = currentArchivedSheet.Items.Find(x => x.PlayerID == keeperPlayerId);
        if (keeperPlayerItem != null)
        {
          keeperPlayerInSheet = true;
        }

        var gonerPlayerInSheet = false;
        var gonerPlayerItem = currentArchivedSheet.Items.Find(x => x.PlayerID == gonerPlayerId);
        if (gonerPlayerItem != null)
        {
          gonerPlayerInSheet = true;
        }

        // if both players are in the sheet, just remove the goner
        if (gonerPlayerInSheet && keeperPlayerInSheet)
        {
          ArchivedCheatSheet.RemoveArchivedCheatSheetItem(currentArchivedSheet.ArchivedCheatSheetID, gonerPlayerId);
        }
        // if only the goner is in the sheet, remove it and insert the keepers in the goner's spot
        if (gonerPlayerInSheet && !keeperPlayerInSheet)
        {
          ArchivedCheatSheet.RemoveArchivedCheatSheetItem(currentArchivedSheet.ArchivedCheatSheetID, gonerPlayerId);

          var mappedProperties = new Dictionary<string, object>
          {
            {CSIProperty.Sleeper.ToString(), false},
            {CSIProperty.Bust.ToString(), false},
            {CSIProperty.Injured.ToString(), false},
          };

          ArchivedCheatSheetItem.InsertArchivedCheatSheetItem(new ArchivedCheatSheetItem(currentArchivedSheet.ArchivedCheatSheetID,
                                                keeperPlayerId, gonerPlayerItem.Seqno, string.Empty, mappedProperties));
        }
      }
    }


  }

}