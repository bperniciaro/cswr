<%@ WebHandler Language="C#" Class="autocomplete" %>

using System;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using BP.CheatSheetWarRoom;
using BP.CheatSheetWarRoom.BLL.Sheets;

public class autocomplete : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/javascript";

        //jQuery will pass in what you've typed in the search box in the "term" query string
        var term = context.Request.QueryString["term"];

        if (!String.IsNullOrEmpty(term))
        {
            //find any matches
            var allMatchingPlayers = Player.GetPlayersByPartialName(FOO.FOOString, term);
            var newResult = allMatchingPlayers.Select(x => x.FirstName + ' ' + x.LastName + " - " + x.Team.Abbreviation + " - " + x.PositionCode).ToArray();
            context.Response.Write(new JavaScriptSerializer().Serialize(newResult));
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}