<%@ Page Language="C#" AutoEventWireup="true" Theme="" CodeFile="PrintSessionVariables.aspx.cs" Inherits="Test_PrintSessionVariables" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Print Session Variables</title>
</head>
<body>
<form id="form1" runat="server">

  <asp:Button runat="server" ID="butClearSession" onclick="butClearSession_Click" Text="Clear Session Variables" />
  <br />
  <br />
  <asp:GridView runat="server" ID="gvSession" AutoGenerateColumns="false" 
    onrowdatabound="gvSession_RowDataBound">
    <Columns>
      <%--Key--%>
      <asp:TemplateField>
        <ItemTemplate>
          <asp:Label runat="server" ID="labKey" />
        </ItemTemplate>
      </asp:TemplateField>
      <%--Value--%>
      <asp:TemplateField>
        <ItemTemplate>
          <asp:Label runat="server" ID="labValue" />
        </ItemTemplate>
      </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
      No Items in Session
    </EmptyDataTemplate>
  </asp:GridView>



</form>
</body>
</html>
