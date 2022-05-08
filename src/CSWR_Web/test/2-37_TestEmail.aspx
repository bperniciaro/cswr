<%@ Page Language="C#" AutoEventWireup="true" CodeFile="2-37_TestEmail.aspx.cs" Inherits="test_TestEmail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
      <asp:TextBox runat="server" ID="tbHost" Text="smtp.gmail.com"></asp:TextBox>
      <br />
      <asp:TextBox runat="server" ID="tbPort" Text="587"></asp:TextBox>
      <br />
      <asp:Button runat="server" ID="butSendEmail" Text="Send Email" OnClick="butSendEmail_Click" />
      <br />
      Result: <asp:Label id="lblMessage" runat="server"></asp:Label> 
    </form>
</body>
</html>
