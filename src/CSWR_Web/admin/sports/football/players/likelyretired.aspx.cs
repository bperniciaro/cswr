using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.Admin.Sheets
{

  public partial class LikelyRetired : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        LoadLikelyRetiredPlayers();
      }
    }

    private void LoadLikelyRetiredPlayers()
    {
      SportSeason currentSportSeason = SportSeason.GetCurrentSportSeason(FOO.FOOString); 

      List<Player> allUnRetiredPlayers = Player.GetPlayers(FOO.FOOString, currentSportSeason.SeasonCode, false);

      List<Player> allStatSortedQBs = Player.GetPlayers(FOO.FOOString, currentSportSeason.LastSeasonCode,
                                             FOOPositionsOffense.QB.ToString(), false, "TFP", "DESC");
      List<Player> allStatSortedRBs = Player.GetPlayers(FOO.FOOString, currentSportSeason.LastSeasonCode,
                                             FOOPositionsOffense.RB.ToString(), false, "TFP", "DESC");
      List<Player> allStatSortedWRs = Player.GetPlayers(FOO.FOOString, currentSportSeason.LastSeasonCode,
                                             FOOPositionsOffense.WR.ToString(), false, "TFP", "DESC");
      List<Player> allStatSortedTEs = Player.GetPlayers(FOO.FOOString, currentSportSeason.LastSeasonCode,
                                             FOOPositionsOffense.TE.ToString(), false, "TFP", "DESC");
      List<Player> allStatSortedKs = Player.GetPlayers(FOO.FOOString, currentSportSeason.LastSeasonCode,
                                             FOOPositionsOffense.K.ToString(), false, "TFP", "DESC");
      List<Player> allStatSortedDFs = Player.GetPlayers(FOO.FOOString, currentSportSeason.LastSeasonCode,
                                             FOOPositionsOffense.DF.ToString(), false, "TFP", "DESC");

      List<Player> allPlayersWithStats = new List<Player>();
      allPlayersWithStats.AddRange(allStatSortedQBs);
      allPlayersWithStats.AddRange(allStatSortedRBs);
      allPlayersWithStats.AddRange(allStatSortedWRs);
      allPlayersWithStats.AddRange(allStatSortedTEs);
      allPlayersWithStats.AddRange(allStatSortedKs);
      allPlayersWithStats.AddRange(allStatSortedDFs);


      // determine players (including rookies) who are not retired, but who did not record a stat in the specified year.  these players
      // will not show-up when querying for things like TFP, but we still need to add them to the available player pool
      List<Player> noStatsPlayers = new List<Player>();
      foreach (Player currentPlayer in allUnRetiredPlayers)
      {
        Player playerWithStat = allPlayersWithStats.Find((delegate(Player targetPlayer) { return (targetPlayer.PlayerID == currentPlayer.PlayerID); }));
        if (playerWithStat == null)
        {
          noStatsPlayers.Add(currentPlayer);
        }
      }

      gvLikelyRetired.DataSource = noStatsPlayers;
      gvLikelyRetired.DataBind();

    }


    protected void ibRetire_Command(object sender, CommandEventArgs e)
    {
      Player targetPlayer = Player.GetPlayer(Convert.ToInt32(e.CommandArgument));
      targetPlayer.Retired = true;
      targetPlayer.Update();

      LoadLikelyRetiredPlayers();
    }


    protected void gvLikelyRetired_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        // get references to the appropriate controls
        Player boundPlayer = (Player)e.Row.DataItem;

        HyperLink hlPlayerProfileLink = (HyperLink)e.Row.FindControl("hlPlayerProfileLink");

        // player profile string
        string searchString = "https://www.google.com/search?q=";
        searchString += "site:nfl.com+\"" + boundPlayer.FirstName + "+" + boundPlayer.LastName + "\"+profile";
        searchString += "&btnI";
        hlPlayerProfileLink.NavigateUrl = searchString;

      }
    }
}
}