<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CSWRFOORankingItemTemplate.ascx.cs" EnableViewState="false" 
  Inherits="BP.CheatSheetWarRoom.UI.UserControls.CSWRFOORankingItemTemplate" %>

<a runat="server" id="playerAnchor"></a>     

<div runat="server" id="divPlayerRankingItem">
  <table class="playerTable teamColors">
    <tr>
      <%--Ranking Cell--%>
      <td class="nCell">
        <div>
  	      <asp:Label runat="server" ID="labRanking" CssClass="number" />
        </div>
	    </td>
      <td class="helmetCell">
        <asp:Image runat="server" ID="imaHelmet"/>
      </td>
      <%--Player Cell--%>
	    <td class="pCell" colspan="2">
        <table class="nameNumber">
          <tr>
            <td colspan="2">
              <asp:Label runat="server" ID="labPlayerName" CssClass="name"/>
              <asp:Label runat="server" ID="labNumber" CssClass="number"/> 
            </td>
          </tr>
        </table>
        <table class="teamExpBye">
          <tr>
            <td>
              <asp:Literal runat="server" ID="litTeam" />
              <asp:Literal runat="server" ID="litExperience" />
              <asp:Literal runat="server" ID="litBye" />
            </td>
          </tr>
        </table>

	    </td>
	    <%--Tags Cell--%>
<%--	    <td class="tCell">
	    </td>--%>
    </tr>
    <!-- Numbers -->
    <tr class="sCell">
      <td colspan="4" class="sCell">
	      <asp:Repeater runat="server" ID="repStats" OnItemDataBound="repStats_ItemDataBound">
	        <ItemTemplate>
	          <asp:Label runat="server" ID="labStatCode" CssClass="statCode" />=<asp:Literal runat="server" ID="litStatValue" />
	        </ItemTemplate>
	      </asp:Repeater>
	    </td>
    </tr>
  </table>
</div>
