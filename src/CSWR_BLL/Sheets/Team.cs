using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Summary description for Team
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  [Serializable()]
  public class Team : BaseSheet
  {

    public Team(string teamCode, string sportCode, string city, string mascot, string abbreviation, int statMapID)
    {
      this.TeamCode = teamCode;
      this.SportCode = sportCode;
      this.City = city;
      this.Mascot = mascot;
      this.Abbreviation = abbreviation;
      this.StatMapID = statMapID;
    }

    // Team Code
    public string TeamCode {get;set;}
    public string SportCode {get;set;}
    public string City {get;set;}
    public string Mascot {get;set;}
    public string Abbreviation {get;set;}
    public int StatMapID {get;set;}


    public string MascotCSSClass
    {
      get
      {
        return (this.Mascot.ToLower() != "49ers") ? this.Mascot.ToLower() : "fortyNiners";

      }
    }

    // This is a CSS class based on a city name, it appends the portions of the city name
    // in the proper camel-case format
    public string CityCSSClass
    {
      get  
      {
        string cityCSSClass = String.Empty;

        // remove periods
        string cityName = this.City.Replace('.', ' ');
        // generate the CSS class based on the city name parts           
        string[] cityNameParts = cityName.Split(' ');
        cityCSSClass = cityNameParts[0].ToLower();

        int partCounter = 0;
        foreach (string name in cityNameParts)
        {
          if (partCounter > 0)
          {
            cityCSSClass += name;
          }
          partCounter++;
        }
        return cityCSSClass;
      }
    }

    // Full Team name with Mascot, LAZY LOADED
    private string _fullTeamName = "";
    public string FullTeamName
    {
      get  {
        if (_fullTeamName == "")
        {
          _fullTeamName = this.City + " " + this.Mascot;
        }
        return _fullTeamName;
      }
    }





    /// <summary>
    /// Returns a Team object with the specified code
    /// </summary>
    public static Team GetTeam(string teamCode)
    {
      Team team = null;
      string key = "Sheets_Team_" + teamCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        team = (Team)BizObject.Cache[key];
      }
      else
      {
        team = GetTeamFromTeamDetails(SiteProvider.Sheets.GetTeam(teamCode));
        BaseSheet.CacheData(key, team);
      }
      return team;
    }

    /// <summary>
    /// Returns a Team object with the specified code
    /// </summary>
    public static Team GetTeam(string sportCode, string mascot)
    {
      Team team = null;
      string key = "Sheets_Team_Sport_" + sportCode + "_" + mascot;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        team = (Team)BizObject.Cache[key];
      }
      else
      {
        team = GetTeamFromTeamDetails(SiteProvider.Sheets.GetTeam(sportCode, mascot));
        BaseSheet.CacheData(key, team);
      }
      return team;
    }


    /// <summary>
    /// Returns a Team object with the specified code
    /// </summary>
    public static Team GetTeam(int playerID)
    {
      Team team = null;
      string key = "Sheets_Team_" + playerID.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        team = (Team)BizObject.Cache[key];
      }
      else
      {
        team = GetTeamFromTeamDetails(SiteProvider.Sheets.GetTeam(playerID));
        BaseSheet.CacheData(key, team);
      }
      return team;
    }


    /// <summary>
    /// Returns a collection with all the categories
    /// </summary>
    public static List<Team> GetTeams(string sportCode)
    {
      List<Team> teams = null;
      string key = "Sheets_Teams_" + sportCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        teams = (List<Team>)BizObject.Cache[key];
      }
      else
      {
        List<TeamDetails> recordset = SiteProvider.Sheets.GetTeams(sportCode);
        teams = GetTeamListFromTeamDetailsList(recordset);
        BaseSheet.CacheData(key, teams);
      }
      return teams.GetRange(0, teams.Count);
    }


    /// <summary>
    /// Returns a Team object filled with the data taken from the input TeamDetails
    /// </summary>
    private static Team GetTeamFromTeamDetails(TeamDetails team)
    {
      if (team == null)
        return null;
      else
      {
        return new Team(team.TeamCode, team.SportCode, team.City, team.Mascot, team.Abbreviation, team.StatMapID);
      }
    }

    /// <summary>
    /// Returns a list of Team objects filled with the data taken from the input list of TeamDetails
    /// </summary>
    private static List<Team> GetTeamListFromTeamDetailsList(List<TeamDetails> recordset)
    {
      List<Team> teams = new List<Team>();
      foreach (TeamDetails record in recordset)
        teams.Add(GetTeamFromTeamDetails(record));
      return teams;
    }


  }
}