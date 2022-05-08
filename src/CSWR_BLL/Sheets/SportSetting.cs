using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Summary description for Setting
/// </summary>
/// 
namespace BP.CheatSheetWarRoom.BLL.Sheets  
{
  public class SportSetting : BaseSheet
  {
    public SportSetting(int sportSettingID, string sportCode, string settingCode, string settingName, string settingValue)
    {
      this.SportSettingID = sportSettingID;
      this.SportCode = sportCode;
      this.SettingCode = settingCode;
      this.SettingName = settingName;
      this.SettingValue = settingValue;
    }

    // SportSettingID Code
    public int SportSettingID {get;set;}

    // Sport Code
    public string SportCode {get;set;}

    // Setting Code
    public string SettingCode {get;set;}

    // Setting Name
    public string SettingName {get;set;}

    // Setting Value
    private string _settingValue = String.Empty;
    public string SettingValue
    {
      get
      {
        return _settingValue.Trim();
      }
      set
      {
        _settingValue = value;
      }
    }

    public static class Football
    {

      public static bool ShowSupplementalRankings
      {
        get
        {
          return (SportSetting.GetSportSetting("FOO", "SUPRNK").SettingValue == "1");
        }
        set
        {
          switch (value)
          {
            case true:
              SportSetting.UpdateSportSettingValue("FOO", "SUPRNK", "0");
              break;
            case false:
              SportSetting.UpdateSportSettingValue("FOO", "SUPRNK", "1");
              break;
          }
        }
      }

      public static bool ShowAffiliateAds
      {
        get
        {
          return (SportSetting.GetSportSetting("FOO", "SHOAFF").SettingValue == "1");
        }
        set
        {
          switch (value)
          {
            case true:
              SportSetting.UpdateSportSettingValue("FOO", "SHOAFF", "0");
              break;
            case false:
              SportSetting.UpdateSportSettingValue("FOO", "SHOAFF", "1");
              break;
          }
        }
      }

      public static bool CalculateADP
      {
        get
        {
          return (SportSetting.GetSportSetting("FOO", "CALADP").SettingValue == "1");
        }
        set
        {
          switch (value)
          {
            case true:
              SportSetting.UpdateSportSettingValue("FOO", "CALADP", "0");
              break;
            case false:
              SportSetting.UpdateSportSettingValue("FOO", "CALADP", "1");
              break;
          }
        }
      }

      public static int TimespanInDays
      {
        get
        {
          int timespanInDays = 0;
          int.TryParse(SportSetting.GetSportSetting("FOO", "TSNDAY").SettingValue, out timespanInDays);
          return timespanInDays;
        }
        set
        {
          SportSetting.UpdateSportSettingValue("FOO", "TSNDAY", value.ToString());
        }
      }

    }

    public static class Racing
    {

      public static bool ShowSupplementalRankings
      {
        get
        {
          return (SportSetting.GetSportSetting("RAC", "SUPRNK").SettingValue == "1");
        }
        set
        {
          switch (value)
          {
            case true:
              SportSetting.UpdateSportSettingValue("RAC", "SUPRNK", "0");
              break;
            case false:
              SportSetting.UpdateSportSettingValue("RAC", "SUPRNK", "1");
              break;
          }
        }
      }

      public static bool ShowAffiliateAds
      {
        get
        {
          return (SportSetting.GetSportSetting("RAC", "SHOAFF").SettingValue == "1");
        }
        set
        {
          switch (value)
          {
            case true:
              SportSetting.UpdateSportSettingValue("RAC", "SHOAFF", "0");
              break;
            case false:
              SportSetting.UpdateSportSettingValue("RAC", "SHOAFF", "1");
              break;
          }
        }
      }
    
    }

    /// <summary>
    /// Returns a Setting object with the specified code
    /// </summary>
    public static SportSetting GetSportSetting(string sportCode, string settingCode)
    {
      SportSetting sportSetting = null;
      string key = "Sheets_SportSetting_" + sportCode + "_" + settingCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        sportSetting = (SportSetting)BizObject.Cache[key];
      }
      else
      {
        sportSetting = GetSportSettingFromSportSettingDetails(SiteProvider.Sheets.GetSportSetting(sportCode, settingCode));
        BaseSheet.CacheData(key, sportSetting);
      }
      return sportSetting;
    }


    // Static Update
    public static bool UpdateSportSettingValue(string sportCode, string settingCode, string settingValue)
    {

      SportSettingDetails sportSetting = new SportSettingDetails(0, sportCode, settingCode, String.Empty, settingValue);
      bool ret = SiteProvider.Sheets.UpdateSportSettingValue(sportSetting);

      BizObject.PurgeCacheItems("Sheets_SportSettings");
      BizObject.PurgeCacheItems("Sheets_SportSetting_" + sportCode + "_" + settingCode);
      return ret;
    }









    /// <summary>
    /// Converts a SportSettingDetails DAL object into a SportSetting BLL object
    /// </summary>
    private static SportSetting GetSportSettingFromSportSettingDetails(SportSettingDetails sportSetting)
    {
      if (sportSetting == null)
        return null;
      else
      {
        return new SportSetting(sportSetting.SportSettingID, sportSetting.SportCode, sportSetting.SettingCode, 
          sportSetting.SettingName, sportSetting.SettingValue);
      }
    }

    /// <summary>
    /// Converts a collection of SportSettingDetails objects into a collection of SportSetting objects
    /// </summary>
    private static List<SportSetting> GetSportSettingListFromSportSettingDetailsList(List<SportSettingDetails> recordset)
    {
      List<SportSetting> sportSettings = new List<SportSetting>();
      foreach (SportSettingDetails record in recordset)
        sportSettings.Add(GetSportSettingFromSportSettingDetails(record));
      return sportSettings;
    }
  
  }





}
