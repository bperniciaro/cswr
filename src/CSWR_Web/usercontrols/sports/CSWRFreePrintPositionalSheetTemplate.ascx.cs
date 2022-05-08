using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class CSWRFreePrintPositionalSheetTemplate : System.Web.UI.UserControl
  {

    public string PositionCode { get; set; }
    public string SportCode { get; set; }

    private int _playerCounter = 0;
    private CSWRRankingType _rankType;
    private List<Player> _statSortedPlayers;
    private SupplementalSheet _suppSheet;
    private string _seasonCode;


    /// <summary>
    /// Load all of the relevant controls
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        SportSeason currentSportSeason = SportSeason.GetCurrentSportSeason(this.SportCode);
        _seasonCode = currentSportSeason.SeasonCode;

          // determine the type of ranking we'll provide
        DetermineRankType();
        // load sorted players for future reference.
        LoadStatSortedPlayers();
        // summary parameters
        LoadSummary();
        // build the sheet
        BuildSheet();
      }
    }

    private void DetermineRankType()
    {
      if (Helpers.IsMiddleOfSeason(this.SportCode))
      {
        _rankType = CSWRRankingType.PlayerStat;
      }
      else
      {
        _rankType = CSWRRankingType.CSWRRank;
      }
    }


    /// <summary>
    /// Go ahead and create a list of players sorted by a particular stat.  This way we'll have a list to reference when
    /// we add the reference rankings to each player in the sheet
    /// </summary>
    private void LoadStatSortedPlayers()
    {
      string supplementalStat = String.Empty;
      SortDir sortDir = SortDir.ASC;
      switch (this.SportCode)
      {
        case "FOO":
          supplementalStat = "TFP";
          sortDir = SortDir.DESC;
          break;
        case "RAC":
          supplementalStat = "RANK";
          sortDir = SortDir.ASC;
          break;
      }

      SportSeason currentSportSeason = SportSeason.GetCurrentSportSeason(this.SportCode);

      _statSortedPlayers = Player.GetPlayers(this.SportCode, currentSportSeason.LastSeasonCode, this.PositionCode, false, supplementalStat, sortDir.ToString());
    }




    /// <summary>
    /// This method actually builds the sheet with dynamic data
    /// </summary>
    private void BuildSheet()
    {
      // if the supplemental sheet is null then show an error
      if (_suppSheet == null)
      {
        mbMessageBox.MessageType = MessageType.ERROR;
        mbMessageBox.Message = new StringBuilder("Sheet Not Found");
        mbMessageBox.WidthPercentage = 30;
        panSheetSummary.Visible = false;
      }
      else
      {

        // since we don't know if we'll be binding players or drivers, we have to wire-up the 
        // itemdatabound even dynamically at run-time
        switch (this.SportCode)
        {
          case "FOO":
            repPlayersLeftSide.ItemDataBound += new RepeaterItemEventHandler(repPlayers_ItemDataBound);
            repPlayersRightSide.ItemDataBound += new RepeaterItemEventHandler(repPlayers_ItemDataBound);
            switch (_rankType)
            {
              case CSWRRankingType.CSWRRank:
                BuildFOOCSWRRankSheet();
                break;
              case CSWRRankingType.PlayerStat:
                BuildFOOPlayerStatSheet();
                break;
            }
            break;
  
          case "RAC":
            repPlayersLeftSide.ItemDataBound += new RepeaterItemEventHandler(repDrivers_ItemDataBound);
            repPlayersRightSide.ItemDataBound += new RepeaterItemEventHandler(repDrivers_ItemDataBound);
            BuildRACSheet();
            break;
        }
      }

    }



    /// <summary>
    /// Build and bind the collections we need to build a football cheat sheet
    /// </summary>
    /// <param name="fooSuppSheetItems"></param>
    private void BuildFOOPlayerStatSheet()
    {
      int maxPlayersPerSheet = Globals.CswrMaxPrintableSingleSheetRows * 2;
      int maxPositionalPlayers = Helpers.GetMaxRankPlayersConsideredBySportPosition(this.SportCode, this.PositionCode);
      if (maxPositionalPlayers < maxPlayersPerSheet)
      {
        maxPlayersPerSheet = maxPositionalPlayers;
      }

      List<SportSeasonPlayerSeasonStat> playerRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(this.SportCode, _seasonCode, 
                                                                      this.PositionCode, "TFP").Take(maxPlayersPerSheet).ToList();

      // determine if we'll need 2 rows
      if (playerRankings.Count > Globals.CswrMaxPrintableSingleSheetRows)
      {
        // bind the left column
        repPlayersLeftSide.DataSource = playerRankings.GetRange(0, Globals.CswrMaxPrintableSingleSheetRows);
        repPlayersLeftSide.DataBind();

        // bind the right column
        if ((playerRankings.Count - Globals.CswrMaxPrintableSingleSheetRows) > 0)
        {
          repPlayersRightSide.DataSource = playerRankings.GetRange(Globals.CswrMaxPrintableSingleSheetRows, playerRankings.Count - Globals.CswrMaxPrintableSingleSheetRows);
          repPlayersRightSide.DataBind();
        }
      }
      else
      {
        panRightColumn.Visible = false;
        panLeftColumn.CssClass = "driverOneColumnContainer";
        repPlayersLeftSide.DataSource = playerRankings;
        repPlayersLeftSide.DataBind();
      }
    }


    /// <summary>
    /// Build and bind the collections we need to build a football cheat sheet
    /// </summary>
    /// <param name="fooSuppSheetItems"></param>
    private void BuildFOOCSWRRankSheet()
    {
      int maxPlayersPerSheet = Globals.CswrMaxPrintableSingleSheetRows * 2;

      int maxPositionalPlayers = Helpers.GetMaxRankPlayersConsideredBySportPosition(this.SportCode, this.PositionCode);
      if (maxPositionalPlayers < maxPlayersPerSheet)
      {
        maxPlayersPerSheet = maxPositionalPlayers;
      }

      SupplementalSource targetSource = SupplementalSource.GetSupplementalSource("CSWR");
      SupplementalSheet targetSheet = SupplementalSheet.GetSupplementalSheet(_seasonCode, targetSource.SupplementalSourceID,
                                                                                FOO.FOOString, this.PositionCode);
      List<SupplementalSheetItem> fooSuppSheetItems = SupplementalSheetItem.GetSupplementalSheetItems(targetSheet.SupplementalSheetID).Take(maxPlayersPerSheet).ToList();

      // determine if we'll need 2 rows
      if (fooSuppSheetItems.Count > Globals.CswrMaxPrintableSingleSheetRows)
      {
        // bind the left column
        repPlayersLeftSide.DataSource = fooSuppSheetItems.GetRange(0, Globals.CswrMaxPrintableSingleSheetRows);
        repPlayersLeftSide.DataBind();

        // bind the right column
        if ((fooSuppSheetItems.Count - Globals.CswrMaxPrintableSingleSheetRows) > 0)
        {
          repPlayersRightSide.DataSource = fooSuppSheetItems.GetRange(Globals.CswrMaxPrintableSingleSheetRows, fooSuppSheetItems.Count - Globals.CswrMaxPrintableSingleSheetRows);
          repPlayersRightSide.DataBind();
        }
      }
      else
      {
        panRightColumn.Visible = false;
        panLeftColumn.CssClass = "driverOneColumnContainer";
        repPlayersLeftSide.DataSource = fooSuppSheetItems;
        repPlayersLeftSide.DataBind();
      }
    }




    /// <summary>
    /// Build and bind the collections we need to build a racing cheat sheet
    /// </summary>
    /// <param name="fooSuppSheetItems"></param>
    private void BuildRACSheet()
    {
      // get the necessary rankings
      SupplementalSource targetSource = SupplementalSource.GetSupplementalSource("CSWR");
      SupplementalSheet targetSheet = SupplementalSheet.GetSupplementalSheet(_seasonCode, targetSource.SupplementalSourceID, 
                                                                              this.SportCode, this.PositionCode);
      int maxPlayersPerSheet = Globals.CswrMaxPrintableSingleSheetRows * 2;
      int maxPositionalPlayers = Helpers.GetMaxRankPlayersConsideredBySportPosition(this.SportCode, this.PositionCode);
      if (maxPositionalPlayers < maxPlayersPerSheet)
      {
        maxPlayersPerSheet = maxPositionalPlayers;
      }
      List<SupplementalSheetItem> racSuppSheetItems = SupplementalSheetItem.GetSupplementalSheetItems(targetSheet.SupplementalSheetID).Take(maxPlayersPerSheet).ToList();

      // determine if we'll need 2 columns
      if (racSuppSheetItems.Count > Globals.CswrMaxPrintableSingleSheetRows)
      {
        // bind the left column
        repPlayersLeftSide.DataSource = racSuppSheetItems.GetRange(0, Globals.CswrMaxPrintableSingleSheetRows);
        repPlayersLeftSide.DataBind();

        // bind the right column
        if ((racSuppSheetItems.Count - Globals.CswrMaxPrintableSingleSheetRows) > 0)
        {
          repPlayersRightSide.DataSource = racSuppSheetItems.GetRange(Globals.CswrMaxPrintableSingleSheetRows, racSuppSheetItems.Count - Globals.CswrMaxPrintableSingleSheetRows);
          repPlayersRightSide.DataBind();
        }
      }
      else
      {
        panRightColumn.Visible = false;
        panLeftColumn.CssClass = "driverOneColumnContainer";
        repPlayersLeftSide.DataSource = racSuppSheetItems;
        repPlayersLeftSide.DataBind();
      }
    }

    /// <summary>
    /// This event will bind a 'player', or currently just football players
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void repPlayers_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        Label labRank = (Label)e.Item.FindControl("labRank");
        Label labPlayerName = (Label)e.Item.FindControl("labPlayerName");
        Label labNote = (Label)e.Item.FindControl("labNote");
        Label labSuppRanking = (Label)e.Item.FindControl("labSuppRanking");
        Label labTeam = (Label)e.Item.FindControl("labTeam");
        Label labByeWeek = (Label)e.Item.FindControl("labByeWeek");
        HtmlControl parContainer = (HtmlControl)e.Item.FindControl("parContainer");
        Image imaPlayerCheckbox = (Image)e.Item.FindControl("imaPlayerCheckbox");
        Image imaSleeper = (Image)e.Item.FindControl("imaSleeper");
        Image imaBust = (Image)e.Item.FindControl("imaBust");
        Player boundPlayer = new Player();

        switch (_rankType)
        {
          case CSWRRankingType.CSWRRank:

            SupplementalSheetItem boundSuppSheetItem = (SupplementalSheetItem)e.Item.DataItem;
            boundPlayer = boundSuppSheetItem.Player;

            // tags
            imaSleeper.Visible = (bool)boundSuppSheetItem.MappedProperties[SSIProperty.Sleeper.ToString()];
            if (imaSleeper.Visible)
            {
              imaSleeper.ToolTip = boundPlayer.Position.Name + " " + boundPlayer.FullName + " has been tagged as a sleeper candidate";
            }

            imaBust.Visible = (bool)boundSuppSheetItem.MappedProperties[SSIProperty.Bust.ToString()];
            if (imaBust.Visible)
            {
              imaBust.ToolTip = boundPlayer.Position.Name + " " + boundPlayer.FullName + " has been tagged as a bust candidate";
            }

            // Determine Supplemental Ranking
            Player suppPlayer = _statSortedPlayers.Find(x => x.PlayerID == boundPlayer.PlayerID);
            if (suppPlayer != null)
            {
              int playerRanking = _statSortedPlayers.IndexOf(suppPlayer) + 1;
              labSuppRanking.Text = "(" + playerRanking.ToString() + ")";
            }
            else
            {
              labSuppRanking.Text = "n/a";
            }

            break;

          case CSWRRankingType.PlayerStat:

            SportSeasonPlayerSeasonStat boundPlayerSeasonStatItem = (SportSeasonPlayerSeasonStat)e.Item.DataItem;
            boundPlayer = boundPlayerSeasonStatItem.Player;
            break;
        }

        // Determine the Player name
        if (boundPlayer.PositionCode != "DF")
        {
          labPlayerName.Text = boundPlayer.FullName;
          labTeam.Text = boundPlayer.Team.Abbreviation;
        }
        else
        {
          labPlayerName.Text = boundPlayer.FirstName;
        }

        // bye week
        if (Helpers.ByeWeeksLoaded())
        {
          ByeWeek teamByeWeek = ByeWeek.GetByeWeek(_seasonCode, this.SportCode, boundPlayer.TeamCode);
          labByeWeek.Text = "[" + teamByeWeek.Bye.ToString() + "]";
        }

        // style the rows
        _playerCounter++;
        labRank.Text = _playerCounter.ToString();
        if (_playerCounter % 2 == 1)
        {
          parContainer.Attributes.Add("class", "alternatingItem");
        }

        // add tooltips
        imaPlayerCheckbox.ToolTip = "Check to indicate that " + boundPlayer.Position.Name.ToLower() + " " + boundPlayer.FullName + " has been selected";

      }
    }

    /// <summary>
    /// This event will bind a 'driver', or currently just nascar drivers
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void repDrivers_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        SupplementalSheetItem boundItem = (SupplementalSheetItem)e.Item.DataItem;

        Label labRank = (Label)e.Item.FindControl("labRank");
        Label labPlayerName = (Label)e.Item.FindControl("labPlayerName");
        Label labNote = (Label)e.Item.FindControl("labNote");
        Label labSuppRanking = (Label)e.Item.FindControl("labSuppRanking");
        Label labTeam = (Label)e.Item.FindControl("labTeam");
        Label labByeWeek = (Label)e.Item.FindControl("labByeWeek");
        Image imaPlayerCheckbox = (Image)e.Item.FindControl("imaPlayerCheckbox");

        HtmlControl parContainer = (HtmlControl)e.Item.FindControl("parContainer");

        Image imaSleeper = (Image)e.Item.FindControl("imaSleeper");
        Image imaBust = (Image)e.Item.FindControl("imaBust");

        // driver name
        if (boundItem.Player.PositionCode != "DF")
        {
          labPlayerName.Text = boundItem.Player.FullName;
        }
        else
        {
          labPlayerName.Text = boundItem.Player.FirstName;
        }

  
        // determine the Supplemental rank for this player
        int i = 0;
        bool playerFound = false;
        foreach (Player currentPlayer in _statSortedPlayers)
        {
          i++;
          if (currentPlayer.PlayerID == boundItem.PlayerID)
          {
            playerFound = true;
            break;
          }
        }
        if (playerFound)
        {
          labSuppRanking.Text = "(" + i.ToString() + ")";
        }
        else
        {
          labSuppRanking.Text = "n/a";
        }

        // style the rows
        _playerCounter++;
        labRank.Text = _playerCounter.ToString();
        if (_playerCounter % 2 == 1)
        {
          parContainer.Attributes.Add("class", "alternatingItem");
        }

        // add tooltips
        imaPlayerCheckbox.ToolTip = "Check to indicate that driver " + boundItem.Player.FullName + " has been selected";

      }
    }



    /// <summary>
    /// Build the textual summary which will appear at the top of each free printable sheet
    /// </summary>
    private void LoadSummary()
    {
      // build a reference to the supplemental sheet being referenced
      string currentPosition = Position.GetPosition(this.PositionCode).Name.ToString();
      int supplementalSourceID = SupplementalSource.GetSupplementalSource("CSWR").SupplementalSourceID;
      _suppSheet = SupplementalSheet.GetSupplementalSheet(_seasonCode, supplementalSourceID, this.SportCode, this.PositionCode);

      if ( (_rankType == CSWRRankingType.PlayerStat) && (this.SportCode == FOO.FOOString) )
      {
        spSuppRanking.Visible = false;
      }

      // various position stubs
      litPosition.Text = currentPosition.ToLower() + "s";
      litPosition3.Text = "My " + currentPosition + "s";
      hlPositionalRankings.Text = currentPosition.ToLower() + " rankings";

      // checkbox
      imaLegendCheckbox.AlternateText = "Indicate a " + currentPosition.ToLower() + " has been selected.";

      // season
      string currentSeason = SportSeason.GetCurrentSportSeason(this.SportCode).SeasonCode;
      labSportSeason.Text = currentSeason;

      // here we assume that the stats for a particular supplemental sheet are from the previous year, not necessarily
      // the case with user cheat sheets
      int suppSheetStatSeason = int.Parse(_suppSheet.SeasonCode) - 1;
      litStatSeason.Text = suppSheetStatSeason.ToString();

      // sport
      litSport.Text = Sport.GetSport(this.SportCode).SportName.ToLower();
      hlLandingPage.Text = Sport.GetSport(this.SportCode).SportName.ToLower() + " cheat sheets";

      switch (this.SportCode)
      {
        case "FOO":
          // legend player type
          litPlayerType.Text = "Player";
          // header
          litHeaderStub.Text = currentPosition + "s";
          // point to landing page
          hlLandingPage.NavigateUrl = "~/fantasy-football/nfl/create/custom-sheet.aspx";
          // league abbreviation
          labSportLeagueAbbreviation.Text = Sport.GetSport(this.SportCode).LeagueAbbreviation;
          // positional rankings
          switch (this.PositionCode)
          {
            case "QB":
              hlPositionalRankings.NavigateUrl = "~/fantasy-football/nfl/free/rankings/offense/quarterbacks.aspx";
              break;
            case "RB":
              hlPositionalRankings.NavigateUrl = "~/fantasy-football/nfl/free/rankings/offense/running-backs.aspx";
              break;
            case "WR":
              hlPositionalRankings.NavigateUrl = "~/fantasy-football/nfl/free/rankings/offense/wide-receivers.aspx";
              break;
            case "TE":
              hlPositionalRankings.NavigateUrl = "~/fantasy-football/nfl/free/rankings/offense/tight-ends.aspx";
              break;
            case "K":
              hlPositionalRankings.NavigateUrl = "~/fantasy-football/nfl/free/rankings/offense/kickers.aspx";
              break;
            case "DF":
              hlPositionalRankings.NavigateUrl = "~/fantasy-football/nfl/free/rankings/offense/defenses.aspx";
              break;
          }
          break;
        case "RAC":
          // legend team
          labLegendTeam.Visible = false;
          // legend bye
          labLegendBye.Visible = false;
          // legend player type
          litPlayerType.Text = "Driver";
          // header
          litHeaderStub.Text = "NASCAR Racing";
          // league abbreviation
          labSportLeagueAbbreviation.Text = "NASCAR";
          // point to landing page
          hlLandingPage.NavigateUrl = "~/fantasy-racing/nascar/create/custom-sheet.aspx";
          // positional rankings
          hlPositionalRankings.NavigateUrl = "~/fantasy-racing/nascar/free/rankings/drivers.aspx";
          labFootballSuppDescription.Visible = false;
          break;
      }
    }

  }
}