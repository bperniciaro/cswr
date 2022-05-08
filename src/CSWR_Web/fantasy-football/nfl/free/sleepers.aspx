<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveTwoCol.master" Theme="Web20" AutoEventWireup="true" 
  CodeFile="sleepers.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.FootballSleepers" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage" 
  CanonicalUrl="https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/sleepers.aspx"
%>


<%@ Register Src="~/usercontrols/sports/football/playerrankings/CSWRFOOBustSleeperItemTemplate.ascx" TagPrefix="cswr" TagName="CSWRFOOBustSleeperItemTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <h1><asp:Literal runat="server" ID="litMainHeader" /></h1>

  <p style="margin:0px;padding:15px 0px;">
    Our fantasy football sleepers are determined dynamically by comparing our 
    <asp:HyperLink runat="server" ID="hlRankingsListYear" ToolTip="Click to view our most recent NFL player rankings." 
      NavigateUrl="~/fantasy-football/nfl/free/rankings/player-rankings.aspx"></asp:HyperLink>
    against other reputable sources.  Sleepers are assigned a score based on the ranking differential, with the 
    highest scores being those sleepers who are most likely out-play their projected value.
  </p>

  <br />

  <asp:Panel runat="server" ID="panTopAd" Visible="false">
      <script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
      <!-- CSWR Sleepers -->
      <ins class="adsbygoogle"
           style="display:block"
           data-ad-client="ca-pub-3703083047268836"
           data-ad-slot="6822336917"
           data-ad-format="auto"></ins>
      <script>
          (adsbygoogle = window.adsbygoogle || []).push({});
      </script>
  </asp:Panel>

  <asp:Panel runat="server" ID="panSleepersContainer">
    <%--QB Sleepers--%>
    <h2>Quarterback Sleepers</h2>
    <asp:Repeater runat="server" ID="repQBSleepers" onitemdatabound="repSleepers_ItemDataBound">
      <ItemTemplate>
        <cswr:CSWRFOOBustSleeperItemTemplate runat="server" ID="cfbsitBustSleeperItemTemplate" />      
      </ItemTemplate>
    </asp:Repeater>

    <%--RB Sleepers--%>
    <h2>Runnings Back Sleepers</h2>
    <asp:Repeater runat="server" ID="repRBSleepers" onitemdatabound="repSleepers_ItemDataBound">
      <ItemTemplate>
        <cswr:CSWRFOOBustSleeperItemTemplate runat="server" ID="cfbsitBustSleeperItemTemplate" />      
      </ItemTemplate>
    </asp:Repeater>

    <%--WR Sleepers--%>
    <h2>Wide Receiver Sleepers</h2>
    <asp:Repeater runat="server" ID="repWRSleepers" onitemdatabound="repSleepers_ItemDataBound">
      <ItemTemplate>
        <cswr:CSWRFOOBustSleeperItemTemplate runat="server" ID="cfbsitBustSleeperItemTemplate" />      
      </ItemTemplate>
    </asp:Repeater>

    <%--TE Sleepers--%>
    <h2>Tight End Sleepers</h2>
    <asp:Repeater runat="server" ID="repTESleepers" onitemdatabound="repSleepers_ItemDataBound">
      <ItemTemplate>
        <cswr:CSWRFOOBustSleeperItemTemplate runat="server" ID="cfbsitBustSleeperItemTemplate" />      
      </ItemTemplate>
    </asp:Repeater>

    <%--K Sleepers--%>
    <h2>Kicker Sleepers</h2>
    <asp:Repeater runat="server" ID="repKSleepers" onitemdatabound="repSleepers_ItemDataBound">
      <ItemTemplate>
        <cswr:CSWRFOOBustSleeperItemTemplate runat="server" ID="cfbsitBustSleeperItemTemplate" />      
      </ItemTemplate>
    </asp:Repeater>

    <%--DF Sleepers--%>
    <h2>Defense Sleepers</h2>
    <asp:Repeater runat="server" ID="repDFSleepers" onitemdatabound="repSleepers_ItemDataBound">
      <ItemTemplate>
        <cswr:CSWRFOOBustSleeperItemTemplate runat="server" ID="cfbsitBustSleeperItemTemplate" />      
      </ItemTemplate>
    </asp:Repeater>
  </asp:Panel>

  <asp:Panel runat="server" ID="panNoSleepers" Visible="false" style="text-align:center;font-style:italic;">
    Sleepers have not yet been calculated for the 
    <asp:Literal runat="server" ID="litCurrentSeason" />
    fantasy football season.
  </asp:Panel>



</asp:Content>

