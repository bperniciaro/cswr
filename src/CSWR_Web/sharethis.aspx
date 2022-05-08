<%@ Page Title="Share Fantasy Cheat Sheet Creation With Your Friends" Language="C#" MasterPageFile="~/MasterPages/NoSport.master" AutoEventWireup="true" 
  CodeFile="sharethis.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.ShareThis" 
  CanonicalUrl="http://www.cheatsheetwarroom.com/donate.aspx" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
  MetaDescription="Be the first to share our cheat sheet creation tools with your friends."
%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

<div class="registrationConfirmationPage">

  <h1>Spread the Word</h1>

  <p>
    As you’ve probably figured out by now, Cheat Sheet War Room is <strong>completely free</strong> even though there are many lesser tools on the market which charge a fee for usage.  
  </p>

  <p>
    If you find this site useful, you could show your appreciation by helping us spread the word.  How?  Take a quick second to do one or more of the following:
  </p>

  <ol>
    <li>
      Use the social media buttons in our banner to share pages & content that you like:
      <asp:Image runat="server" ImageUrl="~/Images/Layout/socialmediabuttons.gif" ToolTip="Example social media buttons for sharing content" />
    </li>
    <li>
      Have a website?
      <asp:HyperLink runat="server" NavigateUrl="~/linktous.aspx">Link to Us</asp:HyperLink>
    </li>
    <li>
      <asp:HyperLink runat="server" NavigateUrl="https://www.facebook.com/cheatsheetwarroom">Like our Facebook page</asp:HyperLink>
      <div class="detailData">We post upcoming features and other site news on our Facebook page.</div>
    </li>
    <li>
      <asp:HyperLink runat="server" NavigateUrl="https://twitter.com/CSWarRoom">Follow Us</asp:HyperLink> on Twitter
      <div class="detailData">We help you stay up-to-speed on player news to ensure your rankings are accurate.</div>
    </li>
    <li>
      <asp:HyperLink runat="server" NavigateUrl="~/Donate.aspx">Make a donation</asp:HyperLink>
    </li>
  </ol>

  <p>
    If you have any problems or ideas for improvement, please <a href="mailto:brad@cheatsheetwarroom.com">contact me directly</a>.
  </p>

  <div style="padding-top:30px 0px 5px 0px;">
    Thanks for checking out the site,
  </div>
  <div style="color:#aaa;font-style:italic;">
    Brad Perniciaro
  </div>

</div>  <!-- close registrationConfirmationPage -->


</asp:Content>

