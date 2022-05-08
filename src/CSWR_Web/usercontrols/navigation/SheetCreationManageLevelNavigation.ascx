<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SheetCreationManageLevelNavigation.ascx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.UserControls.SheetCreationManageLevelNavigation" %>

  <%--  <div class="container-fluid" style="background-color:orange;margin-right:80px;">
      <div class="thirdLevelNav">
      <div class="row">
        <div class="col-lg-12">--%>
          <div class="text-center">

    <asp:Panel runat="server" class="sheetCreationManageNavControl">
      <asp:HyperLink runat="server" ID="hlBack" CssClass="btn btn-default" ToolTip="Navigate back to your cheat sheet.">
        <i class="fa fa-arrow-circle-o-left"></i> Sheet</asp:HyperLink>
      <asp:HyperLink runat="server" ID="hlNewSheet" CssClass="btn btn-default" NavigateUrl="~/fantasy-football/nfl/create/newsheet.aspx" 
        ToolTip="Click to create a new fantasy football cheat sheet.">
        <i class="fa fa-plus"></i> New</asp:HyperLink>
      <asp:HyperLink runat="server" ID="hlManageSheets" CssClass="btn btn-default" NavigateUrl="~/fantasy-football/nfl/create/managesheets.aspx" 
        ToolTip="Click to create a new fantasy football cheat sheet.">
        <i class="fa fa-copy"></i> Sheets</asp:HyperLink>
      <asp:HyperLink runat="server" ID="hlOnePagePrint" CssClass="btn btn-default" NavigateUrl="~/fantasy-football/nfl/create/printable/single-position/configureprint.aspx" 
        ToolTip="Click to configure a single-page, printable fantasy football cheat sheet with all fantasy positions included.">
        <i class="fa fa-print"></i> 1-Page
      </asp:HyperLink>
      <asp:HyperLink runat="server" ID="hlHelp" CssClass="btn btn-default" NavigateUrl="~/fantasy-football/nfl/create/custom-sheet.aspx"> 
        <i class="fa fa-question"></i> Help
      </asp:HyperLink>
    </asp:Panel>


                      </div>