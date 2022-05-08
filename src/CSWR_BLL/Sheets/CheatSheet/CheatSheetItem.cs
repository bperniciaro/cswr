using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  [Serializable()]
  public class CheatSheetItem : SheetItem
  {
    public CheatSheetItem(int cheatSheetID, int playerID, int seqno, string note, Dictionary<string, object> mappedProperties) : base(playerID, seqno, note, mappedProperties)
    {
      this.CheatSheetID = cheatSheetID;
    }

    // Cheat Sheet ID
    public int CheatSheetID {get;set;}

    /// <summary>
    /// Returns a collection with all the cheatsheetitems.  We don't cache here because we don't want this cached collection to
    /// interfere with a RAC or FOO collection since there are fewer properties here and the keys would be the same
    /// </summary>
    public static List<CheatSheetItem> GetCheatSheetItems(int cheatSheetID)
    {
      List<CheatSheetItem> cheatSheetItems = null;
      string key = "Sheets_CheatSheetItems_" + cheatSheetID.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        cheatSheetItems = (List<CheatSheetItem>)BizObject.Cache[key];
      }
      else
      {
        List<CheatSheetItemDetails> recordset = SiteProvider.Sheets.GetCheatSheetItems(cheatSheetID);
        cheatSheetItems = GetCheatSheetItemListFromCheatSheetItemDetailsList(recordset);
        BaseSheet.CacheData(key, cheatSheetItems);
      }

      return cheatSheetItems.GetRange(0, cheatSheetItems.Count);

    }


    /// <summary>
    /// Returns a CheatSheetItem object with the specified CheatSheetID and PlayerID
    /// </summary>
    public static CheatSheetItem GetCheatSheetItem(int cheatSheetID, int playerID)
    {
      CheatSheetItem cheatSheetItem = null;
      string key = "Sheets_CheatSheetItem_" + cheatSheetID.ToString() + "_" + playerID.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        cheatSheetItem = (CheatSheetItem)BizObject.Cache[key];
      }
      else
      {
        cheatSheetItem = GetCheatSheetItemFromCheatSheetItemDetails(SiteProvider.Sheets.GetCheatSheetItem(cheatSheetID, playerID));
        BaseSheet.CacheData(key, cheatSheetItem);
      }
      return cheatSheetItem;
    }


    /// <summary>
    /// Update a cheat sheet item based on the parameters specified
    /// </summary>
    /// <param name="cheatSheetID"></param>
    /// <param name="playerID"></param>
    /// <param name="seqno"></param>
    /// <param name="sleeperTag"></param>
    /// <param name="bustTag"></param>
    /// <param name="injuredTag"></param>
    /// <param name="note"></param>
    /// <returns></returns>
    //public static bool UpdateCheatSheetItem(int cheatSheetID, int playerID, int seqno, bool? sleeperTag, bool? bustTag, bool? injuredTag, string note)
    public static bool UpdateCheatSheetItem(int cheatSheetID, int playerID, int seqno, string note, Dictionary<string, object> mappedProperties)
    {
      BizObject.PurgeCacheItems("Sheets_CheatSheetItems_" + cheatSheetID.ToString());
      BizObject.PurgeCacheItems("Sheets_CheatSheetItem_" + cheatSheetID.ToString() + "_" + playerID.ToString());
      
      // load mapped properties 
      bool? sleeperTag = (mappedProperties[CSIProperty.Sleeper.ToString()] != null) ? (bool?)mappedProperties[CSIProperty.Sleeper.ToString()] : null;
      bool? bustTag = (mappedProperties[CSIProperty.Bust.ToString()] != null) ? (bool?)mappedProperties[CSIProperty.Bust.ToString()] : null;
      bool? injuredTag = (mappedProperties[CSIProperty.Injured.ToString()] != null) ? (bool?)mappedProperties[CSIProperty.Injured.ToString()] : null;

      // build a cheat sheet entity to update, then use the module specific provider to update it
      CheatSheetItemDetails cheatSheetItem = new CheatSheetItemDetails(cheatSheetID, playerID, seqno, sleeperTag, bustTag, injuredTag, note);
      bool ret = SiteProvider.Sheets.UpdateCheatSheetItem(cheatSheetItem);

      // update the cheat sheet's last modified date
      CheatSheet sheetToUpdate = CheatSheet.GetCheatSheet(cheatSheetID);
      sheetToUpdate.LastUpdated = DateTime.Now;
      sheetToUpdate.Update();

      return ret;
    }

    /// <summary>
    /// Instance method for updating the current CheatSheetItem
    /// </summary>
    /// <returns></returns>
    public bool Update()
    {
      return UpdateCheatSheetItem(this.CheatSheetID, this.PlayerID, this.Seqno, this.Note, this.MappedProperties);
    }


    /// <summary>
    /// Inserts a cheat sheet item using the heatSheetItem specified
    /// </summary>
    /// <param name="CheatSheetItem"></param>
    /// <returns></returns>
    public static int InsertCheatSheetItem(CheatSheetItem cheatSheetItem)
    {
      // clear the cache for 'all cheat sheets' and the cheat sheet being updated.
      BizObject.PurgeCacheItems("Sheets_CheatSheetItems_" + cheatSheetItem.CheatSheetID.ToString());
                   
      /* load mapped properties */
      // sleeper
      bool? sleeperTag = null;
      if(cheatSheetItem.MappedProperties[CSIProperty.Sleeper.ToString()] != null)  
      {
        sleeperTag = (bool?)cheatSheetItem.MappedProperties[CSIProperty.Sleeper.ToString()];
      }
      // bust
      bool? bustTag = null;
      if(cheatSheetItem.MappedProperties[CSIProperty.Bust.ToString()] != null)  
      {
        bustTag = (bool?)cheatSheetItem.MappedProperties[CSIProperty.Bust.ToString()];
      }
      // injured
      bool? injuredTag = null;
      if(cheatSheetItem.MappedProperties[CSIProperty.Injured.ToString()] != null)  
      {
        injuredTag = (bool?)cheatSheetItem.MappedProperties[CSIProperty.Injured.ToString()];
      }

      CheatSheetItemDetails record = new CheatSheetItemDetails(cheatSheetItem.CheatSheetID, cheatSheetItem.PlayerID, cheatSheetItem.Seqno,
        sleeperTag, bustTag, injuredTag, cheatSheetItem.Note);
      int itemID = SiteProvider.Sheets.InsertCheatSheetItem(record);

      // update the cheat sheet's last modified date
      CheatSheet sheetToUpdate = CheatSheet.GetCheatSheet(cheatSheetItem.CheatSheetID);
      sheetToUpdate.LastUpdated = DateTime.Now;
      sheetToUpdate.Update();

      return itemID;
    }


    /// <summary>
    /// Returns a CheatSheetItem object filled with the data taken from the input CheatSheetItem
    /// </summary>
    private static CheatSheetItem GetCheatSheetItemFromCheatSheetItemDetails(CheatSheetItemDetails cheatSheetItemDetails)
    {
      if (cheatSheetItemDetails == null)
        return null;
      else
      {
        Dictionary<string, object> mappedProperties = GetMappedProperties(cheatSheetItemDetails);

        return new CheatSheetItem(cheatSheetItemDetails.CheatSheetID, cheatSheetItemDetails.PlayerID, cheatSheetItemDetails.Seqno, 
                                    cheatSheetItemDetails.Note, mappedProperties);
      }
    }

    /// <summary>
    /// Returns a list of CheatSheetItem objects filled with the data taken from the input list of CheatSheetItemDetails
    /// </summary>
    private static List<CheatSheetItem> GetCheatSheetItemListFromCheatSheetItemDetailsList(List<CheatSheetItemDetails> recordset)
    {
      List<CheatSheetItem> cheatSheetItems = new List<CheatSheetItem>();
      foreach (CheatSheetItemDetails record in recordset)
        cheatSheetItems.Add(GetCheatSheetItemFromCheatSheetItemDetails(record));
      return cheatSheetItems;
    }

    private static Dictionary<string, object> GetMappedProperties(CheatSheetItemDetails cheatSheetItemDetails)
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
