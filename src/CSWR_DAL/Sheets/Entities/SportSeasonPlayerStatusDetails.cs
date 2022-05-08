using System;

namespace BP.CheatSheetWarRoom.DAL
{
  public class SportSeasonPlayerStatusDetails
  {
    public SportSeasonPlayerStatusDetails(int sportSeasonPlayerStatusId, string sportCode, string seasonCode, int playerId,
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

  }
}