<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveTwoCol.master" Theme="Web20" AutoEventWireup="true" 
  CodeFile="busts.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.FootballBusts" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage" 
  CanonicalUrl="https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/busts.aspx"
%>
<%@ Register Src="~/usercontrols/sports/football/playerrankings/CSWRFOOBustSleeperItemTemplate.ascx" TagPrefix="cswr" TagName="CSWRFOOBustSleeperItemTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <h1><asp:Literal runat="server" ID="litMainHeader" /></h1>

  <p style="margin:0px;padding:15px 0px;">
    Our fantasy football busts are determined dynamically by comparing our 
    <asp:HyperLink runat="server" ID="hlRankingsListYear" ToolTip="Click to view our most recent NFL player rankings." 
      NavigateUrl="~/fantasy-football/nfl/free/rankings/player-rankings.aspx"></asp:HyperLink>
    against other reputable sources.  Busts are assigned a score based on the ranking differential, with the 
    highest negative score being those busts who are most likely under-play their projected value.
  </p>

  <br />

  <asp:Panel runat="server" ID="panTopAd" Visible="false">
      <script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
      <!-- CSWR Busts -->
      <ins class="adsbygoogle"
           style="display:block"
           data-ad-client="ca-pub-3703083047268836"
           data-ad-slot="5896268059"
           data-ad-format="auto"></ins>
      <script>
          (adsbygoogle = window.adsbygoogle || []).push({});
      </script>
  </asp:Panel>

  <asp:Panel runat="server" ID="panBustsContainer">

    <h2>Quarterback Busts</h2>
    <asp:Repeater runat="server" ID="repQBBusts" onitemdatabound="repBusts_ItemDataBound">
      <ItemTemplate>
        <cswr:CSWRFOOBustSleeperItemTemplate runat="server" ID="cfbsitBustSleeperItemTemplate" />      
      </ItemTemplate>
    </asp:Repeater>

    <h2>Running Back Busts</h2>
    <asp:Repeater runat="server" ID="repRBBusts" onitemdatabound="repBusts_ItemDataBound">
      <ItemTemplate>
        <cswr:CSWRFOOBustSleeperItemTemplate runat="server" ID="cfbsitBustSleeperItemTemplate" />      
      </ItemTemplate>
    </asp:Repeater>

    <h2>Wide Receiver Busts</h2>
    <asp:Repeater runat="server" ID="repWRBusts" onitemdatabound="repBusts_ItemDataBound">
      <ItemTemplate>
        <cswr:CSWRFOOBustSleeperItemTemplate runat="server" ID="cfbsitBustSleeperItemTemplate" />      
      </ItemTemplate>
    </asp:Repeater>

    <h2>Tight End Busts</h2>
    <asp:Repeater runat="server" ID="repTEBusts" onitemdatabound="repBusts_ItemDataBound">
      <ItemTemplate>
        <cswr:CSWRFOOBustSleeperItemTemplate runat="server" ID="cfbsitBustSleeperItemTemplate" />      
      </ItemTemplate>
    </asp:Repeater>

    <h2>Kicker Busts</h2>
    <asp:Repeater runat="server" ID="repKBusts" onitemdatabound="repBusts_ItemDataBound">
      <ItemTemplate>
        <cswr:CSWRFOOBustSleeperItemTemplate runat="server" ID="cfbsitBustSleeperItemTemplate" />      
      </ItemTemplate>
    </asp:Repeater>

    <h2>Defense Busts</h2>
    <asp:Repeater runat="server" ID="repDFBusts" onitemdatabound="repBusts_ItemDataBound">
      <ItemTemplate>
        <cswr:CSWRFOOBustSleeperItemTemplate runat="server" ID="cfbsitBustSleeperItemTemplate" />      
      </ItemTemplate>
    </asp:Repeater>

  </asp:Panel>

  <asp:Panel runat="server" ID="panNoBusts" Visible="false" style="text-align:center;font-style:italic;">
    Busts have not yet been calculated for the 
    <asp:Literal runat="server" ID="litCurrentSeason" />
    fantasy football season.
  </asp:Panel>

</asp:Content>

