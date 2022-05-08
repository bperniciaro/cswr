/// <summary>
/// Summary description for SeasonDetails
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class UpgradeTypeDetails
  {
    public UpgradeTypeDetails(string upgradeTypeCode, string name, string description)
    {
      this.UpgradeTypeCode = upgradeTypeCode;
      this.Name = name;
      this.Description = description;
    }

    public string UpgradeTypeCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
  
  }
}