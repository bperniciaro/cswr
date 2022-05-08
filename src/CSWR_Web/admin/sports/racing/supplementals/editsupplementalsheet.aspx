<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="editsupplementalsheet.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.EditSupplementalSheet" 
Title="Edit Supplemental Sheet - Cheat Sheet War Room" %>
<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<%@ Register Src="~/usercontrols/Sports/SheetItemManager.ascx" TagName="SheetItemManager" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

  <h2>Edit Supplemental Sheet</h2>

  <asp:ScriptManager runat="server" ID="smScriptManager" />

  <div class="editSuppSheetPage">
    
    <div class="activeSheet">
      <asp:Label runat="server" ID="labSource" />: 
      <asp:Label runat="server" ID="labPosition" />
    </div>
    
    <br />
    <uc1:SheetItemManager ID="spmSheetItemManager" runat="server" SheetType="SuppSheet" SportCode="RAC" />

    <asp:HyperLink runat="server" ID="hlBackToSheet" Text="Go back to sheet" />
  
  </div>


</asp:Content>

