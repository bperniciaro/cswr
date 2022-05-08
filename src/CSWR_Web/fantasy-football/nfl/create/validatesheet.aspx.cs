using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.Admin.Sheets
{
  public partial class ValidateCheatSheet : BasePage
  {

    private const int MAXRANKDIFFERENTIAL = 20;

    /// <summary>
    /// This property contains the ID of the cheat sheet being validated
    /// </summary>
    private int UserSheetID { get; set; }

    /// <summary>
    /// This property represents the range of players that we based our calculations on.  If the user's sheet item count is greater
    /// than the default CSWR sheet item count for that position, it will represent the user's sheet item count.  If the user's
    /// sheet item count is less than the default CSWR sheet item count for that position, then it will represent the default CSWR
    /// sheet item count.  The user's sheet count can diminsh as they remove players.
    /// </summary>
    private int ItemConsiderationCount { get; set; }

    // Properties for the cheat sheet we're validating
    private CheatSheet UserSheet { get; set; }
    List<CheatSheetItem> UserSheetItems { get; set; }

    // Properties for the CBS sheet we're comparing against
    private SupplementalSheet CSWRSuppSheet { get; set; }
    List<SupplementalSheetItem> CSWRSuppSheetItems { get; set; }

    // Properties for holding collections of problematic players
    private List<ProblemPlayer> PlayersRankedTooHigh { get; set; }
    private List<ProblemPlayer> PlayersRankedTooLow { get; set; }
    private List<ProblemPlayer> PlayersToRemove { get; set; }

    /// <summary>
    /// This number holds the differential that the user chose to use when validating their sheet
    /// </summary>
    private int RankDifferentialThreshold 
    {
      get
      {
        return (ViewState["RankDifferentialThreshold"] == null) ? 10 : (int)ViewState["RankDifferentialThreshold"];
      }
      set
      {
        ViewState["RankDifferentialThreshold"] = value;
      }
    }


    /// <summary>
    /// Page_Load method
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
      // show the user's rankings on each page load in case they changed their
      // rank differential preference.
      repSheetPlayers.Visible = true;
      headerYourRankings.Visible = true;
      bool sheetSourcesFound = false;

      // We'll load the supplemental sheets on every postback, even the AJAX requests
      if (ProcessQueryString())
      {
        scmlnNavigation.CheatSheetID = this.UserSheetID;
        sheetSourcesFound = LoadSourceSheets();
      }

      // We only perform this validation when the page initially loads
      if (!IsPostBack && sheetSourcesFound)
      {
        panStatus.Visible = false;

        LoadSourceControls();
        ValidateSheet();
        ConfirmSheetOwner();
      }

      Thread.Sleep(500);
    }


    private void ConfirmSheetOwner()
    {
      // as a webmaster I want to be able to view any sheet for for debugging purposes
      if (!this.User.IsInRole("Administrator"))
      {
        if (!this.UserSheet.ConfirmOwner(User.Identity.Name))
        {
          upValidationContainer.Visible = false;
          mbWrongUser.MessageType = MessageType.ERROR;
          mbWrongUser.Message = new StringBuilder("This is not your sheet!");
        }
      }

    }

    /// <summary>
    /// Build the dropdown list which loads the differential choice
    /// </summary>
    private void LoadSourceControls()
    {
      for (int i = 1; i < MAXRANKDIFFERENTIAL+1; i++)  
      {
        ddlDifferential.Items.Add(new ListItem(i.ToString(), i.ToString()));
      }
      ddlDifferential.SelectedValue = this.RankDifferentialThreshold.ToString();
      ddlDifferential.DataBind();
    }


    /// <summary>
    /// This is the method that we'll call if we find a sheet to validate
    /// </summary>
    private void ValidateSheet()
    {
      int problemPlayerCount = 0;

      // load reference data we'll need in all methods
      //LoadSourceSheets();
      // build the header which contains sheet information
      LoadHeaderData();
      // check for players who are ranked to high in the sheet
      problemPlayerCount += CheckRankedTooHigh();
      // check for players who are ranked too low in the sheet
      problemPlayerCount += CheckRankedTooLow();
      // check for players who aren't on the sheet but should be
      problemPlayerCount += CheckPlayersToAdd();
      // check for players who are on the sheet but shouldn't be
      problemPlayerCount += CheckPlayersToRemove();

      // load the right-side cheat sheet rankings
      if (problemPlayerCount > 0)
      {
        repSheetPlayers.DataSource = this.UserSheetItems;
        repSheetPlayers.DataBind();
      }
      else
      {
        //panStatus.Visible = true;
        labStatus.Text = "Your sheet has been completely validated.  We have no further suggestions.";
        panStatus.CssClass = "alert alert-success";
        panStatus.Visible = true;

        // hide the user's rankings
        repSheetPlayers.Visible = false;
        headerYourRankings.Visible = false;
      }

    }

    /// <summary>
    /// Pretty simple, loads the header.  But will probably need to be expanded
    /// </summary>
    private void LoadHeaderData()  
    {
      hlSheetName.NavigateUrl = "~/fantasy-football/nfl/create/custom-sheet.aspx?SheetID=" + this.UserSheetID.ToString();
      hlSheetName.Text = this.UserSheet.SheetName;
      labTotalPlayers.Text = this.UserSheetItems.Count.ToString();
    }

    /// <summary>
    /// This method loads both the user sheets and the reference supplemental sheets
    /// </summary>
    private bool LoadSourceSheets()
    {
      // get user sheet information
      this.UserSheet = CheatSheet.GetCheatSheet(this.UserSheetID);
      if (this.UserSheet == null)
      {
        new CSWRWebEvent("Error querying user's cheat sheet, cheatSheetID: " + this.UserSheetID.ToString(), null, null).Raise();

        labStatus.Text = "The requested sheet no longer exists";
        panStatus.Visible = true;
        panStatus.CssClass = "alert alert-error";
        return false;
      }
      this.UserSheetItems = CheatSheetItem.GetCheatSheetItems(this.UserSheetID);

      // get CSWR source
      SupplementalSource cswrSportsSource = SupplementalSource.GetSupplementalSource("CSWR");
      if (cswrSportsSource == null)
      {
        new CSWRWebEvent("Error querying CSWR Source, CSWR.", null, null).Raise();

        labStatus.Text = "Supplemental Source not found";
        panStatus.Visible = true;
        panStatus.CssClass = "alert alert-error";
        return false;
      }
      
      // get CSWR SuppSheet
      this.CSWRSuppSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode,
                                                  cswrSportsSource.SupplementalSourceID, "FOO", this.UserSheet.Positions[0].PositionCode);
      if (this.CSWRSuppSheet == null)
      {
        new CSWRWebEvent("Error querying CSWR's Supp Sheet, supplementalSheetID: " + this.CSWRSuppSheet.SupplementalSheetID, null, null).Raise();

        labStatus.Text = "Supplemental Sheet not found";
        panStatus.Visible = true;
        panStatus.CssClass = "alert alert-error";
        return false;
      }
      
      this.CSWRSuppSheetItems = SupplementalSheetItem.GetSupplementalSheetItems(this.CSWRSuppSheet.SupplementalSheetID);

      // we're only considering one position here, but we have to pass as an array
      List<string> sheetPositions = new List<string> { this.UserSheet.Positions[0].PositionCode };

      int defaultCSWRItemCountByPosition = Globals.GetDefaultPlayersPerSheet(sheetPositions);
      if (this.UserSheetItems.Count < defaultCSWRItemCountByPosition)
      {
        this.ItemConsiderationCount = defaultCSWRItemCountByPosition;
      }
      else
      {
        this.ItemConsiderationCount = this.UserSheetItems.Count;
      }

      return true;
    }


    /// <summary>
    /// This method parses the query string to see if there is an ID of a cheat sheet to validate
    /// </summary>
    /// <returns></returns>
    private bool ProcessQueryString()
    {
      int cheatSheetID = 0;
      if (Request.QueryString["SheetID"] != null)
      {
        if (int.TryParse(Request.QueryString["SheetID"], out cheatSheetID))
        {
          this.UserSheetID = cheatSheetID;
        }
        else
        {
          labStatus.Text = "Querystring ID couldn't be parsed.";
          panStatus.Visible = true;
          panStatus.CssClass = "alert alert-error";
          return false;
        }
      }
      else
      {
        labStatus.Text = "Querystring value not found.";
        panStatus.Visible = true;
        panStatus.CssClass = "alert alert-error";
        return false;
      }
      return true;
    }

    /// <summary>
    /// Identify the players who are ranked too high
    /// </summary>
    /// <returns>
    /// The count of players who are ranked too high
    /// </returns>
    private int CheckRankedTooHigh()
    {
      List<ProblemPlayer> playersRankedTooHigh = new List<ProblemPlayer>();
      //List<ProblemPlayer> playersNotRankedByCSWR = new List<ProblemPlayer>();

      // isolate players whose rank differential is greater than the defined threshold
      foreach (CheatSheetItem currentUserItem in this.UserSheetItems)
      {

        // find the corresponding item in the CSWR sheet we're comparing against        
        SupplementalSheetItem cswrSportsItem = this.CSWRSuppSheetItems.Find(delegate(SupplementalSheetItem targetItem) { return (currentUserItem.Player.PlayerID == targetItem.Player.PlayerID); });
        if (cswrSportsItem != null)
        {
          // we only want to consider players that differ by the configurable rank differential threshold
          if (currentUserItem.Seqno <= (cswrSportsItem.Seqno - this.RankDifferentialThreshold)) 
          {
            // we only want to consider players that aren't handled by the 'consider removing' logic, so we need to limit our range
            if (cswrSportsItem.Seqno <= (this.ItemConsiderationCount + this.RankDifferentialThreshold))
            {
              // now we've isolated out players, so add them to the list
              int pointDifferential = cswrSportsItem.Seqno - currentUserItem.Seqno;
              playersRankedTooHigh.Add(new ProblemPlayer(currentUserItem.PlayerID, currentUserItem.FullNameLastFirst, currentUserItem.Player.Team.Abbreviation,
                                         currentUserItem.Player.Team.Mascot, currentUserItem.Seqno, cswrSportsItem.Seqno, pointDifferential));
            }
          }
        }
        // if the user sheet player isn't found in the CSWR Sports Sheet, then they should be processed by
        // the 'consider removing' logic
        else { }

      }

      // sort and combine two lists
      playersRankedTooHigh.Sort(delegate(ProblemPlayer player1, ProblemPlayer player2) { return (player2.RankDifferential.CompareTo(player1.RankDifferential)); });
      //playersNotRankedByCBS.Sort(delegate(ProblemPlayer player1, ProblemPlayer player2) { return (player2.RankDifferential.CompareTo(player1.RankDifferential)); });
      //playersRankedTooHigh.AddRange(playersNotRankedByCBS);

      // hide the header if the master collection is empty
      if (playersRankedTooHigh.Count == 0)
      {
        headerRankedTooHigh.Visible = false;
      }
      else
      {
        headerRankedTooHigh.Visible = true;
      }
      
      // store the collection to be used by the right-side player rankings
      this.PlayersRankedTooHigh = playersRankedTooHigh;

      // bind the grid
      gvRankedTooHigh.DataSource = playersRankedTooHigh;
      gvRankedTooHigh.DataBind();

      return playersRankedTooHigh.Count;
    }


    /// <summary>
    /// Identify the players who are ranked too low
    /// </summary>
    /// <returns>
    /// The count of players who are ranked too low
    /// </returns>
    private int CheckRankedTooLow()
    {
      List<ProblemPlayer> playersRankedTooLow = new List<ProblemPlayer>();

      // look through each item in the player's sheet
      foreach (CheatSheetItem currentUserSheetItem in this.UserSheetItems)
      {
        // make sure each player sheet is ranked by CSWR    
        SupplementalSheetItem cswrSportsItem = this.CSWRSuppSheetItems.Find(delegate(SupplementalSheetItem targetItem) { return (currentUserSheetItem.Player.PlayerID == targetItem.Player.PlayerID); });
        if (cswrSportsItem != null)
        {
          if (cswrSportsItem.Seqno <= (currentUserSheetItem.Seqno - this.RankDifferentialThreshold) )
          {
            int pointDifferential = currentUserSheetItem.Seqno - cswrSportsItem.Seqno;
            playersRankedTooLow.Add(new ProblemPlayer(currentUserSheetItem.PlayerID, currentUserSheetItem.FullNameLastFirst, currentUserSheetItem.Player.Team.Abbreviation,
                                      currentUserSheetItem.Player.Team.Mascot, currentUserSheetItem.Seqno, cswrSportsItem.Seqno, pointDifferential));
          }
        }
      }

      // if the list of players is empty hide the header
      if (playersRankedTooLow.Count == 0)
      {
        headerRankedTooLow.Visible = false;
      }
      else
      {
        headerRankedTooLow.Visible = true;
      }
      
      // sort players so the higher rank differentials are listed first
      playersRankedTooLow.Sort(delegate(ProblemPlayer player1, ProblemPlayer player2) { return (player2.RankDifferential.CompareTo(player1.RankDifferential)); });

      // store the collection to be used by the cheat sheet list on right
      this.PlayersRankedTooLow = playersRankedTooLow;

      // bind the grid
      gvRankedTooLow.DataSource = playersRankedTooLow;
      gvRankedTooLow.DataBind();

      return playersRankedTooLow.Count;
    }


    /// <summary>
    /// Identify the players who could be added to the user's sheet
    /// </summary>
    /// <returns>
    /// Identify the players who could be added to the user's sheet
    /// </returns>
    private int CheckPlayersToAdd()
    {
      List<ProblemPlayer> playerAdditionCandidates = new List<ProblemPlayer>();

      // get the size of the User sheet
      //int userSheetItemCount = this.UserSheetItems.Count();

      // look through the CSWR sheet for any players that aren't in the user's sheet
      foreach (SupplementalSheetItem currentCSWRSportsItem in this.CSWRSuppSheetItems)
      {
        // only consider items in CSWR sheet that are within the range of the number of items in the item consideration count, otherwise
        // the user probably isn't interested in them
        if (currentCSWRSportsItem.Seqno <= this.ItemConsiderationCount)
        {
          // look for the CSWR player int he user's sheet, if he isn't found we'll add him to the list    
          CheatSheetItem userItem = this.UserSheetItems.Find(delegate(CheatSheetItem targetItem) { return (currentCSWRSportsItem.Player.PlayerID == targetItem.Player.PlayerID); });
          // if the CSWR Sports player isn't found in the specified range in the user sheet, they should consider adding them so put them in the list
          if (userItem == null)  
          {
            playerAdditionCandidates.Add(new ProblemPlayer(currentCSWRSportsItem.PlayerID, currentCSWRSportsItem.FullNameLastFirst, currentCSWRSportsItem.Player.Team.Abbreviation,
                                              currentCSWRSportsItem.Player.Team.Mascot, 0, currentCSWRSportsItem.Seqno, 0));
          }
        }

      }

      // if the source list is empty, hide the header
      if (playerAdditionCandidates.Count == 0)
      {
        headerPlayersToAdd.Visible = false;
      }
      else
      {
        headerPlayersToAdd.Visible = true;
      }

      // bind the add candicates, order by CSWR ascending
      playerAdditionCandidates.Sort(delegate(ProblemPlayer player1, ProblemPlayer player2) { return (player1.CSWRRank.CompareTo(player2.CSWRRank)); });
      gvPlayersToAdd.DataSource = playerAdditionCandidates;
      gvPlayersToAdd.DataBind();

      return playerAdditionCandidates.Count;
    }


    /// <summary>
    /// Identify players the user may want to remove from their sheet
    /// </summary>
    /// <returns></returns>
    private int CheckPlayersToRemove()
    {
      List<ProblemPlayer> playerRemovalCandidates = new List<ProblemPlayer>();

      // look through the user sheet for any players that aren't in the CSWR sheet, or players in the CSWR sheet that are ranked
      // far below the users ranking
      foreach (CheatSheetItem currentUserSheetItem in this.UserSheetItems)
      {
        // make sure each player sheet is ranked by CSWR    
        SupplementalSheetItem cswrSportsItem = this.CSWRSuppSheetItems.Find(delegate(SupplementalSheetItem targetItem) { return (currentUserSheetItem.Player.PlayerID == targetItem.Player.PlayerID); });
        // if the item wasn't found, then the player may be on IR or retired, the user should probably remove them
        if (cswrSportsItem == null)
        {
          playerRemovalCandidates.Add(new ProblemPlayer(currentUserSheetItem.PlayerID, currentUserSheetItem.FullNameLastFirst, currentUserSheetItem.Player.Team.Abbreviation,
                                    currentUserSheetItem.Player.Team.Mascot, currentUserSheetItem.Seqno, 0, 0));
        }
        else 
        {
          if (cswrSportsItem.Seqno >= (this.ItemConsiderationCount + this.RankDifferentialThreshold))
          {
            int pointDifferential = cswrSportsItem.Seqno + currentUserSheetItem.Seqno;
            playerRemovalCandidates.Add(new ProblemPlayer(currentUserSheetItem.PlayerID, currentUserSheetItem.FullNameLastFirst, currentUserSheetItem.Player.Team.Abbreviation,
                                      currentUserSheetItem.Player.Team.Mascot, currentUserSheetItem.Seqno, cswrSportsItem.Seqno, pointDifferential));
          }
        }
      }

      // if the source list is empty, hide the header
      if (playerRemovalCandidates.Count == 0)
      {
        headerPlayersToRemove.Visible = false;
      }
      else
      {
        headerPlayersToRemove.Visible = true;
      }

      playerRemovalCandidates.Sort(delegate(ProblemPlayer player1, ProblemPlayer player2) { return (player1.UserRank.CompareTo(player2.UserRank)); });

      // store these players so we can use them in the player order
      this.PlayersToRemove = playerRemovalCandidates;
      
      gvPlayersToRemove.DataSource = playerRemovalCandidates;
      gvPlayersToRemove.DataBind();

      return playerRemovalCandidates.Count;
    }
  




    
    protected void gvRankedTooHigh_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      // if we're binding to a data row, manipulate the controls within
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        // get references to the appropriate controls
        ProblemPlayer boundPlayer = (ProblemPlayer)e.Row.DataItem;

        Label labFullNameLastFirst = (Label)e.Row.FindControl("labFullNameLastFirst");
        Label labTeamAbbreviation = (Label)e.Row.FindControl("labTeamAbbreviation");
        Label labUserRank = (Label)e.Row.FindControl("labUserRank");
        Label labCSWRRank = (Label)e.Row.FindControl("labCSWRRank");
        Label labRankDiff = (Label)e.Row.FindControl("labRankDiff");
        Button butDemotePlayer = (Button)e.Row.FindControl("butDemotePlayer");
        HyperLink hlResearchPlayer = (HyperLink)e.Row.FindControl("hlResearchPlayer");

        // if this is a defensive player we don't want the mascot name in search, so we substitute 'defense'
        if (this.UserSheet.Positions[0].PositionCode == "DF")
        {
          hlResearchPlayer.NavigateUrl = "https://www.google.com/search?hl=en&gl=us&tbm=nws&btnmeta_news_search=1&q=" + boundPlayer.FullNameLastFirst + " Defense";
        }
        else
        {
          hlResearchPlayer.NavigateUrl = "https://www.google.com/search?hl=en&gl=us&tbm=nws&btnmeta_news_search=1&q=" + boundPlayer.FullName + " " + boundPlayer.TeamMascot;
        }
        hlResearchPlayer.ToolTip = "Click to view the latest news about " + boundPlayer.FullNameLastFirst + ".";
        
        labFullNameLastFirst.Text = boundPlayer.FullNameLastFirst;
        labTeamAbbreviation.Text = "(" + boundPlayer.TeamAbbreviation + ")";
        labUserRank.Text = boundPlayer.UserRank.ToString();

        // since we're only offering demotion, we only need to know the oldindex and the new index
        int oldIndex = boundPlayer.UserRank - 1;
        int newIndex = 0;

        if (boundPlayer.CSWRRank > this.UserSheetItems.Count)
        {
          newIndex = this.UserSheetItems.Count-1;
          int newRank = newIndex + 1;
          butDemotePlayer.Text = "Demote to " + newRank.ToString();
          butDemotePlayer.ToolTip = "Click to demote this player to position " + newRank.ToString() + " in your sheet.";
        }
        else
        {
          newIndex = boundPlayer.CSWRRank - 1;
          butDemotePlayer.Text = "Demote to " + boundPlayer.CSWRRank.ToString();
          butDemotePlayer.ToolTip = "Click to demote this player to position " + boundPlayer.CSWRRank.ToString() + " in your sheet.";
        }

        // if we're demoting a player we need to know the playerID, the old index (zero-based), and the new index (zero-based)
        butDemotePlayer.CommandArgument = boundPlayer.PlayerID.ToString() + "-" + oldIndex + "-" + newIndex;

        // configure labels based on if we find the lpayer
        labCSWRRank.Text = boundPlayer.CSWRRank.ToString();
        labRankDiff.Text = "+" + boundPlayer.RankDifferential.ToString();



      }
    }


    protected void gvRankedTooLow_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      // if we're binding to a data row, manipulate the controls within
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        // get references to the appropriate controls
        ProblemPlayer boundPlayer = (ProblemPlayer)e.Row.DataItem;

        Label labFullNameLastFirst = (Label)e.Row.FindControl("labFullNameLastFirst");
        Label labTeamAbbreviation = (Label)e.Row.FindControl("labTeamAbbreviation");
        Label labUserRank = (Label)e.Row.FindControl("labUserRank");
        Label labCSWRRank = (Label)e.Row.FindControl("labCSWRRank");
        Label labRankDiff = (Label)e.Row.FindControl("labRankDiff");
        Button butPromotePlayer = (Button)e.Row.FindControl("butPromotePlayer");
        HyperLink hlResearchPlayer = (HyperLink)e.Row.FindControl("hlResearchPlayer");

        // if this is a defensive player we don't want the mascot name in search, so we substitute 'defense'
        if (this.UserSheet.Positions[0].PositionCode == "DF")
        {
          hlResearchPlayer.NavigateUrl = "https://www.google.com/search?hl=en&gl=us&tbm=nws&btnmeta_news_search=1&q=" + boundPlayer.FullNameLastFirst + " Defense";
        }
        else
        {
          hlResearchPlayer.NavigateUrl = "https://www.google.com/search?hl=en&gl=us&tbm=nws&btnmeta_news_search=1&q=" + boundPlayer.FullName + " " + boundPlayer.TeamMascot;
        }
        hlResearchPlayer.ToolTip = "Click to view the latest news about " + boundPlayer.FullName + ".";

        labFullNameLastFirst.Text = boundPlayer.FullNameLastFirst;
        labTeamAbbreviation.Text = "(" + boundPlayer.TeamAbbreviation + ")";
        labUserRank.Text = boundPlayer.UserRank.ToString();
        labCSWRRank.Text = boundPlayer.CSWRRank.ToString();
        labUserRank.Text = boundPlayer.UserRank.ToString();
        labRankDiff.Text = "-" + boundPlayer.RankDifferential.ToString();

        // if we're promoting a player we need to know the playerID, the old index (zero-based), and the new index (zero-based)
        butPromotePlayer.Text = "Promote to " + boundPlayer.CSWRRank.ToString();
        butPromotePlayer.ToolTip = "Click to promote this player to position " + boundPlayer.CSWRRank.ToString() + " in your sheet.";

        // if we're reordering, we only need to know the oldindex and the new index
        int oldIndex = boundPlayer.UserRank - 1;
        int newIndex = boundPlayer.CSWRRank - 1;
        butPromotePlayer.CommandArgument = boundPlayer.PlayerID.ToString() + "-" + oldIndex + "-" + newIndex;

      }
    }

    protected void gvPlayersToAdd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      // if we're binding to a data row, manipulate the controls within
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        // get references to the appropriate controls
        ProblemPlayer boundPlayer = (ProblemPlayer)e.Row.DataItem;

        Label labFullNameLastFirst = (Label)e.Row.FindControl("labFullNameLastFirst");
        Label labTeamAbbreviation = (Label)e.Row.FindControl("labTeamAbbreviation");
        Label labCSWRRank = (Label)e.Row.FindControl("labCSWRRank");

        Button butAddPlayer = (Button)e.Row.FindControl("butAddPlayer");
        Button butInsertPlayer = (Button)e.Row.FindControl("butInsertPlayer");
        HyperLink hlResearchPlayer = (HyperLink)e.Row.FindControl("hlResearchPlayer");

        // if this is a defensive player we don't want the mascot name in search, so we substitute 'defense'
        if (this.UserSheet.Positions[0].PositionCode == "DF")
        {
          hlResearchPlayer.NavigateUrl = "https://www.google.com/search?hl=en&gl=us&tbm=nws&btnmeta_news_search=1&q=" + boundPlayer.FullNameLastFirst + " Defense";
        }
        else
        {
          hlResearchPlayer.NavigateUrl = "https://www.google.com/search?hl=en&gl=us&tbm=nws&btnmeta_news_search=1&q=" + boundPlayer.FullName + " " + boundPlayer.TeamMascot;
        }
        hlResearchPlayer.ToolTip = "Click to view the latest news about " + boundPlayer.FullName + ".";

        labFullNameLastFirst.Text = boundPlayer.FullNameLastFirst;
        labTeamAbbreviation.Text = "(" + boundPlayer.TeamAbbreviation + ")";
        labCSWRRank.Text = boundPlayer.CSWRRank.ToString();

        // if we're adding a player we only need the playerID
        butAddPlayer.CommandArgument = boundPlayer.PlayerID.ToString();
        // if we're inserting a player we need to know the playerID and position to insert
        butInsertPlayer.Text = "Insert at " + boundPlayer.CSWRRank.ToString();
        butInsertPlayer.ToolTip = "Click to insert this player into your cheat sheet at position " + boundPlayer.CSWRRank.ToString();
        butInsertPlayer.CommandArgument = boundPlayer.PlayerID.ToString() + "-" + boundPlayer.CSWRRank;

      }
    }

    protected void gvPlayersToRemove_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      // if we're binding to a data row, manipulate the controls within
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        // get references to the appropriate controls
        ProblemPlayer boundPlayer = (ProblemPlayer)e.Row.DataItem;

        Label labFullNameLastFirst = (Label)e.Row.FindControl("labFullNameLastFirst");
        Label labTeamAbbreviation = (Label)e.Row.FindControl("labTeamAbbreviation");
        Label labUserRank = (Label)e.Row.FindControl("labUserRank");
        Button butRemovePlayer = (Button)e.Row.FindControl("butRemovePlayer");
        Button butDemotePlayer = (Button)e.Row.FindControl("butDemotePlayer");
        HyperLink hlResearchPlayer = (HyperLink)e.Row.FindControl("hlResearchPlayer");
        Label labCSWRRank = (Label)e.Row.FindControl("labCSWRRank");

        // if this is a defensive player we don't want the mascot name in search, so we substitute 'defense'
        if (this.UserSheet.Positions[0].PositionCode == "DF")
        {
          hlResearchPlayer.NavigateUrl = "https://www.google.com/search?hl=en&gl=us&tbm=nws&btnmeta_news_search=1&q=" + boundPlayer.FullNameLastFirst + " Defense";
        }
        else
        {
          hlResearchPlayer.NavigateUrl = "https://www.google.com/search?hl=en&gl=us&tbm=nws&btnmeta_news_search=1&q=" + boundPlayer.FullName + " " + boundPlayer.TeamMascot;
        }
        hlResearchPlayer.ToolTip = "Click to view the latest news about " + boundPlayer.FullName + ".";

        labFullNameLastFirst.Text = boundPlayer.FullNameLastFirst;
        labTeamAbbreviation.Text = boundPlayer.TeamAbbreviation;
        labUserRank.Text = boundPlayer.UserRank.ToString();
        if (boundPlayer.CSWRRank == 0)
        {
          labCSWRRank.Text = "n/r";
        }
        else
        {
          labCSWRRank.Text = boundPlayer.CSWRRank.ToString();
        }
        
        /* We give users the ability to either demote players or remove them from their sheet entirely */

        // for the removal option, we only need the playerID
        butRemovePlayer.CommandArgument = boundPlayer.PlayerID.ToString();

        // for the demotion option, we need the playerID, the old index, and the new index
        int oldIndex = boundPlayer.UserRank - 1;
        int newIndex = this.UserSheetItems.Count-1;
        int newRank = this.UserSheetItems.Count + 1;

        // if we're demoting a player we need to know the playerID, the old index (zero-based), and the new index (zero-based)
        // if the player is already in the last sheet position, don't offer to demote them
        if (boundPlayer.UserRank == newRank-1)
        {
          butDemotePlayer.Visible = false;
        }
        else
        {
          butDemotePlayer.Text = "Demote to end";// +newRank.ToString();
          butDemotePlayer.ToolTip = "Click to demote this player to position " + newRank.ToString() + " in your sheet.";
          butDemotePlayer.CommandArgument = boundPlayer.PlayerID.ToString() + "-" + oldIndex + "-" + newIndex;
        }


      }
    }



    /// <summary>
    /// This repeater build the current player rankings
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void repSheetPlayers_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
      {
        CheatSheetItem boundPlayer = (CheatSheetItem)e.Item.DataItem;

        Label labPlayerRank = (Label)e.Item.FindControl("labPlayerRank");
        Label labPlayerName = (Label)e.Item.FindControl("labPlayerName");
        Label labTeamAbbreviation = (Label)e.Item.FindControl("labTeamAbbreviation");
        Label labDiff = (Label)e.Item.FindControl("labDiff");

        Panel panPlayerContainer = (Panel)e.Item.FindControl("panPlayerContainer");

        labPlayerRank.Text = boundPlayer.Seqno.ToString();
        labPlayerName.Text = boundPlayer.FullNameLastFirst;
        labTeamAbbreviation.Text = "(" + boundPlayer.Player.Team.Abbreviation + ")";

        // see if this player is part of the Ranked Too High collection
        ProblemPlayer targetPlayerTooHigh = this.PlayersRankedTooHigh.Find(delegate(ProblemPlayer probPlayer) { return (probPlayer.PlayerID == boundPlayer.PlayerID); });
        if (targetPlayerTooHigh != null)
        {
          panPlayerContainer.CssClass = "playerContainer playerRankedTooHigh";
          //imaRankedTooHigh.Visible = true;
          labDiff.Text = "+" + targetPlayerTooHigh.RankDifferential;
          labDiff.CssClass = "highDiff";
        }

        // see if this player is part of the Ranked Too Low collection
        ProblemPlayer targetPlayerTooLow = this.PlayersRankedTooLow.Find(delegate(ProblemPlayer probPlayer) { return (probPlayer.PlayerID == boundPlayer.PlayerID); });
        if (targetPlayerTooLow != null)
        {
          panPlayerContainer.CssClass = "playerContainer playerRankedTooLow";
          //imaRankedTooLow.Visible = true;
          labDiff.Text = "-" + targetPlayerTooLow.RankDifferential;
          labDiff.CssClass = "lowDiff";
        }

        // see if this player is part of the Player Removal Candidate collection
        ProblemPlayer targetPlayerToRemove = this.PlayersToRemove.Find(delegate(ProblemPlayer probPlayer) { return (probPlayer.PlayerID == boundPlayer.PlayerID); });
        if (targetPlayerToRemove != null)
        {
          panPlayerContainer.CssClass = "playerContainer playerToRemove";
          //imaPlayerToRemove.Visible = true;
        }


      }
    }




    protected void gvPlayersToAdd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      int playerID;
      bool success = true;

      StringBuilder sbMessage = new StringBuilder();

      switch (e.CommandName)
      {
        case "AddPlayer":
          playerID = int.Parse(e.CommandArgument.ToString());
          if (playerID != 0)
          {
            sbMessage.Append(Player.GetPlayer(playerID).FullName + " added to end of sheet, position ");
            int newSequenceNumber = this.UserSheetItems.Count() + 1;
            sbMessage.Append(newSequenceNumber.ToString() + ".");
            try
            {
              CheatSheet.AddCheatSheetItem(this.UserSheet.CheatSheetID, playerID, String.Empty);
            }
            catch(Exception ex)
            {
              string user = this.User.Identity.Name;
              string cheatSheetID = this.UserSheetID.ToString();
              string playerName = Player.GetPlayer(playerID).FullName;
              CheatSheetItem targetItem = this.UserSheetItems.Find(x => x.PlayerID == playerID);
              int currentPlayerPosition = 0;
              if (targetItem != null)
              {
                currentPlayerPosition = targetItem.Seqno;
              }
              string debugMessage = user + "tried to add  " + playerName + " (" + playerID.ToString() + ") to cheatsheetid:" + cheatSheetID + 
                                    " but he already existed at position: " + currentPlayerPosition.ToString() + " " + ex.InnerException.ToString();
              new CSWRWebEvent(debugMessage, null, null).Raise();
            }
          }
          else
          {
            success = false;
          }
          break;
        case "InsertPlayer":
          string[] argumentParts = e.CommandArgument.ToString().Split('-');
          playerID = int.Parse(argumentParts[0]);
          if (playerID != 0)
          {
            sbMessage.Append(Player.GetPlayer(playerID).FullName + " inserted at position ");
            int cswrSportsRank = int.Parse(argumentParts[1]);
            sbMessage.Append(cswrSportsRank.ToString() + ".");

            // add false tags representing football tags
            Dictionary<string, object> emptyFootballTags = new Dictionary<string, object>();
            emptyFootballTags.Add(CSIProperty.Sleeper.ToString(), false);
            emptyFootballTags.Add(CSIProperty.Bust.ToString(), false);
            emptyFootballTags.Add(CSIProperty.Injured.ToString(), false);

            CheatSheetItem.InsertCheatSheetItem(new CheatSheetItem(this.UserSheet.CheatSheetID, playerID, cswrSportsRank, String.Empty, emptyFootballTags));
          }
          else
          {
            success = false;
          }
          break;
      }

      if (success)
      {
        labStatus.Text = sbMessage.ToString();
        panStatus.Visible = true;
        panStatus.CssClass = "alert alert-success";
      }
      else
      {
        labStatus.Text = "No corresponding CSWR player could be found.  Verify that this player isn't listed as 'retired'";
        panStatus.Visible = true;
        panStatus.CssClass = "alert alert-error";
      }

      // We have to re-pull the user sheet because we invalidated cache when we added the player
      LoadSourceSheets();
      // Validate the cheat sheet
      ValidateSheet();
      Thread.Sleep(1000);
    }

    /// <summary>
    /// This method will be called when the user requests to remove a player from their sheet
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPlayersToRemove_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      int oldIndex, newIndex, newPositionIndex, playerID = 0;
      string[] argumentParts = e.CommandArgument.ToString().Split('-');

      if (argumentParts.Length == 1)
      {
        playerID = int.Parse(argumentParts[0]);
        CheatSheet.RemoveCheatSheetItem(this.UserSheet.CheatSheetID, playerID);
        // provide a status message


        labStatus.Text = Player.GetPlayer(playerID).FullName + " removed from sheet.";
        panStatus.Visible = true;
        panStatus.CssClass = "alert alert-success";
      }
      else if (argumentParts.Length == 3)
      {
        playerID = int.Parse(argumentParts[0]);
        oldIndex = int.Parse(argumentParts[1]);
        newIndex = int.Parse(argumentParts[2]);
        newPositionIndex = newIndex + 1;
        CheatSheet.ReorderCheatSheetItems(this.UserSheet.CheatSheetID, oldIndex, newIndex);
        // provide a status message
        labStatus.Text = Player.GetPlayer(playerID).FullName + " demoted to " + newPositionIndex.ToString();
        panStatus.Visible = true;
        panStatus.CssClass = "alert alert-success";
      }

      // We have to re-pull the user sheet because we invalidated cache when we removed the player
      LoadSourceSheets();
      // Validate the cheat sheet
      ValidateSheet();
      Thread.Sleep(1000);
    }

    protected void gvRankedTooLow_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      int oldIndex, newIndex, newPositionIndex, playerID = 0;
      string[] argumentParts = e.CommandArgument.ToString().Split('-');

      if (argumentParts.Length == 3)
      {
        playerID = int.Parse(argumentParts[0]);
        oldIndex = int.Parse(argumentParts[1]);
        newIndex = int.Parse(argumentParts[2]);
        newPositionIndex = newIndex + 1;

        CheatSheet.ReorderCheatSheetItems(this.UserSheet.CheatSheetID, oldIndex, newIndex);

        // provide a status message
        labStatus.Text = Player.GetPlayer(playerID).FullName + " promoted to " + newPositionIndex.ToString();
        panStatus.CssClass = "alert alert-success";
        panStatus.Visible = true;
        //mbStatus.Message = new StringBuilder(Player.GetPlayer(playerID).FullName + " promoted to " + newPositionIndex.ToString());
        //mbStatus.SetMessage();


      }

      // We have to re-pull the user sheet because we invalidated cache when we promoted the player
      LoadSourceSheets();
      // Validate the cheat sheet
      ValidateSheet();

      Thread.Sleep(1000);
    }


    protected void gvRankedTooHigh_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      int oldIndex, newIndex, newPositionIndex, playerID = 0;
      string[] argumentParts = e.CommandArgument.ToString().Split('-');

      if (argumentParts.Length == 3)  {
        playerID = int.Parse(argumentParts[0]);
        oldIndex = int.Parse(argumentParts[1]);
        newIndex = int.Parse(argumentParts[2]);
        newPositionIndex = newIndex + 1;
        CheatSheet.ReorderCheatSheetItems(this.UserSheet.CheatSheetID, oldIndex, newIndex);
        // provide a status message
        labStatus.Text = Player.GetPlayer(playerID).FullName + " demoted to " + newPositionIndex.ToString();
        panStatus.CssClass = "alert alert-success";
        panStatus.Visible = true;
      }

      // We have to re-pull the user sheet because we invalidated cache when we demoted the player
      LoadSourceSheets();
      // Validate the cheat sheet
      ValidateSheet();

      Thread.Sleep(1000);
    }

    private class ProblemPlayer
    {
      public ProblemPlayer(int playerID, string fullNameLastFirst, string teamAbbreviation, string teamMascot, int userRank, int cswrRank, int rankDifferential)
      {
        this.PlayerID = playerID;
        this.FullNameLastFirst = fullNameLastFirst;
        this.TeamAbbreviation = teamAbbreviation;
        this.TeamMascot = teamMascot;
        this.UserRank = userRank;
        this.CSWRRank = cswrRank;
        this.RankDifferential = rankDifferential;
      }

      public int PlayerID { get; set; }
      public string FullNameLastFirst { get; set; }
      public string TeamAbbreviation { get; set; }
      public string TeamMascot { get; set; }
      public int UserRank { get; set; }
      public int CSWRRank { get; set; }
      public int CBSSportsRank { get; set; }
      public int RankDifferential { get; set; }


      public string FullName
      {
        get
        {
          string[] bothNames = this.FullNameLastFirst.Split(',');
          if (bothNames.Length == 2)
          {
            return bothNames[1].Trim() + " " + bothNames[0];
          }
          else
          {
            return null;
          }
        }
      }
    }


    protected void ddlDifferential_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.RankDifferentialThreshold = int.Parse(ddlDifferential.SelectedValue);
      //mbStatus.MessageType = MessageType.NONE;

      panStatus.Visible = false;

      // We have to re-pull the user sheet because we invalidated cache when we added the player
      //LoadSourceSheets();
      // Validate the cheat sheet
      ValidateSheet();
    }


    protected void gvRankedTooHigh_DataBound(object sender, EventArgs e)
    {
      labRankedTooHighCount.Text = "(" + gvRankedTooHigh.Rows.Count.ToString() + ")";
    }

    protected void gvRankedTooLow_DataBound(object sender, EventArgs e)
    {
      labRankedTooLowCount.Text = "(" + gvRankedTooLow.Rows.Count.ToString() + ")";
    }

    protected void gvPlayersToAdd_DataBound(object sender, EventArgs e)
    {
      labPlayersToAddCount.Text = "(" + gvPlayersToAdd.Rows.Count.ToString() + ")";
    }



    protected void gvPlayersToRemove_DataBound(object sender, EventArgs e)
    {
      labPlayersToRemoveCount.Text = "(" + gvPlayersToRemove.Rows.Count.ToString() + ")";
    }

}
}