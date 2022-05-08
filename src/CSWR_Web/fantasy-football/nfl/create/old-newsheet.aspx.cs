using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
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
        InitializeControls();
      }
      // load JQuery methods
      LoadJQueryValidation();
    }

    /// <summary>
    /// Since we're using an UpdatePanel, this script must be re-loaded after each callback
    /// </summary>
    private void LoadJQueryValidation()
    {
      string checkboxValidatorScript = @"$('.submitButton').click(function () {
                                        var checked = $('.positionBoxes input:checked').length > 0;
                                        if (!checked) {
                                          alert('You must select at least one position.');
                                          return false;
                                        }
                                      });";

      ScriptManager.RegisterStartupScript(Page, Page.GetType(), "checkboxValidator", checkboxValidatorScript, true);

    }

    protected void Page_Init(object sender, EventArgs e)
    {
      // configure navigation
      if (!IsPostBack)
      {
        SessionHandler.CurrentSportCode = FOO.FOOString;
        scmlnNavigation.CheatSheetID = Profile.Football.LastFootballCheatSheetID;
        scmlnNavigation.CurrentStage = SheetCreationManageLevelNavigation.CreationStage.NEWSHEET;
      }
    }

    private void InitializeControls()
    {
      // initially hide PPR configuration
      trScoringConfiguration.Visible = false;

      // if the season hasn't started yet, there is no need to allow for stat season selection
      SportSeason currentSportSeason = SportSeason.GetCurrentSportSeason(FOO.FOOString);
      // load the controls with values from the current year
      rbTFP.Text = currentSportSeason.LastSeasonCode + " Total Fantasy Points";
      rbTFPP.Text = currentSportSeason.LastSeasonCode + " Total Fantasy Points";
      rbFPPG.Text = currentSportSeason.LastSeasonCode + " Fantasy Points Per Game";
      rbFPGP.Text = currentSportSeason.LastSeasonCode + " Fantasy Points Per Game";

      // positions
      cblPositions.DataSource = Position.GetPositions(FOO.FOOString);
      cblPositions.DataBind();
      //cblPositions.Items.FindByValue(FOOPositionsOffense.QB.ToString()).Selected = true;

      // when we start-out, assume multiple positions are not selected
      trTFPOnly.Visible = false;

      // load supp sheet sources should we need them
      LoadSuppSourceOptions();

      if (Helpers.IsMiddleOfSeason(FOO.FOOString))
      {
        // load the appropriate stat season options
        LoadStatSeasons();

        // make the row to select stat season visible
        trStatSeason.Visible = true;
        trTFPOnly.Visible = true;
        trSortTypes.Visible = false;
        // load the string for the forced 'sort by TFP' season
        litForcedTFPSeason.Text = currentSportSeason.SeasonCode;
      }
      else
      {
        trStatSeason.Visible = false;
        // determine whether we should show sort options
        DetermineSortOptionVisibility();
        // load the string for the forced 'sort by TFP' season
        litForcedTFPSeason.Text = currentSportSeason.LastSeasonCode;
      }


    }

    /// <summary>
    /// This method loads the available stat season options based on the state of the current season
    /// </summary>
    private void LoadStatSeasons()
    {
      // get the most seasons which are relevant based on what point we are in the current season
      List<SportSeason> relevantStatSeasons = SportSeason.GetSportStatSeasons(FOO.FOOString).OrderByDescending(x => x.SeasonCode).Take(2).ToList();
      if (relevantStatSeasons != null)
      {
        if (relevantStatSeasons.Count > 1)
        {
          ddlStatsSeasons.Visible = true;
          ddlStatsSeasons.DataSource = relevantStatSeasons;
          ddlStatsSeasons.DataBind();
        }
        else
        {
          ddlStatsSeasons.Visible = false;
        }
      }
    }


    /// <summary>
    /// This method loads the possible supplemental sources which a sheet can be based on.
    /// </summary>
    private void LoadSuppSourceOptions()
    {
      // configure CBSSports item value
      rblSortTypes.Items[0].Value = SupplementalSource.GetSupplementalSource("CSWR").SupplementalSourceID.ToString();
      // configure the CBSSports item value
      rblSortTypes.Items[1].Value = SupplementalSource.GetSupplementalSource("CBS").SupplementalSourceID.ToString();
    }


    /// <summary>
    /// When the sheet sort type is changed, take the appropriate action
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rblSortTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
      // based on the choice of sorting parameter, we show or hide the stat rows
      if (rblSortTypes.SelectedValue == "Stats")
      {
        if (rbStandardScoring.Checked)
        {
          trStandardScoringSortStat.Visible = true;
          trPPRSortStat.Visible = false;
        }
        else
        {
          trStandardScoringSortStat.Visible = false;
          trPPRSortStat.Visible = true;
        }
      }
      // else they chose a supplemental sheet
      else
      {
        // hide sort stats for both scoring configurations
        trStandardScoringSortStat.Visible = false;
        trPPRSortStat.Visible = false;
      }
      Thread.Sleep(350);
    }



    /// <summary>
    /// If a user changes the sheet position, configure the controls based on PPR-relevancy and
    /// the number of positions chosen
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cblPositions_SelectedIndexChanged(object sender, EventArgs e)
    {
      bool pprRelevant = false;
      Position firstPosition = new Position();
      
      // spin through all positions to determine if PPR stats are relevant
      foreach (ListItem positionItem in cblPositions.Items)
      {
        if (positionItem.Selected && Helpers.IsPPRRelevant(positionItem.Value))
        {
          pprRelevant = true;
        }
      }

      // if PPR stats are relevant, show the scoring configuration options
      if(pprRelevant)
      {
        trScoringConfiguration.Visible = true;
      }
      else
      {
        trScoringConfiguration.Visible = false;
        rbStandardScoring.Checked = true;
        rbPPRScoring.Checked = false;
      }


      if (!(Helpers.IsMiddleOfSeason(FOO.FOOString)))
      {
        // if multiple positions are selected, only allow for sorting by either
        // TFP or FPPG
        if (this.MultiplePositionsSelected)
        {
          trSortTypes.Visible = false;
          trStandardScoringSortStat.Visible = false;
          trTFPOnly.Visible = true;
        }
        // if only a single position is selected, let the user dictate the sorting options
        else
        {
          // if the user has chosen PPR scoring, then only allow total fantasy points as an option
          if (rbPPRScoring.Checked)
          {
            trSortTypes.Visible = false;
            trTFPOnly.Visible = true;
          }
          else
          {
            trSortTypes.Visible = true;
            trTFPOnly.Visible = false;
          }
        }
      }


      Thread.Sleep(350);
    }



    /// <summary>
    /// When the user clicks submit, we create the cheat sheet based on the specified parameters
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void butSubmit_Click(object sender, EventArgs e)
    {
      List<Position> cheatSheetPositions = new List<Position>();
      int newSheetID = 0;
      bool pprLeague = false;
      bool auctionDraft = false;

      // Get current season
      SportSeason currentSeason = SportSeason.GetCurrentSportSeason(FOO.FOOString);

      // Determine which statseason is relevant
      string statSeasonCode = String.Empty;
      if (trStatSeason.Visible == true)
      {
        statSeasonCode = ddlStatsSeasons.SelectedValue;
      }
      else
      {
        statSeasonCode = currentSeason.LastSeasonCode;
      }

      // Build a list of positions to include
      foreach (ListItem positionItem in cblPositions.Items)
      {
        if (positionItem.Selected)
        {
          Position selectedPosition = Position.GetPosition(positionItem.Value);
          cheatSheetPositions.Add(selectedPosition);
        }
      }

      // determine PPR relevancy
      if (trScoringConfiguration.Visible && rbPPRScoring.Checked)
      {
        pprLeague = true;
      }
      // determine auction status
      auctionDraft = rbAuction.Checked;

      // build mapped properties
      Dictionary<string, object> fooMappedProperties = new Dictionary<string, object>();
      fooMappedProperties[CSProperty.PPRLeague.ToString()] = pprLeague;
      fooMappedProperties[CSProperty.AuctionDraft.ToString()] = auctionDraft;

      newSheetID = CreateSheet(statSeasonCode, cheatSheetPositions, pprLeague, auctionDraft, fooMappedProperties);

      if (newSheetID != 0)
      {
        Response.Redirect("~/fantasy-football/nfl/create/custom-sheet.aspx?SheetID=" + newSheetID.ToString());
      }
      else
      {
        mbMessage.MessageType = MessageType.ERROR;
        mbMessage.Message = new System.Text.StringBuilder("There was a problem creating your sheet.  Please try again.");
        mbMessage.SetMessage();
        mbMessage.Visible = true;
      }
    }



    private int CreateSheet(string statSeasonCode, List<Position> cheatSheetPositions, bool pprLeague, bool auctionDraft,
                              Dictionary<string, object> fooMappedProperties)  
    {
      int newSheetID = 0;

      // If we're in the middle of a season, and the user selects to create a sheet based on the 
      // current season, then we want to create the sheet based on stats.  
      if (Helpers.IsMiddleOfSeason(FOO.FOOString))
      {
        newSheetID = CreateInSeasonSheet(statSeasonCode, cheatSheetPositions, pprLeague, auctionDraft,fooMappedProperties);
      }
      // Create a stat-based sheet
      else if ((rblSortTypes.SelectedValue == "Stats") || (rbPPRScoring.Checked) || (trPPRSortStat.Visible) || (this.MultiplePositionsSelected))
      {
        newSheetID = CreateStatBasedSheet(statSeasonCode, cheatSheetPositions, pprLeague, auctionDraft, fooMappedProperties);
      }
      // Here we sort by supplemental sheet
      else
      {
        newSheetID = CreateSuppBasedSheet(statSeasonCode, cheatSheetPositions, pprLeague, auctionDraft, fooMappedProperties);
      }

      return newSheetID;
    }


    protected int CreateSuppBasedSheet(string statSeasonCode, List<Position> cheatSheetPositions, bool pprLeague, bool auctionDraft,
                      Dictionary<string, object> fooMappedProperties)

    {
      int newSheetID = 0;
      newSheetID = CheatSheet.CreateCheatSheet(FOO.FOOString, tbSheetName.Text, statSeasonCode, cheatSheetPositions,
                                                Helpers.GetDefaultStatCodes(this.SingleChosenPosition.PositionCode),
                                                int.Parse(rblSortTypes.SelectedValue), fooMappedProperties);
      return newSheetID;
    }


    protected int CreateStatBasedSheet(string statSeasonCode, List<Position> cheatSheetPositions, bool pprLeague, bool auctionDraft,
                          Dictionary<string, object> fooMappedProperties)
    {
      int newSheetID = 0;
      string sortStat = String.Empty;

      if (this.MultiplePositionsSelected)
      {
        sortStat = "TFP";
      }
      else
      {
        // if the user is sorting by PPR Stats
        if (trPPRSortStat.Visible)
        {
          if (rbTFPP.Checked)
          {
            sortStat = "TFPP";
          }
          else
          {
            sortStat = "FPGP";
          }
        }
        // if the user is sorting by Standard Scoring Stats
        else
        {
          if (rbTFP.Checked)
          {
            sortStat = "TFP";
          }
          else
          {
            sortStat = "FPPG";
          }
        }
      }

      List<Stat> cheatSheetStats = new List<Stat>();
      if (!this.MultiplePositionsSelected)
      {
        cheatSheetStats = Helpers.GetDefaultStatCodes(this.SingleChosenPosition.PositionCode);
      }

      newSheetID = CheatSheet.CreateCheatSheet(FOO.FOOString, tbSheetName.Text, statSeasonCode, cheatSheetPositions,
                                                cheatSheetStats, sortStat, SortDir.DESC.ToString(), fooMappedProperties);
      return newSheetID;
    }



    protected int CreateInSeasonSheet(string statSeasonCode, List<Position> cheatSheetPositions, bool pprLeague, bool auctionDraft,
                              Dictionary<string, object> fooMappedProperties)
    {
      int newSheetID;
      string sortStat = String.Empty;


      // if scoring configuration is used (i.e. PPR is an option)
      if (trScoringConfiguration.Visible == true)
      {
        if (rbPPRScoring.Checked)
        {
          sortStat = "TFPP";
        }
        else
        {
          sortStat = "TFP";
        }
      }
      else
      {
        sortStat = "TFP";
      }

      List<Stat> cheatSheetStats = new List<Stat>();
      if (!this.MultiplePositionsSelected)
      {
        cheatSheetStats = Helpers.GetDefaultStatCodes(this.SingleChosenPosition.PositionCode);
      }
      newSheetID = CheatSheet.CreateCheatSheet(FOO.FOOString, tbSheetName.Text, statSeasonCode, cheatSheetPositions, cheatSheetStats,
                        sortStat, SortDir.DESC.ToString(), fooMappedProperties);
      return newSheetID;
    }


    /// <summary>
    /// If the user selects standard scoring
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rbStandardScoring_CheckedChanged(object sender, EventArgs e)
    {
      // if multiple positions are selected, we won't provide sorting options 
      if (this.MultiplePositionsSelected)
      {
        // hide stat sorting options
        trSortTypes.Visible = false;
        // hide TFP/FPPG for standard scoring
        trStandardScoringSortStat.Visible = false;
        // hide TFF/FPPG for PPR scoring
        trPPRSortStat.Visible = false;
        // show the row which only allows for total fantasy points
        trTFPOnly.Visible = true;
      }
      else
      {
        trPPRSortStat.Visible = false;
        trTFPOnly.Visible = false;
        trSortTypes.Visible = true;
      }

      Thread.Sleep(350);

    }


    protected void rbPPRScoring_CheckedChanged(object sender, EventArgs e)
    {
      trSortTypes.Visible = false;
      trTFPOnly.Visible = true;
      Thread.Sleep(350);
    }


    protected void ddlStatsSeasons_SelectedIndexChanged(object sender, EventArgs e)
    {

      if (!Helpers.IsMiddleOfSeason(FOO.FOOString))
      {
        DetermineSortOptionVisibility();
      }
      litForcedTFPSeason.Text = ddlStatsSeasons.SelectedValue;

    }


    private void DetermineSortOptionVisibility()
    {
      SportSeason currentSportSeason = SportSeason.GetCurrentSportSeason(FOO.FOOString);

      if (Helpers.IsMiddleOfSeason(FOO.FOOString) && (CurrentSeasonSelected()))
      {
        trSortTypes.Visible = false;
        trStandardScoringSortStat.Visible = false;
        // change sort options to default value (Use CSWR Rankings) so we don't have to maintain user selection
        rblSortTypes.Items[0].Selected = true;
        rblSortTypes.Items[1].Selected = false;
        rblSortTypes.Items[2].Selected = false;
      }
      else
      {
        trSortTypes.Visible = true;
      }
    }


    /// <summary>
    /// Indicates if the current football season is chosen
    /// </summary>
    /// <returns></returns>
    public bool CurrentSeasonSelected()
    {
      return FOO.CurrentSeason == ddlStatsSeasons.SelectedValue;
    }



    public bool MultiplePositionsSelected
    {
      get  
      {
        int positionCheckedCount = 0;
        foreach (ListItem liPosition in cblPositions.Items)
        {
          if (liPosition.Selected)
          {
            positionCheckedCount++;
          }
        }
        return positionCheckedCount > 1;
      }
    }

    public Position SingleChosenPosition
    {
      get
      {
        int positionCounter = 0;
        Position singlePosition = new Position();

        foreach (ListItem positionItem in cblPositions.Items)
        {
          if (positionItem.Selected)
          {
            positionCounter++;
            singlePosition = Position.GetPosition(positionItem.Value);
          }
        }
        if (positionCounter == 1)
        {
          return singlePosition;
        }
        else
        {
          return null;
        }

      }
    }

  }
}