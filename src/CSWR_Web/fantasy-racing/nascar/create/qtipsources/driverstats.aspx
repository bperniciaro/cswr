<%@ Page Language="C#" AutoEventWireup="true" CodeFile="driverstats.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.DriverStats" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
  <title></title>
  <meta name="robots" content="NOINDEX,NOFOLLOW" />
</head>
<body>
<form id="form1" runat="server">


<asp:Panel runat="server" ID="panStatsPopupMenu" CssClass="qtipRACStatPopup" >
  <div class="body">
    <table>
      <!--Row 1-->
      <tr>
        <!--Rank-->
        <td class="leftCol">
          Rank
        </td>
        <td style="width:50px">
          <asp:Label runat="server" ID="labStatsPopupRank" />
        </td>
        <!--Winnings-->
        <td class="leftCol">
          Winnings
        </td>
        <td>
          <asp:Label runat="server" ID="labStatsPopupWinnings" />
        </td>
      </tr>
      <!--Row 2-->
      <tr>
        <!--Rank-->
        <td class="leftCol">
          Points
        </td>
        <td>
          <asp:Label runat="server" ID="labStatsPopupPoints" />
        </td>
        <!--Winnings-->
        <td class="leftCol">
          Behind
        </td>
        <td>
          <asp:Label runat="server" ID="labStatsPopupBehind" />
        </td>
      </tr>
      <!--Row 3-->
      <tr>
        <!--Starts-->
        <td class="leftCol">
          Starts
        </td>
        <td>
          <asp:Label runat="server" ID="labStatsPopupStarts" />
        </td>
        <!--Poles-->
        <td class="leftCol">
          Poles
        </td>
        <td>
          <asp:Label runat="server" ID="labStatsPopupPoles" />
        </td>
      </tr>
      <!--Row 4-->
      <tr>
        <td class="leftCol">
          Wins
        </td>
        <td>
          <asp:Label runat="server" ID="labStatsPopupWins" />
        </td>
        <td class="leftCol">
          <abbr title="Average Finish Position">AFP</abbr>
        </td>
        <td>
          <asp:Label runat="server" ID="labStatsPopupAFP" />
        </td>
      </tr>
      <!--Row 5-->
      <tr>
        <td class="leftCol">
          Top Five
        </td>
        <td>
          <asp:Label runat="server" ID="labStatsPopupTop5" />
        </td>
        <td class="leftCol">
          Top Ten
        </td>
        <td>
          <asp:Label runat="server" ID="labStatsPopupTop10" />
        </td>
      </tr>
    </table>

  </div>
</asp:Panel>




</form>
</body>
</html>
