<%@ Page Title="Archive User Sheets" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="archiveusersheets.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.ArchiveUserSheets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">


  <h1>Click to archive all user sheets to be graded</h1>

  <p>
    This page copies cheat sheets which meet the following criteria from the <strong>Sheets_CheatSheets</strong> table to an <em>archive</em> table 
    <strong>Sheets_ArchivedCheatSheets</strong> for later processing/grading.
  </p>

  <ul>
    <li>Football Sheet</li>
    <li>Member Sheet</li>
    <li>Standard Scoring</li>
    <li>Current Season</li>
    <li>Single Position Sheets</li>
    <li>For members /w multiple sheets /w same position, we take the most-recently updated sheet</li>
  </ul>

  Kickoff Date: 
      <asp:TextBox runat="server" ID="tbKickoffDate" Width="75"></asp:TextBox>
      <asp:RequiredFieldValidator runat="server" ControlToValidate="tbKickoffDate" Display="Dynamic"
                                         ErrorMessage="Date is required" />
  
  <br />
  <br />
  <asp:Button runat="server" ID="butMovePlayers" Text="Archive Cheat Sheets" onclick="butArchiveCheatSheets_Click" />
  <br />


  <%--This script takes a long time, so provide an update status--%>
  <br />

  <asp:Panel runat="server" ID="panResults" Visible="false">
    <%--Users--%>
    <strong><asp:Literal runat="server" ID="litSeasonCode" /> Users:</strong> <asp:Literal runat="server" ID="litUserCount" />
    <br />
    <%--Sheets--%>
    <strong>Standard Scoring Sheets Archived: </strong> <asp:Literal runat="server" ID="litSheetCount" />
    <br />
    <%--Errors--%>
    <strong>Errors: </strong> <asp:Literal runat="server" ID="litErrors" />
  </asp:Panel>

</asp:Content>

