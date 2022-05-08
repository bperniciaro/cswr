<%@ Page Language="C#" AutoEventWireup="true" Theme="" CodeFile="suppsources.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.FOO.SuppSources" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
  <title></title>
  <meta name="robots" content="NOINDEX,NOFOLLOW" />
</head>
<body>
<form id="form1" runat="server">

<div class="qtipFOOSuppPopup">
  <table>
    <%--CSWR--%>
    <tr>
      <td class="leftCol">
        <asp:Label runat="server" id="labCSWR" CssClass="bold" />
      </td>
      <td>
        <asp:Hyperlink runat="server" ID="hlCSWRRank" NavigateUrl="~/fantasy-football/nfl/free/rankings/offense/wide-receivers.aspx" Target="_blank">CSWR Rank</asp:Hyperlink>
      </td>
    </tr>
    <%--CBS Rank--%>
    <tr>
      <td class="leftCol">
        <asp:Label runat="server" id="labCBS" CssClass="bold" />
      </td>
      <td>
        <asp:Hyperlink runat="server" ID="hlCBSRank" rel="nofollow" NavigateUrl="~/fantasy-football/nfl/free/rankings/offense/wide-receivers.aspx"  Target="_blank">CBSSports Rank</asp:Hyperlink>
      </td>
    </tr>
    <%--ADP--%>
    <%--<tr>
      <td>
        <asp:Label runat="server" id="labADP" />
      </td>
      <td>ADP</td>
    </tr>--%>
  </table>
</div>

  <%--<script type="text/javascript">
    // Create the tooltips only on document load
    $(document).ready(function () {
      $('[title]').qtip({
        show: {
          delay: 550
        }
      });
    });
  </script>--%>

</form>
</body>
</html>
