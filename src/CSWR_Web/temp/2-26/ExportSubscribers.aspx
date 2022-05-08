<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportSubscribers.aspx.cs" Inherits="temp_2_26_DetermineUserDifferences" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <p>Determine the users that are in ASPP vs not in ASPP.  This not in ASPP will be added to a grid.</p>

    <asp:Button runat="server" ID="butExportUsers" OnClick="CustomSheet_ExportHandler" Text="Export Subscribers" />

    <br /><br />

    <strong>Total CSWR Users:</strong> <asp:Label runat="server" ID="labTotalUsers" /> <br />
 
    <strong>In ASPP:</strong> <asp:Label runat="server" ID="labFoundUsers" /> <br />
    <strong>Not in ASPP:</strong> <asp:Label runat="server" ID="labNotFoundUsers" />

    <p>Users only in CSWR along with their Email Preference</p>
    
    <%--<asp:Button runat="server" ID="butExportProblemUsers" OnClick="butExportProblemUsers_Click" Text="Export problematic users to CSV" />--%>
      
    <asp:GridView runat="server" ID="gvUserPreferences">
    </asp:GridView>

    </form>
</body>
</html>
