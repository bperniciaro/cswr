using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class CheatSheetWithRosterMultiple : System.Web.UI.Page
  {

    private string _layout = String.Empty;
    private int _sheetID = 0;
    private int _rankCounter = 0;


    protected void Page_Init(object sender, EventArgs e)
    {
      SessionHandler.CurrentSportCode = "FOO";
      Helpers.AddStyleSheetReferences(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        if (ValidateInput())
        {
          BindColumns();
          // clear any messages
          mbStatus.MessageType = MessageType.NONE;
        }
        else
        {
          // error message
          mbStatus.Message = new StringBuilder("Bad Parameters.  If you believe you reached this message in error, <a href='mailto:admin@cheatsheetwarroom.com'>email the webmaster</a>.");
          panCheatSheetContianer.Visible = false;
        }

      }
    }

    private void BindColumns()
    {
      CheatSheet targetSheet = CheatSheet.GetCheatSheet(_sheetID);
      int totalItems = targetSheet.Items.Count;
      int printedItems = 0;
      int column1MaxItems = 75;
      int column2MaxItems = 45;
      int column3MaxItems = 45;
      int column4MaxItems = 75;

      // Configure Notes
      List<CheatSheetItem> itemsWithNotes = targetSheet.Items.Where(x => x.Note != String.Empty).ToList();
      if(itemsWithNotes.Count > 0)  
      {
        nsNoteSummary.AllPlayerNotes = itemsWithNotes;
      }
      else 
      {
        panSecondPage.Visible = false;
      }

      // Column 1
      int column1Count = (totalItems < column1MaxItems) ? totalItems : column1MaxItems;
      List<CheatSheetItem> column1Items = targetSheet.Items.GetRange(0, column1Count);
      printedItems += column1Count;
      int remainingItems = (totalItems - printedItems);

      // Column 2
      int column2Count = (remainingItems < column2MaxItems) ? remainingItems : column2MaxItems;
      List<CheatSheetItem> column2Items = targetSheet.Items.GetRange(printedItems, column2Count);
      printedItems += column2Count;
      remainingItems = (totalItems - printedItems);

      // Column 3
      int column3Count = (remainingItems < column3MaxItems) ? remainingItems : column3MaxItems;
      List<CheatSheetItem> column3Items = targetSheet.Items.GetRange(printedItems, column3Count);
      printedItems += column3Count;
      remainingItems = (totalItems - printedItems);

      // Column 4
      int column4Count = (remainingItems < column4MaxItems) ? remainingItems : column4MaxItems;
      List<CheatSheetItem> column4Items = targetSheet.Items.GetRange(printedItems, column4Count);
      printedItems += column4Count;
      remainingItems = (totalItems - printedItems);


      // Bind all Columns

      // Column 1
      repColumn1.DataSource = column1Items;
      repColumn1.DataBind();

      // Column 2
      repColumn2.DataSource = column2Items;
      repColumn2.DataBind();

      // Column 3
      repColumn3.DataSource = column3Items;
      repColumn3.DataBind();

      // Column 4
      repColumn4.DataSource = column4Items;
      repColumn4.DataBind();
    }


    private bool ValidateInput()
    {
      bool success = true;

      // Process the SheetID
      if (Request.QueryString["SheetID"] != null)
      {
        int sheetID = 0;
        if (int.TryParse(Request.QueryString["SheetID"], out sheetID))
        {
          _sheetID = sheetID;
        }
        else
        {
          success = false;
        }
      }
      else
      {
        success = false;
      }

      return success;
    }


    protected void repPosition_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        Label rank = (Label)(e.Item.FindControl("labRank"));
        Literal playerName = (Literal)(e.Item.FindControl("litPlayerName"));
        Label labTeamPositionBye = (Label)(e.Item.FindControl("labTeamPositionBye"));
        Label byeWeek = (Label)(e.Item.FindControl("labByeWeek"));
        Image sleeperTag = (Image)(e.Item.FindControl("imaSleeperTag"));
        Image bustTag = (Image)(e.Item.FindControl("imaBustTag"));
        Image injuredTag = (Image)(e.Item.FindControl("imaInjuredTag"));

        CheatSheetItem currentItem = (CheatSheetItem)e.Item.DataItem;

        // Name
        if (currentItem.Player.PositionCode != "DF")
        {
          playerName.Text = currentItem.Player.FullName;
        }
        else
        {
          playerName.Text = currentItem.Player.FirstName;
        }
        // Rank
        _rankCounter++;
        rank.Text = _rankCounter.ToString();

        // Bye Week
        ByeWeek playerBye = ByeWeek.GetByeWeek(SportSeason.GetCurrentSportSeason(SessionHandler.CurrentSportCode).SeasonCode, SessionHandler.CurrentSportCode, currentItem.Player.TeamCode);
        
        // Team Abbreviation
        if (currentItem.Player.PositionCode != "DF")
        {
          if (playerBye == null)
          {
            labTeamPositionBye.Text = "(" + Team.GetTeam(currentItem.Player.TeamCode).Abbreviation + " | " + currentItem.Player.PositionCode + ")";
          }
          else
          {
            labTeamPositionBye.Text = "(" + Team.GetTeam(currentItem.Player.TeamCode).Abbreviation + " | " +
                                            currentItem.Player.PositionCode + " | " + playerBye.Bye.ToString() + ")";
          }
        }
        else
        {
          if(playerBye != null)
          {
            labTeamPositionBye.Text = "(" + playerBye.Bye.ToString() + ")";
          }
        }


        // Tags
        if ((bool)currentItem.MappedProperties[CSIProperty.Sleeper.ToString()] == true)
        {
          sleeperTag.Visible = true;
        }
        if ((bool)currentItem.MappedProperties[CSIProperty.Bust.ToString()] == true)
        {
          bustTag.Visible = true;
        }
        if ((bool)currentItem.MappedProperties[CSIProperty.Injured.ToString()] == true)
        {
          injuredTag.Visible = true;
        }
      }
    }




  }
}