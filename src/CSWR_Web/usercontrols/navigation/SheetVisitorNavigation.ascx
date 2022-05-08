<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SheetVisitorNavigation.ascx.cs" Inherits="BP.CheatSheetWarRoom.UI.UserControls.SheetVisitorNavigation" %>


<asp:Panel runat="server" ID="panVisitorButtons" class="sheetVisitorNavControl">


  <div class="alert alert-info" style="width:350px;margin:10px auto;">
    <asp:HyperLink runat="server" ID="hlVisitorRegister" NavigateUrl="~/access/register.aspx" CssClass="alert-link">Register for free</asp:HyperLink>
    so we can <em>save your sheets</em>.
  </div>

  <div>
    <%--Register--%>
    <asp:HyperLink runat="server" ID="hlRegister" NavigateUrl="~/access/register.aspx" CssClass="btn btn-default" ToolTip="By registering, we'll be able to remember your cheat sheets and re-load them each time you visit this site.">
      <i class="fa fa-sign-in"></i> Register
    </asp:HyperLink>
    <%--Sample--%>
    <asp:HyperLink runat="server" ID="hlSamplePrintableSheet" CssClass="btn btn-default" ToolTip="Click to access a sample, printable sheet to get an idea of what you'll be able to generate after you register.">
      <i class="fa fa-print"></i> Sample
    </asp:HyperLink>
    <%--Help--%>
    <asp:HyperLink runat="server" ID="hlHelp" CssClass="btn btn-default" NavigateUrl="~/fantasy-football/nfl/create/custom-sheet.aspx#cheatSheetHelp"> 
      <i class="fa fa-question"></i> Help
    </asp:HyperLink>
  </div>

</asp:Panel>


