<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CreateSheetPrintPositionalSheetTemplate.ascx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.CreateSheetPrintPositionalSheetTemplate" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>


<div class="printPositionalSheetTemplateControl">

  <div class="banner">
    <asp:HyperLink CssClass="logo" runat="server" ID="hlHomePage" NavigateUrl="~/" ToolTip="Click to navigate back to the Cheat Sheet War Room home page.">
	    <asp:Image runat="server" ImageUrl="~/Images/Layout/printlogo.gif" AlternateText="Home Page Logo" />
	  </asp:HyperLink>

    <asp:Panel runat="server" ID="panRACSheetSummary" CssClass="sheetSummary">
      This sheet lists your driver rankings from the sheet <asp:HyperLink runat="server" ID="hlSheetName" />.  
      In addition to your custom rankings, this sheet also includes 
      <abbr title="Cheat Sheet War Room">CSWR</abbr> supplemental rankings (in parenthesis) as well
      as any notes you have configured for each driver.
      You can use the roster area at the top of your sheet to enter your draft selections.
    </asp:Panel>

    <asp:Panel runat="server" id="panFOOSheetSummary" CssClass="sheetSummary">
      This sheet lists your 
      <asp:Literal runat="server" ID="litPosition2" />
      rankings from the sheet <asp:HyperLink runat="server" ID="hlSheetName2" />.  
      In addition to your custom rankings and notes,
      this sheet includes <asp:Label runat="server" ID="labSuppStatSeason" /> 
      rankings (in parens) based on
      <abbr title="Total Fantasy Points">TFP</abbr> 
      using the 
      <asp:Label runat="server" ID="labScoringConfiguration"></asp:Label>
      scoring system.
      <asp:Label runat="server" ID="labByeWeeksIncluded" Text="Bye weeks are in brackets. " Visible="false"></asp:Label>
      Use the roster area at the top of your sheet to enter your draft selections.
    </asp:Panel>

  
  </div>

  <div style="clear:both;"/>

  <cswr:MessageBox runat="server" MessageType="NONE" ID="mbMessageBox" />

  <div style="text-align:center;">
	  <table class="rosterArea">
	    <tr>
	      <th colspan="4"><asp:Literal runat="server" ID="litPosition" /></th>
	    </tr>
	    <tr>
	      <td>1</td>
	      <td>2</td>
	      <td>3</td>
	      <td>4</td>
	    </tr>
	    <tr runat="server" id="trRosterSecondRow">
	      <td>5</td>
	      <td>6</td>
	      <td>7</td>
	      <td>8</td>
	    </tr>
	  </table>
  </div>

  <asp:Panel runat="server" ID="panLeftColumn" CssClass="playerColumnContainer">
    <asp:Repeater runat="server" ID="repPlayersLeftSide">
      <ItemTemplate>
      
        <div class="playerContainer">
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
            <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" CssClass="checkBoxImage" />
            <%--Driver Name--%>
            <strong><asp:Label runat="server" ID="labPlayerName" CssClass="nameLabel"/></strong>

            <%--Team--%>
            <asp:Label runat="server" ID="labTeam" CssClass="team"/>
            <%--Bye Week--%>
            <asp:Label runat="server" ID="labByeWeek" CssClass="bye" />
            <%--Sleeper--%>
            <asp:Image runat="server" ID="imaSleeper" Visible="false" ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" CssClass="tag"/>
            <%--Bust--%>
            <asp:Image runat="server" ID="imaBust" Visible="false" ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" CssClass="tag"/>
            <%--Injured--%>
            <asp:Image runat="server" ID="imaInjured" Visible="false" ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" CssClass="tag"/>

            <%--Note--%>
            <em><asp:Label runat="server" ID="labNote" CssClass="note" /></em>
          </p>
        </div>

      </ItemTemplate>
    </asp:Repeater>  
  </asp:Panel>
 
  <asp:Panel runat="server" ID="panRightColumn" CssClass="playerColumnContainer">
    <asp:Repeater runat="server" ID="repPlayersRightSide">
      <ItemTemplate>
    
        <div class="playerContainer">
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
            <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" CssClass="checkBoxImage" />
            <%--Driver Name--%>
            <strong><asp:Label runat="server" ID="labPlayerName" CssClass="nameLabel"/></strong>

            <%--Team--%>
            <asp:Label runat="server" ID="labTeam" CssClass="team"/>
            <%--Bye Week--%>
            <asp:Label runat="server" ID="labByeWeek" CssClass="bye" />
            <%--Sleeper--%>
            <asp:Image runat="server" ID="imaSleeper" Visible="false" ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" CssClass="tag"/>
            <%--Bust--%>
            <asp:Image runat="server" ID="imaBust" Visible="false" ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" CssClass="tag"/>
            <%--Injured--%>
            <asp:Image runat="server" ID="imaInjured" Visible="false" ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" CssClass="tag"/>

            <%--Note--%>
            <em><asp:Label runat="server" ID="labNote" CssClass="note" /></em>
          </p>
        </div>

      </ItemTemplate>
    </asp:Repeater>  
  </asp:Panel>

</div>