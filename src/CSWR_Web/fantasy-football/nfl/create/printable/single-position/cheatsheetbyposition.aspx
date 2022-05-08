<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cheatsheetbyposition.aspx.cs" Theme="" Inherits="BP.CheatSheetWarRoom.UI.CheatSheetByPosition"  %>
<%@ Register Src="~/usercontrols/Sports/CreateSheetPrintPositionalSheetTemplate.ascx" TagName="CreateSheetPrintPositionalSheetTemplate" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/GoogleAnalytics.ascx" TagName="GoogleAnalytics" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>

<html lang="en">
<head runat="server">
  <title>Printable Fantasy Football Cheat Sheet</title>
  <link rel="stylesheet" type="text/css" href="~/styles/print.css" media="print" />
  <!-- this page is only for registered members, so it should not be indexed -->
  <meta name="robots" content="NOINDEX,NOFOLLOW" />
  <meta http-equiv="content-language" content="en=us">
  <cswr:GoogleAnalytics runat="server" />
</head>
<body style="background:none;">
<form id="form1" runat="server">


<cswr:MessageBox runat="server" ID="mbError" />

<asp:Panel runat="server" ID="panPrintableSheetContainer">
  <cswr:CreateSheetPrintPositionalSheetTemplate runat="server" ID="ppstSheetTemplate" />
</asp:Panel>



</form>
</body>
</html>
