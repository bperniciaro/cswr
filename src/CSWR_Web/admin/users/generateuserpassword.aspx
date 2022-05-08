<%@ Page Language="C#" AutoEventWireup="true" CodeFile="generateuserpassword.aspx.cs" Inherits="admin_users_generateuserpassword" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">


          Email address:
          <asp:TextBox runat="server" ID="tbEmail"></asp:TextBox>

          <br />

          <asp:Button runat="server" ID="butSubmit" Text="Submit" />

          <br />

          New Password: <asp:Label runat="server" ID="labNewPassword" />
          <br />
          Operation Result: <asp:Label runat="server" ID="labOperationResult" />

    </form>
</body>
</html>
