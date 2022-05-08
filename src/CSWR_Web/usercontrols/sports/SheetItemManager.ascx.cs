using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class SheetItemManager : System.Web.UI.UserControl
  {

    
    /// <summary>
    /// This collection holds all of the players on the current sheet
    /// </summary>
    private List<Player> SheetPlayers { get; set; }

    /// <summary>
    /// This variable holds the current cheat sheet being acted upon
    /// </summary>
    private CheatSheet CurrentCheatSheet
    {
      get
      {
        return (ViewState["CurrentCheatSheet"] == null) ? null : (CheatSheet)ViewState["CurrentCheatSheet"];
      }
      set
      {
        ViewState["CurrentCheatSheet"] = value;
      }
    }

    /// <summary>
    /// This variable holds the current supplemental sheet being acted upon
    /// </summary>
    private SupplementalSheet CurrentSuppSheet
    {
      get
      {
        return (ViewState["CurrentSuppSheet"] == null) ? null : (SupplementalSheet)ViewState["CurrentSuppSheet"];
      }
      set
      {
        ViewState["CurrentSuppSheet"] = value;
      }
    }

    /// <summary>
    /// This variable holds the value indicating what type of sheet we're acting upon
    /// </summary>
    public SheetType SheetType 
    {
      get
      {
        return (ViewState["SheetType"] == null) ? SheetType.CheatSheet : (SheetType)ViewState["SheetType"];
      }
      set
      {
        ViewState["SheetType"] = value;
      }
    }


    /// <summary>
    /// This variable holds the ID of either the cheat sheet or the supplemental sheet
    /// </summary>
    public int SheetID 
    {
      get
      {
        return (ViewState["SheetID"] == null) ? 0 : (int)ViewState["SheetID"];
      }
      set
      {
        ViewState["SheetID"] = value;
      }
    }


    /// <summary>
    /// This variable holds the sport current being targeted
    /// </summary>
    public string SportCode 
    {
      get
      {
        return (ViewState["SportCode"] == null) ? String.Empty : ViewState["SportCode"].ToString();
      }
      set
      {
        ViewState["SportCode"] = value;
      }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        // load a local variable with either the cheat sheet or supplemental sheet object 
        if (GetCurrentSheet())
        {
          InitializeControls();

          switch (this.SheetType)
          {
            case SheetType.CheatSheet:
              // load labels based on sport being processed
              switch (this.SportCode)
              {
                case "FOO":
                  litSheetTitle.Text = "My Sheet Players";
                  litPlayerPoolTitle.Text = "Available Players";
                  break;
                case "RAC":
                  litSheetTitle.Text = "My Sheet Drivers";
                  litPlayerPoolTitle.Text = "Available Drivers";
                  break;
              }
              // load the cheatsheet items
              LoadCheatSheetItems();
              // load the available cheat sheet players
              LoadCheatSheetAvailablePlayers();
              // configure the player limits
              List<CheatSheetPosition> cheatSheetPositions = CheatSheetPosition.GetCheatSheetPositions(this.SheetID);
              break;
            case SheetType.SuppSheet:
              // load labels based on sport being processed
              switch (this.SportCode)
              {
                case "FOO":
                  litSheetTitle.Text = "Supp Sheet Players";
                  litPlayerPoolTitle.Text = "Available Player";
                  break;
                case "RAC":
                  litSheetTitle.Text = "Supp Sheet Drivers";
                  litPlayerPoolTitle.Text = "Available Drivers";
                  break;
              }
              // load the supplemental sheet
              LoadSuppSheetItems();
              // load the available supplemental sheet players
              LoadSuppSheetAvailablePlayers();
              break;
          }
        }
      }
    }


    public void InitializeControls()
    {
      string sortStatCode = String.Empty;
      MessageBox mbMessageBox = (MessageBox)upSheetPlayers.FindControl("mbStatus");

      string statSeason = String.Empty;
      switch (this.SheetType)
      {
        case SheetType.CheatSheet:
          statSeason = this.CurrentCheatSheet.StatsSeasonCode;

          switch (this.SportCode)
          {
            case FOO.FOOString:
              sortStatCode = ((bool)this.CurrentCheatSheet.MappedProperties[CSProperty.PPRLeague.ToString()] == true) ? "TFPP" : "TFP";
              break;
            case "RAC":
              sortStatCode = "RANK";
              break;
          }

          break;
        case SheetType.SuppSheet:
          statSeason = this.CurrentSuppSheet.StatsSeasonCode;
          break;
      }

      mbMessageBox.MessageType = MessageType.INSTRUCTIONS;

      switch (this.SportCode)
      {
        case "FOO":
          ddlSortPoolPlayers.Items.Add(new ListItem("Sort by " + statSeason + " Total Fantasy Points", sortStatCode + "-DESC"));
          labItemType.Text = "Players";
          labItemType2.Text = "Players";
          mbMessageBox.Message = new StringBuilder("To add/remove players to your sheet, use the arrows below.  Players added to your sheet from the available " +
              "players pool will be added to the end of your sheet.");

          break;
        case "RAC":
          ddlSortPoolPlayers.Items.Add(new ListItem("Sort by " + statSeason + " Points", "PNTS-" + "ASC"));
          labItemType.Text = "Drivers";
          labItemType2.Text = "Drivers";
          mbMessageBox.Message = new StringBuilder("To add/remove drivers to your sheet, use the arrows below.  Drivers added to your sheet from the available " +
              "drivers pool will be added to the end of your sheet.");
          break;
      }
    }


    /// <summary>
    /// This method loads the current sheet, whether a cheat sheet or supplemental sheet
    /// </summary>
    private bool GetCurrentSheet()
    {
      bool result = false;

      switch (this.SheetType)
      {
        case SheetType.CheatSheet:
          this.CurrentCheatSheet = CheatSheet.GetCheatSheet(this.SheetID);
          if (this.CurrentCheatSheet != null)
          {
            result = true;
          }
          break;
        case SheetType.SuppSheet:
          this.CurrentSuppSheet = SupplementalSheet.GetSupplementalSheet(this.SheetID);
          if (this.CurrentSuppSheet != null)
          {
            result = true;
          }
          break;
      }
      return result;
    }




    private void LoadCheatSheetItems()
    {
      List<CheatSheetItem> cheatSheetItems = new List<CheatSheetItem>();
      if (ddlSortSheetPlayers.SelectedValue == "Name")
      {
        cheatSheetItems = CheatSheetItem.GetCheatSheetItems(this.SheetID).OrderBy(x => x.FullNameLastFirst).ToList();
      }
      else
      {
        cheatSheetItems = CheatSheetItem.GetCheatSheetItems(this.SheetID);
      }

      labTotalSheetPlayers.Text = cheatSheetItems.Count.ToString();
      lbSheetPlayers.DataSource = cheatSheetItems;
      lbSheetPlayers.DataValueField = "PlayerID";

      if (this.CurrentCheatSheet.Positions.Count > 1)
      {
        lbSheetPlayers.DataTextField = "FullNameAndPosition";
      }
      else
      {
        lbSheetPlayers.DataTextField = "FullNameLastFirst";
      }

      lbSheetPlayers.DataBind();
    }




    private void LoadSuppSheetItems()
    {

      List<SupplementalSheetItem> suppSheetItems;


      if (ddlSortSheetPlayers.SelectedValue == "Name")
      {
        suppSheetItems = SupplementalSheetItem.GetSupplementalSheetItems(this.SheetID).OrderBy(x => x.FullNameLastFirst).ToList();
        lbSheetPlayers.DataSource = suppSheetItems;
      }
      else
      {
        suppSheetItems = SupplementalSheetItem.GetSupplementalSheetItems(this.SheetID);
        lbSheetPlayers.DataSource = suppSheetItems;
      }
      labTotalSheetPlayers.Text = suppSheetItems.Count.ToString();


      lbSheetPlayers.DataTextField = "FullNameLastFirst";
      lbSheetPlayers.DataValueField = "PlayerID";
      lbSheetPlayers.DataBind();
    }


    private void LoadCheatSheetAvailablePlayers()
    {
      List<Player> availablePlayers = CheatSheet.GetCheatSheetAvailablePlayers(this.SheetID, ddlSortPoolPlayers.SelectedValue.Split('-')[0], 
        ddlSortPoolPlayers.SelectedValue.Split('-')[1]);
      lbPlayerPool.DataSource = availablePlayers;
      // build the listbox
      lbPlayerPool.DataValueField = "PlayerID";

      if (this.CurrentCheatSheet.Positions.Count > 1)
      {
        lbPlayerPool.DataTextField = "FullNameAndPosition";
      }
      else
      {
        lbPlayerPool.DataTextField = "FullNameLastFirst";
      }

      lbPlayerPool.DataBind();
      labTotalAvailablePlayers.Text = availablePlayers.Count.ToString();
    }




    private void LoadSuppSheetAvailablePlayers()
    {
      List<Player> availablePlayers = SupplementalSheet.GetSupplementalSheetAvailablePlayers(this.SheetID, ddlSortPoolPlayers.SelectedValue.Split('-')[0], 
        ddlSortPoolPlayers.SelectedValue.Split('-')[1]);

      lbPlayerPool.DataSource = availablePlayers;
      lbPlayerPool.DataTextField = "FullNameLastFirst";
      lbPlayerPool.DataValueField = "PlayerID";
      lbPlayerPool.DataBind();
      labTotalAvailablePlayers.Text = availablePlayers.Count.ToString();
    }

    protected void ibRemovePlayer_Click(object sender, ImageClickEventArgs e)
    {
      Thread.Sleep(1000);
      MessageBox mbMessageBox = (MessageBox)upSheetPlayers.FindControl("mbStatus");
      int removeItemCount = 0;

      switch (this.SheetType)
      {
        case SheetType.CheatSheet:
          for (int i = 0; i < lbSheetPlayers.Items.Count; i++)
          {
            if (lbSheetPlayers.Items[i].Selected == true)
            {

              // only try to remove the player from your sheet if they actually exist on the sheet
              if (this.CurrentCheatSheet.Items.SingleOrDefault(x => x.PlayerID.ToString() == lbSheetPlayers.Items[i].Value) != null)
              {
                int playerId = int.Parse(lbSheetPlayers.Items[i].Value);
                CheatSheet.RemoveCheatSheetItem(this.SheetID, playerId);

                //this.CurrentCheatSheet.Items.RemoveAll(x => x.PlayerID == playerId);
                this.CurrentCheatSheet.Items.Remove(this.CurrentCheatSheet.Items.SingleOrDefault(x => x.PlayerID == playerId));

                removeItemCount++;
              }
            }
          }
          LoadCheatSheetItems();
          LoadCheatSheetAvailablePlayers();
          break;
        case SheetType.SuppSheet:
          for (int i = 0; i < lbSheetPlayers.Items.Count; i++)
          {
            if (lbSheetPlayers.Items[i].Selected == true)
            {
              if (this.CurrentSuppSheet.Items.SingleOrDefault(x => x.PlayerID.ToString() == lbSheetPlayers.Items[i].Value) != null)
              {
                SupplementalSheet.RemoveSupplementalSheetItem(this.SheetID, int.Parse(lbSheetPlayers.Items[i].Value));
                removeItemCount++;
              }
            }
          }
          LoadSuppSheetItems();
          LoadSuppSheetAvailablePlayers();
          break;
      }
      // configure the user message
      DisplayUserMessage(false, removeItemCount);
    }



    protected void ibAddPlayer_Click(object sender, ImageClickEventArgs e)
    {
      Thread.Sleep(1000);
      MessageBox mbMessageBox = (MessageBox)upSheetPlayers.FindControl("mbStatus");
      int addItemCount = 0;

      switch (this.SheetType)
      {
        case SheetType.CheatSheet:
          for (int i = 0; i < lbPlayerPool.Items.Count; i++)
          {
            if (lbPlayerPool.Items[i].Selected == true)
            {
              // only try to add the player if we're sure he doesn't already exist in the cheat sheet
              if (this.CurrentCheatSheet.Items.SingleOrDefault(x => x.PlayerID.ToString() == lbPlayerPool.Items[i].Value) == null)
              {
                int playerId = int.Parse(lbPlayerPool.Items[i].Value);

                CheatSheet.AddCheatSheetItem(this.SheetID, playerId, String.Empty);

                // add false tags representing football tags
                Dictionary<string, object> emptyFootballTags = new Dictionary<string, object>();
                emptyFootballTags.Add(CSIProperty.Sleeper.ToString(), false);
                emptyFootballTags.Add(CSIProperty.Bust.ToString(), false);
                emptyFootballTags.Add(CSIProperty.Injured.ToString(), false);

                this.CurrentCheatSheet.Items.Add(new CheatSheetItem(this.SheetID, playerId, this.CurrentCheatSheet.Items.Count, String.Empty, emptyFootballTags  ));

                addItemCount++;
              }
            }
          }
          LoadCheatSheetItems();
          LoadCheatSheetAvailablePlayers();
          break;
        case SheetType.SuppSheet:
          for (int i = 0; i < lbPlayerPool.Items.Count; i++)
          {
            if (lbPlayerPool.Items[i].Selected == true)
            {
              // only try to add the player if we're sure he doesn't already exist in the supp sheet
              if (this.CurrentSuppSheet.Items.SingleOrDefault(x => x.PlayerID.ToString() == lbPlayerPool.Items[i].Value) == null)
              {
                SupplementalSheet.AddSupplementalSheetItem(this.SheetID, int.Parse(lbPlayerPool.Items[i].Value));
                addItemCount++;
              }

            }
          }
          LoadSuppSheetItems();
          LoadSuppSheetAvailablePlayers();
          break;
      }
      // configure the user message
      DisplayUserMessage(true, addItemCount);
    }

    protected void ddlSortPoolPlayers_SelectedIndexChanged(object sender, EventArgs e)
    {
      switch (this.SheetType)
      {
        case SheetType.CheatSheet:
          LoadCheatSheetAvailablePlayers();
          break;
        case SheetType.SuppSheet:
          LoadSuppSheetAvailablePlayers();
          break;
      }
    }




    protected void ddlSortSheetPlayers_SelectedIndexChanged(object sender, EventArgs e)
    {
      switch (this.SheetType)
      {
        case SheetType.CheatSheet:
          LoadCheatSheetItems();
          break;
        case SheetType.SuppSheet:
          LoadSuppSheetItems();
          break;
      }
    }

    private void DisplayUserMessage(bool added, int count)
    {
      MessageBox mbStatus = (MessageBox)upSheetPlayers.FindControl("mbStatus");
      int sheetID = 0;
      string sheetName = String.Empty;

      if (count == 0)
      {
        mbStatus.MessageType = MessageType.ERROR;
        if (this.SportCode == FOO.FOOString)
        {
          mbStatus.Message = new StringBuilder("No player was selected");
        }
        else
        {
          mbStatus.Message = new StringBuilder("No driver was selected");
        }
      }
      else
      {
        mbStatus.MessageType = MessageType.SUCCESS;
        StringBuilder userMessage = new StringBuilder();
        string action = (added) ? "added to sheet: " : "removed from sheet: ";
        if (this.SportCode == FOO.FOOString)
        {
          if (count > 1)
          {
            userMessage.Append("Players have been successfully " + action);
          }
          else
          {
            userMessage.Append("Player has been successfully " + action);
          }
          if (this.SheetType == SheetType.CheatSheet)
          {
            sheetID = this.CurrentCheatSheet.CheatSheetID;
            sheetName = this.CurrentCheatSheet.SheetName;
            string sheetPage = Page.ResolveClientUrl("~/fantasy-football/nfl/create/custom-sheet.aspx?SheetID=") + sheetID.ToString();
            string sheetLink = " <a title='Click to return to this sheet.' href='" + sheetPage + "'>" + sheetName + " </a>.";
            userMessage.Append(sheetLink);
          }
        }
        else if (this.SportCode == SupportedSport.RAC.ToString())
        {
          if (count > 1)
          {
            userMessage.Append("Drivers have been successfully " + action);
          }
          else
          {
            userMessage.Append("Driver has been successfully " + action);
          }

          if (this.SheetType == SheetType.CheatSheet)
          {
            sheetID = this.CurrentCheatSheet.CheatSheetID;
            sheetName = this.CurrentCheatSheet.SheetName;
            string sheetPage = Page.ResolveClientUrl("~/fantasy-racing/nascar/create/custom-sheet.aspx?SheetID=") + sheetID.ToString();
            string sheetLink = " <a title='Click to return to this sheet.' href='" + sheetPage + "'>" + sheetName + "</a>.";
            userMessage.Append(sheetLink);
          }
        }
        // add link back to sheet
        mbStatus.Message = userMessage;

      }
    }
}
}