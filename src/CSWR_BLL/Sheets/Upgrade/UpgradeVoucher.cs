using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// This class represents a bye week
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class UpgradeVoucher : BaseSheet
  {

    public UpgradeVoucher(int voucherId, string sportCode, string seasonCode, string voucherCode, string campaignTag,
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



    public static UpgradeVoucher GetUpgradeVoucher(string voucherCode)
    {
      UpgradeVoucherDetails record = SiteProvider.Sheets.GetUpgradeVoucher(voucherCode);
      return GetUpgradeVoucherFromUpgradeVoucherDetails(record);
    }


    public static int InsertUpgradeVoucher(UpgradeVoucher voucher)
    {
      UpgradeVoucherDetails record = new UpgradeVoucherDetails(voucher.VoucherID, voucher.SportCode, voucher.SeasonCode, voucher.VoucherCode,
                                                    voucher.CampaignTag, voucher.GeneratedDate, voucher.ClaimedDate);
      int itemID = SiteProvider.Sheets.InsertUpgradeVoucher(record);
      return itemID;
    }

    public int Insert()
    {
      UpgradeVoucherDetails record = new UpgradeVoucherDetails(this.VoucherID, this.SportCode, this.SeasonCode, this.VoucherCode,
                                                    this.CampaignTag, this.GeneratedDate, this.ClaimedDate);
      int itemID = SiteProvider.Sheets.InsertUpgradeVoucher(record);
      return itemID;
    }
    
    /// <summary>
    /// Converts an entity object to a business object
    /// </summary>
    private static UpgradeVoucher GetUpgradeVoucherFromUpgradeVoucherDetails(UpgradeVoucherDetails upgradeVoucher)
    {
      if (upgradeVoucher == null)
        return null;
      else
      {
        return new UpgradeVoucher(upgradeVoucher.VoucherID, upgradeVoucher.SportCode, upgradeVoucher.SeasonCode, upgradeVoucher.VoucherCode,
                                        upgradeVoucher.CampaignTag, upgradeVoucher.GeneratedDate, upgradeVoucher.ClaimedDate);
      }
    }

    /// <summary>
    /// Converta a collection of entity objects to a collection of business objects
    /// </summary>
    private static List<UpgradeVoucher> GetUpgradeVoucherListFromUpgradeVoucherDetailsList(List<UpgradeVoucherDetails> recordset)
    {
      List<UpgradeVoucher> upgradeVouchers = new List<UpgradeVoucher>();
      foreach (UpgradeVoucherDetails record in recordset)
        upgradeVouchers.Add(GetUpgradeVoucherFromUpgradeVoucherDetails(record));
      return upgradeVouchers.GetRange(0, upgradeVouchers.Count);
    }
  
  
  }

}