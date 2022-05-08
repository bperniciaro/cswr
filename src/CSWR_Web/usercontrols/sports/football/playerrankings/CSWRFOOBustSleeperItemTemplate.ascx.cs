using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class CSWRFOOBustSleeperItemTemplate : System.Web.UI.UserControl
  {
    private Player _currentPlayer;

    /// <summary>
    /// This is the string that will get populated in the 'ranking number' area
    /// </summary>
    public string RankDifference { get; set; }

    public BustSleeperComparisonSource ComparisonSource { get; set; }

    public DifferentialPlayerType PlayerType { get; set; }

    public SSIProperty Status { get; set; }

    public int CSWRRank { get; set; }
    public int CBSRank { get; set; }
    public int ADP { get; set; }

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

      // build the helmet icon
      imaHelmet.ImageUrl = "~/Images/Sports/Football/UserControls/SuppRankingItemTemplate/Helmets/" + _currentPlayer.Team.Mascot.ToLower() + "helmet.gif";

      // if this isn't a 'defensive team', we can add player-specific information
      if (!_currentPlayer.IsDefensiveTeamPlayer)
      {
        // twitter icon
        //hlTwitter.Visible = false;
        //hlTwitter.NavigateUrl = "~/fantasy-football/nfl/player-tweets.aspx?PlayerID=" + this.PlayerID.ToString();
        //hlTwitter.ToolTip = "Click to view the latest tweets about " + _currentPlayer.FullName + ".";
        //hlTwitter.CssClass = Team.GetTeam(_currentPlayer.TeamCode).Mascot.ToLower() + "twitter " + "twitterLink";
        //hlTwitter.Target = "_blank";

        // player number
        litNumber.Text = "#" + _currentPlayer.Number.ToString();
        // player name
        labPlayerName.Text = _currentPlayer.FullName;
        // position
        // team name
        litTeam.Text = _currentPlayer.Team.Abbreviation + " " + _currentPlayer.Team.Mascot;
        // experience
        if (_currentPlayer.YearsExperience >= 1)
        {
          litExperience.Text = "- " + _currentPlayer.YearsExperience.ToString() + " years exp";
        }
        else
        {
          litExperience.Text = "- Rookie";
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
      divPlayerRankingItem.Attributes.Add("class", "cswrFOOSBTemplateControl " + teamStyle);

      imaHelmet.AlternateText = _currentPlayer.Team.City + " " + mascotName + " helmet";
   
      // build statistics
      string previousSeasonCode = SportSeason.GetCurrentSportSeason("FOO").LastSeasonCode;
      List<SportSeasonPlayerSeasonStat> playerStats = SportSeasonPlayerSeasonStat.GetSportSeasonPlayerSeasonStats("FOO", previousSeasonCode, this.PlayerID);
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


      HandlePlayerStatus(_currentPlayer);
    }

    public void HandlePlayerStatus(Player currentPlayer)
    {
      int difference = 0;

      switch (this.Status)
      {
        case SSIProperty.Sleeper:
          //difference = this.CBSRank - this.CSWRRank;
          if (this.CBSRank != 0)
          {
            labRankDifference.Text = "+" + this.RankDifference;
            panRankings.CssClass = "sleeper";
          }
          else
          {
            labRankDifference.Text = "~" + this.RankDifference;
            panRankings.CssClass = "sleeper";
          }

          break;
        case SSIProperty.Bust:
          //difference = this.CSWRRank - this.CBSRank;
          labRankDifference.Text = "-" + this.RankDifference;
          panRankings.CssClass = "bust";
          break;
      }


      /* Configure the hyperlink which points to our positional rankings */
      Position targetPosition = Position.GetPosition(_currentPlayer.PositionCode);

      labCSWRRank.Text = "Ranked " + this.CSWRRank.ToString().AddNumberSuffix() + " among ";
      // configure the nested acronym tag
      acrPositionName.InnerText = targetPosition.PositionCode + "s";
      acrPositionName.Attributes.Add("title", targetPosition.Name);
      string positionalPage = targetPosition.Name.ToLower().Replace(' ', '-') + "s.aspx";

      // configurate the hyperlink
      hlCSWRRank.NavigateUrl = "~/fantasy-football/nfl/free/rankings/offense/" + positionalPage + "#" + currentPlayer.FirstName + currentPlayer.LastName;
      hlCSWRRank.ToolTip = "View our latest " + _currentPlayer.PositionCode + " rankings.";

      if (this.CBSRank != 0)
      {
        labCBSRank.Text = "Ranked " + this.CBSRank.ToString().AddNumberSuffix() + " among " + targetPosition.Abbreviation + "s";
      }
      else
      {
        labCBSRank.Text = "not ranked";
      }
      
      imaHelmet.ToolTip = _currentPlayer.FullName + " is projected to be a " + this.PlayerType.ToString().ToLower() + " in " + FOO.CurrentSeason + ".";

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