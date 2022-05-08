<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tempmanageplayers.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.TempManagePlayers" %>
<%@ Register Src="~/admin/usercontrols/FooPlayerManager.ascx" TagName="FooPlayerManager" TagPrefix="cswr" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
<title></title>
</head>
<body>
<form id="form1" runat="server">

  <cswr:FooPlayerManager runat="server" LimitRights="false" />

</form>
</body>
</html>
