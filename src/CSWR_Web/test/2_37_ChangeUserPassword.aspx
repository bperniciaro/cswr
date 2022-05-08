<%@ Page Language="C#" AutoEventWireup="true" CodeFile="2_37_ChangeUserPassword.aspx.cs" Inherits="test_ChangeUserPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

          Email address:
          <asp:TextBox runat="server" ID="tbEmail"></asp:TextBox>

          <br />

          <asp:Button runat="server" ID="butSubmit" Text="Submit" />

          <br />

          New Password:
          <asp:Label runat="server" ID="labNewPassword" />
        </div>
    </form>
</body>
</html>
