<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BellaTest.aspx.cs" Inherits="temp_BellaTest" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
        <p>Are you a girl or a boy or both?</p>

        <asp:DropDownList runat="server" ID="ddlGender">
            <asp:ListItem Text="Boy" Value="Boy" />
            <asp:ListItem Text="Girl" Value="Girl" />
            <asp:ListItem Text="Both" Value="Both" />
        </asp:DropDownList>

        <asp:Button runat="server" ID="butDone" Text="I'm Done!" />

    </form>
</body>
</html>
