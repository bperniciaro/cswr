using System;
using System.Linq;
using System.Text;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class FOOSheetItemTemplate : System.Web.UI.UserControl
  {

    public SheetType SheetType
    {
      get
      {
        return (ViewState["SheetType"] == null) ? SheetType.CheatSheet : (SheetType)ViewState["SheetType"];
      }
      set
      {
        ViewState["SheetType"] = value;
      }
    }


    private Player ItemPlayer { get; set; }
    private bool _pprSheet = false;

    private string _cswrRank = String.Empty;
    private string _cbsRank = String.Empty;
    private string _adp = String.Empty;
    private string _statusSuppInfo = String.Empty;
    private string _statusSuppInfoTitle = String.Empty;

    private SupplementalSheetItem _suppSheetItem = null;
    public SupplementalSheetItem SupplementalSheetItem
    {
      set
      {
        // Load property values
        _suppSheetItem = value;
        this.ItemPlayer = _suppSheetItem.Player;
        // build the control contents
        BuildControlContent();
      }
      get
      {
        return _suppSheetItem;
      }
    }


    private CheatSheetItem _cheatSheetItem = null;
    public CheatSheetItem CheatSheetItem
    {
      set
      {
        // Load property values
        _cheatSheetItem = value;
        this.ItemPlayer = _cheatSheetItem.Player;

        // build the control contents
        BuildControlContent();
      }
      get
      {
        return _cheatSheetItem;
      }
    }


    void BuildControlContent()
    {
      switch (this.SheetType)
      {
        case SheetType.CheatSheet:
          hfItemProperties.Value = this.CheatSheetItem.CheatSheetID.ToString() + "-" + this.CheatSheetItem.PlayerID.ToString();
          BuildCheatSheetContent();
          break;
        case SheetType.SuppSheet:
          hfItemProperties.Value = this.SupplementalSheetItem.SupplementalSheetID.ToString() + "-" + this.SupplementalSheetItem.PlayerID.ToString();
          BuildSuppSheetContent();
          break;
      }
    }

    /// <summary>
    /// If this template is stored in a cheat sheet, we call these methods to load the template
    /// </summary>
    private void BuildCheatSheetContent()
    {
      CheatSheet targetSheet = CheatSheet.GetCheatSheet(this.CheatSheetItem.CheatSheetID);

      // Configure the sleeper/bust/injured tags
      BuildCheatSheetTags();

      // Determine whether or not to show supplemental rankings
      DetermineSupplementalVisibility();

      // Load the players-specific information
      BuildPlayerTeamInfo(targetSheet.SeasonCode, targetSheet.Positions.Count);

      // Configure the feature icons
      BuildFeatureIcons(targetSheet.Positions.Count);

      // Build the status icon
      BuildStatus(this.CheatSheetItem.Player);

      // Build the stats display
      if (targetSheet.Positions.Count == 1)
      {
        BuildStats(targetSheet.StatsSeasonCode);
      }
      else
      {
        panValidStatsContainer.Visible = false;
        panNoStatsContainer.Visible = true;
      }

      // Popuplate Hidden Fields necessary for popups
      PopulateHiddenFields();

      LoadCheatSheetNoteData();
    }


    private void PopulateHiddenFields()
    {
      switch (this.SheetType)
      {
        case SheetType.CheatSheet:
          //CheatSheet targetCheatSheet = CheatSheet.GetCheatSheet(this.CheatSheetItem.CheatSheetID);
          hfItemProperties.Value = this.CheatSheetItem.PlayerID.ToString();
          hfItemProperties.Value += "_" + this.CheatSheetItem.Player.FullName;
          hfItemProperties.Value += "_" + _cswrRank + "_" + _cbsRank + "_" + _adp;
          hfItemProperties.Value += "_" + this.CheatSheetItem.Player.Team.Mascot;
          hfItemProperties.Value += "_" + SheetType.CheatSheet.ToString().ToLower();
          hfItemProperties.Value += "_" + _statusSuppInfo;
          hfItemProperties.Value += "_" + _statusSuppInfoTitle;
          break;

        case SheetType.SuppSheet:
          //SupplementalSheet targetSupplementalSheet = SupplementalSheet.GetSupplementalSheet(this.SupplementalSheetItem.SupplementalSheetID);
          hfItemProperties.Value = this.SupplementalSheetItem.PlayerID.ToString();
          hfItemProperties.Value += "_" + this.SupplementalSheetItem.Player.FullName;
          hfItemProperties.Value += "_" + _cswrRank + "_" + _cbsRank + "_" + _adp;
          hfItemProperties.Value += "_" + this.SupplementalSheetItem.Player.Team.Mascot;
          hfItemProperties.Value += "_" + SheetType.SuppSheet.ToString().ToLower();
          hfItemProperties.Value += "_" + _statusSuppInfo;
          hfItemProperties.Value += "_" + _statusSuppInfoTitle;
          break;
      }
    }


    private void DetermineSupplementalVisibility()
    {
      bool showSupps = true;

      // if we're showing a cheat sheet based on PPR, then we can't show supplemental rankings
      // since they are not PPR-specific.
      CheatSheet targetSheet = CheatSheet.GetCheatSheet(this.CheatSheetItem.CheatSheetID);
      if ((bool)targetSheet.MappedProperties[CSProperty.PPRLeague.ToString()])
      {
        _pprSheet = true;
        showSupps = false;
      }

      // if the sheet has more than one position, we hide the supplemental rankings
      if (targetSheet.Positions.Count > 1)
      {
        showSupps = false;
      }

      
      if (!showSupps)
      {
        panSuppRankingContainer.Visible = false;
        panPlayerContainer.CssClass = "playerContainer playerWithoutSupps";
      }
      else
      {
        // Load supplemental rankings, if necessary
        if (SportSetting.Football.ShowSupplementalRankings)
        {
          BuildCheatSheetSupplementalRankings();
          panPlayerContainer.CssClass = "playerContainer playerWithSupps";
        }
        else
        {
          panSuppRankingContainer.Visible = false;
          panPlayerContainer.CssClass = "playerContainer playerWithoutSupps";
        }
      }


    }

    private void BuildSuppSheetContent()
    {
      SupplementalSheet targetSheet = SupplementalSheet.GetSupplementalSheet(this.SupplementalSheetItem.SupplementalSheetID);

      // Configure the sleeper/bust tags
      BuildSuppSheetTags();

      BuildSuppSheetSupplementalRankings();
      panPlayerContainer.CssClass = "playerContainer playerWithSupps";

      // Load the players-specific information
      BuildPlayerTeamInfo(targetSheet.SeasonCode, 1);

      // Configure the feature icons
      BuildFeatureIcons(1);

      // Build the status icon
      BuildStatus(this.SupplementalSheetItem.Player);

      // Build the stats display
      BuildStats(targetSheet.StatsSeasonCode);

      // Popuplate Hidden Fields necessary for popups
      PopulateHiddenFields();

      LoadSuppSheetNoteData();
    }


    private void BuildCheatSheetTags()
    {
      // sleeper
      siSleeperTag.State = (bool)this.CheatSheetItem.MappedProperties[CSIProperty.Sleeper.ToString()];
      siSleeperTag.ServiceArgument = this.CheatSheetItem.CheatSheetID.ToString() + "-" + this.CheatSheetItem.PlayerID.ToString() + "-sleeper";
      // bust
      siBustTag.State = (bool)this.CheatSheetItem.MappedProperties[CSIProperty.Bust.ToString()];
      siBustTag.ServiceArgument = this.CheatSheetItem.CheatSheetID.ToString() + "-" + this.CheatSheetItem.PlayerID.ToString() + "-bust";
      // injured
      if (!this.CheatSheetItem.Player.IsDefensiveTeamPlayer)
      {
        siInjuredTag.State = (bool)this.CheatSheetItem.MappedProperties[CSIProperty.Injured.ToString()];
        siInjuredTag.ServiceArgument = this.CheatSheetItem.CheatSheetID.ToString() + "-" + this.CheatSheetItem.PlayerID.ToString() + "-injured";
      }
      else
      {
        siInjuredTag.Visible = false;
      }

    }


    private void BuildSuppSheetTags()
    {
      // sleeper
      siSleeperTag.State = (bool)this.SupplementalSheetItem.MappedProperties[SSIProperty.Sleeper.ToString()];
      siSleeperTag.ServiceArgument = this.SupplementalSheetItem.SupplementalSheetID.ToString() + "-" + this.SupplementalSheetItem.PlayerID.ToString() + "-sleeper";
      // bust
      siBustTag.State = (bool)this.SupplementalSheetItem.MappedProperties[SSIProperty.Bust.ToString()];
      siBustTag.ServiceArgument = this.SupplementalSheetItem.SupplementalSheetID.ToString() + "-" + this.SupplementalSheetItem.PlayerID.ToString() + "-bust";
      // injured
      siInjuredTag.Visible = false;
    }


    private void BuildCheatSheetSupplementalRankings()
    {
      SportSeason currentSeason = SportSeason.GetCurrentSportSeason("FOO");


      // Supplemental Source 1 Rankings
      SupplementalSource supplementalSource1 = SupplementalSource.GetSupplementalSource("CSWR");
      SupplementalSheet supplementalSheet1 = SupplementalSheet.GetSupplementalSheet(currentSeason.SeasonCode, supplementalSource1.SupplementalSourceID, this.CheatSheetItem.Player.SportCode, this.CheatSheetItem.Player.PositionCode);
      bool item1Found = false;
      if (supplementalSheet1 != null)
      {
        SupplementalSheetItem targetSuppSheetItem1 = SupplementalSheetItem.GetSupplementalSheetItem(supplementalSheet1.SupplementalSheetID, this.CheatSheetItem.PlayerID);
        if(targetSuppSheetItem1 != null)  
        {
          int supplementalRank1 = targetSuppSheetItem1.Seqno;
          if (supplementalRank1 != 0)
          {
            item1Found = true;
            // load the supplemental rankings into the UI
            labSuppRanking1.Text = supplementalRank1.ToString();
            // load the supplemental ranking into the hideen field used by QTip
            _cswrRank = supplementalRank1.ToString();

            if (supplementalRank1 < 10)
            {
              labSuppRanking1.CssClass = "suppData oneDigit";
            }
            else if ((supplementalRank1 >= 10) && (supplementalRank1 < 100))
            {
              labSuppRanking1.CssClass = "suppData twoDigits";
            }
          }
        }
      }
      if (!item1Found)
      {
        labSuppRanking1.Text = String.Empty;
      }


      // Supplemental Source 2 Rankings
      SupplementalSource supplementalSource2 = SupplementalSource.GetSupplementalSource("CBS");
      SupplementalSheet supplementalSheet2 = SupplementalSheet.GetSupplementalSheet(currentSeason.SeasonCode, supplementalSource2.SupplementalSourceID, this.CheatSheetItem.Player.SportCode, this.CheatSheetItem.Player.PositionCode);
      bool item2Found = false;
      if (supplementalSheet2 != null)
      {
        SupplementalSheetItem targetSuppSheetItem2 = SupplementalSheetItem.GetSupplementalSheetItem(supplementalSheet2.SupplementalSheetID, this.CheatSheetItem.PlayerID);
        if (targetSuppSheetItem2 != null)
        {
          int supplementalRank2 = targetSuppSheetItem2.Seqno;
          if (supplementalRank2 != 0)
          {
            item2Found = true;
            // load the supplemental rankings into the UI
            labSuppRanking2.Text = supplementalRank2.ToString();
            // load the supplemental ranking into the hideen field used by QTip
            _cbsRank = supplementalRank2.ToString();

            if (supplementalRank2 < 10)
            {
              labSuppRanking2.CssClass = "suppData oneDigit";
            }
            else if ((supplementalRank2 >= 10) && (supplementalRank2 < 100))
            {
              labSuppRanking2.CssClass = "suppData twoDigits";
            }
          }
        }
      }
      if (!item2Found)
      {
        labSuppRanking2.Text = String.Empty;
      }
    }


    private void BuildSuppSheetSupplementalRankings()
    {
      SportSeason currentSeason = SportSeason.GetCurrentSportSeason("FOO");

      // Supplemental Source 1 Rankings
      SupplementalSource supplementalSource1 = SupplementalSource.GetSupplementalSource("CBS");
      SupplementalSheet supplementalSheet1 = SupplementalSheet.GetSupplementalSheet(currentSeason.SeasonCode, supplementalSource1.SupplementalSourceID, this.SupplementalSheetItem.Player.SportCode, this.SupplementalSheetItem.Player.PositionCode);
      if (supplementalSheet1 != null)
      {
        SupplementalSheetItem targetItem = SupplementalSheetItem.GetSupplementalSheetItem(supplementalSheet1.SupplementalSheetID, this.SupplementalSheetItem.PlayerID);
        if(targetItem != null)  
        {
        int supplementalRank1 = targetItem.Seqno;
        if (supplementalRank1 != 0)
        {
          // load the supplemental rankings into the UI
          labSuppRanking1.Text = supplementalRank1.ToString();
          // load the supplemental ranking into the hideen field used by QTip
          _cbsRank = supplementalRank1.ToString();

          if (supplementalRank1 < 10)
          {
            labSuppRanking1.CssClass = "suppData oneDigit";
          }
          else if ( (supplementalRank1 >= 10) && (supplementalRank1 < 100) )
          {
            labSuppRanking1.CssClass = "suppData twoDigits";
          }
        }
        else
        {
          labSuppRanking1.Text = String.Empty;
        }
        //this.SuppSheet1Populated = true;
        }
      }
      else
      {
        //this.SuppSheet1Populated = false;
        labSuppRanking1.Text = String.Empty;
      }
    }


    private void BuildFeatureIcons(int positionCount)
    {
      string searchPhrase = String.Empty;
      if (this.ItemPlayer.PositionCode != "DF")
      {
        searchPhrase = this.ItemPlayer.FullName + " " + this.ItemPlayer.Team.Mascot;
      }
      else
      {
        searchPhrase = this.ItemPlayer.FullName + " defense" + " special teams";
      }

      hlGoogleNewsSearch.NavigateUrl = "https://www.google.com/search?hl=en&gl=us&tbm=nws&btnmeta_news_search=1&q=" + searchPhrase;
      hlTwitter.NavigateUrl = "~/fantasy-football/nfl/player-tweets.aspx?PlayerID=" + this.ItemPlayer.PlayerID.ToString();

      if (this.ItemPlayer.PositionCode != "DF")
      {
        hlGoogleNewsSearch.ToolTip = "Click to view the latest fantasy news about " + this.ItemPlayer.FullName + ".";
        // Twitter Search
        hlTwitter.ToolTip = "Click to view the latest tweets about " + this.ItemPlayer.FullName + ".";
      }
      else
      {
        hlGoogleNewsSearch.ToolTip = "Click to view the latest fantasy news about the " + this.ItemPlayer.FullName + " defense & special teams.";
        // Twitter Search
        hlTwitter.ToolTip = "Click to view the latest tweets about the " + this.ItemPlayer.FullName + " defense & special teams.";
      }

      // only hide the depth chart feature icon if the sheet contains multiple positions
      if (positionCount == 1)
      {
        hlDepthChart.Visible = !this.ItemPlayer.IsDefensiveTeamPlayer;
      }
   
    }


    private void BuildStatus(Player targetPlayer)
    {

      if (targetPlayer.StatusLogs.Count > 0)
      {

        hlStatusIcon.CssClass = "statusIcon";

        PlayerStatusLog highestPriorityLog = targetPlayer.StatusLogs.OrderBy(x => x.Priority).Take(1).ToList()[0];

        if (highestPriorityLog.StatusCode == PlayerStatusCodes.THREWR.ToString())
        {
          hlStatusIcon.CssClass = "statusIcon threeYrWR";
          string lastNameLastCharacter = targetPlayer.FullName.Substring(targetPlayer.FullName.Length - 1, 1);
          string playerNameWithApostrophe = (lastNameLastCharacter == "s") ? targetPlayer.FullName + "'" : targetPlayer.FullName + "'s";
          hlStatusIcon.ToolTip = "This is " + playerNameWithApostrophe + " third year in the NFL.";
        }
                else if (highestPriorityLog.StatusCode == PlayerStatusCodes.SWTEAM.ToString())
        {
          hlStatusIcon.CssClass = "statusIcon switchedTeams";
        }
        else if (highestPriorityLog.StatusCode == PlayerStatusCodes.RETIRD.ToString())
        {
          hlStatusIcon.CssClass = "statusIcon retired";
        }

        _statusSuppInfo = highestPriorityLog.SupplementalInfo;
        _statusSuppInfoTitle = highestPriorityLog.SupplementalInfoTitle;
      }
      else
      {
        hlStatusIcon.Visible = false;
      }
    }



    private void BuildStats(string statSeasonCode)
    {

      bool allStatsValid = true;

      // Fantasy Points Per Game Rank
      SportSeasonPlayerSeasonStat pointsPerGameRank = new SportSeasonPlayerSeasonStat();
      if (_pprSheet)
      {
        pointsPerGameRank = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", statSeasonCode, this.ItemPlayer.PlayerID, "FPPR");
      }
      else
      {
        pointsPerGameRank = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", statSeasonCode, this.ItemPlayer.PlayerID, "FPGR");
      }
      if (pointsPerGameRank != null)
      {
        labfppgRank.Text = pointsPerGameRank.StatValue.ToString();
        if (pointsPerGameRank.StatValue > 99.9)
        {
          labfppgRank.CssClass = "rankValue threeDigits";
        }
      }
      else
      {
        allStatsValid = false;
      }

      // Total Fantasy Points Rank
      SportSeasonPlayerSeasonStat totalFantasyPointsRank = new SportSeasonPlayerSeasonStat();
      if (_pprSheet)
      {
        totalFantasyPointsRank = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", statSeasonCode, this.ItemPlayer.PlayerID, "TPPR");
      }
      else
      {
        totalFantasyPointsRank = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", statSeasonCode, this.ItemPlayer.PlayerID, "TFPR");
      }

      if (totalFantasyPointsRank != null)  
      {
        labtfpRank.Text = totalFantasyPointsRank.StatValue.ToString();
        if (totalFantasyPointsRank.StatValue > 99.9)
        {
          labtfpRank.CssClass = "rankValue threeDigits";
        }
      }
      else
      {
        allStatsValid = false;
      }
      
      // Fantasy Points Per Game
      SportSeasonPlayerSeasonStat fantasyPointsPerGame = new SportSeasonPlayerSeasonStat();
      if (_pprSheet)
      {
        fantasyPointsPerGame = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", statSeasonCode, this.ItemPlayer.PlayerID, "FPGP");
      }
      else
      {
        fantasyPointsPerGame = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", statSeasonCode, this.ItemPlayer.PlayerID, "FPPG");
      }

      if (fantasyPointsPerGame != null)  
      {
        labFPPG.Text = fantasyPointsPerGame.StatValue.ToString();
      }
      else
      {
        allStatsValid = false;
      }

      // Total Fantasy Points
      SportSeasonPlayerSeasonStat totalFantasyPoints = new SportSeasonPlayerSeasonStat();
      if (_pprSheet)
      {
        totalFantasyPoints = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", statSeasonCode, this.ItemPlayer.PlayerID, "TFPP");
      }
      else
      {
        totalFantasyPoints = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStat("FOO", statSeasonCode, this.ItemPlayer.PlayerID, "TFP");
      }

      if (totalFantasyPoints != null)  
      {
        labTFP.Text = totalFantasyPoints.StatValue.ToString();
      }
      else
      {
        allStatsValid = false;
      }

      // Top Stat Tooltips
      if (_pprSheet)
      {
        acrTFPTopLeftSide.Attributes["title"] = this.ItemPlayer.FullName + " was ranked #" + labtfpRank.Text + " in 'Total Fantasy Points' in " + statSeasonCode + " using the PPR Scoring System.";
        acrFPPGTopRightSide.Attributes["title"] = this.ItemPlayer.FullName + " was ranked #" + labfppgRank.Text + " in 'Fantasy Points Per Game' in " + statSeasonCode + " using the PPR Scoring System.";
      }
      else
      {
        acrTFPTopLeftSide.Attributes["title"] = this.ItemPlayer.FullName + " was ranked #" + labtfpRank.Text + " in 'Total Fantasy Points' in " + statSeasonCode + " using the Standard Scoring System.";
        acrFPPGTopRightSide.Attributes["title"] = this.ItemPlayer.FullName + " was ranked #" + labfppgRank.Text + " in 'Fantasy Points Per Game' in " + statSeasonCode + " using the Standard Scoring System.";
      }

      // Bottom Stat Tooltips
      if (_pprSheet)
      {
        acrTFPBottomLeftSide.Attributes["title"] = this.ItemPlayer.FullName + " scored " + labTFP.Text + " 'Total Fantasy Points' in " + statSeasonCode + " using the PPR Scoring System.";
        acrFPPGBottomRightSide.Attributes["title"] = this.ItemPlayer.FullName + " scored " + labFPPG.Text + " 'Fantasy Points Per Game' in " + statSeasonCode + " using the PPR Scoring System.";
      }
      else
      {
        acrTFPBottomLeftSide.Attributes["title"] = this.ItemPlayer.FullName + " scored " + labTFP.Text + " 'Total Fantasy Points' in " + statSeasonCode + " using the Standard Scoring System.";
        acrFPPGBottomRightSide.Attributes["title"] = this.ItemPlayer.FullName + " scored " + labFPPG.Text + " 'Fantasy Points Per Game' in " + statSeasonCode + " using the Standard Scoring System.";
      }
      

      if (!allStatsValid)
      {
        panValidStatsContainer.Visible = false;
        panNoStatsContainer.Visible = true;
      }

    }


    private void LoadSuppSheetNoteData()
    {
      // supplemental racing sheets don't support notes
      fneFOONoteEditor.CheatSheetID = this.SupplementalSheetItem.SupplementalSheetID;
      fneFOONoteEditor.PlayerID = this.SupplementalSheetItem.PlayerID;
      // need to add back when implemented in database
      fneFOONoteEditor.Note = fneFOONoteEditor.Note = this.SupplementalSheetItem.Note;
      fneFOONoteEditor.BuildControl();
    }


    private void LoadCheatSheetNoteData()
    {
      // supplemental racing sheets don't support notes
      fneFOONoteEditor.CheatSheetID = this.CheatSheetItem.CheatSheetID;
      fneFOONoteEditor.PlayerID = this.CheatSheetItem.PlayerID;
      // need to add back when implemented in database
      fneFOONoteEditor.Note = fneFOONoteEditor.Note = this.CheatSheetItem.Note;
      fneFOONoteEditor.BuildControl();
    }


    void BuildPlayerTeamInfo(string seasonCode, int positionCount)
    {
      // configure the mascot for the respective team along with the team-color-specific drag handle
      panDragHandle.CssClass = this.ItemPlayer.Team.MascotCSSClass + " dragContainer";

      // configure the styles to determine team color
      string teamStyle = this.ItemPlayer.Team.CityCSSClass + this.ItemPlayer.Team.Mascot;
      // this is temporary and needs to be removed after 2.29 launch
      if(teamStyle == "losAngelesRams")
      {
        teamStyle = "saintLouisRams2 " + teamStyle;
      }
      panPlayerTemplate.CssClass = "fOOCheatSheetItemTemplateControl2 " + teamStyle + "2";

      // configure the player name and positions
      if (this.ItemPlayer.PositionCode != "DF")
      {
        labPlayerName.Text = this.ItemPlayer.FullName;
      }
      else
      {
        labPlayerName.Text = this.ItemPlayer.FirstName;
      }

      // only show the position if there are more than 1 position used in the sheet
      if (positionCount > 1)
      {
        labPlayerPosition.Text = " " + this.ItemPlayer.Position.PositionCode;
      }


      // configure an anchor for traversing the sheet through hyperlinks
      string playerName = this.ItemPlayer.FirstName + this.ItemPlayer.LastName;
      playerAnchor.Attributes.Add("name", playerName);

      // Initialize the team info
      StringBuilder sbExperience = new StringBuilder();
      if (this.ItemPlayer.PositionCode != "DF")
      {
        SportSeason currentSeason = SportSeason.GetCurrentSportSeason("FOO");

        sbExperience.Append(this.ItemPlayer.Team.Mascot + ", ");
        sbExperience.Append("#" + this.ItemPlayer.Number.ToString() + ", ");
        // experience
        int yearsInLeague = Int32.Parse(currentSeason.SeasonCode) - this.ItemPlayer.FirstYear.Year;
        if (yearsInLeague != 0)
        {
          if (yearsInLeague > 1)
          {
            sbExperience.Append(yearsInLeague.ToString() + " yrs exp");
          }
          else
          {
            sbExperience.Append(yearsInLeague.ToString() + " yr exp");
          }
          labTeamInfo.Text = sbExperience.ToString() + ", ";
          labTeamInfo.ToolTip = this.ItemPlayer.FullName + " has " + yearsInLeague.ToString() + " years of experience in the NFL.";
        }
        else
        {
          sbExperience.Append("Rookie");
          labTeamInfo.Text = sbExperience.ToString() + ", ";
        }
      }
     
      // age
      StringBuilder sbAge = new StringBuilder();

      DateTime playerBirthday = this.ItemPlayer.BirthDate;
      DateTime today = DateTime.Today;
      int playerAge = today.Year - playerBirthday.Year;
      if (playerBirthday > today.AddYears(-playerAge))
      {
        playerAge--;
      }
      sbAge.Append(playerAge + " yo");

      // bye week
      ByeWeek teamBye = ByeWeek.GetByeWeek(seasonCode, this.ItemPlayer.Team.SportCode, this.ItemPlayer.Team.TeamCode);
      if (teamBye != null)
      {
        if (this.ItemPlayer.PositionCode != FOOPositionsOffense.DF.ToString())
        {
          hlByeWeek.Text = "Bye " + teamBye.Bye;
        }
        else
        {
          hlByeWeek.Text = "Bye " + teamBye.Bye;
        }
      }
      else
      {
        hlByeWeek.Text = "Bye TBD";
      }

      if (this.ItemPlayer.PositionCode != "DF")
      {
        labAge.Text = sbAge.ToString() + ", ";
        labAge.ToolTip = this.ItemPlayer.FullName + " is " + playerAge.ToString() + " years old.";
      }


      // helmet link
      int statSeason = 0;
      if (Helpers.IsMiddleOfSeason(FOO.FOOString))
      {
        statSeason = int.Parse(seasonCode);
      }
      else
      {
        statSeason = int.Parse(seasonCode) - 1;
      }
      string statSeasonCode = statSeason.ToString();

      hlHelmetLink.ToolTip = "Click to view " + statSeasonCode + " team statistics for the " + this.ItemPlayer.Team.FullTeamName + ".";

      SetTeamStatLinks(this.ItemPlayer.Team.Abbreviation);

      // load a hidden field for the team rankings poup
      //hfMascot.Value = this.ItemPlayer.Team.Mascot;
    }


    private void SetTeamStatLinks(string teamAbbreviation)
    {
      switch (teamAbbreviation)
      {
        // AFC East
        case "BUF":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/buf/buffalo-bills";
          break;
        case "NYJ":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/nyj/new-york-jets";
          break;
        case "NE":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/ne/new-england-patriots";
          break;
        case "MIA":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/mia/miami-dolphins";
          break;

        // AFC North
        case "BAL":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/bal/baltimore-ravens";
          break;
        case "PIT":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/pit/pittsburgh-steelers";
          break;
        case "CLE":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/cle/cleveland-browns";
          break;
        case "CIN":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/cin/cincinnati-bengals";
          break;

        // AFC South
        case "TEN":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/ten/tennessee-titans";
          break;
        case "JAC":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/jac/jacksonville-jaguars";
          break;
        case "IND":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/ind/indianapolis-colts";
          break;
        case "HOU":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/hou/houston-texans";
          break;

        // AFC West
        case "DEN":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/den/denver-broncos";
          break;
        case "SD":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/sd/san-diego-chargers";
          break;
        case "LV":
          hlHelmetLink.NavigateUrl = "https://www.espn.com/nfl/team/stats/_/name/lv";
          break;
        case "KC":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/kc/kansas-city-chiefs";
          break;

        // NFC East
        case "DAL":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/dal/dallas-cowboys";
          break;
        case "WAS":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/wsh/washington-redskins";
          break;
        case "PHI":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/phi/philadelphia-eagles";
          break;
        case "NYG":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/nyg/new-york-giants";
          break;

        // NFC North
        case "CHI":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/chi/chicago-bears";
          break;
        case "MIN":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/min/minnesota-vikings";
          break;
        case "GB":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/gb/green-bay-packers";
          break;
        case "DET":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/det/detroit-lions";
          break;

        // NFC South
        case "ATL":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/atl/atlanta-falcons";
          break;
        case "TB":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/tb/tampa-bay-buccaneers";
          break;
        case "NO":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/no/new-orleans-saints";
          break;
        case "CAR":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/car/carolina-panthers";
          break;

        // NFC West
        case "LAR":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/la/los-angeles-rams";
          break;
        case "SEA":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/sea/seattle-seahawks";
          break;
        case "SF":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/sf/san-francisco-49ers";
          break;
        case "ARI":
          hlHelmetLink.NavigateUrl = "http://espn.go.com/nfl/team/stats/_/name/ari/arizona-cardinals";
          break;

        default:
          break;
      }

    }



 
  }
}
