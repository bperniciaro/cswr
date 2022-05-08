/// <summary>
/// Summary description for SportDetails
/// </summary>
/// 
namespace BP.CheatSheetWarRoom.DAL
{
  public class SportSettingDetails
  {
    public SportSettingDetails(int sportSettingID, string sportCode, string settingCode, string settingName, string settingValue)
    {
      this.SportSettingID = sportSettingID;
      this.SportCode = sportCode;
      this.SettingCode = settingCode;
      this.SettingName = settingName;
      this.SettingValue = settingValue;
    }

    // Sport Setting ID
    public int SportSettingID {get ;set;}

    // Sport Code
    public string SportCode { get; set; }

    // Setting Code
    public string SettingCode { get; set; }

    // Setting Name
    public string SettingName { get; set; }

    // Setting Value
    public string SettingValue { get; set; }

  }
}