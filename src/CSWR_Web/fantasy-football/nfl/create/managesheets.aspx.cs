using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class ManageSheets : BasePage
  {

    private int DeleteColumnIndex
    {
      get
      {
        if (SportSeason.GetSportStatSeasons(FOO.FOOString).Count == 1)
        {
          return 9;
        }
        else
        {
          return 10;
        }
      }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        SessionHandler.CurrentSportCode = "FOO";
        scmlnNavigation.CheatSheetID = Profile.Football.LastFootballCheatSheetID;
      }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        InitializeUserMessage();
      }
      BindCheatSheets();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
      // if there are no user sheets then we want to hide the navigation
      if (CheatSheet.GetCheatSheetCount(this.User.Identity.Name, FOO.FOOString) == 0)
      {
        scmlnNavigation.Visible = false;
      }
    }



    /// <summary>
    /// Load some stringbuilders which we'll use conditionally
    /// </summary>
    private void InitializeUserMessage()
    {
      // instructions
      //StringBuilder sbInstructions = new StringBuilder();
      //sbInstructions.Append("This page lists all fantasy football sheets that you have created.  Use this page to edit, validate, open, print, or delete your sheets as needed.");
      //mbInstructions.Message = sbInstructions;
      // no sheets
      //StringBuilder sbNoSheets = new StringBuilder();
      //sbNoSheets.Append("Please create your first <a href='newsheet.aspx'>football cheat sheet</a>.");
      //mbNoSheets.Message = sbNoSheets;
    }

    /// <summary>
    /// Bind user sheets then show the appropriate message based on if any sheets are found
    /// </summary>
    private void BindCheatSheets()
    {
      List<CheatSheet> userSheets = CheatSheet.GetUserCheatSheets(this.Page.User.Identity.Name, FOO.FOOString);
      gvCheatSheets.DataSource = userSheets.OrderBy(x => x.SheetName);
      gvCheatSheets.DataBind();
      if (userSheets != null)
      {
        if (userSheets.Count > 0)
        {
          // show/hide the appropriate messages
          panInstructionsMessage.Visible = true;
          panNoSheetsMessage.Visible = false;
        }
        else
        {
          // show/hide the appropriate messages
          panInstructionsMessage.Visible = false;
          panNoSheetsMessage.Visible = true;

        }
      }
    }


    protected void gvCheatSheets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      Sport currentSport = Sport.GetSport(FOO.FOOString);
      string leagueAbbreviation = currentSport.LeagueAbbreviation.ToLower();

      CheatSheet boundSheet = (CheatSheet)e.Row.DataItem;

      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        HyperLink hlEditSheet = (HyperLink)e.Row.FindControl("hlEditSheet");
        HyperLink hlGoToSheet = (HyperLink)e.Row.FindControl("hlGoToSheet");
        HyperLink hlValidateSheet = (HyperLink)e.Row.FindControl("hlValidateSheet");
        Label labPositions = (Label)e.Row.FindControl("labPositions");
        Label labItemCount = (Label)e.Row.FindControl("labItemCount");
        //ImageButton deleteButton = (ImageButton)e.Row.Cells[10].Controls[0];
        Label labScoring = (Label)e.Row.FindControl("labScoring");
        HyperLink hlPrintSheet = (HyperLink)e.Row.FindControl("hlPrintSheet");
        Image imaValidateSheetDisabled = (Image)e.Row.FindControl("imaValidateSheetDisabled");

        labItemCount.Text = boundSheet.Items.Count.ToString();
        //Position currentPosition = Position.GetPosition(boundSheet.Positions[0].PositionCode);

        // delete button
        //deleteButton.ToolTip = "Click to delete this sheet.";
        // validate sheet
        hlValidateSheet.NavigateUrl = "~/fantasy-football/" + leagueAbbreviation + "/create/validatesheet.aspx?SheetID=" + boundSheet.CheatSheetID.ToString();
        // edit sheet
        hlEditSheet.NavigateUrl = "~/fantasy-football/" + leagueAbbreviation + "/create/editsheet.aspx?SheetID=" + boundSheet.CheatSheetID.ToString();
        // go to sheet
        hlGoToSheet.NavigateUrl = "~/fantasy-football/" + leagueAbbreviation + "/create/custom-sheet.aspx?SheetID=" + boundSheet.CheatSheetID.ToString();
        // positions
        if (boundSheet.Positions.Count == 1)
        {
          labPositions.Text = boundSheet.Positions[0].PositionCode;
        }
        else
        {
          int positionCounter = 1;
          foreach (CheatSheetPosition currentPosition in boundSheet.CheatSheetPositions)
          {
            if (positionCounter < (boundSheet.Positions.Count))
            {
              labPositions.Text += currentPosition.PositionCode + ", ";
            }
            else  
            {
              labPositions.Text += currentPosition.PositionCode;
            }
            positionCounter++;
          }
        }

        // scoring system
        if ((bool)boundSheet.MappedProperties[CSProperty.PPRLeague.ToString()])
        {
          labScoring.Text = "PPR";
        }
        else
        {
          labScoring.Text = "Standard";
        }
        // print
        hlPrintSheet.Target = "_blank";
        if (boundSheet.Positions.Count == 1)
        {
          hlPrintSheet.NavigateUrl = "~/fantasy-football/nfl/create/printable/single-position/cheatsheetbyposition.aspx?SheetID=" + boundSheet.CheatSheetID.ToString();
        }
        else
        {
          hlPrintSheet.NavigateUrl = "~/fantasy-football/nfl/create/printable/multiple-positions/cheatsheetwithroster.aspx?SheetID=" + boundSheet.CheatSheetID.ToString();
        }


        // validation is not supported for PPR sheets or if Supplementals are hidden, so only show the relevent control
        if (((bool)boundSheet.MappedProperties[CSProperty.PPRLeague.ToString()]) || 
             (!SportSetting.Football.ShowSupplementalRankings) ||
             (boundSheet.Positions.Count > 1)
           )
        {
          hlValidateSheet.Visible = false;
          imaValidateSheetDisabled.Visible = true;

          // determine which message to display
          if (!SportSetting.Football.ShowSupplementalRankings)
          {
            imaValidateSheetDisabled.ToolTip = "Validation of cheat sheets is not currently available during the season.";
          }
          else if ((bool)boundSheet.MappedProperties[CSProperty.PPRLeague.ToString()])
          {
            imaValidateSheetDisabled.ToolTip = "Validation of PPR sheets is not currently supported.";
          }
          else if (boundSheet.Positions.Count > 1)
          {
            imaValidateSheetDisabled.ToolTip = "Validation of sheets containing multiple positions is not currently supported.";
          }
        }
        else
        {
          hlValidateSheet.Visible = true;
          imaValidateSheetDisabled.Visible = false;
        }

      }
      else if (e.Row.RowType == DataControlRowType.EmptyDataRow)
      {
        HyperLink hlNoSheets = (HyperLink)e.Row.FindControl("hlNoSheets");
        hlNoSheets.NavigateUrl = (this.Page as BP.CheatSheetWarRoom.UI.BasePage).BaseUrl + "fantasy-football/" + leagueAbbreviation + "/create/newsheet.aspx";
      }
    }



    protected void gvCheatSheets_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        //ImageButton deleteButton = e.Row.Cells[this.DeleteColumnIndex].Controls[0] as ImageButton;
        //if (deleteButton != null)
        //{
        //  deleteButton.ToolTip = "Click to permanently delete this cheat sheet.";
        //  deleteButton.OnClientClick = "if (confirm('Are you sure you want to delete this sheet?  You will not be able to recover this sheet once it is deleted.')==false)  return false; ";
        //}
      }
    }

    /// <summary>
    /// Called when a deletion request is made
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvCheatSheets_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      if (gvCheatSheets.DataKeys[e.RowIndex] != null)
      {
        CheatSheet.DeleteCheatSheet(int.Parse(gvCheatSheets.DataKeys[e.RowIndex].Value.ToString()));
      }
      BindCheatSheets();
    }

    /// <summary>
    /// If users can configure 2 or more years of sheets, then show the year that stats are relevant
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvCheatSheets_DataBound(object sender, EventArgs e)
    {
      if (SportSeason.GetSportStatSeasons(FOO.FOOString).Count > 1)
      {
        gvCheatSheets.Columns[1].Visible = true;
      }
      else
      {
        gvCheatSheets.Columns[1].Visible = false;
      }
    }

  }
}