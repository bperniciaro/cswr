using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for PlayerStatDetails
/// </summary>
namespace BP.CheatSheetWarRoom.DAL
{
  public class FootballPlayerWeeklyStatDetails
  {
    public FootballPlayerWeeklyStatDetails(string seasonCode, int playerID, int week, string statCode, double statValue)
    {
      this.SeasonCode = seasonCode;
      this.PlayerID = playerID;
      this.Week = week;
      this.StatCode = statCode;
      this.StatValue = statValue;

    }

    // Season Code
    private string _seasonCode = "";
    public string SeasonCode
    {
      get { return _seasonCode; }
      set { _seasonCode = value; }
    }

    // Player ID
    public int _playerID = 0;
    public int PlayerID
    {
      get { return _playerID; }
      set { _playerID = value; }
    }

    // Week
    private int _week = 0;
    public int Week
    {
      get { return _week; }
      set { _week = value; }
    }

    // Stat Code
    private string _statCode = "";
    public string StatCode
    {
      get { return _statCode; }
      set { _statCode = value; }
    }

    // Stat Value
    private double _statValue = 0;
    public double StatValue
    {
      get { return _statValue; }
      set { _statValue = value; }
    }

  }
}