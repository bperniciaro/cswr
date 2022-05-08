<%@ Page Title="Registration Confirmation" Language="C#" MasterPageFile="~/MasterPages/NoSport.master" AutoEventWireup="true" 
  CodeFile="registrationconfirmation.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.RegistrationConfirmation"
  MetaRobotsText="NOINDEX,FOLLOW" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
  MetaDescription="Your registration was successful."
%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

<div class="registrationConfirmationPage">

  <h1 style="padding-bottom:10px;">
    Your Registration was Successful!!
  </h1>

  <div style="text-align:center;">
    <asp:Image runat="server" ImageUrl="~/Images/Layout/registration-successful-fireworks.jpg" />
  </div>
  
  <br />
  <p style="padding:5px 0px;">
    Cheat Sheet War Room is <strong>completely free</strong>.  But you can show your appreciation by:
  </p>

  <h2>1. Supporting our partners</h2>

  <p>
    I also have a huge list of 
    <a href="https://www.cheatsheetwarroom.com/blog/coupons" target="_blank">fantasy football coupons</a>
    for everything from draft boards to daily fantasy football sites.  It's a quick
    way to find some incredible savings for the best fantasy services.
  </p>
  <p>
    If you <a target="_blank" href="https://www.amazon.com/b?_encoding=UTF8&tag=cswr-registrationsuccessful-20&linkCode=ur2&linkId=95447c5b3dfb1c0c9f783a64475c5a44&camp=1789&creative=9325&node=10971181011">shop at Amazon using this link</a><img src="//ir-na.amazon-adsystem.com/e/ir?t=cswr-registrationsuccessful-20&l=ur2&o=1" width="1" height="1" border="0" alt="" style="border:none !important; margin:0px !important;" />,
    I'll get a cut of anything you buy in the next 24 hours.  I use this money to:
  </p>
  <ul>
    <li>Purchase the NFL stat packages that you see loaded in your sheets</li>
    <li>Pay for quality hosting to keep the site running fast</li>
    <li>Buy software add-ons to make the cheat sheet interface even better</li>
  </ul>

  <h2>2. Read my Product Guides</h2>

  <p>If you're looking for fantasy football tools or merchandise, check out my 
    <a href="https://www.cheatsheetwarroom.com/blog/reviews" target="_blank">fantasy football reviews</a>.  
    I've used just about every fantasy-related product on the market, and written in-depth guides to <strong>help you find the best products</strong>.
    </p>

  <asp:Panel runat="server" ID="panEmailWhiteListInstructions" Visible="false">
    <h3>3.  White list our emails</h3>
    <p>
      Thanks for subscribing to our email list.  I have tons of great deals and content ready to sent to you.
    </p>
    <p>
      But you can't get my emails if you don't add us to your white list.  
      <asp:HyperLink runat="server" ID="hlWhiteListInstructions" NavigateUrl="~/access/white-list-instructions.html">Follow these instructions</asp:HyperLink> to learn how to white list
      our emails on your specific email client.
    </p>

  </asp:Panel>

  <br />
  
  <br />
  <h2>You're ready to get started!</h2>

  <%--Show this panel if converted sheets exist--%>
  <asp:Panel runat="server" ID="panUnsavedSheetsExist" Visible="false">
            
    <p>
      <asp:Literal runat="server" ID="litWelcomeMessage" /> 
    </p>
              
    <%--Converted Football Sheets--%>
    <asp:Panel runat="server" ID="panConvertedFOOSheets" Visible="false">
      <h4>Football Sheets</h4>
      <ul>
        <asp:Repeater runat="server" ID="repSavedFootballSheets" OnItemDataBound="repSavedFootballSheets_ItemDataBound">
          <ItemTemplate>
            <li>
              <asp:HyperLink runat="server" ID="hlSavedSheet" />
            </li>
          </ItemTemplate>
        </asp:Repeater>
      </ul>
    </asp:Panel>

    <%--Converted Racing Sheets--%>
    <asp:Panel runat="server" ID="panConvertedRACSheets" Visible="false">
      <h4>Racing Sheets</h4>
      <ul>
        <asp:Repeater runat="server" ID="repSavedRacingSheets" OnItemDataBound="repSavedRacingSheets_ItemDataBound">
          <ItemTemplate>
            <li>
              <asp:HyperLink runat="server" ID="hlSavedSheet" />
            </li>
          </ItemTemplate>
        </asp:Repeater>
      </ul>
    </asp:Panel>
              
  </asp:Panel>

  <%--Show this panel if no unsaved sheets exist--%>
  <asp:Panel runat="server" ID="panNoUnsavedSheetsExist" Visible="false">
    <p>
      Your account has been created and you now have full access to all features.  Get started by
      <asp:HyperLink runat="server" NavigateUrl="~/fantasy-football/nfl/create/newsheet.aspx" Text="creating a fantasy football cheat sheet" />.
    </p>
  </asp:Panel>

  <br /><br /><br />

  <table>
    <tr>
      <td>
        <asp:Image runat="server" Width="100" ImageUrl="~/Images/Layout/thank-you-profile.jpg" />
      </td>
      <td style="width:500px;padding-left:10px;vertical-align:top">
        Thanks for checking out the site.  If you have any problems or ideas for improvement, please 
        <a href="mailto:brad@cheatsheetwarroom.com">contact me directly</a>.
        <span style="font-style:italic;display:block;padding-top:30px;">Brad Perniciaro</span>  
      </td>
    </tr>
  </table>
  <p>
  </p>


</div>  <!-- close registrationConfirmationPage -->


</asp:Content>

