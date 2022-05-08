using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;
using BP.CheatSheetWarRoom.UI.UserControls;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class FootballSleepers : BasePage
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        LoadSocialTags();
        LoadPageParameters();
        LoadSleepers();
        LoadAds();
        
      }
    }

    private void LoadAds()
    {
        if(!Globals.CSWRSettings.EnableAdvertisements)
        {
            panTopAd.Visible = false;
        }
    }

        private void LoadPageParameters()
    {
      string currentSeason = FOO.CurrentSeason;
      Page.Title = currentSeason + " Fantasy Football Sleeper Projections - Players to Target";
      Page.MetaDescription = "Our " + currentSeason + " fantasy football sleepers are players that we believe " +
                                "will produce substantially more fantasy points than their widely-accepted, projected point output.";
      litMainHeader.Text = currentSeason + " Fantasy Sleepers";

      hlRankingsListYear.Text = FOO.CurrentSeason + " NFL player rankings";
    }


    private void LoadSleepers()
    {
      //Quarterbacks
      List<PotentialSleeper> qbSleepers = DetermineSleepers(FOOPositionsOffense.QB.ToString()).OrderByDescending(x => x.RankDifference).ToList();
      if(qbSleepers.Count > 0)  
      {
        repQBSleepers.DataSource = qbSleepers;
        repQBSleepers.DataBind();
      }

      //Running Backs
      List<PotentialSleeper> rbSleepers = DetermineSleepers(FOOPositionsOffense.RB.ToString()).OrderByDescending(x => x.RankDifference).ToList();
      if(rbSleepers.Count > 0)  
      {
        repRBSleepers.DataSource = rbSleepers;
        repRBSleepers.DataBind();
      }


      //Wide Receivers
      List<PotentialSleeper> wrSleepers = DetermineSleepers(FOOPositionsOffense.WR.ToString()).OrderByDescending(x => x.RankDifference).ToList();
      if (wrSleepers.Count > 0)
      {
        repWRSleepers.DataSource = wrSleepers;
        repWRSleepers.DataBind();
      }

      //Tight Ends
      List<PotentialSleeper> teSleepers = DetermineSleepers(FOOPositionsOffense.TE.ToString()).OrderByDescending(x => x.RankDifference).ToList();
      if (teSleepers.Count > 0)
      {
        repTESleepers.DataSource = teSleepers;
        repTESleepers.DataBind();
      }

      //Kickers
      List<PotentialSleeper> kSleepers = DetermineSleepers(FOOPositionsOffense.K.ToString()).OrderByDescending(x => x.RankDifference).ToList();
      if (kSleepers.Count > 0)
      {
        repKSleepers.DataSource = kSleepers;
        repKSleepers.DataBind();
      }

      //Defenses
      List<PotentialSleeper> dstSleepers = DetermineSleepers(FOOPositionsOffense.DF.ToString()).OrderByDescending(x => x.RankDifference).ToList();
      if (dstSleepers.Count > 0)
      {
        repDFSleepers.DataSource = dstSleepers;
        repDFSleepers.DataBind();
      }

    }

    private List<PotentialSleeper> DetermineSleepers(string positionCode)
    {
      // CSWR Rankings
      SupplementalSource cswrSource = SupplementalSource.GetSupplementalSource("CSWR");
      SportSeason currentSportSeason = SportSeason.GetCurrentSportSeason(FOO.FOOString);
      SupplementalSheet cswrRankingSheet = SupplementalSheet.GetSupplementalSheet(currentSportSeason.SeasonCode, cswrSource.SupplementalSourceID, FOO.FOOString, positionCode);
      List<SupplementalSheetItem> cswrRankingItems = SupplementalSheetItem.GetSupplementalSheetItems(cswrRankingSheet.SupplementalSheetID);

      // CBSSports Rankings
      SupplementalSource cbsSource = SupplementalSource.GetSupplementalSource("CBS");
      SupplementalSheet cbsRankingSheet = SupplementalSheet.GetSupplementalSheet(currentSportSeason.SeasonCode, cbsSource.SupplementalSourceID, FOO.FOOString, positionCode);

      // Identify Sleepers
      List<PotentialSleeper> validSleepers = new List<PotentialSleeper>();
      List<PotentialSleeper> almostSleepers = new List<PotentialSleeper>();

      if (cbsRankingSheet != null)
      {
        List<SupplementalSheetItem> cbsRankingItems = SupplementalSheetItem.GetSupplementalSheetItems(cbsRankingSheet.SupplementalSheetID);

        foreach (SupplementalSheetItem targetItem in cswrRankingItems)
        {
          SupplementalSheetItem cbsItem = cbsRankingItems.Find(delegate(SupplementalSheetItem x) { return x.PlayerID == targetItem.PlayerID; });

          // determine ranking differences for player who are in ranked by both CSWR and CBS
          if (cbsItem != null)
          {
            int rankDifference = cbsItem.Seqno - targetItem.Seqno;

            if (rankDifference >= Globals.DefaultSleeperBustDifferential)
            {
              validSleepers.Add(new PotentialSleeper(targetItem.PlayerID, targetItem.Player.PositionCode, targetItem.Seqno, cbsItem.Seqno, 0, rankDifference));
            }
            else
            {
              almostSleepers.Add(new PotentialSleeper(targetItem.PlayerID, targetItem.Player.PositionCode, targetItem.Seqno, cbsItem.Seqno, 0, rankDifference));
            }
          }
          // handle case where I have a player ranked highly but CBS doesn't have them ranked at all
          else
          {
            int lowestCSWRRankToBeSleeper = cbsRankingItems.Count - Globals.DefaultSleeperBustDifferential;
            if (targetItem.Seqno <= lowestCSWRRankToBeSleeper)
            {
              int rankDifferential = cbsRankingItems.Count - targetItem.Seqno;
              validSleepers.Add(new PotentialSleeper(targetItem.PlayerID, targetItem.Player.PositionCode, targetItem.Seqno, 0, 0, rankDifferential));
            }
          }
        }

        // if there are no sleepers found then add the two players with the highest rank difference
        if (validSleepers.Count < Globals.MinSleeperBustListingCount)
        {
          var playerAddCount = Globals.MinSleeperBustListingCount - validSleepers.Count;
          validSleepers.AddRange(almostSleepers.OrderByDescending(x => x.RankDifference).Take(playerAddCount));
        }

      }

      else
      {
        panSleepersContainer.Visible = false;
        panNoSleepers.Visible = true;

        litCurrentSeason.Text = FOO.CurrentSeason;
      }
      return validSleepers;
      
    }



    protected void repSleepers_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        PotentialSleeper boundItem = (PotentialSleeper)e.Item.DataItem;

        CSWRFOOBustSleeperItemTemplate itemTemplate = (CSWRFOOBustSleeperItemTemplate)e.Item.FindControl("cfbsitBustSleeperItemTemplate");
        itemTemplate.PlayerID = boundItem.PlayerID;
        itemTemplate.ComparisonSource = BustSleeperComparisonSource.CBS;
        itemTemplate.Status = SSIProperty.Sleeper;
        itemTemplate.CSWRRank = boundItem.CSWRRank;
        itemTemplate.CBSRank = boundItem.CBSRank;
        itemTemplate.ADP = boundItem.ADP;
        itemTemplate.RankDifference = boundItem.RankDifference.ToString();
        itemTemplate.PlayerType = DifferentialPlayerType.Sleeper;
        itemTemplate.LoadTemplate();
      }
    }

    public class PotentialSleeper
    {
      public PotentialSleeper(int playerID, string positionCode, int cswrRank, int cbsRank, int adp, int rankDifference)
      {
        this.PlayerID = playerID;
        this.PositionCode = positionCode;
        this.CSWRRank = cswrRank;
        this.CBSRank = cbsRank;
        this.ADP = adp;
        this.RankDifference = rankDifference;
      }

      public PotentialSleeper() { }

      public int PlayerID { get; set; }
      public string PositionCode { get; set; }
      public int CSWRRank { get; set; }
      public int CBSRank { get; set; }
      public int ADP { get; set; }
      public int RankDifference { get; set; }
    }

    private void LoadSocialTags()
    {
      //SportMaster myMaster = (SportMaster)this.Page.Master;
      //myMaster.OpenGraphImage = "https://www.cheatsheetwarroom.com/images/socialsharing/fantasy-football-sleepers.jpg";
      //myMaster.SchemaOrgImage = "https://www.cheatsheetwarroom.com/images/socialsharing/fantasy-football-sleepers.jpg";
      //myMaster.TwitterImage = "https://www.cheatsheetwarroom.com/images/socialsharing/fantasy-football-sleepers.jpg";
    }


  }
}