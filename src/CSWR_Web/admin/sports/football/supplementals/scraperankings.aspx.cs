using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class ScrapeRankings : BasePage
  {

    private int unFoundPlayerCount = 0;
    private int wrongTeamCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        panSourcesPanel.Visible = true;
        // load all sport seasons 
        List<SportSeason> allStatSportSeasons = SportSeason.GetSportSeasons("FOO");
        ddlSeasons.DataSource = allStatSportSeasons;
        ddlSeasons.DataBind();
      }
      panSuccessPanel.Visible = false;
    }

    /// <summary>
    /// This is the event that will determine from which website the rankings are harvested.  It reads the
    /// source from the dropdownlist and calls the appropriate method to process the html
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void butGrabRankings_Click(object sender, EventArgs e)
    {
      panRankingsPanel.Visible = true;

      // Load the Title
      SupplementalSource currentSource = SupplementalSource.GetSupplementalSource(ddlSupplementalSources.SelectedValue);
      litRankingTitle.Text = currentSource.Name + " " + ddlPositions.SelectedValue;
      
      // Process the appropriate page  
      switch (ddlSupplementalSources.SelectedValue)
      {
        case "NFL":
          ProcessNFLPlayers();
          break;
        case "CBS":
          ProcessSportslinePlayers();
          break;
      }
      // build a link directly to the supplemental source for easy reference, if needed
      SupplementalSheet currentSheet = SupplementalSheet.GetSupplementalSheet(ddlSeasons.SelectedValue, currentSource.SupplementalSourceID, "FOO", ddlPositions.SelectedValue);
      hlViewSupplementalSheet.Visible = true;
      hlViewSupplementalSheet.NavigateUrl = currentSheet.Url;
    }


    /// <summary>
    /// The main method that will kick off and bind the rankings of Sporstline players
    /// </summary>
    /// <returns></returns>
    private bool ProcessSportslinePlayers()
    {
      // get a table of players based on the position being processed
      string rankingTable = GrabSportslineRankingsTable();
      // get a collection of rows containing the relevant data
      List<string> rankingRows = GrabSportslineRankingRows(rankingTable);

      // build a collection of PlayerInfo objects based on html table rows
      List<PlayerInfo> rankedPlayers = new List<PlayerInfo>();
      if (ddlPositions.SelectedValue != "DF")
      {
        rankedPlayers = LoadSportslinePlayerInfo(rankingRows);
      }
      else
      {
        rankedPlayers = LoadSportslineDefenseInfo(rankingRows);
      }

      repRankings.DataSource = rankedPlayers;
      repRankings.DataBind();

      if ( (unFoundPlayerCount == 0) && (wrongTeamCount == 0)  )
      {
        
        CommitRankings();
      }

      return true;
    }

    private List<PlayerInfo> LoadSportslinePlayerInfo(List<string> rankingRows)
    {
      var playersInfo = new List<PlayerInfo>();

      foreach (var rankingRow in rankingRows)
      {
        int remainingRowLength = 0;
        int tempStartPosition, tempEndPosition;
        int rankIndex = 1;


        PlayerInfo playerInfo = new PlayerInfo();
        string tempRankingRow = rankingRow.Replace("\t", String.Empty).Replace("\n", String.Empty);

        /************************/
        // get the ranking value
        /************************/
        var startPosition = tempRankingRow.IndexOf("<div class=\"rank\"", StringComparison.Ordinal) + 15;
        var endPosition = tempRankingRow.IndexOf("<div class=\"player\">", startPosition, StringComparison.Ordinal) - 5;
        var rankingCell = tempRankingRow.Substring(startPosition, endPosition - startPosition);

        //var playerRankString = rankingCell.Substring(tempStartPosition, tempEndPosition - tempStartPosition);
        var playerRank = Regex.Match(rankingCell, @"\d+").Value;

        playerInfo.Rank = int.Parse(playerRank);

        /*****************************/
        // get the name 
        /*****************************/
        remainingRowLength = (tempRankingRow.Length) - (endPosition + 1);
        tempRankingRow = tempRankingRow.Substring(endPosition, remainingRowLength);
        startPosition = tempRankingRow.IndexOf("<span class=\"player-name\">", StringComparison.Ordinal) + 26;
        endPosition = tempRankingRow.IndexOf("</span>", startPosition, StringComparison.Ordinal);
        var playerName = tempRankingRow.Substring(startPosition, endPosition - startPosition);
        
        // process the name
        var nameArray = playerName.Split(' ');
        var firstInitial = nameArray[0][0];
        var lastName = nameArray[1];
        playerInfo.FirstInitial = firstInitial.ToString();
        playerInfo.LastName = lastName.Trim();

        /*****************************/
        // get the name 
        /*****************************/
        remainingRowLength = (tempRankingRow.Length) - (endPosition + 1);
        tempRankingRow = tempRankingRow.Substring(endPosition, remainingRowLength);
        startPosition = tempRankingRow.IndexOf("<span class=\"team", StringComparison.Ordinal) + 29;
        endPosition = tempRankingRow.IndexOf("</span>", startPosition, StringComparison.Ordinal) - 1;
        var cityAbbreviationAndPrice = tempRankingRow.Substring(startPosition, endPosition - startPosition).Trim();
        var justCityAbbreviation = cityAbbreviationAndPrice.Split('$')[0].Trim();

        playerInfo.TeamAbbreviation = justCityAbbreviation;

        //add the first player to the list
        if (!(((ddlPositions.SelectedValue == FOOPositionsOffense.RB.ToString()) && 
            (playerInfo.FirstInitial == "D" && playerInfo.LastName == "McCluster"))))
        {
          //playerInfo.Rank = rankIndex++;
          playersInfo.Add(playerInfo);
        }



      }

      // here we sort the players
      playersInfo.OrderBy(x => x.Rank);

      return playersInfo;
    }



    private List<PlayerInfo> LoadSportslineDefenseInfo(List<string> rankingRows)
    {
      var playersInfo = new List<PlayerInfo>();
      //var rankingCell = String.Empty;
      //var remainingRowLength = 0;

      foreach (string rankingRow in rankingRows)
      {
        int remainingRowLength = 0;
        int tempStartPosition, tempEndPosition;
        int rankIndex = 1;


        PlayerInfo playerInfo = new PlayerInfo();
        string tempRankingRow = rankingRow.Replace("\t", String.Empty).Replace("\n", String.Empty);

        /************************/
        // get the ranking value
        /************************/
        var startPosition = tempRankingRow.IndexOf("<div class=\"rank\"", StringComparison.Ordinal) + 15;
        var endPosition = tempRankingRow.IndexOf("<div class=\"player\">", startPosition, StringComparison.Ordinal) - 5;
        var rankingCell = tempRankingRow.Substring(startPosition, endPosition - startPosition);

        //var playerRankString = rankingCell.Substring(tempStartPosition, tempEndPosition - tempStartPosition);
        var playerRank = Regex.Match(rankingCell, @"\d+").Value;

        playerInfo.Rank = int.Parse(playerRank);

        /*****************************/
        // get the name 
        /*****************************/
        remainingRowLength = (tempRankingRow.Length) - (endPosition + 1);
        tempRankingRow = tempRankingRow.Substring(endPosition, remainingRowLength);
        startPosition = tempRankingRow.IndexOf("<span class=\"player-name\">", StringComparison.Ordinal) + 26;
        endPosition = tempRankingRow.IndexOf("</span>", startPosition, StringComparison.Ordinal);
        var teamMascot = tempRankingRow.Substring(startPosition, endPosition - startPosition);

        // process the name
        //var nameArray = playerName.Split(' ');
        //var firstInitial = nameArray[0][0];
        //var lastName = nameArray[1];
        //playerInfo.FirstInitial = firstInitial.ToString();
        //playerInfo.LastName = lastName.Trim();

        /*****************************/
        // get the name 
        /*****************************/
        //remainingRowLength = (tempRankingRow.Length) - (endPosition + 1);
        //tempRankingRow = tempRankingRow.Substring(endPosition, remainingRowLength);
        //startPosition = tempRankingRow.IndexOf("<span class=\"team", StringComparison.Ordinal) + 29;
        //endPosition = tempRankingRow.IndexOf("</span>", startPosition, StringComparison.Ordinal) - 1;
        //var cityAbbreviation = tempRankingRow.Substring(startPosition, endPosition - startPosition).Trim();

        playerInfo.TeamAbbreviation = Team.GetTeam(FOO.FOOString, teamMascot).Abbreviation;

        //Team team = Team.GetTeam("FOO", mascot);
        //playerInfo.TeamAbbreviation = nameSpan;
        playersInfo.Add(playerInfo);

      }

      // here we sort the players
      playersInfo.OrderBy(x => x.Rank);

      return playersInfo;
    }


    private List<string> GrabSportslineRankingRows(string rankingContainer)
    {
      var rowsToProcess = true;
      var rankingSections = new List<string>();

      do
      {
        var startPosition = rankingContainer.IndexOf("<div class=\"player-row", StringComparison.Ordinal);
        if (startPosition > -1)
        {
          var endPosition = rankingContainer.IndexOf("<div class=\"player-stats\">", startPosition, StringComparison.Ordinal);
          if (endPosition > -1)
          {
            endPosition += 32;
            var playerContainer = rankingContainer.Substring(startPosition, endPosition - startPosition);
            var remainingTableLength = (rankingContainer.Length) - (endPosition);
            rankingContainer = rankingContainer.Substring(endPosition, remainingTableLength);
            rankingSections.Add(playerContainer);
          }
          else
          {
            rowsToProcess = false;
          }
        }
        else
        {
          rowsToProcess = false;
        }
      }
      while (rowsToProcess == true);

      return rankingSections;


    }


    private string GrabSportslineRankingsTable()
    {
      var position = (ddlPositions.SelectedValue != "DF") ? ddlPositions.SelectedValue : "DST";
      var targetUrl = "https://www.cbssports.com/fantasy/football/rankings/standard/" + position + "/yearly/";

      var myEncoding = new UTF8Encoding();
      var myClient = new WebClient();
      ServicePointManager.Expect100Continue = true;
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
      var pageString = myEncoding.GetString(myClient.DownloadData(targetUrl));

      // find quarterback div      
      var startPosition = pageString.IndexOf("<div class=\"player-wrapper", StringComparison.CurrentCulture) - 17;
      var endPosition = pageString.IndexOf("<a href=\"/writers/dave-richard", startPosition, StringComparison.Ordinal);
      var playerTable = pageString.Substring(startPosition, endPosition - startPosition);
      return playerTable;
    }



    /// <summary>
    /// Accessing the player rankings on NFL.com and binds them to a repeater for processing.
    /// </summary>
    private void ProcessNFLPlayers()
    {
      // get the target table
      string rankingTable = GrabNFLRankingsTable();
      // strip out the rows
      List<string> rankingRows = GrabNFLRankingRows(rankingTable);
      // remove the rows that we don't need
      rankingRows.RemoveAt(0);
      rankingRows.RemoveAt(0);
      // pull out the important data
      //List<PlayerInfo> rankedPlayers = new List<PlayerInfo>();
      if (ddlPositions.SelectedValue != "DF")
      {
        repRankings.DataSource = LoadNFLPlayerInfo(rankingRows);
      }
      else
      {
        repRankings.DataSource = LoadNFLDefenseInfo(rankingRows);
      }
      repRankings.DataBind();
    }

    /// <summary>
    /// Scrapes a rankings page from NFL.com and builds a collection of PlayerInfo objects
    /// based on these rankings.
    /// </summary>
    /// <param name="rankingRows"></param>
    /// <returns>A collection PlayerInfo objects based on the rankings from NFL.com</returns>
    private List<PlayerInfo> LoadNFLPlayerInfo(List<string> rankingRows)
    {
      List<PlayerInfo> playersInfo = new List<PlayerInfo>();
      string rankingCell, nameCell, teamCell, teamAbbreviation, playerName, FirstInitial, lastName = String.Empty;
      string[] nameArray;
      int remainingRowLength = 0;
      int startPosition, tempStartPosition, endPosition, tempEndPosition;



      foreach (string rankingRow in rankingRows)
      {
        PlayerInfo playerInfo = new PlayerInfo();
        string tempRankingRow = rankingRow;

        /************************/
        // get the ranking cell value
        /************************/
        startPosition = endPosition = 0;
        startPosition = tempRankingRow.IndexOf("<td");
        endPosition = tempRankingRow.IndexOf("</td>", startPosition) + 5;
        rankingCell = tempRankingRow.Substring(startPosition, endPosition - startPosition);
        // pull the ranking data out
        tempStartPosition = rankingCell.IndexOf("<td") + 4;
        tempEndPosition = rankingCell.IndexOf(".", tempStartPosition);
        playerInfo.Rank = int.Parse(rankingCell.Substring(tempStartPosition, tempEndPosition - tempStartPosition));

        /*********************/
        // get the name cell value
        /*********************/
        remainingRowLength = (tempRankingRow.Length) - (endPosition + 1);
        tempRankingRow = tempRankingRow.Substring(endPosition + 1, remainingRowLength);
        startPosition = tempRankingRow.IndexOf("<td");
        endPosition = tempRankingRow.IndexOf("</td>", startPosition) + 5;
        nameCell = tempRankingRow.Substring(startPosition, endPosition - startPosition);
        //pull out the name 
        tempStartPosition = nameCell.IndexOf("\">") + 2;
        tempEndPosition = nameCell.IndexOf("</a>", tempStartPosition);
        playerName = nameCell.Substring(tempStartPosition, tempEndPosition - tempStartPosition);
        nameArray = playerName.Split(' ');
        FirstInitial = nameArray[0];
        lastName = nameArray[1];
        playerInfo.FirstInitial = FirstInitial;
        playerInfo.LastName = lastName;


        /*********************/
        // get the team cell value
        /*********************/
        remainingRowLength = (tempRankingRow.Length) - (endPosition + 1);
        tempRankingRow = tempRankingRow.Substring(endPosition + 1, remainingRowLength);
        startPosition = tempRankingRow.IndexOf("<td");
        endPosition = tempRankingRow.IndexOf("</td>", startPosition) + 5;
        teamCell = tempRankingRow.Substring(startPosition, endPosition - startPosition);
        // pull the team data out
        tempStartPosition = teamCell.IndexOf("<td") + 4;
        tempEndPosition = teamCell.IndexOf("</td>", tempStartPosition);
        teamAbbreviation = teamCell.Substring(tempStartPosition, tempEndPosition - tempStartPosition);
        playerInfo.TeamAbbreviation = teamAbbreviation;

        playersInfo.Add(playerInfo);
      }
      return playersInfo;
    }



    /// <summary>
    /// Scrapes a rankings page from NFL.com and builds a collection of PlayerInfo objects
    /// based on these rankings.
    /// </summary>
    /// <param name="rankingRows"></param>
    /// <returns>A collection PlayerInfo objects based on the rankings from NFL.com</returns>
    private List<PlayerInfo> LoadNFLDefenseInfo(List<string> rankingRows)
    {
      List<PlayerInfo> playersInfo = new List<PlayerInfo>();
      string rankingCell, teamCell, remainingTeamCell, lastName = String.Empty;
      int remainingRowLength = 0;
      int startPosition, tempStartPosition, endPosition, tempEndPosition;



      foreach (string rankingRow in rankingRows)
      {
        PlayerInfo playerInfo = new PlayerInfo();
        string tempRankingRow = rankingRow;

        /************************/
        // get the ranking cell value
        /************************/
        startPosition = endPosition = 0;
        startPosition = tempRankingRow.IndexOf("<td");
        endPosition = tempRankingRow.IndexOf("</td>", startPosition) + 5;
        rankingCell = tempRankingRow.Substring(startPosition, endPosition - startPosition);
        // pull the ranking data out
        tempStartPosition = rankingCell.IndexOf("<td") + 4;
        tempEndPosition = rankingCell.IndexOf(".", tempStartPosition);
        playerInfo.Rank = int.Parse(rankingCell.Substring(tempStartPosition, tempEndPosition - tempStartPosition));

        /*********************/
        // get the name cell value
        /*********************/
        remainingRowLength = (tempRankingRow.Length) - (endPosition + 1);
        tempRankingRow = tempRankingRow.Substring(endPosition + 1, remainingRowLength);
        startPosition = tempRankingRow.IndexOf("<td");
        endPosition = tempRankingRow.IndexOf("</td>", startPosition) + 5;
        teamCell = tempRankingRow.Substring(startPosition, endPosition - startPosition);
        //pull out the empty link 
        tempStartPosition = teamCell.IndexOf("<a");
        tempEndPosition = teamCell.IndexOf("</a>", tempStartPosition) + 4;
        //emptyLink = teamCell.Substring(tempStartPosition, tempEndPosition - tempStartPosition);
        //pull out the correct link
        remainingTeamCell = teamCell.Substring(tempStartPosition, teamCell.Length - tempStartPosition);
        tempStartPosition = remainingTeamCell.IndexOf("\">") + 2;
        tempEndPosition = remainingTeamCell.IndexOf("</a>", tempStartPosition);
        string mascot = remainingTeamCell.Substring(tempStartPosition, tempEndPosition - tempStartPosition);

        Team team = Team.GetTeam("FOO", mascot);
        playerInfo.TeamAbbreviation = team.Abbreviation;
        playersInfo.Add(playerInfo);

      }
      return playersInfo;
    }



    /// <summary>
    /// Receives a string representing an html table and builds a collection of table rows.
    /// </summary>
    /// <param name="rankingTable"></param>
    /// <returns>A collection of strings representing table rows.</returns>
    private List<string> GrabNFLRankingRows(string rankingTable)
    {
      bool rowsToProcess = true;
      int startPosition, endPosition = 0;
      int remainingTableLength = 0;
      List<string> rankingRows = new List<string>();

      do
      {
        string tableRow = String.Empty;
        startPosition = rankingTable.IndexOf("<tr");
        if (startPosition > -1)
        {
          endPosition = rankingTable.IndexOf("</tr>", startPosition);
          if (endPosition > -1)
          {
            endPosition += 5;
            tableRow = rankingTable.Substring(startPosition, endPosition - startPosition);
            remainingTableLength = (rankingTable.Length) - (endPosition + 1);
            rankingTable = rankingTable.Substring(endPosition + 1, remainingTableLength);
            rankingRows.Add(tableRow);
          }
        }
        else
        {
          rowsToProcess = false;
        }
      }
      while (rowsToProcess == true);

      return rankingRows;
    }

    /// <summary>
    /// Parses a page of rankings from NFL.com and builds a string which represents the html table scoring these rankings.
    /// </summary>
    /// <returns>A string containing the html table rankings.</returns>
    private string GrabNFLRankingsTable()
    {

      WebClient myClient = new WebClient();
      UTF8Encoding myUTF8Obj = new UTF8Encoding();
      string targetUrl = String.Empty;

      if (ddlPositions.SelectedValue != "DF")
      {
        targetUrl = "https://www.nfl.com/fantasy/rankings-" + ddlSeasons.SelectedValue + "/" + ddlPositions.SelectedValue.ToLower();
      }
      else
      {
        targetUrl = "https://www.nfl.com/fantasy/rankings-" + ddlSeasons.SelectedValue + "/dst";
      }

      string myString = myUTF8Obj.GetString(myClient.DownloadData(targetUrl));
      // isolate the main table holding the player rankings
      string rankingTable = String.Empty;
      int startPosition = 0;
      int endPosition = 0;
      startPosition = myString.IndexOf("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"data-table1\" id=\"rankings\">");
      if (startPosition > -1)
      {
        endPosition = myString.IndexOf("</table>", startPosition);
        if (endPosition > -1)
        {
          endPosition += 8;
          rankingTable = myString.Substring(startPosition, endPosition - startPosition);
        }
      }

      return rankingTable;
    }



    /// <summary>
    /// This method builds the list of dropdownboxes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void repRankings_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
      {
        // get a reference to the bound item
        PlayerInfo boundPlayer = (PlayerInfo)e.Item.DataItem;
        // get a reference to controls 
        DropDownList ddlPlayers = (DropDownList)e.Item.FindControl("ddlPlayers");
        PlayerInfo rankedPlayer = (PlayerInfo)e.Item.DataItem;
        Label labRank = (Label)e.Item.FindControl("labRank");
        Label labSheetPlayer = (Label)e.Item.FindControl("labSheetPlayer");
        Label labWrongTeam = (Label)e.Item.FindControl("labWrongTeam");
        Literal litFoundPlayer = (Literal)e.Item.FindControl("litFoundPlayer");
        HiddenField hfFoundPlayer = (HiddenField)e.Item.FindControl("hfFoundPlayer");
        Label labRookieDesignation = (Label)e.Item.FindControl("labRookieDesignation");


        List<Player> currentPlayers = Player.GetPlayersBySportSeasonPositionCodes("FOO", SportSeason.GetCurrentSportStatSeason("FOO").SeasonCode, ddlPositions.SelectedValue, false, true);
        currentPlayers.OrderBy(x => x.FullNameLastFirst);

        // build the rank
        labRank.Text = rankedPlayer.Rank.ToString();

        // find possible matches
        List<Player> possibleMatches = new List<Player>();
        int teamPlayerID = 0;
        bool matchFound = false;
        if (ddlPositions.SelectedValue == "DF")
        {
          //Player defensivePlayer = Player.GetDefensivePlayer(
          possibleMatches = Player.GetDefensiveTeamPlayers();
          // find the appropriate team
          foreach (Player dbTeam in possibleMatches)
          {
            Team playerTeam = Team.GetTeam(dbTeam.TeamCode);
            if (rankedPlayer.TeamAbbreviation.Trim() == playerTeam.Abbreviation)
            {
              teamPlayerID = dbTeam.PlayerID;
              matchFound = true;
              litFoundPlayer.Text = playerTeam.Abbreviation + " - <em>found</em>";
              ddlPlayers.Visible = false;
              hfFoundPlayer.Value = dbTeam.PlayerID.ToString();
              break;
            }
          }
          //ddlPlayers.SelectedValue = teamPlayerID.ToString();// Player.GetDefensivePlayer(rankedPlayer.TeamAbbreviation + "N").PlayerID.ToString();
          if (!matchFound)
          {
            // bind the dropdown to the correct fields
            ddlPlayers.DataSource = currentPlayers;
            ddlPlayers.DataTextField = "FullName";
            ddlPlayers.DataValueField = "PlayerID";
            ddlPlayers.DataBind();
           
            ddlPlayers.BackColor = Color.Pink;
            labSheetPlayer.Text = rankedPlayer.FirstInitial + " " + rankedPlayer.LastName;
          }
        }
        else
        {
          string relevantSportSeason = ddlSeasons.SelectedValue;
          var firstInitial = rankedPlayer.FirstInitial[0];
          possibleMatches = Player.GetPlayers("FOO", relevantSportSeason, ddlPositions.SelectedValue, firstInitial, rankedPlayer.LastName, false);
          // player found
          if (possibleMatches.Count == 1)
          {
            //ddlPlayers.SelectedValue = possibleMatches[0].PlayerID.ToString();
            litFoundPlayer.Text = possibleMatches[0].FullName + " - <em>found</em>";
            ddlPlayers.Visible = false;
            hfFoundPlayer.Value = possibleMatches[0].PlayerID.ToString();

            if (boundPlayer.TeamAbbreviation != possibleMatches[0].Team.Abbreviation)
            {
              labWrongTeam.Text = "old team: " + possibleMatches[0].Team.Abbreviation + " newteam: " + boundPlayer.TeamAbbreviation;
              wrongTeamCount++;
            }

            labRookieDesignation.Visible = (possibleMatches[0].YearsExperience == 0);
          }
          else
          {
            // load the sheet player name
            labSheetPlayer.Text = rankedPlayer.FirstInitial + " " + rankedPlayer.LastName + " (" + boundPlayer.TeamAbbreviation + ") - "; 

            // bind the dropdown to the correct fields
            ddlPlayers.DataSource = possibleMatches;
            ddlPlayers.DataTextField = "FullNameLastFirst";
            ddlPlayers.DataValueField = "PlayerID";
            ddlPlayers.DataBind();
            ddlPlayers.BackColor = Color.Pink;

            var bestGuessIndex = 0;
            // add the team name to make referencing easier
            for (int i = 1; i <= possibleMatches.Count; i++)
            {
              var teamAbbreviation = Team.GetTeam(possibleMatches[i - 1].PlayerID).Abbreviation;
              ddlPlayers.Items[i].Text = ddlPlayers.Items[i].Text + " - " + teamAbbreviation;
              if(boundPlayer.TeamAbbreviation == teamAbbreviation)
              {
                bestGuessIndex = i;
              }
            }

            ddlPlayers.SelectedIndex = bestGuessIndex;
            unFoundPlayerCount++;
          }
        }
        
      }
    }


    /// <summary>
    /// This event is called after the user clicks the button indicating that they're satisfied with the polled ranking selections.
    /// Once this button is clicked the new rankings will be saved to the database
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void butCommitOrder_Click(object sender, EventArgs e)
    {
      CommitRankings();
      unFoundPlayerCount = 0;
    }

    private void CommitRankings()
    {
      // hide and show the proper panels
      panRankingsPanel.Visible = false;
      panSourcesPanel.Visible = true;
      panSuccessPanel.Visible = true;

      SupplementalSheet relevantSheet = SupplementalSheet.GetSupplementalSheet(ddlSeasons.SelectedValue, SupplementalSource.GetSupplementalSource(ddlSupplementalSources.SelectedValue).SupplementalSourceID, "FOO", ddlPositions.SelectedValue);
      int currentSupplementalSheetID = relevantSheet.SupplementalSheetID;
      // clear all previous items
      bool clearResult = SupplementalSheet.RemoveAllSupplementalSheetItems(currentSupplementalSheetID);
      // timestamp the sheet so we know it was updated, then update it
      relevantSheet.LastUpdated = DateTime.Now;
      relevantSheet.Update();
      int totalPlayers = repRankings.Controls.Count;
      for (int i = 0; i < totalPlayers; i++)
      {
        DropDownList ddlPlayers = (DropDownList)repRankings.Controls[i].FindControl("ddlPlayers");
        HiddenField hfFoundPlayer = (HiddenField)repRankings.Controls[i].FindControl("hfFoundPlayer");

        if (hfFoundPlayer != null)
        {
          if (hfFoundPlayer.Value != "")
          {
            SupplementalSheet.AddSupplementalSheetItem(currentSupplementalSheetID, int.Parse(hfFoundPlayer.Value));
          }
          else
          {
            SupplementalSheet.AddSupplementalSheetItem(currentSupplementalSheetID, int.Parse(ddlPlayers.SelectedValue));
          }
        }
      }

      labResult.Text = "You have successfully ported the " + ddlPositions.SelectedItem.Text + "'s" + " from " + ddlSupplementalSources.SelectedItem.Text + ".";
    }

    /// <summary>
    /// Select the current season by default
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlSeasons_DataBound(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        ddlSeasons.SelectedValue = SportSeason.GetCurrentSportSeason("FOO").SeasonCode;
      }
    }


    /// <summary>
    /// A local class used to store some basic information about a player.
    /// </summary>
    private class PlayerInfo
    {

      //public static Comparison<PlayerInfo> RankComparison = delegate(PlayerInfo pI1, PlayerInfo pI2) { return pI1.Rank.CompareTo(pI2.Rank); };

      public PlayerInfo() { }

      public PlayerInfo(string firstInitial, string lastName, int rank, string TeamAbbreviation)
      {
        this.FirstInitial = firstInitial;
        this.LastName = lastName;
        this.Rank = rank;
        this.TeamAbbreviation = TeamAbbreviation;
      }

      private string _lastName = String.Empty;
      public string LastName
      {
        get { return _lastName; }
        set { _lastName = value; }
      }

      public string FirstInitial {get;set;}

      private int _rank = 0;
      public int Rank
      {
        get { return _rank; }
        set { _rank = value; }
      }

      private string _teamAbbreviation = string.Empty;
      public string TeamAbbreviation
      {
        get { return _teamAbbreviation; }
        set
        {
          //_teamAbbreviation = (value != "LAR") ? value : "LA";
          _teamAbbreviation = value;
        }
      }

    }
  }


}