<%@ Page Title="Cheat Sheet War Room - Account Locked" Language="C#" MasterPageFile="~/MasterPages/NoSport.master" AutoEventWireup="true" CodeFile="AccountLocked.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.AccountLocked"
  MetaDescription="This account has been locked because of too many failed login attempts."
  MetaRobotsText="NOINDEX,FOLLOW" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <h1>Account Locked</h1>

  <p>
    Your account is currently locked (probably because of too many failed login attempts).  To unlock your account
    please contact a system administrator
    <a href="mailto:admin@cheatsheetwarroom.com">via email</a>
    or
    <asp:HyperLink runat="server" NavigateUrl="~/contact.aspx">using our contact form</asp:HyperLink>.  Provide your username or
    email address and we'll unlock your account for you.
  </p>

</asp:Content>

