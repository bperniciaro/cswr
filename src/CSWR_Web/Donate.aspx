<%@ Page Title="Donate to Cheat Sheet War Room" Language="C#" MasterPageFile="~/MasterPages/NoSport.master" AutoEventWireup="true" 
MetaDescription="If you like this free cheat sheet application, consider saying thanks with a donation."
CodeFile="Donate.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.Donate" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
CanonicalUrl="http://www.cheatsheetwarroom.com/donate.aspx"
  %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

<h1>Consider Donating to Cheat Sheet War Room</h1>

<p style="padding:15px 0px 10px 0px;">
  Cheat Sheet War Room has been <strong>completely free since 2007</strong>.  However, as time has gone by and new features have been added, the cost operate this site has
  increased.  I'll be attemping to remove all ads for the upcoming 2015 fantasy football season, so any donations
  would go a long way toward ensuring I can keep <abbr title="Cheat Sheet War Room">CSWR</abbr> afloat.
</p>

<p style="padding:10px 0px 10px 0px;">
  If you would like to show your appreciation for the last seven years of free development, please consider making a donation.  If you'd rather buy 
  something more concrete, I have listed a few products that I either have to purchase periodically for the site or would like to have
  for development purposes.
</p>
  
<p>  
  Any money donated or purchases made will be used exclusively for Cheat Sheet War Room.  These tools/products will improve the site and
  create a better experience for you and all CSWR users.
</p>

<br />
<h2>Brad's <abbr title="Cheat Sheet War Room">CSWR</abbr> Wish List</h2>

<div style="width:500px;margin:20px auto;">
  <table>
    <tr>
      <td style="padding-right: 10px;">ASPPlayground Forum Updates</td>
      <td><a href="http://aspplayground.net/Purchase.aspx" title="The price to maintain our forum each year.">$192 per year</a></td>
    </tr>
    <tr>
      <td>NFL Stats</td>
      <td>$60 per year</td>
    </tr>
    <tr>
      <td>Web Hosting</td>
      <td><a href="http://www.arvixe.com/asp_net_web_hosting" title="My hosting costs each year.">$96 per year</a></td>
    </tr>
    <tr>
      <td>Aweber Newsletter Fees</td>
      <td><a href="http://www.aweber.com/pricing.htm" title="My cost to keep users informed of new features.">$70 per month</a></td>
    </tr>
    <tr>
      <td>HighCharts</td>
      <td><a href="http://shop.highsoft.com/highcharts.html">$590 developer License</a></td>
    </tr>
  </table>
</div>

<p style="padding:5px 0px 10px 0px;">
  Of course, <em>any donation</em> would be appreciated and put to good use.  Thanks for dropping-by this page.
</p>
  

<div style="width:170px;padding:10px 0px;margin: auto;">
  
  <%--C# Paypal Donation Button Re-work w/o form tag: http://williablog.net/williablog/post/2012/10/26/Adding-a-Paypal-Donate-button-to-your-BlogengineNet-blog-or-ASPNet-Page.aspx--%>
  <div style="text-align:center">
    <input name="cmd" type="hidden" value="_s-xclick" />
    <input name="hosted_button_id" type="hidden" value="FJS9BWCU4NVNG" />          
    <asp:ImageButton ID="btnDonate" AlternateText="PayPal - The safer, easier way to pay online!" runat="server"
      ImageUrl="https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif"
      PostBackUrl="https://www.paypal.com/cgi-bin/webscr" OnClick="btnPayNow_Click"/>
      <img alt="" border="0" src="https://www.paypalobjects.com/en_US/i/scr/pixel.gif" width="1" height="1" />
  </div>

  <div style="padding:13px 0px 0px 0px;">
    I appreciate your support,
    <br />
    <em>-Brad Perniciaro </em>
  </div>
</div>


</asp:Content>

