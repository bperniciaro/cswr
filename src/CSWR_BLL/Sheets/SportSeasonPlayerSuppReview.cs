using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class SportSeasonSuppPlayerReview : BaseSheet
  {


    public SportSeasonSuppPlayerReview(string sportCode, string seasonCode, int supplementalSourceID, int playerID, string reviewURL)
    {
      this.SportCode = sportCode;
      this.SeasonCode = seasonCode;
      this.SupplementalSourceID = supplementalSourceID;
      this.PlayerID = playerID;
      this.ReviewURL = reviewURL;

    }

    // Sport Code
    public string SportCode {get;set;}

    // Season Code
    public string SeasonCode {get ; set;}

    // SupplementalSourceID
    public int SupplementalSourceID { get; set; }

    // Player ID
    public int PlayerID { get; set; }
    
    // ReviewURL
    public string ReviewURL { get; set; }


    public static int InsertSportSeasonSuppPlayerReview(string sportCode, string seasonCode, int supplementalSourceID, int playerID, string url)
    {

      // create an article entity to inser and use the specific provider to do the insert
      SportSeasonSuppPlayerReviewDetails record = new SportSeasonSuppPlayerReviewDetails(sportCode, seasonCode, supplementalSourceID, playerID, url);
      int ret = SiteProvider.Sheets.InsertSportSeasonSuppPlayerReview(record);
      BizObject.PurgeCacheItems("Sheets_SportSeasonSuppPlayerReviews");
      return ret;
    }


    /// <summary>
    /// Returns all stats for a sport/season/supplementalsource triad
    /// </summary>
    public static List<SportSeasonSuppPlayerReview> GetSportSeasonSuppPlayerReviews(string sportCode, string seasonCode, int supplementalSourceID)
    {
      List<SportSeasonSuppPlayerReview> sportSeasonSuppPlayerReviews = null;
      string key = "Sheets_SportSeasonSuppPlayerReviewsBySportSeasonSuppID_" + sportCode + "_" + seasonCode + "_" + supplementalSourceID.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        sportSeasonSuppPlayerReviews = (List<SportSeasonSuppPlayerReview>)BizObject.Cache[key];
      }
      else
      {
        List<SportSeasonSuppPlayerReviewDetails> recordset = SiteProvider.Sheets.GetSportSeasonSuppPlayerReviews(sportCode, seasonCode, supplementalSourceID);
        sportSeasonSuppPlayerReviews = GetSportSeasonSuppPlayerReviewListFromSportSeasonSuppPlayerReviewDetailsList(recordset);
        //BaseSheet.CacheData(key, sportSeasonSuppPlayerReviews);
      }
      return sportSeasonSuppPlayerReviews.GetRange(0, sportSeasonSuppPlayerReviews.Count);
    }




    public static bool UpdateSportSeasonSuppPlayerReview(string sportCode, string seasonCode, int supplementalSourceID, int playerID, string reviewURL)
    {
      SportSeasonSuppPlayerReviewDetails record = new SportSeasonSuppPlayerReviewDetails(sportCode, seasonCode, supplementalSourceID, playerID, reviewURL);
      
      bool ret = SiteProvider.Sheets.UpdateSportSeasonSuppPlayerReview(record);

      BizObject.PurgeCacheItems("Sheets_SportSeasonSuppPlayerReviews");
      BizObject.PurgeCacheItems("Sheets_SportSeasonSuppPlayerReviewBySportSeasonSuppPlayerID_" + sportCode + "_" + seasonCode + "_" + supplementalSourceID + "_" + playerID);
      return ret;
    }


    /// <summary>
    /// Deletes the review for a particular player for a particular sport, season, supplementalSourceID
    /// </summary>
    /// <param name="sportCode"></param>
    /// <param name="seasonCode"></param>
    /// <param name="playerID"></param>
    /// <returns></returns>
    public static bool DeleteSportSeasonSuppPlayerReview(string sportCode, string seasonCode, int supplementalSourceID, int playerID)
    {
      bool ret = SiteProvider.Sheets.DeleteSportSeasonSuppPlayerReview(sportCode, seasonCode, supplementalSourceID, playerID);
      BizObject.PurgeCacheItems("Sheets_SportSeasonSuppPlayerReviews");
      BizObject.PurgeCacheItems("Sheets_SportSeasonSuppPlayerReviewBySportSeasonSuppPlayerID_" + sportCode + "_" + seasonCode + "_" + supplementalSourceID + "_" + playerID);
      return ret;
    }



    public static SportSeasonSuppPlayerReview GetSportSeasonSuppPlayerReview(string sportCode, string seasonCode, int supplementalSourceID, int playerID)
    {
      SportSeasonSuppPlayerReview sportSeasonSuppPlayerReview;

      string key = "Sheets_SportSeasonSuppPlayerReviewBySportSeasonSuppPlayerID_" + sportCode + "_" + seasonCode + "_" + supplementalSourceID + "_" + playerID;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        sportSeasonSuppPlayerReview = (SportSeasonSuppPlayerReview)BizObject.Cache[key];
      }
      else
      {
        sportSeasonSuppPlayerReview = GetSportSeasonSuppPlayerReviewFromSportSeasonSuppPlayerReviewDetails(SiteProvider.Sheets.GetSportSeasonSuppPlayerReview(sportCode, seasonCode, supplementalSourceID, playerID));
        BaseSheet.CacheData(key, sportSeasonSuppPlayerReview);
      }
      return sportSeasonSuppPlayerReview;
    }




    /// <summary>
    /// Converts a SportSeasonSuppPlayerReviewDetails entity object to a SportSeasonSuppPlayerReview business object
    /// </summary>
    private static SportSeasonSuppPlayerReview GetSportSeasonSuppPlayerReviewFromSportSeasonSuppPlayerReviewDetails(SportSeasonSuppPlayerReviewDetails sportSeasonSuppPlayerReview)
    {
      if (sportSeasonSuppPlayerReview == null)
        return null;
      else
      {
        return new SportSeasonSuppPlayerReview(sportSeasonSuppPlayerReview.SportCode,  sportSeasonSuppPlayerReview.SeasonCode, sportSeasonSuppPlayerReview.SupplementalSourceID, sportSeasonSuppPlayerReview.PlayerID,
          sportSeasonSuppPlayerReview.ReviewURL); 
      }
    }

    /// <summary>
    /// Converts a collection of SportSeasonSuppPlayerReviewDetails objects to a collection of SportSeason business objects
    /// </summary>
    private static List<SportSeasonSuppPlayerReview> GetSportSeasonSuppPlayerReviewListFromSportSeasonSuppPlayerReviewDetailsList(List<SportSeasonSuppPlayerReviewDetails> recordset)
    {
      List<SportSeasonSuppPlayerReview> sportSeasonSuppPlayerReviews = new List<SportSeasonSuppPlayerReview>();
      foreach (SportSeasonSuppPlayerReviewDetails record in recordset)
        sportSeasonSuppPlayerReviews.Add(GetSportSeasonSuppPlayerReviewFromSportSeasonSuppPlayerReviewDetails(record));
      return sportSeasonSuppPlayerReviews;
    }


  }
}
