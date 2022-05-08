<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="sheetsettings.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.SheetSettings" Title="Manage Sheets - Cheat Sheet War Room" %>
<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

<asp:ScriptManager runat="server" />

<div class="sheetSettingsPage">

  <h2>Football Settings</h2>

  <asp:UpdatePanel runat="server" ID="upCorruption">
    <ContentTemplate>

      <cswr:MessageBox runat="server" ID="mbStatus" />

      <table>
        <%--Source 1--%>
        <tr>
          <td>
            <asp:Label runat="server" ID="labSuppSource1" Text="Default Source 1"/>
          </td>
          <td>
            <asp:DropDownList runat="server" ID="ddlSuppSource1" DataTextField="Name" DataValueField="SupplementalSourceID"/>
          </td>
        </tr>
        <%--Source 2--%>
        <tr>
          <td>
            <asp:Label runat="server" ID="labSuppSource2" Text="Default Source 2"/>
          </td>
          <td>
            <asp:DropDownList runat="server" ID="ddlSuppSource2" DataTextField="Name" DataValueField="SupplementalSourceID"/>
          </td>
        </tr>
        <%--Source 3--%>
        <tr>
          <td>
            <asp:Label runat="server" ID="labSuppSource3" Text="Default Source 3"/>
          </td>
          <td>
            <asp:DropDownList runat="server" ID="ddlSuppSource3" DataTextField="Name" DataValueField="SupplementalSourceID"/>
          </td>
        </tr>
        <%--Show Supplementals--%>
        <tr>
          <td>
            Show Supps
          </td>
          <td>
            <asp:CheckBox runat="server" ID="cbShowSupps" />  
          </td>
        </tr>
        <%--Show Affiliate ADs--%>
        <tr>
          <td>
            Show Affiliate Advertisements
          </td>
          <td>
            <asp:CheckBox runat="server" ID="cbShowAffiliates" />  
          </td>
        </tr>
        <%--Calculate ADP--%>
        <tr>
          <td>
            Calculate ADP
          </td>
          <td>
            <asp:CheckBox runat="server" ID="cbCalculateADP" />  
          </td>
        </tr>
        <%--TimespanInDays--%>
        <tr>
          <td>
            ADP Timespan (days)
          </td>
          <td>
            <asp:TextBox runat="server" ID="tbTimespanInDays" />
          </td>
        </tr>


      </table>

      <asp:Button runat="server" ID="butSaveSheetSettings" onclick="butSaveSheetSettings_Click" Text="Save Settings" />

    </ContentTemplate>
  </asp:UpdatePanel>

  <hr />


  <h2>Operations</h2>

  <div class="visitorSheets">
    Delete FOO Visitor Sheets:
    <asp:Button runat="server" ID="butDeleteFOOVisitorSheets" Text="Delete" 
      onclick="butDeleteFOOVisitorSheets_Click" />
  </div>
  
  <div>
    Delete all FOO Sheets more than 2 weeks old
    <asp:Button runat="server" ID="butDeleteOldFOOSheets" Text="Delete" 
      onclick="butDeleteOldFOOSheets_Click" />
  </div>
  

</div>


</asp:Content>

