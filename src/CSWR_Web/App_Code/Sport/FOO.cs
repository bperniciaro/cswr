using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom
{

  public static class FOO
  {
    public static string CurrentSeason
    {
      get
      {
        return SportSeason.GetCurrentSportSeason(FOO.FOOString).SeasonCode;
      }
    }

    public static string LastSeason
    {
      get
      {
        return SportSeason.GetCurrentSportSeason(FOO.FOOString).LastSeasonCode;
      }
    }

    public const string FOOString = Globals.FooString;

  }

}