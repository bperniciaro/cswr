<%@ Page Title="Import Season Stats" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" 
  CodeFile="importweeklystats.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.ImportWeeklyStats" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

<h2>Import Weekly Stats</h2>

<cswr:MessageBox  runat="server" ID="mbSetDBFlag" MessageType="INSTRUCTIONS" />

<strong>Purpose:</strong> 
<p>
  Weekly stats are imported so we can use them as a reference for week-specific features (like fantasy points by  week).  Currently
  there are none.
</p>

<strong>Directions:</strong> 
<ol>
  <li>
    Before you import stats for a particular week, 
    <asp:HyperLink runat="server" NavigateUrl="~/admin/sports/football/players/stats/import/mapplayerids.aspx">map StatMapIDs</asp:HyperLink>  
    for that week so that all players will be in the database before the import.
  </li>
  <li>
    To import weekly stats 
    <a href="http://ffa.73rhs.com/download.htm" target="_blank">download the CSV zip file for the respective week</a>, 
    unzip, and add (for example) <em>13wk01co.csv</em> and <em>13wk01ct.csv</em>.
  </li>
</ol>
<p>
  <strong>Seasonals:</strong> This page deletes then loads weekly stats for the selected week. No seasonal stats are calculated or stored.   
</p>
<p>
  <strong>UnFounds:</strong> If a player is not found in the database (there is no player whose StatMapID matches that in the spreadsheet), then that player isn't
  processed.  You'll need to re-map StatMapIDs and then import again.
</p>

<strong>Processed Weeks:</strong> <asp:Label runat="server" ID="labImportedWeeks" />

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


<br /><br />

<asp:Button runat="server" ID="butImportOffenseWeeklyStats" Text="Import Offense Weekly Stats" onclick="butImportOffenseWeeklyStats_Click" />
<asp:Button runat="server" ID="butImportDefenseWeeklyStats" Text="Import Defense Weekly Stats" onclick="butImportDefenseWeeklyStats_Click" />

<%--Execute the import--%>
<%--Only allow the import if all players are mapped to their respective IDs--%>

<asp:Panel runat="server" ID="panStatOutput">

  <br />
  Players Processed: <asp:Label runat="server" ID="labPlayersProcessed" />
  <br />
  <%--Quarterbacks Not Found--%>
  <br />
  <asp:Label runat="server" ID="labUnfoundQBs" />

  <%--Running Backs Not Found--%>
  <br />
  <asp:Label runat="server" ID="labUnfoundRBs" />

  <%--Wide Receivers Not Found--%>
  <br />
  <asp:Label runat="server" ID="labUnfoundWRs" />

  <%--Tight Ends Not Found--%>
  <br />
  <asp:Label runat="server" ID="labUnfoundTEs" />

  <%--Kickers Not Found--%>
  <br />
  <asp:Label runat="server" ID="labUnfoundKs" />

  <%--Defenses Not Found--%>
  <br />
  <br />
  <asp:Label runat="server" ID="labUnfoundTeams" />
  <br /><br />

  <asp:Repeater runat="server" ID="repUnfoundPlayers" onitemdatabound="repUnfoundPlayers_ItemDataBound">
    <ItemTemplate>
      <asp:Label runat="server" ID="labPlayerName" />
      <asp:Label runat="server" ID="labPlayerPosition" />
      <asp:Label runat="server" ID="labStatMapID" /><br /><br />
    </ItemTemplate>
  </asp:Repeater>
</asp:Panel>





</asp:Content>

