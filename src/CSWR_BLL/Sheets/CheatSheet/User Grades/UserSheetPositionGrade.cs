using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// This is the class for a cheat sheet, which descends from the base class BaseSheet
/// </summary>
/// 

namespace BP.CheatSheetWarRoom.BLL.Sheets
{

  [Serializable()]
  public class UserSheetPositionGrade : BaseSheet
  {

    public UserSheetPositionGrade() { }

    public UserSheetPositionGrade(int userSheetPositionGradeID, string seasonCode, string sportCode, string positionCode, 
                                    int archivedCheatSheetID, string userName, int totalRankDifferential, int score, int rank)
    {
      this.UserSheetPositionGradeID = userSheetPositionGradeID;
      this.SeasonCode = seasonCode;
      this.SportCode = sportCode;
      this.PositionCode = positionCode;
      this.ArchivedCheatSheetID = archivedCheatSheetID;
      this.UserName = userName;
      this.TotalRankDifferential = totalRankDifferential;
      this.Score = score;
      this.Rank = rank;
    }

    public int UserSheetPositionGradeID {get;set;}
    public string SeasonCode { get; set; }
    public string SportCode { get; set; }

    private string _positionCode = String.Empty;
    public string PositionCode
    {
      get
      {
        return _positionCode.Trim();
      }
      set
      {
        _positionCode = value;
      }
    }
    public int ArchivedCheatSheetID { get; set; }
    public string UserName { get; set; }
    public int TotalRankDifferential { get; set; }
    public int Score { get; set; }
    public int Rank { get; set; }


    /// <summary>
    /// Returns all stats for a player for a particular sport/season pair
    /// </summary>
    public static UserSheetPositionGrade GetUserSheetPositionGrade(string userName, string seasonCode, string positionCode)
    {
      UserSheetPositionGrade userSheetPositionGrade = null;
      string key = "Sheets_GetUserSheetPositionGradeByUsernameSeasonPositionCodes_" + userName + "_" + seasonCode + "_" + positionCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        userSheetPositionGrade = (UserSheetPositionGrade)BizObject.Cache[key];
      }
      else
      {
        UserSheetPositionGradeDetails recordset = SiteProvider.Sheets.GetUserSheetPositionGrade(userName, seasonCode, positionCode);
        userSheetPositionGrade = GetUserSheetPositionGradeFromUserSheetPositionGradeDetails(recordset);
        BaseSheet.CacheData(key, userSheetPositionGrade);
      }
      return userSheetPositionGrade;
    }


    /// <summary>
    /// Returns all stats for a player for a particular sport/season pair
    /// </summary>
    public static List<UserSheetPositionGrade> GetUserSheetPositionGrades(string sportCode, string seasonCode)
    {
      List<UserSheetPositionGrade> userSheetPositionGrade = null;
      string key = "Sheets_UserSheetPositionGradesBySportSeason_" + sportCode + "_" + seasonCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        userSheetPositionGrade = (List<UserSheetPositionGrade>)BizObject.Cache[key];
      }
      else
      {
        List<UserSheetPositionGradeDetails> recordset = SiteProvider.Sheets.GetUserSheetPositionGrades(sportCode, seasonCode);
        userSheetPositionGrade = GetUserSheetPositionGradeListFromUserSheetPositionGradeDetailsList(recordset);
        BaseSheet.CacheData(key, userSheetPositionGrade);
      }
      return userSheetPositionGrade.GetRange(0, userSheetPositionGrade.Count);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="seasonCode"></param>
    /// <param name="sportCode"></param>
    /// <param name="positionCode"></param>
    /// <param name="archivedCheatSheetId"></param>
    /// <param name="userName"></param>
    /// <param name="totalDifferential"></param>
    /// <param name="grade"></param>
    /// <param name="rank"></param>
    /// <returns></returns>
    public static int InsertUserSheetPositionGrade(string seasonCode, string sportCode, string positionCode, int archivedCheatSheetId,
                                                     string userName, int totalDifferential, int grade, int rank)
    {

      var record = new UserSheetPositionGradeDetails(0, seasonCode, sportCode, positionCode,
        archivedCheatSheetId, userName, totalDifferential, grade, rank);
      
      return SiteProvider.Sheets.InsertUserSheetPositionGrade(record);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="seasonCode"></param>
    /// <param name="sportCode"></param>
    /// <param name="positionCode"></param>
    /// <returns></returns>
    public static int GetUserSheetPositionGradeCount(string seasonCode, string sportCode, string positionCode)
    {
      return SiteProvider.Sheets.GetUserSheetPositionGradesCount(seasonCode, sportCode, positionCode);
    }



    /// <summary>
    /// Converts a entity into a business-level domain object.
    /// </summary>
    /// <param name="cheatSheet">A cheat sheet entity object.</param>
    /// <returns>A cheat sheet domain object.</returns>
    private static UserSheetPositionGrade GetUserSheetPositionGradeFromUserSheetPositionGradeDetails(UserSheetPositionGradeDetails sheetPositionalGrade)
    {
      if (sheetPositionalGrade == null)
        return null;
      else
      {
        return new UserSheetPositionGrade(sheetPositionalGrade.UserSheetPositionGradeID, sheetPositionalGrade.SeasonCode, sheetPositionalGrade.SportCode,
          sheetPositionalGrade.PositionCode, sheetPositionalGrade.ArchivedCheatSheetID, sheetPositionalGrade.UserName, sheetPositionalGrade.TotalRankDifferential,
          sheetPositionalGrade.Score, sheetPositionalGrade.Rank);
      }
    }


    /// <summary>
    /// Converts a collection of sheet entities into a collection of business-level domain objects.
    /// </summary>
    /// <param name="cheatSheet">A collection of cheat sheet entity objects.</param>
    /// <returns>A collection cheat sheet domain objects.</returns>
    private static List<UserSheetPositionGrade> GetUserSheetPositionGradeListFromUserSheetPositionGradeDetailsList(List<UserSheetPositionGradeDetails> recordset)
    {
      List<UserSheetPositionGrade> UserSheetPositionGrade = new List<UserSheetPositionGrade>();
      foreach (UserSheetPositionGradeDetails record in recordset)
        UserSheetPositionGrade.Add(GetUserSheetPositionGradeFromUserSheetPositionGradeDetails(record));
      return UserSheetPositionGrade;
    }

  }
}