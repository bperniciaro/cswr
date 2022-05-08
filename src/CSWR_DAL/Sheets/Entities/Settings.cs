/// <summary>
/// Summary description for SportDetails
/// </summary>
/// 
namespace BP.CheatSheetWarRoom.DAL
{
  public class SettingDetails
  {
    public SettingDetails(string settingCode, string settingName, string settingValue)
    {
      this.SettingCode = settingCode;
      this.SettingName = settingName;
      this.SettingValue = settingValue;
    }

    // Setting Code
    private string _settingCode = "";
    public string SettingCode
    {
      get { return _settingCode; }
      set { _settingCode = value; }
    }

    // Setting Name
    private string _settingName = "";
    public string SettingName
    {
      get { return _settingName; }
      set { _settingName = value; }
    }

    // Setting Value
    private string _settingValue = "";
    public string SettingValue
    {
      get { return _settingValue; }
      set { _settingValue = value; }
    }

  }
}