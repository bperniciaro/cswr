using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace BP.CheatSheetWarRoom.DAL
{
  public abstract partial class SheetsProvider : DataAccess
  {
    public SheetsProvider()
    {
      this.ConnectionString = Globals.CSWRSettings.Sheets.ConnectionString;
    }

    // Setting Methods
    public abstract SettingDetails GetSetting(string settingCode);
    public abstract bool UpdateSettingValue(SettingDetails setting);

    // SportSetting Methods
    public abstract SportSettingDetails GetSportSetting(string sportCode, string settingCode);
    public abstract bool UpdateSportSettingValue(SportSettingDetails setting);

    // Team Methods
    public abstract TeamDetails GetTeam(string teamCode);
    public abstract TeamDetails GetTeam(int playerID);
    public abstract TeamDetails GetTeam(string sportCode, string mascot);
    public abstract List<TeamDetails> GetTeams(string sportCode);

    // Sport Methods
    public abstract SportDetails GetSport(string sportCode);
    public abstract List<SportDetails> GetSports();

    // Stat Methods
    public abstract StatDetails GetStat(string statCode);
    public abstract List<StatDetails> GetStats(string sportCode, string positionCode);

    // Position Methods
    public abstract PositionDetails GetPosition(string positionCode);
    public abstract List<PositionDetails> GetPositions(string sportCode);

    // Player Methods
    public abstract PlayerDetails GetPlayer(int playerID);
    public abstract PlayerDetails GetPlayerByStatMapID(int statMapID);
    public abstract PlayerDetails GetPlayer(string sportCode, string firstName, string middleName, string lastName);
    public abstract PlayerDetails GetPlayer(string sportCode, string teamCode, string firstName, string middleName, string lastName);
    public abstract PlayerDetails GetDefensivePlayer(string teamCode);
    public abstract List<PlayerDetails> GetDefensivePlayers();

    public abstract List<PlayerDetails> GetPlayers(string sportCode);
    public abstract List<PlayerDetails> GetPlayersByPartialName(string sportCode, string partialNameFirstLast);
    public abstract List<PlayerDetails> GetPlayers(string sportCode, string firstName, string lastName);
    public abstract List<PlayerDetails> GetPlayers(string sportCode, string seasonCode, bool includeRetired);
    public abstract List<PlayerDetails> GetPlayers(string sportCode, string seasonCode, string teamCode, bool includeRetired);
    public abstract List<PlayerDetails> GetPlayers(string sportCode, string seasonCode, string teamCode, string positionCode, bool includeRetired);
    public abstract List<PlayerDetails> GetPlayers(string sportCode, string seasonCode, string positionCode, bool includeRetired, string statCode, string sortDir);
    public abstract List<PlayerDetails> GetPlayers(string sportCode, string seasonCode, string positionCode, bool includeRetired, string statCode, string sortDir, int limit);
    public abstract List<PlayerDetails> GetPlayers(string sportCode, string seasonCode, string positionCode, string firstName, string lastName, bool includeRetired);
    public abstract List<PlayerDetails> GetPlayers(string sportCode, string seasonCode, string positionCode, char firstInitial, string lastName, bool includeRetired);
    public abstract List<PlayerDetails> GetPlayersBySportSeasonPositionCodes(string sportCode, string seasonCode, string positionCode, bool includeRetired);
    public abstract List<PlayerDetails> GetPlayers(string sportCode, string lastName);

    public abstract List<PlayerDetails> GetPlayers(string sportCode, string seasonCode, List<PositionDetails> positions, bool includeRetired, string statCode);

    
    public abstract int InsertPlayer(PlayerDetails player);
    //public abstract List<PlayerDetails> GetUnprocessedPlayersBySeasonSportPositionCodes(string seasonCode, string sportCode, string positionCode);
    public abstract List<PlayerDetails> GetPlayerRookies(string sportCode, string seasonCode);
    public abstract List<PlayerDetails> GetPlayerRookies(string sportCode, string seasonCode, string teamCode);
    public abstract List<PlayerDetails> GetPlayerRookiesBySportSeasonPositionCodes(string sportCode, string seasonCode, string positionCode);
    public abstract bool DeletePlayer(int playerID, ref List<CheatSheetDetails> cheatSheets, ref List<SupplementalSheetDetails> supplementalSheets);
    public abstract bool UpdatePlayer(PlayerDetails player);


    // PlayerStatusCode Methods
    public abstract List<PlayerStatusCodeDetails> GetPlayerStatusCodes(string sportCode);
    public abstract PlayerStatusCodeDetails GetPlayerStatusCode(string statusCode);

    // SportSeasonPlayerStatus Methods
    public abstract int InsertSportSeasonPlayerStatus(SportSeasonPlayerStatusDetails status);
    public abstract bool UpdateSportSeasonPlayerStatus(SportSeasonPlayerStatusDetails playerStatus);
    public abstract List<SportSeasonPlayerStatusDetails> GetSportSeasonPlayerStatuses(string seasonCode, string statusCode);



    // Supplemental Source Methods
    public abstract List<SupplementalSourceDetails> GetSupplementalSources();
    public abstract List<SupplementalSourceDetails> GetSupplementalSources(string seasonCode, string sportCode, string positionCode);
    
    
    public abstract SupplementalSourceDetails GetSupplementalSource(int supplementalSourceID);
    public abstract SupplementalSourceDetails GetSupplementalSource(string abbreviation);
    public abstract int InsertSupplementalSource(SupplementalSourceDetails supplementalSource);
    public abstract bool DeleteSupplementalSource(int supplementalSourceID);
    public abstract bool UpdateSupplementalSource(SupplementalSourceDetails supplementalSource);


    // Supplemental Sheet Methods
    public abstract List<SupplementalSheetDetails> GetSupplementalSheets(string sportCode);
    public abstract List<SupplementalSheetDetails> GetSupplementalSheets(string seasonCode, string sportCode);

    public abstract bool CreateSupplementalSheet(SupplementalSheetDetails supplementalSheet);
    public abstract SupplementalSheetDetails GetSupplementalSheet(int supplementalSheetID);
    public abstract SupplementalSheetDetails GetSupplementalSheet(string seasonCode, int supplementalSourceID, string sportCode, string positionCode);
    
    public abstract bool UpdateSupplementalSheet(SupplementalSheetDetails supplementalSheet);
    public abstract bool DeleteSupplementalSheet(int supplementalSheetID);
    public abstract bool ReorderSupplementalSheetItems(int supplementalSheetID, int oldIndex, int newIndex);
    public abstract bool RemoveAllSupplementalSheetItems(int supplementalSheetID);


    // Supplemental Sheet Item Methods
    public abstract List<SupplementalSheetItemDetails> GetSupplementalSheetItems(int supplementalSheetID);
    public abstract SupplementalSheetItemDetails GetSupplementalSheetItem(int supplementalSheetID, int playerID);

    public abstract bool UpdateSupplementalSheetItem(SupplementalSheetItemDetails supplementalSheetItem);

    /* adds an item to the end of the sheet */
    public abstract bool AddSupplementalSheetItem(SupplementalSheetItemDetails supplementalSheetItem);
    /* removes the indicated item from the sheet, identified by PlayerID, other records are adjusted */
    public abstract bool RemoveSupplementalSheetItem(SupplementalSheetItemDetails supplementalSheetItem);
    /* inserts an item at the position indicated by the sequence number, other records are adjusted */
    public abstract int InsertSupplementalSheetItem(SupplementalSheetItemDetails supplementalSheetItem);

    // Player Weekly Stat Methods
    public abstract int GetSportSeasonPlayerWeeklyStatCount(string sportCode, string seasonCode, int week);
    public abstract int InsertSportSeasonPlayerWeeklyStat(SportSeasonPlayerWeeklyStatDetails sportSeasonPlayerWeeklyStat);
    public abstract bool DeleteSportSeasonPlayerWeeklyStats(string sportCode, string seasonCode, int week, int playerID);
    public abstract bool DeleteSportSeasonPlayerWeeklyStats(string sportCode, string seasonCode, int week, string positionCode);

    // Sport Season Player Season Stat Methods
    public abstract List<SportSeasonPlayerSeasonStatDetails> GetSportSeasonPlayerSeasonStats(string sportCode, string seasonCode, int playerID);
    public abstract List<SportSeasonPlayerSeasonStatDetails> GetSportSeasonPlayerSeasonStats(string sportCode, string seasonCode);
    public abstract List<SportSeasonPlayerSeasonStatDetails> GetSportSeasonPlayerSeasonStats(string sportCode, string seasonCode, string positionCode, string statCode);
    public abstract SportSeasonPlayerSeasonStatDetails GetSportSeasonPlayerSeasonStat(string sportCode, string seasonCode, int playerID, string statCode);

    // Sport Season Player Team Methods
    public abstract SportSeasonPlayerTeamDetails GetSportSeasonPlayerTeam(string sportCode, string seasonCode, int playerID);
    public abstract int InsertSportSeasonPlayerTeam(SportSeasonPlayerTeamDetails sportSeasonPlayerTeam);


    public abstract int InsertSportSeasonPlayerSeasonStat(SportSeasonPlayerSeasonStatDetails sportSeasonPlayerSeasonStat);
    public abstract bool UpdateSportSeasonPlayerSeasonStat(SportSeasonPlayerSeasonStatDetails sportSeasonPlayerSeasonStat);
    public abstract bool DeleteSportSeasonPlayerSeasonStats(string sportCode, string seasonCode, int playerID);
    public abstract bool DeleteSportSeasonPlayerSeasonStats(string sportCode, string seasonCode, string positionCode);

    // Bye Week Methods
    public abstract ByeWeekDetails GetByeWeek(string seasonCode, string sportCode, string teamCode);
    public abstract List<ByeWeekDetails> GetByeWeeks(string seasonCode, string sportCode);

    // Season Methods
    public abstract List<SeasonDetails> GetSeasons();
    public abstract SeasonDetails GetCurrentSeason();

    // Sports Season Methods
    public abstract SportSeasonDetails GetCurrentSportSeason(string sportCode);
    public abstract List<SportSeasonDetails> GetSportSeasons(string sportCode);
    public abstract SportSeasonDetails GetCurrentSportStatSeason(string sportCode);
    public abstract List<SportSeasonDetails> GetSportStatSeasons(string sportCode);



    // Cheat Sheet Stat Methods
    public abstract List<CheatSheetStatDetails> GetCheatSheetStats(int cheatSheetID, string sportCode, string positionCode);
    public abstract int InsertCheatSheetStat(CheatSheetStatDetails cheatSheetStat);
    public abstract int InsertCheatSheetStat(CheatSheetStatDetails cheatSheetStat, SqlConnection cn, SqlTransaction transaction);

    // Cheat Sheet Position Methods
    public abstract List<CheatSheetPositionDetails> GetCheatSheetPositions(int cheatSheetID);
    public abstract int InsertCheatSheetPosition(CheatSheetPositionDetails cheatSheetPosition);
    public abstract int InsertCheatSheetPosition(CheatSheetPositionDetails cheatSheetPosition, SqlConnection cn, SqlTransaction transaction);


    // Cheat Sheet Methods
    public abstract CheatSheetDetails GetCheatSheet(int cheatSheetID);
    public abstract List<CheatSheetDetails> GetUserCheatSheets(string userName);
    public abstract List<CheatSheetDetails> GetUserCheatSheets(string userName, string sportCode);
    public abstract List<CheatSheetDetails> GetUserCheatSheets(string userName, string sportCode, string positionCode);
    public abstract List<CheatSheetDetails> GetCheatSheets(string sportCode);
    public abstract List<CheatSheetDetails> GetCheatSheets(string sportCode, string seasonCode, string positionCode);

    public abstract int GetCheatSheetCount(string userName);
    public abstract int GetCheatSheetCount(string userName, string sportCode);
    
    public abstract int GetMemberCheatSheetCount();
    public abstract int GetMemberCheatSheetCount(string sportCode);
    public abstract int GetVisitorCheatSheetCount();
    public abstract int GetVisitorCheatSheetCount(string sportCode);

    /* stats for the purpose of evaluating feature usage (and improving usage percentages) */
    public abstract int GetUserCheatSheetSleeperUsageCount(string sportCode, string seasonCode);
    public abstract int GetUserCheatSheetBustUsageCount(string sportCode, string seasonCode);
    public abstract int GetUserCheatSheetNoteUsageCount(string sportCode, string seasonCode);
    
    /* should add to end of sheet */
    public abstract bool AddCheatSheetItem(CheatSheetItemDetails cheatSheetItem);
    /* should remove based on PlayerID, then adjust records */
    public abstract bool RemoveCheatSheetItem(CheatSheetItemDetails cheatSheetItem);
    /* inserts an item at the position indicated by the sequence number, other records are adjusted */
    public abstract int InsertCheatSheet(CheatSheetDetails cheatSheet);
    public abstract int InsertCheatSheet(CheatSheetDetails cheatSheet, SqlConnection cn, SqlTransaction transaction);

    public abstract bool RemoveAllCheatSheetItems(int cheatSheetID);
    public abstract int CreateCheatSheet(CheatSheetDetails cheatSheet, string userName, List<PositionDetails> cheatSheetPositions, 
                                         List<StatDetails> cheatSheetStats, List<PlayerDetails> cheatSheetPlayers, int tryCount);
    public abstract bool UpdateCheatSheet(CheatSheetDetails cheatSheet);
    public abstract bool DeleteCheatSheet(int cheatSheetID);
    public abstract bool DeleteCheatSheets(string userName);
    public abstract void DeleteOldVisitorCheatSheets();

    // Cheat Sheet Items
    public abstract List<CheatSheetItemDetails> GetCheatSheetItems(int cheatSheetID);
    public abstract List<CheatSheetItemDetails> GetCheatSheetItems(int cheatSheetID, int recordCount);
    public abstract CheatSheetItemDetails GetCheatSheetItem(int cheatSheetID, int playerID);

    public abstract bool UpdateCheatSheetItem(CheatSheetItemDetails cheatSheetItem);
    public abstract int InsertCheatSheetItem(CheatSheetItemDetails cheatSheetItem);
    public abstract int InsertCheatSheetItem(CheatSheetItemDetails cheatSheetItem, SqlConnection cn, SqlTransaction transaction);
    public abstract bool ReorderCheatSheetItems(int cheatSheetId, int oldSeqno, int newSeqno);

    // Archived Cheat Sheets
    public abstract List<ArchivedCheatSheetDetails> GetArchivedCheatSheets(string seasonCode, string sportCode, string positionCode);
    public abstract ArchivedCheatSheetDetails GetArchivedCheatSheet(int archivedCheatSheetId);
    public abstract List<ArchivedCheatSheetItemDetails> GetArchivedCheatSheetItems(int archivedCheatSheetId);
    public abstract bool RemoveArchivedCheatSheetItem(ArchivedCheatSheetItemDetails archivedCheatSheetItem);
    public abstract int InsertArchivedCheatSheetItem(ArchivedCheatSheetItemDetails supplementalSheetItem);

    // User Sheet Player Differentials
    public abstract int InsertUserSheetPlayerDifferential(UserSheetPlayerDifferentialDetails userSheetPlayerDifferential);
    public abstract List<UserSheetPlayerDifferentialDetails> GetUserSheetPlayerDifferentials(string sportCode, string seasonCode);

    // User Sheet Position Grades
    public abstract int InsertUserSheetPositionGrade(UserSheetPositionGradeDetails sheetGrade);
    public abstract List<UserSheetPositionGradeDetails> GetUserSheetPositionGrades(string sportCode, string seasonCode);
    public abstract UserSheetPositionGradeDetails GetUserSheetPositionGrade(string sportCode, string seasonCode, string positionCode);
    public abstract int GetUserSheetPositionGradesCount(string sportCode, string seasonCode, string positionCode);

    // User Sport Season Leaderboard
    public abstract List<string> GetLeaderboardYears(string sportCode);
    public abstract int InsertUserSportSeasonLeaderboard(UserSportSeasonLeaderboardDetails leaderboard);
    public abstract List<UserSportSeasonLeaderboardDetails> GetUserSportSeasonLeaderboards(string sportCode, string seasonCode);


    // ADP Calculations
    public abstract int InsertADPCalculation(ADPCalculationDetails ADPCalculation);
    public abstract int GetADPCalculationCount(string seasonCode, string sportCode, string positionCode, DateTime calculationTimeStamp);
    public abstract List<ADPCalculationDetails> GetADPCalculations(string seasonCode, string sportCode, string positionCode);
    public abstract List<ADPCalculationDetails> GetADPCalculations(string seasonCode, string sportCode);
    public abstract List<ADPCalculationDetails> GetADPCalculations(string seasonCode, string sportCode, string positionCode, DateTime calcTimestamp);
    public abstract bool DeleteADPCalculation(int adpCalculationID);

    // ADP Player Logs
    public abstract List<ADPPlayerLogDetails> GetADPPlayerLogs(string sportCode, string seasonCode, string positionCode);
    public abstract List<ADPPlayerLogDetails> GetADPPlayerLogs(string sportCode, string seasonCode, string positionCode, DateTime calcTimestamp);
    public abstract int InsertADPPlayerLog(ADPPlayerLogDetails adpPlayerLog);

    // User Sessions
    public abstract bool LogUserSession(Guid userId);
    public abstract List<UserSessionDetails> GetUserSessions();

    // Sport Season Supplemental Player Review
    public abstract int InsertSportSeasonSuppPlayerReview(SportSeasonSuppPlayerReviewDetails sportSeasonSuppPlayerReview);
    public abstract List<SportSeasonSuppPlayerReviewDetails> GetSportSeasonSuppPlayerReviews(string sportCode, string seasonCode, int supplementalSourceID);
    public abstract bool UpdateSportSeasonSuppPlayerReview(SportSeasonSuppPlayerReviewDetails sportSeasonSuppPlayerReview);
    public abstract bool DeleteSportSeasonSuppPlayerReview(string sportCode, string seasonCode, int supplementalSourceID, int playerID);
    public abstract SportSeasonSuppPlayerReviewDetails GetSportSeasonSuppPlayerReview(string sportCode, string seasonCode, int supplementalSourceID, int playerID);

    // Upgrade Vouchers
    public abstract UpgradeVoucherDetails GetUpgradeVoucher(string voucherCode);
    public abstract int InsertUpgradeVoucher(UpgradeVoucherDetails upgradeVoucher);

    // Upgrade Users
    public abstract List<UpgradeUserDetails> GetUpgradeUsers(string sportCode, string seasonCode);
    public abstract int InsertUpgradeUser(UpgradeUserDetails upgradeUser);
    public abstract bool ConfirmUpgradeUser(string sportCode, string seasonCode, Guid userId);

    // Upgrade Type
    public abstract UpgradeTypeDetails GetUpgradeType(string upgradeTypeCode);


    // Blog
    //public abstract BlogDetails GetLatestBlogPost(string categoryName);


    /// <summary>
    /// Returns an instance of the provider type specified in the config file
    /// </summary>
    static private SheetsProvider _instance = null;
    static public SheetsProvider Instance
    {
      get
      {
        if (_instance == null)
          _instance = (SheetsProvider)Activator.CreateInstance(Type.GetType(Globals.CSWRSettings.Sheets.ProviderType));
        return _instance;
      }
    }


  }
}