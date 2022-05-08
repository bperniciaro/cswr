using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class ManageSheets : BasePage
  {

    private int _deleteColumn = 5;

    protected void Page_Init(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        SessionHandler.CurrentSportCode = "RAC";
        scmlnNavigation.CheatSheetID = Profile.Racing.LastRacingCheatSheetID;
      }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
      // if there are no user sheets then we want to hide the navigation
      if (CheatSheet.GetCheatSheetCount(this.User.Identity.Name, SessionHandler.CurrentSportCode) == 0)
      {
        scmlnNavigation.Visible = false;
      }
    }



    public int DeleteButtonColumn { get; set; }


    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        LoadMessages();
      }
      BindCheatSheets();
    }

    private void LoadMessages()
    {
      // instructions
      StringBuilder sbInstructions = new StringBuilder();
      sbInstructions.Append("This page lists all fantasy racing sheets that you have created.  Use this page to edit, open, or delete your sheets as needed.");
      mbInstructions.Message = sbInstructions;
      // no sheets
      StringBuilder sbNoSheets = new StringBuilder();
      sbNoSheets.Append("Please create your first " + SportSeason.GetCurrentSportSeason(SessionHandler.CurrentSportCode).SeasonCode + " <a href='newsheet.aspx'>racing cheat sheet</a>.");
      mbNoSheets.Message = sbNoSheets;
    }

    private void BindCheatSheets()
    {
      List<CheatSheet> userSheets = CheatSheet.GetUserCheatSheets(this.Page.User.Identity.Name, SupportedSport.RAC.ToString());
      gvCheatSheets.DataSource = userSheets;
      gvCheatSheets.DataBind();
      if (userSheets != null)
      {
        if (userSheets.Count > 0)
        {
          // show/hide the appropriate messages
          mbInstructions.Visible = true;
          mbNoSheets.Visible = false;
        }
        else
        {
          // show/hide the appropriate messages
          mbInstructions.Visible = false;
          mbNoSheets.Visible = true;

        }
      }
    }

    protected void gvCheatSheets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      Sport currentSport = Sport.GetSport(SupportedSport.RAC.ToString());
      CheatSheet boundSheet = (CheatSheet)e.Row.DataItem;

      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        HyperLink hlEditSheet = (HyperLink)e.Row.FindControl("hlEditSheet");
        HyperLink hlGoToSheet = (HyperLink)e.Row.FindControl("hlGoToSheet");
        Label labItemCount = (Label)e.Row.FindControl("labItemCount");
        ImageButton deleteButton = (ImageButton)e.Row.Cells[_deleteColumn].Controls[0];

        labItemCount.Text = boundSheet.Items.Count.ToString();
        Position currentPosition = Position.GetPosition(boundSheet.Positions[0].PositionCode);

        // delete button
        deleteButton.ToolTip = "Click to delete this sheet.";
        // edit sheet
        hlEditSheet.NavigateUrl = "~/fantasy-racing/nascar/create/editsheet.aspx?Sheet=" + boundSheet.CheatSheetID.ToString();
        // go to sheet
        hlGoToSheet.NavigateUrl = "~/fantasy-racing/nascar/create/custom-sheet.aspx?Sheet=" + boundSheet.CheatSheetID.ToString();
      }
      else if (e.Row.RowType == DataControlRowType.EmptyDataRow)
      {
        HyperLink hlNoSheets = (HyperLink)e.Row.FindControl("hlNoSheets");
        hlNoSheets.NavigateUrl = (this.Page as BP.CheatSheetWarRoom.UI.BasePage).BaseUrl + "fantasy-racing/nascar/create/newsheet.aspx";
      }
    }



    protected void gvCheatSheets_RowCreated(object sender, GridViewRowEventArgs e)
    {
      //if (e.Row.RowType == DataControlRowType.DataRow)
      //{
      //  ImageButton deleteButton = e.Row.Cells[_deleteColumn].Controls[0] as ImageButton;
      //  deleteButton.OnClientClick = "if (confirm('Are you sure you want to delete this sheet?  You will not be able to recover this sheet once it is deleted.')==false)  return false; ";
      //}
    }

    protected void gvCheatSheets_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      if (gvCheatSheets.DataKeys[e.RowIndex] != null)
      {
        CheatSheet.DeleteCheatSheet(int.Parse(gvCheatSheets.DataKeys[e.RowIndex].Value.ToString()));
      }
      BindCheatSheets();
    }




  }
}