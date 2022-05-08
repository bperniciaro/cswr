<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveOneCol.master" Theme="Web20" AutoEventWireup="true" CodeFile="about.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.About" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
  Title="The Story Behind Cheat Sheet War Room"
  MetaDescription="I created Cheat Sheet War Room to make fantasy football cheat sheet creation simple, intuitive, and fun."
  CanonicalUrl="https://www.cheatsheetwarroom.com/about.aspx"
%>
<%@ MasterType VirtualPath="~/MasterPages/NoSport.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

<div class="aboutPage">

  <h1>About Cheat Sheet War Room</h1>

  <p class="intro">
    Cheat Sheet War Room was born from my frustration with creating 
    <asp:HyperLink runat="server" NavigateUrl="https://www.cheatsheetwarroom.com" ToolTip="Create custom fantasy cheat sheet sheets.">fantasy cheat sheets</asp:HyperLink>
    for my various fantasy football drafts.  For me,
    the process of creating 
    <asp:HyperLink runat="server" NavigateUrl="~/fantasy-football/nfl/create/custom-sheet.aspx">fantasy football cheat sheets</asp:HyperLink>
    has always been fun, but I routinely found myself spending more time 
    creating, populating, and formating my spreadsheets than I spent actually manipulating my 
    <asp:HyperLink runat="server" NavigateUrl="~/fantasy-football/nfl/free/rankings/player-rankings.aspx">fantasy football player rankings</asp:HyperLink>.
  </p>

  <p>
    Being a software developer by trade, I recognized that many of the steps I was performing on my
    spreadsheet-based cheat sheets could be automated.  In addition, if I could store all of the player data and statistics in a database, 
    cheat sheets could calculate fantasy point output according to specific league scoring rules beyond the stantard
    <asp:HyperLink runat="server" NavigateUrl="https://www.cheatsheetwarroom.com/blog/fantasy-football/leagues/scoring-systems">fantasy football points system</asp:HyperLink>.
    Finally, by incorporating all of my 
    desired functionality into a single, dynamic, intuitive, drag & drop interface, I could both erase the prep time needed 
    to create, populate, and format my cheat sheets while also reducing the time needed to manipulate my player rankings.    
  </p>

  <p>
    Armed with my idea (and the need to learn ASP.net programming for my new job) I started working and within a year launched the first version 
    of Chat Sheet War Room.  Since its launch, this application has gained a strong following in the fantasy football community and people 
    have been amazed that an application with so much functionality could be completely free.  I regularly receive positive feedback from our users 
    and try my best to incorporate any suggestions into the application. 
  </p>

  <p>
    Since the problems associated with cheat sheet creation are common to all 
    fantasy sports, shouldn't there be a free cheat sheet creation interface for every fantasy sport imaginable?  I believe so and have integeated 
    <asp:HyperLink runat="server" NavigateUrl="~/fantasy-racing/nascar/create/custom-sheet.aspx">fantasy nascar racing cheat sheets</asp:HyperLink>
    into the site as well.  My ultimate goal is to 
    create a community where fantasy sports enthusists can gather to research player news, discuss player rankings, and apply that 
    knowledge directly to their fantasy cheat sheets in an intuitive manner.  
  </p>

  <p>
    When preparing for your fantasy draft, your time should be spent creating educated rankings based on the most relevant and current 
    player information available.  Time spent formatting spreadsheets and scouring the web for useful information is time wasted.     
    The days of clumsily copying and pasting players around a spreadsheet using outdated information 
    are over.  Separate yourself from the competition by creating custom player rankings in a fraction of the time, for free.  
  </p>



</div>




</asp:Content>

