<%@ Page Language="C#" MasterPageFile="~/MasterPages/NoSport.master" AutoEventWireup="true" CodeFile="AccessDenied.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.AccessDenied" Title="Cheat Sheet War Room - Access Denied" 
  MetaDescription="You do not have the rights to access this page."
  MetaRobotsText="NOINDEX,FOLLOW" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
%>
<%@ MasterType VirtualPath="~/MasterPages/Sport.master" %>  

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
  
  <div class="accessDeniedPage">
    <asp:Image runat="server" ID="imgLockImage" ImageUrl="~/Images/lock.gif" CssClass="floatLeft" />
  
    <%--Login Required--%>
    <asp:Label runat="server" ID="labLoginRequired">
      In order to access this page you must be a registered user.  If you already have an account, please login with your credentials; otherwise you can 
      <asp:HyperLink ID="HyperLink1" runat="server" Text="register" NavigateUrl="~/access/register.aspx" />
      now for free.
    </asp:Label>
  
    <%--Insufficient Permissions--%>
    <asp:Label runat="server" ID="labInsufficientPermissions">
      Sorry, the account you are logged-in with does not have permissions required to access this page.
    </asp:Label>
  
    <%--Invalid Credentials--%>
    <asp:Label runat="server" ID="labInvalidCredentials">
      The submitted credentials are not valid.  Please ensure they are correct and try again.
      If you forgot your password, <a href="../PasswordRecovery.aspx">click here</a> to recover it, or you can attempt to 
      <asp:HyperLink runat="server" NavigateUrl="~/access/login.aspx" Text="login" />
      again
    </asp:Label>
  
  </div>
  
</asp:Content>

