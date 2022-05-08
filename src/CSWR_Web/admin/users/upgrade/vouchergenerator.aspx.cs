using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.Admin.Users
{
  public partial class VoucherGenerator : BasePage
  {

    private static Random random = new Random();
    private const int _voucherLength = 10;


    private static string GenerateVoucher(char[] keys, int lengthOfVoucher)
    {
      return Enumerable
          .Range(1, lengthOfVoucher) // for(i.. ) 
          .Select(k => keys[random.Next(0, keys.Length - 1)])  // generate a new random char 
          .Aggregate("", (e, c) => e + c); // join into a string
    }

    protected void butGenerateVouchers_Click(object sender, EventArgs e)
    {
      int vouchersToGenerate = 0;
      StringBuilder voucherList = new StringBuilder();

      int.TryParse(tbVoucherCount.Text, out vouchersToGenerate);

      List<string> generatedVouchers = new List<string>();
      char[] keys = "ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890".ToCharArray();

      int voucherCount = 0;
      while (voucherCount < vouchersToGenerate)
      {
        string newVoucherCode = GenerateVoucher(keys, _voucherLength);
        if (UpgradeVoucher.GetUpgradeVoucher(newVoucherCode) == null)
        {
          UpgradeVoucher newVoucher = new UpgradeVoucher(0, FOO.FOOString, FOO.CurrentSeason, newVoucherCode, tbCampaignTag.Text,
                                                            DateTime.Now, DateTime.MinValue);
          newVoucher.Insert();
          if (voucherCount == 0)
          {
            voucherList.Append(newVoucherCode);
          }
          else
          {
            voucherList.Append(", " + newVoucherCode);
          }
          voucherCount++;
        }
      }

      labVoucherCodes.Text = voucherList.ToString();
    }
}
}