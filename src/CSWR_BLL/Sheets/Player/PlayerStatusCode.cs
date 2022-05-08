using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  [Serializable()]
  public class PlayerStatusCode : BaseSheet
  {
    public PlayerStatusCode(string statusCode, string name, string description, bool suppInfoRequired, string suppInfoLabel,
      string suppInfoExample, string suppInstructions, bool countRequired, string countLabel, int countExample, string countInstructions,
      int seqno, int priority, bool dynamic)
    {
      this.StatusCode = statusCode;
      this.Name = name;
      this.Description = description;
      this.SuppInfoRequired = suppInfoRequired;
      this.SuppInfoLabel = suppInfoLabel;
      this.SuppInfoExample = suppInfoExample;
      this.SuppInfoInstructions = suppInstructions;
      this.CountRequired = countRequired;
      this.CountLabel = countLabel;
      this.CountExample = countExample;
      this.CountInstructions = countInstructions;
      this.Seqno = seqno;
      this.Priority = priority;
      this.Dynamic = dynamic;
    }


    public string StatusCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool SuppInfoRequired { get; set; }
    public string SuppInfoLabel { get; set; }
    public string SuppInfoExample { get; set; }
    public string SuppInfoInstructions { get; set; }
    public bool CountRequired { get; set; }
    public string CountLabel { get; set; }
    public int CountExample { get; set; }
    public string CountInstructions { get; set; }
    public int Seqno { get; set; }
    public int Priority { get; set; }
    public bool Dynamic { get; set; }


    /// <summary>
    /// Returns a collection with all the sports
    /// </summary>
    public static List<PlayerStatusCode> GetPlayerStatusCodes(string sportCode)
    {
      List<PlayerStatusCode> playerStatusCodes = null;
      var key = "Sheets_PlayerStatusCodes_" + sportCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        playerStatusCodes = (List<PlayerStatusCode>)BizObject.Cache[key];
      }
      else
      {
        List<PlayerStatusCodeDetails> recordset = SiteProvider.Sheets.GetPlayerStatusCodes(sportCode);
        playerStatusCodes = GetPlayerStatusCodeListFromPlayerStatusCodeDetailsList(recordset);
        BaseSheet.CacheData(key, playerStatusCodes);
      }
      return playerStatusCodes.GetRange(0, playerStatusCodes.Count);

    }


    /// <summary>
    /// Returns a PlayerStatusCode object based on the requested statusCode
    /// </summary>
    public static PlayerStatusCode GetPlayerStatusCode(string statusCode)
    {
      PlayerStatusCode playerStatusCode = null;
      var key = "Sheets_PlayerStatusCode_" + statusCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        playerStatusCode = (PlayerStatusCode)BizObject.Cache[key];
      }
      else
      {
        PlayerStatusCodeDetails recordset = SiteProvider.Sheets.GetPlayerStatusCode(statusCode);
        playerStatusCode = GetPlayerStatusCodeFromPlayerStatusCodeDetails(recordset);
        BaseSheet.CacheData(key, playerStatusCode);
      }
      return playerStatusCode;
    }



    /// <summary>
    /// Converts a Details entity object to a business object
    /// </summary>
    private static PlayerStatusCode GetPlayerStatusCodeFromPlayerStatusCodeDetails(PlayerStatusCodeDetails playerStatusCode)
    {
      if (playerStatusCode == null)
        return null;
      else
      {
        return new PlayerStatusCode(playerStatusCode.StatusCode, playerStatusCode.Name, playerStatusCode.Description,
                                    playerStatusCode.SuppInfoRequired, playerStatusCode.SuppInfoLabel, playerStatusCode.SuppInfoExample,
                                    playerStatusCode.SuppInfoInstructions, playerStatusCode.CountRequired, playerStatusCode.CountLabel, 
                                    playerStatusCode.CountExample, playerStatusCode.CountInstructions,
                                    playerStatusCode.Seqno, playerStatusCode.Priority, playerStatusCode.Dynamic);
      }
    }

    /// <summary>
    /// Converts a collection of Details objects to a collection of business objects
    /// </summary>
    private static List<PlayerStatusCode> GetPlayerStatusCodeListFromPlayerStatusCodeDetailsList(List<PlayerStatusCodeDetails> recordset)
    {
      List<PlayerStatusCode> playerStatusCodes = new List<PlayerStatusCode>();
      foreach (PlayerStatusCodeDetails record in recordset)
        playerStatusCodes.Add(GetPlayerStatusCodeFromPlayerStatusCodeDetails(record));
      return playerStatusCodes;
    }


  }

}
