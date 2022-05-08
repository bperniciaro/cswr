using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class CSWRFOORankingItemTemplate : System.Web.UI.UserControl
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
    /// This property indicates if the player should be tagged as a Sleeper
    /// </summary>
    private bool _isSleeper = false;
    public bool IsSleeper 
    {
      get
      {
        return _isSleeper;
      }
      set
      {
        _isSleeper = value;
      }
    }

    /// <summary>
    /// This property indicates if the player should be tagged as a Bust
    /// </summary>
    public bool IsBust { get; set; }

    /// <summary>
    /// This method loads the data which builds the player ranking item
    /// </summary>
    public void LoadTemplate()
    {
      // load the Player object
      _currentPlayer = Player.GetPlayer(this.PlayerID);
      string positionName = Position.GetPosition(_currentPlayer.PositionCode).Name;
      SportSeason currentSeason = SportSeason.GetCurrentSportSeason("FOO");

      // configure an anchor
      string playerName = _currentPlayer.FirstName + _currentPlayer.LastName;
      playerAnchor.Attributes.Add("name", playerName);


      // build the ranking text
      labRanking.Text = this.RankLabel;
      
      // build the helmet icon
      imaHelmet.ImageUrl = "~/Images/Sports/Football/UserControls/SuppRankingItemTemplate/Helmets/" + _currentPlayer.Team.Mascot.ToLower() + "helmet.gif";
      imaHelmet.ToolTip = "The " + _currentPlayer.Team.FullTeamName + " have a player listed in our " + currentSeason.SeasonCode + " " + positionName + " rankings.";
      imaHelmet.AlternateText = _currentPlayer.FullName + " of the " + _currentPlayer.Team.FullTeamName + " is ranked " + this.RankLabel + " in our " + currentSeason.SeasonCode + " " + positionName + " rankings.";

      // if this isn't a 'defensive team', we can add player-specific information
      if (!_currentPlayer.IsDefensiveTeamPlayer)
      {
        // twitter icon
        //hlTwitter.Visible = true;
        //hlTwitter.NavigateUrl = "~/fantasy-football/nfl/player-tweets.aspx?PlayerID=" + this.PlayerID.ToString();
        //hlTwitter.ToolTip = "Click to view the latest tweets about " + _currentPlayer.FullName + ".";
        //hlTwitter.CssClass = Team.GetTeam(_currentPlayer.TeamCode).Mascot.ToLower() + "twitter " + "twitterLink";
        //hlTwitter.Target = "_blank";

        // player number
        labNumber.Text = "#" + _currentPlayer.Number.ToString();
        
        // player number may only be applicable if we integrate multiple positions
        //litPositionCode.Text = "| " + _supplementalSheetItem.Player.PositionCode;

        // player name
        labPlayerName.Text = _currentPlayer.FullName;
        // team name
        litTeam.Text = _currentPlayer.Team.Abbreviation + " " + _currentPlayer.Team.Mascot + ", ";
        // experience
        if (_currentPlayer.YearsExperience != 0)
        {
          litExperience.Text = _currentPlayer.YearsExperience.ToString() + " years exp";
        }
        else
        {
          litExperience.Text = "Rookie";
        }
      }
      else
      {
        // pseudo-team name
        labPlayerName.Text = _currentPlayer.FirstName;
      }

      // Dynamically-create CSS classes to create team-specific colors
      string cityName = _currentPlayer.Team.City.Replace('.', ' ');
      string[] cityNames;
      cityNames = cityName.Split(' ');
      cityNames[0] = cityNames[0].ToLower();
      cityName = String.Empty;
      foreach (string name in cityNames)
      {
        cityName = cityName + name;
      }
      string mascotName = _currentPlayer.Team.Mascot;
      string teamStyle = cityName + mascotName;
      divPlayerRankingItem.Attributes.Add("class", "cswrFOORankingItemControl " + teamStyle);

      // sleeper/bust tags
      //if (!this.IsSleeper)
      //{
      //  imaSleeperTag.CssClass = "tagInactive";
      //}
      //if (!this.IsBust)
      //{
      //  imaBustTag.CssClass = "tagInactive";
      //}
    
      // build statistics
      string relevantSeasonCode = String.Empty;
      if (Helpers.IsMiddleOfSeason(FOO.FOOString))
      {
        relevantSeasonCode = currentSeason.SeasonCode;
      }
      else
      {
        relevantSeasonCode = currentSeason.LastSeasonCode;
      }
      List<SportSeasonPlayerSeasonStat> playerStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", relevantSeasonCode, this.PlayerID);
      repStats.DataSource = playerStats;
      repStats.DataBind();

      // bye week
      ByeWeek teamBye = ByeWeek.GetByeWeek(SportSeason.GetCurrentSportSeason("FOO").SeasonCode, "FOO", _currentPlayer.Team.TeamCode);
      if (teamBye != null)
      {
        if (!_currentPlayer.IsDefensiveTeamPlayer)
        {
          litBye.Text = ", Bye " + teamBye.Bye;
        }
        else
        {
          litBye.Text = "Bye " + teamBye.Bye;
        }
      }

    }




    protected void repStats_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        SportSeasonPlayerSeasonStat currentStat = (SportSeasonPlayerSeasonStat)e.Item.DataItem;
        Label labStatCode = (Label)e.Item.FindControl("labStatCode");
        Literal litStatValue = (Literal)e.Item.FindControl("litStatValue");


        labStatCode.Text = currentStat.StatCode;
        labStatCode.ToolTip = Stat.GetStat(currentStat.StatCode).Name;
        
        List<SportSeasonPlayerSeasonStat> source = (List<SportSeasonPlayerSeasonStat>)repStats.DataSource;
        if(e.Item.ItemIndex < source.Count-1)  {
          litStatValue.Text = Math.Round(currentStat.StatValue, 1).ToString() + ", ";
        }
        else  {
          litStatValue.Text = currentStat.StatValue.ToString();
        }
      }
    }
  }
}