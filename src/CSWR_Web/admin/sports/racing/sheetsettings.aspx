<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="sheetsettings.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.RACSheetSettings" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

<asp:ScriptManager runat="server" />

<div class="sheetSettingsPage">

  <h2>Racing Settings</h2>

  <asp:UpdatePanel runat="server" ID="upCorruption">
    <ContentTemplate>

      <cswr:MessageBox runat="server" ID="mbStatus" />

      <table>
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
      </table>

      <asp:Button runat="server" ID="butSaveSheetSettings" onclick="butSaveSheetSettings_Click" Text="Save Settings" />

    </ContentTemplate>
  </asp:UpdatePanel>

</div>  
  
  <h4>Calc ADP</h4>
  <h4>Time Between ADPs</h4>
  <h4>Minimum Modification Period For Sheet Inclusion</h4>

  <asp:Button runat="server" ID="butCalcADP" Text="CalcADPManually" onclick="butCalcADP_Click" />

</asp:Content>

