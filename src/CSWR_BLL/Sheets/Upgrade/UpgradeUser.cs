using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// This class represents a bye week
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class UpgradeUser : BaseSheet
  {

    public UpgradeUser(string sportCode, string seasonCode, Guid userId, int voucherId,
                                  string upgradeTypeCode, DateTime confirmationPageTimestamp, DateTime ipnTimestamp)
    {
      this.SportCode = sportCode;
      this.SeasonCode = seasonCode;
      this.UserId = userId;
      this.VoucherId = voucherId;
      this.UpgradeTypeCode = upgradeTypeCode;
      this.UpgradeTypeCode = upgradeTypeCode;
      this.IPNTimestamp = ipnTimestamp;
    }

    public string SportCode { get; set; }
    public string SeasonCode { get; set; }
    public Guid UserId { get; set; }
    public int VoucherId { get; set; }
    public string UpgradeTypeCode { get; set; }
    public DateTime ConfirmationPageTimestamp { get; set; }
    public DateTime IPNTimestamp { get; set; }



    /// <summary>
    /// Gets all users who upgraded in a particular year for a particular sport
    /// </summary>
    public static List<UpgradeUser> GetUpgradeUsers(string sportCode, string seasonCode)
    {
      List<UpgradeUser> upgradeUsers = GetUpgradeUserListFromUpgradeUserDetailsList(SiteProvider.Sheets.GetUpgradeUsers(sportCode, seasonCode));
      return upgradeUsers;
    }



    public static int InsertUpgradeUser(UpgradeUser upgradeUser)
    {
      UpgradeUserDetails record = new UpgradeUserDetails(upgradeUser.SportCode, upgradeUser.SeasonCode, upgradeUser.UserId, upgradeUser.VoucherId,
                                                          upgradeUser.UpgradeTypeCode, upgradeUser.ConfirmationPageTimestamp, upgradeUser.IPNTimestamp);
      int itemID = SiteProvider.Sheets.InsertUpgradeUser(record);
      return itemID;
    }


    public static bool ConfirmUpgradeUser(string sportCode, string seasonCode, Guid userId)
    {
      return SiteProvider.Sheets.ConfirmUpgradeUser(sportCode, seasonCode, userId);
    }






    
    /// <summary>
    /// Converts an entity object to a business object
    /// </summary>
    private static UpgradeUser GetUpgradeUserFromUpgradeUserDetails(UpgradeUserDetails upgradeUser)
    {
      if (upgradeUser == null)
        return null;
      else
      {
        return new UpgradeUser(upgradeUser.SportCode, upgradeUser.SeasonCode, upgradeUser.UserId, upgradeUser.VoucherId, upgradeUser.UpgradeTypeCode,
                                  upgradeUser.ConfirmationPageTimestamp, upgradeUser.IPNTimestamp);
      }
    }

    /// <summary>
    /// Converta a collection of entity objects to a collection of business objects
    /// </summary>
    private static List<UpgradeUser> GetUpgradeUserListFromUpgradeUserDetailsList(List<UpgradeUserDetails> recordset)
    {
      List<UpgradeUser> upgradeUsers = new List<UpgradeUser>();
      foreach (UpgradeUserDetails record in recordset)
        upgradeUsers.Add(GetUpgradeUserFromUpgradeUserDetails(record));
      return upgradeUsers.GetRange(0, upgradeUsers.Count);
    }
  
  
  }

}