using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class RegistrationConfirmation : BasePage
  {

    public int ConvertedFootballSheetCount
    {
      get
      {
        return (ViewState["ConvertedFootballSheetCount"] == null) ? 0 : (int)ViewState["ConvertedFootballSheetCount"];
      }
      set
      {
        ViewState["ConvertedFootballSheetCount"] = value;
      }
    }

    public int ConvertedRacingSheetCount
    {
      get
      {
        return (ViewState["ConvertedRacingSheetCount"] == null) ? 0 : (int)ViewState["ConvertedRacingSheetCount"];
      }
      set
      {
        ViewState["ConvertedRacingSheetCount"] = value;
      }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.User.Identity.IsAuthenticated)
      {
        if (Roles.GetRolesForUser(this.User.Identity.Name).Length == 0)
        {
          Roles.AddUserToRole(this.User.Identity.Name, "Member");
        }
        LoadRegistrationInfo();
        if(Profile.GetProfile(this.User.Identity.Name).EmailNotifications)
        {
          panEmailWhiteListInstructions.Visible = true;
        }
      }
      else
      {
        // show error message
      }
    }

    private void LoadRegistrationInfo()
    {
      // if the user figured out reordering as a visitor, there is no need to make them watch the instructional 'video' again.
      if (SessionHandler.FiguredOutReordering)
      {
        Profile.FiguredOutReordering = true;
      }

      // create a variable for storing converted football sheets
      List<CheatSheet> fooVisitorSheets = new List<CheatSheet>();
      // create a variable for storing converted racing sheets
      List<CheatSheet> racVisitorSheets = new List<CheatSheet>();
      int visitorSheetID = 0;
      int totalSheetCount = 0;

      /* When visitor cheat sheets are created, the username associated with those cheat sheets is an empty string.  In order
         associate those visitor cheets with a newly-created user, we need to determine the ids of the visitor cheat sheets
         and update them with the new username chosen by the new user.  The ids of the visitor cheat sheets are saved in session 
         variables keyed off of the position codes of the respective visitor sheets.  So, we need to spin through those visitor
         sheets belonging to this user and update them with his username. */

      /********************/
      /*  Convert Football Sheets
      /********************/
      List<Position> possiblePositions = Position.GetPositions("FOO");
      for (int i = 0; i < possiblePositions.Count; i++)
      {
        // if a visitor sheet was created for this particular position
        if (Session["FOO" + "_" + possiblePositions[i].PositionCode] != null)
        {
          // get a reference to the visitor sheet (visitor sheets are prefixed with the sport code)
          visitorSheetID = (int)Session["FOO" + "_" + possiblePositions[i].PositionCode];
          CheatSheet visitorSheet = CheatSheet.GetCheatSheet(visitorSheetID);
          // add the new username to the sheet, then update it
          visitorSheet.Username = this.User.Identity.Name; //tbUserName.Text;
          visitorSheet.Update();
          // add this sheet to a local collection of sheets which we'll bind to a repeater upon registration to
          // inform the user of the new name
          fooVisitorSheets.Add(visitorSheet);
          totalSheetCount++;
          this.ConvertedFootballSheetCount++;
          // clear out the session variable
          Session["FOO" + "_" + possiblePositions[i].PositionCode] = null;
        }
      }
      // clear our the 'current football sheet position' variable for this visitor
      SessionHandler.SetCurrentVisitorSheetPosition("FOO", null);

      /********************/
      /*  Convert Racing Sheets
      /********************/
      if (Session["RAC_DR"] != null)
      {
        // get a reference to the visitor sheet 
        visitorSheetID = (int)Session["RAC_DR"];
        CheatSheet visitorSheet = CheatSheet.GetCheatSheet(visitorSheetID);
        // add the new username to the sheet, then update it
        visitorSheet.Username = this.User.Identity.Name;  //tbUserName.Text;
        visitorSheet.Update();
        // add this sheet to a local collection of sheets which we'll bind to a repeater upon registration
        racVisitorSheets.Add(visitorSheet);
        totalSheetCount++;
        this.ConvertedRacingSheetCount++;
        // clear out the session variable
        Session["RAC_DR"] = null;
      }
      SessionHandler.SetCurrentVisitorSheetPosition("RAC", null);


      /* If the new user had unsaved sheets, build a message to tell the user that they are saved, then display all saved sheets */
      if (totalSheetCount > 0)
      {
        panUnsavedSheetsExist.Visible = true;
        if (totalSheetCount == 1)
        {
          litWelcomeMessage.Text = "Your account has been created and your sheet has been saved.  Simply click on the link below to return to your sheet.";
        }
        else
        {
          litWelcomeMessage.Text = "Your account has been created and your sheets have been saved.  Simply click on any of the links below to continue ranking.";
        }

        // bind the converted football sheets to their repeater
        if (fooVisitorSheets.Count > 0)
        {
          repSavedFootballSheets.DataSource = fooVisitorSheets;
          repSavedFootballSheets.DataBind();
          panConvertedFOOSheets.Visible = true;
        }
        // bind the converted racing sheets to their repeater
        if (racVisitorSheets.Count > 0)
        {
          repSavedRacingSheets.DataSource = racVisitorSheets;
          repSavedRacingSheets.DataBind();
          panConvertedRACSheets.Visible = true;
        }
      }
      else
      {
        panNoUnsavedSheetsExist.Visible = true;
      }
    }


    protected void repSavedFootballSheets_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        HyperLink hlSavedSheet = (HyperLink)e.Item.FindControl("hlSavedSheet");
        CheatSheet currentCheatSheet = (CheatSheet)e.Item.DataItem;

        hlSavedSheet.Text = currentCheatSheet.SheetName;
        hlSavedSheet.NavigateUrl = "~/fantasy-football/nfl/create/custom-sheet.aspx?Sheet=" + currentCheatSheet.CheatSheetID.ToString();
      }
    }

    protected void repSavedRacingSheets_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        HyperLink hlSavedSheet = (HyperLink)e.Item.FindControl("hlSavedSheet");
        CheatSheet currentCheatSheet = (CheatSheet)e.Item.DataItem;

        hlSavedSheet.Text = currentCheatSheet.SheetName;
        hlSavedSheet.NavigateUrl = "~/fantasy-racing/nascar/create/custom-sheet.aspx?Sheet=" + currentCheatSheet.CheatSheetID.ToString();
      }
    }


  }
}