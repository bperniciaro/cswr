<%@ Page Title="Import Season Stats" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="importseasonstats.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.ImportSeasonStats" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

<h2>Import Seasonal Stats</h2>

<cswr:MessageBox runat="server" ID="mbResult" WidthPercentage="20" />

<strong>Purpose:</strong> 
<p>
  Seasonal stats are used to sort all in-season and pre-season cheat sheets.  We import an updated version of seasonal (to-date)
  stats every week and the previous YTD stats are over-written.
</p>

<p>
  To import season stats 
  <a href="http://ffa.73rhs.com/download.htm" target="_blank">download the CSV zip file for the respective week</a>, 
  unzip, and add (for example) <em>12wk17coytd</em> and <em>12wk17ctytd</em>.
</p>

<p>
  Will process current season stats unless you override by specifying a year here:
  <asp:TextBox runat="server" ID="tbSeasonStats" Width="100" />
</p>

<strong>Processed (weekly):</strong> <asp:Label runat="server" ID="labImportedWeeks" />

<%--Allow the user to select a week to import--%>
<br /><br />
Week: 
<asp:DropDownList runat="server" ID="ddlWeek">
  <asp:ListItem Text="1" Value="1" />
  <asp:ListItem Text="2" Value="2" />
  <asp:ListItem Text="3" Value="3" />
  <asp:ListItem Text="4" Value="4" />
  <asp:ListItem Text="5" Value="5" />
  <asp:ListItem Text="6" Value="6" />
  <asp:ListItem Text="7" Value="7" />
  <asp:ListItem Text="8" Value="8" />
  <asp:ListItem Text="9" Value="9" />
  <asp:ListItem Text="10" Value="10" />
  <asp:ListItem Text="11" Value="11" />
  <asp:ListItem Text="12" Value="12" />
  <asp:ListItem Text="13" Value="13" />
  <asp:ListItem Text="14" Value="14" />
  <asp:ListItem Text="15" Value="15" />
  <asp:ListItem Text="16" Value="16" />
  <asp:ListItem Text="17" Value="17" />
</asp:DropDownList>

<asp:Button runat="server" ID="butImportStats" Text="Import Stats & Calculate Rankings" onclick="butImportStats_Click" />
<br /><br />

Unfound QBs: <asp:Label runat="server" ID="labUnfoundQBs" />
<br />
Unfound RBs: <asp:Label runat="server" ID="labUnfoundRBs" />
<br />
Unfound WRs: <asp:Label runat="server" ID="labUnfoundWRs" />
<br />
Unfound TEs: <asp:Label runat="server" ID="labUnfoundTEs" />
<br />
Unfound Ks: <asp:Label runat="server" ID="labUnfoundKs" />
<br />
Unfound Teams: <asp:Label runat="server" ID="labUnfoundTeams" />
<br /><br />

<asp:Repeater runat="server" ID="repUnfoundPlayers" onitemdatabound="repUnfoundPlayers_ItemDataBound">
  <ItemTemplate>
    <asp:Label runat="server" ID="labPlayerName" />
    <asp:Label runat="server" ID="labPlayerPosition" />
    <asp:Label runat="server" ID="labStatMapID" /><br /><br />
  </ItemTemplate>
</asp:Repeater>




</asp:Content>

