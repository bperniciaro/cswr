<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" 
  CodeFile="managedrivers.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.ManageDrivers" %>
<%@ Register Src="~/admin/usercontrols/PlayerManager.ascx" TagName="PlayerManager" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

<div class="manageRacingDriversPage">
  
  <h2>Manage Drivers</h2>
  
  <cswr:PlayerManager runat="server" SportCode="RAC" />
  
     
</asp:Content>

