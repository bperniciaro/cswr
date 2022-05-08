<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CSWRRACRankingItemTemplate.ascx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.UserControls.CSWRRACRankingItemTemplate" EnableViewState="false" %>


<div runat="server" id="divPlayerRankingItem">
  <table class="playerTable racTable teamColors">
    <tr>
      <%--Ranking Cell--%>
      <td class="nCell">
	      <asp:Label runat="server" ID="labRanking" CssClass="number" /><asp:Label runat="server" ID="labDecimalPart" CssClass="decimal" />
	    </td>
      <%--Player Cell--%>
	    <td class="pCell">
        <p class="playerName">
          <asp:Literal runat="server" ID="litPlayerName" />
          <asp:Label runat="server" ID="labCarNumber" />
        </p>
        <table>
          <tr>
            <td>
              <asp:Literal runat="server" ID="litTeam" />
              <asp:Literal runat="server" ID="litExperience" />
            </td>
            <td class="twitterCell">
              <%--<asp:HyperLink runat="server" ID="hlTwitter" CssClass="twitterLink" NavigateUrl="#"/>--%>
            </td>
          </tr>
        </table>

	    </td>
    </tr>
    <!-- Numbers -->
    <tr>
      <td colspan="3" class="sCell">
	      <asp:Repeater runat="server" ID="repStats" OnItemDataBound="repStats_ItemDataBound">
	        <ItemTemplate>
	          <asp:Label runat="server" ID="labStatCode" CssClass="statCode" />=<asp:Literal runat="server" ID="litStatValue" />
	        </ItemTemplate>
	      </asp:Repeater>
	    </td>
    </tr>
  </table>
</div>
