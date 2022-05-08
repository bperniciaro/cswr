<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" 
  CodeFile="managesupplementalsheets.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.ManageSupplementalSheets" 
Title="Manage Supplemental Sheets - Cheat Sheet War Room" %>
<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<%@ Register Src="~/admin/usercontrols/SuppSheetManager.ascx" TagName="SuppSheetManager" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

<h2>Manage Football Supplemental Sheets</h2>

<cswr:SuppSheetManager runat="server" SportCode="FOO" />


</asp:Content>

