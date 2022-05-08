<%@ Page Title="Make Email Lists" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" 
  CodeFile="makeemaillists.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.Admin.MakeEmailLists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

<h1>Make Email Lists for Newsletters</h1>

    <asp:Button runat="server" ID="butCreateEmailLists" Text="Create Email Lists" 
      onclick="butCreateEmailLists_Click" />
    
    <div>

    Group 1:<asp:TextBox runat="server" TextMode="MultiLine" Width="300" Height="300" ID="tbGroup1"></asp:TextBox>
    <br />
    Group 2:<asp:TextBox runat="server" TextMode="MultiLine" Width="300" Height="300" ID="tbGroup2"></asp:TextBox>
    <br />
    Group 3:<asp:TextBox runat="server" TextMode="MultiLine" Width="300" Height="300" ID="tbGroup3"></asp:TextBox>
    <br />
    Group 4:<asp:TextBox runat="server" TextMode="MultiLine" Width="300" Height="300" ID="tbGroup4"></asp:TextBox>
    <br />
    Group 5:<asp:TextBox runat="server" TextMode="MultiLine" Width="300" Height="300" ID="tbGroup5"></asp:TextBox>
    <br />
    
    </div>

    <asp:Label runat="server" ID="labSubscribers" />
</asp:Content>

