using System;
using BP.CheatSheetWarRoom;

public partial class usercontrols_AddThis : System.Web.UI.UserControl
{

  public bool UseSmallIcons
  {
    get
    {
      return (ViewState["UseSmallIcons"] == null) ? false : (bool)ViewState["UseSmallIcons"];
    }
    set
    {
      ViewState["UseSmallIcons"] = value;
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    if (Globals.CSWRSettings.EnableSocialMedia)
    {
      LoadAddThisCode();
    }
    else
    {
      panAsyncScript.Visible = false;
    }
  }

  private void LoadAddThisCode()
  {
    if (this.UseSmallIcons)
    {
      litAddThis.Text = @"
        <div class=""addthis_toolbox addthis_default_style"">
          <a class=""addthis_button_facebook_like"" fb:like:layout=""button_count""></a>
          <a class=""addthis_button_tweet"" style=""width:85px;""></a>
          <a class=""addthis_button_pinterest_pinit"" pi:pinit:layout=""horizontal"" style=""width:65px;""></a>
          <a class=""addthis_counter addthis_pill_style""></a>
        </div>
        <script type=""text/javascript"">var addthis_config = { ""data_track_addressbar"": true };</script>
        <script type=""text/javascript"" src=""//s7.addthis.com/js/300/addthis_widget.js#pubid=bperniciaro&async=1""></script>";
    }
    else
    {
      litAddThis.Text = @"
      <div class=""addthis_toolbox addthis_default_style addthis_32x32_style"">
        <a class=""addthis_button_preferred_1""></a>
        <a class=""addthis_button_preferred_2""></a>
        <a class=""addthis_button_preferred_3""></a>
        <a class=""addthis_button_preferred_4""></a>
        <a class=""addthis_button_preferred_5""></a>
        <a class=""addthis_button_compact""></a>
        <a class=""addthis_counter addthis_bubble_style""></a>
      </div>
      <script type=""text/javascript"">  var addthis_config = { ""data_track_clickback"": true };</script>
      <script type=""text/javascript"" src=""http://s7.addthis.com/js/250/addthis_widget.js#pubid=bperniciaro&async=1""></script>";
    }
  }

}
