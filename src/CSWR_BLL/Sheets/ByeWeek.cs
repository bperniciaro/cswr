using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// This class represents a bye week
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class ByeWeek : BaseSheet
  {

    /// <summary>
    /// The default constructor for the ByeWeek class
    /// </summary>
    public ByeWeek()
    {}

    /// <summary>
    /// Constructor used to initialize an object
    /// </summary>
    /// <param name="sportCode">The sport on which the bye week is based.</param>
    /// <param name="seasonCode">The season on which the bye week is based.</param>
    /// <param name="teamCode">The team on which the bye week is based.</param>
    /// <param name="byeWeek">The week on which the team won't play.</param>
    public ByeWeek(string sportCode, string seasonCode, string teamCode, int byeWeek)
    {
      this.SportCode = sportCode;
      this.SeasonCode = seasonCode;
      this.TeamCode = teamCode;
      this.Bye = byeWeek;
    }

    /// <summary>
    /// The sport on which the bye week is based
    /// </summary>
    public string SportCode {get;set;}

    /// <summary>
    /// The season in which the bye week is relevant
    /// </summary>
    public string SeasonCode {get;set;}

    /// <summary>
    /// The team for which the bye week is relevant
    /// </summary>
    public string TeamCode {get;set;}

    /// <summary>
    /// The week on which the team doesn't play
    /// </summary>
    public int Bye {get;set;}
    

    /// <summary>
    /// Gets the bye week for a team based on the season, sport, and team
    /// </summary>
    public static ByeWeek GetByeWeek(string seasonCode, string sportCode, string teamCode)
    {
      ByeWeek byeWeek = null;
      string key = "Sheets_ByeWeek_" + seasonCode.ToString() + "_" + sportCode.ToString() + "_" + teamCode.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        byeWeek = (ByeWeek)BizObject.Cache[key];
      }
      else
      {
        byeWeek = GetByeWeekFromByeWeekDetails(SiteProvider.Sheets.GetByeWeek(seasonCode, sportCode, teamCode));
        BaseSheet.CacheData(key, byeWeek);
      }
      return byeWeek;
    }



    /// <summary>
    /// Returns all bye weeks for a particular sport and season
    /// </summary>
    public static List<ByeWeek> GetByeWeeks(string seasonCode, string sportCode)
    {
      List<ByeWeek> byeWeeks = null;
      string key = "Sheets_ByeWeeks_" + seasonCode.ToString() + "_" + sportCode.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        byeWeeks = (List<ByeWeek>)BizObject.Cache[key];
      }
      else
      {
        List<ByeWeekDetails> recordset = SiteProvider.Sheets.GetByeWeeks(seasonCode, sportCode);
        byeWeeks = GetByeWeekListFromByeWeekDetailsList(recordset);
        BaseSheet.CacheData(key, byeWeeks);
      }
      return byeWeeks.GetRange(0, byeWeeks.Count);
    }




    /// <summary>
    /// Converts a ByeWeekDetails entity object to a ByeWeek business object
    /// </summary>
    private static ByeWeek GetByeWeekFromByeWeekDetails(ByeWeekDetails byeWeek)
    {
      if (byeWeek == null)
        return null;
      else
      {
        return new ByeWeek(byeWeek.SeasonCode, byeWeek.SportCode, byeWeek.TeamCode, byeWeek.ByeWeek);
      }
    }

    /// <summary>
    /// Converta a collection of ByeWeekDetails objects to a collection of ByeWeek business objects
    /// </summary>
    private static List<ByeWeek> GetByeWeekListFromByeWeekDetailsList(List<ByeWeekDetails> recordset)
    {
      List<ByeWeek> byeWeeks = new List<ByeWeek>();
      foreach (ByeWeekDetails record in recordset)
        byeWeeks.Add(GetByeWeekFromByeWeekDetails(record));
      return byeWeeks.GetRange(0, byeWeeks.Count);
    }
  
  
  }

}