using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class SportSeasonPlayerStatus : BaseSheet
  {
    public SportSeasonPlayerStatus(int sportSeasonPlayerStatusId, string sportCode, string seasonCode, int playerId,
      string statusCode, string suppInfo, int? count, bool approved, bool archived, string createdByUsername, 
      DateTime createdTimestamp, string modifiedByUsername, DateTime? modifiedTimestamp)
    {
      this.SportSeasonPlayerStatusId = sportSeasonPlayerStatusId;
      this.SportCode = sportCode;
      this.SeasonCode = seasonCode;
      this.PlayerId = playerId;
      this.StatusCode = statusCode;
      this.SuppInfo = suppInfo;
      this.Count = count;
      this.Approved = approved;
      this.Archived = archived;
      this.CreatedByUsername = createdByUsername;
      this.CreatedTimestamp = createdTimestamp;
      this.ModifiedByUsername = modifiedByUsername;
      this.ModifiedTimestamp = modifiedTimestamp;
    }

    public int SportSeasonPlayerStatusId { get; set; }
    public string SportCode { get; set; }
    public string SeasonCode { get; set; }
    public int PlayerId { get; set; }
    public string StatusCode { get; set; }
    public string SuppInfo { get; set; }
    public int? Count { get; set; }
    public bool Approved { get; set; }
    public bool Archived { get; set; }
    public string CreatedByUsername { get; set; }
    public DateTime CreatedTimestamp { get; set; }
    public string ModifiedByUsername { get; set; }
    public DateTime? ModifiedTimestamp { get; set; }



    public static List<SportSeasonPlayerStatus> GetSportSeasonPlayerStatuses(string sportCode, string seasonCode)
    {
      List<SportSeasonPlayerStatus> sportSeasonPlayerStatuses = null;
      string key = "Sheets_SportSeasonPlayerStatuses_" + sportCode + "_" + seasonCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        sportSeasonPlayerStatuses = (List<SportSeasonPlayerStatus>)BizObject.Cache[key];
      }
      else
      {
        List<SportSeasonPlayerStatusDetails> recordset = SiteProvider.Sheets.GetSportSeasonPlayerStatuses(seasonCode, sportCode);
        sportSeasonPlayerStatuses = GetSportSeasonPlayerStatusListFromSportSeasonPlayerStatusDetails(recordset);
        BaseSheet.CacheData(key, sportSeasonPlayerStatuses);
      }
      return sportSeasonPlayerStatuses.GetRange(0, sportSeasonPlayerStatuses.Count);
    }



    public static bool UpdateSportSeasonPlayerStatus(int sportSeasonPlayerStatusId, string sportCode, string seasonCode, int playerId,
      string statusCode, string suppInfo, int? count, bool approved, bool archived, string createdByUsername,
      DateTime createdTimestamp, string modifiedByUsername, DateTime? modifiedTimestamp)
    {

      // build an entity to update, then use the module specific provider to update it
      var playerStatus = new SportSeasonPlayerStatusDetails(sportSeasonPlayerStatusId, sportCode, seasonCode, playerId,
        statusCode, suppInfo, count, approved, archived, createdByUsername, createdTimestamp, modifiedByUsername,
        modifiedTimestamp);

      BizObject.PurgeCacheItems("Sheets_SportSeasonPlayerStatuses_" + sportCode + "_" + seasonCode);

      var ret = SiteProvider.Sheets.UpdateSportSeasonPlayerStatus(playerStatus);

      return ret;
    }

    public bool Update()
    {
      return SportSeasonPlayerStatus.UpdateSportSeasonPlayerStatus(this.SportSeasonPlayerStatusId, this.SportCode, 
        this.SeasonCode, this.PlayerId, this.StatusCode, this.SuppInfo, this.Count, this.Approved, this.Archived, 
        this.CreatedByUsername, this.CreatedTimestamp, this.ModifiedByUsername, this.ModifiedTimestamp);
    }


    public static int InsertSportSeasonPlayerStatus(string sportCode, string seasonCode, int playerId,
      string statusCode, string suppInfo, int? count, bool approved, bool archived, string createdByUsername, 
      DateTime createdTimestamp, string modifiedByUsername, DateTime? modifiedTimestamp)
    {
      var record = new SportSeasonPlayerStatusDetails(0, sportCode, seasonCode, playerId, statusCode, suppInfo, count, 
        approved, archived, createdByUsername, createdTimestamp, modifiedByUsername, modifiedTimestamp);
      int ret = SiteProvider.Sheets.InsertSportSeasonPlayerStatus(record);

      BizObject.PurgeCacheItems("Sheets_SportSeasonPlayerStatuses_" + sportCode + "_" + seasonCode);
      return ret;
    }

    public int Insert()
    {
      return SportSeasonPlayerStatus.InsertSportSeasonPlayerStatus(this.SportCode, this.SeasonCode, this.PlayerId,
        this.StatusCode, this.SuppInfo, this.Count, this.Approved, this.Archived, this.CreatedByUsername, 
        this.CreatedTimestamp, this.ModifiedByUsername, this.ModifiedTimestamp);
    }


    private static SportSeasonPlayerStatus GetSportSeasonPlayerStatusFromSportSeasonPlayerStatusDetails(SportSeasonPlayerStatusDetails playerStatus)
    {
      if (playerStatus == null)
        return null;
      else
      {
        return new SportSeasonPlayerStatus(playerStatus.SportSeasonPlayerStatusId, playerStatus.SportCode,
          playerStatus.SeasonCode, playerStatus.PlayerId, playerStatus.StatusCode, playerStatus.SuppInfo, 
          playerStatus.Count, playerStatus.Approved, playerStatus.Archived, playerStatus.CreatedByUsername, 
          playerStatus.CreatedTimestamp, playerStatus.ModifiedByUsername, playerStatus.ModifiedTimestamp);
      }
    }

    /// <summary>
    /// Returns a list of Player objects filled with the data taken from the input list of PlayerDetails
    /// </summary>
    private static List<SportSeasonPlayerStatus> GetSportSeasonPlayerStatusListFromSportSeasonPlayerStatusDetails(List<SportSeasonPlayerStatusDetails> recordset)
    {
      var playerStatuses = new List<SportSeasonPlayerStatus>();
      foreach (SportSeasonPlayerStatusDetails record in recordset)
      { 
        playerStatuses.Add(GetSportSeasonPlayerStatusFromSportSeasonPlayerStatusDetails(record));
      }
      return playerStatuses;
    }

  }

}
