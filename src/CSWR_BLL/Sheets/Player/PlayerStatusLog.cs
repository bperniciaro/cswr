using System;
using System.Collections.Generic;

/// <summary>
/// Summary description for Player
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  [Serializable()]
  public class PlayerStatusLog : BaseSheet
  {
    public PlayerStatusLog(int playerStatusID, int playerID, string seasonCode, string statusCode, string supplementalInfo,   
                           string supplementalInfoTitle, DateTime createdDatetime, string createdBy, int priority, bool archived)
    {
      this.PlayerStatusID = playerStatusID;
      this.PlayerID = playerID;
      this.SeasonCode = seasonCode;
      this.StatusCode = statusCode;
      this.SupplementalInfoTitle = supplementalInfoTitle;  // needs to be moved to PlayerStatus class when static statuses are added
      this.SupplementalInfo = supplementalInfo;
      this.CreatedDateTime =  createdDatetime;
      this.CreatedBy = createdBy;
      this.Priority = priority;
      this.Archived = archived;
    }

    public PlayerStatusLog() { }

    public int PlayerStatusID { get; set; }
    public int PlayerID { get; set; }
    public string SeasonCode { get; set; }
    public string StatusCode { get; set; }
    public string SupplementalInfo { get; set; }
    public string SupplementalInfoTitle { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public string CreatedBy { get; set; }
    public int Priority { get; set; }
    public bool Archived { get; set; }


    public static List<PlayerStatusLog> GetPlayerStatusLogs(int playerID, string seasonCode)
    {
      List<PlayerStatusLog> playerStati = new List<PlayerStatusLog>();

      List<PlayerStatusLog> dynamicStati = PlayerStatusLog.GetDynamicStati(playerID, seasonCode);
      playerStati.AddRange(dynamicStati);

      return playerStati;
    }


    private static List<PlayerStatusLog> GetDynamicStati(int playerID, string seasonCode)
    {
      List<PlayerStatusLog> dynamicStati = new List<PlayerStatusLog>();
      Player targetPlayer = Player.GetPlayer(playerID);

      // Retired Status
      if (targetPlayer.Retired)
      {
        string supplementalInfo = targetPlayer.FullName + " is currently retired.  Edit your sheet to remove this player";
        dynamicStati.Add(new PlayerStatusLog(0, playerID, seasonCode, PlayerStatusCodes.RETIRD.ToString(), supplementalInfo,
                          "Retired Player", DateTime.Now, CurrentUserName, 1, false));
      }

      // 3rd year WR
      if (targetPlayer.PositionCode == FOOPositionsOffense.WR.ToString())
      {
        if (targetPlayer.YearsExperience == 2)
        {
          string lastNameLastCharacter = targetPlayer.FullName.Substring(targetPlayer.FullName.Length - 1, 1);
          string playerNameWithApostrophe = (lastNameLastCharacter == "s") ? targetPlayer.FullName + "'" : targetPlayer.FullName + "'s";
          string supplementalInfo = "This is " + playerNameWithApostrophe + " third year in the NFL.";
          dynamicStati.Add(new PlayerStatusLog(0, playerID, seasonCode, PlayerStatusCodes.THREWR.ToString(), supplementalInfo,
                           "3rd Year Wide Receiver", DateTime.Now, CurrentUserName, 3, false));
        }
      }

      // Switched Teams
      int previousSeason = int.Parse(seasonCode) - 1;
      SportSeasonPlayerTeam playerPreviousTeam = SportSeasonPlayerTeam.GetSportSeasonPlayerTeam(targetPlayer.SportCode, previousSeason.ToString(), playerID);
      if (playerPreviousTeam != null)
      {
        if (playerPreviousTeam.TeamCode != targetPlayer.TeamCode)
        {
          // team mascots
          string oldMascot = Team.GetTeam(playerPreviousTeam.TeamCode).Mascot;
          string newMascot = Team.GetTeam(targetPlayer.TeamCode).Mascot;

          SportSeason currentSeason = SportSeason.GetCurrentSportSeason(targetPlayer.SportCode);
          string supplementalInfo = targetPlayer.FullName + " played for the " + oldMascot + " in " + currentSeason.LastSeasonCode + " but plays for the " + newMascot + " in " + currentSeason.SeasonCode + ".";
          dynamicStati.Add(new PlayerStatusLog(0, playerID, seasonCode, PlayerStatusCodes.SWTEAM.ToString(), supplementalInfo, "Player Switched Teams",
                            DateTime.Now, CurrentUserName, 2, false));
        }
      }



      return dynamicStati;
    }





  }




}