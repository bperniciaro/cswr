using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{

  public partial class TeamPlayerRanks : System.Web.UI.Page
  {

    private int SheetID { get; set; }
    private int PlayerID { get; set; }
    private SheetType CurrentSheetType { get; set; }
   

    //private string _seasonCode;
    private List<CheatSheetPosition> _sheetPositions;
    private string _positionCode;  // used only for supplemental sheets
    private bool _showSuppRankings = false;



    protected void Page_Load(object sender, EventArgs e)
    {
      if (LoadRequestVariables())
      {
        LoadTeamRankings();
      }
    }



    private void LoadTeamRankings()
    {
      switch (this.CurrentSheetType)
      {
        // determine positions and whether or not to show supplemental rankings
        case SheetType.CheatSheet:
          _sheetPositions = CheatSheet.GetCheatSheet(this.SheetID).CheatSheetPositions;
          if (_sheetPositions.Count == 1)
          {
            _positionCode = _sheetPositions[0].PositionCode;
          }
          _showSuppRankings = (_sheetPositions.Count == 1);
          break;
        case SheetType.SuppSheet:
          _positionCode = SupplementalSheet.GetSupplementalSheet(this.SheetID).PositionCode;
          _showSuppRankings = true;
          break;
      }

      // determine table header visibility
      if (!_showSuppRankings)
      {
        thCSWRHeader.Visible = false;
        //thCBSHeader.Visible = false;
      }
      else
      {
        thPositionHeader.Visible = false;
      }

      Player targetPlayer = Player.GetPlayer(this.PlayerID);
      if (targetPlayer != null)
      {
        switch (this.CurrentSheetType)
        {
          case SheetType.CheatSheet:
            repPlayers.DataSource = CheatSheetItem.GetCheatSheetItems(this.SheetID).Where(x => x.Player.TeamCode == targetPlayer.TeamCode).OrderBy(x => x.Seqno).ToList();
            break;
          case SheetType.SuppSheet:
            repPlayers.DataSource = SupplementalSheetItem.GetSupplementalSheetItems(this.SheetID).Where(x => x.Player.TeamCode == targetPlayer.TeamCode).OrderBy(x => x.Seqno).ToList();
            break;
        }
        repPlayers.DataBind();
      }

    }

    protected void repPlayers_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        SheetItem bountItem = (SheetItem)e.Item.DataItem;

        Label labRank = (Label)e.Item.FindControl("labRank");
        Label labName = (Label)e.Item.FindControl("labName");
        Label labPosition = (Label)e.Item.FindControl("labPosition");
        Label labCSWRRank = (Label)e.Item.FindControl("labCSWRRank");
        HyperLink hlCSWRRank = (HyperLink)e.Item.FindControl("hlCSWRRank");
        Label labCBSRank = (Label)e.Item.FindControl("labCBSRank");
        HyperLink hlPlayerAnchor = (HyperLink)e.Item.FindControl("hlPlayerAnchor");


        HtmlControl tdPositionCell = (HtmlControl)e.Item.FindControl("tdPositionCell");
        HtmlControl tdCSWRCell = (HtmlControl)e.Item.FindControl("tdCSWRCell");
        HtmlControl tdCBSCell = (HtmlControl)e.Item.FindControl("tdCBSCell");


        if (_showSuppRankings)
        {
          // determine CSWR rank

          SupplementalSource cswrSource = SupplementalSource.GetSupplementalSource("CSWR");
          SupplementalSheet cswrSheet = SupplementalSheet.GetSupplementalSheet(FOO.CurrentSeason, cswrSource.SupplementalSourceID,
                                            FOO.FOOString, _positionCode);
          SupplementalSheetItem targetCSWRItem = SupplementalSheetItem.GetSupplementalSheetItems(cswrSheet.SupplementalSheetID).SingleOrDefault(x => x.PlayerID == bountItem.PlayerID);
          if (targetCSWRItem != null)
          {


            hlCSWRRank.Text = targetCSWRItem.Seqno.ToString();
            switch (_positionCode)
            {
              case "QB":
                hlCSWRRank.NavigateUrl = "https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/rankings/offense/quarterbacks.aspx";
                break;
              case "RB":
                hlCSWRRank.NavigateUrl = "https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/rankings/offense/running-backs.aspx";
                break;
              case "WR":
                hlCSWRRank.NavigateUrl = "https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/rankings/offense/wide-receivers.aspx";
                break;
              case "TE":
                hlCSWRRank.NavigateUrl = "https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/rankings/offense/tight-ends.aspx";
                break;
              case "K":
                hlCSWRRank.NavigateUrl = "https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/rankings/offense/kickers.aspx";
                break;
              case "DF":
                hlCSWRRank.NavigateUrl = "https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/rankings/offense/defenses.aspx";
                break;
            }

            hlCSWRRank.ToolTip = "Click to view all CSWR " + _positionCode + " rankings";

          }
          else
          {
            labCSWRRank.Text = "n/r";
            labCSWRRank.ToolTip = "CSWR does not have this player ranked.";
            labCSWRRank.CssClass = "notRanked";
            hlCSWRRank.Visible = false;
          }

          // determine CBSSports rank
          SupplementalSource cbsSource = SupplementalSource.GetSupplementalSource("CBS");
          SupplementalSheet cbsSheet = SupplementalSheet.GetSupplementalSheet(FOO.CurrentSeason, cbsSource.SupplementalSourceID,
                                            FOO.FOOString, _positionCode);
          if (cbsSheet != null)
          {
            SupplementalSheetItem targetCBSItem = SupplementalSheetItem.GetSupplementalSheetItems(cbsSheet.SupplementalSheetID).SingleOrDefault(x => x.PlayerID == bountItem.PlayerID);
            if (targetCBSItem != null)
            {
              labCBSRank.Text = targetCBSItem.Seqno.ToString();
            }
            else
            {
              labCBSRank.Text = "n/r";
              labCBSRank.ToolTip = "CBS does not have this player ranked.";
              labCBSRank.CssClass = "notRanked";
            }
          }
          else
          {
            labCBSRank.Text = "n/r";
          }
          tdPositionCell.Visible = false;
        }
        else
        {
          labPosition.Text = bountItem.Player.PositionCode;
          tdCSWRCell.Visible = false;
          tdCBSCell.Visible = false;
        }

        // configure a text ranking for when we're showing the target players
        labRank.Text = bountItem.Seqno.ToString();

        // build the anchor to jump to player
        hlPlayerAnchor.Text = bountItem.Seqno.ToString();
        hlPlayerAnchor.NavigateUrl = "#" + bountItem.Player.FirstName + bountItem.Player.LastName;
        hlPlayerAnchor.ToolTip = "Click to jump directly to " + bountItem.Player.FirstName + " " + bountItem.Player.LastName;

        labName.Text = bountItem.Player.FullName;

        // if this is the player who the user hovered over
        labRank.Visible = false;
        hlPlayerAnchor.Visible = true;
        if (bountItem.Player.PlayerID == this.PlayerID)
        {
          HtmlControl trRankingRow = (HtmlControl)e.Item.FindControl("trRankingRow");
          trRankingRow.Attributes["class"] = "targetPlayerRow";
          labRank.Visible = true;
          hlPlayerAnchor.Visible = false;

        }
        else if (e.Item.ItemType == ListItemType.AlternatingItem)
        {
          HtmlControl trRankingRow = (HtmlControl)e.Item.FindControl("trRankingRow");
          trRankingRow.Attributes["class"] = "alternatingRow";
        }
      }

    }

    private bool LoadRequestVariables()
    {
      // if we receive all of the expected variables...
      if ((Request["sheetid"] != null) && (Request["playerid"] != null))
      {
        //SheetID
        if (Request["sheetid"] != null)
        {
          int sheetID = 0;
          if (int.TryParse(Request["sheetid"], out sheetID))
          {
            this.SheetID = sheetID;

          }
          else
          {
            return false;
          }
        }

        //PlayerID
        if (Request["playerid"] != null)
        {
          int playerID = 0;
          if (int.TryParse(Request["playerid"], out playerID))
          {
            this.PlayerID = playerID;

          }
          else
          {
            return false;
          }
        }

        //SheetType
        if (Request["sheettype"] != null)
        {
          if (Request["sheettype"] == SheetType.CheatSheet.ToString().ToLower())
          {
            this.CurrentSheetType = SheetType.CheatSheet;
          }
          else
          {
            this.CurrentSheetType = SheetType.SuppSheet;
          }
        }


        return true;
      }
      else
      {
        return false;
      }
    }



  }
}