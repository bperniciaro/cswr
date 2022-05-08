using System;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class SportMaster : System.Web.UI.MasterPage
  {

    Web20Frame _myMaster;

    // Schema.org for Google+
    public string SchemaOrgName
    {
      set { _myMaster.SchemaOrgName = value;}
    }

    public string SchemaOrgDescription 
    {
      set { _myMaster.SchemaOrgDescription = value; }
    }

    public string SchemaOrgImage
    {
      set { _myMaster.SchemaOrgImage = value; }
    }


    // Twitter
    public string TwitterTitle 
    {
      set { _myMaster.TwitterTitle = value; }
    }

    public string TwitterDescription
    {
      set { _myMaster.TwitterDescription = value; }
    }

    public string TwitterImage 
    {
      set { _myMaster.TwitterImage = value; }
    }


    // Open Graph for Facebook
    public string OpenGraphUrl
    {
      set { _myMaster.OpenGraphUrl = value; }
    }

    public string OpenGraphImage
    {
      set { _myMaster.OpenGraphImage = value; }
    }

    public string OpenGraphTitle 
    {
      set { _myMaster.OpenGraphTitle = value; }
    }

    public string OpenGraphDescription 
    {
      set { _myMaster.OpenGraphDescription = value; }
    }



    protected void Page_Init(object sender, EventArgs e)
    {
      _myMaster = (Web20Frame)this.Master;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
      

      DetermineSport();
      if (!IsPostBack)
      {
        if (!Globals.CSWRSettings.EnableAdvertisements)
        {
          panLeftColumnAffiliate.Visible = false;
        }

        hlDriverRankings.Text = SportSeason.GetCurrentSportSeason(SupportedSport.RAC.ToString()).SeasonCode + " DRIVER RANKINGS";
        hlDriverADPRankings.Text = SportSeason.GetCurrentSportSeason(SupportedSport.RAC.ToString()).SeasonCode + " DRIVER ADP RANKINGS";
      }
    }

    
    private void DetermineSport()
    {
      Web20Frame myMaster = (Web20Frame)this.Page.Master.Master;

      if (Globals.isPageInSitemapTree("/fantasy-football/"))
      {
        panLandingContainer.CssClass = "landingContainer footballLanding";
        panFootballNavigation.Visible = true;
        panRacingNavigation.Visible = false;
        //hlSheetIcon.NavigateUrl = "~/fantasy-football/nfl/cheat-sheets.aspx";
        SessionHandler.CurrentSportCode = "FOO";
      }
      if (Globals.isPageInSitemapTree("/fantasy-racing/"))
      {
        panLandingContainer.CssClass = "landingContainer racingLanding";
        panFootballNavigation.Visible = false;
        panRacingNavigation.Visible = true;
        //hlSheetIcon.NavigateUrl = "~/fantasy-racing/nascar/cheat-sheets.aspx";
        SessionHandler.CurrentSportCode = "RAC";
      }

    }


  }
}