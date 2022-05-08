using BP.CheatSheetWarRoom.BLL.Sheets;
using LinqToTwitter;
using Newtonsoft.Json;
using System;

public partial class Widget_HtmlGenerator : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    //string id = Request.QueryString["id"];
    // Determine if the Username route parameters is found, if so load it
    int id = 0;
      if (Page.RouteData.Values["id"] != null)
      {
        id = int.Parse(Page.RouteData.Values["id"].ToString());
        
      }

      var items = CheatSheet.GetCheatSheet(id).Items;
      

    //string json = id.ToString() + "( {'html': '<table><tr><td>Player1</td><td>100</td></tr><tr><td>Player2</td><td>200</td></tr></table>' } )";
      Response.Clear();
      Response.ContentType = "application/json";
      Response.Charset = null;
      Response.Write(JsonConvert.SerializeObject(items));
      Response.End();
    }
}