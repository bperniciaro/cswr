<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveTwoCol.master" Theme="Web20" AutoEventWireup="true" 
  CodeFile="editsheet.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.EditSheet" MetaRobotsText="NOINDEX,FOLLOW" 
  Title="Edit Fantasy Football Cheat Sheet" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage" %>
<%@ MasterType VirtualPath="~/MasterPages/ResponsiveTwoCol.master" %>
<%@ Register Src="~/usercontrols/navigation/SheetCreationManageLevelNavigation.ascx" TagName="SheetCreationManageLevelNavigation" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/Sports/SheetItemManager2.ascx" TagName="SheetItemManager2" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <asp:ScriptManager runat="server" />

  <%--Navigation--%>
  <cswr:SheetCreationManageLevelNavigation runat="server" ID="scmlnNavigation" CurrentStage="EDITSHEET" SportCode="FOO" />

  <%--Message Box--%>
  <cswr:MessageBox runat="server" ID="mbStatus" WidthPercentage="50" />
 
  <div class="editSheetPage">

    <%--Sheet Settings--%>

    <h2>Sheet Settings</h2>

    <%--<div class="container-fluid">--%>
      <div class="row">

        <div class="form-horizontal">

          <cswr:MessageBox runat="server" ID="mbSheetSettingsMessage" />

          <%--Sheet Name--%>
          <div class="form-group">
            <label class="control-label col-sm-4">Sheet Name</label>
            <div class="col-sm-8">
              <asp:TextBox runat="server" ID="tbSheetName" MaxLength="50" CssClass="form-control"></asp:TextBox>
                  <asp:RequiredFieldValidator runat="server" ID="rfvNameRequired" ControlToValidate="tbSheetName" Display="Dynamic" SetFocusOnError="true"
                    ErrorMessage="<img src='../../Images/error.gif' alt='Sheet Name is required.' title='Sheet Name is required.' />" 
                    ToolTip="Sheet Name is required."></asp:RequiredFieldValidator> 
            </div>
          </div>

          <%--Draft Configuration--%>
          <div class="form-group">
            <label class="control-label col-sm-4">Draft Style</label>
            <div class="col-sm-8">
              <asp:RadioButton runat="server" ID="rbSerpentine" GroupName="rbDraftConfiguration" Text="Serpentine" Checked="true"/>
              <asp:RadioButton runat="server" ID="rbAuction" GroupName="rbDraftConfiguration" Text="Auction" />
            </div>
          </div>

          <%--Position--%>
          <div class="form-group">
            <label class="control-label col-sm-4">Position</label>
            <div class="col-sm-8">
              <asp:Label runat="server" ID="labPositions"></asp:Label>
            </div>
          </div>

          <%--Stat Season--%>
          <div class="form-group">
            <label class="control-label col-sm-4">Stats Season</label>
            <div class="col-sm-8">
              <asp:Label runat="server" ID="labStatsSeason" />
            </div>
          </div>

          <%--Scoring Configuration--%>
          <div class="form-group">
            <label class="control-label col-sm-4">Scoring Configuration</label>
            <div class="col-sm-8">
               <asp:Label runat="server" ID="labScoringConfiguration" />
            </div>
          </div>

          <div class="form-group">
            <div class="col-sm-offset-4 col-sm-8">
              <asp:Button runat="server" ID="butSave" Text="Save Changes" OnClick="butSave_Click" CssClass="btn btn-primary" />
            </div>
          </div>

        </div>

      </div>
   <%-- </div>--%>


    <h2>Sheet Players</h2>

    <cswr:SheetItemManager2 ID="spmSheetItemManager" runat="server" SheetType="CheatSheet" SportCode="FOO" />

  </div>


</asp:Content>

