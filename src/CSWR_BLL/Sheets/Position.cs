using System;
using System.Collections.Generic;
using BP.CheatSheetWarRoom.DAL;

/// <summary>
/// A position which which a player holds in some sport
/// </summary>
namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  [Serializable]
  public class Position : BaseSheet
  {


    /// <summary>
    /// Constructor which fully-loads a Position object.
    /// </summary>
    /// <param name="positionCode"></param>
    /// <param name="name"></param>
    /// <param name="abbreviation"></param>
    public Position(string positionCode, string name, string abbreviation)
    {
      this.PositionCode = positionCode;
      this.Name = name;
      this.Abbreviation = abbreviation;
    }

    public Position() { }


    /// <summary>
    /// The specific code which represents a position
    /// </summary>
    private string _positionCode = String.Empty;
    public string PositionCode
    {
      get
      {
        return _positionCode.Trim();
      }
      set
      {
        _positionCode = value;
      }
    }

    /// <summary>
    /// The name assocated with the position
    /// </summary>
    public string Name {get;set;}

    /// <summary>
    /// The abbreviation of a position
    /// </summary>
    public string Abbreviation {get;set;}

    /// <summary>
    /// Returns a Position object with the specified code
    /// </summary>
    public static Position GetPosition(string positionCode)
    {
      Position position = null;
      string key = "Sheets_Position_" + positionCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        position = (Position)BizObject.Cache[key];
      }
      else
      {
        position = GetPositionFromPositionDetails(SiteProvider.Sheets.GetPosition(positionCode));
        BaseSheet.CacheData(key, position);
      }
      return position;
    }


    /// <summary>
    /// Returns all position for the specified sport
    /// </summary>
    public static List<Position> GetPositions(string sportCode)
    {
      List<Position> positions = null;
      string key = "Sheets_Positions_" + sportCode;

      if (BaseSheet.Settings.EnableCaching && BizObject.Cache[key] != null)
      {
        positions = (List<Position>)BizObject.Cache[key];
      }
      else
      {
        List<PositionDetails> recordset = SiteProvider.Sheets.GetPositions(sportCode);
        positions = GetPositionListFromPositionDetailsList(recordset);
        BaseSheet.CacheData(key, positions);
      }
      return positions.GetRange(0, positions.Count);
    }




    /// <summary>
    /// Returns a Position object filled with the data taken from the input PositionDetails
    /// </summary>
    private static Position GetPositionFromPositionDetails(PositionDetails position)
    {
      if (position == null)
        return null;
      else
      {
        return new Position(position.PositionCode, position.Name, position.Abbreviation);
      }
    }

    /// <summary>
    /// Returns a list of Position objects filled with the data taken from the input list of PositionDetails
    /// </summary>
    private static List<Position> GetPositionListFromPositionDetailsList(List<PositionDetails> recordset)
    {
      List<Position> positions = new List<Position>();
      foreach (PositionDetails record in recordset)
        positions.Add(GetPositionFromPositionDetails(record));
      return positions;
    }






  }

}