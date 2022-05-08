using System;

/// <summary>
/// Summary description for SeasonDetails
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class UpgradeUserDetails
  {
    public UpgradeUserDetails(string sportCode, string seasonCode, Guid userId, int voucherId, 
                                  string upgradeTypeCode, DateTime confirmationPageTimestamp, DateTime ipnTimestamp)
    {
      this.SportCode = sportCode;
      this.SeasonCode = seasonCode;
      this.UserId = userId;
      this.VoucherId = voucherId;
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
  
  }
}