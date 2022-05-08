using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;
using BP.CheatSheetWarRoom.UI.UserControls;

namespace BP.CheatSheetWarRoom.UI.Admin.Sheets
{
  public partial class RankSupplementalPlayers : BasePage
  {

    protected SupplementalSheet CurrentSuppSheet
    {
      get
      {
        return(SupplementalSheet)ViewState["CurrentSuppSheet"];
      }
      set
      {
        ViewState["CurrentSuppSheet"] = value;
      }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

      // we only want to bind when the page initially loads and when the 'Save Sheet' button is clicked
      if (!this.IsPostBack)
      {
        // Load Positions Dropdown
        ddlPositions.DataSource = Position.GetPositions("FOO");
        ddlPositions.DataBind();

        LoadSheet();
      }
    }

    private void LoadSheet()
    {
      // Determine the sheet to load by the querystring
      if (Request.QueryString["ID"] == null)
      {
        BindSheet(0);
      }
      else
      {
        BindSheet(int.Parse(Request.QueryString["ID"]));
      }
    }

    private void BindSheet(int supplementalSheetID)
    {
      // query the appropriate supplemental sheet
      SupplementalSheet editSheet = new SupplementalSheet();
      if (supplementalSheetID == 0)
      {
        editSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, SupplementalSource.GetSupplementalSource("CSWR").SupplementalSourceID, "FOO", "QB");
      }
      else
      {
        editSheet = SupplementalSheet.GetSupplementalSheet(supplementalSheetID);
      }
      this.CurrentSuppSheet = editSheet;

      // build the link to the 'edit' page
      hlEditSuppSheet.NavigateUrl = hlEditSuppSheet2.NavigateUrl = "editsupplementalsheet.aspx?ID=" + editSheet.SupplementalSheetID.ToString();
      hlValidateSuppSheet.NavigateUrl = hlValidateSuppSheet2.NavigateUrl = "validatesuppsheet.aspx?ID=" + editSheet.SupplementalSheetID.ToString();
      // load the supplemental source name to the top and botttom
      labSourceTitle.Text = labSourceTitle2.Text = SupplementalSource.GetSupplementalSource(editSheet.SupplementalSourceID).Name;
      // select the appropriate position
      ddlPositions.SelectedValue = editSheet.PositionCode;

      // bind the players to the reorderlist
      List<SupplementalSheetItem> supplementalSheetPlayers = GetSupplementalSheetItems(editSheet.SupplementalSheetID);
      repFootballSheet.DataSource = supplementalSheetPlayers;
      repFootballSheet.DataBind();

      // bind the background
      //int totalItems = supplementalSheetPlayers.Count;
      //int[] seqNoArray = new int[totalItems];
      //for (int i = 0; i < totalItems; i++)
      //{
      //  seqNoArray[i] = i + 1;
      //  ddlNewPositions.Items.Add(new ListItem(seqNoArray[i].ToString(), seqNoArray[i].ToString()));
      //}
      //repNumberList.DataSource = seqNoArray;
      //repNumberList.DataBind();


      // bind the players to the mobile controls
      foreach (SupplementalSheetItem currentItem in supplementalSheetPlayers)
      {
        ddlAllSheetPlayers.Items.Add(new ListItem(currentItem.FullNameLastFirst, currentItem.PlayerID.ToString()));
      }
    

      PopulateHiddenFields();
    }


    private void PopulateHiddenFields()
    {
      hfPositionCode.Value = this.CurrentSuppSheet.PositionCode;
      hfSheetID.Value = this.CurrentSuppSheet.SupplementalSheetID.ToString();
      hfStatSeason.Value = this.CurrentSuppSheet.StatsSeasonCode;
    }
   

    /// <summary>
    /// If the session variable is empty, we retrieve the cheat sheet from the database.  Otherwise, we pull the
    /// cheat sheet from the session variable
    /// </summary>
    /// <returns></returns>
    private List<SupplementalSheetItem> GetSupplementalSheetItems(int supplementalSheetID)
    {
      List<SupplementalSheetItem> supplementalSheetItems = SupplementalSheetItem.GetSupplementalSheetItems(supplementalSheetID);
      return supplementalSheetItems;
    }


    protected void repNumberList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ( (e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem) )
      {
        Label seqNoLabel = (Label)e.Item.FindControl("labSeqno");
        seqNoLabel.Text = e.Item.DataItem.ToString();
      }
    }


    [System.Web.Services.WebMethod]
    public static void ChangeTag(string arg, bool state)
    {
      string[] arguments = arg.Split('-');
      int sheetID = int.Parse(arguments[0]);
      int playerID = int.Parse(arguments[1]);
      string tagType = arguments[2];

      SupplementalSheetItem targetSuppSheetItem = SupplementalSheetItem.GetSupplementalSheetItem(sheetID, playerID);

      switch (tagType)
      {
        case "sleeper":
          targetSuppSheetItem.MappedProperties[SSIProperty.Sleeper.ToString()] = state;
          //SupplementalSheetItem.UpdateSupplementalSheetItem(targetSuppSheetItem.SupplementalSheetID, playerID, targetSuppSheetItem.Seqno,
    
          //FOOSupplementalSheetItem.UpdateSleeperStatus(sheetID, playerID, state);
          break;
        case "bust":
          targetSuppSheetItem.MappedProperties[SSIProperty.Bust.ToString()] = state;
          //FOOSupplementalSheetItem.UpdateBustStatus(sheetID, playerID, state);
          break;
      }
      targetSuppSheetItem.Update();
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
      int supplementalSheetID = int.Parse(serviceArguments[0]);
      int playerID = int.Parse(serviceArguments[1]);
      // save the modified item
      SupplementalSheetItem targetSuppSheetItem = SupplementalSheetItem.GetSupplementalSheetItem(supplementalSheetID, playerID);
      targetSuppSheetItem.Note = String.Empty;
      targetSuppSheetItem.Update();
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
      int supplementalSheetID = int.Parse(serviceArguments[0]);
      int playerID = int.Parse(serviceArguments[1]);
      // save the modified item
      SupplementalSheetItem targetSuppSheetItem = SupplementalSheetItem.GetSupplementalSheetItem(supplementalSheetID, playerID);
      targetSuppSheetItem.Note = note;
      targetSuppSheetItem.Update();
    }


    protected void ddlPositions_SelectedIndexChanged(object sender, EventArgs e)
    {
      BindSheet(SupplementalSheet.GetSupplementalSheet(this.CurrentSuppSheet.SeasonCode, this.CurrentSuppSheet.SupplementalSourceID, "FOO", ddlPositions.SelectedValue).SupplementalSheetID);
    }

    protected void butMovePlayer_Click(object sender, EventArgs e)
    {
      List<SupplementalSheetItem> supplementalSheetItems = SupplementalSheetItem.GetSupplementalSheetItems(this.CurrentSuppSheet.SupplementalSheetID);
      SupplementalSheetItem targetItem = supplementalSheetItems.Single(x => x.PlayerID.ToString() == ddlAllSheetPlayers.SelectedValue);

      // reorder based on request
      int oldPosition = targetItem.Seqno - 1;
      int newPosition = 1;
      int.TryParse(ddlNewPositions.SelectedValue, out newPosition);
      newPosition--;
      SupplementalSheet.ReorderSupplementalSheetItems(this.CurrentSuppSheet.SupplementalSheetID, oldPosition, newPosition);

      // reload the sheet
      LoadSheet();
    }



    protected void repFootballSheet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        // First, get a reference to the cheat sheet item being bound
        SupplementalSheetItem suppSheetItem = (SupplementalSheetItem)(e.Item.DataItem);

        // Get a reference to the controls
        FOOSheetItemTemplate fssiTemplate = (FOOSheetItemTemplate)e.Item.FindControl("fssiTemplate");
        HtmlControl liPlayerItem = (HtmlControl)e.Item.FindControl("liPlayerItem");

        // load the player into the template for display
        fssiTemplate.SupplementalSheetItem = suppSheetItem;
        liPlayerItem.Attributes.Add("value", suppSheetItem.PlayerID.ToString());
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
    public static void ReorderItems(int supplementalSheetID, int oldIndex, int newIndex)
    {
      // we know the id of the sheet is determined by the dropdownlist
      SupplementalSheet.ReorderSupplementalSheetItems(supplementalSheetID, oldIndex, newIndex);


    }

  }
}