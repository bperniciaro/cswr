<%@ Page Language="C#" AutoEventWireup="true" Theme="" CodeFile="teamplayerranks.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.TeamPlayerRanks" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
  <title>Team Player Rankings</title>
  <meta name="robots" content="NOINDEX,NOFOLLOW" />
</head>
<body>
<form id="form1" runat="server">

<%-- QTip Stat Popu --%>
<div class="qtipFOOTeamRankPopup">
  <table>
    <!-- Column Headers -->
    <tr class="subTitle">
      <th>Rank</th>
      <th>Player</th>
      <th runat="server" id="thPositionHeader">Position</th>
      <th runat="server" id="thCSWRHeader">CSWR</th>
      <th runat="server" id="thCBSHeader">CBS</th>
    </tr>
    <asp:Repeater runat="server" ID="repPlayers" onitemdatabound="repPlayers_ItemDataBound">
      <ItemTemplate>
        <tr runat="server" id="trRankingRow">
          <%--Rank--%>
          <td>
            <asp:Label runat="server" ID="labRank" />
            <asp:HyperLink runat="server" ID="hlPlayerAnchor" />
          </td>
          <%--Name--%>
          <td>
            <asp:Label runat="server" ID="labName" />
          </td>
          <%--Position--%>
          <td runat="server" id="tdPositionCell">
            <asp:Label runat="server" ID="labPosition" />
          </td>
          <%--CSWR--%>
          <td runat="server" id="tdCSWRCell">
            <asp:HyperLink runat="server" Target="_blank" ID="hlCSWRRank"></asp:HyperLink>
            <asp:Label runat="server" ID="labCSWRRank"></asp:Label>
          </td>
          <%--CBS--%>
          <td runat="server" id="tdCBSCell">
            <asp:Label runat="server" ID="labCBSRank" />
          </td>
        </tr>
      </ItemTemplate>
    </asp:Repeater>
  </table>
</div>



</form>
</body>
</html>
