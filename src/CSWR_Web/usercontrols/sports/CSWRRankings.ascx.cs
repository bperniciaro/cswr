using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class CSWRRankings : System.Web.UI.UserControl
  {

    /// <summary>
    ///  Allows containers to specify the position code to consider in the rankings
    /// </summary>
    public string PositionCode { get; set; }

    /// <summary>
    /// Allows containers to specify the sport code to consider in the rankings
    /// </summary>
    public string SportCode { get; set; }

    /// <summary>
    /// Allows containers to specify the season to consider in the rankings
    /// </summary>
    public string SeasonCode { get; set; }

    /// <summary>
    /// Allows containers to specify the rank type (normal RANKing or ADP)
    /// </summary>
    public CSWRRankingType RankType { get; set; }


    private string _positionName;
    private int _playerRankCounter = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        _positionName = Position.GetPosition(this.PositionCode).Name;

        // Build the link navigation for all positions, if necessary
        ConfigureNavigation();

        // Build a link to the associated printable sheet
        BuildPrintSheetLink();

        // Loads the heading on the page which contains the positional abbreviation
        //LoadSubHeading();

        // Load the call to action
        LoadCallToAction();

        // show or hide advertisements based on global settings
        panAdContainer.Visible = Globals.CSWRSettings.EnableAdvertisements;
      }

      BindRelevantPlayers();

    }


    public void BindRelevantPlayers()
    {
      switch (this.RankType)
      {
        case CSWRRankingType.CSWRRank:
          BindCSWRItems();
          break;
        case CSWRRankingType.PlayerStat:
          BindPlayerRankingItems();
          break;
        case CSWRRankingType.ADP:
          BindADPs();
          break;
      }
    }


    private void BindADPs()
    {
      int maxItemsToShow = Helpers.GetMaxRankPlayersConsideredBySportPosition(this.SportCode, this.PositionCode);
      List<ADPPlayerLog> playerAdpLogs = ADPPlayerLog.GetADPPlayerLogs(this.SportCode, this.SeasonCode, this.PositionCode).Take(maxItemsToShow).ToList();
     
      repCSWRRankings.DataSource = playerAdpLogs;
      repCSWRRankings.DataBind();

      if (playerAdpLogs.Count == 0)
      {
        panSuppItemListing.Visible = false;
        panNoRecords.Visible = true;
        labNoRecords.Text = "ADP is has not yet been calculated";
      }
      else
      {
        panSuppItemListing.Visible = true;
        panNoRecords.Visible = false;
      }
    }

    /// <summary>
    /// Binds the players based on CSWR supplemental rankings
    /// </summary>
    private void BindCSWRItems()
    {
      SupplementalSource targetSource = SupplementalSource.GetSupplementalSource("CSWR");
      SupplementalSheet targetSheet = SupplementalSheet.GetSupplementalSheet(this.SeasonCode, targetSource.SupplementalSourceID, this.SportCode, this.PositionCode);

      int maxItemsToShow = Helpers.GetMaxRankPlayersConsideredBySportPosition(this.SportCode, this.PositionCode);
      List<SupplementalSheetItem> cswrRankings = SupplementalSheetItem.GetSupplementalSheetItems(targetSheet.SupplementalSheetID).Take(maxItemsToShow).ToList();

      repCSWRRankings.DataSource = cswrRankings;
      repCSWRRankings.DataBind();

    }


    /// <summary>
    /// Binds the players based on CSWR supplemental rankings
    /// </summary>
    private void BindPlayerRankingItems()
    {
      int maxItemsToShow = Helpers.GetMaxRankPlayersConsideredBySportPosition(this.SportCode, this.PositionCode);
      List<SportSeasonPlayerSeasonStat> playerRankings = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats(this.SportCode, this.SeasonCode, this.PositionCode, "TFP").Take(maxItemsToShow).ToList();

      repCSWRRankings.DataSource = playerRankings;
      repCSWRRankings.DataBind();
    }


    /// <summary>
    /// Builds a link to the printable sheet
    /// </summary>
    private void BuildPrintSheetLink()
    {
      //switch (SessionHandler.CurrentSportCode)
      //{
      //  case "FOO":
      //    if (this.RankType == CSWRRankingType.ADP)
      //    {
      //      hlPrintableSheetLink.Visible = false;
      //    }
      //    else
      //    {
      //      litPosition.Text = _positionName + "s";
      //      hlPrintableSheetLink.NavigateUrl = "~/fantasy-football/nfl/free/printable/offense/" + _positionName.ToLower().Replace(' ', '-') + "s-cheat-sheet.aspx";
      //    }
      //    break;
      //  case "RAC":
      //    if (this.RankType == CSWRRankingType.ADP)
      //    {
      //      hlPrintableSheetLink.Visible = false;
      //    }
      //    else
      //    {
      //      litPosition.Text = _positionName + "s";
      //      hlPrintableSheetLink.NavigateUrl = "~/fantasy-racing/nascar/free/printable/" + _positionName.ToLower().Replace(' ', '-') + "s-cheat-sheet.aspx";
      //    }
      //    break;
      //}
    }


    protected void repCSWRRankings_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        CSWRFOORankingItemTemplate rankingFOOItemTemplate = (CSWRFOORankingItemTemplate)e.Item.FindControl("pritFOORankingItemTemplate");
        CSWRRACRankingItemTemplate rankingRACItemTemplate = (CSWRRACRankingItemTemplate)e.Item.FindControl("pritRACRankingItemTemplate");
        Player boundPlayer = new Player();

        string rank = String.Empty;
        bool isSleeper = false;
        bool isBust = false;

        switch (this.RankType)
        {
          case CSWRRankingType.CSWRRank:
            SupplementalSheetItem boundItem = (SupplementalSheetItem)e.Item.DataItem;
            boundPlayer = boundItem.Player;
            rank = boundItem.Seqno.ToString();
            if (this.SportCode == FOO.FOOString)
            {
              isSleeper = (bool)boundItem.MappedProperties[CSIProperty.Sleeper.ToString()];
              isBust = (bool)boundItem.MappedProperties[CSIProperty.Bust.ToString()];
            }
            break;
          case CSWRRankingType.PlayerStat:
            SportSeasonPlayerSeasonStat boundPlayerRanking = (SportSeasonPlayerSeasonStat)e.Item.DataItem;
            boundPlayer = boundPlayerRanking.Player;
            rank = _playerRankCounter.ToString();
            _playerRankCounter++;
            
            break;
          case CSWRRankingType.ADP:
            ADPPlayerLog boundPlayerLog = (ADPPlayerLog)e.Item.DataItem;
            boundPlayer = boundPlayerLog.Player;
            rank = boundPlayerLog.ADP.ToString("0.0");
            break;
        }

        switch (this.SportCode)
        {
          case "FOO":
            rankingFOOItemTemplate.PlayerID = boundPlayer.PlayerID;
            rankingFOOItemTemplate.RankLabel = rank;
            rankingFOOItemTemplate.IsSleeper = isSleeper;
            rankingFOOItemTemplate.IsBust = isBust;
            rankingFOOItemTemplate.LoadTemplate();
            // hide the racing template
            rankingRACItemTemplate.Visible = false;
            break;
          case "RAC":
            rankingRACItemTemplate.PlayerID = boundPlayer.PlayerID;
            rankingRACItemTemplate.RankLabel = rank;
            rankingRACItemTemplate.LoadTemplate();
            // hide the football template
            rankingFOOItemTemplate.Visible = false;
            break;
        }
      }
    }


    //private void LoadSubHeading()
    //{
    //  // build the 'sub-heading' which contains 'Ratings' vs 'Rankings'
    //  if ( (this.RankType == CSWRRankingType.CSWRRank) || (this.RankType == CSWRRankingType.PlayerStat)  )
    //  {
    //    if (this.SportCode == FOO.FOOString)
    //    {
    //      litSubHeading.Text = this.SeasonCode + " " + this.PositionCode + " Rankings";
    //    }
    //    else
    //    {
    //      litSubHeading.Text = this.SeasonCode + " Driver Rankings";
    //    }
    //  }
    //  else
    //  {
    //    if (this.SportCode == FOO.FOOString)
    //    {
    //      litSubHeading.Text = this.SeasonCode + " " + this.PositionCode + " ADP";
    //    }
    //    else
    //    {
    //      litSubHeading.Text = this.SeasonCode + " Driver ADP";
    //    }
    //  }
    //}
    
    private void LoadCallToAction()
    {
      // only tell people to create a new sheet if 
      if (this.Page.User.Identity.IsAuthenticated)
      {
        panCall2Action.Visible = false;
      }
      else
      {
        if (this.SportCode == FOO.FOOString)
        {
          hlCall2Action.Text = "fantasy football cheat sheet";
          hlCall2Action.NavigateUrl = "~/fantasy-football/nfl/create/custom-sheet.aspx";
        }
        else
        {
          hlCall2Action.Text = "fantasy racing cheat sheet";
          hlCall2Action.NavigateUrl = "~/fantasy-racing/nascar/create/custom-sheet.aspx";
        }
      }
    }


    private void ConfigureNavigation()
    {

      if (SessionHandler.CurrentSportCode != SupportedSport.RAC.ToString())
      {
        // configure navigation
        switch (this.PositionCode)
        {
          case "QB":
            prnRankingsNavigation.CurrentPosition = FOOPositionsOffense.QB;
            break;
          case "RB":
            prnRankingsNavigation.CurrentPosition = FOOPositionsOffense.RB;
            break;
          case "WR":
            prnRankingsNavigation.CurrentPosition = FOOPositionsOffense.WR;
            break;
          case "TE":
            prnRankingsNavigation.CurrentPosition = FOOPositionsOffense.TE;
            break;
          case "K":
            prnRankingsNavigation.CurrentPosition = FOOPositionsOffense.K;
            break;
          case "DF":
            prnRankingsNavigation.CurrentPosition = FOOPositionsOffense.DF;
            break;
        }

        prnRankingsNavigation.RankingType = this.RankType;

      }
      else
      {
        prnRankingsNavigation.Visible = false;
      }
    }


  }
}