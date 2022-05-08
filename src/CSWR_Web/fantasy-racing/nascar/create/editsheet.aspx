<%@ Page Title="Edit Fantasy Racing Cheat Sheet" Language="C#" MasterPageFile="~/MasterPages/Sport.master" AutoEventWireup="true" 
  CodeFile="editsheet.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.EditSheet" MetaRobotsText="NOINDEX,FOLLOW"
  CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage" %>
<%@ Register Src="~/usercontrols/navigation/SheetCreationManageLevelNavigation.ascx" TagName="SheetCreationManageLevelNavigation" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/Sports/SheetItemManager.ascx" TagName="SheetItemManager" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <asp:ScriptManager runat="server" />

  <%--Navigation--%>
  <cswr:SheetCreationManageLevelNavigation runat="server" ID="scmlnNavigation" CurrentStage="EDITSHEET" SportCode="RAC" />
  
  <div class="editSheetPage">
 
    <asp:Panel runat="server" id="panSheetForm" CssClass="racForm sheetForm">
    
      <%--Main Sheet Properties--%>
      <asp:Panel ID="panSheetProperties" runat="server" DefaultButton="butSave" CssClass="sheetProperties">
      
        <table class="main">
          <%--Title--%>
          <tr>
            <th colspan="2">
              Sheet Settings
            </th>
          </tr>
          <%--Message--%>
          <tr runat="server" id="trSettingsMessageRow">
            <td colspan="2">
              <cswr:MessageBox runat="server" ID="mbSheetSettingsMessage" />
            </td>
          </tr>
          <%--Sheet Name--%>
          <tr>
            <td class="leftCol">
              <span class="required">(required)</span> Sheet Name 
            </td>
            <td>
              <asp:TextBox runat="server" ID="tbSheetName" MaxLength="50"></asp:TextBox>
              <asp:RequiredFieldValidator runat="server" ID="rfvNameRequired" ControlToValidate="tbSheetName" Display="Dynamic" SetFocusOnError="true"
                ErrorMessage="<img src='../../Images/error.gif' alt='Sheet Name is required.' title='Sheet Name is required.' />" 
                ToolTip="Sheet Name is required."></asp:RequiredFieldValidator> 
            </td>
          </tr>
          <%--Stats Season--%>
          <tr runat="server" id="trStatSeasonRow" class="alternatingRow">
            <td class="leftCol">
              Stats Season
            </td>
            <td>
              <asp:DropDownList runat="server" ID="ddlStatsSeason" DataTextField="SeasonCode" DataValueField="SeasonCode" OnDataBound="ddlStatsSeason_DataBound"></asp:DropDownList>
            </td>
          </tr>
          <%--Submit Button--%>
          <tr>
            <td colspan="2" class="cAlign">
              <asp:Button runat="server" ID="butSave" Text="Save Changes" OnClick="butSave_Click" />
            </td>
          </tr>
        </table>  
      </asp:Panel>


      <%--Sheet Players--%>
      <asp:Panel runat="server" ID="panSheetPlayers" CssClass="sheetPlayers">
        <table class="main">
          <tr>
            <th>Manage Drivers</th>
          </tr>
          <tr>
            <td>
              <cswr:SheetItemManager ID="spmSheetItemManager" runat="server" SheetType="CheatSheet" />
            </td>
          </tr>
        </table>
    
      </asp:Panel>  

    </asp:Panel>
      
  </div>

</asp:Content>

