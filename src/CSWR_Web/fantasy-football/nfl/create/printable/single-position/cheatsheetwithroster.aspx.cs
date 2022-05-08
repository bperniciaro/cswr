using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class CheatSheetWithRosterSingle : System.Web.UI.Page
  {
    private int _quarterbackCounter = 0;
    private int _runningBackCounter = 0;
    private int _wideReceiverCounter = 0;
    private int _tightEndCounter = 0;
    private int _kickerCounter = 0;
    private int _defenseCounter = 0;

    private int _QBID = 0;
    private int _RBID = 0;
    private int _WRID = 0;
    private int _TEID = 0;
    private int _KID = 0;
    private int _DFID = 0;

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
          // load the players into the cheat sheet
          LoadQuarterbacks();
          LoadRunningBacks();
          LoadWideReceivers();
          LoadTightEnds();
          LoadKickers();
          LoadDefenses();

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



    private void LoadQuarterbacks()
    {
      // Quarterbacks
      if (Request.QueryString["QB"] == "SS")
      {
        SupplementalSheet targetSheet = SupplementalSheet.GetSupplementalSheet(FOO.CurrentSeason,
          _QBID, FOO.FOOString, FOOPositionsOffense.QB.ToString());

        List<SupplementalSheetItem> supplementalSheetItems = SupplementalSheetItem.GetSupplementalSheetItems(targetSheet.SupplementalSheetID);

        // load sheet and notes
        repQuarterbacks.DataSource = supplementalSheetItems.Take(42);
      }
      else
      {
        List<CheatSheetItem> cheatSheetItems = CheatSheetItem.GetCheatSheetItems(_QBID);

        // load sheet and notes
        repQuarterbacks.DataSource = cheatSheetItems.Take(42);

        // load notes
        nsNoteSummary.QBNotes = cheatSheetItems.Where(x => x.Note != String.Empty).ToList();
      }
      repQuarterbacks.DataBind();
    }


    private void LoadRunningBacks()
    {
      // Running Backs
      if (Request.QueryString["RB"] == "SS")
      {
        SupplementalSheet targetSheet = SupplementalSheet.GetSupplementalSheet(FOO.CurrentSeason,
          _RBID, FOO.FOOString, FOOPositionsOffense.RB.ToString());
        // 50 is all that will fit on this sheet type
        repRunningBacks.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetSheet.SupplementalSheetID).Take(50);
      }
      else
      {
        List<CheatSheetItem> cheatSheetItems = CheatSheetItem.GetCheatSheetItems(_RBID);
        repRunningBacks.DataSource = cheatSheetItems.Take(50);

        // load notes
        nsNoteSummary.RBNotes = cheatSheetItems.Where(x => x.Note != String.Empty).ToList();
      }
      repRunningBacks.DataBind();
    }

    private void LoadWideReceivers()
    {
      // Wide Receivers
      if (Request.QueryString["WR"] == "SS")
      {
        SupplementalSheet targetSheet = SupplementalSheet.GetSupplementalSheet(FOO.CurrentSeason,
          _WRID, FOO.FOOString, FOOPositionsOffense.WR.ToString());
        repWideReceivers.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetSheet.SupplementalSheetID).Take(50);
      }
      else
      {
        List<CheatSheetItem> cheatSheetItems = CheatSheetItem.GetCheatSheetItems(_WRID);
        repWideReceivers.DataSource = cheatSheetItems.Take(50);

        // load notes
        nsNoteSummary.WRNotes = cheatSheetItems.Where(x => x.Note != String.Empty).ToList();
      }
      repWideReceivers.DataBind();
    }

    private void LoadTightEnds()
    {
      // TightEnds
      if (Request.QueryString["TE"] == "SS")
      {
        SupplementalSheet targetSheet = SupplementalSheet.GetSupplementalSheet(FOO.CurrentSeason,
          _TEID, FOO.FOOString, FOOPositionsOffense.TE.ToString());
        repTightEnds.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetSheet.SupplementalSheetID).Take(42);
      }
      else
      {
        List<CheatSheetItem> cheatSheetItems = CheatSheetItem.GetCheatSheetItems(_TEID);
        repTightEnds.DataSource = cheatSheetItems.Take(42);

        // load notes
        nsNoteSummary.TENotes = cheatSheetItems.Where(x => x.Note != String.Empty).ToList();
      }
      repTightEnds.DataBind();
    }

    private void LoadKickers()
    {
      // Kickers
      if (Request.QueryString["K"] == "SS")
      {
        SupplementalSheet targetSheet = SupplementalSheet.GetSupplementalSheet(FOO.CurrentSeason,
          _KID, FOO.FOOString, FOOPositionsOffense.K.ToString());
        repKickers.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetSheet.SupplementalSheetID).Take(23);
      }
      else
      {
        List<CheatSheetItem> cheatSheetItems = CheatSheetItem.GetCheatSheetItems(_KID);
        repKickers.DataSource = cheatSheetItems.Take(23);

        // load notes
        nsNoteSummary.KNotes = cheatSheetItems.Where(x => x.Note != String.Empty).ToList();
      }
      repKickers.DataBind();
    }

    private void LoadDefenses()
    {
      // Defenses
      if (Request.QueryString["DF"] == "SS")
      {
        SupplementalSheet targetSheet = SupplementalSheet.GetSupplementalSheet(FOO.CurrentSeason,
          _DFID, FOO.FOOString, FOOPositionsOffense.DF.ToString());
        repDefenses.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(targetSheet.SupplementalSheetID).Take(23);
      }
      else
      {
        List<CheatSheetItem> cheatSheetItems = CheatSheetItem.GetCheatSheetItems(_DFID);
        repDefenses.DataSource = cheatSheetItems.Take(23);

        // load notes
        nsNoteSummary.DFNotes = cheatSheetItems.Where(x => x.Note != String.Empty).ToList();
      }
      repDefenses.DataBind();
    }

    private bool ValidateInput()
    {
      bool success = true;
      // Validate QB Input
      if (Request.QueryString["QB"] != null)
      {
        if ((Request.QueryString["QB"] != "CS") && (Request.QueryString["QB"] != "SS"))
        {
          success = false;
        }
        else
        {
          if (!int.TryParse(Request.QueryString["QBID"], out _QBID))
          {
            success = false;
          }
        }
      }
      else
      {
        success = false;
      }
      // Validate RB Input
      if (Request.QueryString["RB"] != null)
      {
        if ((Request.QueryString["RB"] != "CS") && (Request.QueryString["RB"] != "SS"))
        {
          success = false;
        }
        else
        {
          if (!int.TryParse(Request.QueryString["RBID"], out _RBID))
          {
            success = false;
          }
        }
      }
      else
      {
        success = false;
      }
      // Validate WR Input
      if (Request.QueryString["WR"] != null)
      {
        if ((Request.QueryString["WR"] != "CS") && (Request.QueryString["WR"] != "SS"))
        {
          success = false;
        }
        else
        {
          if (!int.TryParse(Request.QueryString["WRID"], out _WRID))
          {
            success = false;
          }
        }
      }
      else
      {
        success = false;
      }
      // Validate TE Input
      if (Request.QueryString["TE"] != null)
      {
        if ((Request.QueryString["TE"] != "CS") && (Request.QueryString["TE"] != "SS"))
        {
          success = false;
        }
        else
        {
          if (!int.TryParse(Request.QueryString["TEID"], out _TEID))
          {
            success = false;
          }
        }
      }
      else
      {
        success = false;
      }
      // Validate K Input
      if (Request.QueryString["K"] != null)
      {
        if ((Request.QueryString["K"] != "CS") && (Request.QueryString["K"] != "SS"))
        {
          success = false;
        }
        else
        {
          if (!int.TryParse(Request.QueryString["KID"], out _KID))
          {
            success = false;
          }
        }
      }
      else
      {
        success = false;
      }
      // Validate DF Input
      if (Request.QueryString["DF"] != null)
      {
        if ((Request.QueryString["DF"] != "CS") && (Request.QueryString["DF"] != "SS"))
        {
          success = false;
        }
        else
        {
          if (!int.TryParse(Request.QueryString["DFID"], out _DFID))
          {
            success = false;
          }
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
        Label teamAbbreviation = (Label)(e.Item.FindControl("labTeamAbbreviation"));
        Label byeWeek = (Label)(e.Item.FindControl("labByeWeek"));
        Image sleeperTag = (Image)(e.Item.FindControl("imaSleeperTag"));
        Image bustTag = (Image)(e.Item.FindControl("imaBustTag"));
        Image injuredTag = (Image)(e.Item.FindControl("imaInjuredTag"));

        if (e.Item.DataItem is CheatSheetItem)
        {
          CheatSheetItem currentItem = (CheatSheetItem)e.Item.DataItem;
          LoadCheatSheetItemData(ref currentItem, ref playerName, ref rank, ref sleeperTag, ref bustTag, ref injuredTag, ref byeWeek, ref teamAbbreviation);
        }
        else if (e.Item.DataItem is SupplementalSheetItem)
        {
          SupplementalSheetItem currentItem = (SupplementalSheetItem)e.Item.DataItem;
          LoadSuppSheetItemData(ref currentItem, ref playerName, ref rank, ref sleeperTag, ref bustTag, ref byeWeek, ref teamAbbreviation);
        }
      }
    }


    void LoadCheatSheetItemData(ref CheatSheetItem currentItem, ref Literal playerName, ref Label rank, ref Image sleeperTag, ref Image bustTag, ref Image injuredTag, ref Label byeWeek, ref Label teamAbbreviation)
    {
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
      int currentRank = 0;
      switch (currentItem.Player.PositionCode)
      {
        case "QB":
          currentRank = ++_quarterbackCounter;
          break;
        case "RB":
          currentRank = ++_runningBackCounter;
          break;
        case "WR":
          currentRank = ++_wideReceiverCounter;
          break;
        case "TE":
          currentRank = ++_tightEndCounter;
          break;
        case "K":
          currentRank = ++_kickerCounter;
          break;
        case "DF":
          currentRank = ++_defenseCounter;
          break;
      }
      rank.Text = currentRank.ToString();
      // Team Abbreviation
      if (currentItem.Player.PositionCode != "DF")
      {
        teamAbbreviation.Text = "(" + Team.GetTeam(currentItem.Player.TeamCode).Abbreviation + ")";
      }
      // Bye Week
      ByeWeek playerBye = ByeWeek.GetByeWeek(SportSeason.GetCurrentSportSeason(SessionHandler.CurrentSportCode).SeasonCode, SessionHandler.CurrentSportCode, currentItem.Player.TeamCode);
      if (playerBye != null)
      {
        byeWeek.Text = "[" + playerBye.Bye.ToString() + "]";
      }
      else
      {
        //byeWeek.Text = "[n/a]";
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



    void LoadSuppSheetItemData(ref SupplementalSheetItem currentItem, ref Literal playerName, ref Label rank, ref Image sleeperTag, ref Image bustTag, ref Label byeWeek, ref Label teamAbbreviation)
    {
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
      int currentRank = 0;
      switch (currentItem.Player.PositionCode)
      {
        case "QB":
          currentRank = ++_quarterbackCounter;
          break;
        case "RB":
          currentRank = ++_runningBackCounter;
          break;
        case "WR":
          currentRank = ++_wideReceiverCounter;
          break;
        case "TE":
          currentRank = ++_tightEndCounter;
          break;
        case "K":
          currentRank = ++_kickerCounter;
          break;
        case "DF":
          currentRank = ++_defenseCounter;
          break;
      }
      rank.Text = currentRank.ToString();
      // Team Abbreviation
      if (currentItem.Player.PositionCode != "DF")
      {
        teamAbbreviation.Text = "(" + Team.GetTeam(currentItem.Player.TeamCode).Abbreviation + ")";
      }
      // Bye Week
      ByeWeek playerBye = ByeWeek.GetByeWeek(SportSeason.GetCurrentSportSeason(SessionHandler.CurrentSportCode).SeasonCode, SessionHandler.CurrentSportCode, currentItem.Player.TeamCode);
      if (playerBye != null)
      {
        byeWeek.Text = "[" + playerBye.Bye.ToString() + "]";
      }
      else
      {
        //byeWeek.Text = "[n/a]";
      }
      // Tags
      if ((bool)currentItem.MappedProperties[SSIProperty.Sleeper.ToString()] == true)
      {
        sleeperTag.Visible = true;
      }
      if ((bool)currentItem.MappedProperties[SSIProperty.Bust.ToString()] == true)
      {
        bustTag.Visible = true;
      }
    }

}
}