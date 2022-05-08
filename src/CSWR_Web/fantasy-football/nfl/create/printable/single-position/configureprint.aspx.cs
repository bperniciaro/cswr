using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class ConfigurePrintSingle : BasePage
  {

    #region LocalVariablesAndProperties
      private StringBuilder _sbNavigateQuery = new StringBuilder();

      /// <summary>
      /// Indicates whether the target quarterback sheet is a user cheat sheet or supplemental sheet
      /// </summary>
      public SheetType QBSheetType
      {
        get { return (SheetType)ViewState["qbSheetType"]; }
        set { ViewState["qbSheetType"] = value; }
      }

      /// <summary>
      /// The ID of the target quarterback sheet
      /// </summary>
      public int QBSheetID
      {
        get
        {
          return (ViewState["qbSheetID"] == null) ? 0 : (int)ViewState["qbSheetID"];
        }
        set { ViewState["qbSheetID"] = value; }
      }

      /// <summary>
      /// Indicates whether the target running back sheet is a user cheat sheet or supplemental sheet
      /// </summary>
      public SheetType RBSheetType
      {
        get { return (SheetType)ViewState["rbSheetType"]; }
        set { ViewState["rbSheetType"] = value; }
      }

      /// <summary>
      /// The ID of the target running back sheet
      /// </summary>
      public int RBSheetID
      {
        get
        {
          return (ViewState["rbSheetID"] == null) ? 0 : (int)ViewState["rbSheetID"];
        }
        set { ViewState["rbSheetID"] = value; }
      }

      /// <summary>
      /// Indicates whether the target wide receiver sheet is a user cheat sheet or supplemental sheet
      /// </summary>
      public SheetType WRSheetType
      {
        get { return (SheetType)ViewState["wrSheetType"]; }
        set { ViewState["wrSheetType"] = value; }
      }

      /// <summary>
      /// The ID of the target wide receiver sheet
      /// </summary>
      public int WRSheetID
      {
        get
        {
          return (ViewState["wrSheetID"] == null) ? 0 : (int)ViewState["wrSheetID"];
        }
        set { ViewState["wrSheetID"] = value; }
      }

      /// <summary>
      /// Indicates whether the target tight end sheet is a user cheat sheet or supplemental sheet
      /// </summary>
      public SheetType TESheetType
      {
        get { return (SheetType)ViewState["teSheetType"]; }
        set { ViewState["teSheetType"] = value; }
      }

      /// <summary>
      /// The ID of the target tight end sheet
      /// </summary>
      public int TESheetID
      {
        get
        {
          return (ViewState["teSheetID"] == null) ? 0 : (int)ViewState["teSheetID"];
        }
        set { ViewState["teSheetID"] = value; }
      }

      /// <summary>
      /// Indicates whether the target kicker sheet is a user cheat sheet or supplemental sheet
      /// </summary>
      public SheetType KSheetType
      {
        get { return (SheetType)ViewState["kSheetType"]; }
        set { ViewState["kSheetType"] = value; }
      }

      /// <summary>
      /// The ID of the target kicker sheet
      /// </summary>
      public int KSheetID
      {
        get
        {
          return (ViewState["kSheetID"] == null) ? 0 : (int)ViewState["kSheetID"];
        }
        set { ViewState["kSheetID"] = value; }
      }

      /// <summary>
      /// Indicates whether the target defense sheet is a user cheat sheet or supplemental sheet
      /// </summary>
      public SheetType DFSheetType
      {
        get { return (SheetType)ViewState["dfSheetType"]; }
        set { ViewState["dfSheetType"] = value; }
      }

      /// <summary>
      /// The ID of the target defense sheet
      /// </summary>
      public int DFSheetID
      {
        get
        {
          return (ViewState["dfSheetID"] == null) ? 0 : (int)ViewState["dfSheetID"];
        }
        set { ViewState["dfSheetID"] = value; }
      }
    #endregion


    protected void Page_Init(object sender, EventArgs e)
    {
      SessionHandler.CurrentSportCode = FOO.FOOString;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        LoadSheetOptions();
        scmlnNavigation.CheatSheetID = Profile.Football.LastFootballCheatSheetID;
        BuildQueryString();
      }
      Thread.Sleep(400);
    }

    private void LoadSheetOptions()
    {
      LoadQBSheets();
      LoadRBSheets();
      LoadWRSheets();
      LoadTESheets();
      LoadKSheets();
      LoadDEFSheets();
    }


    private void LoadQBSheets()  {

      QBSheetType = SheetType.CheatSheet;
      labQBSheetType.Text = "Sheet";
      List<CheatSheet> qbSheets = CheatSheet.GetUserCheatSheets(this.User.Identity.Name, SessionHandler.CurrentSportCode, "QB").Where(x => x.Positions.Count == 1).ToList();

      if (qbSheets.Count == 0)
      {
        QBSheetType = SheetType.SuppSheet;
        labQBSheetType.Text = "Source";
        List<SupplementalSource> availableSources = SupplementalSource.GetSupplementalSources(SportSeason.GetCurrentSportSeason(SessionHandler.CurrentSportCode).SeasonCode, SessionHandler.CurrentSportCode, "QB");
        if (availableSources.Count > 0)
        {
          ddlAvailableQBSources.DataSource = availableSources;
          ddlAvailableQBSources.DataTextField = "Name";
          ddlAvailableQBSources.DataValueField = "SupplementalSourceID";
          ddlAvailableQBSources.Visible = true;
          ddlAvailableQBSources.DataBind();
          labQBSheetSourceMessage.Visible = true;
          labQBSheetSourceMessage.Text = "no sheets, choose source";
        }
        else
        {
          labSingleQBSheet.Text = "No data";
          labSingleQBSheet.Visible = true;
        }
      }
      else if (qbSheets.Count == 1)
      {
        labSingleQBSheet.Text = "\"" + qbSheets[0].SheetName + "\"";
        labSingleQBSheet.Visible = true;
        QBSheetID = qbSheets[0].CheatSheetID;
      }
      else
      {
        ddlQBSheets.DataSource = qbSheets;
        ddlQBSheets.DataTextField = "SheetName";
        ddlQBSheets.DataValueField = "CheatSheetID";
        ddlQBSheets.Visible = true;
        ddlQBSheets.DataBind();
        labQBSheetSourceMessage.Visible = true;
        labQBSheetSourceMessage.Text = "multiple sheets found, choose one";
      }


    }


    private void LoadRBSheets()
    {
      List<CheatSheet> rbSheets = CheatSheet.GetUserCheatSheets(this.User.Identity.Name, SessionHandler.CurrentSportCode, "RB").Where(x => x.Positions.Count == 1).ToList();
      RBSheetType = SheetType.CheatSheet;
      labRBSheetType.Text = "Sheet";
      if (rbSheets.Count == 0)
      {
        RBSheetType = SheetType.SuppSheet;
        labRBSheetType.Text = "Source";
        List<SupplementalSource> availableSources = SupplementalSource.GetSupplementalSources(SportSeason.GetCurrentSportSeason(SessionHandler.CurrentSportCode).SeasonCode, SessionHandler.CurrentSportCode, "RB");
        if (availableSources.Count > 0)
        {
          ddlAvailableRBSources.DataSource = availableSources;
          ddlAvailableRBSources.DataTextField = "Name";
          ddlAvailableRBSources.DataValueField = "SupplementalSourceID";
          ddlAvailableRBSources.Visible = true;
          ddlAvailableRBSources.DataBind();
          labRBSheetSourceMessage.Visible = true;
          labRBSheetSourceMessage.Text = "no sheets, choose source";
        }
        else
        {
          labSingleRBSheet.Text = "No data";
          labSingleRBSheet.Visible = true;
        }
      }
      else if (rbSheets.Count == 1)
      {
        labSingleRBSheet.Text = "\"" + rbSheets[0].SheetName + "\"";
        labSingleRBSheet.Visible = true;
        RBSheetID = rbSheets[0].CheatSheetID;
      }
      else
      {
        ddlRBSheets.DataSource = rbSheets;
        ddlRBSheets.DataTextField = "SheetName";
        ddlRBSheets.DataValueField = "CheatSheetID";
        ddlRBSheets.Visible = true;
        ddlRBSheets.DataBind();
        labRBSheetSourceMessage.Visible = true;
        labRBSheetSourceMessage.Text = "multiple sheets found, choose one";
      }
    }



    private void LoadWRSheets()
    {
      List<CheatSheet> wrSheets = CheatSheet.GetUserCheatSheets(this.User.Identity.Name, SessionHandler.CurrentSportCode, "WR").Where(x => x.Positions.Count == 1).ToList();
      WRSheetType = SheetType.CheatSheet;
      labWRSheetType.Text = "Sheet";
      if (wrSheets.Count == 0)
      {
        WRSheetType = SheetType.SuppSheet;
        labWRSheetType.Text = "Source";
        List<SupplementalSource> availableSources = SupplementalSource.GetSupplementalSources(SportSeason.GetCurrentSportSeason(SessionHandler.CurrentSportCode).SeasonCode, SessionHandler.CurrentSportCode, "WR");
        if (availableSources.Count > 0)
        {
          ddlAvailableWRSources.DataSource = availableSources;
          ddlAvailableWRSources.DataTextField = "Name";
          ddlAvailableWRSources.DataValueField = "SupplementalSourceID";
          ddlAvailableWRSources.Visible = true;
          ddlAvailableWRSources.DataBind();
          labWRSheetSourceMessage.Visible = true;
          labWRSheetSourceMessage.Text = "no sheets, choose source";
        }
        else
        {
          labSingleWRSheet.Text = "No data";
          labSingleWRSheet.Visible = true;
        }
      }
      else if (wrSheets.Count == 1)
      {
        labSingleWRSheet.Text = "\"" + wrSheets[0].SheetName + "\"";
        labSingleWRSheet.Visible = true;
        WRSheetID = wrSheets[0].CheatSheetID;
      }
      else
      {
        ddlWRSheets.DataSource = wrSheets;
        ddlWRSheets.DataTextField = "SheetName";
        ddlWRSheets.DataValueField = "CheatSheetID";
        ddlWRSheets.Visible = true;
        ddlWRSheets.DataBind();
        labWRSheetSourceMessage.Visible = true;
        labWRSheetSourceMessage.Text = "multiple sheets found, choose one";
      }
    }

    private void LoadTESheets()
    {
      List<CheatSheet> teSheets = CheatSheet.GetUserCheatSheets(this.User.Identity.Name, SessionHandler.CurrentSportCode, "TE").Where(x => x.Positions.Count == 1).ToList();
      TESheetType = SheetType.CheatSheet;
      labTESheetType.Text = "Sheet";
      if (teSheets.Count == 0)
      {
        TESheetType = SheetType.SuppSheet;
        labTESheetType.Text = "Source";
        List<SupplementalSource> availableSources = SupplementalSource.GetSupplementalSources(SportSeason.GetCurrentSportSeason(SessionHandler.CurrentSportCode).SeasonCode, SessionHandler.CurrentSportCode, "TE");
        if (availableSources.Count > 0)
        {
          ddlAvailableTESources.DataSource = availableSources;
          ddlAvailableTESources.DataTextField = "Name";
          ddlAvailableTESources.DataValueField = "SupplementalSourceID";
          ddlAvailableTESources.Visible = true;
          ddlAvailableTESources.DataBind();
          labTESheetSourceMessage.Visible = true;
          labTESheetSourceMessage.Text = "no sheets, choose source";
        }
        else
        {
          labSingleTESheet.Text = "No data";
          labSingleTESheet.Visible = true;
        }
      }
      else if (teSheets.Count == 1)
      {
        labSingleTESheet.Text = "\"" + teSheets[0].SheetName + "\"";
        labSingleTESheet.Visible = true;
        TESheetID = teSheets[0].CheatSheetID;
      }
      else
      {
        ddlTESheets.DataSource = teSheets;
        ddlTESheets.DataTextField = "SheetName";
        ddlTESheets.DataValueField = "CheatSheetID";
        ddlTESheets.Visible = true;
        ddlTESheets.DataBind();
        labTESheetSourceMessage.Visible = true;
        labTESheetSourceMessage.Text = "multiple sheets found, choose one";
      }
    }



    private void LoadKSheets()
    {
      List<CheatSheet> kSheets = CheatSheet.GetUserCheatSheets(this.User.Identity.Name, SessionHandler.CurrentSportCode, "K").Where(x => x.Positions.Count == 1).ToList();
      KSheetType = SheetType.CheatSheet;
      labKSheetType.Text = "Sheet";
      if (kSheets.Count == 0)
      {
        KSheetType = SheetType.SuppSheet;
        labKSheetType.Text = "Source";
        List<SupplementalSource> availableSources = SupplementalSource.GetSupplementalSources(SportSeason.GetCurrentSportSeason(SessionHandler.CurrentSportCode).SeasonCode, SessionHandler.CurrentSportCode, "K");
        if (availableSources.Count > 0)
        {
          ddlAvailableKSources.DataSource = availableSources;
          ddlAvailableKSources.DataTextField = "Name";
          ddlAvailableKSources.DataValueField = "SupplementalSourceID";
          ddlAvailableKSources.Visible = true;
          ddlAvailableKSources.DataBind();
          labKSheetSourceMessage.Visible = true;
          labKSheetSourceMessage.Text = "no sheets, choose source";
        }
        else
        {
          labSingleKSheet.Text = "No data";
          labSingleKSheet.Visible = true;
        }
      }
      else if (kSheets.Count == 1)
      {
        labSingleKSheet.Text = "\"" + kSheets[0].SheetName + "\"";
        labSingleKSheet.Visible = true;
        KSheetID = kSheets[0].CheatSheetID;
      }
      else
      {
        ddlKSheets.DataSource = kSheets;
        ddlKSheets.DataTextField = "SheetName";
        ddlKSheets.DataValueField = "CheatSheetID";
        ddlKSheets.Visible = true;
        ddlKSheets.DataBind();
        labKSheetSourceMessage.Visible = true;
        labKSheetSourceMessage.Text = "multiple sheets found, choose one";
      }
    }

    private void LoadDEFSheets()
    {
      List<CheatSheet> defSheets = CheatSheet.GetUserCheatSheets(this.User.Identity.Name, SessionHandler.CurrentSportCode, "DF").Where(x => x.Positions.Count == 1).ToList();
      DFSheetType = SheetType.CheatSheet;
      labDFSheetType.Text = "Sheet";
      if (defSheets.Count == 0)
      {
        DFSheetType = SheetType.SuppSheet;
        labDFSheetType.Text = "Source";
        List<SupplementalSource> availableSources = SupplementalSource.GetSupplementalSources(SportSeason.GetCurrentSportSeason(SessionHandler.CurrentSportCode).SeasonCode, SessionHandler.CurrentSportCode, "DF");
        if (availableSources.Count > 0)
        {
          ddlAvailableDEFSources.DataSource = availableSources;
          ddlAvailableDEFSources.DataTextField = "Name";
          ddlAvailableDEFSources.DataValueField = "SupplementalSourceID";
          ddlAvailableDEFSources.Visible = true;
          ddlAvailableDEFSources.DataBind();
          labDFSheetSourceMessage.Visible = true;
          labDFSheetSourceMessage.Text = "no sheets, choose source";
        }
        else
        {
          labSingleDEFSheet.Text = "No data";
          labSingleDEFSheet.Visible = true;
        }
      }
      else if (defSheets.Count == 1)
      {
        labSingleDEFSheet.Text = "\"" + defSheets[0].SheetName + "\"";
        labSingleDEFSheet.Visible = true;
        DFSheetID = defSheets[0].CheatSheetID;
      }
      else
      {
        ddlDEFSheets.DataSource = defSheets;
        ddlDEFSheets.DataTextField = "SheetName";
        ddlDEFSheets.DataValueField = "CheatSheetID";
        ddlDEFSheets.Visible = true;
        ddlDEFSheets.DataBind();
        labDFSheetSourceMessage.Visible = true;
        labDFSheetSourceMessage.Text = "multiple sheets found, choose one";
      }
    }



    /// <summary>
    // If the format options change, re-build the query string
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rbOption1_CheckedChanged(object sender, EventArgs e)
    {
      BuildQueryString();
    }

    /// <summary>
    // If the format options change, re-build the query string
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rbOption2_CheckedChanged(object sender, EventArgs e)
    {
      BuildQueryString();
    }


    /// <summary>
    /// Select CSWR supplemental sheet by default, and rebuild the query string
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlAvailableQBSources_DataBound(object sender, EventArgs e)
    {
      ddlAvailableQBSources.SelectedValue = SupplementalSource.GetSupplementalSource("CSWR").SupplementalSourceID.ToString();
      if (IsPostBack)
      {
        BuildQueryString();
      }
    }

    /// <summary>
    /// Select CSWR supplemental sheet by default, and rebuild the query string
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlAvailableRBSources_DataBound(object sender, EventArgs e)
    {
      ddlAvailableRBSources.SelectedValue = SupplementalSource.GetSupplementalSource("CSWR").SupplementalSourceID.ToString();
      if (IsPostBack)
      {
        BuildQueryString();
      }
    }

    /// <summary>
    /// Select CSWR supplemental sheet by default, and rebuild the query string
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlAvailableWRSources_DataBound(object sender, EventArgs e)
    {
      ddlAvailableWRSources.SelectedValue = SupplementalSource.GetSupplementalSource("CSWR").SupplementalSourceID.ToString();
      if (IsPostBack)
      {
        BuildQueryString();
      }
    }

    /// <summary>
    /// Select CSWR supplemental sheet by default, and rebuild the query string
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlAvailableTESources_DataBound(object sender, EventArgs e)
    {
      ddlAvailableTESources.SelectedValue = SupplementalSource.GetSupplementalSource("CSWR").SupplementalSourceID.ToString();
      if (IsPostBack)
      {
        BuildQueryString();
      }
    }

    /// <summary>
    /// Select CSWR supplemental sheet by default, and rebuild the query string
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlAvailableKSources_DataBound(object sender, EventArgs e)
    {
      ddlAvailableKSources.SelectedValue = SupplementalSource.GetSupplementalSource("CSWR").SupplementalSourceID.ToString();
      if (IsPostBack)
      {
        BuildQueryString();
      }
    }

    /// <summary>
    /// Select CSWR supplemental sheet by default, and rebuild the query string
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlAvailableDEFSources_DataBound(object sender, EventArgs e)
    {
      ddlAvailableDEFSources.SelectedValue = SupplementalSource.GetSupplementalSource("CSWR").SupplementalSourceID.ToString();
      if (IsPostBack)
      {
        BuildQueryString();
      }
    }


    //protected void butGenerateSheet_Click(object sender, EventArgs e)
    //{
    //  BuildQueryString();
    //}


    /// <summary>
    /// If the QB sheet is changed, rebuild the query string based on the new sheet ID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlQBSheets_SelectedIndexChanged(object sender, EventArgs e)
    {
      int qbID = 0;
      int.TryParse(ddlQBSheets.SelectedValue, out qbID);
      this.QBSheetID = qbID;
      BuildQueryString();
    }

    /// <summary>
    /// If the RB sheet is changed, rebuild the query string based on the new sheet ID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlRBSheets_SelectedIndexChanged(object sender, EventArgs e)
    {
      int rbID = 0;
      int.TryParse(ddlRBSheets.SelectedValue, out rbID);
      this.RBSheetID = rbID;
      BuildQueryString();
    }

    /// <summary>
    /// If the WR sheet is changed, rebuild the query string based on the new sheet ID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlWRSheets_SelectedIndexChanged(object sender, EventArgs e)
    {
      int wrID = 0;
      int.TryParse(ddlWRSheets.SelectedValue, out wrID);
      this.WRSheetID = wrID;
      BuildQueryString();
    }

    /// <summary>
    /// If the TE sheet is changed, rebuild the query string based on the new sheet ID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTESheets_SelectedIndexChanged(object sender, EventArgs e)
    {
      int teID = 0;
      int.TryParse(ddlTESheets.SelectedValue, out teID);
      this.TESheetID = teID;
      BuildQueryString();
    }

    /// <summary>
    /// If the K sheet is changed, rebuild the query string based on the new sheet ID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlKSheets_SelectedIndexChanged(object sender, EventArgs e)
    {
      int kID = 0;
      int.TryParse(ddlTESheets.SelectedValue, out kID);
      this.KSheetID = kID;
      BuildQueryString();
    }

    /// <summary>
    /// If the DF sheet is changed, rebuild the query string based on the new sheet ID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDEFSheets_SelectedIndexChanged(object sender, EventArgs e)
    {
      int dfID = 0;
      int.TryParse(ddlDEFSheets.SelectedValue, out dfID);
      this.DFSheetID = dfID;
      BuildQueryString();
    }


    protected void rbIntegratedRoster_CheckedChanged(object sender, EventArgs e)
    {
      BuildQueryString();
    }

    protected void rbNoRoster_CheckedChanged(object sender, EventArgs e)
    {
      BuildQueryString();
    }


    protected void BuildQueryString()
    {
      //QB
      if (ddlAvailableQBSources.Items.Count > 0)
      {
        QBSheetID = int.Parse(ddlAvailableQBSources.SelectedValue);
      }
      if (ddlQBSheets.Items.Count > 0)
      {
        QBSheetID = int.Parse(ddlQBSheets.SelectedValue);
      }
      //RB
      if (ddlAvailableRBSources.Items.Count > 0)
      {
        RBSheetID = int.Parse(ddlAvailableRBSources.SelectedValue);
      }
      if (ddlRBSheets.Items.Count > 0)
      {
        RBSheetID = int.Parse(ddlRBSheets.SelectedValue);
      }
      //WR
      if (ddlAvailableWRSources.Items.Count > 0)
      {
        WRSheetID = int.Parse(ddlAvailableWRSources.SelectedValue);
      }
      if (ddlWRSheets.Items.Count > 0)
      {
        WRSheetID = int.Parse(ddlWRSheets.SelectedValue);
      }
      //TE
      if (ddlAvailableTESources.Items.Count > 0)
      {
        TESheetID = int.Parse(ddlAvailableTESources.SelectedValue);
      }
      if (ddlTESheets.Items.Count > 0)
      {
        TESheetID = int.Parse(ddlTESheets.SelectedValue);
      }
      //K
      if (ddlAvailableKSources.Items.Count > 0)
      {
        KSheetID = int.Parse(ddlAvailableKSources.SelectedValue);
      }
      if (ddlKSheets.Items.Count > 0)
      {
        KSheetID = int.Parse(ddlKSheets.SelectedValue);
      }
      //DF
      if (ddlAvailableDEFSources.Items.Count > 0)
      {
        DFSheetID = int.Parse(ddlAvailableDEFSources.SelectedValue);
      }
      if (ddlDEFSheets.Items.Count > 0)
      {
        DFSheetID = int.Parse(ddlDEFSheets.SelectedValue);
      }

      StringBuilder queryStringParams = new StringBuilder();
      // Quarterbacks
      if (this.QBSheetType == SheetType.CheatSheet)
      {
        queryStringParams.Append("QB=CS&" + "QBID=" + this.QBSheetID + "&");
      }
      else
      {
        queryStringParams.Append("QB=SS&" + "QBID=" + ddlAvailableQBSources.SelectedValue + "&");
      }
      // Running Backs
      if (this.RBSheetType == SheetType.CheatSheet)
      {
        queryStringParams.Append("RB=CS&" + "RBID=" + this.RBSheetID + "&");
      }
      else
      {
        queryStringParams.Append("RB=SS&" + "RBID=" + ddlAvailableRBSources.SelectedValue + "&");
      }
      // Wide Receivers
      if (this.WRSheetType == SheetType.CheatSheet)
      {
        queryStringParams.Append("WR=CS&" + "WRID=" + this.WRSheetID + "&");
      }
      else
      {
        queryStringParams.Append("WR=SS&" + "WRID=" + ddlAvailableWRSources.SelectedValue + "&");
      }
      // Tight Ends
      if (this.TESheetType == SheetType.CheatSheet)
      {
        queryStringParams.Append("TE=CS&" + "TEID=" + this.TESheetID + "&");
      }
      else
      {
        queryStringParams.Append("TE=SS&" + "TEID=" + ddlAvailableTESources.SelectedValue + "&");
      }
      // Kickers
      if (this.KSheetType == SheetType.CheatSheet)
      {
        queryStringParams.Append("K=CS&" + "KID=" + this.KSheetID + "&");
      }
      else
      {
        queryStringParams.Append("K=SS&" + "KID=" + ddlAvailableKSources.SelectedValue + "&");
      }
      // Defenses
      if (this.DFSheetType == SheetType.CheatSheet)
      {
        queryStringParams.Append("DF=CS&" + "DFID=" + this.DFSheetID);
      }
      else
      {
        queryStringParams.Append("DF=SS&" + "DFID=" + ddlAvailableDEFSources.SelectedValue);
      }

      // determine sheet type
      if (rbIntegratedRoster.Checked)
      {
        hlGenerateSheet.NavigateUrl = "~/fantasy-football/nfl/create/printable/single-position/cheatsheetwithroster.aspx?" + queryStringParams.ToString();
      }
      else
      {
        hlGenerateSheet.NavigateUrl = "~/fantasy-football/nfl/create/printable/single-position/cheatsheetwithoutroster.aspx?" + queryStringParams.ToString();
      }
    }


    protected void ddlAvailableQBSources_SelectedIndexChanged(object sender, EventArgs e)
    {
      int qbID = 0;
      int.TryParse(ddlAvailableQBSources.SelectedValue, out qbID);
      this.QBSheetID = qbID;
      BuildQueryString();
    }

    protected void ddlAvailableRBSources_SelectedIndexChanged(object sender, EventArgs e)
    {
      int rbID = 0;
      int.TryParse(ddlAvailableRBSources.SelectedValue, out rbID);
      this.RBSheetID = rbID;
      BuildQueryString();
    }

    protected void ddlAvailableWRSources_SelectedIndexChanged(object sender, EventArgs e)
    {
      int wrID = 0;
      int.TryParse(ddlAvailableWRSources.SelectedValue, out wrID);
      this.WRSheetID = wrID;
      BuildQueryString();
    }

    protected void ddlAvailableTESources_SelectedIndexChanged(object sender, EventArgs e)
    {
      int teID = 0;
      int.TryParse(ddlAvailableTESources.SelectedValue, out teID);
      this.TESheetID = teID;
      BuildQueryString();
    }

    protected void ddlAvailableKSources_SelectedIndexChanged(object sender, EventArgs e)
    {
      int kID = 0;
      int.TryParse(ddlAvailableKSources.SelectedValue, out kID);
      this.KSheetID = kID;
      BuildQueryString();
    }

    protected void ddlAvailableDFSources_SelectedIndexChanged(object sender, EventArgs e)
    {
      int dfID = 0;
      int.TryParse(ddlAvailableDEFSources.SelectedValue, out dfID);
      this.DFSheetID = dfID;
      BuildQueryString();
    }
  }
}