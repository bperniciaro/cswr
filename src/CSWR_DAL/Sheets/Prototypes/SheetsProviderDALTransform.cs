using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Summary description for SheetsProvider
/// </summary>
/// 
namespace BP.CheatSheetWarRoom.DAL
{
  public abstract partial class SheetsProvider : DataAccess
  {

    // Setting Readers
    protected virtual SettingDetails GetSettingFromReader(IDataReader reader)
    {
      SettingDetails setting = new SettingDetails(
          reader["SettingCode"].ToString(),
          reader["SettingName"].ToString(),
          reader["SettingValue"].ToString());
      return setting;
    }

    protected virtual List<SettingDetails> GetSettingCollectionFromReader(IDataReader reader)
    {
      List<SettingDetails> settings = new List<SettingDetails>();
      while (reader.Read())
        settings.Add(GetSettingFromReader(reader));
      return settings;
    }



    // Sport Setting Readers
    protected virtual SportSettingDetails GetSportSettingFromReader(IDataReader reader)
    {
      SportSettingDetails sportSetting = new SportSettingDetails(
          (int)reader["SportSettingID"],
          reader["SportCode"].ToString(),
          reader["SettingCode"].ToString(),
          reader["SettingName"].ToString(),
          reader["SettingValue"].ToString());
      return sportSetting;
    }

    protected virtual List<SportSettingDetails> GetSportSettingCollectionFromReader(IDataReader reader)
    {
      List<SportSettingDetails> sportSettings = new List<SportSettingDetails>();
      while (reader.Read())
        sportSettings.Add(GetSportSettingFromReader(reader));
      return sportSettings;
    }



    // Team Readers

    /// <summary>
    /// Returns a list of TeamDetails filled with the datareader's current read data
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    protected virtual TeamDetails GetTeamFromReader(IDataReader reader)
    {

      int statMapID = 0;
      if (reader["StatMapID"] != DBNull.Value)
      {
        statMapID = (int)reader["StatMapID"];
      }

      TeamDetails team = new TeamDetails(
          reader["TeamCode"].ToString(),
          reader["SportCode"].ToString(),
          reader["City"].ToString(),
          reader["Mascot"].ToString(),
          reader["Abbreviation"].ToString(),
          statMapID);
      return team;
    }

    protected virtual List<TeamDetails> GetTeamCollectionFromReader(IDataReader reader)
    {
      List<TeamDetails> teams = new List<TeamDetails>();
      while (reader.Read())
        teams.Add(GetTeamFromReader(reader));
      return teams;
    }

    // Sport Readers

    protected virtual SportDetails GetSportFromReader(IDataReader reader)
    {
      SportDetails sport = new SportDetails(
          reader["SportCode"].ToString(),
          reader["SportName"].ToString(),
          reader["LeagueName"].ToString(),
          reader["LeagueAbbreviation"].ToString());
      return sport;
    }

    protected virtual List<SportDetails> GetSportCollectionFromReader(IDataReader reader)
    {
      List<SportDetails> sports = new List<SportDetails>();
      while (reader.Read())
        sports.Add(GetSportFromReader(reader));
      return sports;
    }


    // Sport Season Readers

    protected virtual SportSeasonDetails GetSportSeasonFromReader(IDataReader reader)
    {
      SportSeasonDetails sportSeason = new SportSeasonDetails(
          reader["SportCode"].ToString(),
          reader["SeasonCode"].ToString(),
          (bool)reader["CurrentSeason"],
          (bool)reader["SeasonStarted"],
          (bool)reader["SeasonEnded"],
          (bool)reader["SomeStatsLoaded"]);
      return sportSeason;
    }

    protected virtual List<SportSeasonDetails> GetSportSeasonCollectionFromReader(IDataReader reader)
    {
      List<SportSeasonDetails> sportSeasons = new List<SportSeasonDetails>();
      while (reader.Read())
        sportSeasons.Add(GetSportSeasonFromReader(reader));
      return sportSeasons;
    }


    // Position Readers

    protected virtual PositionDetails GetPositionFromReader(IDataReader reader)
    {
      PositionDetails position = new PositionDetails(
          reader["PositionCode"].ToString(),
          reader["Name"].ToString(),
          reader["Abbreviation"].ToString());
      return position;
    }

    protected virtual List<PositionDetails> GetPositionCollectionFromReader(IDataReader reader)
    {
      List<PositionDetails> positions = new List<PositionDetails>();
      while (reader.Read())
        positions.Add(GetPositionFromReader(reader));
      return positions;
    }


    // Player Readers

    protected virtual PlayerDetails GetPlayerFromReader(IDataReader reader)
    {
      int statMapID = (reader["StatMapID"] == DBNull.Value) ? 0 : (int)reader["StatMapID"];
      DateTime birthDate = (reader["BirthDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)reader["BirthDate"];

      PlayerDetails player = new PlayerDetails(
          (int)reader["PlayerID"],
          reader["SportCode"].ToString(),
          reader["PositionCode"].ToString(),
          reader["FirstName"].ToString(),
          reader["LastName"].ToString(),
          reader["MiddleName"].ToString(),
          reader["TeamCode"].ToString(),
          (short)reader["Number"],
          (DateTime)reader["FirstYear"],
          birthDate,
          reader["TwitterUsername"].ToString(),
          statMapID,
          (bool)reader["Retired"]);
      return player;
    }

    protected virtual List<PlayerDetails> GetPlayerCollectionFromReader(IDataReader reader)
    {
      List<PlayerDetails> players = new List<PlayerDetails>();
      while (reader.Read())
        players.Add(GetPlayerFromReader(reader));
      return players;
    }



    protected virtual PlayerStatusCodeDetails GetPlayerStatusCodeFromReader(IDataReader reader)
    {
      var countExample = (reader["CountExample"] == DBNull.Value) ? 0 : int.Parse(reader["CountExample"].ToString());

      var playerStatusCode = new PlayerStatusCodeDetails(
          reader["StatusCode"].ToString(),
          reader["Name"].ToString(),
          reader["Description"].ToString(),
          (bool)reader["SuppInfoRequired"],
          reader["SuppInfoLabel"].ToString(),
          reader["SuppInfoExample"].ToString(),
          reader["SuppInfoInstructions"].ToString(),
          (bool)reader["CountRequired"],
          reader["CountLabel"].ToString(),
          countExample,
          reader["CountInstructions"].ToString(),
          (int)(reader["Seqno"]),
          (int)(reader["Priority"]),
          (bool)reader["Dynamic"]);
      return playerStatusCode;
    }

    protected virtual List<PlayerStatusCodeDetails> GetPlayerStatusCodeCollectionFromReader(IDataReader reader)
    {
      var playerStatusCodes = new List<PlayerStatusCodeDetails>();
      while (reader.Read())
        playerStatusCodes.Add(GetPlayerStatusCodeFromReader(reader));
      return playerStatusCodes;
    }



    protected virtual SportSeasonPlayerStatusDetails GetSportSeasonPlayerStatusFromReader(IDataReader reader)
    {
      int? castCount = (reader["Count"]==DBNull.Value) ? 0 : (int?)reader["Count"];

      var sportSeasonPlayerStatus = new SportSeasonPlayerStatusDetails(
          (int)reader["PlayerStatusId"],
          reader["SportCode"].ToString(),
          reader["SeasonCode"].ToString(),
          (int)(reader["PlayerId"]),
          reader["StatusCode"].ToString(),
          reader["SuppInfo"].ToString(),
          null,
          (bool)reader["Approved"],
          (bool)reader["Archived"],
          reader["CreatedByUsername"].ToString(),
          (DateTime)reader["CreatedTimestamp"],
          reader["ModifiedByUsername"].ToString(),
          null);
      return sportSeasonPlayerStatus;
    }

    protected virtual List<SportSeasonPlayerStatusDetails> GetSportSeasonPlayerStatusCollectionFromReader(IDataReader reader)
    {
      var playerStatusCodes = new List<SportSeasonPlayerStatusDetails>();
      while (reader.Read())
        playerStatusCodes.Add(GetSportSeasonPlayerStatusFromReader(reader));
      return playerStatusCodes;
    }




    // Supplemental Source Readers

    protected virtual SupplementalSourceDetails GetSupplementalSourceFromReader(IDataReader reader)
    {
      SupplementalSourceDetails supplementalSource = new SupplementalSourceDetails(
          (int)reader["SupplementalSourceID"],
          reader["Abbreviation"].ToString(),
          reader["Name"].ToString(),
          reader["URL"].ToString(),
          reader["ImageURL"].ToString());
      return supplementalSource;
    }

    protected virtual List<SupplementalSourceDetails> GetSupplementalSourceCollectionFromReader(IDataReader reader)
    {
      List<SupplementalSourceDetails> supplementalSources = new List<SupplementalSourceDetails>();
      while (reader.Read())
        supplementalSources.Add(GetSupplementalSourceFromReader(reader));
      return supplementalSources;
    }

    // Supplemental Sheet Readers

    protected virtual SupplementalSheetDetails GetSupplementalSheetFromReader(IDataReader reader)
    {
      SupplementalSheetDetails supplementalSheet = new SupplementalSheetDetails(
          (int)reader["SupplementalSheetID"],
          reader["SeasonCode"].ToString(),
          (int)reader["SupplementalSourceID"],
          reader["SportCode"].ToString(),
          reader["PositionCode"].ToString(),
          (DateTime)reader["LastUpdated"],
          reader["URL"].ToString());
      return supplementalSheet;
    }

    protected virtual List<SupplementalSheetDetails> GetSupplementalSheetCollectionFromReader(IDataReader reader)
    {
      List<SupplementalSheetDetails> supplementalSheets = new List<SupplementalSheetDetails>();
      while (reader.Read())
        supplementalSheets.Add(GetSupplementalSheetFromReader(reader));
      return supplementalSheets;
    }


    // Supplemental Sheet Player Readers

    protected virtual SupplementalSheetItemDetails GetSupplementalSheetItemFromReader(IDataReader reader)
    {
      SupplementalSheetItemDetails supplementalSheetItem = new SupplementalSheetItemDetails(
          (int)reader["SupplementalSheetID"],
          (int)reader["PlayerID"],
          (int)reader["Seqno"],
          (bool)reader["SleeperTag"],
          (bool)reader["BustTag"],
          (string)reader["Note"]);
      return supplementalSheetItem;
    }

    protected virtual List<SupplementalSheetItemDetails> GetSupplementalSheetItemCollectionFromReader(IDataReader reader)
    {
      List<SupplementalSheetItemDetails> supplementalSheetItems = new List<SupplementalSheetItemDetails>();
      while (reader.Read())
        supplementalSheetItems.Add(GetSupplementalSheetItemFromReader(reader));
      return supplementalSheetItems;
    }

    // Stats

    protected virtual StatDetails GetStatFromReader(IDataReader reader)
    {
      StatDetails stat = new StatDetails(
          reader["StatCode"].ToString(),
          reader["Name"].ToString(),
          reader["Abbreviation"].ToString(),
          reader["Description"].ToString());
      return stat;
    }

    protected virtual List<StatDetails> GetStatCollectionFromReader(IDataReader reader)
    {
      List<StatDetails> stats = new List<StatDetails>();
      while (reader.Read())
        stats.Add(GetStatFromReader(reader));
      return stats;
    }

    // Player Stats

    // SportSeasonPlayerSeasonStats

    protected virtual SportSeasonPlayerSeasonStatDetails GetSportSeasonPlayerSeasonStatFromReader(IDataReader reader)
    {
      SportSeasonPlayerSeasonStatDetails sportSeasonPlayerSeasonStat = new SportSeasonPlayerSeasonStatDetails(
        reader["SportCode"].ToString(),
        reader["SeasonCode"].ToString(),
        (int)reader["PlayerID"],
        reader["StatCode"].ToString(),
        (double)reader["StatValue"]);
      return sportSeasonPlayerSeasonStat;
    }

    protected virtual List<SportSeasonPlayerSeasonStatDetails> GetSportSeasonPlayerSeasonStatCollectionFromReader(IDataReader reader)
    {
      List<SportSeasonPlayerSeasonStatDetails> sportSeasonPlayerSeasonStats = new List<SportSeasonPlayerSeasonStatDetails>();
      while (reader.Read())
        sportSeasonPlayerSeasonStats.Add(GetSportSeasonPlayerSeasonStatFromReader(reader));
      return sportSeasonPlayerSeasonStats;
    }


    // SportSeasonPlayerTeam

    protected virtual SportSeasonPlayerTeamDetails GetSportSeasonPlayerTeamFromReader(IDataReader reader)
    {
      SportSeasonPlayerTeamDetails sportSeasonPlayerTeam = new SportSeasonPlayerTeamDetails(
        reader["SportCode"].ToString(),
        reader["SeasonCode"].ToString(),
        (int)reader["PlayerID"],
        reader["TeamCode"].ToString());
      return sportSeasonPlayerTeam;
    }

    //protected virtual List<SportSeasonPlayerTeamDetails> GetSportSeasonPlayerTeamCollectionFromReader(IDataReader reader)
    //{
    //  List<SportSeasonPlayerTeamDetails> sportSeasonPlayerSeasonStats = new List<SportSeasonPlayerTeamDetails>();
    //  while (reader.Read())
    //    sportSeasonPlayerSeasonStats.Add(GetSportSeasonPlayerSeasonStatFromReader(reader));
    //  return sportSeasonPlayerSeasonStats;
    //}



    // Bye Weeks

    protected virtual ByeWeekDetails GetByeWeekFromReader(IDataReader reader)
    {
      ByeWeekDetails byeWeek = new ByeWeekDetails(
        reader["SeasonCode"].ToString(),
        reader["SportCode"].ToString(),
        reader["TeamCode"].ToString(),
        (int)reader["ByeWeek"]);
      return byeWeek;
    }

    protected virtual List<ByeWeekDetails> GetByeWeekCollectionFromReader(IDataReader reader)
    {
      List<ByeWeekDetails> byeWeeks = new List<ByeWeekDetails>();
      while (reader.Read())
        byeWeeks.Add(GetByeWeekFromReader(reader));
      return byeWeeks;
    }

    // Seasons

    protected virtual SeasonDetails GetSeasonFromReader(IDataReader reader)
    {
      SeasonDetails season = new SeasonDetails(
        reader["SeasonCode"].ToString(),
        reader["Name"].ToString());
      return season;
    }

    protected virtual List<SeasonDetails> GetSeasonCollectionFromReader(IDataReader reader)
    {
      List<SeasonDetails> seasons = new List<SeasonDetails>();
      while (reader.Read())
        seasons.Add(GetSeasonFromReader(reader));
      return seasons;
    }


    // Cheat Sheet Stats

    protected virtual CheatSheetStatDetails GetCheatSheetStatFromReader(IDataReader reader)
    {
      CheatSheetStatDetails cheatSheetStat = new CheatSheetStatDetails(
        (int)reader["CheatSheetID"],
        reader["StatCode"].ToString());
      return cheatSheetStat;
    }

    protected virtual List<CheatSheetStatDetails> GetCheatSheetStatCollectionFromReader(IDataReader reader)
    {
      List<CheatSheetStatDetails> cheatSheetStats = new List<CheatSheetStatDetails>();
      while (reader.Read())
        cheatSheetStats.Add(GetCheatSheetStatFromReader(reader));
      return cheatSheetStats;
    }

    // Cheat Sheet Positions

    protected virtual CheatSheetPositionDetails GetCheatSheetPositionFromReader(IDataReader reader)
    {
      CheatSheetPositionDetails cheatSheetPosition = new CheatSheetPositionDetails(
        (int)reader["CheatSheetID"],
        reader["PositionCode"].ToString());
      return cheatSheetPosition;
    }

    protected virtual List<CheatSheetPositionDetails> GetCheatSheetPositionCollectionFromReader(IDataReader reader)
    {
      List<CheatSheetPositionDetails> cheatSheetPositions = new List<CheatSheetPositionDetails>();
      while (reader.Read())
        cheatSheetPositions.Add(GetCheatSheetPositionFromReader(reader));
      return cheatSheetPositions;
    }

    // Cheat Sheets

    protected virtual CheatSheetDetails GetCheatSheetFromReader(IDataReader reader)
    {

      bool? pprLeague = (reader["PPRLeague"] == DBNull.Value) ? null : (bool?)reader["PPRLeague"];
      bool? auctionDraft = (reader["AuctionDraft"] == DBNull.Value) ? null : (bool?)reader["AuctionDraft"];

      CheatSheetDetails cheatSheet = new CheatSheetDetails(
        (int)reader["CheatSheetID"],
        reader["Username"].ToString(),
        reader["SeasonCode"].ToString(),
        reader["SportCode"].ToString(),
        reader["SheetName"].ToString(),
        reader["StatsSeasonCode"].ToString(),
        (DateTime)reader["Created"],
        (DateTime)reader["LastUpdated"],
        pprLeague,
        auctionDraft);
      return cheatSheet;
    }

    protected virtual List<CheatSheetDetails> GetCheatSheetCollectionFromReader(IDataReader reader)
    {
      List<CheatSheetDetails> cheatSheets = new List<CheatSheetDetails>();
      while (reader.Read())
        cheatSheets.Add(GetCheatSheetFromReader(reader));
      return cheatSheets;
    }


    protected virtual ArchivedCheatSheetDetails GetArchivedCheatSheetFromReader(IDataReader reader)
    {

      bool? pprLeague = (reader["PPRLeague"] == DBNull.Value) ? null : (bool?)reader["PPRLeague"];

      ArchivedCheatSheetDetails archivedCheatSheet = new ArchivedCheatSheetDetails(
        (int)reader["ArchivedCheatSheetID"],
        reader["SeasonCode"].ToString(),
        reader["SportCode"].ToString(),
        reader["PositionCode"].ToString(),
        reader["SheetName"].ToString(),
        reader["Username"].ToString(),
        (DateTime)reader["Created"],
        (DateTime)reader["LastUpdated"],
        pprLeague);
      return archivedCheatSheet;
    }

    protected virtual List<ArchivedCheatSheetDetails> GetArchivedCheatSheetCollectionFromReader(IDataReader reader)
    {
      List<ArchivedCheatSheetDetails> archivedCheatSheets = new List<ArchivedCheatSheetDetails>();
      while (reader.Read())
        archivedCheatSheets.Add(GetArchivedCheatSheetFromReader(reader));
      return archivedCheatSheets;
    }


    protected virtual UserSheetPositionGradeDetails GetUserSheetPositionGradeFromReader(IDataReader reader)
    {
      UserSheetPositionGradeDetails UserSheetPositionGrade = new UserSheetPositionGradeDetails(
        (int)reader["UserSheetPositionGradeID"],
        reader["SeasonCode"].ToString(),
        reader["SportCode"].ToString(),
        reader["PositionCode"].ToString(),
        (int)reader["ArchivedCheatSheetID"],
        reader["UserName"].ToString(),
        (int)reader["TotalRankDifferential"],
        (int)reader["Score"],
        (int)reader["Rank"]);
      return UserSheetPositionGrade;
    }

    protected virtual List<UserSheetPositionGradeDetails> GetUserSheetPositionGradeCollectionFromReader(IDataReader reader)
    {
      List<UserSheetPositionGradeDetails> UserSheetPositionGrades = new List<UserSheetPositionGradeDetails>();
      while (reader.Read())
        UserSheetPositionGrades.Add(GetUserSheetPositionGradeFromReader(reader));
      return UserSheetPositionGrades;
    }





    protected virtual UserSheetPlayerDifferentialDetails GetUserSheetPlayerDifferentialDetailsFromReader(IDataReader reader)
    {

      UserSheetPlayerDifferentialDetails userSheetPlayerDifferential = new UserSheetPlayerDifferentialDetails(
        (int)reader["UserSheetPlayerDifferentialID"],
        reader["SeasonCode"].ToString(),
        reader["SportCode"].ToString(),
        reader["PositionCode"].ToString(),
        (int)reader["PlayerID"],
        (decimal)reader["AverageDifferential"]);
      return userSheetPlayerDifferential;
    }

    protected virtual List<UserSheetPlayerDifferentialDetails> GetUserSheetPlayerDifferentialCollectionFromReader(IDataReader reader)
    {
      List<UserSheetPlayerDifferentialDetails> userSheetPlayerDifferentials = new List<UserSheetPlayerDifferentialDetails>();
      while (reader.Read())
        userSheetPlayerDifferentials.Add(GetUserSheetPlayerDifferentialDetailsFromReader(reader));
      return userSheetPlayerDifferentials;
    }



    protected virtual UserSportSeasonLeaderboardDetails GetUserSportSeasonLeaderboardDetailsFromReader(IDataReader reader)
    {

      UserSportSeasonLeaderboardDetails userSportSeasonLeaderboard = new UserSportSeasonLeaderboardDetails(
        (int)reader["UserSheetLeaderboardID"],
        reader["SportCode"].ToString(),
        reader["SeasonCode"].ToString(),
        reader["Username"].ToString(),
        (int)reader["QBScore"],
        (int)reader["RBScore"],
        (int)reader["WRScore"],
        (int)reader["TEScore"],
        (int)reader["KScore"],
        (int)reader["DFScore"],
        (int)reader["OverallScore"],
        (int)reader["Rank"]);
      return userSportSeasonLeaderboard;
    }

    protected virtual List<UserSportSeasonLeaderboardDetails> GetUserSportSeasonLeaderboardCollectionFromReader(IDataReader reader)
    {
      List<UserSportSeasonLeaderboardDetails> userSheetPlayerDifferentials = new List<UserSportSeasonLeaderboardDetails>();
      while (reader.Read())
        userSheetPlayerDifferentials.Add(GetUserSportSeasonLeaderboardDetailsFromReader(reader));
      return userSheetPlayerDifferentials;
    }








    // Cheat Sheet Items

    protected virtual CheatSheetItemDetails GetCheatSheetItemFromReader(IDataReader reader)
    {

      bool? sleeper = (reader["SleeperTag"] == DBNull.Value) ? null : (bool?)reader["SleeperTag"];
      bool? bust = (reader["BustTag"] == DBNull.Value) ? null : (bool?)reader["BustTag"];
      bool? injured = (reader["InjuredTag"] == DBNull.Value) ? null : (bool?)reader["InjuredTag"];

      CheatSheetItemDetails cheatSheetItem = new CheatSheetItemDetails(
        (int)reader["CheatSheetID"],
        (int)reader["PlayerID"],
        (short)reader["Seqno"],
        sleeper,
        bust,
        injured,
        reader["Note"].ToString());
      return cheatSheetItem;
    }

    protected virtual List<CheatSheetItemDetails> GetCheatSheetItemCollectionFromReader(IDataReader reader)
    {
      List<CheatSheetItemDetails> cheatSheetItems = new List<CheatSheetItemDetails>();
      while (reader.Read())
        cheatSheetItems.Add(GetCheatSheetItemFromReader(reader));
      return cheatSheetItems;
    }



    // Archived Cheat Sheet Items

    protected virtual ArchivedCheatSheetItemDetails GetArchivedCheatSheetItemFromReader(IDataReader reader)
    {

      bool? sleeper = (reader["SleeperTag"] == DBNull.Value) ? null : (bool?)reader["SleeperTag"];
      bool? bust = (reader["BustTag"] == DBNull.Value) ? null : (bool?)reader["BustTag"];
      bool? injured = (reader["InjuredTag"] == DBNull.Value) ? null : (bool?)reader["InjuredTag"];

      ArchivedCheatSheetItemDetails archivedCheatSheetItem = new ArchivedCheatSheetItemDetails(
        (int)reader["ArchivedCheatSheetID"],
        (int)reader["PlayerID"],
        (short)reader["Seqno"],
        sleeper,
        bust,
        injured,
        reader["Note"].ToString());
      return archivedCheatSheetItem;
    }

    protected virtual List<ArchivedCheatSheetItemDetails> GetArchivedCheatSheetItemCollectionFromReader(IDataReader reader)
    {
      List<ArchivedCheatSheetItemDetails> archivedCheatSheetItems = new List<ArchivedCheatSheetItemDetails>();
      while (reader.Read())
        archivedCheatSheetItems.Add(GetArchivedCheatSheetItemFromReader(reader));
      return archivedCheatSheetItems;
    }


    // ADP Positional Timestamps

    protected virtual ADPCalculationDetails GetADPCalculationFromReader(IDataReader reader)
    {

      int timespanInDays = (reader["TimespanInDays"] == DBNull.Value) ? 0 : int.Parse(reader["TimespanInDays"].ToString());
      int totalSheetsConsidered = (reader["TotalSheetsConsidered"] == DBNull.Value) ? 0 : int.Parse(reader["TotalSheetsConsidered"].ToString());
      int last24Hours = (reader["TotalSheetsConsidered"] == DBNull.Value) ? 0 : int.Parse(reader["Last24Sheets"].ToString());
      int last48Hours = (reader["TotalSheetsConsidered"] == DBNull.Value) ? 0 : int.Parse(reader["Last48Sheets"].ToString());
      int last72Hours = (reader["TotalSheetsConsidered"] == DBNull.Value) ? 0 : int.Parse(reader["Last72Sheets"].ToString());

      ADPCalculationDetails ADPCalculation = new ADPCalculationDetails(
        int.Parse(reader["ADPCalculationID"].ToString()),
        reader["SportCode"].ToString(),
        reader["SeasonCode"].ToString(),
        reader["PositionCode"].ToString(),
        (DateTime)reader["CalcTimestamp"],
        totalSheetsConsidered,
        last24Hours,
        last48Hours,
        last72Hours,
        timespanInDays);
      return ADPCalculation;
    }

    protected virtual List<ADPCalculationDetails> GetADPCalculationCollectionFromReader(IDataReader reader)
    {
      List<ADPCalculationDetails> ADPCalculations = new List<ADPCalculationDetails>();
      while (reader.Read())
        ADPCalculations.Add(GetADPCalculationFromReader(reader));
      return ADPCalculations;
    }

    // ADP Player Logs 

    protected virtual ADPPlayerLogDetails GetADPPlayerLogDetailsFromReader(IDataReader reader)
    {
      ADPPlayerLogDetails adpPlayerLogDetails = new ADPPlayerLogDetails(
        (int)reader["ADPPlayerLogID"],
        (int)reader["ADPCalculationID"],
        reader["SportCode"].ToString(),
        reader["SeasonCode"].ToString(),
        (int)reader["PlayerID"],
        (double)reader["ADP"],
        (DateTime)reader["CalcTimestamp"]);
      return adpPlayerLogDetails;
    }

    protected virtual List<ADPPlayerLogDetails> GetADPlayerLogDetailsCollectionFromReader(IDataReader reader)
    {
      List<ADPPlayerLogDetails> adpPlayerLogs = new List<ADPPlayerLogDetails>();
      while (reader.Read())
        adpPlayerLogs.Add(GetADPPlayerLogDetailsFromReader(reader));
      return adpPlayerLogs;
    }




    protected virtual SportSeasonSuppPlayerReviewDetails GetSportSeasonSuppPlayerReviewDetailsFromReader(IDataReader reader)
    {
      SportSeasonSuppPlayerReviewDetails suppSeasonSuppPlayerReviewDetails = new SportSeasonSuppPlayerReviewDetails(
        reader["SportCode"].ToString(),
        reader["SeasonCode"].ToString(),
        (int)reader["SupplementalSourceID"],
        (int)reader["PlayerID"],
        reader["ReviewURL"].ToString());
      return suppSeasonSuppPlayerReviewDetails;
    }

    protected virtual List<SportSeasonSuppPlayerReviewDetails> GetSportSeasonSuppPlayerReviewDetailsCollectionFromReader(IDataReader reader)
    {
      List<SportSeasonSuppPlayerReviewDetails> sportSeasonSuppPlayerReviewLogs = new List<SportSeasonSuppPlayerReviewDetails>();
      while (reader.Read())
        sportSeasonSuppPlayerReviewLogs.Add(GetSportSeasonSuppPlayerReviewDetailsFromReader(reader));
      return sportSeasonSuppPlayerReviewLogs;
    }



    protected virtual BlogDetails GetBlogDetailsFromReader(IDataReader reader)
    {
      BlogDetails blogDetails = new BlogDetails(
        (Guid)reader["PostID"],
        reader["Title"].ToString(),
        reader["Description"].ToString(),
        reader["Slug"].ToString(),
        (DateTime)reader["DateCreated"]);
      return blogDetails;
    }

    protected virtual List<BlogDetails> GetBlogDetailsCollectionFromReader(IDataReader reader)
    {
      List<BlogDetails> blogs = new List<BlogDetails>();
      while (reader.Read())
        blogs.Add(GetBlogDetailsFromReader(reader));
      return blogs;
    }


    protected virtual UpgradeVoucherDetails GetUpgradeVoucherDetailsFromReader(IDataReader reader)
    {
      DateTime claimedDate = (reader["ClaimedDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)reader["ClaimedDate"];

      UpgradeVoucherDetails upgradeVoucherDetails = new UpgradeVoucherDetails(
        (int)reader["VoucherID"],
        reader["SportCode"].ToString(),
        reader["SeasonCode"].ToString(),
        reader["VoucherCode"].ToString(),
        reader["CampaignTag"].ToString(),
        (DateTime)reader["GeneratedDate"],
        claimedDate);
      return upgradeVoucherDetails;
    }

    protected virtual List<UpgradeVoucherDetails> GetUpgradeVoucherDetailsCollectionFromReader(IDataReader reader)
    {
      List<UpgradeVoucherDetails> upgradeVouchers = new List<UpgradeVoucherDetails>();
      while (reader.Read())
        upgradeVouchers.Add(GetUpgradeVoucherDetailsFromReader(reader));
      return upgradeVouchers;
    }


    protected virtual UpgradeUserDetails GetUpgradeUserDetailsFromReader(IDataReader reader)
    {
      int voucherId = 0;
      int.TryParse(reader["VoucherId"].ToString(), out voucherId);

      DateTime confirmationTimestamp = DateTime.MinValue;
      DateTime.TryParse(reader["ConfirmationPageTimestamp"].ToString(), out confirmationTimestamp);

      DateTime ipnTimestamp = DateTime.MinValue;
      DateTime.TryParse(reader["IPNTimestamp"].ToString(), out ipnTimestamp);

      UpgradeUserDetails upgradeUserDetails = new UpgradeUserDetails(
        reader["SportCode"].ToString(),
        reader["SeasonCode"].ToString(),
        (Guid)reader["UserId"],
        voucherId,
        reader["UpgradeTypeCode"].ToString(),
        confirmationTimestamp,
        ipnTimestamp);
      return upgradeUserDetails;
    }

    protected virtual List<UpgradeUserDetails> GetUpgradeUserDetailsDetailsCollectionFromReader(IDataReader reader)
    {
      List<UpgradeUserDetails> upgradeUsers = new List<UpgradeUserDetails>();
      while (reader.Read())
        upgradeUsers.Add(GetUpgradeUserDetailsFromReader(reader));
      return upgradeUsers;
    }



    protected virtual UpgradeTypeDetails GetUpgradeTypeDetailsFromReader(IDataReader reader)
    {
      UpgradeTypeDetails upgradeType = new UpgradeTypeDetails(
        reader["UpgradeTypeCode"].ToString(),
        reader["Name"].ToString(),
        reader["Description"].ToString());
      return upgradeType;
    }

    protected virtual List<UpgradeTypeDetails> GetUpgradeTypeDetailsCollectionFromReader(IDataReader reader)
    {
      List<UpgradeTypeDetails> upgradeTypes = new List<UpgradeTypeDetails>();
      while (reader.Read())
        upgradeTypes.Add(GetUpgradeTypeDetailsFromReader(reader));
      return upgradeTypes;
    }



    protected virtual UserSessionDetails GetUserSessionDetailsFromReader(IDataReader reader)
    {
      UserSessionDetails blogDetails = new UserSessionDetails(
        (Guid)reader["UserID"],
        (int)reader["SessionCount"],
        reader["Email"].ToString(),
        reader["UserName"].ToString());
      return blogDetails;
    }

    protected virtual List<UserSessionDetails> GetUserSessionDetailsCollectionFromReader(IDataReader reader)
    {
      List<UserSessionDetails> userSessions = new List<UserSessionDetails>();
      while (reader.Read())
        userSessions.Add(GetUserSessionDetailsFromReader(reader));
      return userSessions;
    }

  }
}
