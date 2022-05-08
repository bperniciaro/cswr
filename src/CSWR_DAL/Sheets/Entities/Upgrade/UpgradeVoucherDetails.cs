using System;

/// <summary>
/// Summary description for SeasonDetails
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class UpgradeVoucherDetails
  {
    public UpgradeVoucherDetails(int voucherId, string sportCode, string seasonCode, string voucherCode, string campaignTag,
                                 DateTime generatedDate, DateTime claimedDate)
    {
      this.VoucherID = voucherId;
      this.SportCode = sportCode;
      this.SeasonCode = seasonCode;
      this.VoucherCode = voucherCode;
      this.CampaignTag = campaignTag;
      this.GeneratedDate = generatedDate;
      this.ClaimedDate = claimedDate;

    }

    public int VoucherID { get; set; }
    public string SportCode { get; set; }
    public string SeasonCode { get; set; }
    public string VoucherCode { get; set; }
    public string CampaignTag { get; set; }
    public DateTime GeneratedDate { get; set; }
    public DateTime ClaimedDate { get; set; }
  
  }
}