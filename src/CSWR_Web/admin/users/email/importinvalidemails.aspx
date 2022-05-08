<%@ Page Title="Import Invalid Emails" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" 
  CodeFile="importinvalidemails.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.Admin.Users.ImportInvalidEmails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

<div class="instructions">
  Import a .csv to designate users have an invalid email address.
</div>

<br /><br />

Upload list:
<asp:FileUpload ID="filUpload" runat="server" />&nbsp;
<asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" CausesValidation="false" /><br />


<asp:Label ID="lblFeedbackOK" SkinID="FeedbackOK" runat="server"></asp:Label>
<asp:Label ID="lblFeedbackKO" SkinID="FeedbackKO" runat="server"></asp:Label>
<br /><br /><br />

</asp:Content>

