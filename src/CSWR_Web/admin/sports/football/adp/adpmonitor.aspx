<%@ Page Title="ADP Monitor" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" 
  CodeFile="adpmonitor.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.ADPMonitor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

<h1>ADP Monitor</h1>

<asp:GridView runat="server" ID="gvTest" OnRowDataBound="gvTest_RowDataBound" 
              CellPadding="4" AutoGenerateColumns="false">
  <Columns>
    <asp:TemplateField HeaderText="Position">
      <ItemTemplate>
        <asp:HyperLink runat="server" ID="hlPosition" />
      </ItemTemplate>
    </asp:TemplateField>
    <asp:BoundField HeaderText="Total" DataField="TotalSheetsConsidered" />
    <asp:BoundField HeaderText="Last 72" DataField="Last72Sheets" />
    <asp:BoundField HeaderText="Last 48" DataField="Last48Sheets" />
    <asp:BoundField HeaderText="Last 24" DataField="Last24Sheets" />
    <asp:BoundField HeaderText="Timespan" DataField="TimespanInDays" />
    <asp:BoundField HeaderText="Calculated" DataField="CalcTimestamp" />
  </Columns>
</asp:GridView>

</asp:Content>

