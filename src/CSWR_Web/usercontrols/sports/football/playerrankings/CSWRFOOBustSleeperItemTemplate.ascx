<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CSWRFOOBustSleeperItemTemplate.ascx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.UserControls.CSWRFOOBustSleeperItemTemplate" %>

<div runat="server" id="divPlayerRankingItem">
  <table class="playerTable teamColors">
    <tr>
      <%--Ranking Cell--%>
      <td class="nCell" style="display:none;">

	    </td>
      <td class="helmetCell">
        <asp:Image runat="server" ID="imaHelmet"/>
      </td>
      <%--Player Cell--%>
	    <td class="pCell">
        <table class="nameNumber">
          <tr>
            <td>
              <asp:Label runat="server" ID="labPlayerName" CssClass="name"/>  
            </td>
            <%--<td class="twitterCell">
              <asp:HyperLink runat="server" ID="hlTwitter" CssClass="twitter" NavigateUrl="#"/>
            </td>--%>
          </tr>
        </table>
        <table class="teamExpBye">
          <tr>
            <td>
              <asp:Literal runat="server" ID="litNumber" />  
              <asp:Literal runat="server" ID="litTeam" />
              <asp:Literal runat="server" ID="litExperience" />
              <asp:Literal runat="server" ID="litBye" />
            </td>
          </tr>
        </table>

	    </td>
	    <%--Tags Cell--%>
	    <td class="tCell"">
        <asp:Panel runat="server" ID="panRankings">
          <table>
            <tr>
              <td rowspan="2">
                <asp:Label runat="server" ID="labRankDifference" CssClass="rankDifference"/>
              </td>
              <td class="sourceRank">
                CSWR: 
                <asp:HyperLink runat="server" ID="hlCSWRRank"> <asp:Label runat="server" ID="labCSWRRank" /> 
                <abbr runat="server" id="acrPositionName"></abbr> </asp:HyperLink>
                
              </td>
            </tr>
            <tr>
              <td class="sourceRank">
                CBS: <asp:Label runat="server" ID="labCBSRank" />
              </td>
            </tr>
          </table>
        </asp:Panel>
	    </td>
    </tr>
    <!-- Numbers -->
    <tr style="display:none;">
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
