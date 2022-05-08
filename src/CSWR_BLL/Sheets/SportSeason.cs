using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class SportSeason : BaseSheet
  {

    public SportSeason(string sportCode, string seasonCode, bool currentSeason, bool seasonStarted, bool seasonEnded, bool someStatsLoaded)
    {
      this.SportCode = sportCode;
      this.SeasonCode = seasonCode;
      this.IsCurrentSeason = currentSeason;
      this.SeasonStarted = seasonStarted;
      this.SeasonEnded = seasonEnded;
      this.SomeStatsLoaded = someStatsLoaded;
    }

    public SportSeason() { }

    /// <summary>
    /// The sport associated with the particular season
    /// </summary>
    public string SportCode  {get;set;}

    /// <summary>
    /// The season in question
    /// </summary>
    public string SeasonCode {get;set;}

    /// <summary>
    /// Indicates if this SportSeason is the current season
    /// </summary>
    public bool IsCurrentSeason {get;set;}

    /// <summary>
    /// Indicates if the season has started
    /// </summary>
    public bool SeasonStarted {get;set;}

    /// <summary>
    /// Indicates if the season has ended
    /// </summary>
    public bool SeasonEnded { get; set; }

    /// <summary>
    /// Indicates if stats have been loaded for the respective season.  We set this flag manually after stats from
    /// the first week of the NFL regular season have been loaded
    /// </summary>
    public bool SomeStatsLoaded { get; set; }


    /// <summary>
    /// A calculated property allowing the user to get quick acess to the last season
    /// </summary>
    private string _lastSeasonCode = String.Empty;
    public string LastSeasonCode
    {
      get {
        int currentSeason = int.Parse(this.SeasonCode);
        int lastSeason = currentSeason - 1;
        return lastSeason.ToString();
      }
    }

    /// <summary>
    /// A calculated property allowing the user to get quick acess to the next season
    /// </summary>
    private string _nextSeasonCode = String.Empty;
    public string NextSeasonCode
    {
      get
      {
        int currentSeason = int.Parse(this.SeasonCode);
        int nextSeason = currentSeason + 1;
        return nextSeason.ToString();
      }
    }


    /// <summary>
    /// Returns a SportSeason representing the current year for the specified sport.
    /// </summary>
    public static SportSeason GetCurrentSportSeason(string sportCode)
    {
      SportSeason sportSeason = null;
      string key = "Sheets_SportSeason_" + sportCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        sportSeason = (SportSeason)BizObject.Cache[key];
      }
      else
      {
        sportSeason = GetSportSeasonFromSportSeasonDetails(SiteProvider.Sheets.GetCurrentSportSeason(sportCode));
        BaseSheet.CacheData(key, sportSeason);
      }
      return sportSeason;
    }


    /// <summary>
    /// Returns a SportSeason representing the most recent year for which stats are available.
    /// </summary>
    public static SportSeason GetCurrentSportStatSeason(string sportCode)
    {
      SportSeason sportSeason = null;
      string key = "Sheets_SportStatSeason_" + sportCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        sportSeason = (SportSeason)BizObject.Cache[key];
      }
      else
      {
        sportSeason = GetSportSeasonFromSportSeasonDetails(SiteProvider.Sheets.GetCurrentSportStatSeason(sportCode));
        BaseSheet.CacheData(key, sportSeason);
      }
      return sportSeason;
    }


    /// <summary>
    /// Returns all seasons associated with a particular sport
    /// </summary>
    public static List<SportSeason> GetSportSeasons(string sportCode)
    {
      List<SportSeason> sportSeasons = null;
      string key = "Sheets_SportSeasons_" + sportCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        sportSeasons = (List<SportSeason>)BizObject.Cache[key];
      }
      else
      {
        List<SportSeasonDetails> recordset = SiteProvider.Sheets.GetSportSeasons(sportCode);
        sportSeasons = GetSportSeasonListFromSportSeasonDetailsList(recordset);
        BaseSheet.CacheData(key, sportSeasons);
      }
      return sportSeasons.GetRange(0, sportSeasons.Count);
    }


    /// <summary>
    /// Returns all seasons associated with a particular sport in which stats have been recorded.
    /// </summary>
    public static List<SportSeason> GetSportStatSeasons(string sportCode)
    {
      List<SportSeason> sportSeasons = null;
      string key = "Sheets_SportStatSeasons_" + sportCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        sportSeasons = (List<SportSeason>)BizObject.Cache[key];
      }
      else
      {
        List<SportSeasonDetails> recordset = SiteProvider.Sheets.GetSportStatSeasons(sportCode);
        sportSeasons = GetSportSeasonListFromSportSeasonDetailsList(recordset);
        BaseSheet.CacheData(key, sportSeasons);
      }
      return sportSeasons.GetRange(0, sportSeasons.Count);
    }



    /// <summary>
    /// Converts a SportSeasonDetails entity object to a SportSeason business object
    /// </summary>
    private static SportSeason GetSportSeasonFromSportSeasonDetails(SportSeasonDetails sportSeason)
    {
      if (sportSeason == null)
        return null;
      else
      {
        return new SportSeason(sportSeason.SportCode, sportSeason.SeasonCode, sportSeason.CurrentSeason, sportSeason.SeasonStarted, sportSeason.SeasonEnded, sportSeason.SomeStatsLoaded); 
      }
    }

    /// <summary>
    /// Converts a collection of SportSeasonDetails objects to a collection of SportSeason business objects
    /// </summary>
    private static List<SportSeason> GetSportSeasonListFromSportSeasonDetailsList(List<SportSeasonDetails> recordset)
    {
      List<SportSeason> sportSeasons = new List<SportSeason>();
      foreach (SportSeasonDetails record in recordset)
        sportSeasons.Add(GetSportSeasonFromSportSeasonDetails(record));
      return sportSeasons;
    }


  }
}
