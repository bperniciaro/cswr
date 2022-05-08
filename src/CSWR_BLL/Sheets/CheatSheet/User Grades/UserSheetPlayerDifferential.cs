using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class UserSheetPlayerDifferential : BaseSheet
  {
    public UserSheetPlayerDifferential(int userSheetPlayerDifferentialID, string seasonCode, string sportCode, string positionCode, 
                                           int playerID, decimal averageDifferential)
    {
      this.UserSheetPlayerDifferentialID = userSheetPlayerDifferentialID;
      this.SeasonCode = seasonCode;
      this.SportCode = sportCode;
      this.PositionCode = positionCode.Trim();
      this.PlayerID = playerID;
      this.AverageDifferential = averageDifferential;
    }

    public UserSheetPlayerDifferential()
    {
    }

    public int UserSheetPlayerDifferentialID {get;set;}
    public string SeasonCode { get; set; }
    public string SportCode { get; set; }
    public string PositionCode { get; set; }
    public int PlayerID { get; set; }
    public decimal AverageDifferential { get; set; }



    /// <summary>
    /// Returns all stats for a player for a particular sport/season pair
    /// </summary>
    public static List<UserSheetPlayerDifferential> GetUserSheetPlayerDifferentials(string sportCode, string seasonCode)
    {
      List<UserSheetPlayerDifferential> userSheetPlayerDifferentials = null;
      string key = "Sheets_UserSheetPlayerDifferentialsBySportSeason_" + sportCode + "_" + seasonCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        userSheetPlayerDifferentials = (List<UserSheetPlayerDifferential>)BizObject.Cache[key];
      }
      else
      {
        List<UserSheetPlayerDifferentialDetails> recordset = SiteProvider.Sheets.GetUserSheetPlayerDifferentials(sportCode, seasonCode);
        userSheetPlayerDifferentials = GetUserSheetPlayerDifferentialListFromUserSheetPlayerDifferentialDetailsList(recordset);
        BaseSheet.CacheData(key, userSheetPlayerDifferentials);
      }
      return userSheetPlayerDifferentials.GetRange(0, userSheetPlayerDifferentials.Count);
    }




    /// <summary>
    /// Inserts a user sheet player differential
    /// </summary>
    /// <param name="CheatSheetItem"></param>
    /// <returns></returns>
    public static int InsertUserSheetPlayerDifferential(string seasonCode, string sportCode, string positionCode, 
                                                      int playerID, decimal averageDifferential)
    {

      UserSheetPlayerDifferentialDetails record = new UserSheetPlayerDifferentialDetails(0, seasonCode, sportCode, positionCode,
        playerID, averageDifferential);

      return SiteProvider.Sheets.InsertUserSheetPlayerDifferential(record);
    }



    /// <summary>
    /// Converts a entity into a business-level domain object.
    /// </summary>
    /// <param name="cheatSheet">A cheat sheet entity object.</param>
    /// <returns>A cheat sheet domain object.</returns>
    private static UserSheetPlayerDifferential GetUserSheetPlayerDifferentialFromUserSheetPlayerDifferentialDetails(UserSheetPlayerDifferentialDetails userSheetPlayerDifferential)
    {
      if (userSheetPlayerDifferential == null)
        return null;
      else
      {
        return new UserSheetPlayerDifferential(userSheetPlayerDifferential.UserSheetPlayerDifferentialID, userSheetPlayerDifferential.SeasonCode,
          userSheetPlayerDifferential.SportCode, userSheetPlayerDifferential.PositionCode, userSheetPlayerDifferential.PlayerID,
          userSheetPlayerDifferential.AverageDifferential);
      }
    }


    /// <summary>
    /// Converts a collection of sheet entities into a collection of business-level domain objects.
    /// </summary>
    /// <param name="cheatSheet">A collection of cheat sheet entity objects.</param>
    /// <returns>A collection cheat sheet domain objects.</returns>
    private static List<UserSheetPlayerDifferential> GetUserSheetPlayerDifferentialListFromUserSheetPlayerDifferentialDetailsList(List<UserSheetPlayerDifferentialDetails> recordset)
    {
      List<UserSheetPlayerDifferential> UserSheetPositionGrade = new List<UserSheetPlayerDifferential>();
      foreach (UserSheetPlayerDifferentialDetails record in recordset)
        UserSheetPositionGrade.Add(GetUserSheetPlayerDifferentialFromUserSheetPlayerDifferentialDetails(record));
      return UserSheetPositionGrade;
    }

  }
}