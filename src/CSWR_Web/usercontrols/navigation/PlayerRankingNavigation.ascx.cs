using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class PlayerRankingNavigation : System.Web.UI.UserControl
  {

    private FOOPositionsOffense _currentPosition = new FOOPositionsOffense();
    private bool PositionSet { get; set; }

    private CSWRRankingType _rankingType = CSWRRankingType.CSWRRank;
    public CSWRRankingType RankingType 
    {
      get
      {
        return _rankingType;
      }
      set
      {
        _rankingType = value;
      }
    }

    public FOOPositionsOffense CurrentPosition
    {
      get
      {
        return _currentPosition;
      }
      set
      {
        this.PositionSet = true;
        _currentPosition = value;
      }
    }

    public string CssClass
    {
      set { ulNavigationList.Attributes["class"] = value; }
    }

    public void Page_Load(object sender, EventArgs e)  
    {
      List<Position> fooPositions = Position.GetPositions(SessionHandler.CurrentSportCode);
      repMenu.DataSource = fooPositions;
      repMenu.DataBind();
    }

    protected void  repMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        Position boundPosition = (Position)e.Item.DataItem;

        Label labPosition = (Label)e.Item.FindControl("labPosition");
        HyperLink hlLink = (HyperLink)e.Item.FindControl("hlPositionLink");
        HtmlGenericControl liItem = (HtmlGenericControl)e.Item.FindControl("item");

        if (this.RankingType == CSWRRankingType.ADP)
        {
          hlLink.Text = boundPosition.Abbreviation + " ADP";
        }
        else
        {
          hlLink.Text = boundPosition.Abbreviation + "s";
        }

        if ( (this.PositionSet) && (boundPosition.PositionCode == Enum.GetName(typeof(FOOPositionsOffense), this.CurrentPosition).Trim()) )
        {
          liItem.Attributes.Add("class", "active");
        }
        else
        {
          //labPosition.Visible = false;
          if (this.RankingType == CSWRRankingType.ADP)
          {
            hlLink.NavigateUrl = "~/fantasy-football/nfl/free/rankings/adp/" + boundPosition.Name.Replace(' ', '-').ToLower() + "s.aspx";
          }
          else
          {
            hlLink.NavigateUrl = "~/fantasy-football/nfl/free/rankings/offense/" + boundPosition.Name.Replace(' ', '-').ToLower() + "s.aspx";
          }
        }

      }
    }




  }
}