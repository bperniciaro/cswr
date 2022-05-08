using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Summary description for CheatSheetItem
/// </summary>

namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  public class ArchivedCheatSheetItem : SheetItem
  {
    public ArchivedCheatSheetItem(int archivedCheatSheetItemID, int playerID, int seqno, string note, Dictionary<string, object> mappedProperties)
      : base(playerID, seqno, note, mappedProperties)
    {
      this.ArchivedCheatSheetID = archivedCheatSheetItemID;
    }

    // Cheat Sheet ID
    public int ArchivedCheatSheetID { get; set; }

    /// <summary>
    /// Returns a collection with all the archived cheat sheet items.  
    /// </summary>
    public static List<ArchivedCheatSheetItem> GetArchivedCheatSheetItems(int archivedCheatSheetID)
    {
      List<ArchivedCheatSheetItem> cheatSheetItems = null;
      string key = "Sheets_ArchivedCheatSheetItems_" + archivedCheatSheetID.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        cheatSheetItems = (List<ArchivedCheatSheetItem>)BizObject.Cache[key];
      }
      else
      {
        List<ArchivedCheatSheetItemDetails> recordset = SiteProvider.Sheets.GetArchivedCheatSheetItems(archivedCheatSheetID);
        cheatSheetItems = GetArchivedCheatSheetItemListFromArchivedCheatSheetItemDetailsList(recordset);
        BaseSheet.CacheData(key, cheatSheetItems);
      }

      return cheatSheetItems.GetRange(0, cheatSheetItems.Count);

    }





    /// <summary>
    /// 
    /// </summary>
    /// <param name="archivedCheatSheetItem"></param>
    /// <returns></returns>
    public static int InsertArchivedCheatSheetItem(ArchivedCheatSheetItem archivedCheatSheetItem)
    {
      // clear the cache for 'all cheat sheets' and the cheat sheet being updated.
      BizObject.PurgeCacheItems("Sheets_archivedCheatSheetItems_" + archivedCheatSheetItem.ArchivedCheatSheetID);

      /* load mapped properties */
      // sleeper
      bool? sleeperTag = null;
      if (archivedCheatSheetItem.MappedProperties.ContainsKey(CSIProperty.Sleeper.ToString()))
      {
          sleeperTag = (bool?)archivedCheatSheetItem.MappedProperties[CSIProperty.Sleeper.ToString()];
      }
      
      // bust
      bool? bustTag = null;
      if (archivedCheatSheetItem.MappedProperties.ContainsKey(CSIProperty.Bust.ToString()))
      {
        bustTag = (bool?)archivedCheatSheetItem.MappedProperties[CSIProperty.Bust.ToString()];
      }
      // injured
      bool? injuredTag = null;
      if (archivedCheatSheetItem.MappedProperties.ContainsKey(CSIProperty.Injured.ToString()))
      {
        injuredTag = (bool?)archivedCheatSheetItem.MappedProperties[CSIProperty.Injured.ToString()];
      }

      var record = new ArchivedCheatSheetItemDetails(archivedCheatSheetItem.ArchivedCheatSheetID, 
        archivedCheatSheetItem.PlayerID, archivedCheatSheetItem.Seqno,
        sleeperTag, bustTag, injuredTag, archivedCheatSheetItem.Note);
      int itemID = SiteProvider.Sheets.InsertArchivedCheatSheetItem(record);

      return itemID;
    }








    /// <summary>
    /// Returns a ArchivedCheatSheetItem object filled with the data taken from the input CheatSheetItem
    /// </summary>
    private static ArchivedCheatSheetItem GetArchivedCheatSheetItemFromArchivedCheatSheetItemDetails(ArchivedCheatSheetItemDetails archivedCheatSheetDetails)
    {
      if (archivedCheatSheetDetails == null)
        return null;
      else
      {
        Dictionary<string, object> mappedProperties = GetMappedProperties(archivedCheatSheetDetails);

        return new ArchivedCheatSheetItem(archivedCheatSheetDetails.ArchivedCheatSheetID, archivedCheatSheetDetails.PlayerID, archivedCheatSheetDetails.Seqno,
                                    archivedCheatSheetDetails.Note, mappedProperties);
      }
    }

    /// <summary>
    /// Returns a list of CheatSheetItem objects filled with the data taken from the input list of CheatSheetItemDetails
    /// </summary>
    private static List<ArchivedCheatSheetItem> GetArchivedCheatSheetItemListFromArchivedCheatSheetItemDetailsList(List<ArchivedCheatSheetItemDetails> recordset)
    {
      List<ArchivedCheatSheetItem> archivedCheatSheetItems = new List<ArchivedCheatSheetItem>();
      foreach (ArchivedCheatSheetItemDetails record in recordset)
        archivedCheatSheetItems.Add(GetArchivedCheatSheetItemFromArchivedCheatSheetItemDetails(record));
      return archivedCheatSheetItems;
    }

    private static Dictionary<string, object> GetMappedProperties(ArchivedCheatSheetItemDetails cheatSheetItemDetails)
    {
      Dictionary<string, object> mappedProperties = new Dictionary<string, object>();
      if (cheatSheetItemDetails.SleeperTag != null)
      {
        mappedProperties.Add(CSIProperty.Sleeper.ToString(), cheatSheetItemDetails.SleeperTag);
      }
      if (cheatSheetItemDetails.BustTag != null)
      {
        mappedProperties.Add(CSIProperty.Bust.ToString(), cheatSheetItemDetails.BustTag);
      }
      if (cheatSheetItemDetails.InjuredTag != null)
      {
        mappedProperties.Add(CSIProperty.Injured.ToString(), cheatSheetItemDetails.InjuredTag);
      }
      return mappedProperties;
    }

  }
}