using System;

namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public static class StringHelper
  {

    public static string ToFormattedMoney(this double dollars)
    {
      string formatttedDollars = String.Empty;
      if (dollars != null)
      {
        formatttedDollars = "$" + dollars.ToString("#,##0");
        return formatttedDollars;
      }
      else
      {
        return null;
      }
    }
  }

}
