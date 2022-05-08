using System;
using System.Collections.Generic;
using System.Text;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class RACSheetItemTemplate : System.Web.UI.UserControl
  {

    /// <summary>
    /// Saves the items type of the particular item, either a CheatSheetItem or SupplementalSheetItem
    /// </summary>
    public SheetType CurrentItemType 
    {
      get
      {
        return (ViewState["CurrentItemType"] == null) ? SheetType.CheatSheet : (SheetType)ViewState["CurrentItemType"];
      }
      set
      {
        ViewState["CurrentItemType"] = value;
      }
    }


    /// <summary>
    /// The sheet to which this particular item belongs
    /// </summary>
    private SupplementalSheet _currentSupplementalSheet = null;
    public SupplementalSheet CurrentSupplementalSheet
    {
      get
      {
        return _currentSupplementalSheet;
      }
      set
      {
        _currentSupplementalSheet = value;
      }
    }

    /// <summary>
    /// If this is a Supplemental Sheet Item, then this properly will hold a reference to that supplemental sheet
    /// </summary>
    private SupplementalSheetItem _supplementalSheetItem = null;
    public SupplementalSheetItem CurrentSupplementalSheetItem
    {
      get { return _supplementalSheetItem; }
      set { _supplementalSheetItem = value; }
    }

    /// <summary>
    /// If this is a Cheat Sheet Item, then this property will hold a reference to that cheat sheet
    /// </summary>
    private CheatSheet _currentCheatSheet = null;
    public CheatSheet CurrentCheatSheet
    {
      get
      {
        return _currentCheatSheet;
      }
      set
      {
        _currentCheatSheet = value;
      }
    }

    /// <summary>
    /// If this is a cheat sheet item then this property will hold a reference to the current cheat sheet item
    /// </summary>
    private CheatSheetItem _cheatSheetItem = null;
    public CheatSheetItem CurrentCheatSheetItem
    {
      get { return _cheatSheetItem; }
      set { _cheatSheetItem = value; }
    }

    /// <summary>
    /// If this is a Supplemental Sheet Item then this property will be used to populate the item.
    /// </summary>
    public SupplementalSheetItem SupplementalSheetPlayerItem
    {
      set 
      {

        this.CurrentSupplementalSheetItem = new SupplementalSheetItem(value.SupplementalSheetID, value.PlayerID, value.Seqno, value.Note, value.MappedProperties);
        this.CurrentSupplementalSheet = SupplementalSheet.GetSupplementalSheet(value.SupplementalSheetID);
      }
    }

    /// <summary>
    /// If this is a Cheat Sheet Item then this property will be used to populate the item.
    /// </summary>
    public CheatSheetItem CheatSheetPlayerItem
    {
      set
      {

        this.CurrentCheatSheetItem = new CheatSheetItem(value.CheatSheetID, value.PlayerID, value.Seqno, value.Note, null);
        this.CurrentCheatSheet = CheatSheet.GetCheatSheet(value.CheatSheetID);
      }
    }

  

    protected void Page_Load(object sender, EventArgs e)
    {
      //BuildControlContent();
    }


    /// <summary>
    /// This method calls the other methods which actually build the driver template;
    /// </summary>
    public void BuildControlContent()
    {
      // Load supplemental rankings, if necessary
      if (SportSetting.Racing.ShowSupplementalRankings || (this.CurrentItemType == SheetType.SuppSheet))
      {
        BuildSupplementalRankings();
      }
      else
      {
        panSuppRankingContainer.Visible = false;
        panMapGlassContainer.Visible = false;
      }


      BuildPlayerTeamInfo();
      BuildStats();
      LoadSocialIcons();
      // only cheat sheets have notes
      if (this.CurrentItemType == SheetType.CheatSheet)
      {
        LoadNoteData();
      }
      else
      {
        panNote.Visible = false;
      }
      //RegisterSupplementalSheetItemPopup();
    }

    /// <summary>
    /// This method will build the supplemental rankings associated with a racing cheat sheet
    /// </summary>
    void BuildSupplementalRankings()
    {
      SportSeason currentSportSeason = SportSeason.GetCurrentSportSeason("RAC");
      string sportCode = String.Empty;
      string positionCode = String.Empty;

      // determine sport code
      switch (this.CurrentItemType)
      {
        case SheetType.CheatSheet:
          sportCode = this.CurrentCheatSheetItem.Player.Team.SportCode;
          positionCode = this.CurrentCheatSheetItem.Player.PositionCode;    
          break;
        case SheetType.SuppSheet:
          sportCode = this.CurrentSupplementalSheetItem.Player.Team.SportCode;
          positionCode = this.CurrentSupplementalSheetItem.Player.PositionCode;    
          break;
      }

      // CSWR Ranking
      SupplementalSheet cswrSuppSheet = SupplementalSheet.GetSupplementalSheet(currentSportSeason.SeasonCode, SupplementalSource.GetSupplementalSource("CSWR").SupplementalSourceID, 
        sportCode, positionCode);
      if (cswrSuppSheet != null)
      {
        int driverRank = 0;

        SupplementalSheetItem targetSuppSheetItem = new SupplementalSheetItem();

        switch (this.CurrentItemType)
        {
          case SheetType.CheatSheet:
            targetSuppSheetItem = SupplementalSheetItem.GetSupplementalSheetItem(cswrSuppSheet.SupplementalSheetID, this.CurrentCheatSheetItem.PlayerID);
            if (targetSuppSheetItem != null)
            {
              driverRank = targetSuppSheetItem.Seqno;
            }
            else
            {
              driverRank = 0;
            }
            break;
          case SheetType.SuppSheet:
            targetSuppSheetItem = SupplementalSheetItem.GetSupplementalSheetItem(cswrSuppSheet.SupplementalSheetID, this.CurrentSupplementalSheetItem.PlayerID);
            if (targetSuppSheetItem != null)
            {
              driverRank = targetSuppSheetItem.Seqno;
            }
            else
            {
              driverRank = 0;
            }
            break;
        }

        if (driverRank > 0)
        {
          labCSWRRank.Text = driverRank.ToString() + ".";
          labCSWRRankDecimal.Text = "0";
          hfCSWR.Value = driverRank.ToString();
        }
        else
        {
          labCSWRRank.Text = "n/r";
          labCSWRRank.CssClass = "notRanked";
          labCSWRRank.ToolTip = "not ranked";
        }
      }

    }


    void BuildPlayerTeamInfo()
    {

      // build car number
      string carNumber = String.Empty;
      // determine type of sheet
      switch (this.CurrentItemType)
      {
        case SheetType.CheatSheet:
          carNumber = this.CurrentCheatSheetItem.Player.Number.ToString();
          break;
        case SheetType.SuppSheet:
          carNumber = this.CurrentSupplementalSheetItem.Player.Number.ToString();
          break;
      }
      // configure number appearance to be as uniform as posible
      if (carNumber.Length == 1)
      {
        carNumber = "0" + carNumber;
      }
      else if (carNumber.Length == 3)
      {
        labCarNumber.CssClass = "threeNumbers";
      }
      labCarNumber.Text = carNumber; 

      // build driver name
      switch (this.CurrentItemType)
      {
        case SheetType.CheatSheet:
          labDriverName.Text = this.CurrentCheatSheetItem.Player.FirstName + " " + this.CurrentCheatSheetItem.Player.LastName;
          break;
        case SheetType.SuppSheet:
          labDriverName.Text = this.CurrentSupplementalSheetItem.Player.FirstName + " " + this.CurrentSupplementalSheetItem.Player.LastName;
          break;
      }

      // build stats driver popup
      switch (this.CurrentItemType)
      {
        case SheetType.CheatSheet:
          //labStatsPopupDriverName.Text = this.CurrentCheatSheetItem.Player.FirstName + " " + this.CurrentCheatSheetItem.Player.LastName;
          break;
        case SheetType.SuppSheet:
          //labStatsPopupDriverName.Text = this.CurrentSupplementalSheetItem.Player.FirstName + " " + this.CurrentSupplementalSheetItem.Player.LastName;
          break;
      }
      
      // experience
      switch (this.CurrentItemType)
      {
        case SheetType.CheatSheet:
          // experience
          switch (this.CurrentCheatSheetItem.Player.YearsExperience)
          {
            case 0:
              labExpRookie.Visible = true;
              labExpYearsSingular.Visible = false;
              labExpYearsPlural.Visible = false;
              break;
            case 1:
              labExpYears.Text = this.CurrentCheatSheetItem.Player.YearsExperience.ToString();
              labExpRookie.Visible = false;
              labExpYearsSingular.Visible = true;
              labExpYearsPlural.Visible = false;
              break;
            default:
              labExpYears.Text = this.CurrentCheatSheetItem.Player.YearsExperience.ToString();
              labExpRookie.Visible = false;
              labExpYearsSingular.Visible = false;
              labExpYearsPlural.Visible = true;
              break;
          }
          break;
        case SheetType.SuppSheet:
          // experience
          switch (this.CurrentSupplementalSheetItem.Player.YearsExperience)
          {
            case 0:
              labExpRookie.Visible = true;
              labExpYearsSingular.Visible = false;
              labExpYearsPlural.Visible = false;
              break;
            case 1:
              labExpYears.Text = this.CurrentSupplementalSheetItem.Player.YearsExperience.ToString();
              labExpRookie.Visible = false;
              labExpYearsSingular.Visible = true;
              labExpYearsPlural.Visible = false;
              break;
            default:
              labExpYears.Text = this.CurrentSupplementalSheetItem.Player.YearsExperience.ToString();
              labExpRookie.Visible = false;
              labExpYearsSingular.Visible = false;
              labExpYearsPlural.Visible = true;
              break;
          }
          break;
      }

      // car make
      switch (this.CurrentItemType)
      {
        case SheetType.CheatSheet:
          labCarMake.Text = this.CurrentCheatSheetItem.Player.TeamCode.Substring(0, 1) + this.CurrentCheatSheetItem.Player.TeamCode.Substring(1, 3).ToLower();
          break;
        case SheetType.SuppSheet:
          labCarMake.Text = this.CurrentSupplementalSheetItem.Player.TeamCode.Substring(0, 1) + this.CurrentSupplementalSheetItem.Player.TeamCode.Substring(1, 3).ToLower();
          break;
      }


      // configure driver name on container class
      switch (this.CurrentItemType)
      {
        case SheetType.CheatSheet:
          panPlayerTemplate.CssClass = "racCheatSheetItemTemplateControl " + this.CurrentCheatSheetItem.Player.FirstName.ToLower().Replace(".", "") + this.CurrentCheatSheetItem.Player.LastName.Replace(" ", "").Replace(".", "");
          break;
        case SheetType.SuppSheet:
          panPlayerTemplate.CssClass = "racCheatSheetItemTemplateControl " + this.CurrentSupplementalSheetItem.Player.FirstName.ToLower().Replace(".", "") + this.CurrentSupplementalSheetItem.Player.LastName.Replace(" ", "").Replace(".", "");
          break;
      }

      switch (this.CurrentItemType)
      {
        case SheetType.CheatSheet:
          panPlayerTemplate.CssClass = "racCheatSheetItemTemplateControl " + this.CurrentCheatSheetItem.Player.FirstName.ToLower().Replace(".", "") + this.CurrentCheatSheetItem.Player.LastName.Replace(" ", "").Replace(".", "");
          break;
        case SheetType.SuppSheet:
          panPlayerTemplate.CssClass = "racCheatSheetItemTemplateControl " + this.CurrentSupplementalSheetItem.Player.FirstName.ToLower().Replace(".", "") + this.CurrentSupplementalSheetItem.Player.LastName.Replace(" ", "").Replace(".", "");
          break;
      }
    }





    public void LoadSocialIcons()  
    {
      // configure twitter icon
      hlTwitter.Visible = true;
      hlTwitter.Target = "_blank";
      if (this.CurrentItemType == SheetType.CheatSheet)
      {
        //hlTwitter.NavigateUrl = "~/fantasy-racing/nascar/driver-tweets.aspx?PlayerID=" + this.CurrentCheatSheetItem.PlayerID.ToString();
        hlTwitter.ToolTip = "Click to view the latest tweets about " + this.CurrentCheatSheetItem.Player.FullName + ".";
      }
      else  
      {
        //hlTwitter.NavigateUrl = "~/fantasy-racing/nascar/driver-tweets.aspx?PlayerID=" + this.CurrentSupplementalSheetItem.PlayerID.ToString();
        hlTwitter.ToolTip = "Click to view the latest tweets about " + this.CurrentSupplementalSheetItem.FullName + ".";
      }
      
      // google news
      StringBuilder sbGoogleNewsLink = new StringBuilder();
      string firstName = String.Empty;
      string lastName = String.Empty;
      if (this.CurrentItemType == SheetType.CheatSheet)
      {
        firstName = this.CurrentCheatSheetItem.Player.FirstName;
        lastName = this.CurrentCheatSheetItem.Player.LastName;
      }
      else
      {
        firstName = this.CurrentSupplementalSheetItem.Player.FirstName;
        lastName = this.CurrentSupplementalSheetItem.Player.LastName;
      }

      string fullName = firstName.Trim() + "+" + lastName.Trim(); 
      
      sbGoogleNewsLink.Append("https://www.google.com/search?hl=en&gl=us&tbm=nws&btnmeta_news_search=1&q=");
      sbGoogleNewsLink.Append(fullName);
      sbGoogleNewsLink.Append("&oq=");
      sbGoogleNewsLink.Append(fullName);
      sbGoogleNewsLink.Append("&aq=f&aqi=d1g1d-o1&aql=&gs_sm=e&gs_upl=1069l2701l0l2898l12l12l0l5l5l0l238l1031l1.4.2l7l0");

      hlGoogleNews.NavigateUrl = sbGoogleNewsLink.ToString();
      hlGoogleNews.ToolTip = "Click to view the latest news about " + firstName + " " + lastName;

      // google search
      StringBuilder sbGoogleSearchLink = new StringBuilder();

      sbGoogleSearchLink.Append("https://www.google.com/webhp?hl=en&tab=nw#hl=en&cp=6&gs_id=10&xhr=t&q=");
      sbGoogleSearchLink.Append(fullName);
      sbGoogleSearchLink.Append("&oq=carl+edwards");
      sbGoogleSearchLink.Append(fullName);

      hlGoogleSearch.NavigateUrl = sbGoogleSearchLink.ToString();
      hlGoogleSearch.ToolTip = "Click to search Google for " + firstName + " " + lastName;

      // ifantasyrace
      string seasonCode = String.Empty;
      int playerID = 0;
      int supplementalSourceID = 0;
      SupplementalSource activeSource = SupplementalSource.GetSupplementalSource("IFR");
      supplementalSourceID = activeSource.SupplementalSourceID;

      if (this.CurrentItemType == SheetType.CheatSheet)
      {
        playerID = this.CurrentCheatSheetItem.PlayerID;
        seasonCode = this.CurrentCheatSheet.SeasonCode;
      }
      else  
      {
        playerID = this.CurrentSupplementalSheetItem.PlayerID;
        seasonCode = this.CurrentSupplementalSheet.SeasonCode;
      }

      SportSeasonSuppPlayerReview driverReview = SportSeasonSuppPlayerReview.GetSportSeasonSuppPlayerReview(SessionHandler.CurrentSportCode, seasonCode, supplementalSourceID, playerID);
      if (driverReview != null)
      {
        hlIFantasyRace.Visible = true;
        hlIFantasyRace.NavigateUrl = driverReview.ReviewURL;
        hlIFantasyRace.ToolTip = "Click to read a " + seasonCode + " preview of " + firstName + " " + lastName + " from ifantasyrace.com.";
      }
      else
      {
        hlDummyLink.Visible = true;
      }


    }


    void LoadNoteData()
    {
      // supplemental racing sheets don't support notes
      if (this.CurrentItemType == SheetType.CheatSheet)
      {
        neNoteEditor.CheatSheetID = this.CurrentCheatSheet.CheatSheetID;
        neNoteEditor.PlayerID = this.CurrentCheatSheetItem.PlayerID;
        neNoteEditor.Note = this.CurrentCheatSheetItem.Note;
        neNoteEditor.BuildControl();
      }

    }






    void BuildStats()
    {
      string sportCode = String.Empty;
      string seasonCode = String.Empty;
      string playerName = String.Empty;
      int statSeason = 0;
      int playerID = 0;

      // configure driver name on container class
      switch (this.CurrentItemType)
      {
        case SheetType.CheatSheet:
          sportCode = this.CurrentCheatSheet.SportCode;
          seasonCode = this.CurrentCheatSheet.SeasonCode;
          playerID = this.CurrentCheatSheetItem.PlayerID;
          statSeason = int.Parse(this.CurrentCheatSheet.StatsSeasonCode);
          playerName = this.CurrentCheatSheetItem.Player.FullName;
          break;
        case SheetType.SuppSheet:
          sportCode = this.CurrentSupplementalSheet.SportCode;
          seasonCode = this.CurrentSupplementalSheet.SeasonCode;
          playerID = this.CurrentSupplementalSheetItem.PlayerID;
          playerName = this.CurrentSupplementalSheetItem.Player.FullName;
          statSeason = int.Parse(seasonCode) - 1;
          break;
      }

      string statSeasonString = statSeason.ToString();
      //labStatsSeason.Text = statSeasonString;
      
      // some drivers may have an ADP, but no stats from the previous season
      List<SportSeasonPlayerSeasonStat> playerStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(sportCode, statSeasonString, playerID);
      if (playerStats != null && playerStats.Count > 0)
      {
        // points
        SportSeasonPlayerSeasonStat pointsStat = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "PNTS"; });
        if (pointsStat != null)
        {
          labPoints.Text = pointsStat.StatValue.ToString();
        }
        else
        {
          labPoints.Text = "0";
        }
        // rank
        SportSeasonPlayerSeasonStat rankStat = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "RANK"; });
        if (rankStat != null)
        {
          labRank.Text = rankStat.StatValue.ToString();
        }
        else
        {
          labRank.Text = "0";
        }
        // wins
        SportSeasonPlayerSeasonStat winsStat = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "WINS"; });
        if (winsStat != null)
        {
          labWins.Text = winsStat.StatValue.ToString();
        }
        else
        {
          labWins.Text = "0";
        }
        // afp
        SportSeasonPlayerSeasonStat afpStat = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "AFP"; });
        if (afpStat != null)
        {
          labAFP.Text = afpStat.StatValue.ToString();
        }
        else
        {
          labAFP.Text = "0";
        }
        // top 10
        SportSeasonPlayerSeasonStat top10Stat = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "TP10"; });
        if (top10Stat != null)
        {
          labTop10.Text = top10Stat.StatValue.ToString();
        }
        else
        {
          labTop10.Text = "0";
        }
        // winnings
        SportSeasonPlayerSeasonStat winnings = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "WNGS"; });
        // behind
        SportSeasonPlayerSeasonStat behind = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "BHND"; });
        // starts
        SportSeasonPlayerSeasonStat starts = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "STRT"; });
        // poles
        SportSeasonPlayerSeasonStat poles = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "POLE"; });
        // top 5
        SportSeasonPlayerSeasonStat top5Stat = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "TP5"; });
      }
      else  
      {
        // points
        labPoints.Text = "0";
        // rank
        labRank.Text = "0";
        // wins
        labWins.Text = "0";
        // afp
        labAFP.Text = "0";
        // top 10
        labTop10.Text = "0";
      }

      /*****************************/
      /*         ADP               */
      /*****************************/

      // Supplemental
      SportSeasonPlayerSeasonStat adp = playerStats.Find(delegate(SportSeasonPlayerSeasonStat p) { return p.StatCode == "ADP"; });

      if (adp != null)
      {
        double roundedADP = Math.Round(adp.StatValue, 1);
        // load the hidden field necessary to load the supplmental popup
        hfADP.Value = roundedADP.ToString();

        string[] adpParts = roundedADP.ToString().Split('.');
        // if it's not a whole number
        if (adpParts.Length > 1)
        {
          // Supplemental
          labADP.Text = adpParts[0] + ".";
          labADPDecimal.Text = adpParts[1];
          // Supp Popup
          //labSuppRankingsADP.Text = adpParts[0] + ".";
          //labSuppRankingsADPDecimal.Text = adpParts[1];
        }
        // if it is a whole number, no number after decimal
        else
        {
          labADP.Text = adpParts[0] + ".";
          labADPDecimal.Text = "0";
          //labSuppRankingsADP.Text = adpParts[0] + ".";
          //labSuppRankingsADPDecimal.Text = "0";
        }
      }
      else
      {
        labADP.Text = "n/r";
        labADP.CssClass = "notRanked";
        labADP.ToolTip = "not ranked";
      }


      // load the fields necessary to generate the stats JQuery QTip popup
      hfStatSeasonCode.Value = statSeasonString;
      hfPlayerID.Value = playerID.ToString();
      hfPlayerName.Value = playerName;

    }  /* close BuildStats */


  
  }
}
