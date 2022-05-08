using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class Sport : BaseSheet
  {
    public Sport(string sportCode, string sportName, string leagueName, string leagueAbbreviation)
    {
      this.SportCode = sportCode;
      this.SportName = sportName;
      this.LeagueName = leagueName;
      this.LeagueAbbreviation = leagueAbbreviation;
    }

    // Sport Code

    /// <summary>
    /// The unique code for the sport
    /// </summary>
    public string SportCode  { get;set;}

    /// <summary>
    /// The name of the sport
    /// </summary>
    public string SportName {get;set;}

    // League Name
    //private string _leagueName = "";

    /// <summary>
    /// The name of the league the sport is in
    /// </summary>
    public string LeagueName {get;set;}

    /// <summary>
    /// The abbreviation of the league the sport is in
    /// </summary>
    public string LeagueAbbreviation {get;set;}



    /// <summary>
    /// Returns a Sport with the specified code
    /// </summary>
    public static Sport GetSport(string sportCode)
    {
      Sport sport = null;
      string key = "Sheets_Sport_" + sportCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        sport = (Sport)BizObject.Cache[key];
      }
      else
      {
        sport = GetSportFromSportDetails(SiteProvider.Sheets.GetSport(sportCode));
        BaseSheet.CacheData(key, sport);
      }
      return sport;
    }

    /// <summary>
    /// Returns a collection with all the sports
    /// </summary>
    public static List<Sport> GetSports()
    {
      List<Sport> sports = null;
      string key = "Sheets_Sports";

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        sports = (List<Sport>)BizObject.Cache[key];
      }
      else
      {
        List<SportDetails> recordset = SiteProvider.Sheets.GetSports();
        sports = GetSportListFromSportDetailsList(recordset);
        BaseSheet.CacheData(key, sports);
      }
      return sports.GetRange(0, sports.Count);
    }





    /// <summary>
    /// Converts a SportDetails entity object to a Sport business object
    /// </summary>
    private static Sport GetSportFromSportDetails(SportDetails sport)
    {
      if (sport == null)
        return null;
      else
      {
        return new Sport(sport.SportCode, sport.SportName, sport.LeagueName, sport.LeagueAbbreviation);
      }
    }

    /// <summary>
    /// Converts a collection of SportDetails objects to a collection of Sport business objects
    /// </summary>
    private static List<Sport> GetSportListFromSportDetailsList(List<SportDetails> recordset)
    {
      List<Sport> sports = new List<Sport>();
      foreach (SportDetails record in recordset)
        sports.Add(GetSportFromSportDetails(record));
      return sports;
    }


  }
}