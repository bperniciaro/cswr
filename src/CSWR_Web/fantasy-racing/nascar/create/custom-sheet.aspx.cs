using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Blog;
using BP.CheatSheetWarRoom.BLL.Sheets;
using BP.CheatSheetWarRoom.UI.UserControls;
using CarlosAg.ExcelXmlWriter;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class FantasyRacingCustomSheet : BasePage
  {

    //public CheatSheet CurrentCheatSheet
    //{
    //  get
    //  {
    //    return (ViewState["CurrentCheatSheet"] == null) ? null : (CheatSheet)ViewState["CurrentCheatSheet"];
    //  }
    //  set
    //  {
    //    ViewState["CurrentCheatSheet"] = value;
    //  }
    //}


    public int CurrentCheatSheetID
    {
      get
      {
        return (ViewState["CurrentCheatSheetID"] == null) ? 0 : (int)ViewState["CurrentCheatSheetID"];
      }
      set
      {
        ViewState["CurrentCheatSheetID"] = value;
      }
    }

    /// <summary>
    /// Register JavaScript note editing functions
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        SessionHandler.CurrentSportCode = "RAC";
      }
    }

    /// <summary>
    /// This is the main event that will be called on each page load.  
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
      // we only want to bind the cheat sheet on initial page load, after that we load
      // from session state...
      if (!this.IsPostBack)
      {
        // if the user is logged in
        if (this.User.Identity.IsAuthenticated)
        {
          // hide svisitor navigation
          svnNavigation.Visible = false;
          // show member navigation
          scmlnNavigation.Visible = true;

          // Load the dropdownlist with  all user sheets
          List<CheatSheet> availableSheets = CheatSheet.GetUserCheatSheets(this.User.Identity.Name, SessionHandler.CurrentSportCode);
          if (availableSheets.Count > 0)
          {
            ddlAvailableSheets.Visible = true;
            ddlAvailableSheets.DataSource = availableSheets;
            ddlAvailableSheets.DataTextField = "SheetName";
            ddlAvailableSheets.DataValueField = "CheatSheetID";
            ddlAvailableSheets.DataBind();

            // Don't show the 'no sheets' message
            mbNoSheets.Visible = false;

            // load the cheat sheet based on the dropdownlist value
            BindCheatSheet(int.Parse(ddlAvailableSheets.SelectedValue));
          }
          // if the user has no cheat sheets, show a message
          else
          {
            // Build the 'no sheets' message
            LoadNoSheetsMessage();

            // Hide the member navigation
            scmlnNavigation.Visible = false;

            panSheetHeaderControls.Visible = false;
          }
        }
        // if the user is a visitor
        else
        {
          // create a 'dummy' sheet so that buttons will look normally spaced if the user is a visitor
          ddlAvailableSheets.Items.Add(new ListItem("My Drivers", "0"));

          // if it's a visitor sheet, show the 'help' buttons
          svnNavigation.Visible = true;
          scmlnNavigation.Visible = false;

          // create or load the appropriate cheat sheet
          LoadVisitorCheatSheet();
        }
      }

      // load the instructional popup, if necessary
      LoadInstructionalPopup();
      // declare an event handler to handle export requests
      scplnPageLevelNavigation.ExportEvent += new Globals.ExportEventHandler(CustomSheet_ExportHandler);
      // determine advertisement display
      //DetermineAdDisplay();
    }


    

    private void LoadNoSheetsMessage()  
    {
      StringBuilder sbNoSheets = new StringBuilder();
      sbNoSheets.Append("You do not have any saved " + SportSeason.GetCurrentSportSeason(SessionHandler.CurrentSportCode).SeasonCode + " sheets.  To get started please ");
      sbNoSheets.Append("<a href='newsheet.aspx'>create a new sheet</a>.");
      mbNoSheets.Message = sbNoSheets;
      mbNoSheets.MessageType = MessageType.INSTRUCTIONS;
    }


    /// <summary>
    /// Based on the cheat sheet to bind, we bind all of the cheat sheet items to the ReorderList control
    /// </summary>
    /// <returns></returns>
    private void BindCheatSheet(int cheatSheetID)
    {
      this.CurrentCheatSheetID = cheatSheetID;
      CheatSheet currentCheatSheet = CheatSheet.GetCheatSheet(cheatSheetID);

      // validate it has no errors      
      currentCheatSheet.Validate();
      
      // load the cheat sheet items based on the id passed into this method
      List<CheatSheetItem> cheatSheetItems = CheatSheetItem.GetCheatSheetItems(cheatSheetID);
      repDriverSheet.DataSource = cheatSheetItems;
      repDriverSheet.DataBind();
    }


    /// <summary>
    /// In this method we either create the visitor cheat sheet (during their first visit) or load the visitor cheat
    /// sheet from the database if it has already been created.
    /// </summary>
    /// <returns></returns>
    private void LoadVisitorCheatSheet()
    {
      List<CheatSheetItem> cheatSheetItems = new List<CheatSheetItem>();
      CheatSheet visitorCheatSheet = new CheatSheet();
      List<Position> cheatSheetPositions = new List<Position>();
      List<Stat> cheatSheetStats = new List<Stat>();
      int newSheetID = 0;
      string temporarySheetName = String.Empty;
      string seasonCode = SportSeason.GetCurrentSportStatSeason(SessionHandler.CurrentSportCode).SeasonCode;

      // if no visitor cheat sheet has been created, create one based on the QB position
      if (SessionHandler.GetCurrentVisitorSheetPosition(SessionHandler.CurrentSportCode) == null)
      {
        //need to create the QB sheet and load it
        Position visitorSheetPosition = Position.GetPosition("DR");
        cheatSheetPositions.Add(visitorSheetPosition);
        cheatSheetStats = Helpers.GetDefaultStatCodes("DR");
        // create the first visitor sheet, based on CSWR ranking
        SupplementalSource targetSource = SupplementalSource.GetSupplementalSource("CSWR");
        Dictionary<string, object> emptyProperties = new Dictionary<string, object>();
        newSheetID = CheatSheet.CreateCheatSheet(SessionHandler.CurrentSportCode, "My " + visitorSheetPosition.Name + "s", seasonCode, cheatSheetPositions, 
                                                    cheatSheetStats, targetSource.SupplementalSourceID, emptyProperties);
        // store the ID of the visitor sheet should we need it later (i.e. the user goes back to this sheet)
        Session[SessionHandler.CurrentSportCode + "_" + "DR"] = newSheetID;
        // store the position code of the current visitor sheet being modified
        SessionHandler.SetCurrentVisitorSheetPosition(SessionHandler.CurrentSportCode, "DR");
        // load the new visitor cheat sheet
        visitorCheatSheet = CheatSheet.GetCheatSheet(newSheetID);
      }
      // if at least one cheat sheet exists, load it
      else
      {
        visitorCheatSheet = CheatSheet.GetCheatSheet((int)Session[SessionHandler.CurrentSportCode + "_" + SessionHandler.GetCurrentVisitorSheetPosition(SessionHandler.CurrentSportCode)]);
      }

      // bind the visitor sheet to the ReorderList control
      BindCheatSheet(visitorCheatSheet.CheatSheetID);
    }


    /// <summary>
    /// This method is fired each time the items are reordered
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void rolReorderList_ItemReorder(object sender, ReorderListItemReorderEventArgs e)
    //{
    //  // issue the reorder call to the business layer
    //  if (this.User.Identity.IsAuthenticated)
    //  {
    //    // we know the id of the sheet is determined by the dropdownlist
    //    CheatSheet.ReorderCheatSheetItems(int.Parse(ddlAvailableSheets.SelectedValue), e.OldIndex, e.NewIndex);
    //    // once the user reorders once, we note that they no longer need to see the instructional message
    //    Profile.FiguredOutReordering = true;
    //  }
    //  else
    //  {
    //    // we know the id of the sheet by what is stored in a session variable
    //    CheatSheet.ReorderCheatSheetItems((int)Session[SessionHandler.CurrentSportCode + "_" + SessionHandler.GetCurrentVisitorSheetPosition(SessionHandler.CurrentSportCode)], e.OldIndex, e.NewIndex);
    //    // once the visitor reorders once, we note that they no longer need to see the instructional message
    //    SessionHandler.FiguredOutReordering = true;
    //  }
    //}

    /// <summary>
    /// As we initially bind the collection of cheat sheet items to the ReorderList, we pass the item to the respective player item template
    /// so it can load the player's information
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void rolReorderList_ItemDataBound(object sender, AjaxControlToolkit.ReorderListItemEventArgs e)
    //{

    //  // First, get a reference to the cheat sheet item being bound
    //  CheatSheetItem cheatSheetItem = (CheatSheetItem)(e.Item.DataItem);

    //  // Second, get a reference to the player template currently being manipulated
    //  ReorderListItem currentItem = (ReorderListItem)(e.Item);
    //  RACSheetItemTemplate currentPlayerTemplate = (RACSheetItemTemplate)currentItem.FindControl("ptPlayerTemplate");

    //  // load the player into the template for display
    //  currentPlayerTemplate.CheatSheetPlayerItem = cheatSheetItem;
    //  currentPlayerTemplate.BuildControlContent(); 
    //}


    /// <summary>
    /// Each time the user selects a different sheet using the dropdownlist we need to rebind the respective sheet
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlAvailableSheets_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.User.Identity.IsAuthenticated)
      {
        int dropDownCheatSheetID = int.Parse(ddlAvailableSheets.SelectedValue);

        BindCheatSheet(dropDownCheatSheetID);
        SaveCurrentSheetIDInProfile(dropDownCheatSheetID);
        // set the edit button of the navigation control
        scmlnNavigation.CheatSheetID = dropDownCheatSheetID;
        scplnPageLevelNavigation.CheatSheetID = dropDownCheatSheetID;
      }
      else
      {
        // store the position of the visitor sheet to be manipulated
        SessionHandler.SetCurrentVisitorSheetPosition(SessionHandler.CurrentSportCode, ddlAvailableSheets.SelectedValue);
        LoadVisitorCheatSheet();
      }
    }


    /// <summary>
    /// This event fires each time the list of available cheat sheets is bound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlAvailableSheets_DataBound(object sender, EventArgs e)
    {
      // if the user is authenticated, we load a sheet from the database
      if (this.User.Identity.IsAuthenticated)
      {
        // if the querystring is empty its the first time in and we need to figure out which sheet to load
        if (this.Request.QueryString["SheetID"] == null)
        {
          // if no profile variable (which notes the last sheet edited by the user) has NOT been saved, select the 
          // first item in the dropdownlist and put it in the profile variable
          if (Profile.Racing.LastRacingCheatSheetID == 0)
          {
            ddlAvailableSheets.SelectedIndex = 0;
            ddlAvailableSheets.SelectedValue = ddlAvailableSheets.Items[0].Value;
            SaveCurrentSheetIDInProfile(int.Parse(ddlAvailableSheets.Items[0].Value));
          }
          // if the 'most recently edited sheet' profile variable isn't empty, select it in the dropdownlist
          else
          {
            if (CheatSheet.GetCheatSheet(Profile.Racing.LastRacingCheatSheetID) != null)
            {
              ddlAvailableSheets.SelectedValue = Profile.Racing.LastRacingCheatSheetID.ToString();
            }
          }
        }
        // if the querystring isn't empty, we load that cheat sheet specified in the querystring and store its id in the 'most recent sheet' profile variable
        else
        {
          ddlAvailableSheets.SelectedValue = this.Request.QueryString["SheetID"];
          SaveCurrentSheetIDInProfile(int.Parse(this.Request.QueryString["SheetID"]));
        }
        // build the 'edit' link to the sheet selected in the dropdownlist
        scmlnNavigation.CheatSheetID = int.Parse(ddlAvailableSheets.SelectedValue);
        scplnPageLevelNavigation.CheatSheetID = int.Parse(ddlAvailableSheets.SelectedValue);
      }
      // if the user isn't authenticated, we load the dropdown based on session variables
      else
      {
        // if this is the first time in for the visitor, select 'QB' first
        if (SessionHandler.GetCurrentVisitorSheetPosition(SessionHandler.CurrentSportCode) == null)
        {
          ddlAvailableSheets.SelectedValue = "QB";
        }
        // if the visitor has already created a sheet, load it from the session variable
        else
        {
          ddlAvailableSheets.SelectedValue = SessionHandler.GetCurrentVisitorSheetPosition(SessionHandler.CurrentSportCode);
        }
      }
    }

    /// <summary>
    /// This method stores a cheat sheet id in a profile variable.  This allows us to note which cheat
    /// cheat was edited last so that we can re-load it the next time the user starts a session
    /// </summary>
    /// <param name="sheetID"></param>
    private void SaveCurrentSheetIDInProfile(int sheetID)
    {
      Profile.Racing.LastRacingCheatSheetID = sheetID;
    }

    /// <summary>
    /// This event is fired each time the repeater for the number list is bound.  It just allows
    /// us to sequentially order the cheat sheet items to simulate a numbered sheet
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void repNumberList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        Label seqNoLabel = (Label)e.Item.FindControl("labSeqno");
        seqNoLabel.Text = e.Item.DataItem.ToString();
      }
    }



    /// <summary>
    /// This method applies a background image which gives the cheat sheet the 'sheet' look
    /// </summary>
    /// <param name="totalItems"></param>
    //private void BindBackgroundImage(int totalItems)
    //{
    //  int[] seqNoArray = new int[totalItems];
    //  for (int i = 0; i < totalItems; i++)
    //  {
    //    seqNoArray[i] = i + 1;
    //  }
    //  repNumberList.DataSource = seqNoArray;
    //  repNumberList.DataBind();
    //}

    /// <summary>
    /// This method is initiated via a callback (AJAX).  It allows the user to delete a note
    /// for a particular player
    /// </summary>
    /// <param name="serviceArgument">Holds the cheat sheet and the player ID for the item being modified</param>
    [System.Web.Services.WebMethod]
    public static void DeleteNote(string serviceArgument)
    {
      string[] serviceArguments = serviceArgument.Split('-');
      int cheatSheetID = int.Parse(serviceArguments[0]);
      int playerID = int.Parse(serviceArguments[1]);

      CheatSheetItem targetItem = CheatSheetItem.GetCheatSheetItem(cheatSheetID, playerID);
      targetItem.Note = String.Empty;
      targetItem.Update();
    }

    /// <summary>
    /// This method is called via a callback (AJAX) and allows the user to save a note
    /// for a player
    /// </summary>
    /// <param name="serviceArgument">Holdes the cheat sheet ID and player ID for the item being modified</param>
    /// <param name="note">The note to be saved</param>
    [System.Web.Services.WebMethod]
    public static void SaveNote(string serviceArgument, string note)
    {
      string[] serviceArguments = serviceArgument.Split('-');
      int cheatSheetID = int.Parse(serviceArguments[0]);
      int playerID = int.Parse(serviceArguments[1]);

      CheatSheetItem targetItem = CheatSheetItem.GetCheatSheetItem(cheatSheetID, playerID);
      targetItem.Note = note;
      targetItem.Update();
    }


    public void CustomSheet_ExportHandler(object sender, Globals.ExportEventArgs e)
    {
      CheatSheet currentCheatSheet = CheatSheet.GetCheatSheet(this.CurrentCheatSheetID);

      Workbook book = new Workbook();

      // Specify which Sheet should be opened and the size of window by 
      book.ExcelWorkbook.ActiveSheetIndex = 1;
      book.ExcelWorkbook.WindowTopX = 100;
      book.ExcelWorkbook.WindowTopY = 200;
      book.ExcelWorkbook.WindowHeight = 7000;
      book.ExcelWorkbook.WindowWidth = 8000;

      // Some optional properties of the Document
      book.Properties.Author = "Cheat Sheet War Room";
      book.Properties.Title = ddlAvailableSheets.SelectedItem.Text;
      book.Properties.Created = DateTime.Now;

      // Create the Default Style for all cells
      WorksheetStyle defaultStyle = book.Styles.Add("DefaultStyle");
      defaultStyle.Font.FontName = "Tahoma";
      defaultStyle.Font.Size = 10;
      defaultStyle.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
      defaultStyle.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
      defaultStyle.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
      defaultStyle.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);

      // Create the Sleeper Style for sleepers
      WorksheetStyle sleeperStyle = book.Styles.Add("SleeperStyle");
      sleeperStyle.Font.FontName = "Tahoma";
      sleeperStyle.Font.Size = 10;
      sleeperStyle.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
      sleeperStyle.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
      sleeperStyle.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
      sleeperStyle.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);
      sleeperStyle.Font.Color = "White";
      sleeperStyle.Interior.Color = "#03be02";
      sleeperStyle.Interior.Pattern = StyleInteriorPattern.Solid;

      // Create the Bust Style for busts
      WorksheetStyle bustStyle = book.Styles.Add("BustStyle");
      bustStyle.Font.FontName = "Tahoma";
      bustStyle.Font.Size = 10;
      bustStyle.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
      bustStyle.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
      bustStyle.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
      bustStyle.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);
      bustStyle.Font.Color = "White";
      bustStyle.Interior.Color = "#ed3b3f";
      bustStyle.Interior.Pattern = StyleInteriorPattern.Solid;

      // Create the Injured Style for injured
      WorksheetStyle injuredStyle = book.Styles.Add("InjuredStyle");
      injuredStyle.Font.FontName = "Tahoma";
      injuredStyle.Font.Size = 10;
      injuredStyle.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
      injuredStyle.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
      injuredStyle.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
      injuredStyle.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);
      injuredStyle.Font.Color = "White";
      injuredStyle.Interior.Color = "#f78909";
      injuredStyle.Interior.Pattern = StyleInteriorPattern.Solid;

      // Create the Title Style
      WorksheetStyle titleStyle = book.Styles.Add("TitleStyle");
      titleStyle.Font.Size = 14;
      titleStyle.Font.Bold = true;
      titleStyle.Alignment.Horizontal = StyleHorizontalAlignment.Center;
      titleStyle.Font.Color = "Black";
      titleStyle.Interior.Color = "#94dafa";
      titleStyle.Interior.Pattern = StyleInteriorPattern.Solid;
      titleStyle.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
      titleStyle.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
      titleStyle.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
      titleStyle.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);

      // Create the Header Style
      WorksheetStyle headerStyle = book.Styles.Add("HeaderStyle");
      headerStyle.Font.FontName = "Tahoma";
      headerStyle.Font.Size = 10;
      headerStyle.Font.Bold = true;
      headerStyle.Alignment.Horizontal = StyleHorizontalAlignment.Center;
      headerStyle.Font.Color = "Black";
      headerStyle.Interior.Color = "#adfb81";
      headerStyle.Interior.Pattern = StyleInteriorPattern.Solid;
      headerStyle.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
      headerStyle.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
      headerStyle.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
      headerStyle.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);

      // Create an Italic Style
      WorksheetStyle italicStyle = book.Styles.Add("ItalicStyle");
      italicStyle.Font.Italic = true;
      italicStyle.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1);
      italicStyle.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1);
      italicStyle.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1);
      italicStyle.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1);


      // Get a reference to the main sheet
      Worksheet sheet = book.Worksheets.Add(ddlAvailableSheets.SelectedItem.Text);

      // Build the title in the first row
      WorksheetRow titleRow = sheet.Table.Rows.Add();
      WorksheetCell titleCell = titleRow.Cells.Add(ddlAvailableSheets.SelectedItem.Text);
      titleCell.MergeAcross = 17;
      titleCell.StyleID = "TitleStyle";

      // Supplemental Source 1 
      SupplementalSource supplementalSource = SupplementalSource.GetSupplementalSource("CSWR");
      SupplementalSheet supplementalSheet = SupplementalSheet.GetSupplementalSheet(currentCheatSheet.SeasonCode, supplementalSource.SupplementalSourceID, SessionHandler.CurrentSportCode, currentCheatSheet.Positions[0].PositionCode);
      
      WorksheetRow row = sheet.Table.Rows.Add();
      // Rank
      sheet.Table.Columns.Add(new WorksheetColumn(30));
      row.Cells.Add(new WorksheetCell("Rank", "HeaderStyle"));
      // ADP
      sheet.Table.Columns.Add(new WorksheetColumn(30));
      row.Cells.Add(new WorksheetCell("ADP", "HeaderStyle"));
      // CSWR Rank
      sheet.Table.Columns.Add(new WorksheetColumn(35));
      row.Cells.Add(new WorksheetCell(supplementalSource.Abbreviation, "HeaderStyle"));
      // Car Number
      sheet.Table.Columns.Add(new WorksheetColumn(30));
      row.Cells.Add(new WorksheetCell("Num", "HeaderStyle"));
      // Driver
      sheet.Table.Columns.Add(new WorksheetColumn(115));
      row.Cells.Add(new WorksheetCell("Driver", "HeaderStyle"));
      // Experience
      sheet.Table.Columns.Add(new WorksheetColumn(25));
      row.Cells.Add(new WorksheetCell("Exp", "HeaderStyle"));
      // Car Make
      sheet.Table.Columns.Add(new WorksheetColumn(55));
      row.Cells.Add(new WorksheetCell("Make", "HeaderStyle"));

      // Stats
      List<Stat> sheetStats = Stat.GetStats(SessionHandler.CurrentSportCode, "DR");
      foreach (Stat currentStat in sheetStats)
      {
        if (currentStat.StatCode != "ADP")
        {
          if (currentStat.StatCode == "WNGS")
          {
            sheet.Table.Columns.Add(new WorksheetColumn(60));
            row.Cells.Add(new WorksheetCell(currentStat.Abbreviation, "HeaderStyle"));
          }
          else
          {
            sheet.Table.Columns.Add(new WorksheetColumn(30));
            row.Cells.Add(new WorksheetCell(currentStat.Abbreviation, "HeaderStyle"));
          }
        }
      }
      // Note
      sheet.Table.Columns.Add(new WorksheetColumn(250));
      row.Cells.Add(new WorksheetCell("Note", "HeaderStyle"));

      // generate the exportable spreadsheet...
      int i = 1;


      // cast the cheat sheet as a RACCheatSheet so we can iterate over the items
      CheatSheet currentSheet = CheatSheet.GetCheatSheet(currentCheatSheet.CheatSheetID);
      List<CheatSheetItem> cheatSheetItems = CheatSheetItem.GetCheatSheetItems(currentSheet.CheatSheetID);

      foreach (CheatSheetItem currentItem in cheatSheetItems)
      {
        // Stats
        List<SportSeasonPlayerSeasonStat> playerStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("RAC", currentCheatSheet.StatsSeasonCode, currentItem.PlayerID);

        SportSeasonPlayerSeasonStat sportStat = playerStats.Find((delegate(SportSeasonPlayerSeasonStat targetStat) { return (targetStat.StatCode == "ADP"); }));


        row = sheet.Table.Rows.Add();
        // Rank
        row.Cells.Add(new WorksheetCell(i.ToString(), "DefaultStyle"));
        // ADP
        if (sportStat != null)
        {
          row.Cells.Add(new WorksheetCell(Math.Round(sportStat.StatValue, 1).ToString(), "DefaultStyle"));
        }
        else
        {
          row.Cells.Add(new WorksheetCell("n/a", "DefaultStyle"));
        }
        // CSWR Rank
        int supplementalRank = SupplementalSheetItem.GetSupplementalSheetItem(supplementalSheet.SupplementalSheetID, currentItem.PlayerID).Seqno;
        row.Cells.Add(new WorksheetCell(supplementalRank.ToString(), "DefaultStyle"));
        // Car Number
        row.Cells.Add(new WorksheetCell(currentItem.Player.Number.ToString(), "DefaultStyle"));
        // Full Name
        row.Cells.Add(new WorksheetCell(currentItem.Player.FullName, "DefaultStyle"));
        // Experience
        row.Cells.Add(new WorksheetCell(currentItem.Player.YearsExperience.ToString(), "DefaultStyle"));
        // Experience
        row.Cells.Add(new WorksheetCell(Team.GetTeam(currentItem.Player.TeamCode).FullTeamName, "DefaultStyle"));
        i++;
        if (playerStats.Count > 0)
        {
          foreach (SportSeasonPlayerSeasonStat currentStat in playerStats)
          {
            if (currentStat.StatCode != "ADP")
            {
              if (currentStat.StatCode == "WNGS")
              {
                row.Cells.Add(new WorksheetCell(currentStat.StatValue.ToFormattedMoney(), "DefaultStyle"));
              }
              else
              {
                row.Cells.Add(new WorksheetCell(currentStat.StatValue.ToString(), "DefaultStyle"));
              }
            }
          }
        }
        else
        {
          for (int j = 0; j < sheetStats.Count; j++)
          {
            row.Cells.Add(new WorksheetCell("n/a", "DefaultStyle"));
          }
        }
        // Experience
        row.Cells.Add(new WorksheetCell(currentItem.Note, "DefaultStyle"));
      }

      Response.ContentType = "application/vnd.ms-excel";
      Response.Charset = String.Empty;
      Response.AddHeader("content-disposition", "attachment;filename=" + ddlAvailableSheets.SelectedItem.Text.Replace(" ", "") + ".xls");
      Response.Clear();
      book.Save(Response.OutputStream);
      Response.End();


      // since we normally don't bind on postback, we have to do so explicitly here
      BindCheatSheet(int.Parse(ddlAvailableSheets.SelectedValue));
    }


    //private void DetermineAdDisplay()
    //{
    //  // determine ad display
    //  if (Globals.CSWRSettings.EnableAdvertisements == false)
    //  {
    //    panHeaderAds.Visible = false;
    //  }
    //  else
    //  {
    //    if (SportSetting.Racing.ShowAffiliateAds)
    //    {
    //      LoadAdRotater();
    //    }
    //    else
    //    {
    //      agGoogleAd.Visible = true;
    //    }
    //  }
    //}


    //private void LoadAdRotater()
    //{
    //  System.Random randNum = new System.Random();
    //  int myRandomNumber = randNum.Next(4) + 1;

    //  hlFantasyTrophies.Visible = false;
    //  hlTrophies2Go.Visible = false;
    //  agGoogleAd.Visible = false;

    //  switch (myRandomNumber)
    //  {
    //    case 1:
    //      hlFantasyTrophies.Visible = true;
    //      break;
    //    case 2:
    //      hlTrophies2Go.Visible = true;
    //      break;
    //    case 3:
    //      agGoogleAd.Visible = true;
    //      break;
    //    case 4:
    //      hlUndisputedBelts.Visible = true;
    //      break;
    //    default:
    //      break;
    //  }
    //}


    /// <summary>
    /// This method will inject JQuery into the page which will show an instructional popup, if necessaryz
    /// </summary>
    private void LoadInstructionalPopup()
    {

      string jqueryPopupScript = @"$(document).ready(function () { 
                                    $('div.dragContainer:first').qtip({  
                                    content: {
                                      text: 'Use the ""drag handle"" (shown left) to move drivers around your cheat sheet. Changes will be automatically saved.', 
                                      title: 
                                      {      
                                        text: ""How to Reorder"", 
                                        button: true 
                                      }
                                    },
                                    show: {
                                      ready: true,
                                      event: false
                                    },
                                    position: {
                                      my: 'leftMiddle',
                                      at: 'rightMiddle'
                                    },
                                    hide: false,
                                    style: {
                                      classes: 'ui-tooltip-instructional',
                                      width: 180,
                                      height: 110
                                    }
                                  });
                                });";

      if ((this.User.Identity.IsAuthenticated) && (!Profile.FiguredOutReordering))
      {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "instructionalPopup", jqueryPopupScript, true);
      }
      else if ((!this.User.Identity.IsAuthenticated) && (!SessionHandler.FiguredOutReordering))
      {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "instructionalPopup", jqueryPopupScript, true);
      }

    }


    protected void repDriverSheet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ( (e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem) )
      {
        // First, get a reference to the cheat sheet item being bound
        CheatSheetItem cheatSheetItem = (CheatSheetItem)(e.Item.DataItem);

        // Get a reference to the controls
        RACSheetItemTemplate rsitRACSheetItemTemplate = (RACSheetItemTemplate)e.Item.FindControl("rsitRACSheetItemTemplate");
        HtmlControl liDriverItem = (HtmlControl)e.Item.FindControl("liDriverItem");

        // load the player into the template for display
        rsitRACSheetItemTemplate.CheatSheetPlayerItem = cheatSheetItem;
        rsitRACSheetItemTemplate.BuildControlContent();
        liDriverItem.Attributes.Add("value", cheatSheetItem.PlayerID.ToString());
      }
    }


    /// <summary>
    /// It is important to note that this static webmethod CANNOT access properties normally set by the page class, so we
    /// have to reference the HttpContext
    /// </summary>
    /// <param name="targetPlayerID"></param>
    /// <param name="oldIndex"></param>
    /// <param name="newIndex"></param>
    [System.Web.Services.WebMethod]
    public static void ReorderItems(int cheatSheetID, int oldIndex, int newIndex)
    {
      if(HttpContext.Current.User.Identity.IsAuthenticated)
      {
        // we know the id of the sheet is determined by the dropdownlist
        CheatSheet.ReorderCheatSheetItems(cheatSheetID, oldIndex, newIndex);
        // once the user reorders once, we note that they no longer need to see the instructional message
        ((ProfileCommon)(HttpContext.Current.Profile)).FiguredOutReordering = true;
      }
      else
      {
        // we know the id of the sheet by what is stored in a session variable
        string visitorSheetIDString = HttpContext.Current.Session[SessionHandler.CurrentSportCode + "_" + SessionHandler.GetCurrentVisitorSheetPosition(SessionHandler.CurrentSportCode)].ToString();
        int visitorSheetID = 0;
        int.TryParse(visitorSheetIDString, out visitorSheetID);

        CheatSheet.ReorderCheatSheetItems(visitorSheetID, oldIndex, newIndex);
        // once the visitor reorders once, we note that they no longer need to see the instructional message
        SessionHandler.FiguredOutReordering = true;
      }
    }

  }
}
