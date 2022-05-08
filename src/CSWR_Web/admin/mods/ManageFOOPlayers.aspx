<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageFOOPlayers.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.Admin.Mods.ManageFOOPlayers" %>
<%@ Register Src="~/admin/usercontrols/FooPlayerManager.ascx" TagName="FOOPlayerManager" TagPrefix="cswr" %>
<!DOCTYPE html>
<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
</head>
<body style="background-color:white;background:none;">
<form id="form1" runat="server">

  <asp:HyperLink runat="server" NavigateUrl="~/Default.aspx" Text="Home Page" />

  <br />
  <br />

  <h2>Manage Players </h2>
  <cswr:FOOPlayerManager runat="server" ID="fpmPlayerManager" LimitRights="true" />

</form>
</body>
</html>
