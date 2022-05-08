<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoopCount.aspx.cs" Inherits="temp_2_40_LoopCount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   
      Loop Count:
      <asp:TextBox runat="server" ID="tbLoopCount" Text="100"></asp:TextBox>
      <asp:Button runat="server" ID="butStart" Text="Start" OnClick="butStart_Click" />
      <br />
      Loops: 
      <asp:Label runat="server" ID="labLoops"></asp:Label>
    </form>
</body>
</html>
