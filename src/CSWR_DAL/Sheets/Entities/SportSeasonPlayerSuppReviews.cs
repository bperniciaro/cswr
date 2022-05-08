/// <summary>
/// Summary description for SportDetails
/// </summary>
/// 
namespace BP.CheatSheetWarRoom.DAL
{
  public class SportSeasonSuppPlayerReviewDetails
  {
    public SportSeasonSuppPlayerReviewDetails(string sportCode, string seasonCode, int supplementalSourceID, int playerID, string reviewURL)
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


  }


}