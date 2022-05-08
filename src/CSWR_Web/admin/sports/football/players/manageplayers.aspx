<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="manageplayers.aspx.cs" MaintainScrollPositionOnPostback="true" 
  Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.ManagePlayers" Title="Manage Players - Cheat Sheet War Room" EnableEventValidation="false" %>
<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<%@ Register Src="~/admin/usercontrols/FooPlayerManager.ascx" TagName="FooPlayerManager" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

  <h2>Manage Players</h2>

  <cswr:FooPlayerManager runat="server" LimitRights="false" />


</asp:Content>

