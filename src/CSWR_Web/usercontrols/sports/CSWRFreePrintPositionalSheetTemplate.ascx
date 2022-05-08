<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CSWRFreePrintPositionalSheetTemplate.ascx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.UserControls.CSWRFreePrintPositionalSheetTemplate" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>



<div class="cswrPrintPositionSheetTemplateControl">

  <div class="banner">

    <!-- Logo -->
    <asp:HyperLink CssClass="logo" runat="server" ID="hlHomePage" NavigateUrl="~/" ToolTip="Click to navigate back to the Cheat Sheet War Room home page.">
	    <asp:Image runat="server" ImageUrl="~/Images/Layout/printlogo.gif" AlternateText="Home Page Logo" />
	  </asp:HyperLink>

    <!-- Title and Roster -->
    <asp:Panel runat="server" ID="panTitleAndRoster" CssClass="headerAndTitle">
    
      <h1>Printable <asp:Literal runat="server" ID="litHeaderStub" /> Cheat Sheet</h1>
      
	    <table class="rosterArea">
	      <tr>
	        <th colspan="3"><asp:Literal runat="server" ID="litPosition3" /></th>
	      </tr>
	      <tr>
	        <td>1</td><td>2</td><td>3</td>
	      </tr>
	    </table>
      
    </asp:Panel>

  </div> <!-- close banner -->

  <div style="clear:both;"/>
  
  <%--Explanation of sheet with additional links--%>
  <asp:Panel runat="server" ID="panSheetSummary" CssClass="sheetSummary">
    <p>
      This free, printable 
      fantasy
      <asp:Literal runat="server" ID="litSport" />
      <strong><asp:Literal runat="server" ID="litPosition" /> cheat sheet</strong>
      lists the top 
      <asp:HyperLink runat="server" ID="hlPositionalRankings" Target="_blank"/>
      from cheatsheetwarroom.com for the 
      <asp:Label runat="server" ID="labSportSeason" />
      <asp:Label runat="server" ID="labSportLeagueAbbreviation" />
      season.  
      We are your one-stop shop for custom fantasy football cheat sheets.
      But why would you rely on a random website for your fantasy rankings when you can easily create your own 
      <asp:HyperLink runat="server" ID="hlLandingPage"></asp:HyperLink>
      for free using drag and drop? 
    </p>
  </asp:Panel>
  
  <%--Legend--%>
  <asp:Panel runat="server" ID="panLegend" CssClass="legend">
    <span class="rank">Rank</span>
    <span runat="server" id="spSuppRanking" class="suppRanking">(<asp:Literal runat="server" ID="litStatSeason"/> rank <asp:Label runat="server" ID="labFootballSuppDescription" Text="using total fantasy points" />)</span>
    <asp:Image runat="server" ID="imaLegendCheckbox" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" CssClass="checkBoxImage"/>
    <span class="playerName"><asp:Literal runat="server" ID="litPlayerType" /> Name</span>
    <asp:Label runat="server" ID="labLegendTeam" CssClass="team">TEAM</asp:Label>
    <asp:Label runat="server" ID="labLegendBye" CssClass="bye">[Bye]</asp:Label>
  </asp:Panel>
  
  <cswr:MessageBox runat="server" MessageType="NONE" ID="mbMessageBox" />

  <%--Left Column Players--%>
  <asp:Panel runat="server" ID="panLeftColumn" CssClass="playerColumnContainer">
    <asp:Repeater runat="server" ID="repPlayersLeftSide">
      <ItemTemplate>
        <div class="player">
          <p runat="server" id="parContainer">        
            <%--Rank--%>
            <span class="rank">
              <asp:Label runat="server" ID="labRank"/>
            </span>
            <%--Supp Ranking--%>
            <span class="suppRanking">
              <asp:Label runat="server" ID="labSuppRanking" />
            </span>
            <%--Checkbox--%>
            <asp:Image runat="server" ID="imaPlayerCheckbox" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" CssClass="checkBoxImage" />
            <%--Driver Name--%>
            <strong><asp:Label runat="server" ID="labPlayerName" CssClass="nameLabel"/></strong>
            <%--Team--%>
            <asp:Label runat="server" ID="labTeam" CssClass="team"/>
            <%--Bye Week--%>
            <asp:Label runat="server" ID="labByeWeek" CssClass="bye" />
            <%--Sleeper--%>
            <asp:Image runat="server" ID="imaSleeper" Visible="false" ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag_large.gif" CssClass="tag"/>
            <%--Bust--%>
            <asp:Image runat="server" ID="imaBust" Visible="false" ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag_large.gif" CssClass="tag"/>
          </p>
        </div>
      </ItemTemplate>
    </asp:Repeater>  
  </asp:Panel>

  <%--Right Column Players--%> 
  <asp:Panel runat="server" ID="panRightColumn" CssClass="playerColumnContainer">
    <asp:Repeater runat="server" ID="repPlayersRightSide">
      <ItemTemplate>
        <div class="player">
          <p runat="server" id="parContainer">        
            <%--Rank--%>
            <span class="rank">
              <asp:Label runat="server" ID="labRank" />
            </span>
            <%--Supp Ranking--%>
            <span class="suppRanking">
              <asp:Label runat="server" ID="labSuppRanking" />
            </span>
            <%--Checkbox--%>
            <asp:Image runat="server" ID="imaPlayerCheckbox" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" CssClass="checkBoxImage" />
            <%--Driver Name--%>
            <strong><asp:Label runat="server" ID="labPlayerName" CssClass="nameLabel"/></strong>
            <%--Team--%>
            <asp:Label runat="server" ID="labTeam" CssClass="team"/>
            <%--Bye Week--%>
            <asp:Label runat="server" ID="labByeWeek" CssClass="bye" />
            <%--Sleeper--%>
            <asp:Image runat="server" ID="imaSleeper" Visible="false" ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag_large.gif" CssClass="tag"/>
            <%--Bust--%>
            <asp:Image runat="server" ID="imaBust" Visible="false" ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag_large.gif" CssClass="tag"/>
          </p>
        </div>
      </ItemTemplate>  
    </asp:Repeater>  
  </asp:Panel>

</div>

