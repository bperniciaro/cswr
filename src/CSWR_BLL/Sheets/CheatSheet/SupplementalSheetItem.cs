using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// Summary description for SupplementalSheetPlayer
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  [Serializable()]
  public class SupplementalSheetItem : SheetItem
  {

    public SupplementalSheetItem(int supplementalSheetID, int playerID, int seqno, string note, Dictionary<string, object> mappedProperties) : base(playerID, seqno, note, mappedProperties)
    {
      this.SupplementalSheetID = supplementalSheetID;
    }

    public SupplementalSheetItem() {}

    // Supplemental Sheet ID
    public int SupplementalSheetID {get;set;}

    /// <summary>
    /// Returns a SupplementalSheetItem object with the specified CheatSheetID and PlayerID
    /// </summary>
    public static SupplementalSheetItem GetSupplementalSheetItem(int suppSheetID, int playerID)
    {
      SupplementalSheetItem suppSheetItem = null;
      string key = "Sheets_SupplementalSheetItem_" + suppSheetID.ToString() + "_" + playerID.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        suppSheetItem = (SupplementalSheetItem)BizObject.Cache[key];
      }
      else
      {
        suppSheetItem = GetSupplementalSheetItemFromSupplementalSheetItemDetails(SiteProvider.Sheets.GetSupplementalSheetItem(suppSheetID, playerID));
        BaseSheet.CacheData(key, suppSheetItem);
      }
      return suppSheetItem;
    }

    /// <summary>
    /// Returns a collection of SupplementalSheetItem objects based on the supplementalSheetID specified.  This is
    /// generally done when loading an entire sheet.
    /// </summary>
    public static List<SupplementalSheetItem> GetSupplementalSheetItems(int supplementalSheetID)
    {
      List<SupplementalSheetItem> supplementalSheetItems = null;
      string key = "Sheets_SupplementalSheetItemsBySupplementalSheetID_" + supplementalSheetID.ToString();

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        supplementalSheetItems = (List<SupplementalSheetItem>)BizObject.Cache[key];
      }
      else
      {
        List<SupplementalSheetItemDetails> recordset = SiteProvider.Sheets.GetSupplementalSheetItems(supplementalSheetID);
        supplementalSheetItems = GetSupplementalSheetItemListFromSupplementalSheetItemDetailsList(recordset);
        BaseSheet.CacheData(key, supplementalSheetItems);
      }
      return supplementalSheetItems.GetRange(0, supplementalSheetItems.Count);
    }


    public static bool UpdateSupplementalSheetItem(int suppSheetID, int playerID, int seqno, string note, Dictionary<string, object> mappedProperties)
    {
      // if we update this item, clear that one item from cache
      BizObject.PurgeCacheItems("Sheets_SupplementalSheetItem_" + suppSheetID.ToString() + "_" + playerID.ToString());
      // if we update an item in a sheet, clear all caches related to that cheat sheet
      BizObject.PurgeCacheItems("Sheets_SupplementalSheetItemsBySupplementalSheetID_" + suppSheetID.ToString());  // clear any cache which pulls multiple records

      // load mapped properties 
      bool? sleeperTag = (mappedProperties[CSIProperty.Sleeper.ToString()] != null) ? (bool?)mappedProperties[CSIProperty.Sleeper.ToString()] : null;
      bool? bustTag = (mappedProperties[CSIProperty.Bust.ToString()] != null) ? (bool?)mappedProperties[CSIProperty.Bust.ToString()] : null;

      // build a cheat sheet entity to update, then use the module specific provider to update it
      SupplementalSheetItemDetails suppSheetItem = new SupplementalSheetItemDetails(suppSheetID, playerID, seqno, sleeperTag, bustTag, note);
      bool ret = SiteProvider.Sheets.UpdateSupplementalSheetItem(suppSheetItem);
      //bool ret2 = SiteProvider.Sheets.UpdateSupplementalSheetItemNote(suppSheetID, playerID, note);

      // update the supplemental sheet's last modified date
      SupplementalSheet sheetToUpdate = SupplementalSheet.GetSupplementalSheet(suppSheetID);
      sheetToUpdate.LastUpdated = DateTime.Now;
      sheetToUpdate.Update();

      return ret;
    }



    /// <summary>
    /// This method inserts a supplemental sheet item when performing screen scraping on supplemental sources 
    /// </summary>
    //public static bool InsertSupplementalSheetItem(int supplementalSheetID, int playerID, int seqNo, bool sleeperTag, bool bustTag, string note)
    public static int InsertSupplementalSheetItem(SupplementalSheetItem suppSheetItem)
    {
      // if we update an item in a sheet, clear all caches related to that cheat sheet
      BizObject.PurgeCacheItems("Sheets_SupplementalSheetItemsBySupplementalSheetID_" + suppSheetItem.SupplementalSheetID.ToString());  // clear any cache which pulls multiple records

      /* load mapped properties */
      // sleeper
      bool? sleeperTag = null;
      if (suppSheetItem.MappedProperties.ContainsKey(SSIProperty.Sleeper.ToString()))
      {
        if (suppSheetItem.MappedProperties[SSIProperty.Sleeper.ToString()] != null)
        {
          sleeperTag = (bool?)suppSheetItem.MappedProperties[CSIProperty.Sleeper.ToString()];
        }
      }
      // bust
      bool? bustTag = null;
      if (suppSheetItem.MappedProperties.ContainsKey(SSIProperty.Bust.ToString()))
      {
        if (suppSheetItem.MappedProperties[SSIProperty.Bust.ToString()] != null)
        {
          bustTag = (bool?)suppSheetItem.MappedProperties[CSIProperty.Bust.ToString()];
        }
      }

      // create an supplementalsheetitem entity to insert and use the specific provider to do the insert
      SupplementalSheetItemDetails record = new SupplementalSheetItemDetails(suppSheetItem.SupplementalSheetID, suppSheetItem.PlayerID,
        suppSheetItem.Seqno, sleeperTag, bustTag, suppSheetItem.Note);
      int itemID = SiteProvider.Sheets.InsertSupplementalSheetItem(record);

      return itemID;
    }


    /// <summary>
    /// Instance method for updating the current SuppSheetItem
    /// </summary>
    /// <returns></returns>
    public bool Update()
    {
      return UpdateSupplementalSheetItem(this.SupplementalSheetID, this.PlayerID, this.Seqno, this.Note, this.MappedProperties);
    }

    /// <summary>
    /// Returns a SupplementalSheetPlayer object filled with the data taken from the input SupplementalSheetPlayerDetails
    /// </summary>
    private static SupplementalSheetItem GetSupplementalSheetItemFromSupplementalSheetItemDetails(SupplementalSheetItemDetails supplementalSheetItem)
    {
      if (supplementalSheetItem == null)
        return null;
      else
      {
        Dictionary<string, object> mappedProperties = new Dictionary<string, object>();
        mappedProperties.Add(CSIProperty.Sleeper.ToString(), supplementalSheetItem.SleeperTag);
        mappedProperties.Add(CSIProperty.Bust.ToString(), supplementalSheetItem.BustTag);

        return new SupplementalSheetItem(supplementalSheetItem.SupplementalSheetID, supplementalSheetItem.PlayerID, supplementalSheetItem.Seqno,
          supplementalSheetItem.Note, mappedProperties);
      }
    }

    /// <summary>
    /// Returns a list of SupplementalSheetPlayer objects filled with the data taken from the input list of SupplementalSheetPlayerDetails
    /// </summary>
    private static List<SupplementalSheetItem> GetSupplementalSheetItemListFromSupplementalSheetItemDetailsList(List<SupplementalSheetItemDetails> recordset)
    {
      List<SupplementalSheetItem> supplementalSheetItems = new List<SupplementalSheetItem>();
      foreach (SupplementalSheetItemDetails record in recordset)
        supplementalSheetItems.Add(GetSupplementalSheetItemFromSupplementalSheetItemDetails(record));
      return supplementalSheetItems;
    }



  }
}