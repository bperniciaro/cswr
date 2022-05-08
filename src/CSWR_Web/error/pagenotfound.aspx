<%@ Page Title="Page Not Found" Language="C#" MasterPageFile="~/MasterPages/NoSport.master" AutoEventWireup="true" CodeFile="pagenotfound.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.PageNotFound" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"  
  MetaDescription="The page you requested cannot be found."
  MetaRobotsText="NOINDEX,FOLLOW" 
  %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <h1>Page Not Found</h1>

  <p>
    The page you requested does not exist, but we have other useful pages that you would most-definitely enjoy.
  </p>

  <br />
  <h2>Custom Fantasy Football Cheat Sheet</h2>
  <p>
    Use our 
    <asp:HyperLink runat="server" NavigateUrl="~/fantasy-football/nfl/create/custom-sheet.aspx">cheat sheet creation interface</asp:HyperLink>
    to easily create player rankings using drag & drop.
  </p>

  <h2>Current NFL Player Rankings</h2>

  <p>
    Check out our 
    <asp:HyperLink runat="server" NavigateUrl="~/fantasy-football/nfl/free/rankings/player-rankings.aspx">pre-season NFL player rankings</asp:HyperLink>
    along with player stats.
  </p>

  <h2>Print a Free Cheat Sheet</h2>

  <p>
    Our 
    <asp:HyperLink runat="server" NavigateUrl="~/fantasy-football/nfl/free/printable/offense/cheat-sheet-with-roster.aspx">printable cheatsheet</asp:HyperLink>
    contains all major offensive positions and is draft-ready.
  </p>


</asp:Content>

