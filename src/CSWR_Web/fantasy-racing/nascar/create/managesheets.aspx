<%@ Page Title="Manage Fantasy Racing Cheat Sheets" Language="C#" MasterPageFile="~/MasterPages/Sport.master" AutoEventWireup="true" 
  CodeFile="managesheets.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.ManageSheets" MetaRobotsText="NOINDEX,FOLLOW"
  CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage" %>
<%@ MasterType VirtualPath="~/MasterPages/Sport.master" %>
<%@ Register Src="~/usercontrols/navigation/SheetCreationManageLevelNavigation.ascx" TagName="SheetCreationManageLevelNavigation" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <%--Navigation--%>
  <cswr:SheetCreationManageLevelNavigation runat="server" ID="scmlnNavigation" CurrentStage="MANAGESHEETS" SportCode="RAC" />

<div class="manageSheetsPage">

  <%--Instuctions--%> 
  <cswr:MessageBox runat="server" ID="mbInstructions" MessageType="INSTRUCTIONS" WidthPercentage="70" />
  <%--No Sheets--%> 
  <cswr:MessageBox runat="server" ID="mbNoSheets" MessageType="INSTRUCTIONS" WidthPercentage="60" />
  <br />


  <%--All Sheets--%>
  <asp:GridView runat="server" ID="gvCheatSheets" AutoGenerateColumns="False" 
    PageSize="20" SkinID="Racing" CssClass="racSheetManagerControl"
    OnRowDataBound="gvCheatSheets_RowDataBound" 
    OnRowCreated="gvCheatSheets_RowCreated" DataKeyNames="CheatSheetID" Width="80%" 
    HorizontalAlign="center" onrowdeleting="gvCheatSheets_RowDeleting">
    <Columns>
      <%--Season--%>
      <%--<asp:BoundField DataField="SeasonCode" HeaderText="Season" SortExpression="SeasonCode"/>--%>
      <%--Sheet Name--%>
      <asp:BoundField DataField="SheetName" HeaderText="Sheet Name" SortExpression="SheetName" />
      <%--Item Count--%>
      <asp:TemplateField HeaderText="Drivers">
        <ItemTemplate>
          <asp:Label runat="server" ID="labItemCount" />
        </ItemTemplate>
      </asp:TemplateField>
      <%--Updated Timestamp--%>
      <asp:BoundField DataField="LastUpdated" HeaderText="Updated" SortExpression="LastUpdated" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
      <%--Edit--%>
      <asp:TemplateField HeaderStyle-Width="15">
        <ItemTemplate>
          <asp:HyperLink runat="server" ID="hlEditSheet" ToolTip="Click to edit the properties of this sheet.">
            <asp:Image runat="server" ImageUrl="~/Images/GridViewButtons/Edit.gif" />
          </asp:HyperLink>
        </ItemTemplate>
      </asp:TemplateField>

      <%--Go to Sheet--%>
      <asp:TemplateField HeaderStyle-Width="15">
        <ItemTemplate>
          <asp:HyperLink runat="server" ID="hlGoToSheet" ToolTip="Click to open this sheet.">
            <asp:Image runat="server" ImageUrl="~/Images/GridViewButtons/Sheet.gif" />
          </asp:HyperLink>
        </ItemTemplate>
      </asp:TemplateField>
      <%--Delete--%>
      <asp:CommandField ButtonType="Image"  HeaderStyle-Width="15" DeleteText="Click to delete this sheet." DeleteImageUrl="~/Images/GridViewButtons/Delete.gif" ShowDeleteButton="True" />
    </Columns>
  </asp:GridView>

</div>

</asp:Content>

