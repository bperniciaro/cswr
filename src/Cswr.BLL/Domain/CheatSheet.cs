using System;
using System.Collections.Generic;
using System.Text;

namespace Cswr.BLL.Domain
{

    //public CheatSheet(int cheatSheetID, string username, string seasonCode, string sportCode, string sheetName, string statsSeasonCode,
    //                    DateTime created, DateTime lastUpdated, Dictionary<string, object> mappedProperties)
    //{
    //    this.CheatSheetID = cheatSheetID;
    //    this.Username = username;
    //    this.SeasonCode = seasonCode;
    //    this.SportCode = sportCode;
    //    this.SheetName = sheetName;
    //    this.StatsSeasonCode = statsSeasonCode;
    //    this.Created = created;
    //    this.LastUpdated = lastUpdated;
    //    this.MappedProperties = mappedProperties;
    //}


    //public int CheatSheetID { get; set; }
    //private string _userName { get; set; }
    //public string Username
    //{
    //    get
    //    {
    //        return _userName.Trim();
    //    }
    //    set
    //    {
    //        _userName = value;
    //    }
    //}

    //public string SeasonCode { get; set; }
    //public string SportCode { get; set; }
    //public string SheetName { get; set; }
    //public string StatsSeasonCode { get; set; }
    //public DateTime Created { get; set; }
    //public DateTime LastUpdated { get; set; }
    //public Dictionary<string, object> MappedProperties { get; set; }

    //private List<CheatSheetPosition> _cheatSheetPositions = null;
    //public List<CheatSheetPosition> CheatSheetPositions
    //{
    //    get
    //    {
    //        if (_cheatSheetPositions == null)
    //        {
    //            _cheatSheetPositions = CheatSheetPosition.GetCheatSheetPositions(this.CheatSheetID);
    //        }
    //        return _cheatSheetPositions;
    //    }
    //}

    //private List<Position> _positions = null;
    //public List<Position> Positions
    //{
    //    get
    //    {
    //        if (_positions == null)
    //        {
    //            _positions = new List<Position>();
    //            foreach (CheatSheetPosition currentCheatSheetPosition in this.CheatSheetPositions)
    //            {
    //                _positions.Add(Position.GetPosition(currentCheatSheetPosition.PositionCode));
    //            }
    //        }
    //        return _positions;
    //    }
    //}

}
