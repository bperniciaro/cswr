using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class CreateSheetPrintPositionalSheetTemplate : System.Web.UI.UserControl
  {

    /// <summary>
    /// This constand defines how many characters can fit on a single row line
    /// </summary>
    private const int MAX_LINE_LENGTH = 70;

    /// <summary>
    /// This counter is used to control the alternating row styles
    /// </summary>
    private int _playerCounter = 0;
    private int PlayerCounter 
    { 
      get  
      {
       return _playerCounter; 
      }
      set
      {
        _playerCounter = value;
      }
    }

    /// <summary>
    /// This variable holds the ID of the sheet that is being generated in single-sheet positional printable format
    /// </summary>
    public int CheatSheetID { get; set; }


    private CheatSheet CurrentCheatSheet { get; set; }

    /// <summary>
    /// This collection holds a sorted list of CSWR ranks for the purpose of providing a supplemental ranking
    /// </summary>
    private List<SupplementalSheetItem> CSWRRACRankItems {get;set;}

    /// <summary>
    /// This collection holds a list of players sorted by TFP
    /// </summary>
    private List<Player> TFPPlayers { get; set; }



    protected void Page_Load(object sender, EventArgs e)
    {
      if (!this.IsPostBack)
      {
        if (!GetCheatSheet())
        {
          DisplaySheetError();
        }
        else
        {
          ConfigureSheet();
        }
      }
    }

    private void ConfigureSheet()
    {
      // Build into article
      BuildIntroArticle();
      // Get Supplemental Items if a single-position sheet
      if (this.CurrentCheatSheet.Positions.Count == 1)
      {
        GetSupplementalItems();
      }
      // bind the data to the sheet
      BuildSheet();
    }


    private void DisplaySheetError()
    {
      mbMessageBox.MessageType = MessageType.ERROR;
      mbMessageBox.Message = new StringBuilder("Sheet Not Found");
      mbMessageBox.WidthPercentage = 30;
      panRACSheetSummary.Visible = false;
      panFOOSheetSummary.Visible = false;
    }

    private bool GetCheatSheet()
    {
      // get the current cheat sheet being printed
      if (this.CheatSheetID != 0)
      {
        CheatSheet currentSheet = CheatSheet.GetCheatSheet(this.CheatSheetID);
        if (currentSheet != null)
        {
          this.CurrentCheatSheet = currentSheet;
          return true;
        }
      }
      return false;
    }


    private void BuildIntroArticle()
    {
      switch (this.CurrentCheatSheet.SportCode)
      {
        case "FOO":
          panRACSheetSummary.Visible = false;
          BuildFOOIntroArticle();
          break;
        case "RAC":
          panFOOSheetSummary.Visible = false;
          BuildRACIntroArticle();
          break;
      }
    }

    private void BuildFOOIntroArticle()  
    {
      if (!(bool)this.CurrentCheatSheet.MappedProperties[CSProperty.PPRLeague.ToString()])
      {
        labScoringConfiguration.Text = "standard";
      }
      else
      {
        labScoringConfiguration.Text = "PPR";
      }
      // if this is a football sheet we don't need as many slots for a roster to
      // enter positional picks
      trRosterSecondRow.Visible = false;
      hlSheetName2.Text = this.CurrentCheatSheet.SheetName;

      // load content labels
      Position currentPosition = Position.GetPosition(this.CurrentCheatSheet.Positions[0].PositionCode);
      litPosition.Text = "My " + currentPosition.Name + "s";
      litPosition2.Text = currentPosition.Name.ToLower();
      labSuppStatSeason.Text = this.CurrentCheatSheet.StatsSeasonCode;

      // load a reference to the cheat sheet
      hlSheetName2.Text = this.CurrentCheatSheet.SheetName;
      hlSheetName2.NavigateUrl = "~/fantasy-football/nfl/create/custom-sheet.aspx?SheetID=" + this.CurrentCheatSheet.CheatSheetID.ToString();
    }

    private void BuildRACIntroArticle()  
    {
      // load content labels
      litPosition.Text = "Drivers";

      // load a reference to the cheat sheet
      hlSheetName.Text = this.CurrentCheatSheet.SheetName;
      hlSheetName.NavigateUrl = "~/fantasy-racing/nascar/create/custom-sheet.aspx?SheetID=" + this.CurrentCheatSheet.CheatSheetID.ToString();
    }



    private void GetSupplementalItems()
    {
      string seasonCode = SportSeason.GetCurrentSportSeason(this.CurrentCheatSheet.SportCode).SeasonCode;
      int cswrSuppSourceID = SupplementalSource.GetSupplementalSource("CSWR").SupplementalSourceID;
      string sportCode = this.CurrentCheatSheet.SportCode;
      string positionCode = this.CurrentCheatSheet.Positions[0].PositionCode;

      SupplementalSheet cswrSuppSheet = SupplementalSheet.GetSupplementalSheet(seasonCode, cswrSuppSourceID, sportCode, positionCode);

      switch (this.CurrentCheatSheet.SportCode)
      {
        case "FOO":
          // total fantasy points as supplemental stat
          string supplementalStat = String.Empty;
          if (!(bool)this.CurrentCheatSheet.MappedProperties[CSProperty.PPRLeague.ToString()])
          {
            supplementalStat = "TFPR";
          }
          else
          {
            labScoringConfiguration.Text = "PPR";
          }
          SortDir sortDir = SortDir.ASC;
          this.TFPPlayers = Player.GetPlayers(sportCode, this.CurrentCheatSheet.StatsSeasonCode, positionCode, false, supplementalStat, sortDir.ToString());
          break;
        case "RAC":
          this.CSWRRACRankItems = SupplementalSheetItem.GetSupplementalSheetItems(cswrSuppSheet.SupplementalSheetID);
          break;
      }
    }


    public void BuildSheet()
    {
      // test bye weeks to see if they are available (should add a dedicated SP for this)
      if (ByeWeek.GetByeWeek(FOO.CurrentSeason, this.CurrentCheatSheet.SportCode, "NOSN") != null)
      {
        labByeWeeksIncluded.Visible = true;
      }

      // build the sheet based on the sportcode involved
      switch (this.CurrentCheatSheet.SportCode)
      {
        case "FOO":
          BuildFOOSheet();
          break;
        case "RAC":
          BuildRACSheet();
          break;
      }
    }


    public void BuildRACSheet()
    {
      // dynamically assign the item databound events
      repPlayersLeftSide.ItemDataBound += new RepeaterItemEventHandler(repDrivers_ItemDataBound);
      repPlayersRightSide.ItemDataBound += new RepeaterItemEventHandler(repDrivers_ItemDataBound);

      // configure left side of printable sheet
      List<CheatSheetItem> sheetItems = CheatSheetItem.GetCheatSheetItems(this.CurrentCheatSheet.CheatSheetID);

      // determine the maximum number of items to display
      int maxPlayersPerSheet = Globals.CswrMaxPrintableSingleSheetRows * 2;
      if (sheetItems.Count > maxPlayersPerSheet)
      {
        sheetItems = sheetItems.Take(maxPlayersPerSheet).ToList();
      }

      // create a copy of the cheat sheet items
      if (sheetItems.Count > Globals.CswrMaxPrintableSingleSheetRows)
      {
        // bind the left column
        repPlayersLeftSide.DataSource = sheetItems.GetRange(0, Globals.CswrMaxPrintableSingleSheetRows);
        repPlayersLeftSide.DataBind();

        // bind the right column
        if ((sheetItems.Count - Globals.CswrMaxPrintableSingleSheetRows) > 0)
        {
          repPlayersRightSide.DataSource = sheetItems.GetRange(Globals.CswrMaxPrintableSingleSheetRows, sheetItems.Count - Globals.CswrMaxPrintableSingleSheetRows);
          repPlayersRightSide.DataBind();
        }
      }
      else
      {
        panRightColumn.Visible = false;
        panLeftColumn.CssClass = "driverOneColumnContainer";
        repPlayersLeftSide.DataSource = sheetItems;
        repPlayersLeftSide.DataBind();
      }
    }


    public void BuildFOOSheet()
    {
      // dynamically assign the item databound events
      repPlayersLeftSide.ItemDataBound += new RepeaterItemEventHandler(repPlayers_ItemDataBound);
      repPlayersRightSide.ItemDataBound += new RepeaterItemEventHandler(repPlayers_ItemDataBound);

      // get a list of all items to display
      List<CheatSheetItem> sheetItems = CheatSheetItem.GetCheatSheetItems(this.CurrentCheatSheet.CheatSheetID);

      // determine the maximum number of items to display
      int maxPlayersPerSheet = Globals.CswrMaxPrintableSingleSheetRows * 2;
      if (sheetItems.Count > maxPlayersPerSheet)
      {
        sheetItems = sheetItems.Take(maxPlayersPerSheet).ToList();
      }
      
      // create a copy of the cheat sheet items
      if (sheetItems.Count > Globals.CswrMaxPrintableSingleSheetRows)
      {
        // bind the left column
        repPlayersLeftSide.DataSource = sheetItems.GetRange(0, Globals.CswrMaxPrintableSingleSheetRows);
        repPlayersLeftSide.DataBind();

        // bind the right column
        if ((sheetItems.Count - Globals.CswrMaxPrintableSingleSheetRows) > 0)
        {
          repPlayersRightSide.DataSource = sheetItems.GetRange(Globals.CswrMaxPrintableSingleSheetRows, sheetItems.Count - Globals.CswrMaxPrintableSingleSheetRows);
          repPlayersRightSide.DataBind();
        }
      }
      else
      {
        panRightColumn.Visible = false;
        panLeftColumn.CssClass = "driverOneColumnContainer";
        repPlayersLeftSide.DataSource = sheetItems;
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
        // get a reference to the data being bound
        CheatSheetItem boundItem = (CheatSheetItem)e.Item.DataItem;
        // get references to the relevant controls
        Label labRank = (Label)e.Item.FindControl("labRank");
        Label labPlayerName = (Label)e.Item.FindControl("labPlayerName");
        Label labNote = (Label)e.Item.FindControl("labNote");
        Label labSuppRanking = (Label)e.Item.FindControl("labSuppRanking");
        Label labTeam = (Label)e.Item.FindControl("labTeam");
        Label labByeWeek = (Label)e.Item.FindControl("labByeWeek");
        HtmlControl parContainer = (HtmlControl)e.Item.FindControl("parContainer");
        Image imaSleeper = (Image)e.Item.FindControl("imaSleeper");
        Image imaBust = (Image)e.Item.FindControl("imaBust");
        Image imaInjured = (Image)e.Item.FindControl("imaInjured");

        // driver name
        if (boundItem.Player.PositionCode != "DF")
        {
          labPlayerName.Text = boundItem.Player.FullName;
        }
        else
        {
          labPlayerName.Text = boundItem.Player.FirstName;
        }


        // determine the TFP rank for this player
        if (this.CurrentCheatSheet.Positions.Count == 1)
        {
          int i = 0;
          bool playerFound = false;
          foreach (Player currentPlayer in this.TFPPlayers)
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
        }

        // team
        Team playerTeam = Team.GetTeam(boundItem.Player.TeamCode);
        labTeam.Text = playerTeam.Abbreviation;
        // bye week
        ByeWeek targetByWeek = ByeWeek.GetByeWeek(this.CurrentCheatSheet.SeasonCode, this.CurrentCheatSheet.SportCode, playerTeam.TeamCode);
        if (targetByWeek != null)  
        {
          labByeWeek.Text = "[" + targetByWeek.Bye.ToString() + "]";
        }

        // tags
        imaSleeper.Visible = (bool)boundItem.MappedProperties[CSIProperty.Sleeper.ToString()];
        imaBust.Visible = (bool)boundItem.MappedProperties[CSIProperty.Bust.ToString()];
        imaInjured.Visible = (bool)boundItem.MappedProperties[CSIProperty.Injured.ToString()];

        // add the note
        if (boundItem.Note != String.Empty)
        {
          labNote.Text = "\"" + boundItem.Note + "\"";
        }


        // style the rows
        this.PlayerCounter++;
        labRank.Text = this.PlayerCounter.ToString();
        if (this.PlayerCounter % 2 == 1)
        {
          parContainer.Attributes.Add("class", "alternatingItem");
        }

      }
    }



    protected void repDrivers_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        // the racing item being bound
        CheatSheetItem boundItem = (CheatSheetItem)e.Item.DataItem;
        // get references to the relevant controls
        Label labPlayerName = (Label)e.Item.FindControl("labPlayerName");
        Label labNote = (Label)e.Item.FindControl("labNote");
        Label labSuppRanking = (Label)e.Item.FindControl("labSuppRanking");
        Label labRank = (Label)e.Item.FindControl("labRank");
        HtmlControl parContainer = (HtmlControl)e.Item.FindControl("parContainer");

        // driver name
        labPlayerName.Text = boundItem.Player.FullName;

        // driver note
        if (boundItem.Note != String.Empty)
        {
          int remainingLineSpace = MAX_LINE_LENGTH - boundItem.Player.FullName.Length;
          if (boundItem.Note.Length < remainingLineSpace)
          {
            labNote.Text = "\"" + boundItem.Note + "\"";
          }
          else
          {
            labNote.Text = "\"" + boundItem.Note.Substring(0, remainingLineSpace) + "..\"";
          }
        }

        // determine CSWR's supplemental ranking
        SupplementalSheetItem foundItem = this.CSWRRACRankItems.Find((delegate(SupplementalSheetItem targetSuppSheetItem) { return (targetSuppSheetItem.PlayerID == boundItem.PlayerID); }));
        if (foundItem != null)
        {
          labSuppRanking.Text = "(" + foundItem.Seqno.ToString() + ")";
        }
        else
        {
          labSuppRanking.Text = "(n/r)";
        }

        // increment the counter so we can configure the alternating color rows
        this.PlayerCounter++;
        labRank.Text = this.PlayerCounter.ToString();
        if (this.PlayerCounter % 2 == 1)
        {
          parContainer.Attributes.Add("class", "alternatingItem");
        }

      }
    }
  }
}