using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Represents site-wide settings
/// </summary>
/// 
namespace BP.CheatSheetWarRoom.BLL.Sheets  
{
  public class Setting : BaseSheet
  {
    public Setting(string settingCode, string settingName, string settingValue)
    {
      this.SettingCode = settingCode;
      this.SettingName = settingName;
      this.SettingValue = settingValue;
    }

    /// <summary>
    /// The code which represents the setting
    /// </summary>
    public string SettingCode {get;set;}

    /// <summary>
    /// The name given to the setting
    /// </summary>
    public string SettingName {get;set;}

    /// <summary>
    /// The value assigned to the setting
    /// </summary>
    public string SettingValue {get;set;}




    /// <summary>
    /// Returns a Setting object with the specified code
    /// </summary>
    public static Setting GetSetting(string settingCode)
    {
      Setting setting = null;
      string key = "Sheets_Setting_" + settingCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        setting = (Setting)BizObject.Cache[key];
      }
      else
      {
        setting = GetSettingFromSettingDetails(SiteProvider.Sheets.GetSetting(settingCode));
        BaseSheet.CacheData(key, setting);
      }
      return setting;
    }


    /// <summary>
    /// Updates the setting with the specified value
    /// </summary>
    /// <param name="settingCode"></param>
    /// <param name="settingValue"></param>
    /// <returns></returns>
    public static bool UpdateSettingValue(string settingCode, string settingValue)
    {
      // build an article entity to update, then use the module specific provider to update it
      SettingDetails setting = new SettingDetails(settingCode, String.Empty, settingValue);
      bool ret = SiteProvider.Sheets.UpdateSettingValue(setting);

      BizObject.PurgeCacheItems("Sheets_Settings");
      BizObject.PurgeCacheItems("Sheets_Setting_" + settingCode);
      return ret;
    }






    /// <summary>
    /// Converts a SettingDetails entity object to a Setting business object
    /// </summary>
    private static Setting GetSettingFromSettingDetails(SettingDetails setting)
    {
      if (setting == null)
        return null;
      else
      {
        return new Setting(setting.SettingCode, setting.SettingName, setting.SettingValue);
      }
    }

    /// <summary>
    /// Converts a collection of SettingDetails objects to a collection of Setting business objects
    /// </summary>
    private static List<Setting> GetSettingListFromSettingDetailsList(List<SettingDetails> recordset)
    {
      List<Setting> settings = new List<Setting>();
      foreach (SettingDetails record in recordset)
        settings.Add(GetSettingFromSettingDetails(record));
      return settings;
    }
  
  }





}
