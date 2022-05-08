using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class SuppSheetManager : System.Web.UI.UserControl
  {

    /// <summary>
    /// This is a configurable property that will let us use this user control for multiple sports
    /// </summary>
    private string _sportCode = String.Empty;
    public string SportCode
    {
      get
      {
        if (_sportCode != String.Empty)
        {
          return _sportCode;
        }
        else
        {
          return "FOO";
        }
      }
      set
      {
        _sportCode = value;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
      // on the first page load load the relevant seasons.
      if (!IsPostBack)
      {
        var sportSeasons = SportSeason.GetSportSeasons(this.SportCode);
        sportSeasons.Reverse();
        ddlSportSeason.DataSource = sportSeasons; 
        ddlSportSeason.DataBind();

        ddlSportSeason.SelectedValue = FOO.CurrentSeason;
      }
    }


    /// <summary>
    /// When a row is created, we add an onclick even to the delete button to double-check intent
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSupplementalSheets_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        ImageButton deleteButton = e.Row.Cells[10].Controls[0] as ImageButton;
        if (deleteButton != null)
        {
          deleteButton.OnClientClick = "if (confirm('Are you sure you want to delete this supplemental sheet?')==false)  return false; ";
        }
      }
    }

    /// <summary>
    /// This method is called from multiple places and allows use to put the grid in a normal state and re-bind the data
    /// </summary>
    private void ResetGrid()
    {
      gvSupplementalSheets.SelectedIndex = -1;
      gvSupplementalSheets.DataBind();

      dvSheetDetails.ChangeMode(DetailsViewMode.Insert);
    }

    /// <summary>
    /// If we delete a row, rebind the supplemental sheets and put the details view back in insert mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSupplementalSheets_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
      dvSheetDetails.ChangeMode(DetailsViewMode.Insert);
      ResetGrid();
    }

    /// <summary>
    /// If we select a sheet, put the details view in edit mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSupplementalSheets_SelectedIndexChanged(object sender, EventArgs e)
    {
      dvSheetDetails.ChangeMode(DetailsViewMode.Edit);
    }


    /// <summary>
    /// If we cancel the sheet view we need to deselect the current item in the gridview and rebind it
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dvSheetDetails_ItemCommand(object sender, DetailsViewCommandEventArgs e)
    {
      if (e.CommandName == "Cancel")
      {
        ResetGrid();
      }
    }


    /// <summary>
    /// If we insert a new item into the details view we need to deselect the currrent item in the grid view and rebind it
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dvSheetDetails_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
      ResetGrid();
    }

    /// <summary>
    /// If we update an item in the details view we need to deselect the current item in the grid view and rebind it
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dvSheetDetails_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
    {
      ResetGrid();
    }

    /// <summary>
    /// When the details view is bound, we need to manually load many of the control properties
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dvSheetDetails_DataBound(object sender, EventArgs e)
    {
      //get references to some controls
      Label labDetailsHeader = (Label)dvSheetDetails.FindControl("labDetailsHeader");
      DropDownList ddlPositions = (DropDownList)dvSheetDetails.FindControl("ddlPositionCode");
      DropDownList ddlSeason = (DropDownList)dvSheetDetails.FindControl("ddlSeasonCode");
      DropDownList ddlSource = (DropDownList)dvSheetDetails.FindControl("ddlSourceCode");
      Label labSport = (Label)dvSheetDetails.FindControl("labSport");
      TextBox tbUrl = (TextBox)dvSheetDetails.FindControl("tbUrl");
      Calendar updatedCalendar = (Calendar)dvSheetDetails.FindControl("calLastUpdated");

      List<Position> sportPositions = Position.GetPositions(this.SportCode);
      ddlPositions.DataSource = sportPositions;
      ddlPositions.DataBind();

      switch (dvSheetDetails.CurrentMode)
      {
        case DetailsViewMode.Edit:

          // get a reference to the bound item
          SupplementalSheet boundSupplementalSheet = (SupplementalSheet)dvSheetDetails.DataItem;

          // header
          labDetailsHeader.Text = "Edit Supplemental Sheet";
          // position
          ddlPositions.SelectedValue = boundSupplementalSheet.PositionCode;
          // season
          ddlSeason.SelectedValue = boundSupplementalSheet.SeasonCode;
          // source
          ddlSource.SelectedValue = boundSupplementalSheet.SupplementalSourceID.ToString();
          // sport
          labSport.Text = Sport.GetSport(this.SportCode).SportName;
          // sheet url
          tbUrl.Text = boundSupplementalSheet.Url;
          // updated
          updatedCalendar.SelectedDate = boundSupplementalSheet.LastUpdated;
          break;

        case DetailsViewMode.Insert:

          // header
          labDetailsHeader.Text = "Create Supplemental Sheet";
          // position
          ddlPositions.Items.Insert(0, new ListItem("Select Position", "0"));
          ddlPositions.SelectedValue = "0";
          // calendar
          updatedCalendar.SelectedDate = DateTime.Today;
          // season
          ddlSeason.Items.Insert(0, new ListItem("Select Season", "0"));
          // source
          ddlSource.Items.Insert(0, new ListItem("Select Source", "0"));
          ddlSource.SelectedValue = "0";
          // sport
          labSport.Text = Sport.GetSport(this.SportCode).SportName;
          break;

      }
    }

    /// <summary>
    /// When we're selecting sheets, we need to load the sport based on a page property
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsSupplementalSheets_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
      e.InputParameters["SportCode"] = this.SportCode;
    }

    /// <summary>
    /// When we're selecting sport seasons, we need to load the sportseasons based on a page property
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsSeasons_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
      e.InputParameters["SportCode"] = this.SportCode;
    }

    /// <summary>
    /// When the sport season is changed, we need to reset the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlSportSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
      ResetGrid();
    }


    /// <summary>
    /// When we're inserting a new sheet, we have to tell the object data source the values of the important controls
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsSupplementalSheet_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
      //sport
      e.InputParameters["SportCode"] = this.SportCode;
      //season
      e.InputParameters["SeasonCode"] = ddlSportSeason.SelectedValue;
      //lastupdated
      Calendar updatedCalendar = (Calendar)dvSheetDetails.FindControl("calLastUpdated");
      e.InputParameters["LastUpdated"] = updatedCalendar.SelectedDate;
      //url
      TextBox tbUrl = (TextBox)dvSheetDetails.FindControl("tbUrl");
      e.InputParameters["Url"] = tbUrl.Text;
      //supplementalsourceid
      DropDownList ddlSupplementalSource = (DropDownList)dvSheetDetails.FindControl("ddlSourceCode");
      e.InputParameters["SupplementalSourceID"] = ddlSupplementalSource.SelectedValue;
      // positions
      DropDownList positionList = (DropDownList)dvSheetDetails.FindControl("ddlPositionCode");
      e.InputParameters["PositionCode"] = positionList.SelectedValue;

    }

    /// <summary>
    /// When we're updating an existing sheet, we have to tell the object data source the values of the important controls
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void odsSupplementalSheet_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
      //sport
      e.InputParameters["SportCode"] = this.SportCode;
      //season
      e.InputParameters["SeasonCode"] = ddlSportSeason.SelectedValue;
      //lastupdated
      Calendar updatedCalendar = (Calendar)dvSheetDetails.FindControl("calLastUpdated");
      e.InputParameters["LastUpdated"] = updatedCalendar.SelectedDate;
      //url
      TextBox tbUrl = (TextBox)dvSheetDetails.FindControl("tbUrl");
      e.InputParameters["Url"] = tbUrl.Text;
      //supplementalsourceid
      DropDownList ddlSupplementalSource = (DropDownList)dvSheetDetails.FindControl("ddlSourceCode");
      e.InputParameters["SupplementalSourceID"] = ddlSupplementalSource.SelectedValue;
      // positions
      DropDownList positionList = (DropDownList)dvSheetDetails.FindControl("ddlPositionCode");
      e.InputParameters["PositionCode"] = positionList.SelectedValue;
    }






    protected void gvSupplementalSheets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      // if we're binding to a data row, manipulate the controls within
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        // get references to the appropriate controls
        SupplementalSheet boundSuppSheet = (SupplementalSheet)e.Row.DataItem;

        HyperLink hlRankPage = (HyperLink)e.Row.FindControl("hlRankPage");
        HyperLink hlRankPage2 = (HyperLink)e.Row.FindControl("hlRankPage2");
        HyperLink hlEditSheet = (HyperLink)e.Row.FindControl("hlEditSheet");

        Label labSportName = (Label)e.Row.FindControl("labSportName");
        Label labSuppSourceName = (Label)e.Row.FindControl("labSuppSourceName");
        HyperLink hlValidateSheet = (HyperLink)e.Row.FindControl("hlValidateSheet");

        // if the row which was bound isn't being edited, load it with the relevant data
        if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
        {
          hlRankPage.NavigateUrl = "~/admin/sports/" + Sport.GetSport(this.SportCode).SportName + "/supplementals/ranksupplementalplayers.aspx?ID=" + boundSuppSheet.SupplementalSheetID.ToString();
          labSportName.Text = Sport.GetSport(boundSuppSheet.SportCode).SportName;
          labSuppSourceName.Text = SupplementalSource.GetSupplementalSource(boundSuppSheet.SupplementalSourceID).Name;
          hlEditSheet.NavigateUrl = "~/admin/sports/" + Sport.GetSport(this.SportCode).SportName + "/supplementals/editsupplementalsheet.aspx?ID=" + boundSuppSheet.SupplementalSheetID.ToString();
          if ((this.SportCode == "FOO") && (labSuppSourceName.Text == "Cheat Sheet War Room"))
          {
            hlValidateSheet.NavigateUrl = "~/admin/sports/" + Sport.GetSport(this.SportCode).SportName + "/supplementals/validatesuppsheet.aspx?ID=" + boundSuppSheet.SupplementalSheetID.ToString();
          }
          else
          {
            hlValidateSheet.Visible = false;
          }
        }
      }
    }

    protected void gvSupplementalSheets_DataBound(object sender, EventArgs e)
    {
      int validateSheetIndex = 9;

      if (this.SportCode == "RAC")
      {
        // Hide the Countries column
        gvSupplementalSheets.Columns[validateSheetIndex].Visible = false;
      }
    }
}
}