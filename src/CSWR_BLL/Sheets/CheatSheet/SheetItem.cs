using System;
using System.Collections.Generic;

/// <summary>
/// Summary description for SheetItem
/// </summary>
/// 
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  [Serializable()]
  public class SheetItem : BaseSheet
  {
    public SheetItem(int playerID, int seqno, string note, Dictionary<string, object> mappedProperties)
    {
      this.PlayerID = playerID;
      this.Seqno = seqno;
      this.Note = note;
      this.MappedProperties = mappedProperties;
    }

    public SheetItem() { }

    /// <summary>
    /// The PlayerID of the sheet item
    /// </summary>
    public int PlayerID {get;set;}

    /// <summary>
    /// Represents the ordering of the item in the cheat sheet, starting with 1
    /// </summary>
    public int Seqno {get;set;}

    /// <summary>
    /// A configurable note for all types of sheets
    /// </summary>
    public string Note { get; set; }

    // Mapped Properties
    public Dictionary<string, object> MappedProperties { get; set; }


    /// <summary>
    /// A lazy-load reference to the Player referenced in the SheetItem
    /// </summary>
    //private Player _player = null;
    public Player Player
    {
      get
      {
        //if (_player == null)
        //  _player = Player.GetPlayer(this.PlayerID);
        return Player.GetPlayer(this.PlayerID);
      }
    }


    /// <summary>
    /// This represents the full name of the player being referenced.  This is needed to
    /// bind a dropdown
    /// </summary>
    private string _fullName = "";
    public string FullName
    {
      get
      {
        if (_fullName == "")
        {
          if (this.Player != null)
          {
            return this.Player.FullName;
          }
        }
        return _fullName;
      }
    }

    /// <summary>
    /// This represents the full name of the player being referenced.  This is needed to
    /// bind a dropdown
    /// </summary>
    private string _fullNameLastFirst = "";
    public string FullNameLastFirst
    {
      get
      {
        if (_fullNameLastFirst == "")
        {
          if (this.Player != null)
          {
            return this.Player.FullNameLastFirst;
          }
        }
        return _fullNameLastFirst;
      }
    }


    private string _fullNameAndPosition = "";
    public string FullNameAndPosition
    {
      get
      {
        if (_fullNameAndPosition == "")
        {
          if (this.Player != null)
          {
            _fullNameAndPosition = this.Player.FullNameAndPosition;
          }
        }
        return _fullNameAndPosition;
      }
    }

  
  }
}