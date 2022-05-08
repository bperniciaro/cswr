using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Represents a Season for a particular sport.
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets  {
  public class Season : BaseSheet
  {
    public Season(string seasonCode, string name)
    {
      this.SeasonCode = seasonCode;
      this.Name = name;
    }

    /// <summary>
    /// The code which represents a particular season
    /// </summary>
    public string SeasonCode {get;set;}

    /// <summary>
    /// The name given to the particular season
    /// </summary>
    public string Name {get;set;}

    /// <summary>
    /// A flag which indicates if the season is the current season
    /// </summary>
    public bool CurrentYearFlag {get;set;}

    public static string GetNextSeason(string currentSeasonString)
    {
      int currentSeasonInt = 0;
      string nextSeasonString = String.Empty;
      if(int.TryParse(currentSeasonString, out currentSeasonInt))  
      {
        return (++currentSeasonInt).ToString();
      }
      return null;
    }

    /// <summary>
    /// Returns all seasons
    /// </summary>
    public static List<Season> GetSeasons()
    {
      List<Season> seasons = null;
      string key = "Sheets_Seasons";

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        seasons = (List<Season>)BizObject.Cache[key];
      }
      else
      {
        List<SeasonDetails> recordset = SiteProvider.Sheets.GetSeasons();
        seasons = GetSeasonListFromSeasonDetailsList(recordset);
        BaseSheet.CacheData(key, seasons);
      }
      return seasons.GetRange(0, seasons.Count);
    }


    /// <summary>
    /// Returns the current season
    /// </summary>
    //public static Season GetCurrentSeason()
    //{
    //  Season currentSeason = null;
    //  string key = "Sheets_CurrentSeason";

    //  if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
    //  {
    //    currentSeason = (Season)BizObject.Cache[key];
    //  }
    //  else
    //  {
    //    currentSeason = GetSeasonFromSeasonDetails(SiteProvider.Sheets.GetCurrentSeason());
    //    BaseSheet.CacheData(key, currentSeason);
    //  }
    //  return currentSeason;
    //}



    /// <summary>
    /// Converts a SeasonDetails entity object to a Season business object
    /// </summary>
    private static Season GetSeasonFromSeasonDetails(SeasonDetails season)
    {
      if (season == null)
        return null;
      else
      {
        return new Season(season.SeasonCode, season.Name);
      }
    }

    /// <summary>
    /// Converts a collection of SeasonDetails objects to a collection of Season business objects
    /// </summary>
    private static List<Season> GetSeasonListFromSeasonDetailsList(List<SeasonDetails> recordset)
    {
      List<Season> seasons = new List<Season>();
      foreach (SeasonDetails record in recordset)
        seasons.Add(GetSeasonFromSeasonDetails(record));
      return seasons;
    }


  }

}
