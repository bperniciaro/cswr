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
using System.Web.Security;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class CustomSheet : BasePage
  {

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
        SessionHandler.CurrentSportCode = "FOO";
      }
    }


    /// <summary>
    /// This is the main event that will be called on each page load.  
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

      // we only want to bind the cheat sheet on initial page load
      if (!this.IsPostBack)
      {
        //AddFacebookTrafficPop();
        //LoadSocialTags();

        // if the user is logged in
        if (this.User.Identity.IsAuthenticated)
        {
          //hlRegisterNow.Visible = false;
          // determine what advertisements to display
          //panMemberMessageContainer.Visible = true;
          //panVisitorMessageContainer.Visible = false;
          //var currentUser = Membership.GetUser(Page.User.Identity.Name);
          //if(currentUser.CreationDate >= new DateTime(2017, 8, 2, 0, 0, 0))
          //{
          //  litAdditional.Visible = true;
          //}


          //DetermineAdDisplay();

          InitMessages();

          // only visitors get to see the 'help' buttons
          svnNavigation.Visible = false;
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
        
            mbNoSheets.Visible = false;

            // load the cheat sheet items
            BindCheatSheet(int.Parse(ddlAvailableSheets.SelectedValue));

            ConfirmSheetOwner();
          }
          // if the user has no cheat sheets, show a message
          else
          {
            mbNoSheets.Visible = true;
            //ddlAvailableSheets.Visible = false;
            scmlnNavigation.Visible = false;
            //panSheetHeaderControls.Visible = false;
            //hlPageTitle.Visible = false;  
          }
        }
        // if the user is a visitor
        else
        {
          //hlRegisterNow.Visible = false;
          // determine what advertisements to display

          //hlRegisterNow.Visible = true;
          // if it's a visitor sheet, show the 'help' buttons
          svnNavigation.Visible = true;
          // hide the user 'sheet management' buttons
          scmlnNavigation.Visible = false;
                    // show the 'register now' call to action
                    //hlRegisterNow.Visible = true;

          PopulateVisitorSheetsDropdown();

          // create or load the appropriate cheat sheet
          LoadVisitorCheatSheet();
        }
      }

      // load the instructional popup, if necessary
      LoadInstructionalPopup();
      // declare an event handler to handle export requests
      scplnPageLevelNavigation.ExportEvent += new Globals.ExportEventHandler(CustomSheet_ExportHandler);
      // blog information
      LoadBlogInfo();
    }

    //private void LoadSocialTags()  
    //{
    //  SportMaster myMaster = (SportMaster)this.Page.Master;
    //  myMaster.OpenGraphImage = "https://www.cheatsheetwarroom.com/images/socialsharing/custom-fantasy-football-cheat-sheet.jpg";
    //  myMaster.SchemaOrgImage = "https://www.cheatsheetwarroom.com/images/socialsharing/custom-fantasy-football-cheat-sheet.jpg";
    //  myMaster.TwitterImage = "https://www.cheatsheetwarroom.com/images/socialsharing/custom-fantasy-football-cheat-sheet.jpg";
    //}



    private void PopulateVisitorSheetsDropdown()
    {

      // we load the dropdownlist based on the available positions for the current sport
      List<Position> positions = Position.GetPositions(SessionHandler.CurrentSportCode);
      ddlAvailableSheets.DataSource = positions;
      ddlAvailableSheets.DataTextField = "Name";
      ddlAvailableSheets.DataValueField = "PositionCode";
      ddlAvailableSheets.DataBind();
      // modify the default cheat sheet names
      foreach (ListItem targetItem in ddlAvailableSheets.Items)
      {
        targetItem.Text = "My " + targetItem.Text + "s";
      }
      // if this is the first time in for the visitor, select 'RB' first
      if (SessionHandler.GetCurrentVisitorSheetPosition(SessionHandler.CurrentSportCode) == null)
      {
        ddlAvailableSheets.SelectedValue = FOOPositionsOffense.RB.ToString();
      }
      // if the visitor has already created a sheet, load it from the session variable
      else
      {
        ddlAvailableSheets.SelectedValue = SessionHandler.GetCurrentVisitorSheetPosition(SessionHandler.CurrentSportCode);
      }
    }


    private void ConfirmSheetOwner()
    {
      CheatSheet currentCheatSheet = CheatSheet.GetCheatSheet(this.CurrentCheatSheetID);

      // as an administrator I want to be able to view any sheet for for debugging purposes
      if (!this.User.IsInRole("Administrator"))
      {
        if (!currentCheatSheet.ConfirmOwner(User.Identity.Name))
        {
          mbNoSheets.Message = new StringBuilder("This is not your sheet!");
          mbNoSheets.MessageType = MessageType.ERROR;
          // hide page components
          //panSheetHeaderControls.Visible = false;
          panFootballSheet.Visible = false;
        }
      }
    }

    private void LoadBlogInfo()
    {
      //BlogPost latestFantasyFootballPost = BlogPost.GetLatestBlogPost(new string[] { "Site News", "Fantasy Football" });
      //hlBlogPostTitle.NavigateUrl = Globals.BaseProdUrl + "/blog/post/" + latestFantasyFootballPost.Slug + ".aspx";
      //hlBlogPostTitle.Text = latestFantasyFootballPost.Title;
    }

    private void InitMessages()
    {
      StringBuilder sbNoSheets = new StringBuilder();
      sbNoSheets.Append("You do not have any saved " + SportSeason.GetCurrentSportSeason(SessionHandler.CurrentSportCode).SeasonCode + " cheat sheets.  To get started, please ");
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
      // load the applicable cheat sheet into the page session variable
      CheatSheet currentCheatSheet = CheatSheet.GetCheatSheet(cheatSheetID);
      currentCheatSheet.Validate();
      this.CurrentCheatSheetID = currentCheatSheet.CheatSheetID;
      
      // load the cheat sheet items based on the sheet selected in the dropdown
      List<CheatSheetItem> cheatSheetItems = CheatSheetItem.GetCheatSheetItems(cheatSheetID);
      repFootballSheet.DataSource = cheatSheetItems;
      repFootballSheet.DataBind();

      // store the sheetid
      hfSheetID.Value = currentCheatSheet.CheatSheetID.ToString();
      // store the statseason
      hfStatSeason.Value = currentCheatSheet.StatsSeasonCode;
      // store the position code in a hidden field for JQuery purposes
      hfPositionCode.Value = (currentCheatSheet.Positions.Count == 1) ? currentCheatSheet.Positions[0].PositionCode : String.Empty;
      // determine if this is a PPR league sheet so we can load jquery and the PPR label
      if (currentCheatSheet.MappedProperties.ContainsKey(CSProperty.PPRLeague.ToString()))
      {
        hfPPRLeague.Value = currentCheatSheet.MappedProperties[CSProperty.PPRLeague.ToString()].ToString();
        if ((bool)currentCheatSheet.MappedProperties[CSProperty.PPRLeague.ToString()])
        {
          labPPRLeague.Visible = true;
        }
        else
        {
          labPPRLeague.Visible = false;
        }
      }
      // determine the width of the feature icon popup width
      hfDepthChartPopupWidth.Value = (currentCheatSheet.Positions.Count == 1) ? "250" : "240";
    }


    /// <summary>
    /// In this method we either create the visitor cheat sheet (during their first visit) or load the visitor cheat
    /// sheet from the database if it has already been created.
    /// </summary>
    /// <returns></returns>
    private void LoadVisitorCheatSheet()
    {
      CheatSheet visitorCheatSheet;
      List<Stat> cheatSheetStats;
      int newSheetId;

      var cheatSheetPositions = new List<Position>();

      // for visitor cheat sheets, we will assume the sheet is non-ppr and non-auction
      Dictionary<string, object> visitorCheatSheetMappedProperties = new Dictionary<string,object>();
      visitorCheatSheetMappedProperties[CSProperty.PPRLeague.ToString()] = false;
      visitorCheatSheetMappedProperties[CSProperty.AuctionDraft.ToString()] = false;

      // get a reference to the CSWR Source
      var currentSuppSource = SupplementalSource.GetSupplementalSource("CSWR");
      if (currentSuppSource == null)
      {
        new CSWRWebEvent("CSWR Source Not Found", null, null).Raise();
      }
      
      // ensure the sport code isn't empty
      if (SessionHandler.CurrentSportCode == null)
      {
        new CSWRWebEvent("SportCode Session variable is empty", null, null).Raise();
      }

      // if no visitor cheat sheet has been created, create one based on the RB position
      if (SessionHandler.GetCurrentVisitorSheetPosition(SessionHandler.CurrentSportCode) == null)
      {
        //need to create the RB sheet and load it
        var visitorSheetPosition = Position.GetPosition(FOOPositionsOffense.RB.ToString());
        if (visitorSheetPosition == null)
        {
          new CSWRWebEvent("Visitor Sheet Position is empty", null, null).Raise();
        }
        cheatSheetPositions.Add(visitorSheetPosition);

        cheatSheetStats = Helpers.GetDefaultStatCodes(FOOPositionsOffense.RB.ToString());
        // for a sheet created during the pre-season, use CSWR rankings so that the sheet validates when saved
        if (!Helpers.IsMiddleOfSeason(FOO.FOOString))
        {
          string seasonCode = SportSeason.GetCurrentSportStatSeason(SessionHandler.CurrentSportCode).SeasonCode;
          newSheetId = CheatSheet.CreateCheatSheet(SessionHandler.CurrentSportCode, "My " + visitorSheetPosition.Name + "s", seasonCode,
                                                      cheatSheetPositions, cheatSheetStats, currentSuppSource.SupplementalSourceID, visitorCheatSheetMappedProperties);
        }
        // for a sheet created during the season, use TFP ranking
        else
        {
          newSheetId = CheatSheet.CreateCheatSheet(SessionHandler.CurrentSportCode, "My " + visitorSheetPosition.Name + "s", SportSeason.GetCurrentSportStatSeason(SessionHandler.CurrentSportCode).SeasonCode,
                                                      cheatSheetPositions, cheatSheetStats, "TFP", "DESC", visitorCheatSheetMappedProperties);
        }

        // store the ID of the visitor sheet should we need it later (i.e. the user goes back to this sheet)
        Session[SessionHandler.CurrentSportCode + "_" + FOOPositionsOffense.RB.ToString()] = newSheetId;
        // store the position code of the current visitor sheet being modified
        SessionHandler.SetCurrentVisitorSheetPosition(SessionHandler.CurrentSportCode, FOOPositionsOffense.RB.ToString());
        // load the new visitor cheat sheet
        visitorCheatSheet = CheatSheet.GetCheatSheet(newSheetId);
      }
      // if at least one cheat sheet exists when the page loads, load it
      else
      {

        // get the position code of the most recently edited visitor cheat sheet
        string currentPosition = SessionHandler.GetCurrentVisitorSheetPosition(SessionHandler.CurrentSportCode);


        // if there is no sheet for the currently selected position, create one
        if (Session[SessionHandler.CurrentSportCode + "_" + currentPosition] == null)
        {
          Position visitorSheetPosition = Position.GetPosition(SessionHandler.GetCurrentVisitorSheetPosition(SessionHandler.CurrentSportCode));
          cheatSheetPositions.Add(visitorSheetPosition);
          cheatSheetStats = Helpers.GetDefaultStatCodes(ddlAvailableSheets.SelectedValue);
          // create the visitor cheat sheet
          if (!Helpers.IsMiddleOfSeason(FOO.FOOString))
          {
            newSheetId = CheatSheet.CreateCheatSheet(SessionHandler.CurrentSportCode, "My " + visitorSheetPosition.Name + "s", SportSeason.GetCurrentSportStatSeason(SessionHandler.CurrentSportCode).SeasonCode,
                                                       cheatSheetPositions, cheatSheetStats, currentSuppSource.SupplementalSourceID, visitorCheatSheetMappedProperties);
          }
          else
          {
            newSheetId = CheatSheet.CreateCheatSheet(SessionHandler.CurrentSportCode, "My " + visitorSheetPosition.Name + "s", SportSeason.GetCurrentSportStatSeason(SessionHandler.CurrentSportCode).SeasonCode,
                                                       cheatSheetPositions, cheatSheetStats, "TFP", "DESC", visitorCheatSheetMappedProperties);
          }

          // store the sheet id into a session variable in case we need to load the sheet later
          Session[SessionHandler.CurrentSportCode + "_" + SessionHandler.GetCurrentVisitorSheetPosition(SessionHandler.CurrentSportCode)] = newSheetId;
          // get the sheet
          visitorCheatSheet = CheatSheet.GetCheatSheet(newSheetId);
        }
        // if there is a sheet for the currently selected position, load it
        else
        {
          string existingVisitorSheetPosition = SessionHandler.GetCurrentVisitorSheetPosition(SessionHandler.CurrentSportCode);
          visitorCheatSheet = CheatSheet.GetCheatSheet((int)Session[SessionHandler.CurrentSportCode + "_" + existingVisitorSheetPosition]);
        }
      }

      // bind the visitor sheet to the ReorderList control
      if (visitorCheatSheet == null)
      {
        new CSWRWebEvent("visitorCheatSheet is null", null, null).Raise();
      }
      BindCheatSheet(visitorCheatSheet.CheatSheetID);
    }



    /// <summary>
    /// Each time the user selects a different sheet using the dropdownlist we need to rebind the respective sheet
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlAvailableSheets_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.User.Identity.IsAuthenticated)
      {
        // try to convert the sheet DDL selected value into an integer
        int dropDownCheatSheetID = 0;
        if (!int.TryParse(ddlAvailableSheets.SelectedValue, out dropDownCheatSheetID))
        {
          string debugString = "Trying to parse DDL value: " + ddlAvailableSheets.SelectedValue;
          new CSWRWebEvent(debugString, null, null).Raise();
        }

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
          // if the profile variable (which notes the last sheet edited by the user) has NOT been saved, select the 
          // first item in the dropdownlist and put it in the profile variable
          if (Profile.Football.LastFootballCheatSheetID == 0)
          {
            ddlAvailableSheets.SelectedIndex = 0;
            ddlAvailableSheets.SelectedValue = ddlAvailableSheets.Items[0].Value;
            SaveCurrentSheetIDInProfile(int.Parse(ddlAvailableSheets.Items[0].Value));
          }
          // if the 'most recently edited sheet' profile variable isn't empty, select it in the dropdownlist
          else
          {
            if (CheatSheet.GetCheatSheet(Profile.Football.LastFootballCheatSheetID) != null)
            {
              ddlAvailableSheets.SelectedValue = Profile.Football.LastFootballCheatSheetID.ToString();
            }
          }
        }
        // if the querystring isn't empty, we load that cheat sheet specified in the querystring and store its id in the 'most recent sheet' profile variable
        else
        {
          // here we SHOULD make sure the ID is valid and belongs to the user editing the sheet, but if it isn't it will default to the first
          // item in the dropdown which ensures that users can only see their own sheets
          ddlAvailableSheets.SelectedValue = this.Request.QueryString["SheetID"];
          SaveCurrentSheetIDInProfile(int.Parse(ddlAvailableSheets.SelectedValue));
        }
        // build the 'edit' link to the sheet selected in the dropdownlist
        scmlnNavigation.CheatSheetID = int.Parse(ddlAvailableSheets.SelectedValue);
        scplnPageLevelNavigation.CheatSheetID = int.Parse(ddlAvailableSheets.SelectedValue);
      }
      // if the user isn't authenticated, we load the dropdown based on session variables
      else
      {
      }
    }

    /// <summary>
    /// This method stores a cheat sheet id in a profile variable.  This allows us to note which cheat
    /// cheat was edited last so that we can re-load it the next time the user starts a session
    /// </summary>
    /// <param name="sheetID"></param>
    private void SaveCurrentSheetIDInProfile(int sheetID)
    {
      Profile.Football.LastFootballCheatSheetID = sheetID;
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
    /// This is a method that is called through a callback (AJAX).  It allows us to change the state of the
    /// tags
    /// </summary>
    /// <param name="arg">Holds the sheetID, playerID, and name of the tag for the item being modified</param>
    /// <param name="state">Indicates if the tag is active (true) or not</param>
    [System.Web.Services.WebMethod]
    public static void ChangeTag(string arg, bool state)
    {
      string[] arguments = arg.Split('-');
      int sheetID = int.Parse(arguments[0]);
      int playerID = int.Parse(arguments[1]);
      string tagType = arguments[2];

      CheatSheetItem targetCheatSheetItem = CheatSheetItem.GetCheatSheetItem(sheetID, playerID);
      
      switch (tagType)
      {
        case "sleeper":
          targetCheatSheetItem.MappedProperties[CSIProperty.Sleeper.ToString()] = state;
          break;
        case "bust":
          targetCheatSheetItem.MappedProperties[CSIProperty.Bust.ToString()] = state;
          break;
        case "injured":
          targetCheatSheetItem.MappedProperties[CSIProperty.Injured.ToString()] = state;
          break;
      }

      targetCheatSheetItem.Update();

    }


    private void AddFacebookTrafficPop()
    {

      if (Globals.CSWRSettings.ShowTrafficPop)
      {
        string facebookTrafficPop = "$(document).ready(function() { \n" +
        "$().facebookTrafficPop({ \n" +
          "timeout: 20, \n" +
          "delay: 60, \n" +
          "title: \"Support Cheat Sheet War Room\", \n " +
          "message: 'Like this page or share it on Facebook and enter our competition to win free <a rel=\"nofollow\" href=\"https://www.fantasyjocks.com?rfsn=105771.96046&subid=traffic-pop\"></a> Fantasy Jocks merchandise!<center><img src=\"https://www.cheatsheetwarroom.com/images/layout/ads/trafficpop/fantasyjocksring.jpg\" border=\"0\" style=\"margin:10px 0px;\" /></center>', \n" +
          "url: \"https://www.cheatsheetwarroom.com/fantasy-football/nfl/create/custom-sheet.aspx\", \n" +
          "share_url: 'https://www.cheatsheetwarroom.com/fantasy-football/nfl/create/custom-sheet.aspx', \n" +
          "closeable: true \n" +
          "} \n" +
        " ); \n" +
        "});";

        ScriptManager.RegisterStartupScript(this, Page.GetType(), "traffiPop", facebookTrafficPop, true);
      }

    }

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

      // remove special characters
      targetItem.Note = HttpUtility.HtmlEncode(note);


      targetItem.Update();
    }


    public void CustomSheet_ExportHandler(object sender, Globals.ExportEventArgs e)
    {
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
      titleCell.StyleID = "TitleStyle";

      CheatSheet currentSheet = CheatSheet.GetCheatSheet(this.CurrentCheatSheetID);

      if (SportSetting.Football.ShowSupplementalRankings && !(bool)currentSheet.MappedProperties[CSProperty.PPRLeague.ToString()])
      {
        switch (currentSheet.Positions[0].PositionCode)
        {
          case "QB":
            titleCell.MergeAcross = 20;
            break;
          case "RB":
            titleCell.MergeAcross = 21;
            break;
          case "WR":
            titleCell.MergeAcross = 20;
            break;
          case "TE":
            titleCell.MergeAcross = 20;
            break;
          case "K":
            titleCell.MergeAcross = 17;
            break;
          case "DF":
            titleCell.MergeAcross = 17;
            break;
        }
      }
      else
      {
        switch (currentSheet.Positions[0].PositionCode)
        {
          case "QB":
            titleCell.MergeAcross = 18;
            break;
          case "RB":
            titleCell.MergeAcross = 19;
            break;
          case "WR":
            titleCell.MergeAcross = 18;
            break;
          case "TE":
            titleCell.MergeAcross = 18;
            break;
          case "K":
            titleCell.MergeAcross = 15;
            break;
          case "DF":
            titleCell.MergeAcross = 15;
            break;
        }
      }
        

      SupplementalSheet supplementalSheet1 = new SupplementalSheet();
      SupplementalSheet supplementalSheet2 = new SupplementalSheet();
      SupplementalSheet supplementalSheet3 = new SupplementalSheet();

      SupplementalSource supplementalSource1 = SupplementalSource.GetSupplementalSource(int.Parse(Setting.GetSetting("DEFSS1").SettingValue));
      SupplementalSource supplementalSource2 = SupplementalSource.GetSupplementalSource(int.Parse(Setting.GetSetting("DEFSS2").SettingValue));
      SupplementalSource supplementalSource3 = SupplementalSource.GetSupplementalSource(int.Parse(Setting.GetSetting("DEFSS3").SettingValue));

      if (SportSetting.Football.ShowSupplementalRankings && !(bool)currentSheet.MappedProperties[CSProperty.PPRLeague.ToString()])
      {
        // Supplemental Source 1 
        if (supplementalSource1 != null)
        {
          supplementalSheet1 = SupplementalSheet.GetSupplementalSheet(currentSheet.SeasonCode, supplementalSource1.SupplementalSourceID, SessionHandler.CurrentSportCode, currentSheet.Positions[0].PositionCode);
        }
        // Supplemental Source 2 
        if (supplementalSource2 != null)
        {
          supplementalSheet2 = SupplementalSheet.GetSupplementalSheet(currentSheet.SeasonCode, supplementalSource2.SupplementalSourceID, SessionHandler.CurrentSportCode, currentSheet.Positions[0].PositionCode);
        }
        // Supplemental Source 3 
        if (supplementalSource3 != null)
        {
          supplementalSheet3 = SupplementalSheet.GetSupplementalSheet(currentSheet.SeasonCode, supplementalSource3.SupplementalSourceID, SessionHandler.CurrentSportCode, currentSheet.Positions[0].PositionCode);
        }
      }

      WorksheetRow row = sheet.Table.Rows.Add();
      // Rank
      sheet.Table.Columns.Add(new WorksheetColumn(30));
      row.Cells.Add(new WorksheetCell("Rank", "HeaderStyle"));
      if (SportSetting.Football.ShowSupplementalRankings && !(bool)currentSheet.MappedProperties[CSProperty.PPRLeague.ToString()])
      {
        // Supp Rank 1
        if (supplementalSheet1 != null)
        {
          sheet.Table.Columns.Add(new WorksheetColumn(35));
          row.Cells.Add(new WorksheetCell(supplementalSource1.Abbreviation, "HeaderStyle"));
        }
        // Supp Rank 2
        if (supplementalSheet2 != null)
        {
          sheet.Table.Columns.Add(new WorksheetColumn(35));
          row.Cells.Add(new WorksheetCell(supplementalSource2.Abbreviation, "HeaderStyle"));
        }
        // Supp Rank 3
        if (supplementalSheet3 != null)
        {
          sheet.Table.Columns.Add(new WorksheetColumn(35));
          row.Cells.Add(new WorksheetCell(supplementalSource3.Abbreviation, "HeaderStyle"));
        }
      }
      // Sleeper
      sheet.Table.Columns.Add(new WorksheetColumn(15));
      row.Cells.Add(new WorksheetCell("SL", "HeaderStyle"));
      // Bust
      sheet.Table.Columns.Add(new WorksheetColumn(15));
      row.Cells.Add(new WorksheetCell("BU", "HeaderStyle"));
      // Injured
      sheet.Table.Columns.Add(new WorksheetColumn(15));
      row.Cells.Add(new WorksheetCell("IN", "HeaderStyle"));

      if (currentSheet.Positions[0].PositionCode != "DF")
      {
        // Player
        sheet.Table.Columns.Add(new WorksheetColumn(115));
        row.Cells.Add(new WorksheetCell("Player", "HeaderStyle"));
      }
      // Team
      sheet.Table.Columns.Add(new WorksheetColumn(45));
      row.Cells.Add(new WorksheetCell("Team", "HeaderStyle"));
      // Experience
      sheet.Table.Columns.Add(new WorksheetColumn(25));
      row.Cells.Add(new WorksheetCell("Exp", "HeaderStyle"));
      // Bye
      sheet.Table.Columns.Add(new WorksheetColumn(25));
      row.Cells.Add(new WorksheetCell("Bye", "HeaderStyle"));
      // Stats
      List<Stat> sheetStats = Stat.GetStats(SessionHandler.CurrentSportCode, currentSheet.Positions[0].PositionCode);
      foreach (Stat currentStat in sheetStats)
      {
        sheet.Table.Columns.Add(new WorksheetColumn(40));
        row.Cells.Add(new WorksheetCell(currentStat.Abbreviation, "HeaderStyle"));
      }
      // Note
      sheet.Table.Columns.Add(new WorksheetColumn(250));
      row.Cells.Add(new WorksheetCell("Note", "HeaderStyle"));

      // generate the exportable spreadsheet...
      int i = 1;

      List<CheatSheetItem> cheatSheetItems = CheatSheetItem.GetCheatSheetItems(currentSheet.CheatSheetID);

      foreach (CheatSheetItem currentItem in cheatSheetItems)
        {
        row = sheet.Table.Rows.Add();
        // Rank
        row.Cells.Add(new WorksheetCell(i.ToString(), "DefaultStyle"));
        if (SportSetting.Football.ShowSupplementalRankings && !(bool)currentSheet.MappedProperties[CSProperty.PPRLeague.ToString()])
        {
          // Supp Rank 1
          if (supplementalSheet1 != null)
          {
            SupplementalSheetItem targetItem = SupplementalSheetItem.GetSupplementalSheetItem(supplementalSheet1.SupplementalSheetID, currentItem.PlayerID);
            if (targetItem != null)
            {
              int supplementalRank1 = targetItem.Seqno;
              row.Cells.Add(new WorksheetCell(supplementalRank1.ToString(), "DefaultStyle"));
            }
            else
            {
              row.Cells.Add(new WorksheetCell("n/r", "DefaultStyle"));
            }
          }
          // Supp Rank 2
          if (supplementalSheet2 != null)
          {
            SupplementalSheetItem targetItem = SupplementalSheetItem.GetSupplementalSheetItem(supplementalSheet2.SupplementalSheetID, currentItem.PlayerID);
            if (targetItem != null)
            {
              int supplementalRank2 = targetItem.Seqno;
              row.Cells.Add(new WorksheetCell(supplementalRank2.ToString(), "DefaultStyle"));
            }
            else
            {
              row.Cells.Add(new WorksheetCell("n/r", "DefaultStyle"));
            }
          }
          // Supp Rank 3
          if (supplementalSheet3 != null)
          {
            SupplementalSheetItem targetItem = SupplementalSheetItem.GetSupplementalSheetItem(supplementalSheet3.SupplementalSheetID, currentItem.PlayerID);
            if (targetItem != null)
            {
              int supplementalRank3 = targetItem.Seqno;
              row.Cells.Add(new WorksheetCell(supplementalRank3.ToString(), "DefaultStyle"));
            }
            else
            {
              row.Cells.Add(new WorksheetCell("n/r", "DefaultStyle"));
            }
          }
        }
        // Sleeper
        if ((bool)currentItem.MappedProperties[CSIProperty.Sleeper.ToString()])
        {
          row.Cells.Add(new WorksheetCell("SL", "SleeperStyle"));
        }
        else
        {
          row.Cells.Add(new WorksheetCell(String.Empty, "DefaultStyle"));
        }
        // Bust
        if ((bool)currentItem.MappedProperties[CSIProperty.Bust.ToString()])
        {
          row.Cells.Add(new WorksheetCell("BU", "BustStyle"));
        }
        else
        {
          row.Cells.Add(new WorksheetCell(String.Empty, "DefaultStyle"));
        }
        // Injured
        if ((bool)currentItem.MappedProperties[CSIProperty.Injured.ToString()])
        {
          row.Cells.Add(new WorksheetCell("IN", "InjuredStyle"));
        }
        else
        {
          row.Cells.Add(new WorksheetCell(String.Empty, "DefaultStyle"));
        }
        // Full Name
        if (currentSheet.Positions[0].PositionCode != "DF")
        {
          row.Cells.Add(new WorksheetCell(currentItem.Player.FullName, "DefaultStyle"));
        }
        // Abbreviation
        row.Cells.Add(new WorksheetCell(currentItem.Player.Team.Abbreviation, "DefaultStyle"));
        // Experience
        row.Cells.Add(new WorksheetCell(currentItem.Player.YearsExperience.ToString(), "DefaultStyle"));
        // Bye Week
        ByeWeek targetByeWeek = new ByeWeek();
        targetByeWeek = ByeWeek.GetByeWeek(currentSheet.SeasonCode, SessionHandler.CurrentSportCode, currentItem.Player.Team.TeamCode);
        if (targetByeWeek != null)
        {
          row.Cells.Add(new WorksheetCell(targetByeWeek.Bye.ToString(), "DefaultStyle"));
        }
        else
        {
          row.Cells.Add(new WorksheetCell(String.Empty, "DefaultStyle"));
        }
        i++;
        // Stats
        List<SportSeasonPlayerSeasonStat> playerStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO",
              currentSheet.StatsSeasonCode, currentItem.PlayerID, (bool)currentSheet.MappedProperties[CSProperty.PPRLeague.ToString()]);

        if (playerStats.Count > 0)
        {
          foreach (SportSeasonPlayerSeasonStat currentStat in playerStats)
          {
            row.Cells.Add(new WorksheetCell(currentStat.StatValue.ToString(), "DefaultStyle"));
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
        string decodedNote = HttpUtility.HtmlDecode(currentItem.Note);
        row.Cells.Add(new WorksheetCell(decodedNote, "DefaultStyle"));
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
    //  if (Globals.CSWRSettings.EnableAdvertisements == false)
    //  {
    //    panHeaderAds.Visible = false;
    //  }
    //  else
    //  {
    //    panHeaderAds.Visible = true;
    //    // determine ad display
    //    if (Globals.CSWRSettings.EnableAdvertisements == false)
    //    {
    //      panHeaderAds.Visible = false;
    //    }
    //    else
    //    {
    //      if (SportSetting.Football.ShowAffiliateAds)
    //      {
    //        LoadAdRotater();
    //      }
    //      else
    //      {
    //        agGoogleAd.Visible = true;
    //      }
    //    }
    //  }
    //}



    //private void LoadAdRotater()
    //{
    //  System.Random randNum = new System.Random();
    //  int myRandomNumber = randNum.Next(3) + 1;

    //  hlFantasyTrophies.Visible = false;
    //  agGoogleAd.Visible = false;

    //  switch (myRandomNumber)
    //  {
    //    case 1:
    //      hlFantasyTrophies.Visible = true;
    //      break;
    //    case 2:
    //      agGoogleAd.Visible = true;
    //      break;
    //    case 3:
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
                                    $('.dragContainer div:first').qtip({  
                                    content: {
                                      text: 'Use the ""drag handle"" (shown left) to move players around your cheat sheet.  Changes will be automatically saved.', 
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

    protected void repFootballSheet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        // First, get a reference to the cheat sheet item being bound
        CheatSheetItem cheatSheetItem = (CheatSheetItem)(e.Item.DataItem);

        // Get a reference to the controls
        FOOSheetItemTemplate fssiTemplate = (FOOSheetItemTemplate)e.Item.FindControl("fssiTemplate");
        HtmlControl liPlayerItem = (HtmlControl)e.Item.FindControl("liPlayerItem");

        // load the player into the template for display
        fssiTemplate.CheatSheetItem = cheatSheetItem;
        //fssiTemplate.BuildControlContent();
        liPlayerItem.Attributes.Add("value", cheatSheetItem.PlayerID.ToString());
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
    public static void ReorderItems(string cheatSheetID, int oldIndex, int newIndex)
    {
      if (HttpContext.Current.User.Identity.IsAuthenticated)
      {
        int sheetID = int.Parse(cheatSheetID);

        // we know the id of the sheet is determined by the dropdownlist
        CheatSheet.ReorderCheatSheetItems(sheetID, oldIndex, newIndex);
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
