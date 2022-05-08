using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;
using BP.CheatSheetWarRoom.UI.UserControls;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class FootballBusts : BasePage
  {

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        //LoadSocialTags();
        LoadPageParameters();
        LoadSleepers();
        LoadAds();
      }
    }

    private void LoadAds()
    {
        if (!Globals.CSWRSettings.EnableAdvertisements)
        {
            panTopAd.Visible = false;
        }
    }

    private void LoadPageParameters()
    {
      string currentSeason = FOO.CurrentSeason;
      Page.Title = currentSeason + " Fantasy Football Bust Projections - Top Players to Avoid";
      Page.MetaDescription = "Our " + currentSeason + " fantasy football busts are players that we believe " +
                                "will produce substantially less fantasy points than their widely-accepted, projected point output.";
      litMainHeader.Text = currentSeason + " Fantasy Football Busts";

      hlRankingsListYear.Text = FOO.CurrentSeason + " NFL football rankings";

    }

    private void LoadSleepers()
    {
      // Quarterbacks
      List<PotentialBust> qbBusts = DetermineBusts(FOOPositionsOffense.QB.ToString()).OrderByDescending(x => x.RankDifference).ToList();
      if (qbBusts.Count > 0)
      {
        repQBBusts.DataSource = qbBusts;
        repQBBusts.DataBind();
      }

      // Running Backs
      List<PotentialBust> rbBusts = DetermineBusts(FOOPositionsOffense.RB.ToString()).OrderByDescending(x => x.RankDifference).ToList();
      if (rbBusts.Count > 0)
      {
        repRBBusts.DataSource = rbBusts;
        repRBBusts.DataBind();
      }

      // Wide Receivers
      List<PotentialBust> wrBusts = DetermineBusts(FOOPositionsOffense.WR.ToString()).OrderByDescending(x => x.RankDifference).ToList();
      if (wrBusts.Count > 0)
      {
        repWRBusts.DataSource = wrBusts;
        repWRBusts.DataBind();
      }

      // Tight Ends
      List<PotentialBust> teBusts = DetermineBusts(FOOPositionsOffense.TE.ToString()).OrderByDescending(x => x.RankDifference).ToList();
      if (teBusts.Count > 0)
      {
        repTEBusts.DataSource = teBusts;
        repTEBusts.DataBind();
      }

      // Kicker
      List<PotentialBust> kBusts = DetermineBusts(FOOPositionsOffense.K.ToString()).OrderByDescending(x => x.RankDifference).ToList();
      if (kBusts.Count > 0)
      {
        repKBusts.DataSource = kBusts;
        repKBusts.DataBind();
      }

      // Defenses
      List<PotentialBust> dstBusts = DetermineBusts(FOOPositionsOffense.DF.ToString()).OrderByDescending(x => x.RankDifference).ToList();
      if (dstBusts.Count > 0)
      {
        repDFBusts.DataSource = dstBusts;
        repDFBusts.DataBind();
      }
    }

    private List<PotentialBust> DetermineBusts(string positionCode)
    {
      // CSWR Rankings
      SupplementalSource cswrSource = SupplementalSource.GetSupplementalSource("CSWR");
      SportSeason currentSportSeason = SportSeason.GetCurrentSportSeason(FOO.FOOString);
      SupplementalSheet cswrRankingSheet = SupplementalSheet.GetSupplementalSheet(currentSportSeason.SeasonCode, cswrSource.SupplementalSourceID, FOO.FOOString, positionCode);
      List<SupplementalSheetItem> cswrRankingItems = SupplementalSheetItem.GetSupplementalSheetItems(cswrRankingSheet.SupplementalSheetID);

      // CBSSports Rankings
      SupplementalSource cbsSource = SupplementalSource.GetSupplementalSource("CBS");
      SupplementalSheet cbsRankingSheet = SupplementalSheet.GetSupplementalSheet(currentSportSeason.SeasonCode, cbsSource.SupplementalSourceID, FOO.FOOString, positionCode);

      // Identify Busts
      // Identify Sleepers
      List<PotentialBust> validBusts = new List<PotentialBust>();
      List<PotentialBust> almostBusts = new List<PotentialBust>();

      if (cbsRankingSheet != null)
      {
        List<SupplementalSheetItem> cbsRankingItems = SupplementalSheetItem.GetSupplementalSheetItems(cbsRankingSheet.SupplementalSheetID);


        foreach (SupplementalSheetItem targetItem in cswrRankingItems)
        {
          SupplementalSheetItem cbsItem = cbsRankingItems.Find(delegate(SupplementalSheetItem x) { return x.PlayerID == targetItem.PlayerID; });
          if (cbsItem != null)
          {
            int rankDifference = targetItem.Seqno - cbsItem.Seqno;

            if (rankDifference > Globals.DefaultSleeperBustDifferential)
            {
              validBusts.Add(new PotentialBust(targetItem.PlayerID, targetItem.Player.PositionCode, targetItem.Seqno, cbsItem.Seqno, 0, rankDifference));
            }
            else
            {
              almostBusts.Add(new PotentialBust(targetItem.PlayerID, targetItem.Player.PositionCode, targetItem.Seqno, cbsItem.Seqno, 0, rankDifference));
            }
          }
          // handle cases where CBS ranked a player but I did not
          else
          {
            // there really won't be any cases of CBS having a player ranked and CSWR not have them ranked.  If this
            // ever does happen, we'll catch it in the validation process

          }
        }

        // if there are no sleepers found then add the two players with the highest rank difference
        if (validBusts.Count < Globals.MinSleeperBustListingCount)
        {
          var playerAddCount = Globals.MinSleeperBustListingCount - validBusts.Count;
          validBusts.AddRange(almostBusts.OrderByDescending(x => x.RankDifference).Take(playerAddCount));
        }
      }
      else
      {
        panBustsContainer.Visible = false;
        panNoBusts.Visible = true;

        litCurrentSeason.Text = FOO.CurrentSeason;
      }
      return validBusts;
       
    }



    protected void repBusts_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        PotentialBust boundItem = (PotentialBust)e.Item.DataItem;

        CSWRFOOBustSleeperItemTemplate itemTemplate = (CSWRFOOBustSleeperItemTemplate)e.Item.FindControl("cfbsitBustSleeperItemTemplate");
        itemTemplate.PlayerID = boundItem.PlayerID;
        itemTemplate.ComparisonSource = BustSleeperComparisonSource.CBS;
        itemTemplate.Status = SSIProperty.Bust;
        itemTemplate.CSWRRank = boundItem.CSWRRank;
        itemTemplate.CBSRank = boundItem.CBSRank;
        itemTemplate.ADP = boundItem.ADP;
        itemTemplate.RankDifference = boundItem.RankDifference.ToString();
        itemTemplate.PlayerType = DifferentialPlayerType.Bust;
        itemTemplate.LoadTemplate();
      }
    }

    public class PotentialBust
    {
      public PotentialBust(int playerID, string positionCode, int cswrRank, int cbsRank, int adp, int rankDifference)
      {
        this.PlayerID = playerID;
        this.PositionCode = positionCode;
        this.CSWRRank = cswrRank;
        this.CBSRank = cbsRank;
        this.ADP = adp;
        this.RankDifference = rankDifference;
      }

      public PotentialBust() { }

      public int PlayerID { get; set; }
      public string PositionCode { get; set; }
      public int CSWRRank { get; set; }
      public int CBSRank { get; set; }
      public int ADP { get; set; }
      public int RankDifference { get; set; }
    }


    //private void LoadSocialTags()
    //{
    //  SportMaster myMaster = (SportMaster)this.Page.Master;
    //  myMaster.OpenGraphImage = "https://www.cheatsheetwarroom.com/images/socialsharing/fantasy-football-busts.jpg";
    //  myMaster.SchemaOrgImage = "https://www.cheatsheetwarroom.com/images/socialsharing/fantasy-football-busts.jpg";
    //  myMaster.TwitterImage = "https://www.cheatsheetwarroom.com/images/socialsharing/fantasy-football-busts.jpg";
    //}

  }
}