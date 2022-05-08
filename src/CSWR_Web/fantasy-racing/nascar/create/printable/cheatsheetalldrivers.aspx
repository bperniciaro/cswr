<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cheatsheetalldrivers.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.CheatSheetAllDrivers" Theme="" %>
<%@ Register Src="~/usercontrols/Sports/CreateSheetPrintPositionalSheetTemplate.ascx" TagName="CreateSheetPrintPositionalSheetTemplate" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/GoogleAnalytics.ascx" TagName="GoogleAnalytics" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
  <title>Printable Fantasy Racing Cheat Sheet</title>
  <link rel="stylesheet" type="text/css" href="~/styles/printable/web.css" media="screen" />
  <link rel="stylesheet" type="text/css" href="~/styles/printable/print.css" media="print" />
  <meta http-equiv="content-language" content="en=us">
  <!-- this page is only for registered members, so it should not be indexed -->
  <meta name="robots" content="NOINDEX,NOFOLLOW" />
  <cswr:GoogleAnalytics runat="server" />
</head>
<body>
<form id="form1" runat="server">

<cswr:MessageBox runat="server" ID="mbError" />

<asp:Panel runat="server" ID="panPrintableSheetContainer">
  <cswr:CreateSheetPrintPositionalSheetTemplate runat="server" ID="ppstSheetTemplate" />
</asp:Panel>



</form>
</body>
</html>
