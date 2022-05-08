using System;
using System.Collections.Generic;
using System.Linq;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// A collection of values which together represent one statistic
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class Stat : BaseSheet
  {
    public Stat(string statCode, string name, string abbreviation,string description)
    {
      this.StatCode = statCode;
      this.Name = name;
      this.Abbreviation = abbreviation;
      this.Description = description;
    }

    // Stat Code
    private string _statCode = String.Empty;
    public string StatCode 
    {
      get
      {
        return _statCode.Trim();
      }
      set
      {
        _statCode = value;
      }
    }

    // Name
    public string Name {get;set;}

    // Abbreviation
    private string _abbreviation = "";
    public string Abbreviation
    {
      get { return _abbreviation.Trim(); }
      set { _abbreviation = value; }
    }

    // Description
    private string _description = "";
    public string Description
    {
      get { return _description; }
      set { _description = value; }
    }

    /// <summary>
    /// Returns a Position object with the specified code
    /// </summary>
    public static Stat GetStat(string statCode)
    {
      Stat stat = null;
      string key = "Sheets_Stat_" + statCode;

      if (BaseSheet.Settings.EnableCaching && (BizObject.Cache[key] != null))
      {
        stat = (Stat)BizObject.Cache[key];
      }
      else
      {
        stat = GetStatFromStatDetails(SiteProvider.Sheets.GetStat(statCode));
        BaseSheet.CacheData(key, stat);
      }
      return stat;
    }


    /// <summary>
    /// Returns a collection with all the stats
    /// </summary>
    public static List<Stat> GetStats(string sportCode, string positionCode, bool pprLeague = false)
    {
      List<Stat> stats = null;
      string key = "Sheets_StatsBySportCodePositionCode_" + sportCode.ToString() + "_" + positionCode.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        stats = (List<Stat>)BizObject.Cache[key];
      }
      else
      {
        List<StatDetails> recordset = SiteProvider.Sheets.GetStats(sportCode, positionCode);
        stats = GetStatListFromStatDetailsList(recordset);

        // only provide relevant stats for PPR vs Standard Scoring
        if (pprLeague)
        {
          stats.Remove(stats.SingleOrDefault(x => x.StatCode == "FPPG"));
          stats.Remove(stats.SingleOrDefault(x => x.StatCode == "TFP"));
        }
        else
        {
          stats.Remove(stats.SingleOrDefault(x => x.StatCode == "FPGP"));
          stats.Remove(stats.SingleOrDefault(x => x.StatCode == "TFPP"));
        }
        
        BaseSheet.CacheData(key, stats);
      }
      return stats.GetRange(0, stats.Count);
    }


    /// <summary>
    /// Returns a Stat object filled with the data taken from the input StatDetails
    /// </summary>
    private static Stat GetStatFromStatDetails(StatDetails stat)
    {
      if (stat == null)
        return null;
      else
      {
        return new Stat(stat.StatCode, stat.Name, stat.Abbreviation, stat.Description);
      }
    }

    /// <summary>
    /// Returns a list of Stat objects filled with the data taken from the input list of StatDetails
    /// </summary>
    private static List<Stat> GetStatListFromStatDetailsList(List<StatDetails> recordset)
    {
      List<Stat> stats = new List<Stat>();
      foreach (StatDetails record in recordset)
        stats.Add(GetStatFromStatDetails(record));
      return stats;
    }

  }
}