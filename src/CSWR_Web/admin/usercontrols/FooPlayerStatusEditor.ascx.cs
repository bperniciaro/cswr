using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom;
using BP.CheatSheetWarRoom.BLL.Sheets;


namespace BP.CheatSheetWarRoom.UI.UserControls
{

  public partial class FooPlayerStatusEditor : System.Web.UI.UserControl
  {

    public int PlayerId
    {
      get { return ViewState["PlayerID"] == null ? 0 : int.Parse(ViewState["PlayerID"].ToString()); }
      set { ViewState["PlayeID"] = value; }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        InitializeControls();
        litEditorTitle.Text = "Add Player Status";
      }

      // the html editor has to be initialized on every postback 
      var smManager = ScriptManager.GetCurrent(Page);
      if (smManager == null) return;
      if (!smManager.IsInAsyncPostBack)
      {
        ScriptManager.RegisterOnSubmitStatement(this, this.GetType(), "SaveTextBoxBeforePostBack",
          "SaveTextBoxBeforePostBack()");
      }

      // configure the header
      if (this.PlayerId == 0)
      {
        litEditorTitle.Text = "Add Player Status";
      }
      else
      {
        litEditorTitle.Text = "Edit Player Status";
      }

    }



    private void InitializeControls()
    {
      // we only want to let Mods enter non-dynamic statuses
      ddlStatusCodes.DataSource = PlayerStatusCode.GetPlayerStatusCodes(FOO.FOOString).Where(x => !x.Dynamic);
      ddlStatusCodes.DataBind();

      trSupplementalInfo.Visible = false;
      trCountInfo.Visible = false;
    }

    public int InsertPlayerStatus()
    {
      return 0;
    }


    protected void ddlStatusCodes_SelectedIndexChanged(object sender, EventArgs e)
    {
      var targetStatus = PlayerStatusCode.GetPlayerStatusCode(ddlStatusCodes.SelectedValue);

      // don't do anything if 'Select Status' was selected
      if (targetStatus == null)
      {
        trSupplementalInfo.Visible = false;
        trCountInfo.Visible = false;
        return;
      }

      // configure the form based on whether supplemental information is required
      if (targetStatus.SuppInfoRequired)
      {
        trSupplementalInfo.Visible = true;

        tbSupplementalInfo.Text = targetStatus.SuppInfoExample;
        labSuppInfoLabel.Text = targetStatus.SuppInfoLabel;
        labSuppInfoHelp.ToolTip = targetStatus.SuppInfoInstructions;
      }
      else
      {
        trSupplementalInfo.Visible = false;
      }

      // configure the form based on whether count information is required
      if (targetStatus.CountRequired)
      {
        trCountInfo.Visible = true;

        tbCount.Text = targetStatus.CountExample.ToString();
        labCountLabel.Text = targetStatus.CountLabel;
        labCountInfoHelp.ToolTip = targetStatus.CountInstructions;
      }
      else
      {
        trCountInfo.Visible = false;
      }


    }

    protected void ddlStatusCodes_DataBound(object sender, EventArgs e)
    {
      foreach (ListItem item in ddlStatusCodes.Items)
      {
        // skip the "Select Status" item
        if (item.Value != "0")
        {
          item.Attributes.Add("Title", PlayerStatusCode.GetPlayerStatusCode(item.Value).Description);
        }
      }
    }

    public int SaveStatus()
    {
      var targetPlayerId = DeterminePlayerId();
      var encodedSuppInfo = WebUtility.HtmlEncode(tbSupplementalInfo.Text);
      int? countValue = null;
      if (tbCount.Text != String.Empty)
      {
        countValue = int.Parse(tbCount.Text);
      }
      string currentUserName = HttpContext.Current.User.Identity.Name;

      int newPlayerId = SportSeasonPlayerStatus.InsertSportSeasonPlayerStatus(FOO.FOOString, FOO.CurrentSeason, targetPlayerId,
        ddlStatusCodes.SelectedValue, encodedSuppInfo, countValue, true, false, currentUserName, DateTime.Now,
        null, null);

      if (newPlayerId != 0)
      {
        ClearFormFields();
      }

      return newPlayerId;
    }

    private void ClearFormFields()
    {
      tbPlayer.Text = String.Empty;
      ddlStatusCodes.SelectedIndex = 0;
      trSupplementalInfo.Visible = false;
      trCountInfo.Visible = false;
    }

    private int DeterminePlayerId()
    {
      // determine the player we're targetting
      var playerParts = tbPlayer.Text.Split('-'); // name - team abbr - city
      var fullPlayerName = playerParts[0].Trim();
      var teamAbbreviation = playerParts[1].Trim();
      var playerTeam = Team.GetTeams(FOO.FOOString).Single(x => x.Abbreviation == teamAbbreviation);

      var playerNameParts = fullPlayerName.Split(' ');
      var targetPlayer = Player.GetPlayer(FOO.FOOString, playerTeam.TeamCode, playerNameParts[0], String.Empty,
        playerNameParts[1].Trim());
      return targetPlayer.PlayerID;
    }




  }
}