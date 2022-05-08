using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.Admin.Sheets
{
  public partial class ValidateSuppSheet : BasePage
  {
    private int SuppSheetID { get; set; }

    // Properties for the CSWR sheet we're validating
    private SupplementalSheet CSWRSuppSheet { get; set; }
    List<SupplementalSheetItem> CSWRSuppSheetItems { get; set; }

    // Properties for the CBS sheet we're comparing against
    private SupplementalSheet CBSSportsSheet { get; set; }
    List<SupplementalSheetItem> CBSSportsSheetItems { get; set; }

    // Properties for holding collections of problematic players
    private List<ProblemPlayer> PlayersRankedTooHigh { get; set; }
    private List<ProblemPlayer> PlayersRankedTooLow { get; set; }
    private List<ProblemPlayer> PlayersToRemove { get; set; }

    // Retain the differential threshold while we're on this page
    private int RankDifferentialThreshold 
    {
      get
      {
        return (ViewState["RankDifferentialThreshold"] == null) ? 5 : (int)ViewState["RankDifferentialThreshold"];
      }
      set
      {
        ViewState["RankDifferentialThreshold"] = value;
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        LoadControls();
        if (ProcessQueryString())
        {
          LoadSourceSheets();
        }
        LoadDifferentialDropdown();
        // load data we'll need in all methods
        ValidateSheet();
      }
      else
      {
        SupplementalSource cswrSource = SupplementalSource.GetSupplementalSource("CSWR");
        SupplementalSheet targetSuppSheet = SupplementalSheet.GetSupplementalSheet(FOO.CurrentSeason, cswrSource.SupplementalSourceID, FOO.FOOString, ddlPosition.SelectedValue);
        this.SuppSheetID = targetSuppSheet.SupplementalSheetID;
        LoadSourceSheets();
      }
    }

    private void LoadControls()
    {
      ddlPosition.DataTextField = "Name";
      ddlPosition.DataValueField = "PositionCode";
      ddlPosition.DataSource = Position.GetPositions(FOO.FOOString);
      ddlPosition.DataBind();
    }
    
    
    private void LoadDifferentialDropdown()
    {
      for(int i=1;i<21;i++)  
      {
        ddlDifferential.Items.Add(new ListItem(i.ToString(), i.ToString()));
      }
      ddlDifferential.SelectedValue = this.RankDifferentialThreshold.ToString();
      ddlDifferential.DataBind();
    }



    private void ValidateSheet()
    {
      int problemPlayerCount = 0;

      // load data we'll need in all methods
      LoadSourceSheets();
      // check for players who are ranked to high in the sheet
      problemPlayerCount += CheckRankedTooHigh();
      // check for players who are ranked too low in the sheet
      problemPlayerCount += CheckRankedTooLow();
      // check for players who aren't on the sheet but should be
      problemPlayerCount += CheckPlayersToAdd();
      // check for players who are on the sheet but shouldn't be
      problemPlayerCount += CheckPlayersToRemove();

      // populate the list of players on the right-hand side
      repSheetPlayers.DataSource = this.CSWRSuppSheetItems;
      repSheetPlayers.DataBind();

      // load the right-side cheat sheet rankings
      if (problemPlayerCount == 0)
      {
        panNoSuggestions.Visible = true;
      }

    }

    private void LoadSourceSheets()
    {
      // get CSWR sheet information
      this.CSWRSuppSheet = SupplementalSheet.GetSupplementalSheet(this.SuppSheetID);
      this.CSWRSuppSheetItems = SupplementalSheetItem.GetSupplementalSheetItems(this.SuppSheetID);
      ddlPosition.SelectedValue = this.CSWRSuppSheet.PositionCode;

      // get compare sheet (CBSSports) information
      SupplementalSource cbsSportsSource = SupplementalSource.GetSupplementalSource("CBS");
      this.CBSSportsSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode,
                                                  cbsSportsSource.SupplementalSourceID, "FOO", this.CSWRSuppSheet.PositionCode);
      this.CBSSportsSheetItems = SupplementalSheetItem.GetSupplementalSheetItems(this.CBSSportsSheet.SupplementalSheetID);
    }


    private bool ProcessQueryString()
    {
      int supplementalSheetID = 0;
      if (Request.QueryString["ID"] != null)
      {
        if (int.TryParse(Request.QueryString["ID"], out supplementalSheetID))
        {
          this.SuppSheetID = supplementalSheetID;
        }
        else
        {
          mbStatus.Message = new StringBuilder("Querystring ID couldn't be parsed.");
          mbStatus.MessageType = MessageType.ERROR;
          return false;
        }
      }
      else
      {
        mbStatus.Message = new StringBuilder("Querystring value not found.");
        mbStatus.MessageType = MessageType.ERROR;
        return false;
      }
      return true;
    }


    private int CheckRankedTooHigh()
    {
      List<ProblemPlayer> playersRankedTooHigh = new List<ProblemPlayer>();
      List<ProblemPlayer> playersNotRankedByCBS = new List<ProblemPlayer>();

      // isolate players whose rank differential is greater than the defined threshold
      foreach (SupplementalSheetItem currentCSWRItem in this.CSWRSuppSheetItems)
      {
        // find the corresponding item in the cbs sheet we're comparing against        
        SupplementalSheetItem cbsSportsItem = this.CBSSportsSheetItems.Find(delegate(SupplementalSheetItem targetItem) { return (currentCSWRItem.PlayerID == targetItem.PlayerID); });
        if (cbsSportsItem != null)
        {
          int pointDifferential = cbsSportsItem.Seqno - currentCSWRItem.Seqno;
          if (pointDifferential >= this.RankDifferentialThreshold)
          {
            playersRankedTooHigh.Add(new ProblemPlayer(currentCSWRItem.PlayerID, currentCSWRItem.FullNameLastFirst, currentCSWRItem.Player.Team.Abbreviation, 
                                       currentCSWRItem.Player.Team.Mascot, currentCSWRItem.Seqno, cbsSportsItem.Seqno, pointDifferential));
          }
        }
        // if the CSWR players isn't found in the CBS Sports Sheet, they may be ranked too high,
        // but only if the playher is ranked higher than (CBSSportsItemCount - Threshold)
        /* THIS PROBABLY WON'T BE RELEVANT TO USER SHEETS BECAUSE CSWR Ranks ALL players */
        else
        {
          if (currentCSWRItem.Seqno < (this.CBSSportsSheetItems.Count() - this.RankDifferentialThreshold))
          {
            int pointDifferential = this.CBSSportsSheetItems.Count() - currentCSWRItem.Seqno;
            playersNotRankedByCBS.Add(new ProblemPlayer(currentCSWRItem.PlayerID, currentCSWRItem.FullNameLastFirst, currentCSWRItem.Player.Team.Abbreviation,
                                                 currentCSWRItem.Player.Team.Mascot, currentCSWRItem.Seqno, 0, pointDifferential));
          }
        }

      }

      // sort and combine two lists
      playersRankedTooHigh.Sort(delegate(ProblemPlayer player1, ProblemPlayer player2) { return (player2.RankDifferential.CompareTo(player1.RankDifferential)); });
      playersNotRankedByCBS.Sort(delegate(ProblemPlayer player1, ProblemPlayer player2) { return (player2.RankDifferential.CompareTo(player1.RankDifferential)); });
      playersRankedTooHigh.AddRange(playersNotRankedByCBS);

      // hide the header if the master collection is empty
      if (playersRankedTooHigh.Count == 0)
      {
        headerRankedTooHigh.Visible = false;
      }
      else
      {
        headerRankedTooHigh.Visible = true;
      }
      
      // store the collection to be ready by the cheat sheet on righ
      this.PlayersRankedTooHigh = playersRankedTooHigh;

      // bind the grid
      gvRankedTooHigh.DataSource = playersRankedTooHigh;
      gvRankedTooHigh.DataBind();

      return playersRankedTooHigh.Count;
    }


    private int CheckRankedTooLow()
    {
      List<ProblemPlayer> playersRankedTooLow = new List<ProblemPlayer>();

      foreach (SupplementalSheetItem currentCSWRItem in this.CSWRSuppSheetItems)
      {
        // find the corresponding item in the cbs sheet we're comparing against        
        SupplementalSheetItem cbsSportsItem = this.CBSSportsSheetItems.Find(delegate(SupplementalSheetItem targetItem) { return (currentCSWRItem.PlayerID == targetItem.PlayerID); });
        if (cbsSportsItem != null)
        {
          int pointDifferential = currentCSWRItem.Seqno - cbsSportsItem.Seqno;
          if (pointDifferential >= this.RankDifferentialThreshold)
          {
            playersRankedTooLow.Add(new ProblemPlayer(currentCSWRItem.PlayerID, currentCSWRItem.FullNameLastFirst, currentCSWRItem.Player.Team.Abbreviation,
                                      currentCSWRItem.Player.Team.Mascot, currentCSWRItem.Seqno,cbsSportsItem.Seqno, pointDifferential));
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

      // store the collection to be ready by the cheat sheet on righ
      this.PlayersRankedTooLow = playersRankedTooLow;

      // bind the grid
      gvRankedTooLow.DataSource = playersRankedTooLow;
      gvRankedTooLow.DataBind();

      return playersRankedTooLow.Count;
    }

    
    private int CheckPlayersToAdd()
    {
      List<ProblemPlayer> playerAdditionCandidates = new List<ProblemPlayer>();

      // get the size of the CSWR sheet
      int CSWRSuppSheetItemCount = this.CSWRSuppSheetItems.Count();

      // look through the CBSSports sheet for any players that aren't in my sheet
      foreach (SupplementalSheetItem currentCBSSportsItem in this.CBSSportsSheetItems)
      {
        // only consider items in CBS sheet that are within the range of the number of items in the CSWR sheet, otherwise
        // the user probably isn't interested in them
        if (currentCBSSportsItem.Seqno < CSWRSuppSheetItemCount)
        {
          // find the corresponding item in the CSWR sheet         
          SupplementalSheetItem cswrItem = this.CSWRSuppSheetItems.Find(delegate(SupplementalSheetItem targetItem) { return (currentCBSSportsItem.PlayerID == targetItem.PlayerID); });
          // if the CBS Sports player isn't found in the specified range in the CSWR sheet, they should consider adding them so put them in the list
          if(cswrItem == null)  
          {
            int targetPlayerID = 0;
            // if the player isn't in the user's cheat sheet, try to see if we can find them in the database based on their first and last name
            List<Player> targetPlayers = Player.GetPlayers("FOO", this.CSWRSuppSheet.SeasonCode, this.CSWRSuppSheet.PositionCode, currentCBSSportsItem.Player.FirstName, currentCBSSportsItem.Player.LastName, false);

            if (targetPlayers.Count == 1)
            {
              targetPlayerID = targetPlayers[0].PlayerID;
            }
            playerAdditionCandidates.Add(new ProblemPlayer(targetPlayerID, currentCBSSportsItem.FullNameLastFirst, currentCBSSportsItem.Player.Team.Abbreviation,
                                              currentCBSSportsItem.Player.Team.Mascot, 0, currentCBSSportsItem.Seqno, 0));
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

      playerAdditionCandidates.Sort(delegate(ProblemPlayer player1, ProblemPlayer player2) { return (player1.CBSSportsRank.CompareTo(player2.CBSSportsRank)); });
      gvPlayersToAdd.DataSource = playerAdditionCandidates;
      gvPlayersToAdd.DataBind();

      return playerAdditionCandidates.Count;
    }



    private int CheckPlayersToRemove()
    {
      List<ProblemPlayer> playerRemovalCandidates = new List<ProblemPlayer>();

      // look through the CBSSports sheet for any players that aren't in my sheet
      foreach (SupplementalSheetItem currentCSWRItem in this.CSWRSuppSheetItems)
      {
        if (currentCSWRItem.Player.Retired)
        {
          playerRemovalCandidates.Add(new ProblemPlayer(currentCSWRItem.PlayerID, currentCSWRItem.FullNameLastFirst, currentCSWRItem.Player.Team.Abbreviation, 
                                         currentCSWRItem.Player.Team.Mascot, currentCSWRItem.Seqno, 0, 0));
        }
      }

      // if the source list is empty, hide the eader
      if (playerRemovalCandidates.Count == 0)
      {
        headerPlayersToRemove.Visible = false;
      }
      else
      {
        headerPlayersToRemove.Visible = true;
      }

      playerRemovalCandidates.Sort(delegate(ProblemPlayer player1, ProblemPlayer player2) { return (player1.CBSSportsRank.CompareTo(player2.CBSSportsRank)); });

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
        Label labCSWRRank = (Label)e.Row.FindControl("labCSWRRank");
        Label labCBSRank = (Label)e.Row.FindControl("labCBSRank");
        Label labRankDiff = (Label)e.Row.FindControl("labRankDiff");
        Button butDemotePlayer = (Button)e.Row.FindControl("butDemotePlayer");
        HyperLink hlResearchPlayer = (HyperLink)e.Row.FindControl("hlResearchPlayer");

        hlResearchPlayer.NavigateUrl = "https://www.google.com/search?hl=en&gl=us&tbm=nws&btnmeta_news_search=1&q=" + boundPlayer.FullName + " " + boundPlayer.TeamMascot;

        labFullNameLastFirst.Text = boundPlayer.FullNameLastFirst;
        labTeamAbbreviation.Text = boundPlayer.TeamAbbreviation;
        labCSWRRank.Text = boundPlayer.CSWRRank.ToString();

        // if we're reordering, we only need to know the oldindex and the new index
        int oldIndex = boundPlayer.CSWRRank - 1;
        // if CBS doesn't have the player ranked, then we can only suggesting moving the player based on the offset
        // between the player's rank and the lowest ranked person on CBSSports's list
        int newIndex = 0;
        if (boundPlayer.CBSSportsRank != 0)
        {
          newIndex = boundPlayer.CBSSportsRank - 1;
          // if we're promoting a player we need to know the playerID, the old index (zero-based), and the new index (zero-based)
          butDemotePlayer.Text = "Demote Player to " + boundPlayer.CBSSportsRank.ToString();
          butDemotePlayer.ToolTip = "Click to demote this player to position " + boundPlayer.CBSSportsRank.ToString() + " in your sheet.";
        }
        else
        {
          newIndex = boundPlayer.CSWRRank + boundPlayer.RankDifferential;
          // if we're promoting a player we need to know the playerID, the old index (zero-based), and the new index (zero-based)
          int newDemotionPosition = newIndex + 1;
          butDemotePlayer.Text = "Demote Player to " + newDemotionPosition;
          butDemotePlayer.ToolTip = "Click to demote this player to position " + newDemotionPosition + " in your sheet.";
        }
        butDemotePlayer.CommandArgument = oldIndex + "-" + newIndex;


        if (boundPlayer.CBSSportsRank != 0)
        {
          labCBSRank.Text = boundPlayer.CBSSportsRank.ToString();
          labRankDiff.Text = "+" + boundPlayer.RankDifferential.ToString();
        }
        else
        {
          labCBSRank.Text = "n/r";
          labRankDiff.Text = ">=" + boundPlayer.RankDifferential.ToString();
        }


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
        Label labCSWRRank = (Label)e.Row.FindControl("labCSWRRank");
        Label labCBSRank = (Label)e.Row.FindControl("labCBSRank");
        Label labRankDiff = (Label)e.Row.FindControl("labRankDiff");
        Button butPromotePlayer = (Button)e.Row.FindControl("butPromotePlayer");
        HyperLink hlResearchPlayer = (HyperLink)e.Row.FindControl("hlResearchPlayer");

        hlResearchPlayer.NavigateUrl = "https://www.google.com/search?hl=en&gl=us&tbm=nws&btnmeta_news_search=1&q=" + boundPlayer.FullName + " " + boundPlayer.TeamMascot;

        labFullNameLastFirst.Text = boundPlayer.FullNameLastFirst;
        labTeamAbbreviation.Text = boundPlayer.TeamAbbreviation;
        labCSWRRank.Text = boundPlayer.CSWRRank.ToString();
        labCBSRank.Text = boundPlayer.CBSSportsRank.ToString();
        labRankDiff.Text = "-" + boundPlayer.RankDifferential.ToString();

        // if we're promoting a player we need to know the playerID, the old index (zero-based), and the new index (zero-based)
        butPromotePlayer.Text = "Promote Player to " + boundPlayer.CBSSportsRank.ToString();
        butPromotePlayer.ToolTip = "Click to promote this player to position " + boundPlayer.CBSSportsRank.ToString() + " in your sheet.";

        // if we're reordering, we only need to know the oldindex and the new index
        int oldIndex = boundPlayer.CSWRRank - 1;
        int newIndex = boundPlayer.CBSSportsRank - 1;
        butPromotePlayer.CommandArgument = oldIndex + "-" + newIndex;

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
        Label labCBSRank = (Label)e.Row.FindControl("labCBSRank");

        Button butAddPlayer = (Button)e.Row.FindControl("butAddPlayer");
        Button butInsertPlayer = (Button)e.Row.FindControl("butInsertPlayer");
        HyperLink hlResearchPlayer = (HyperLink)e.Row.FindControl("hlResearchPlayer");

        hlResearchPlayer.NavigateUrl = "https://www.google.com/search?hl=en&gl=us&tbm=nws&btnmeta_news_search=1&q=" + boundPlayer.FullName + " " + boundPlayer.TeamMascot;

        labFullNameLastFirst.Text = boundPlayer.FullNameLastFirst;
        labTeamAbbreviation.Text = boundPlayer.TeamAbbreviation;
        labCBSRank.Text = boundPlayer.CBSSportsRank.ToString();

        // if we're adding a player we only need the playerID
        butAddPlayer.CommandArgument = boundPlayer.PlayerID.ToString();
        // if we're inserting a player we need to know the playerID and position to insert
        butInsertPlayer.Text = "Insert at Position " + boundPlayer.CBSSportsRank.ToString();
        butInsertPlayer.ToolTip = "Click to insert this player into your cheat sheet at position " + boundPlayer.CBSSportsRank.ToString();
        butInsertPlayer.CommandArgument = boundPlayer.PlayerID.ToString() + "-" + boundPlayer.CBSSportsRank;

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
        Label labCSWRRank = (Label)e.Row.FindControl("labCSWRRank");
        Button butRemovePlayer = (Button)e.Row.FindControl("butRemovePlayer");
        HyperLink hlResearchPlayer = (HyperLink)e.Row.FindControl("hlResearchPlayer");

        hlResearchPlayer.NavigateUrl = "https://www.google.com/search?hl=en&gl=us&tbm=nws&btnmeta_news_search=1&q=" + boundPlayer.FullName + " " + boundPlayer.TeamMascot;

        labFullNameLastFirst.Text = boundPlayer.FullNameLastFirst;
        labTeamAbbreviation.Text = boundPlayer.TeamAbbreviation;
        labCSWRRank.Text = boundPlayer.CSWRRank.ToString();

        
        // if we're removing a player we only need the playerID
        butRemovePlayer.CommandArgument = boundPlayer.PlayerID.ToString();
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
        SupplementalSheetItem boundPlayer = (SupplementalSheetItem)e.Item.DataItem;

        Label labPlayerRank = (Label)e.Item.FindControl("labPlayerRank");
        Label labPlayerName = (Label)e.Item.FindControl("labPlayerName");
        Label labTeamAbbreviation = (Label)e.Item.FindControl("labTeamAbbreviation");
        Label labDiff = (Label)e.Item.FindControl("labDiff");

        //Image imaRankedTooHigh = (Image)e.Item.FindControl("imaRankedTooHigh");
        //Image imaRankedTooLow = (Image)e.Item.FindControl("imaRankedTooLow");
        //Image imaPlayerToRemove = (Image)e.Item.FindControl("imaPlayerToRemove");
        
        Panel panPlayerContainer = (Panel)e.Item.FindControl("panPlayerContainer");

        labPlayerRank.Text = boundPlayer.Seqno.ToString();
        labPlayerName.Text = boundPlayer.FullNameLastFirst + " - ";
        labTeamAbbreviation.Text = boundPlayer.Player.Team.Abbreviation;

        // see if this player is part of the Ranked Too High collection
        ProblemPlayer targetPlayerTooHigh = this.PlayersRankedTooHigh.Find(delegate(ProblemPlayer probPlayer) { return (probPlayer.PlayerID == boundPlayer.PlayerID); });
        if (targetPlayerTooHigh != null)
        {
          panPlayerContainer.CssClass = "playerContainer playerRankedTooHigh";
          //imaRankedTooHigh.Visible = true;
          labDiff.Text = "(+" + targetPlayerTooHigh.RankDifferential + ")";
          labDiff.CssClass = "highDiff";
        }

        // see if this player is part of the Ranked Too Low collection
        ProblemPlayer targetPlayerTooLow = this.PlayersRankedTooLow.Find(delegate(ProblemPlayer probPlayer) { return (probPlayer.PlayerID == boundPlayer.PlayerID); });
        if (targetPlayerTooLow != null)
        {
          panPlayerContainer.CssClass = "playerContainer playerRankedTooLow";
          //imaRankedTooLow.Visible = true;
          labDiff.Text = "(-" + targetPlayerTooLow.RankDifferential + ")";
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
      int playerID = 0;
      bool success = true;
      bool reorder = false;

      StringBuilder sbMessage = new StringBuilder();

      //Since we only show 100 players, we need to determine if the user is actually on the cheat sheet
      List<SupplementalSheetItem> allItems = SupplementalSheetItem.GetSupplementalSheetItems(this.SuppSheetID);
      // if we find the item, we need to REORDER, not add
      if (allItems.Find(delegate(SupplementalSheetItem fooItem) { return (fooItem.PlayerID == playerID); }) != null)
      {
        reorder = true;
      }


      switch (e.CommandName)
      {
        case "AddPlayer":
          playerID = int.Parse(e.CommandArgument.ToString());
          if (playerID != 0)
          {
            sbMessage.Append(Player.GetPlayer(playerID).FullNameLastFirst + " added to position ");
            int newSequenceNumber = this.CSWRSuppSheetItems.Count() + 1;
            sbMessage.Append(newSequenceNumber.ToString() + ".");
            SupplementalSheet.AddSupplementalSheetItem(this.CSWRSuppSheet.SupplementalSheetID, playerID);
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
            sbMessage.Append(Player.GetPlayer(playerID).FullNameLastFirst + " added to position ");
            int cbsSportsRank = int.Parse(argumentParts[1]);
            sbMessage.Append(cbsSportsRank.ToString() + ".");

            Dictionary<string, object> mappedProperties = new Dictionary<string,object>();
            mappedProperties[SSIProperty.Sleeper.ToString()] = false;
            mappedProperties[SSIProperty.Bust.ToString()] = false;

            SupplementalSheetItem itemToInsert = new SupplementalSheetItem(this.CSWRSuppSheet.SupplementalSheetID, playerID, cbsSportsRank, String.Empty, mappedProperties);
            SupplementalSheetItem.InsertSupplementalSheetItem(itemToInsert);

          }
          else
          {
            //success = false;
          }
          break;
      }
      ValidateSheet();
      Thread.Sleep(1000);

      if (success)
      {
        mbStatus.MessageType = MessageType.SUCCESS;
        mbStatus.Message = sbMessage;
      }
      else
      {
        mbStatus.Message = new StringBuilder("No corresponding CSWR player could be found.  Verify that this player isn't listed as 'retired'");
        mbStatus.MessageType = MessageType.ERROR;
      }
    }

    /// <summary>
    /// This method will be called when the user requests to remove a player from their sheet
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPlayersToRemove_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      bool success = false;
      StringBuilder sbMessage = new StringBuilder();

      int playerID = 0;
      if(int.TryParse(e.CommandArgument.ToString(), out playerID))
      {
        SupplementalSheet.RemoveSupplementalSheetItem(this.CSWRSuppSheet.SupplementalSheetID, playerID);
        Player targetPlayer = Player.GetPlayer(playerID);
        sbMessage.Append(targetPlayer.FullName + " successfully removed from sheet.");
        success = true;
      }
      ValidateSheet();

      if (success)
      {
        mbStatus.MessageType = MessageType.SUCCESS;
        mbStatus.Message = sbMessage;
      }
      else
      {
        mbStatus.Message = new StringBuilder("No corresponding CSWR player could be found.  Verify that this player isn't listed as 'retired'");
        mbStatus.MessageType = MessageType.ERROR;
      }
      Thread.Sleep(1000);
    }

    protected void gvRankedTooLow_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      StringBuilder sbMessage = new StringBuilder();
      bool success = false;
      int oldIndex, newIndex = 0;
      string[] argumentParts = e.CommandArgument.ToString().Split('-');
      oldIndex = int.Parse(argumentParts[0]);
      newIndex = int.Parse(argumentParts[1]);

      Player targetPlayer = SupplementalSheetItem.GetSupplementalSheetItems(this.CSWRSuppSheet.SupplementalSheetID)[oldIndex].Player;

      if (SupplementalSheet.ReorderSupplementalSheetItems(this.CSWRSuppSheet.SupplementalSheetID, oldIndex, newIndex))
      {
        sbMessage.Append(targetPlayer.FullName + " successfully promoted.");
        success = true;
      }

      ValidateSheet();

      if (success)
      {
        mbStatus.MessageType = MessageType.SUCCESS;
        mbStatus.Message = sbMessage;
      }
      else
      {
        mbStatus.Message = new StringBuilder("No corresponding CSWR player could be found.  Verify that this player isn't listed as 'retired'");
        mbStatus.MessageType = MessageType.ERROR;
      }
      Thread.Sleep(1000);
    }


    protected void gvRankedTooHigh_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      StringBuilder sbMessage = new StringBuilder();
      bool success = false;
      int oldIndex, newIndex = 0;
      string[] argumentParts = e.CommandArgument.ToString().Split('-');
      oldIndex = int.Parse(argumentParts[0]);
      newIndex = int.Parse(argumentParts[1]);

      Player targetPlayer = SupplementalSheetItem.GetSupplementalSheetItems(this.CSWRSuppSheet.SupplementalSheetID)[oldIndex].Player;

      if (SupplementalSheet.ReorderSupplementalSheetItems(this.CSWRSuppSheet.SupplementalSheetID, oldIndex, newIndex))
      {
        sbMessage.Append(targetPlayer.FullName + " successfully demoted.");
        success = true;
      }

      ValidateSheet();

      if (success)
      {
        mbStatus.MessageType = MessageType.SUCCESS;
        mbStatus.Message = sbMessage;
      }
      else
      {
        mbStatus.Message = new StringBuilder("No corresponding CSWR player could be found.  Verify that this player isn't listed as 'retired'");
        mbStatus.MessageType = MessageType.ERROR;
      }
    }

    private class ProblemPlayer
    {
      public ProblemPlayer(int playerID, string fullNameLastFirst, string teamAbbreviation, string teamMascot, int cswrRank, int cbsSportsRank, int rankDifferential)
      {
        this.PlayerID = playerID;
        this.FullNameLastFirst = fullNameLastFirst;
        this.TeamAbbreviation = teamAbbreviation;
        this.TeamMascot = teamMascot;
        this.CSWRRank = cswrRank;
        this.CBSSportsRank = cbsSportsRank;
        this.RankDifferential = rankDifferential;
      }

      public int PlayerID { get; set; }
      public string FullNameLastFirst { get; set; }
      public string TeamAbbreviation { get; set; }
      public string TeamMascot { get; set; }
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
      LoadSheetsAndValidate();
    }

    protected void ddlPosition_SelectedIndexChanged(object sender, EventArgs e)
    {
      LoadSheetsAndValidate();
      mbStatus.Visible = false;
    }

    private void LoadSheetsAndValidate()
    {
      LoadDifferentialDropdown();
      // load data we'll need in all methods
      ValidateSheet();
    }

  }
}