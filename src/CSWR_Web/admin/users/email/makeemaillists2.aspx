<%@ Page Language="C#" AutoEventWireup="true" Theme="" CodeFile="makeemaillists2.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.Admin.MakeEmailLists2" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
  <title>Create Email Lists for Newlestter</title>
</head>
<body>
    <form id="form1" runat="server">


    
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
    </form>
</body>
</html>
