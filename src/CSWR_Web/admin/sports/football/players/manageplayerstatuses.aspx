<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="manageplayerstatuses.aspx.cs" 
  Inherits="admin_sports_football_players_manageplayerstatuses" ValidateRequest="false" %>
<%@ Register src="../../../usercontrols/FooPlayerStatusManager.ascx" TagName="FooPlayerStatusManager" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">
  
  <h1>Manage Player Statuses</h1>

  <cswr:FooPlayerStatusManager runat="server" ID="fpsm"/>
  

</asp:Content>

