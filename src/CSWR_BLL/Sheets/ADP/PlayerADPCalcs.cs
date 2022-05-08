namespace BP.CheatSheetWarRoom.BLL.Sheets
{
  /// <summary>
  /// Class for keeping count of player rankings and calculating the final ADP
  /// </summary>
  public class PlayerADPCalcs
  {
    public PlayerADPCalcs() { }
    public PlayerADPCalcs(int playerID, int rankTotal, int rankCounter)
    {
      this.PlayerID = playerID;
      this.RankTotal = rankTotal;
      this.RankCounter = rankCounter;
    }

    public int PlayerID { get; set; }
    public int RankTotal { get; set; }
    public int RankCounter { get; set; }

    public double GetADP()
    {
      return (double)this.RankTotal / this.RankCounter;
    }
  }
}
