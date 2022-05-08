using System;

public partial class test_TestRedirect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      Response.Write("Request.Url.Host.ToLower(): " + Request.Url.Host.ToLower() + "<br/>");
      Response.Write("Request.Url.PathAndQuery: " + Request.Url.PathAndQuery + "<br/>");
      if (!Request.Url.Host.ToLower().ToString().StartsWith("www."))
      {
        Response.Clear();
        Response.Status = "301 Moved Permanently";
        string newAddress = "http://www." + Request.Url.PathAndQuery;
        Response.Write("newAddress: " + newAddress + "<br/>");
        Response.AddHeader("Location", newAddress);
        Response.End();
      }
      Response.Write("Redirected");

    }

}