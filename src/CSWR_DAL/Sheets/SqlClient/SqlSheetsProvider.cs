using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

//using BP.CheatSheetWarRoom.BLL.Sheets;


namespace BP.CheatSheetWarRoom.DAL.SqlClient
{
  public class SqlSheetsProvider : SheetsProvider
  {
    public SqlSheetsProvider()  {}


    // Setting Methods
    public override SettingDetails GetSetting(string settingCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSettingByCode", cn);
        cmd.Parameters.Add("@SettingCode", SqlDbType.NVarChar).Value = settingCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetSettingFromReader(reader);
        else
          return null;
      }
    }

    public override SportSettingDetails  GetSportSetting(string sportCode, string settingCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSportSettingBySportCodeSettingCode", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.Parameters.Add("@SettingCode", SqlDbType.Char).Value = settingCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetSportSettingFromReader(reader);
        else
          return null;
      }
    }


    public override bool UpdateSettingValue(SettingDetails setting)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_UpdateSettingValue", cn);
        cmd.Parameters.Add("@SettingCode", SqlDbType.NVarChar).Value = setting.SettingCode;
        cmd.Parameters.Add("@SettingValue", SqlDbType.NVarChar).Value = setting.SettingValue;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }

    public override bool UpdateSportSettingValue(SportSettingDetails sportSetting)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_UpdateSportSettingValue", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportSetting.SportCode;
        cmd.Parameters.Add("@SettingCode", SqlDbType.Char).Value = sportSetting.SettingCode;
        cmd.Parameters.Add("@SettingValue", SqlDbType.Char).Value = sportSetting.SettingValue;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }



    // Team Methods

    public override TeamDetails GetTeam(string teamCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetTeamByCode", cn);
        cmd.Parameters.Add("@TeamCode", SqlDbType.NVarChar).Value = teamCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetTeamFromReader(reader);
        else
          return null;
      }
    }


    public override TeamDetails GetTeam(string sportCode, string mascot)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetTeamBySportCodeMascot", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.Parameters.Add("@Mascot", SqlDbType.VarChar).Value = mascot;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetTeamFromReader(reader);
        else
          return null;
      }
    }
    
    public override TeamDetails GetTeam(int playerID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetTeamByPlayerID", cn);
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = playerID;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetTeamFromReader(reader);
        else
          return null;
      }
    }


    public override List<TeamDetails> GetTeams(string sportCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetTeamsBySportCode", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetTeamCollectionFromReader(ExecuteReader(cmd));
      }
    }




    // Sport Methods

    public override SportDetails GetSport(string sportCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSportByCode", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetSportFromReader(reader);
        else
          return null;
      }
    }

    public override List<SportDetails> GetSports()
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSports", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetSportCollectionFromReader(ExecuteReader(cmd));
      }
    }

    /*******************************************/
    /* Sport Season Methods                    */
    /*******************************************/

    public override SportSeasonDetails GetCurrentSportSeason(string sportCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetCurrentSportSeason", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetSportSeasonFromReader(reader);
        else
          return null;
      
      }
    }

    public override SportSeasonDetails GetCurrentSportStatSeason(string sportCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetCurrentSportStatSeason", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetSportSeasonFromReader(reader);
        else
          return null;
      }
    }


    public override List<SportSeasonDetails> GetSportSeasons(string sportCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSportSeasonsBySportCode", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetSportSeasonCollectionFromReader(ExecuteReader(cmd));
      }
    }

    public override List<SportSeasonDetails> GetSportStatSeasons(string sportCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSportStatSeasonsBySportCode", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetSportSeasonCollectionFromReader(ExecuteReader(cmd));
      }
    }


    // Position Methods

    public override PositionDetails GetPosition(string positionCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPositionByCode", cn);
        cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = positionCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetPositionFromReader(reader);
        else
          return null;
      }
    }

    public override List<PositionDetails> GetPositions(string sportCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPositionsBySportCode", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetPositionCollectionFromReader(ExecuteReader(cmd));
      }
    }

    // Player Methods

    public override PlayerDetails GetPlayer(int playerID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayerByID", cn);
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = playerID;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetPlayerFromReader(reader);
        else
          return null;
      }
    }


    public override PlayerDetails GetPlayerByStatMapID(int statMapID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayerByStatMapID", cn);
        cmd.Parameters.Add("@StatMapID", SqlDbType.Int).Value = statMapID;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetPlayerFromReader(reader);
        else
          return null;
      }
    }



    public override PlayerDetails GetPlayer(string sportCode, string firstName, string middleName, string lastName)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayerBySportFirstMiddleLastName", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = firstName;
        cmd.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = middleName;
        cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lastName;

        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetPlayerFromReader(reader);
        else
          return null;
      }
    }

    public override PlayerDetails GetPlayer(string sportCode, string teamCode, string firstName, string middleName, string lastName)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayerBySportTeamFirstMiddleLastName", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.Parameters.Add("@TeamCode", SqlDbType.Char).Value = teamCode;
        cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = firstName;
        cmd.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = middleName;
        cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lastName;

        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetPlayerFromReader(reader);
        else
          return null;
      }
    }

    /// <summary>
    /// Returns all players who have played the specified sport.  Retired players are also returned
    /// </summary>
    /// <param name="sportCode"></param>
    /// <returns></returns>
    public override List<PlayerDetails> GetPlayers(string sportCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayersBySportCode", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cn.Open();
        return GetPlayerCollectionFromReader(ExecuteReader(cmd));
      }
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="partialName"></param>
    /// <returns></returns>
    public override List<PlayerDetails> GetPlayersByPartialName(string sportCode, string partialName)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayersBySportCodePartialName", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.Parameters.Add("@PartialName", SqlDbType.VarChar).Value = partialName;
        cn.Open();
        return GetPlayerCollectionFromReader(ExecuteReader(cmd));
      }
    }


    /// <summary>
    /// Returns players for whom the specified first and last name match exactly.  Retired players
    /// are also returned.
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <returns></returns>
    public override List<PlayerDetails> GetPlayers(string sportCode, string firstName, string lastName)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayersBySportFirstNameLastName", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = firstName;
        cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = lastName;
        cn.Open();
        return GetPlayerCollectionFromReader(ExecuteReader(cmd));
      }
    }

    /// <summary>
    /// Returned players who played during the specified season or any preceding season, regardless
    /// of whether or not they registered a statistic.
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode"></param>
    /// <param name="includeRetired"></param>
    /// <returns></returns>
    public override List<PlayerDetails> GetPlayers(string sportCode, string seasonCode, bool includeRetired)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayersBySportSeasonCodes", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@IncludeRetired", SqlDbType.Bit).Value = includeRetired;
        cn.Open();
        return GetPlayerCollectionFromReader(ExecuteReader(cmd));
      }
    }


    /// <summary>
    /// Returned players who played the specified position during the specified season (or any preceding season), regardless
    /// of whether or not they registered a statistic.
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode"></param>
    /// <param name="includeRetired"></param>
    /// <returns></returns>
    public override List<PlayerDetails> GetPlayersBySportSeasonPositionCodes(string sportCode, string seasonCode, string positionCode, bool includeRetired)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayersBySportSeasonPositionCodes", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = positionCode;
        cmd.Parameters.Add("@IncludeRetired", SqlDbType.Bit).Value = includeRetired;
        cn.Open();
        return GetPlayerCollectionFromReader(ExecuteReader(cmd));
      }
    }


    public override List<PlayerDetails> GetPlayers(string sportCode, string seasonCode, string positionCode, bool includeRetired, string statCode, string sortDir)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayersBySportSeasonPositionStatCodes", cn);
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = positionCode;
        cmd.Parameters.Add("@StatCode", SqlDbType.NVarChar).Value = statCode;
        cmd.Parameters.Add("@IncludeRetired", SqlDbType.Bit).Value = includeRetired;
        cmd.Parameters.Add("@SortDir", SqlDbType.NVarChar).Value = sortDir;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetPlayerCollectionFromReader(ExecuteReader(cmd));
      }
    }


    public override List<PlayerDetails> GetPlayers(string sportCode, string seasonCode, string positionCode, bool includeRetired, string statCode, string sortDir, int limit)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayersBySportSeasonPositionStatCodesWithLimit", cn);
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = positionCode;
        cmd.Parameters.Add("@StatCode", SqlDbType.NVarChar).Value = statCode;
        cmd.Parameters.Add("@IncludeRetired", SqlDbType.Bit).Value = includeRetired;
        cmd.Parameters.Add("@SortDir", SqlDbType.NVarChar).Value = sortDir;
        cmd.Parameters.Add("@Limit", SqlDbType.Int).Value = limit;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetPlayerCollectionFromReader(ExecuteReader(cmd));
      }
    }

 
    public override List<PlayerDetails> GetPlayers(string sportCode, string seasonCode, string teamCode, bool includeRetired)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayersBySportSeasonTeamCodes", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode; 
        cmd.Parameters.Add("@TeamCode", SqlDbType.NVarChar).Value = teamCode;
        cmd.Parameters.Add("@IncludeRetired", SqlDbType.Bit).Value = includeRetired;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetPlayerCollectionFromReader(ExecuteReader(cmd));

      }
    }

    public override List<PlayerDetails> GetPlayers(string sportCode, string seasonCode, string teamCode, string positionCode, bool retired)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayersBySportSeasonTeamPositionCodes", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@TeamCode", SqlDbType.NVarChar).Value = teamCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = positionCode;
        cmd.Parameters.Add("@IncludeRetired", SqlDbType.Bit).Value = retired;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetPlayerCollectionFromReader(ExecuteReader(cmd));
      }
    }

    public override List<PlayerDetails> GetPlayers(string sportCode, string lastName)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayersBySportCodeLastName", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value =  lastName;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetPlayerCollectionFromReader(ExecuteReader(cmd));
      }
    }

    public override List<PlayerDetails> GetPlayers(string sportCode, string seasonCode, string positionCode, string firstName, string lastName, bool includeRetired)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayersBySportSeasonPositionFirstNameLastName", cn);
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.Char).Value = positionCode;
        cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = firstName;
        cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lastName;
        cmd.Parameters.Add("@Retired", SqlDbType.Bit).Value = includeRetired;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetPlayerCollectionFromReader(ExecuteReader(cmd));
      }
    }

    public override List<PlayerDetails> GetPlayers(string sportCode, string seasonCode, string positionCode, char firstInitial, string lastName, bool includeRetired)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayersBySportSeasonPositionFirstInitialLastName", cn);
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.Char).Value = positionCode;
        cmd.Parameters.Add("@FirstInitial", SqlDbType.VarChar).Value = firstInitial.ToString();
        cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lastName;
        cmd.Parameters.Add("@Retired", SqlDbType.Bit).Value = includeRetired;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetPlayerCollectionFromReader(ExecuteReader(cmd));
      }
    }


    public override List<PlayerDetails> GetPlayers(string sportCode, string seasonCode, List<PositionDetails> positions, bool includeRetired, string statCode)
    {
      DataTable positionTable = new DataTable();
      positionTable.Columns.Add("PositionCode");
      foreach(PositionDetails currentPosition in positions)  
      {
        positionTable.Rows.Add(currentPosition.PositionCode);
      }

      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayersBySportSeasonPositionsStatCode", cn);
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.Parameters.Add("@IncludeRetired", SqlDbType.Bit).Value = includeRetired;

        //SqlParameter positionTableValuedParameter = cmd.Parameters.AddWithValue("@PositionCodes", positionTable);
        //positionTableValuedParameter.SqlDbType = SqlDbType.Structured;

        cmd.Parameters.Add("@PositionCodes", SqlDbType.Structured).Value = positionTable;


        cmd.Parameters.Add("@StatCode", SqlDbType.VarChar).Value = statCode;

        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetPlayerCollectionFromReader(ExecuteReader(cmd));
      }
    }


    public override List<PlayerDetails> GetPlayerRookies(string sportCode, string seasonCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayerRookiesBySportSeasonCodes", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetPlayerCollectionFromReader(ExecuteReader(cmd));

      }
    }

    public override List<PlayerDetails> GetPlayerRookies(string sportCode, string seasonCode, string teamCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayerRookiesBySportSeasonTeamCodes", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@TeamCode", SqlDbType.NVarChar).Value = teamCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetPlayerCollectionFromReader(ExecuteReader(cmd));

      }
    }

    public override List<PlayerDetails> GetPlayerRookiesBySportSeasonPositionCodes(string sportCode, string seasonCode, string positionCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayerRookiesBySportSeasonPositionCodes", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.NChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NChar).Value = seasonCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NChar).Value = positionCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetPlayerCollectionFromReader(ExecuteReader(cmd));

      }
    }

    


    /// <summary>
    /// Inserts a player
    /// </summary>
    /// <param name="article"></param>
    /// <returns></returns>
    public override int InsertPlayer(PlayerDetails player)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertPlayer", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = player.SportCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = player.PositionCode;
        cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = player.FirstName;
        cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = player.LastName;
        cmd.Parameters.Add("@MiddleName", SqlDbType.NVarChar).Value = player.MiddleName;
        cmd.Parameters.Add("@TeamCode", SqlDbType.NVarChar).Value = player.TeamCode;
        cmd.Parameters.Add("@Number", SqlDbType.SmallInt).Value = player.Number;
        cmd.Parameters.Add("@FirstYear", SqlDbType.DateTime).Value = player.FirstYear;
        if (player.BirthDate == DateTime.MinValue)
        {
          cmd.Parameters.Add("@BirthDate", SqlDbType.Date).Value = DBNull.Value;
        }
        else
        {
          cmd.Parameters.Add("@BirthDate", SqlDbType.Date).Value = player.BirthDate;
        }
        cmd.Parameters.Add("@TwitterUsername", SqlDbType.NVarChar).Value = player.TwitterUsername;
        cmd.Parameters.Add("@StatMapID", SqlDbType.Int).Value = player.StatMapID;
        cmd.Parameters.Add("@Retired", SqlDbType.Bit).Value = player.Retired;
        // returns an ArticleID
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Direction = ParameterDirection.Output;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (int)cmd.Parameters["@PlayerID"].Value;
      }
    }

    /// <summary>
    /// Deletes a player with the specified id
    /// </summary>
    /// <param name="articleID"></param>
    /// <returns></returns>
    public override bool DeletePlayer(int playerID, ref List<CheatSheetDetails> cheatSheets, ref List<SupplementalSheetDetails> supplementalSheets)
    {

      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        cn.Open();
        SqlTransaction myTrans = cn.BeginTransaction();

        try
        {
          // remove the player from any cheat sheets (and adjust)
          for (int i = 0; i < cheatSheets.Count; i++)
          {
            SqlCommand cmd = new SqlCommand("Sheets_RemoveCheatSheetItem", cn, myTrans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheets[i].CheatSheetID;
            cmd.Parameters.Add("@PlayerID", SqlDbType.SmallInt).Value = playerID;
            int ret = ExecuteNonQuery(cmd);
          }

          // remove the player from any supplemental sheets (and adjust)
          for (int i = 0; i < supplementalSheets.Count; i++)
          {
            SqlCommand cmd2 = new SqlCommand("Sheets_RemoveSupplementalSheetItem", cn, myTrans);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("@SupplementalSheetID", SqlDbType.Int).Value = supplementalSheets[i].SupplementalSheetID;
            cmd2.Parameters.Add("@PlayerID", SqlDbType.Int).Value = playerID;
            int ret2 = ExecuteNonQuery(cmd2);
          }
          
          
          // delete the player
          SqlCommand cmd3 = new SqlCommand("Sheets_DeletePlayer", cn, myTrans);
          cmd3.CommandType = CommandType.StoredProcedure;
          cmd3.Parameters.Add("@PlayerID", SqlDbType.Int).Value = playerID;
          int ret3 = ExecuteNonQuery(cmd3);

          myTrans.Commit();
        }
        catch(Exception ex)
        {
          myTrans.Rollback();
        }
      }
      return true;
    
    }

    public override bool UpdatePlayer(PlayerDetails player)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_UpdatePlayer", cn);
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = player.PlayerID;
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = player.SportCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = player.PositionCode;
        cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = player.FirstName;
        cmd.Parameters.Add("@MiddleName", SqlDbType.NVarChar).Value = player.MiddleName;
        cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = player.LastName;
        cmd.Parameters.Add("@TeamCode", SqlDbType.NVarChar).Value = player.TeamCode;
        cmd.Parameters.Add("@Number", SqlDbType.Int).Value = player.Number;
        cmd.Parameters.Add("@FirstYear", SqlDbType.DateTime).Value = player.FirstYear;
        if (player.BirthDate == DateTime.MinValue)
        {
          cmd.Parameters.Add("@BirthDate", SqlDbType.Date).Value = DBNull.Value;
        }
        else
        {
          cmd.Parameters.Add("@BirthDate", SqlDbType.Date).Value = player.BirthDate;
        }
        cmd.Parameters.Add("@TwitterUsername", SqlDbType.NVarChar).Value = player.TwitterUsername;
        cmd.Parameters.Add("@StatMapID", SqlDbType.Int).Value = player.StatMapID;
        cmd.Parameters.Add("@Retired", SqlDbType.Bit).Value = player.Retired;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }


    public override PlayerDetails GetDefensivePlayer(string teamCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetDefensivePlayer", cn);
        cmd.Parameters.Add("@TeamCode", SqlDbType.VarChar).Value = teamCode;

        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetPlayerFromReader(reader);
        else
          return null;
      }
    }

    public override List<PlayerDetails> GetDefensivePlayers()
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetDefensivePlayers", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetPlayerCollectionFromReader(ExecuteReader(cmd));
      }
    }


    public override List<PlayerStatusCodeDetails> GetPlayerStatusCodes(string sportCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayerStatusCodesBySportCode", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetPlayerStatusCodeCollectionFromReader(ExecuteReader(cmd));
      }
    }

    public override PlayerStatusCodeDetails GetPlayerStatusCode(string statusCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetPlayerStatusCodeByStatusCode", cn);
        cmd.Parameters.Add("@StatusCode", SqlDbType.Char).Value = statusCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
        {
          return GetPlayerStatusCodeFromReader(reader);
        }
        else
        {
          return null;
        }
      }
    }

    // SportSeasonPlayerStatus Methods

    public override int InsertSportSeasonPlayerStatus(SportSeasonPlayerStatusDetails sportSeasonPlayerStatus)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertSportSeasonPlayerStatus", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportSeasonPlayerStatus.SportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.Char).Value = sportSeasonPlayerStatus.SeasonCode;
        cmd.Parameters.Add("@PlayerId", SqlDbType.Int).Value = sportSeasonPlayerStatus.PlayerId;
        cmd.Parameters.Add("@StatusCode", SqlDbType.Char).Value = sportSeasonPlayerStatus.StatusCode;
        cmd.Parameters.Add("@SuppInfo", SqlDbType.VarChar).Value = sportSeasonPlayerStatus.SuppInfo;

        if (sportSeasonPlayerStatus.Count != null)
        {
          cmd.Parameters.Add("@Count", SqlDbType.Int).Value = sportSeasonPlayerStatus.Count;
        }
        else
        {
          cmd.Parameters.Add("@Count", SqlDbType.Int).Value = DBNull.Value;
        }

        cmd.Parameters.Add("@Approved", SqlDbType.Bit).Value = sportSeasonPlayerStatus.Approved;
        cmd.Parameters.Add("@Archived", SqlDbType.Bit).Value = sportSeasonPlayerStatus.Archived;
        cmd.Parameters.Add("@CreatedByUsername", SqlDbType.VarChar).Value = sportSeasonPlayerStatus.CreatedByUsername;
        cmd.Parameters.Add("@CreatedTimestamp", SqlDbType.DateTime).Value = sportSeasonPlayerStatus.CreatedTimestamp;
        cmd.Parameters.Add("@ModifiedByUsername", SqlDbType.VarChar).Value = DBNull.Value;
        cmd.Parameters.Add("@ModifiedTimestamp", SqlDbType.DateTime).Value = DBNull.Value;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return ret;
      }
    }


    public override bool UpdateSportSeasonPlayerStatus(SportSeasonPlayerStatusDetails sportSeasonPlayerStatus)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_UpdateSportSeasonPlayerStatus", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportSeasonPlayerStatusId", SqlDbType.Int).Value = sportSeasonPlayerStatus.SportSeasonPlayerStatusId;
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportSeasonPlayerStatus.SportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.Char).Value = sportSeasonPlayerStatus.SeasonCode;
        cmd.Parameters.Add("@PlayerId", SqlDbType.Int).Value = sportSeasonPlayerStatus.PlayerId;
        cmd.Parameters.Add("@StatusCode", SqlDbType.Char).Value = sportSeasonPlayerStatus.StatusCode;
        cmd.Parameters.Add("@SuppInfo", SqlDbType.VarChar).Value = sportSeasonPlayerStatus.SuppInfo;

        if (sportSeasonPlayerStatus.Count != null)
        {
          cmd.Parameters.Add("@Count", SqlDbType.Int).Value = sportSeasonPlayerStatus.Count;
        }
        else
        {
          cmd.Parameters.Add("@Count", SqlDbType.Int).Value = DBNull.Value;
        }

        cmd.Parameters.Add("@Approved", SqlDbType.Bit).Value = sportSeasonPlayerStatus.Approved;
        cmd.Parameters.Add("@Archived", SqlDbType.Bit).Value = sportSeasonPlayerStatus.Archived;
        cmd.Parameters.Add("@CreatedByUsername", SqlDbType.VarChar).Value = sportSeasonPlayerStatus.CreatedByUsername;
        cmd.Parameters.Add("@CreatedTimestamp", SqlDbType.DateTime).Value = sportSeasonPlayerStatus.CreatedTimestamp;
        cmd.Parameters.Add("@ModifiedByUsername", SqlDbType.VarChar).Value = sportSeasonPlayerStatus.ModifiedByUsername;
        cmd.Parameters.Add("@ModifiedTimestamp", SqlDbType.DateTime).Value = DateTime.Now;

        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }

    public override List<SportSeasonPlayerStatusDetails> GetSportSeasonPlayerStatuses(string seasonCode, string sportCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSportSeasonPlayerStatusesBySportSeasonCodes", cn);
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NChar).Value = seasonCode;
        cmd.Parameters.Add("@SportCode", SqlDbType.NChar).Value = sportCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetSportSeasonPlayerStatusCollectionFromReader(ExecuteReader(cmd));
      }
    }






    // Supplemental Source Methods

    public override SupplementalSourceDetails GetSupplementalSource(int supplementalSourceID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSupplementalSourceByID", cn);
        cmd.Parameters.Add("@SupplementalSourceID", SqlDbType.Int).Value = supplementalSourceID;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetSupplementalSourceFromReader(reader);
        else
          return null;
      }
    }

    // Supplemental Source Methods

    public override SupplementalSourceDetails GetSupplementalSource(string abbreviation)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSupplementalSourceByAbbreviation", cn);
        cmd.Parameters.Add("@Abbreviation", SqlDbType.NVarChar).Value = abbreviation;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetSupplementalSourceFromReader(reader);
        else
          return null;
      }
    }



    public override List<SupplementalSourceDetails> GetSupplementalSources()
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSupplementalSources", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetSupplementalSourceCollectionFromReader(ExecuteReader(cmd));
      }
    }

    public override List<SupplementalSourceDetails> GetSupplementalSources(string seasonCode, string sportCode, string positionCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSupplementalSourcesBySeasonSportPositionCodes", cn);
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NChar).Value = seasonCode;
        cmd.Parameters.Add("@SportCode", SqlDbType.NChar).Value = sportCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NChar).Value = positionCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetSupplementalSourceCollectionFromReader(ExecuteReader(cmd));
      }
    }




    /// <summary>
    /// Inserts a supplemental source
    /// </summary>
    /// <param name="article"></param>
    /// <returns></returns>
    public override int InsertSupplementalSource(SupplementalSourceDetails supplementalSource)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertSupplementalSource", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Abbreviation", SqlDbType.NVarChar).Value = supplementalSource.Abbreviation;
        cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = supplementalSource.Name;
        cmd.Parameters.Add("@URL", SqlDbType.NVarChar).Value = supplementalSource.Url;
        cmd.Parameters.Add("@ImageUrl", SqlDbType.NVarChar).Value = supplementalSource.ImageUrl;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return ret;
      }
    }

    /// <summary>
    /// Deletes a supplemental source with the specified id
    /// </summary>
    /// <param name="articleID"></param>
    /// <returns></returns>
    public override bool DeleteSupplementalSource(int supplementalSourceID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_DeleteSupplementalSource", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SupplementalSourceID", SqlDbType.Int).Value = supplementalSourceID;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }

    public override bool UpdateSupplementalSource(SupplementalSourceDetails supplementalSource)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_UpdateSupplementalSource", cn);
        cmd.Parameters.Add("@SupplementalSourceID", SqlDbType.Int).Value = supplementalSource.SupplementalSourceID;
        cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = supplementalSource.Name;
        cmd.Parameters.Add("@Abbreviation", SqlDbType.NVarChar).Value = supplementalSource.Abbreviation;
        cmd.Parameters.Add("@Url", SqlDbType.NVarChar).Value = supplementalSource.Url;
        cmd.Parameters.Add("@ImageUrl", SqlDbType.NVarChar).Value = supplementalSource.ImageUrl;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }

    public override SupplementalSheetDetails GetSupplementalSheet(int supplementalSheetID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSupplementalSheetByID", cn);
        cmd.Parameters.Add("@SupplementalSheetID", SqlDbType.Int).Value = supplementalSheetID;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetSupplementalSheetFromReader(reader);
        else
          return null;
      }
    }


    public override SupplementalSheetDetails GetSupplementalSheet(string seasonCode, int supplementalSourceID, string sportCode, string positionCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSupplementalSheetBySourceIDSeasonSportPositionCodes", cn);
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@SupplementalSourceID", SqlDbType.Int).Value = supplementalSourceID;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = positionCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetSupplementalSheetFromReader(reader);
        else
          return null;
      }
    }
    
    public override List<SupplementalSheetDetails> GetSupplementalSheets(string sportCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSupplementalSheetsBySportCode", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.NChar).Value = sportCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetSupplementalSheetCollectionFromReader(ExecuteReader(cmd));
      }
    }

    public override List<SupplementalSheetDetails> GetSupplementalSheets(string seasonCode, string sportCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSupplementalSheetsBySeasonSportCodes", cn);
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NChar).Value = seasonCode;
        cmd.Parameters.Add("@SportCode", SqlDbType.NChar).Value = sportCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetSupplementalSheetCollectionFromReader(ExecuteReader(cmd));
      }
    }


    public override bool ReorderSupplementalSheetItems(int supplementalSheetID, int oldIndex, int newIndex)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_ReorderSupplementalSheetItems", cn);
        cmd.Parameters.Add("@SupplementalSheetID", SqlDbType.Int).Value = supplementalSheetID;
        cmd.Parameters.Add("@OldIndex", SqlDbType.Int).Value = oldIndex;
        cmd.Parameters.Add("@NewIndex", SqlDbType.Int).Value = newIndex;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret > 0);
      }
    }

    public override bool UpdateSupplementalSheet(SupplementalSheetDetails supplementalSheet)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_UpdateSupplementalSheet", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SupplementalSheetID", SqlDbType.Int).Value = supplementalSheet.SupplementalSheetID;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.VarChar).Value = supplementalSheet.SeasonCode;
        cmd.Parameters.Add("@SupplementalSourceID", SqlDbType.Int).Value = supplementalSheet.SupplementalSourceID;
        cmd.Parameters.Add("@SportCode", SqlDbType.VarChar).Value = supplementalSheet.SportCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.VarChar).Value = supplementalSheet.PositionCode;
        cmd.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = supplementalSheet.LastUpdated;
        cmd.Parameters.Add("Url", SqlDbType.VarChar).Value = supplementalSheet.Url;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }


    public override bool CreateSupplementalSheet(SupplementalSheetDetails supplementalSheet)
    {

      
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        cn.Open();
        SqlTransaction myTrans = cn.BeginTransaction();

        try
        {
          /* Insert the supplemental sheet first */
          SqlCommand cmd = new SqlCommand("Sheets_InsertSupplementalSheet", cn, myTrans);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.Add("@SupplementalSourceID", SqlDbType.Int).Value = supplementalSheet.SupplementalSourceID;
          cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = supplementalSheet.SeasonCode;
          cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = supplementalSheet.SportCode;
          cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = supplementalSheet.PositionCode;
          cmd.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = supplementalSheet.LastUpdated;
          cmd.Parameters.Add("@Url", SqlDbType.VarChar).Value = supplementalSheet.Url;
          // returns an SupplemeentalSheetID
          cmd.Parameters.Add("@SupplementalSheetID", SqlDbType.Int).Direction = ParameterDirection.Output;

          int ret = ExecuteNonQuery(cmd);
          int newSupplementalSheetID = (int)cmd.Parameters["@SupplementalSheetID"].Value;

          /* if we made it this far, we successfully created the sheet itself, now we need to add players 
             we only want to include players who recorded a stat the previous year, or rookies */

          // we only want to consider stats from the previous year
          int statSeason = int.Parse(supplementalSheet.SeasonCode) - 1;

          // the stat we key off-of dependson the sport (there should really be a settings for this)
          List<PlayerDetails> suppSheetPlayers = new List<PlayerDetails>();

          // we create new supplemental sheets differently based on the sport
          switch (supplementalSheet.SportCode)
          {
            case "FOO":
              // get only players who recorded a TFP the previous year
              List<PlayerDetails> previousYearPlayers = GetPlayers(supplementalSheet.SportCode, statSeason.ToString(), supplementalSheet.PositionCode, false, "TFP", "DESC");
              // get rookies for the current year
              List<PlayerDetails> currentYearRookies = GetPlayerRookiesBySportSeasonPositionCodes(supplementalSheet.SportCode, supplementalSheet.SeasonCode, supplementalSheet.PositionCode);
              // combine the two lists
              suppSheetPlayers.AddRange(previousYearPlayers);
              suppSheetPlayers.AddRange(currentYearRookies);
              break;
            case "RAC":
              // get all players initially
              List<PlayerDetails> currentYearPlayers = GetPlayers(supplementalSheet.SportCode, statSeason.ToString(), "DR", true, "PNTS", "DESC");
              suppSheetPlayers.AddRange(currentYearPlayers);

              break;
          }


          for (int i = 0; i < suppSheetPlayers.Count; i++)
          {
            SqlCommand cmd2 = new SqlCommand("Sheets_InsertSupplementalSheetItem", cn, myTrans);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("@SupplementalSheetID", SqlDbType.Int).Value = newSupplementalSheetID;
            cmd2.Parameters.Add("@PlayerID", SqlDbType.Int).Value = suppSheetPlayers[i].PlayerID;
            cmd2.Parameters.Add("@Seqno", SqlDbType.Int).Value = i+1;
            cmd2.Parameters.Add("@SleeperTag", SqlDbType.Bit).Value = false;
            cmd2.Parameters.Add("@BustTag", SqlDbType.Bit).Value = false;
            cmd2.Parameters.Add("@Note", SqlDbType.NVarChar).Value = String.Empty;
            ret = ExecuteNonQuery(cmd2);
          }

          myTrans.Commit();
          return true;
        }
        catch
        {
          myTrans.Rollback();
        }
      }
      return true;
    }



    /// <summary>
    /// Deletes a supplemental sheet with the specified id
    /// </summary>
    /// <param name="articleID"></param>
    /// <returns></returns>
    public override bool DeleteSupplementalSheet(int supplementalSheetID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_DeleteSupplementalSheet", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SupplementalSheetID", SqlDbType.Int).Value = supplementalSheetID;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }

    /// <summary>
    /// Deletes a supplemental sheet with the specified id
    /// </summary>
    /// <param name="articleID"></param>
    /// <returns></returns>
    public override bool RemoveAllSupplementalSheetItems(int supplementalSheetID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_ClearSupplementalSheetItems", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SupplementalSheetID", SqlDbType.Int).Value = supplementalSheetID;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }



    // Supplemental Sheet Player Methods

    public override List<SupplementalSheetItemDetails> GetSupplementalSheetItems(int supplementalSheetID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSupplementalSheetItemsBySheetID", cn);
        cmd.Parameters.Add("@SupplementalSheetID", SqlDbType.Int).Value = supplementalSheetID;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetSupplementalSheetItemCollectionFromReader(ExecuteReader(cmd));
      }
    }


    public override SupplementalSheetItemDetails GetSupplementalSheetItem(int suppSheetID, int playerID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSupplementalSheetItemBySheetIDPlayerID", cn);
        cmd.Parameters.Add("@SupplementalSheetID", SqlDbType.Int).Value = suppSheetID;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = playerID;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetSupplementalSheetItemFromReader(reader);
        else
          return null;
      }
    }

    //public override bool UpdateSupplementalSheetItemNote(int supplementalSheetID, int playerID, string note)
    //{
    //  using (SqlConnection cn = new SqlConnection(this.ConnectionString))
    //  {
    //    SqlCommand cmd = new SqlCommand("Sheets_UpdateSupplementalSheetItemNote", cn);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.Add("@SupplementalSheetID", SqlDbType.Int).Value = supplementalSheetID;
    //    cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = playerID;
    //    cmd.Parameters.Add("@Note", SqlDbType.NVarChar).Value = note;
    //    cn.Open();
    //    int ret = ExecuteNonQuery(cmd);
    //    return (ret == 1);
    //  }
    //}

    public override bool UpdateSupplementalSheetItem(SupplementalSheetItemDetails supplementalSheetItem)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_UpdateSupplementalSheetItem", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SupplementalSheetID", SqlDbType.Int).Value = supplementalSheetItem.SupplementalSheetID;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = supplementalSheetItem.PlayerID;
        cmd.Parameters.Add("@Seqno", SqlDbType.SmallInt).Value = supplementalSheetItem.Seqno;
        cmd.Parameters.Add("@SleeperTag", SqlDbType.Bit).Value = supplementalSheetItem.SleeperTag;
        cmd.Parameters.Add("@BustTag", SqlDbType.Bit).Value = supplementalSheetItem.BustTag;
        cmd.Parameters.Add("@Note", SqlDbType.VarChar).Value = supplementalSheetItem.Note;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }


    public override bool RemoveSupplementalSheetItem(SupplementalSheetItemDetails supplementalSheetItem)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_RemoveSupplementalSheetItem", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SupplementalSheetID", SqlDbType.Int).Value = supplementalSheetItem.SupplementalSheetID;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = supplementalSheetItem.PlayerID;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }

    public override bool AddSupplementalSheetItem(SupplementalSheetItemDetails supplementalSheetItem)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_AddSupplementalSheetItem", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SupplementalSheetID", SqlDbType.Int).Value = supplementalSheetItem.SupplementalSheetID;
        cmd.Parameters.Add("@PlayerID", SqlDbType.SmallInt).Value = supplementalSheetItem.PlayerID;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }


    public override int InsertSupplementalSheetItem(SupplementalSheetItemDetails supplementalSheetItem)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertSupplementalSheetItem", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SupplementalSheetID", SqlDbType.Int).Value = supplementalSheetItem.SupplementalSheetID;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = supplementalSheetItem.PlayerID;
        cmd.Parameters.Add("@Seqno", SqlDbType.Int).Value = supplementalSheetItem.Seqno;
        // Sleeper
        if (supplementalSheetItem.SleeperTag != null)
        {
          cmd.Parameters.Add("@SleeperTag", SqlDbType.Bit).Value = supplementalSheetItem.SleeperTag;
        }
        else
        {
          cmd.Parameters.Add("@SleeperTag", SqlDbType.Bit).Value = DBNull.Value;
        }
        // Bust
        if (supplementalSheetItem.BustTag != null)
        {
          cmd.Parameters.Add("@BustTag", SqlDbType.Bit).Value = supplementalSheetItem.BustTag;
        }
        else
        {
          cmd.Parameters.Add("@BustTag", SqlDbType.Bit).Value = DBNull.Value;
        }
        cmd.Parameters.Add("@Note", SqlDbType.VarChar).Value = supplementalSheetItem.Note;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return ret;
      }
    }


    // Stat Methods


    public override StatDetails GetStat(string statCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetStatByStatCode", cn);
        cmd.Parameters.Add("@StatCode", SqlDbType.NVarChar).Value = statCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetStatFromReader(reader);
        else
          return null;
      }
    }

    public override List<StatDetails> GetStats(string sportCode, string positionCode)
    {
      using(SqlConnection cn = new SqlConnection(this.ConnectionString))  
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetStatsBySportCodePositionCode",cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = positionCode;
        cn.Open();
        return GetStatCollectionFromReader(ExecuteReader(cmd));
      }
    }




    // Player Weekly Stat Methods
    public override int GetSportSeasonPlayerWeeklyStatCount(string sportCode, string seasonCode, int week)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSportSeasonPlayerWeeklyStatCount", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@Week", SqlDbType.TinyInt).Value = week;
        cn.Open();
        return (int)ExecuteScalar(cmd);
      }
    }

    public override int InsertSportSeasonPlayerWeeklyStat(SportSeasonPlayerWeeklyStatDetails sportSeasonPlayerWeeklyStat)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertSportSeasonPlayerWeeklyStat", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportSeasonPlayerWeeklyStat.SportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = sportSeasonPlayerWeeklyStat.SeasonCode;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = sportSeasonPlayerWeeklyStat.PlayerID;
        cmd.Parameters.Add("@Week", SqlDbType.Int).Value = sportSeasonPlayerWeeklyStat.Week;
        cmd.Parameters.Add("@StatCode", SqlDbType.NVarChar).Value = sportSeasonPlayerWeeklyStat.StatCode;
        cmd.Parameters.Add("@StatValue", SqlDbType.Float).Value = sportSeasonPlayerWeeklyStat.StatValue;

        cn.Open();
        return ExecuteNonQuery(cmd);
      }
    }


    /// <summary>
    /// Deletes the season stats for a player of a particular sport and season
    /// </summary>
    /// <param name="articleID"></param>
    /// <returns></returns>
    public override bool DeleteSportSeasonPlayerWeeklyStats(string sportCode, string seasonCode, int week, int playerID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_DeleteSportSeasonPlayerWeeklyStatsByPlayerID", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@Week", SqlDbType.Int).Value = week;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = playerID;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }


    /// <summary>
    /// Deletes the season stats for a player of a particular sport and season
    /// </summary>
    /// <param name="articleID"></param>
    /// <returns></returns>
    public override bool DeleteSportSeasonPlayerWeeklyStats(string sportCode, string seasonCode, int week, string positionCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_DeleteSportSeasonPlayerWeeklyStatsByPositionCode", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@Week", SqlDbType.Int).Value = week;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = positionCode;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }


    // Sport Season Player Season Methods
    public override int InsertSportSeasonPlayerSeasonStat(SportSeasonPlayerSeasonStatDetails sportSeasonPlayerSeasonStat)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertSportSeasonPlayerSeasonStat", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportSeasonPlayerSeasonStat.SportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = sportSeasonPlayerSeasonStat.SeasonCode;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = sportSeasonPlayerSeasonStat.PlayerID;
        cmd.Parameters.Add("@StatCode", SqlDbType.NVarChar).Value = sportSeasonPlayerSeasonStat.StatCode;
        cmd.Parameters.Add("@StatValue", SqlDbType.Float).Value = sportSeasonPlayerSeasonStat.StatValue;

        cn.Open();
        return ExecuteNonQuery(cmd);
      }
    }

    public override List<SportSeasonPlayerSeasonStatDetails> GetSportSeasonPlayerSeasonStats(string sportCode, string seasonCode, int playerID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSportSeasonPlayerSeasonStatsBySportSeasonPlayerID", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = playerID;
        cn.Open();
        return GetSportSeasonPlayerSeasonStatCollectionFromReader(ExecuteReader(cmd));
      }
    }


    public override List<SportSeasonPlayerSeasonStatDetails> GetSportSeasonPlayerSeasonStats(string sportCode, string seasonCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSportSeasonPlayerSeasonStatsBySportSeasonCodes", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cn.Open();
        return GetSportSeasonPlayerSeasonStatCollectionFromReader(ExecuteReader(cmd));
      }
    }


    public override List<SportSeasonPlayerSeasonStatDetails> GetSportSeasonPlayerSeasonStats(string sportCode, string seasonCode, string positionCode, string statCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSportSeasonPlayerSeasonStatsBySportSeasonPositionStatCodes", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = positionCode;
        cmd.Parameters.Add("@StatCode", SqlDbType.NVarChar).Value = statCode;
        cn.Open();
        return GetSportSeasonPlayerSeasonStatCollectionFromReader(ExecuteReader(cmd));
      }
    }




    public override SportSeasonPlayerSeasonStatDetails GetSportSeasonPlayerSeasonStat(string sportCode, string seasonCode, int playerID, string statCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSportSeasonPlayerSeasonStatBySportSeasonPlayerStatCodes", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@PlayerID", SqlDbType.NVarChar).Value = playerID;
        cmd.Parameters.Add("@StatCode", SqlDbType.NVarChar).Value = statCode;

        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetSportSeasonPlayerSeasonStatFromReader(reader);
        else
          return null;
      }
    }


    public override SportSeasonPlayerTeamDetails GetSportSeasonPlayerTeam(string sportCode, string seasonCode, int playerID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSportSeasonPlayerTeamBySportSeasonPlayerID", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@PlayerID", SqlDbType.NVarChar).Value = playerID;

        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetSportSeasonPlayerTeamFromReader(reader);
        else
          return null;
      }
    }

    public override int InsertSportSeasonPlayerTeam(SportSeasonPlayerTeamDetails sportSeasonPlayerTeam)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertSportSeasonPlayerTeam", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportSeasonPlayerTeam.SportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = sportSeasonPlayerTeam.SeasonCode;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = sportSeasonPlayerTeam.PlayerID;
        cmd.Parameters.Add("@TeamCode", SqlDbType.NVarChar).Value = sportSeasonPlayerTeam.TeamCode;

        cn.Open();
        return ExecuteNonQuery(cmd);
      }
    }


    public override bool UpdateSportSeasonPlayerSeasonStat(SportSeasonPlayerSeasonStatDetails sportSeasonPlayerSeasonStat)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_UpdateSportSeasonPlayerSeasonStat", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportSeasonPlayerSeasonStat.SportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = sportSeasonPlayerSeasonStat.SeasonCode;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = sportSeasonPlayerSeasonStat.PlayerID;
        cmd.Parameters.Add("@StatCode", SqlDbType.NVarChar).Value = sportSeasonPlayerSeasonStat.StatCode;
        cmd.Parameters.Add("@StatValue", SqlDbType.Float).Value = sportSeasonPlayerSeasonStat.StatValue;

        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }

    /// <summary>
    /// Deletes the season stats for a player of a particular sport and season
    /// </summary>
    /// <param name="articleID"></param>
    /// <returns></returns>
    public override bool DeleteSportSeasonPlayerSeasonStats(string sportCode, string seasonCode, int playerID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_DeleteSportSeasonPlayerSeasonStatsByPlayerID", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = playerID;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }

    /// <summary>
    /// Deletes the season stats for a player of a particular sport and season
    /// </summary>
    /// <param name="articleID"></param>
    /// <returns></returns>
    public override bool DeleteSportSeasonPlayerSeasonStats(string sportCode, string seasonCode, string positionCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_DeleteSportSeasonPlayerSeasonStatsByPositionCode", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = positionCode;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }


    // Bye Week Methods

    public override ByeWeekDetails GetByeWeek(string seasonCode, string sportCode, string teamCode)
    {
      using(SqlConnection cn = new SqlConnection(this.ConnectionString)) 
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetByeWeekBySeasonCodeSportCodeTeamCodes", cn);
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@TeamCode", SqlDbType.NVarChar).Value = teamCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetByeWeekFromReader(reader);
        else
          return null;
      }
    }

    public override List<ByeWeekDetails> GetByeWeeks(string seasonCode, string sportCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetByeWeeksBySeasonCodeSportCode", cn);
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetByeWeekCollectionFromReader(ExecuteReader(cmd));
      }
    }

    // Season Methods

    public override List<SeasonDetails> GetSeasons()
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSeasons", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetSeasonCollectionFromReader(ExecuteReader(cmd));
      }
    }

    public override SeasonDetails GetCurrentSeason()
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetCurrentSeason", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetSeasonFromReader(reader);
        else
          return null;
      }
    }




    // Cheat Sheet Stat Methods

    public override List<CheatSheetStatDetails> GetCheatSheetStats(int cheatSheetID, string sportCode, string positionCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetCheatSheetStatsBySheetIDSportPositionCodes", cn);
        cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheetID;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = positionCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetCheatSheetStatCollectionFromReader(ExecuteReader(cmd));
      }
    }

    // Cheat Sheet Position Methods

    public override List<CheatSheetPositionDetails> GetCheatSheetPositions(int cheatSheetID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetCheatSheetPositionsBySheetID", cn);
        cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheetID;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetCheatSheetPositionCollectionFromReader(ExecuteReader(cmd));
      }
    }

    // Cheat Sheet Methods

    public override CheatSheetDetails GetCheatSheet(int cheatSheetID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetCheatSheetBySheetID", cn);
        cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheetID;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetCheatSheetFromReader(reader);
        else
          return null;
      }
    }

    //public override CheatSheetDetails GetVisitorCheatSheet(string visitorID, string positionCode)
    //{
    //  using (SqlConnection cn = new SqlConnection(this.ConnectionString))
    //  {
    //    SqlCommand cmd = new SqlCommand("Sheets_GetVisitorCheatSheetByVisitorIDPositionCode", cn);
    //    cmd.Parameters.Add("@VisitorID", SqlDbType.NVarChar).Value = visitorID;
    //    cmd.Parameters.Add("@PositionCode", SqlDbType.NChar).Value = positionCode;
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cn.Open();
    //    IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
    //    if (reader.Read())
    //      return GetCheatSheetFromReader(reader);
    //    else
    //      return null;
    //  }
    //}

    public override List<CheatSheetDetails> GetUserCheatSheets(string userName)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetCheatSheetsByUsername", cn);
        cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = userName;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetCheatSheetCollectionFromReader(ExecuteReader(cmd));
      }
    }

    public override List<CheatSheetDetails> GetUserCheatSheets(string userName, string sportCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetCheatSheetsByUsernameSport", cn);
        cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = userName;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetCheatSheetCollectionFromReader(ExecuteReader(cmd));
      }
    }

    public override List<CheatSheetDetails> GetUserCheatSheets(string userName, string sportCode, string positionCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetCheatSheetsByUsernameSportPositionCodes", cn);
        cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = userName;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = positionCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetCheatSheetCollectionFromReader(ExecuteReader(cmd));
      }
    }



    public override List<CheatSheetDetails> GetCheatSheets(string sportCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetCheatSheetsBySportCode", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetCheatSheetCollectionFromReader(ExecuteReader(cmd));
      }
    }

    public override List<CheatSheetDetails> GetCheatSheets(string sportCode, string seasonCode, string positionCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetCheatSheetsBySportSeasonPositionCodes", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = positionCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetCheatSheetCollectionFromReader(ExecuteReader(cmd));
      }
    }


    public override List<ArchivedCheatSheetDetails> GetArchivedCheatSheets(string sportCode, string seasonCode, string positionCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetArchivedCheatSheetsBySportSeasonPositionCodes", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = positionCode;

        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetArchivedCheatSheetCollectionFromReader(ExecuteReader(cmd));
      }
    }

    public override ArchivedCheatSheetDetails GetArchivedCheatSheet(int archivedCheatSheetId)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetArchivedCheatSheetByID", cn);
        cmd.Parameters.Add("@ArchivedCheatSheetID", SqlDbType.Int).Value = archivedCheatSheetId;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetArchivedCheatSheetFromReader(reader);
        else
          return null;
      }
    }



    // Archived Cheat Sheet Item Methods

    public override List<ArchivedCheatSheetItemDetails> GetArchivedCheatSheetItems(int archivedCheatSheetID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetArchivedCheatSheetItemsBySheetID", cn);
        cmd.Parameters.Add("@ArchivedCheatSheetID", SqlDbType.Int).Value = archivedCheatSheetID;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        List<ArchivedCheatSheetItemDetails> items = GetArchivedCheatSheetItemCollectionFromReader(ExecuteReader(cmd));
        return items;
      }
    }


    public override bool RemoveArchivedCheatSheetItem(ArchivedCheatSheetItemDetails cheatSheetItem)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_RemoveArchivedCheatSheetItem", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ArchivedCheatSheetID", SqlDbType.Int).Value = cheatSheetItem.ArchivedCheatSheetID;
        cmd.Parameters.Add("@PlayerID", SqlDbType.SmallInt).Value = cheatSheetItem.PlayerID;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }


    public override int InsertArchivedCheatSheetItem(ArchivedCheatSheetItemDetails archivedCheatSheetItem)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertArchivedCheatSheetItem", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ArchivedCheatSheetID", SqlDbType.Int).Value = archivedCheatSheetItem.ArchivedCheatSheetID;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = archivedCheatSheetItem.PlayerID;
        cmd.Parameters.Add("@Seqno", SqlDbType.SmallInt).Value = archivedCheatSheetItem.Seqno;
        cmd.Parameters.Add("@SleeperTag", SqlDbType.Bit).Value = archivedCheatSheetItem.SleeperTag;
        cmd.Parameters.Add("@BustTag", SqlDbType.Bit).Value = archivedCheatSheetItem.BustTag;
        cmd.Parameters.Add("@InjuredTag", SqlDbType.Bit).Value = archivedCheatSheetItem.InjuredTag;
        cmd.Parameters.Add("@Note", SqlDbType.NVarChar).Value = archivedCheatSheetItem.Note;

        cn.Open();
        return ExecuteNonQuery(cmd);
      }
    }



    // Cheat Sheet Position Grades
    /// <summary>
    /// Inserts a cheat sheet
    /// </summary>
    /// <param name="article"></param>
    /// <returns></returns>
    public override int InsertUserSheetPositionGrade(UserSheetPositionGradeDetails sheetGrade)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertUserSheetPositionGrade", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NChar).Value = sheetGrade.SeasonCode;
        cmd.Parameters.Add("@SportCode", SqlDbType.NChar).Value = sheetGrade.SportCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NChar).Value = sheetGrade.PositionCode;
        cmd.Parameters.Add("@ArchivedCheatSheetID", SqlDbType.Int).Value = sheetGrade.ArchivedCheatSheetID;
        cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = sheetGrade.UserName;
        cmd.Parameters.Add("@TotalRankDifferential", SqlDbType.Int).Value = sheetGrade.TotalRankDifferential;
        cmd.Parameters.Add("@Score", SqlDbType.SmallInt).Value = sheetGrade.Score;
        cmd.Parameters.Add("@Rank", SqlDbType.Int).Value = sheetGrade.Rank;
        // returns an ArticleID
        cmd.Parameters.Add("@UserSheetPositionGradeID", SqlDbType.Int).Direction = ParameterDirection.Output;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (int)cmd.Parameters["@UserSheetPositionGradeID"].Value;
      }
    }


    public override List<UserSheetPositionGradeDetails> GetUserSheetPositionGrades(string sportCode, string seasonCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetUserSheetPositionGradesBySeasonSportCodes", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.NChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NChar).Value = seasonCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetUserSheetPositionGradeCollectionFromReader(ExecuteReader(cmd));
      }
    }


    public override UserSheetPositionGradeDetails GetUserSheetPositionGrade(string userName, string seasonCode, string positionCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetUserSheetPositionGradeByUsernameSeasonPositionCodes", cn);
        cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userName;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NChar).Value = seasonCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NChar).Value = positionCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetUserSheetPositionGradeFromReader(reader);
        else
          return null;
      }
    }

    public override int GetUserSheetPositionGradesCount(string seasonCode, string sportCode, string positionCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetUserSheetPositionGradesCountBySeasonSportPositionCodes", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NChar).Value = positionCode;
        cn.Open();
        return (int)ExecuteScalar(cmd);
      }
    }



    public override int InsertUserSheetPlayerDifferential(UserSheetPlayerDifferentialDetails userSheetPlayerDifferential)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertUserSheetPlayerDifferential", cn);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@SeasonCode", SqlDbType.NChar).Value = userSheetPlayerDifferential.SeasonCode;
        cmd.Parameters.Add("@SportCode", SqlDbType.NChar).Value = userSheetPlayerDifferential.SportCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NChar).Value = userSheetPlayerDifferential.PositionCode;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = userSheetPlayerDifferential.PlayerID;
        cmd.Parameters.Add("@AverageDifferential", SqlDbType.Decimal).Value = userSheetPlayerDifferential.AverageDifferential;

        // returns an ArticleID
        cmd.Parameters.Add("@UserSheetPlayerDifferentialID", SqlDbType.Int).Direction = ParameterDirection.Output;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (int)cmd.Parameters["@UserSheetPlayerDifferentialID"].Value;
      }
    }


    public override List<UserSheetPlayerDifferentialDetails> GetUserSheetPlayerDifferentials(string sportCode, string seasonCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetUserSheetPlayerDifferentialsBySeasonSportCodes", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetUserSheetPlayerDifferentialCollectionFromReader(ExecuteReader(cmd));
      }
    }

    public override List<string> GetLeaderboardYears(string sportCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        var leaderboardYears = new List<string>();

        SqlCommand cmd = new SqlCommand("Sheets_GetLeaderboardYearsBySportCode", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();

        var reader = ExecuteReader(cmd);
        while (reader.Read())
        {
          leaderboardYears.Add(reader[0].ToString());
        }
        return leaderboardYears;
      }
    }


    // Cheat Sheet Position Grades
    /// <summary>
    /// Inserts a cheat sheet
    /// </summary>
    /// <param name="article"></param>
    /// <returns></returns>
    public override int InsertUserSportSeasonLeaderboard(UserSportSeasonLeaderboardDetails userSportSeasonLeaderboard)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertUserSportSeasonLeaderboard", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NChar).Value = userSportSeasonLeaderboard.SeasonCode;
        cmd.Parameters.Add("@SportCode", SqlDbType.NChar).Value = userSportSeasonLeaderboard.SportCode;
        cmd.Parameters.Add("@Username", SqlDbType.NChar).Value = userSportSeasonLeaderboard.Username;
        cmd.Parameters.Add("@QBScore", SqlDbType.Int).Value = userSportSeasonLeaderboard.QBScore;
        cmd.Parameters.Add("@RBScore", SqlDbType.Int).Value = userSportSeasonLeaderboard.RBScore;
        cmd.Parameters.Add("@WRScore", SqlDbType.Int).Value = userSportSeasonLeaderboard.WRScore;
        cmd.Parameters.Add("@TEScore", SqlDbType.Int).Value = userSportSeasonLeaderboard.TEScore;
        cmd.Parameters.Add("@KScore", SqlDbType.Int).Value = userSportSeasonLeaderboard.KScore;
        cmd.Parameters.Add("@DFScore", SqlDbType.Int).Value = userSportSeasonLeaderboard.DFScore;
        cmd.Parameters.Add("@OverallScore", SqlDbType.Int).Value = userSportSeasonLeaderboard.OverallScore;
        cmd.Parameters.Add("@Rank", SqlDbType.Int).Value = userSportSeasonLeaderboard.Rank;
        // returns an ArticleID
        cmd.Parameters.Add("@UserSportSeasonLeaderboardID", SqlDbType.Int).Direction = ParameterDirection.Output;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (int)cmd.Parameters["@UserSportSeasonLeaderboardID"].Value;
      }
    }



    // Archived Cheat Sheet Item Methods

    public override List<UserSportSeasonLeaderboardDetails> GetUserSportSeasonLeaderboards(string sportCode, string seasonCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetUserSportSeasonLeaderboards", cn);
        
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;

        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        List<UserSportSeasonLeaderboardDetails> items = GetUserSportSeasonLeaderboardCollectionFromReader(ExecuteReader(cmd));
        return items;
      }
    }



    public override bool RemoveCheatSheetItem(CheatSheetItemDetails cheatSheetItem)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_RemoveCheatSheetItem", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheetItem.CheatSheetID;
        cmd.Parameters.Add("@PlayerID", SqlDbType.SmallInt).Value = cheatSheetItem.PlayerID;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }

    public override bool RemoveAllCheatSheetItems(int cheatSheetID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_RemoveAllCheatSheetItems", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheetID;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret >= 1);
      }
    }



    public override bool AddCheatSheetItem(CheatSheetItemDetails cheatSheetItem)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_AddCheatSheetItem", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheetItem.CheatSheetID;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = cheatSheetItem.PlayerID;
        cmd.Parameters.Add("@Note", SqlDbType.VarChar).Value = cheatSheetItem.Note;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }



    /// <summary>
    /// Inserts a cheat sheet
    /// </summary>
    /// <param name="article"></param>
    /// <returns></returns>
    public override int InsertCheatSheet(CheatSheetDetails cheatSheet)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertCheatSheet", cn);

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = cheatSheet.SeasonCode;
        cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = cheatSheet.Username.Trim();
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = cheatSheet.SportCode;
        cmd.Parameters.Add("@SheetName", SqlDbType.NVarChar).Value = cheatSheet.SheetName;
        cmd.Parameters.Add("@StatsSeasonCode", SqlDbType.NVarChar).Value = cheatSheet.StatsSeasonCode;
        cmd.Parameters.Add("@Created", SqlDbType.DateTime).Value = cheatSheet.Created;
        cmd.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = cheatSheet.LastUpdated;
        // returns an ArticleID
        cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Direction = ParameterDirection.Output;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (int)cmd.Parameters["@CheatSheetID"].Value;
      }
    }

    /// <summary>
    /// Inserts a cheat sheet
    /// </summary>
    /// <param name="article"></param>
    /// <returns></returns>
    public override int InsertCheatSheet(CheatSheetDetails cheatSheet, SqlConnection cn, SqlTransaction transaction)
    {
      SqlCommand cmd = new SqlCommand("Sheets_InsertCheatSheet", cn, transaction);

      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = cheatSheet.SeasonCode;
      cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = cheatSheet.Username;
      cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = cheatSheet.SportCode;
      cmd.Parameters.Add("@SheetName", SqlDbType.NVarChar).Value = cheatSheet.SheetName;
      cmd.Parameters.Add("@StatsSeasonCode", SqlDbType.NVarChar).Value = cheatSheet.StatsSeasonCode;
      cmd.Parameters.Add("@Created", SqlDbType.DateTime).Value = cheatSheet.Created;
      cmd.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = cheatSheet.LastUpdated;
      // PPR Flag
      if (cheatSheet.PPRLeague != null)
      {
        cmd.Parameters.Add("@PPRLeague", SqlDbType.Bit).Value = cheatSheet.PPRLeague;
      }
      else
      {
        cmd.Parameters.Add("@PPRLeague", SqlDbType.Bit).Value = DBNull.Value;
      }
      // Auction Flag
      if (cheatSheet.ActionDraft != null)
      {
        cmd.Parameters.Add("@AuctionDraft", SqlDbType.Bit).Value = cheatSheet.ActionDraft;
      }
      else
      {
        cmd.Parameters.Add("@AuctionDraft", SqlDbType.Bit).Value = DBNull.Value;
      }

      // returns an ArticleID
      cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Direction = ParameterDirection.Output;
      ExecuteNonQuery(cmd);
      return (int)cmd.Parameters["@CheatSheetID"].Value;
    }

    /// <summary>
    /// Inserts a cheat sheet position
    /// </summary>
    /// <param name="article"></param>
    /// <returns></returns>
    public override int InsertCheatSheetPosition(CheatSheetPositionDetails cheatSheetPosition)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertCheatSheetPosition", cn);

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheetPosition.CheatSheetID;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = cheatSheetPosition.PositionCode;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return ret;
      }
    }

    /// <summary>
    /// Inserts a cheat sheet position
    /// </summary>
    /// <param name="article"></param>
    /// <returns></returns>
    public override int InsertCheatSheetPosition(CheatSheetPositionDetails cheatSheetPosition, SqlConnection cn, SqlTransaction transaction)
    {
      SqlCommand cmd = new SqlCommand("Sheets_InsertCheatSheetPosition", cn, transaction);

      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheetPosition.CheatSheetID;
      cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = cheatSheetPosition.PositionCode;
      int ret = ExecuteNonQuery(cmd);
      return ret;
    }
    
    
    /// <summary>
    /// Inserts a cheat sheet stat
    /// </summary>
    /// <param name="article"></param>
    /// <returns></returns>
    public override int InsertCheatSheetStat(CheatSheetStatDetails cheatSheetStat)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertCheatSheetStat", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheetStat.CheatSheetID;
        cmd.Parameters.Add("@StatCode", SqlDbType.NVarChar).Value = cheatSheetStat.StatCode;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return ret;
      }
    }

    /// <summary>
    /// Inserts a cheat sheet stat
    /// </summary>
    /// <param name="article"></param>
    /// <returns></returns>
    public override int InsertCheatSheetStat(CheatSheetStatDetails cheatSheetStat, SqlConnection cn, SqlTransaction transaction)
    {
      SqlCommand cmd = new SqlCommand("Sheets_InsertCheatSheetStat", cn, transaction);
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheetStat.CheatSheetID;
      cmd.Parameters.Add("@StatCode", SqlDbType.NVarChar).Value = cheatSheetStat.StatCode;
      int ret = ExecuteNonQuery(cmd);
      return ret;
    }


    public override bool UpdateCheatSheet(CheatSheetDetails cheatSheet)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_UpdateCheatSheet", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheet.CheatSheetID;
        cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = cheatSheet.Username.Trim();
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = cheatSheet.SeasonCode;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = cheatSheet.SportCode;
        cmd.Parameters.Add("@SheetName", SqlDbType.NVarChar).Value = cheatSheet.SheetName;
        cmd.Parameters.Add("@StatsSeasonCode", SqlDbType.NVarChar).Value = cheatSheet.StatsSeasonCode;
        cmd.Parameters.Add("@Created", SqlDbType.DateTime).Value = cheatSheet.Created;
        cmd.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = cheatSheet.LastUpdated;
        // PPR League
        if (cheatSheet.PPRLeague == null)
        {
          cmd.Parameters.Add("@PPRLeague", SqlDbType.Bit).Value = DBNull.Value;
        }
        else
        {
          cmd.Parameters.Add("@PPRLeague", SqlDbType.Bit).Value = cheatSheet.PPRLeague;
        }
        // Auction Draft
        if (cheatSheet.ActionDraft == null)
        {
          cmd.Parameters.Add("@AuctionDraft", SqlDbType.Bit).Value = DBNull.Value;
        }
        else
        {
          cmd.Parameters.Add("@AuctionDraft", SqlDbType.Bit).Value = cheatSheet.ActionDraft;
        }
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }

    public override int GetCheatSheetCount(string userName)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetCheatSheetCountByUsername", cn);
        cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = userName;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return (int)ExecuteScalar(cmd);
      }
    }

    public override int GetCheatSheetCount(string userName, string sportCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetCheatSheetCountBySportCodeUsername", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = userName;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return (int)ExecuteScalar(cmd);
      }
    }


    public override int GetMemberCheatSheetCount()
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetMemberCheatSheetCount", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        int cheatSheetCount = (int)ExecuteScalar(cmd);
        return cheatSheetCount;
      }
    }


    public override int GetMemberCheatSheetCount(string sportCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetMemberCheatSheetCountBySportCode", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cn.Open();
        int cheatSheetCount = (int)ExecuteScalar(cmd);
        return cheatSheetCount;
      }
    }


    public override int GetVisitorCheatSheetCount()
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetVisitorCheatSheetCount", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return (int)ExecuteScalar(cmd);
      }
    }

    public override int GetVisitorCheatSheetCount(string sportCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetVisitorCheatSheetCountBySportCode", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cn.Open();
        int cheatSheetCount = (int)ExecuteScalar(cmd);
        return cheatSheetCount;
      }
    }


    //public override int CreateCheatSheet(CheatSheetDetails cheatSheet, string userName, List<PositionDetails> cheatSheetPositions, List<StatDetails> cheatSheetStats, List<PlayerDetails> cheatSheetPlayers)
    //{
    //  int newSheetID = 0;

    //  using (SqlConnection cn = new SqlConnection(this.ConnectionString))
    //  {
    //    cn.Open();
    //    SqlTransaction myTrans = cn.BeginTransaction();

    //    try
    //    {

    //      // insert the cheat sheet
    //      SqlCommand cmd = new SqlCommand("Sheets_InsertCheatSheet", cn, myTrans);
    //      cmd.CommandType = CommandType.StoredProcedure;
    //      cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = cheatSheet.SeasonCode;
    //      cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = cheatSheet.Username;
    //      cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = cheatSheet.SportCode;
    //      cmd.Parameters.Add("@SheetName", SqlDbType.NVarChar).Value = cheatSheet.SheetName;
    //      cmd.Parameters.Add("@StatsSeasonCode", SqlDbType.NVarChar).Value = cheatSheet.StatsSeasonCode;
    //      cmd.Parameters.Add("@Created", SqlDbType.DateTime).Value = cheatSheet.Created;
    //      cmd.Parameters.Add("@LastUpdated", SqlDbType.DateTime).Value = cheatSheet.LastUpdated;
    //      // PPR Flag
    //      if (cheatSheet.PPRLeague != null)
    //      {
    //        cmd.Parameters.Add("@PPRLeague", SqlDbType.Bit).Value = cheatSheet.PPRLeague;
    //      }
    //      else
    //      {
    //        cmd.Parameters.Add("@PPRLeague", SqlDbType.Bit).Value = DBNull.Value;
    //      }
    //      // Auction Flag
    //      if (cheatSheet.ActionDraft != null)
    //      {
    //        cmd.Parameters.Add("@AuctionDraft", SqlDbType.Bit).Value = cheatSheet.ActionDraft;
    //      }
    //      else
    //      {
    //        cmd.Parameters.Add("@AuctionDraft", SqlDbType.Bit).Value = DBNull.Value;
    //      }

    //      // returns an ArticleID
    //      cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Direction = ParameterDirection.Output;
    //      int ret = ExecuteNonQuery(cmd);
    //      newSheetID = (int)cmd.Parameters["@CheatSheetID"].Value;

    //      // insert cheat sheet positions
    //      for (int i = 0; i < cheatSheetPositions.Count; i++)
    //      {
    //        SqlCommand cmd3 = new SqlCommand("Sheets_InsertCheatSheetPosition", cn, myTrans);
    //        cmd3.CommandType = CommandType.StoredProcedure;
    //        cmd3.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = newSheetID;
    //        cmd3.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = cheatSheetPositions[i].PositionCode;
    //        int ret3 = ExecuteNonQuery(cmd3);
    //      }
          
    //      // insert cheat sheet stats
    //      for (int i = 0; i < cheatSheetStats.Count; i++)
    //      {
    //        SqlCommand cmd4 = new SqlCommand("Sheets_InsertCheatSheetStat", cn, myTrans);
    //        cmd4.CommandType = CommandType.StoredProcedure;
    //        cmd4.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = newSheetID;
    //        cmd4.Parameters.Add("@StatCode", SqlDbType.NVarChar).Value = cheatSheetStats[i].StatCode;
    //        int ret4 = ExecuteNonQuery(cmd4);
    //      }

    //      // insert cheat sheet players
    //      for (int i = 0; i <  cheatSheetPlayers.Count; i++)
    //      {
    //        SqlCommand cmd5 = new SqlCommand("Sheets_InsertCheatSheetItem", cn, myTrans);
    //        cmd5.CommandType = CommandType.StoredProcedure;
    //        cmd5.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = newSheetID;
    //        cmd5.Parameters.Add("@PlayerID", SqlDbType.Int).Value = cheatSheetPlayers[i].PlayerID;
    //        cmd5.Parameters.Add("@Seqno", SqlDbType.SmallInt).Value = i+1;
    //        cmd5.Parameters.Add("@SleeperTag", SqlDbType.Bit).Value = false;
    //        cmd5.Parameters.Add("@BustTag", SqlDbType.Bit).Value = false;
    //        cmd5.Parameters.Add("@InjuredTag", SqlDbType.Bit).Value = false;
    //        cmd5.Parameters.Add("@Note", SqlDbType.NVarChar).Value = String.Empty;

    //        int ret5 = ExecuteNonQuery(cmd5);
    //      }

    //      myTrans.Commit();
    //    }
    //    catch(Exception ex)
    //    {
    //      new CSWRWebEvent("Error in DAL creating cheat sheet", null, ex).Raise();
    //      myTrans.Rollback();
    //      newSheetID = 999;
    //    }
    //  }
    //  return newSheetID;
    //}




    public override int CreateCheatSheet(CheatSheetDetails cheatSheet, string userName, List<PositionDetails> cheatSheetPositions, 
                                          List<StatDetails> cheatSheetStats, List<PlayerDetails> cheatSheetPlayers, int tryCount)
    {
      int newSheetID = 0;

      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        cn.Open();
        SqlTransaction myTrans = cn.BeginTransaction();

        try
        {

          // create the cheat sheet
          newSheetID = this.InsertCheatSheet(cheatSheet, cn, myTrans);

          // insert cheat sheet positions
          for (int i = 0; i < cheatSheetPositions.Count; i++)
          {
            this.InsertCheatSheetPosition(new CheatSheetPositionDetails(newSheetID, cheatSheetPositions[i].PositionCode), cn, myTrans);
          }

          // insert cheat sheet stats
          for (int i = 0; i < cheatSheetStats.Count; i++)
          {
            this.InsertCheatSheetStat(new CheatSheetStatDetails(newSheetID, cheatSheetStats[i].StatCode), cn, myTrans);
          }

          // insert cheat sheet players
          for (int i = 0; i < cheatSheetPlayers.Count; i++)
          {
            int seqNo = i + 1;
            CheatSheetItemDetails newItem = new CheatSheetItemDetails(newSheetID, cheatSheetPlayers[i].PlayerID, seqNo, false, false, false, String.Empty);
            this.InsertCheatSheetItem(newItem, cn, myTrans);
          }

          myTrans.Commit();
        }
        catch (Exception ex)
        {
          myTrans.Rollback();
          string errorMessage = "Error in DAL creating cheat sheet. sheetid: " + newSheetID.ToString() +  ", tryCount: " + tryCount.ToString();
          new CSWRWebEvent(errorMessage, null, ex).Raise();
          newSheetID = Globals.TransactionErrorSentinel;
        }
      }
      return newSheetID;
    }


    /// <summary>
    /// Deletes a player with the specified id
    /// </summary>
    /// <param name="articleID"></param>
    /// <returns></returns>
    public override bool DeleteCheatSheet(int cheatSheetID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_DeleteCheatSheet", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheetID;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }


    /// <summary>
    /// Deletes a player with the specified id
    /// </summary>
    /// <param name="articleID"></param>
    /// <returns></returns>
    public override bool DeleteCheatSheets(string userName)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_DeleteCheatSheetsByUsername", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = userName;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }


    /// <summary>
    /// Deletes all user cheat sheets older than a day old
    /// </summary>
    /// <param name="articleID"></param>
    /// <returns></returns>
    public override void DeleteOldVisitorCheatSheets()
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_DeleteOldVisitorCheatSheets", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        ExecuteNonQuery(cmd);
      }
    }



    // Cheat Sheet Item Methods

    public override List<CheatSheetItemDetails> GetCheatSheetItems(int cheatSheetID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetCheatSheetItemsBySheetID", cn);
        cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheetID;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        List<CheatSheetItemDetails> items = GetCheatSheetItemCollectionFromReader(ExecuteReader(cmd));
        return items;
      }
    }

    public override List<CheatSheetItemDetails> GetCheatSheetItems(int cheatSheetID, int recordCount)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetCheatSheetItemsBySheetIDCount", cn);
        cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheetID;
        cmd.Parameters.Add("@RecordCount", SqlDbType.Int).Value = recordCount;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetCheatSheetItemCollectionFromReader(ExecuteReader(cmd));
      }
    }


    public override CheatSheetItemDetails GetCheatSheetItem(int cheatSheetID, int playerID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetCheatSheetItemBySheetIDPlayerID", cn);
        cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheetID;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = playerID;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetCheatSheetItemFromReader(reader);
        else
          return null;
      }
    }


    public override int InsertCheatSheetItem(CheatSheetItemDetails cheatSheetItem)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertCheatSheetItem", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheetItem.CheatSheetID;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = cheatSheetItem.PlayerID;
        cmd.Parameters.Add("@Seqno", SqlDbType.SmallInt).Value = cheatSheetItem.Seqno;
        cmd.Parameters.Add("@SleeperTag", SqlDbType.Bit).Value = cheatSheetItem.SleeperTag;
        cmd.Parameters.Add("@BustTag", SqlDbType.Bit).Value = cheatSheetItem.BustTag;
        cmd.Parameters.Add("@InjuredTag", SqlDbType.Bit).Value = cheatSheetItem.InjuredTag;
        cmd.Parameters.Add("@Note", SqlDbType.NVarChar).Value = cheatSheetItem.Note;
        // returns an ItemID
        //cmd.Parameters.Add("@ItemID", SqlDbType.Int).Direction = ParameterDirection.Output;
        
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        //return (int)cmd.Parameters["@ItemID"].Value;
        return ret;
      }
    }


    public override int InsertCheatSheetItem(CheatSheetItemDetails cheatSheetItem, SqlConnection cn, SqlTransaction transaction)
    {
      SqlCommand cmd = new SqlCommand("Sheets_InsertCheatSheetItem", cn, transaction);
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheetItem.CheatSheetID;
      cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = cheatSheetItem.PlayerID;
      cmd.Parameters.Add("@Seqno", SqlDbType.SmallInt).Value = cheatSheetItem.Seqno;
      cmd.Parameters.Add("@SleeperTag", SqlDbType.Bit).Value = cheatSheetItem.SleeperTag;
      cmd.Parameters.Add("@BustTag", SqlDbType.Bit).Value = cheatSheetItem.BustTag;
      cmd.Parameters.Add("@InjuredTag", SqlDbType.Bit).Value = cheatSheetItem.InjuredTag;
      cmd.Parameters.Add("@Note", SqlDbType.NVarChar).Value = cheatSheetItem.Note;
      int ret = ExecuteNonQuery(cmd);
      return ret;
    }


    public override bool ReorderCheatSheetItems(int cheatSheetID, int oldIndex, int newIndex)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_ReorderCheatSheetItems", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheetID;
        cmd.Parameters.Add("@OldIndex", SqlDbType.Int).Value = oldIndex;
        cmd.Parameters.Add("@NewIndex", SqlDbType.Int).Value = newIndex;
        cn.Open();
        int ret = 0;
        try
        {
          ret = ExecuteNonQuery(cmd);
        }
        catch
        {
          int i = 0;
        }
        return (ret == 1);
      }
    }


    /// <summary>
    /// Returns a count of the number of users sheets which are utilizing sleeper functionality
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode"></param>
    /// <returns></returns>
    public override int GetUserCheatSheetSleeperUsageCount(string sportCode, string seasonCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetUserCheatSheetSleeperUsageCount", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cn.Open();
        return (int)ExecuteScalar(cmd);
      }
    }


    /// <summary>
    /// Returns a count of the number of users sheets which are utilizing bust functionality
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode"></param>
    /// <returns></returns>
    public override int GetUserCheatSheetBustUsageCount(string sportCode, string seasonCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetUserCheatSheetBustUsageCount", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cn.Open();
        return (int)ExecuteScalar(cmd);
      }
    }

    /// <summary>
    /// Returns a count of the number of users sheets which are utilizing note functionality
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode"></param>
    /// <returns></returns>
    public override int GetUserCheatSheetNoteUsageCount(string sportCode, string seasonCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetUserCheatSheetNoteUsageCount", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cn.Open();
        return (int)ExecuteScalar(cmd);
      }
    }




    public override bool UpdateCheatSheetItem(CheatSheetItemDetails cheatSheetItem)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_UpdateCheatSheetItem", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@CheatSheetID", SqlDbType.Int).Value = cheatSheetItem.CheatSheetID;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = cheatSheetItem.PlayerID;
        cmd.Parameters.Add("@Seqno", SqlDbType.SmallInt).Value = cheatSheetItem.Seqno;
        cmd.Parameters.Add("@SleeperTag", SqlDbType.Bit).Value = cheatSheetItem.SleeperTag;
        cmd.Parameters.Add("@BustTag", SqlDbType.Bit).Value = cheatSheetItem.BustTag;
        cmd.Parameters.Add("@InjuredTag", SqlDbType.Bit).Value = cheatSheetItem.InjuredTag;
        cmd.Parameters.Add("@Note", SqlDbType.NVarChar).Value = cheatSheetItem.Note;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }

    

    // ADP Calculation Count
    public override int GetADPCalculationCount(string seasonCode, string sportCode, string positionCode, DateTime calcTimestamp)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetADPCalculationCount", cn);
        cmd.Parameters.Add("@SeasonCode", SqlDbType.Char).Value = seasonCode;
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.Char).Value = positionCode;
        cmd.Parameters.Add("@CalcTimestamp", SqlDbType.DateTime).Value = calcTimestamp;
        cmd.CommandType = CommandType.StoredProcedure;

        cn.Open();
        return (int)ExecuteScalar(cmd);
      }
    }

    public override List<ADPCalculationDetails> GetADPCalculations(string sportCode, string seasonCode, string positionCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetADPCalculationsBySportSeasonPositionCodes", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.Char).Value = seasonCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.Char).Value = positionCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetADPCalculationCollectionFromReader(ExecuteReader(cmd));
      }
    }

    public override List<ADPCalculationDetails> GetADPCalculations(string sportCode, string seasonCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetADPCalculationsBySportSeasonCodes", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.Char).Value = seasonCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetADPCalculationCollectionFromReader(ExecuteReader(cmd));
      }
    }

    public override List<ADPCalculationDetails> GetADPCalculations(string sportCode, string seasonCode, string positionCode, DateTime calcTimestamp)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetADPCalculationsBySportSeasonPositionDate", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.Char).Value = seasonCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.Char).Value = positionCode;
        cmd.Parameters.Add("@CalcTimestamp", SqlDbType.DateTime).Value = calcTimestamp;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetADPCalculationCollectionFromReader(ExecuteReader(cmd));
      }
    }

  
    // ADP Player Logs

    /// <summary>
    /// Inserts an ADP Player Log
    /// </summary>
    /// <param name="article"></param>
    /// <returns></returns>
    public override int InsertADPPlayerLog(ADPPlayerLogDetails adpPlayerLog)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertADPPlayerLog", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ADPCalculationID", SqlDbType.Int).Value = adpPlayerLog.ADPCalculationID;
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = adpPlayerLog.SportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.Char).Value = adpPlayerLog.SeasonCode;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = adpPlayerLog.PlayerID;
        cmd.Parameters.Add("@ADP", SqlDbType.Float).Value = adpPlayerLog.ADP;
        cmd.Parameters.Add("@CalcTimestamp", SqlDbType.DateTime).Value = adpPlayerLog.CalcTimestamp;
        // returns an ArticleID
        cmd.Parameters.Add("@ADPPlayerLogID", SqlDbType.Int).Direction = ParameterDirection.Output;
        cn.Open();
        try
        {
          int ret = ExecuteNonQuery(cmd);
        }
        catch (Exception ex)
        {

        }
        return (int)cmd.Parameters["@ADPPlayerLogID"].Value;
      }
    }


    // ADP Positional Timestamps
    //public override DateTime GetADPCalculation(string sportCode, string seasonCode, string positionCode)
    //{
    //  using (SqlConnection cn = new SqlConnection(this.ConnectionString))
    //  {
    //    SqlCommand cmd = new SqlCommand("Sheets_GetADPCalculationBySportSeasonPositionCodes", cn);
    //    cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
    //    cmd.Parameters.Add("@SeasonCode", SqlDbType.Char).Value = seasonCode;
    //    cmd.Parameters.Add("@PositionCode", SqlDbType.Char).Value = positionCode;

    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cn.Open();
    //    IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
    //    if (reader.Read())
    //      return (DateTime)reader["CalcTimestamp"];
    //    else
    //      return DateTime.MinValue;
    //  }
    //}

    //public override bool UpdateADPCalculation(ADPCalculationDetails ADPCalculation)
    //{
    //  using (SqlConnection cn = new SqlConnection(this.ConnectionString))
    //  {
    //    SqlCommand cmd = new SqlCommand("Sheets_UpdateADPCalculation", cn);
    //    cmd.Parameters.Add("@ADPCalculationID", SqlDbType.Int).Value = ADPCalculation.ADPCalculationID;
    //    cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = ADPCalculation.SportCode;
    //    cmd.Parameters.Add("@SeasonCode", SqlDbType.Char).Value = ADPCalculation.SeasonCode;
    //    cmd.Parameters.Add("@PositionCode", SqlDbType.Char).Value = ADPCalculation.PositionCode;
    //    cmd.Parameters.Add("@CalcTimestamp", SqlDbType.DateTime).Value = ADPCalculation.CalcTimestamp;
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cn.Open();
    //    int ret = ExecuteNonQuery(cmd);
    //    return (ret == 1);
    //  }
    //}

    public override int InsertADPCalculation(ADPCalculationDetails ADPCalculation)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertADPCalculation", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = ADPCalculation.SportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = ADPCalculation.SeasonCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.NVarChar).Value = ADPCalculation.PositionCode;
        cmd.Parameters.Add("@CalcTimestamp", SqlDbType.DateTime).Value = ADPCalculation.CalcTimestamp;
        cmd.Parameters.Add("@TotalSheetsConsidered", SqlDbType.Int).Value = ADPCalculation.TotalSheetsConsidered;
        cmd.Parameters.Add("@Last24Sheets", SqlDbType.Int).Value = ADPCalculation.Last24Sheets;
        cmd.Parameters.Add("@Last48Sheets", SqlDbType.Int).Value = ADPCalculation.Last48Sheets;
        cmd.Parameters.Add("@Last72Sheets", SqlDbType.Int).Value = ADPCalculation.Last72Sheets;
        cmd.Parameters.Add("@TimespanInDays", SqlDbType.Int).Value = ADPCalculation.TimespanInDays;
        cmd.Parameters.Add("@ADPCalculationID", SqlDbType.Int).Direction = ParameterDirection.Output;

        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (int)cmd.Parameters["@ADPCalculationID"].Value;
      }
    }


    /// <summary>
    /// Deletes a supplemental source with the specified id
    /// </summary>
    /// <param name="articleID"></param>
    /// <returns></returns>
    public override bool DeleteADPCalculation(int adpCalculationID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_DeleteADPCalculation", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@ADPCalculationID", SqlDbType.Int).Value = adpCalculationID;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }


    public override List<ADPPlayerLogDetails> GetADPPlayerLogs(string sportCode, string seasonCode, string positionCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetADPPlayerLogsBySportSeasonPositionCodes", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.Char).Value = seasonCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.Char).Value = positionCode;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetADPlayerLogDetailsCollectionFromReader(ExecuteReader(cmd));
      }
    }

    public override List<ADPPlayerLogDetails> GetADPPlayerLogs(string sportCode, string seasonCode, string positionCode, DateTime calcTimestamp)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetADPPlayerLogsBySportSeasonPositionDate", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.Char).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.Char).Value = seasonCode;
        cmd.Parameters.Add("@PositionCode", SqlDbType.Char).Value = positionCode;
        cmd.Parameters.Add("@CalcTimestamp", SqlDbType.DateTime).Value = calcTimestamp;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetADPlayerLogDetailsCollectionFromReader(ExecuteReader(cmd));
      }
    }



    // Sport Season Supp Player Review Methods
    public override int InsertSportSeasonSuppPlayerReview(SportSeasonSuppPlayerReviewDetails sportSeasonSuppPlayerReview)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertSportSeasonSuppPlayerReview", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportSeasonSuppPlayerReview.SportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = sportSeasonSuppPlayerReview.SeasonCode;
        cmd.Parameters.Add("@SupplementalSourceID", SqlDbType.Int).Value = sportSeasonSuppPlayerReview.SupplementalSourceID;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = sportSeasonSuppPlayerReview.PlayerID;
        cmd.Parameters.Add("@ReviewURL", SqlDbType.NVarChar).Value = sportSeasonSuppPlayerReview.ReviewURL;
        cn.Open();
        return ExecuteNonQuery(cmd);
      }
    }

    public override List<SportSeasonSuppPlayerReviewDetails> GetSportSeasonSuppPlayerReviews(string sportCode, string seasonCode, int supplementalSourceID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSportSeasonSuppPlayerReviewsBySportSeasonSuppCodes", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@SupplementalSourceID", SqlDbType.Int).Value = supplementalSourceID;
        cn.Open();
        return GetSportSeasonSuppPlayerReviewDetailsCollectionFromReader(ExecuteReader(cmd));
      }
    }


    public override bool UpdateSportSeasonSuppPlayerReview(SportSeasonSuppPlayerReviewDetails sportSeasonSuppPlayerReview)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_UpdateSportSeasonSuppPlayerReview", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportSeasonSuppPlayerReview.SportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = sportSeasonSuppPlayerReview.SeasonCode;
        cmd.Parameters.Add("@SupplementalSourceID", SqlDbType.Int).Value = sportSeasonSuppPlayerReview.SupplementalSourceID;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = sportSeasonSuppPlayerReview.PlayerID;
        cmd.Parameters.Add("@ReviewURL", SqlDbType.NVarChar).Value = sportSeasonSuppPlayerReview.ReviewURL;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }


    /// <summary>
    /// Deletes the supplemental review for a player of a particular sport and season
    /// </summary>
    /// <param name="articleID"></param>
    /// <returns></returns>
    public override bool DeleteSportSeasonSuppPlayerReview(string sportCode, string seasonCode, int supplementalSourceID, int playerID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_DeleteSportSeasonSuppPlayerReview", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@SupplementalSourceID", SqlDbType.Int).Value = supplementalSourceID;
        cmd.Parameters.Add("@PlayerID", SqlDbType.Int).Value = playerID;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }


    public override SportSeasonSuppPlayerReviewDetails GetSportSeasonSuppPlayerReview(string sportCode, string seasonCode, int supplementalSourceID, int playerID)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetSportSeasonSuppPlayerReviewBySportSeasonSuppPlayerID", cn);
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@SupplementalSourceID", SqlDbType.NVarChar).Value = supplementalSourceID;
        cmd.Parameters.Add("@PlayerID", SqlDbType.NVarChar).Value = playerID;

        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetSportSeasonSuppPlayerReviewDetailsFromReader(reader);
        else
          return null;
      }
    }



    public override UpgradeVoucherDetails GetUpgradeVoucher(string voucherCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetUpgradeVoucherByVoucherCode", cn);
        cmd.Parameters.Add("@VoucherCode", SqlDbType.NVarChar).Value = voucherCode;

        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetUpgradeVoucherDetailsFromReader(reader);
        else
          return null;
      }
    }


    public override List<UpgradeUserDetails> GetUpgradeUsers(string sportCode, string seasonCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetUpgradeUsersBySportSeasonCodes", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cn.Open();
        return GetUpgradeUserDetailsDetailsCollectionFromReader(ExecuteReader(cmd));
      }
    }

    public override bool ConfirmUpgradeUser(string sportCode, string seasonCode, Guid userId)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetUpgradeUsersBySportSeasonCodes", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = sportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = seasonCode;
        cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = userId;
        cn.Open();
        return ExecuteNonQuery(cmd) == 1;
      }
    }



    // Log User Session
    public override bool LogUserSession(Guid userId)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_LogUserSession", cn);
        cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = userId;
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (ret == 1);
      }
    }


    public override List<UserSessionDetails> GetUserSessions()
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetUserSessions", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        return GetUserSessionDetailsCollectionFromReader(ExecuteReader(cmd));
      }
    }


    public override int InsertUpgradeUser(UpgradeUserDetails upgradeUser)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertUpgradeUser", cn);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = upgradeUser.SportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = upgradeUser.SeasonCode;
        cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = upgradeUser.UserId;

        if (upgradeUser.VoucherId != 0)
        {
          cmd.Parameters.Add("@VoucherId", SqlDbType.Int).Value = upgradeUser.VoucherId;
        }
        else
        {
          cmd.Parameters.Add("@VoucherId", SqlDbType.Int).Value = DBNull.Value;
        }
        cmd.Parameters.Add("@UpgradeTypeCode", SqlDbType.NVarChar).Value = upgradeUser.UpgradeTypeCode;
        cmd.Parameters.Add("@ConfirmationPageTimeStamp", SqlDbType.DateTime).Value = upgradeUser.ConfirmationPageTimestamp;

        if (upgradeUser.IPNTimestamp != DateTime.MinValue)
        {
          cmd.Parameters.Add("@IPNTimeStamp", SqlDbType.DateTime).Value = upgradeUser.IPNTimestamp;
        }
        else
        {
          cmd.Parameters.Add("@IPNTimeStamp", SqlDbType.DateTime).Value = DBNull.Value;
        }
        cn.Open();
        return ExecuteNonQuery(cmd);
      }
    }



    public override int InsertUpgradeVoucher(UpgradeVoucherDetails upgradeVoucher)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_InsertUpgradeVoucher", cn);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@SportCode", SqlDbType.NVarChar).Value = upgradeVoucher.SportCode;
        cmd.Parameters.Add("@SeasonCode", SqlDbType.NVarChar).Value = upgradeVoucher.SeasonCode;
        cmd.Parameters.Add("@VoucherCode", SqlDbType.NVarChar).Value = upgradeVoucher.VoucherCode;
        cmd.Parameters.Add("@CampaignTag", SqlDbType.NVarChar).Value = upgradeVoucher.CampaignTag;
        cmd.Parameters.Add("@GeneratedDate", SqlDbType.DateTime).Value = upgradeVoucher.GeneratedDate;

        if (upgradeVoucher.ClaimedDate != DateTime.MinValue)
        {
          cmd.Parameters.Add("@ClaimedDate", SqlDbType.DateTime).Value = upgradeVoucher.ClaimedDate;
        }
        else
        {
          cmd.Parameters.Add("@ClaimedDate", SqlDbType.DateTime).Value = DBNull.Value;
        }


        cmd.Parameters.Add("@VoucherID", SqlDbType.Int).Direction = ParameterDirection.Output;

        cn.Open();
        int ret = ExecuteNonQuery(cmd);
        return (int)cmd.Parameters["@VoucherID"].Value;
      }
    }

    public override UpgradeTypeDetails GetUpgradeType(string upgradeTypeCode)
    {
      using (SqlConnection cn = new SqlConnection(this.ConnectionString))
      {
        SqlCommand cmd = new SqlCommand("Sheets_GetUpgradeTypeByUpgradeTypeCode", cn);
        cmd.Parameters.Add("@UpgradeTypeCode", SqlDbType.NVarChar).Value = upgradeTypeCode;

        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        IDataReader reader = ExecuteReader(cmd, CommandBehavior.SingleRow);
        if (reader.Read())
          return GetUpgradeTypeDetailsFromReader(reader);
        else
          return null;
      }
    }

  }
}