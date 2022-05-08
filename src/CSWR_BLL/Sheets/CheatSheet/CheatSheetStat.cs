using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Defines the stats that are relevant for a particular spreadsheet.  Cheatsheets support different
/// stats based on the positions included in the cheat sheet.
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class CheatSheetStat : BaseSheet
  {
    public CheatSheetStat(int cheatSheetID, string statCode)
    {
      this.CheatSheetID = cheatSheetID;
      this.StatCode = statCode;
    }

    /// <summary>
    /// The unique ID of the cheat sheet 
    /// </summary>
    public  int CheatSheetID  {get;set;}

    /// <summary>
    /// The stat code associated with the cheat sheet
    /// </summary>
    public string StatCode {get;set;}


    /// <summary>
    /// Returns a collection of CheatSheetStats which for a particular cheatsheetid
    /// </summary>
    public static List<CheatSheetStat> GetCheatSheetStats(int cheatSheetID, string sportCode, string positionCode)
    {
      List<CheatSheetStat> cheatSheetStats = null;
      string key = "Sheets_CheatSheetStat_ID_" + cheatSheetID.ToString() + "_SportCode" + sportCode + "_PositionCode" + positionCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        cheatSheetStats = (List<CheatSheetStat>)BizObject.Cache[key];
      }
      else
      {
        List<CheatSheetStatDetails> recordset = SiteProvider.Sheets.GetCheatSheetStats(cheatSheetID, sportCode, positionCode);
        cheatSheetStats = GetCheatSheetStatListFromCheatSheetStatDetailsList(recordset);
        BaseSheet.CacheData(key, cheatSheetStats);
      }
      return cheatSheetStats.GetRange(0, cheatSheetStats.Count);
    }

    /// <summary>
    /// Inserts a collection of CheatSheetStats into the database
    /// </summary>
    /// <param name="cheatSheetStats"></param>
    /// <returns></returns>
    public static int InsertCheatSheetStats(List<CheatSheetStat> cheatSheetStats)
    {
      int ret = 0;
      for (int i = 0; i < cheatSheetStats.Count; i++)
      {
        CheatSheetStatDetails record = new CheatSheetStatDetails(cheatSheetStats[i].CheatSheetID, cheatSheetStats[i].StatCode);
        ret = SiteProvider.Sheets.InsertCheatSheetStat(record);
      }
      return ret;
    }

    /// <summary>
    /// Inserts a single CheatSheetStat record into the database
    /// </summary>
    /// <param name="cheatSheetStat"></param>
    /// <returns></returns>
    public static int InsertCheatSheetStat(CheatSheetStat cheatSheetStat)
    {
      int ret = 0;
      CheatSheetStatDetails record = new CheatSheetStatDetails(cheatSheetStat.CheatSheetID, cheatSheetStat.StatCode);
      ret = SiteProvider.Sheets.InsertCheatSheetStat(record);

      BizObject.PurgeCacheItems("Sheets_CheatSheetStat_ID_" + cheatSheetStat.CheatSheetID.ToString());
      return ret;
    }



    /// <summary>
    /// Converts a CheatSheetStatDetails entity object to a CheatSheetStat business object
    /// </summary>
    private static CheatSheetStat GetCheatSheetStatFromCheatSheetStatDetails(CheatSheetStatDetails cheatSheetStat)
    {
      if (cheatSheetStat == null)
        return null;
      else
      {
        return new CheatSheetStat(cheatSheetStat.CheatSheetID, cheatSheetStat.StatCode);
      }
    }

    /// <summary>
    /// Converta a collection of CheatSheetStatDetails objects to a collection of CheatSheetStat business objects
    /// </summary>
    private static List<CheatSheetStat> GetCheatSheetStatListFromCheatSheetStatDetailsList(List<CheatSheetStatDetails> recordset)
    {
      List<CheatSheetStat> cheatSheetStats = new List<CheatSheetStat>();
      foreach (CheatSheetStatDetails record in recordset)
        cheatSheetStats.Add(GetCheatSheetStatFromCheatSheetStatDetails(record));
      return cheatSheetStats;
    }




  }
}