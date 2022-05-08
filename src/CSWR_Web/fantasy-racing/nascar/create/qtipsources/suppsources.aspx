<%@ Page Language="C#" AutoEventWireup="true" CodeFile="suppsources.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.RAC.SuppSources" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
  <title></title>
  <meta name="robots" content="NOINDEX,NOFOLLOW" />
</head>
<body>
<form id="form1" runat="server">


<asp:Panel runat="server" ID="panSupplementalMenu" CssClass="qtipRACSuppPopup">
  <div class="body">
    <p>
      <%--ADP Rank Value--%>
      <strong><asp:Label runat="server" ID="labSuppRankingsADP"/></strong> - 
      <%--ADP Link--%>
      <asp:HyperLink runat="server" ID="hlADPRankings" Text="Average Draft Position" />
    </p>
    <p>
      <%--CSWR Rank Value --%>
      <strong><asp:Label runat="server" ID="labSuppRankingCSWRRank" /></strong> - 
      <%--CSWR Rank Link--%>
      <asp:HyperLink runat="server" ID="hlCSWRRankings" Text="CSWR Rank" />
    </p>
  </div>
</asp:Panel>


</form>
</body>
</html>
