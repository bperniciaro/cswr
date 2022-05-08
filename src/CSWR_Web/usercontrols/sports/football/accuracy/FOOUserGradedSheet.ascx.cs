using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom;
using BP.CheatSheetWarRoom.BLL.Sheets;
using BP.CheatSheetWarRoom.UI;

namespace UserControls.Sports.Football.Accuracy 
{ 
  public partial class FooUserGradedSheet : System.Web.UI.UserControl
  {

    /// <summary>
    /// Set by the container page, indicates the season on which the cheat sheet is based
    /// </summary>
    public string SeasonCode
    {
      get { return (ViewState["SeasonCode"] == null) ? FOO.LastSeason : (string)ViewState["SeasonCode"]; }
      set { ViewState["SeasonCode"] = value; }
    }

    /// <summary>
    /// Set by the container page, indicates the user to whom the cheat sheet belongs
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Set by the container page, indicates the position on which the cheat sheet is based
    /// </summary>
    public string PositionCode { get; set; }

    // a collection of ranked players from the previous season
    private List<RankedPlayer> _rankedPlayers = new List<RankedPlayer>();

    // a collection of players and their ranking differentials for a particular sport and season
    private List<UserSheetPlayerDifferential> _playerDifferentials = new List<UserSheetPlayerDifferential>();

    // local variables to hold constants in [short] form
    private int _maxPositionalGradingSize = 0;
    private int _maxRankedPlayersConsidered = 0;

    // local variable to hold the sum of the user ranking differentials for the entire sheet
    private int _userRankingDifferentialSum = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
      PopulateSheet();
    }

   
    private void PopulateSheet()
    {
      if ((!String.IsNullOrEmpty(this.PositionCode)) && (!String.IsNullOrEmpty(this.Username)))
      {
        LoadGlobalVariables();
        GenerateButtons();
        BindUserSheet();
      }
      else
      {
        panHeader.Visible = false;
        mbStatus.Message = new StringBuilder("Invalid Data.");
        mbStatus.MessageType = MessageType.ERROR;
        mbStatus.SetMessage();
      }
    }




    private void LoadGlobalVariables()
    {
      // need to try to use projection here to load the new class
      List<Player> statSortedPlayers = Player.GetPlayers(FOO.FOOString, this.SeasonCode, this.PositionCode, false, "TFP", SortDir.DESC.ToString());
      int i = 1;
      foreach (Player targetPlayer in statSortedPlayers)
      {
        _rankedPlayers.Add(new RankedPlayer(i, targetPlayer));
        i++;
      }
      // load all player ranking differentials for a particular season
      _playerDifferentials = UserSheetPlayerDifferential.GetUserSheetPlayerDifferentials(FOO.FOOString, this.SeasonCode);
      // determine the  number of players that we actually graded
      _maxPositionalGradingSize = Helpers.GetUserSheetGradedItemsBySportPosition(FOO.FOOString, this.PositionCode);
      // determine the maximum number of top players we want to hold users responsible for
      _maxRankedPlayersConsidered = Helpers.GetMaxRankPlayersConsideredBySportPosition(FOO.FOOString, this.PositionCode);

      //var qbCount = _playerDifferentials.Where(x => x.PositionCode == "QB").Count();
      //var rbCount = _playerDifferentials.Where(x => x.PositionCode == "RB").Count();
      //var wrCount = _playerDifferentials.Where(x => x.PositionCode == "WR").Count();
      //var teCount = _playerDifferentials.Where(x => x.PositionCode == "TE").Count();
      //var kCount = _playerDifferentials.Where(x => x.PositionCode == "K").Count();
      //var dfCount = _playerDifferentials.Where(x => x.PositionCode == "DF").Count();


      //var playerX = _playerDifferentials.SingleOrDefault(x => x.PlayerID == 4680);
      //var test2 = _playerDifferentials.Where(x => x.PositionCode == "QB");

    }



    private void BindUserSheet()
    {
      var targetSheetGrade = UserSheetPositionGrade.GetUserSheetPositionGrade(this.Username, this.SeasonCode, this.PositionCode);

      if (targetSheetGrade == null)
      {
        panSheetRank.Visible = false;
        panHeader.Visible = false;
        panNoSheetFound.Visible = true;
      }
      else
      {
        panNoSheetFound.Visible = false;
        BuildHeader(targetSheetGrade);

        // bind sheet (addin faux items if necessary to round-out the sheet
        List<GradedPlayer> gradedSheetPlayers = ArchivedCheatSheetItem.GetArchivedCheatSheetItems(targetSheetGrade.ArchivedCheatSheetID)
                                        .Take(_maxPositionalGradingSize)
                                        .Select(x => new GradedPlayer(x.Seqno, x.PlayerID, x.FullName)).ToList();

        if (gradedSheetPlayers.Count() < _maxRankedPlayersConsidered)
        {
          for (var i = gradedSheetPlayers.Count(); i < _maxPositionalGradingSize; i++)
          {
            int sheetSeqNo = i + 1;
            GradedPlayer fauxItem = new GradedPlayer(sheetSeqNo, 0, String.Empty);
            gradedSheetPlayers.Add(fauxItem);
          }
        }

        gvUserSheet.DataSource = gradedSheetPlayers;
        gvUserSheet.DataBind();
      }
    }




    private void BuildHeader(UserSheetPositionGrade targetSheetGrade)
    {
      litUsername.Text = this.Username.GetPossessive();
      litSeasonCode.Text = this.SeasonCode;
      litPosition.Text = this.PositionCode;
      
      // need to build the ranking description
      litRank.Text = targetSheetGrade.Rank.ToString().AddNumberSuffix();
      litScore.Text = targetSheetGrade.Score.ToString().AddNumberSuffix();
      litTotalSheetsGraded.Text = UserSheetPositionGrade.GetUserSheetPositionGradeCount(this.SeasonCode, FOO.FOOString, this.PositionCode).ToString();

      
      litPositionName.Text = Position.GetPosition(this.PositionCode).Name.ToLower();

      ArchivedCheatSheet targetSheet = ArchivedCheatSheet.GetArchivedCheatSheet(targetSheetGrade.ArchivedCheatSheetID);
      labSheetName.Text = "\"" + targetSheet.SheetName + "\"";
    }



    protected void gvUserSheet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.Header)
      {
        e.Row.Cells[2].Text = FOO.LastSeason.ToString() + " Rank";
      }
      else if (e.Row.RowType == DataControlRowType.DataRow)
      {
        // get a reference to the bound object
        var boundItem = (GradedPlayer)e.Row.DataItem;

        // get references to the appropriate controls
        var labRank = (Label)e.Row.FindControl("labRank");
        var labUserRankDifference = (Label)e.Row.FindControl("labUserRankDifference");
        var labAverageRankDifference = (Label)e.Row.FindControl("labAverageRankDifference");
        var labUserVersusAverage = (Label)e.Row.FindControl("labUserVersusAverage");
        var labPlayerName = (Label) e.Row.FindControl("labPlayerName");

        // if this is a faux item we only want to load a blank row because this is the case where the user did
        // not have the required number of players to be graded within their sheet
        if (boundItem.PlayerId == 0)
        {
          labRank.Text = "n/a";
          labAverageRankDifference.Text = "n/a";
          labUserVersusAverage.Text = "n/a";
          labPlayerName.Text = "No Player Ranked";
          // populate the default grading parameters and add them to the total
          labUserRankDifference.Text = _maxPositionalGradingSize.ToString();
          _userRankingDifferentialSum += _maxPositionalGradingSize;
          e.Row.Attributes.Add("class", "noPlayerRanked");
          return;
        }

        // if we get to this point then we're processing a real player
        labPlayerName.Text = boundItem.FullName;


        // determine rank differentials
        var playerRankDifferential = 0;

        var targetRankedPlayer = _rankedPlayers.SingleOrDefault(x => x.Player.PlayerID == boundItem.PlayerId);

        // determine the rank differentials based on if the player finished in the range of qualified players
        if (targetRankedPlayer != null)
        {
          labRank.Text = targetRankedPlayer.Rank.ToString();
          int relativeRankDifferential = Math.Abs(boundItem.Seqno - targetRankedPlayer.Rank);
          playerRankDifferential = (relativeRankDifferential < _maxRankedPlayersConsidered) ? relativeRankDifferential : _maxRankedPlayersConsidered;
        }
        else
        {
          playerRankDifferential = _maxRankedPlayersConsidered - boundItem.Seqno;
          labRank.Text = "did not rank";
          labAverageRankDifference.Text = _maxRankedPlayersConsidered.ToString();
        }

        // populate the user rank differential
        labUserRankDifference.Text = playerRankDifferential.ToString();

        // populate the average differentials and the user vs average differentials
        var targetDifferential = _playerDifferentials.Where(x => x.PositionCode == this.PositionCode).SingleOrDefault(x => x.PlayerID == boundItem.PlayerId);

        if (targetDifferential != null)
        {
          labAverageRankDifference.Text = targetDifferential.AverageDifferential.ToString("");
          var userVersusAverageDifferential = targetDifferential.AverageDifferential - playerRankDifferential;

          // load the differential and the appropriate styling rule
          if (userVersusAverageDifferential > 0)
          {
            labUserVersusAverage.Text = "+" + userVersusAverageDifferential.ToString(CultureInfo.InvariantCulture);
            e.Row.Attributes.Add("class", "goodRanking");
          }
          else if(userVersusAverageDifferential < 0)
          {
            labUserVersusAverage.Text = userVersusAverageDifferential.ToString(CultureInfo.InvariantCulture);
            e.Row.Attributes.Add("class", "badRanking");
          }
          else
          {
            labUserVersusAverage.Text = userVersusAverageDifferential.ToString(CultureInfo.InvariantCulture);
            e.Row.Attributes.Add("class", "neutralRanking");
          }
        }
        else
        {
          labUserVersusAverage.Text = "?";
        }

        // keep a running total of user rank differentials
        _userRankingDifferentialSum += playerRankDifferential;

      }
      else if (e.Row.RowType == DataControlRowType.Footer)
      {
        // Display the summary data in the appropriate cells
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;

        //e.Row.Cells[0].Text = "Total Rank Differential";
        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
        e.Row.Cells[0].Font.Bold = true;
        e.Row.Cells[0].Attributes.Add("style", "border-right:none");
        e.Row.Cells[0].ColumnSpan = 3;

        e.Row.Cells[3].ColumnSpan = 3;
        e.Row.Cells[3].Font.Bold = true;
        e.Row.Cells[3].Font.Size = 16;
        e.Row.Cells[3].Attributes.Add("style", "border-left:none;padding-left:5px;");

        e.Row.Cells[4].Visible = false;
        e.Row.Cells[5].Visible = false;

        // add total
        Label labTotal = new Label();
        labTotal.Text = _userRankingDifferentialSum.ToString();

        // add label
        Label labTotalDescription = new Label();
        labTotalDescription.Text = "Total Rank Differential";
        labTotalDescription.Attributes.Add("style", "color:#888;font-size:16px;padding-left:7px;");

        e.Row.Cells[3].Controls.AddAt(0, labTotal);
        e.Row.Cells[3].Controls.AddAt(1, labTotalDescription);

      }
    }


    private void GenerateButtons()
    {
      // Generate the season buttons
      GenerateSeasonalButtons();

      // Generate the positional buttons
      GeneratePositionalButtons();

    }



    /// <summary>
    /// Configure the grid so that it generated the controls necessary to work with DataTables
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUserSheet_PreRender(object sender, EventArgs e)
    {
      if (gvUserSheet.Rows.Count > 0)
      {
        //This replaces <td> with <th> and adds the scope attribute
        gvUserSheet.UseAccessibleHeader = true;

        //This will add the <thead> and <tbody> elements
        gvUserSheet.HeaderRow.TableSection = TableRowSection.TableHeader;

        //This adds the <tfoot> element. 
        //Remove if you don't have a footer row
        gvUserSheet.FooterRow.TableSection = TableRowSection.TableFooter;
      }
    }


    protected void repSeasons_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        var boundSeasonInt = (int) e.Item.DataItem;
        var butSeason = (Button)e.Item.FindControl("butSeason");
        var spSeasonButtonContainer = (HtmlControl) e.Item.FindControl("spSeasonButtonContainer");

        // build button
        butSeason.Text = boundSeasonInt.ToString();

        List<string> gradedPositions = GetGradedSheetPositions(boundSeasonInt.ToString());
        if (gradedPositions.Count > 0)
        {
          // if we're looking at the sheet for the season being bound, show a bold font
          if (boundSeasonInt.ToString() == this.SeasonCode)
          {
            butSeason.CssClass = "btn btn-primary";
          }
          else
          {
            // determine we'll send the user to, depending on the positions for which they've been graded
            butSeason.PostBackUrl = "~/fantasy-football/nfl/accuracy/users/user/" + this.Username + "/" + boundSeasonInt.ToString() + "/";
            if (gradedPositions.Contains(FOOPositionsOffense.QB.ToString()))
            {
              butSeason.PostBackUrl += FOOPositionsOffense.QB;
            }
            else if (gradedPositions.Contains(FOOPositionsOffense.RB.ToString()))
            {
              butSeason.PostBackUrl += FOOPositionsOffense.RB;
            }
            else if (gradedPositions.Contains(FOOPositionsOffense.WR.ToString()))
            {
              butSeason.PostBackUrl += FOOPositionsOffense.WR;
            }
            else if (gradedPositions.Contains(FOOPositionsOffense.TE.ToString()))
            {
              butSeason.PostBackUrl += FOOPositionsOffense.TE;
            }
            else if (gradedPositions.Contains(FOOPositionsOffense.K.ToString()))
            {
              butSeason.PostBackUrl += FOOPositionsOffense.K;
            }
            else if (gradedPositions.Contains(FOOPositionsOffense.DF.ToString()))
            {
              butSeason.PostBackUrl += FOOPositionsOffense.DF;
            }

            butSeason.CssClass = "btn btn-default";
          }

        }
        else
        {
          butSeason.CssClass = "btn btn-default";
          spSeasonButtonContainer.Attributes.Add("title", this.Username + " did not create any cheat sheets for the " + butSeason.Text + " fantasy football season.");
          butSeason.Enabled = false;
        }
      }
    }

    protected void gvUserSheet_DataBound(object sender, EventArgs e)
    {
      int gridViewRows = gvUserSheet.Rows.Count;
    }



    private void GenerateSeasonalButtons()
    {
      // Generate Season Buttons
      List<int> gradedSeasons = new List<int>();
      int lastSeason = int.Parse(FOO.LastSeason);
      for (var season = Globals.FirstGradedSeason; season <= lastSeason; season++)
      {
        gradedSeasons.Add(season);
      }
      gradedSeasons.Reverse();
      repSeasons.DataSource = gradedSeasons;
      repSeasons.DataBind();
    }



    private void GeneratePositionalButtons()
    {
      // Generate Positional Buttons
      // QB Button
      if (UserSheetPositionGrade.GetUserSheetPositionGrade(this.Username, this.SeasonCode, FOOPositionsOffense.QB.ToString()) != null)
      {
        butQBSheet.PostBackUrl = "~/fantasy-football/nfl/accuracy/users/user/" + this.Username + "/" + this.SeasonCode + "/QB";
        spQBButtonContainer.Attributes.Add("title", BuildEnabledTooltip(FOOPositionsOffense.QB.ToString()));
      }
      else
      {
        butQBSheet.Enabled = false;
        spQBButtonContainer.Attributes.Add("title", BuildDisabledTooltip(FOOPositionsOffense.QB.ToString()));
      }

      // RB Button
      if (UserSheetPositionGrade.GetUserSheetPositionGrade(this.Username, this.SeasonCode, FOOPositionsOffense.RB.ToString()) != null)
      {
        butRBSheet.PostBackUrl = "~/fantasy-football/nfl/accuracy/users/user/" + this.Username + "/" + this.SeasonCode + "/RB";
        spRBButtonContainer.Attributes.Add("title", BuildEnabledTooltip(FOOPositionsOffense.RB.ToString()));
      }
      else
      {
        butRBSheet.Enabled = false;
        spRBButtonContainer.Attributes.Add("title", BuildDisabledTooltip(FOOPositionsOffense.RB.ToString()));
      }

      // WR Button
      if (UserSheetPositionGrade.GetUserSheetPositionGrade(this.Username, this.SeasonCode, FOOPositionsOffense.WR.ToString()) != null)
      {
        butWRSheet.PostBackUrl = "~/fantasy-football/nfl/accuracy/users/user/" + this.Username + "/" + this.SeasonCode + "/WR";
        spWRButtonContainer.Attributes.Add("title", BuildEnabledTooltip(FOOPositionsOffense.WR.ToString()));
      }
      else
      {
        butWRSheet.Enabled = false;
        spWRButtonContainer.Attributes.Add("title", BuildDisabledTooltip(FOOPositionsOffense.WR.ToString()));
      }

      // TE Button
      if (UserSheetPositionGrade.GetUserSheetPositionGrade(this.Username, this.SeasonCode, FOOPositionsOffense.TE.ToString()) != null)
      {
        butTESheet.PostBackUrl = "~/fantasy-football/nfl/accuracy/users/user/" + this.Username + "/" + this.SeasonCode + "/TE";
        spTEButtonContainer.Attributes.Add("title", BuildEnabledTooltip(FOOPositionsOffense.TE.ToString()));
      }
      else
      {
        butTESheet.Enabled = false;
        spTEButtonContainer.Attributes.Add("title", BuildDisabledTooltip(FOOPositionsOffense.TE.ToString()));
      }

      // K Button
      if (UserSheetPositionGrade.GetUserSheetPositionGrade(this.Username, this.SeasonCode, FOOPositionsOffense.K.ToString()) != null)
      {
        butKSheet.PostBackUrl = "~/fantasy-football/nfl/accuracy/users/user/" + this.Username + "/" + this.SeasonCode + "/K";
        spKButtonContainer.Attributes.Add("title", BuildEnabledTooltip(FOOPositionsOffense.K.ToString()));
      }
      else
      {
        butKSheet.Enabled = false;
        spKButtonContainer.Attributes.Add("title", BuildDisabledTooltip(FOOPositionsOffense.K.ToString()));
      }

      // DF Button
      if (UserSheetPositionGrade.GetUserSheetPositionGrade(this.Username, this.SeasonCode, FOOPositionsOffense.DF.ToString()) != null)
      {
        butDFSheet.PostBackUrl = "~/fantasy-football/nfl/accuracy/users/user/" + this.Username + "/" + this.SeasonCode + "/DF";
        spDFButtonContainer.Attributes.Add("title", BuildEnabledTooltip(FOOPositionsOffense.DF.ToString()));
      }
      else
      {
        butDFSheet.Enabled = false;
        spDFButtonContainer.Attributes.Add("title", BuildDisabledTooltip(FOOPositionsOffense.DF.ToString()));
      }
      
      // style the positional buttons
      if (this.PositionCode == FOOPositionsOffense.QB.ToString())
      {
        butQBSheet.CssClass = "btn btn-primary";
      }
      else if (this.PositionCode == FOOPositionsOffense.RB.ToString())
      {
        butRBSheet.CssClass = "btn btn-primary";
      }
      else if (this.PositionCode == FOOPositionsOffense.WR.ToString())
      {
        butWRSheet.CssClass = "btn btn-primary";
      }
      else if (this.PositionCode == FOOPositionsOffense.TE.ToString())
      {
        butTESheet.CssClass = "btn btn-primary";
      }
      else if (this.PositionCode == FOOPositionsOffense.K.ToString())
      {
        butKSheet.CssClass = "btn btn-primary";
      }
      else if (this.PositionCode == FOOPositionsOffense.DF.ToString())
      {
        butDFSheet.CssClass = "btn btn-primary";
      }
    }

    private string BuildEnabledTooltip(string positionCode)
    {
      return "Click to view " + this.Username.GetPossessive() + " " + this.SeasonCode + " " + Position.GetPosition(positionCode).Name.ToLower() + " cheat sheet.";
    }

    private string BuildDisabledTooltip(string positionCode)
    {
      return this.Username + " did not create a " + Position.GetPosition(positionCode).Name.ToLower() + " cheat sheet for the " + this.SeasonCode +
             " fantasy football season.";
    }


    private List<string> GetGradedSheetPositions(string seasonCode)
    {
      var gradedPositions = new List<string>();

      // QB
      if((UserSheetPositionGrade.GetUserSheetPositionGrade(this.Username, seasonCode, FOOPositionsOffense.QB.ToString()) != null))
      {
        gradedPositions.Add(FOOPositionsOffense.QB.ToString());
      }

      // RB
      if ((UserSheetPositionGrade.GetUserSheetPositionGrade(this.Username, seasonCode, FOOPositionsOffense.RB.ToString()) != null))
      {
        gradedPositions.Add(FOOPositionsOffense.RB.ToString());
      }

      // WR
      if ((UserSheetPositionGrade.GetUserSheetPositionGrade(this.Username, seasonCode, FOOPositionsOffense.WR.ToString()) != null))
      {
        gradedPositions.Add(FOOPositionsOffense.WR.ToString());
      }

      // TE
      if ((UserSheetPositionGrade.GetUserSheetPositionGrade(this.Username, seasonCode, FOOPositionsOffense.TE.ToString()) != null))
      {
        gradedPositions.Add(FOOPositionsOffense.TE.ToString());
      }

      // K
      if ((UserSheetPositionGrade.GetUserSheetPositionGrade(this.Username, seasonCode, FOOPositionsOffense.K.ToString()) != null))
      {
        gradedPositions.Add(FOOPositionsOffense.K.ToString());
      }

      // DF
      if ((UserSheetPositionGrade.GetUserSheetPositionGrade(this.Username, seasonCode, FOOPositionsOffense.DF.ToString()) != null))
      {
        gradedPositions.Add(FOOPositionsOffense.DF.ToString());
      }


      return gradedPositions;
    }


    public class GradedPlayer
    {
      public GradedPlayer(int seqNo, int playerId, string fullName)
      {
        this.Seqno = seqNo;
        this.PlayerId = playerId;
        this.FullName = fullName;
      }

      public int Seqno { get; set; }
      public int PlayerId { get; set; }
      public string FullName { get; set; }
    }

    /// <summary>
    /// A local class used simply to hold a statistical ranking of players from the previous season
    /// </summary>
    public class RankedPlayer
    {
      public RankedPlayer(int rank, Player player)
      {
        this.Rank = rank;
        this.Player = player;
      }

      public int Rank { get; set; }
      public Player Player { get; set; }
    }




  }
}