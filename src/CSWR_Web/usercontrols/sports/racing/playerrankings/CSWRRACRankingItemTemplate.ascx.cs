using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class CSWRRACRankingItemTemplate : System.Web.UI.UserControl
  {
    private Player _currentPlayer;

    /// <summary>
    /// This is the string that will get populated in the 'ranking number' area
    /// </summary>
    public string RankLabel { get; set; }

    /// <summary>
    /// This is the ID of the player being bound
    /// </summary>
    public int PlayerID { get; set; }


    /// <summary>
    /// This method loads the data which builds the player ranking item
    /// </summary>
    public void LoadTemplate()
    {
      // load the Player object
      _currentPlayer = Player.GetPlayer(this.PlayerID);

      // rankings
      if (this.RankLabel.Contains("."))
      {
        labRanking.Text = this.RankLabel.Substring(0, this.RankLabel.IndexOf('.'));
        labDecimalPart.Text = this.RankLabel.Substring(this.RankLabel.IndexOf('.'), 2);
      }
      else
      {
        labRanking.Text = this.RankLabel;
      }

      // driver number
      //labNumber.Text = _currentPlayer.Number.ToString();
      labCarNumber.Text = "#" + _currentPlayer.Number.ToString();

      // driver name
      litPlayerName.Text = _currentPlayer.FullName;

      // team (i.e. make) name
      litTeam.Text = _currentPlayer.Team.Abbreviation + " " + _currentPlayer.Team.Mascot + ", ";
      
      // experience
      litExperience.Text = _currentPlayer.YearsExperience.ToString() + " years exp";
      
      // for NASCAR ranking templates the CSS styles won't change
      divPlayerRankingItem.Attributes.Add("class", "cswrRACRankingItemControl " + "nascarDriver");

      // car number CSS classes are probably all but useless
      string carNumberCellClass = _currentPlayer.FirstName.ToLower() + _currentPlayer.LastName + " ";

      labCarNumber.CssClass = carNumberCellClass.Replace(" ", String.Empty).Replace(".", String.Empty).Trim();

      // configure twitter
      //hlTwitter.Visible = true;
      //hlTwitter.NavigateUrl = "~/fantasy-racing/nascar/driver-tweets.aspx?PlayerID=" + _currentPlayer.PlayerID.ToString();
      //hlTwitter.ToolTip = "Click to view the latest tweets about " + _currentPlayer.FullName + ".";
      //hlTwitter.Target = "_blank";

      // bind statistics
      int statSeason = int.Parse(SportSeason.GetCurrentSportSeason("RAC").SeasonCode) - 1;
      List<SportSeasonPlayerSeasonStat> playerStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("RAC", statSeason.ToString(), _currentPlayer.PlayerID);
      repStats.DataSource = playerStats;
      repStats.DataBind();
    }




    protected void repStats_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        SportSeasonPlayerSeasonStat currentStat = (SportSeasonPlayerSeasonStat)e.Item.DataItem;
        Label labStatCode = (Label)e.Item.FindControl("labStatCode");
        Literal litStatValue = (Literal)e.Item.FindControl("litStatValue");

        // statCode
        labStatCode.Text = currentStat.StatCode;
        labStatCode.ToolTip = Stat.GetStat(currentStat.StatCode).Name;
        
        // statValue
        List<SportSeasonPlayerSeasonStat> source = (List<SportSeasonPlayerSeasonStat>)repStats.DataSource;
        if (currentStat.StatCode == "WNGS")
        {
          litStatValue.Text = currentStat.StatValue.ToFormattedMoney();
        }
        else
        {
          litStatValue.Text = currentStat.StatValue.ToString();
        }
        if(e.Item.ItemIndex < source.Count-1)  
        {
          litStatValue.Text += ", ";
        }
      }
    }
  }
}