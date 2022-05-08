<%@ Page Title="Add a link to Cheat Sheet War Room from your website" Language="C#" MasterPageFile="~/MasterPages/NoSport.master" 
  AutoEventWireup="true" CodeFile="linktous.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.LinkToUs" 
  MetaDescription="If you like our application, consider adding a link to us from your site."
  CanonicalUrl="http://www.cheatsheetwarroom.com/linktous.aspx" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
  %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

<div class="linkToUsPage">

  <h1>How to link to us</h1>

  <p class="intro">
    If you like this site, we'd appreciate a link.  For a quick and easy way to link to our site, simply copy &  paste the following
    HTML code into your web page.  You can choose from any of the following options, or create your own if you'd like to link to a different page.
  </p>

  <asp:Panel runat="server" id="panTextLink1">
    <h2>Option 1: Text & Link</h2>
    <p>
      Create a free, customized <a href="http://www.cheatsheetwarroom.com/fantasy-football/nfl/create/custom-sheet.aspx" title="Click to generate a custom fantasy football cheat sheet on an interactive, web-based cheat sheet.">fantasy football cheat sheet</a>.
    </p>
    <textarea rows="3" cols="70" readonly="readonly">Create a free, customized &lt;a href=&quot;http://www.cheatsheetwarroom.com/fantasy-football/nfl/create/custom-sheet.aspx&quot; title=&quot;Click to generate a custom fantasy football cheat sheet on an interactive, web-based cheat sheet.&quot;&gt;fantasy football cheat sheet&lt;/a&gt;.</textarea>
  </asp:Panel>

  <asp:Panel runat="server" id="panImageLink1">
    <h2>Option 2: Link</h2>
    <p>
      <a href="http://www.cheatsheetwarroom.com/fantasy-football/nfl/free/printable/offense/cheat-sheet-with-roster.aspx" title="Click to generate a printable fantasy football cheat sheet with roster area.">Printable Fantasy Football Cheat Sheet</a>
    </p>
    <textarea rows="3" cols="70" readonly="readonly">&lt;a href=&quot;http://www.cheatsheetwarroom.com/fantasy-football/nfl/free/printable/offense/cheat-sheet-with-roster.aspx&quot; title=&quot;Click to generate a printable fantasy football cheat sheet with roster area.&quot;&gt;Printable Fantasy Football Cheat Sheet&lt;/a&gt;</textarea>
  </asp:Panel>

  <asp:Panel runat="server" id="panButtonLink1">
    <h2>Option 3: Button</h2>
    <p>
      <a href="http://www.cheatsheetwarroom.com" title="Click to create free fantasy cheat sheets.">
        <asp:Image runat="server" ImageUrl="http://www.cheatsheetwarroom.com/Images/Layout/linktous/cswr-button.gif" AlternateText="Create free fantasy cheat sheets." />
      </a>
    </p>
    <textarea rows="3" cols="70" readonly="readonly">&lt;a href=&quot;http://www.cheatsheetwarroom.com&quot; title=&quot;Click to create free fantasy cheat sheet.&quot;&gt; &lt;img src=&quot;http://www.cheatsheetwarroom.com/Images/Layout/linktous/cswr-button.gif&quot; alt=&quot;Create free fantasy cheat sheets.&quot; /&gt; &lt;/a&gt;</textarea>
  </asp:Panel>

  <asp:Panel runat="server" id="panMiniButtonLink1">
    <h2>Option 4: Mini-Button</h2>
    <p>
      <a href="http://www.cheatsheetwarroom.com" title="Click to create free fantasy cheat sheets.">
        <asp:Image runat="server" ImageUrl="http://www.cheatsheetwarroom.com/Images/Layout/linktous/cswr-minibutton.gif" AlternateText="Create free fantasy cheat sheets." />
      </a>
    </p>
    <textarea rows="3" cols="70" readonly="readonly">&lt;a href=&quot;http://www.cheatsheetwarroom.com&quot; title=&quot;Click to create free fantasy cheat sheet.&quot;&gt; &lt;img src=&quot;http://www.cheatsheetwarroom.com/Images/Layout/linktous/cswr-minibutton.gif&quot; alt=&quot;Create free fantasy cheat sheets.&quot; /&gt; &lt;/a&gt;</textarea>
  </asp:Panel>

</div>


<script type="text/javascript">
  $('textarea').click(function () {
    // the select() function on the DOM element will do what you want
    this.select();
  });</script>

</asp:Content>

