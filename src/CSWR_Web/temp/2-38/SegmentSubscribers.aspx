<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SegmentSubscribers.aspx.cs" Inherits="temp_2_26_DetermineUserDifferences" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <h2>Determine the users that have engaged in the last year and those who haven't.</h2>

    <p><strong>Total CSWR Users:</strong> <asp:Label runat="server" ID="labTotalUsers" /></p> 
    <p><strong>Total CSWR Subscribers:</strong> <asp:Label runat="server" ID="labSubscribers" /></p> 

    <br />
    <p><strong>Recent Subscribers: </strong><asp:Label runat="server" ID="labRecentSubscribers" /></p>
    <p><strong>Old Subscribers: </strong><asp:Label runat="server" ID="labOldSubscribers" /></p>

    <br />
    <p><strong>Subscribed Rankers: </strong><asp:Label runat="server" ID="labSubscribedRankers" /></p>
    <p><strong>(New Import) Recent Subscribers Combined /w Rankers: </strong><asp:Label runat="server" ID="labRecentSubscribersAndRankers" /></p>
    <br />

    <p><strong>(New Opt-in List) InactiveSubscribers: </strong><asp:Label runat="server" ID="labInactiveSubscribers" /></p>


    <asp:Button runat="server" ID="butExportEngagers" OnClick="CustomSheet_ExportEngagers" Text="Export Engagers" />

    <br /><br />

    <p>Non-Engagers are subscribers that have not created an account or modified a sheet since 7/1/18.</p>

    <asp:Button runat="server" ID="butExportNonEngagers" OnClick="CustomSheet_ExportNonEngagers" Text="Export Opt-Ins" />


 


    </form>
</body>
</html>
