using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Contains the CheatSheet/Position pairs which define the positions included in a spreadsheet
/// </summary>
/// 

namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  [Serializable]
  public class CheatSheetPosition : BaseSheet
  {
    public CheatSheetPosition(int cheatSheetID, string positionCode)
    {
      this.CheatSheetID = cheatSheetID;
      this.PositionCode = positionCode;
    }

    
    /// <summary>
    /// The unique ID of the cheat sheet
    /// </summary>
    public int CheatSheetID {get;set;}

    /// <summary>
    /// The positions contained within a particular cheat sheet
    /// </summary>
    public string PositionCode {get;set;}


    /// <summary>
    /// Returns a collection with all positions associated with a particular cheat sheet
    /// </summary>
    public static List<CheatSheetPosition> GetCheatSheetPositions(int cheatSheetID)
    {
      List<CheatSheetPosition> cheatSheetPositions = null;
      string key = "Sheets_CheatSheetPosition_" + cheatSheetID.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        cheatSheetPositions = (List<CheatSheetPosition>)BizObject.Cache[key];
      }
      else
      {
        List<CheatSheetPositionDetails> recordset = SiteProvider.Sheets.GetCheatSheetPositions(cheatSheetID);
        cheatSheetPositions = GetCheatSheetPositionListFromCheatSheetPositionDetailsList(recordset);
        BaseSheet.CacheData(key, cheatSheetPositions);
      }
      return cheatSheetPositions.GetRange(0, cheatSheetPositions.Count);
    }

    /// <summary>
    /// Inserts a collection of Positions into a cheat sheet
    /// </summary>
    /// <param name="cheatSheetPositions"></param>
    /// <returns></returns>
    public static int InsertCheatSheetPositions(List<CheatSheetPosition> cheatSheetPositions)
    {
      int ret = 0;
      for (int i = 0; i < cheatSheetPositions.Count; i++)
      {
        CheatSheetPositionDetails record = new CheatSheetPositionDetails(cheatSheetPositions[i].CheatSheetID, cheatSheetPositions[i].PositionCode);
        SiteProvider.Sheets.InsertCheatSheetPosition(record);
      }
      return ret;
    }




    /// <summary>
    /// Converts a CheatSheetPositionDetails entity object to a CheatSheetPosition business object
    /// </summary>
    public static CheatSheetPosition GetCheatSheetPositionFromCheatSheetPositionDetails(CheatSheetPositionDetails cheatSheetPosition)
    {
      if (cheatSheetPosition == null)
        return null;
      else
      {
        return new CheatSheetPosition(cheatSheetPosition.CheatSheetID, cheatSheetPosition.PositionCode);
      }
    }

    /// <summary>
    /// Converta a collection of CheatSheetPositionDetails objects to a collection of CheatSheetPosition business objects
    /// </summary>
    private static List<CheatSheetPosition> GetCheatSheetPositionListFromCheatSheetPositionDetailsList(List<CheatSheetPositionDetails> recordset)
    {
      List<CheatSheetPosition> cheatSheetPositions = new List<CheatSheetPosition>();
      foreach (CheatSheetPositionDetails record in recordset)
        cheatSheetPositions.Add(GetCheatSheetPositionFromCheatSheetPositionDetails(record));
      return cheatSheetPositions;
    }


  }
}