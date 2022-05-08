using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;
using BP.CheatSheetWarRoom.UI.UserControls;

namespace BP.CheatSheetWarRoom.UI.Admin.Sheets.Racing
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
        //ddlPositions.DataSource = Position.GetPositions("FOO");
        //ddlPositions.DataBind();
        
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
    }

    private void BindSheet(int supplementalSheetID)
    {
      // query the appropriate supplemental sheet
      SupplementalSheet editSheet = new SupplementalSheet();
      if (supplementalSheetID == 0)
      {
        editSheet = SupplementalSheet.GetSupplementalSheet(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, SupplementalSource.GetSupplementalSource("CSWR").SupplementalSourceID, "RAC", "DR");
      }
      else
      {
        editSheet = SupplementalSheet.GetSupplementalSheet(supplementalSheetID);
      }
      this.CurrentSuppSheet = editSheet;
      hfSupplementalSheetID.Value = editSheet.SupplementalSheetID.ToString();

      // build the link to the 'edit' page
      hlEditSuppSheet.NavigateUrl = hlEditSuppSheet2.NavigateUrl = "EditSupplementalSheet.aspx?ID=" + editSheet.SupplementalSheetID.ToString();
      // load the supplemental source name to the top and botttom
      labSourceTitle.Text = labSourceTitle2.Text = SupplementalSource.GetSupplementalSource(editSheet.SupplementalSourceID).Name;
      // select the appropriate position
      //ddlPositions.SelectedValue = editSheet.PositionCode;

      // bind the players
      List<SupplementalSheetItem> supplementalSheetPlayers = GetSupplementalSheetItems(editSheet.SupplementalSheetID);
      repDriverSheet.DataSource = supplementalSheetPlayers;
      repDriverSheet.DataBind();
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


    protected void repDriverSheet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        // First, get a reference to the cheat sheet item being bound
        SupplementalSheetItem supplementalSheetItem = (SupplementalSheetItem)(e.Item.DataItem);

        // Get a reference to the controls
        RACSheetItemTemplate rsitRACSheetItemTemplate = (RACSheetItemTemplate)e.Item.FindControl("rsitRACSheetItemTemplate");
        HtmlControl liDriverItem = (HtmlControl)e.Item.FindControl("liDriverItem");

        // load the player into the template for display
        rsitRACSheetItemTemplate.SupplementalSheetPlayerItem = supplementalSheetItem;
        rsitRACSheetItemTemplate.BuildControlContent();
        liDriverItem.Attributes.Add("value", supplementalSheetItem.PlayerID.ToString());
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