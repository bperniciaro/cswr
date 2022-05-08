<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="gradeusersheets.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.GradeUserSheets" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">


  <cswr:MessageBox runat="server" ID="mbStatus" />

  <h1>Grade Archived User Cheat Sheets</h1>

  Grade Sheets from Year <asp:TextBox runat="server" ID="tbSheetYear" />
  <br/>
  <asp:CheckBox runat="server" ID="cbLimitGradedSheets" Text="Limit Graded Sheets" Checked="True"/><asp:TextBox runat="server" ID="tbSheetLimit" Text="200" Width="75"></asp:TextBox>
  
  <br/><br/>
  <asp:Button runat="server" ID="butGrade" Text="Grade Sheets" OnClick="butGrade_Click" />


</asp:Content>

