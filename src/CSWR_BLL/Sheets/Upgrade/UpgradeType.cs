using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// This class represents a bye week
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class UpgradeType : BaseSheet
  {

    public UpgradeType(string upgradeTypeCode, string name, string description)
    {
      this.UpgradeTypeCode = upgradeTypeCode;
      this.Name = name;
      this.Description = description;
    }

    public string UpgradeTypeCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }



    public static UpgradeType GetUpgradeType(string upgradeTypeCode)
    {
      UpgradeTypeDetails record = SiteProvider.Sheets.GetUpgradeType(upgradeTypeCode);
      return GetUpgradeTypeFromUpgradeTypeDetails(record);
    }


    
    /// <summary>
    /// Converts an entity object to a business object
    /// </summary>
    private static UpgradeType GetUpgradeTypeFromUpgradeTypeDetails(UpgradeTypeDetails upgradeType)
    {
      if (upgradeType == null)
        return null;
      else
      {
        return new UpgradeType(upgradeType.UpgradeTypeCode, upgradeType.Name, upgradeType.Description);
      }
    }

    /// <summary>
    /// Converta a collection of entity objects to a collection of business objects
    /// </summary>
    private static List<UpgradeType> GetUpgradeUserListFromUpgradeUserDetailsList(List<UpgradeTypeDetails> recordset)
    {
      List<UpgradeType> upgradeTypes = new List<UpgradeType>();
      foreach (UpgradeTypeDetails record in recordset)
        upgradeTypes.Add(GetUpgradeTypeFromUpgradeTypeDetails(record));
      return upgradeTypes.GetRange(0, upgradeTypes.Count);
    }
  
  
  }

}