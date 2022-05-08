/// <summary>
/// Summary description for SiteProvider
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public static class SiteProvider
  {

    public static SheetsProvider Sheets
    {
      get { return SheetsProvider.Instance; }
    }

    public static ForumProvider Forum
    {
      get { return ForumProvider.Instance; }
    }

    public static BlogProvider Blog
    {
      get { return BlogProvider.Instance; }
    }

  }
}