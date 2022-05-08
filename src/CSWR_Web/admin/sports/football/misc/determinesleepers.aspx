<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="determinesleepers.aspx.cs" Inherits="admin_sports_football_determinesleepers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

<h1>Verified 2013 Sleepers</h1>

<p>
  This page compares the total fantasy point output of each player to their pre-season ADP.  
</p>

<h2>Quarterbacks</h2>
<asp:GridView runat="server" ID="gvQBSleepers" />

<h2>Running Backs</h2>
<asp:GridView runat="server" ID="gvRBSleepers" />

<h2>Wide Receivers</h2>
<asp:GridView runat="server" ID="gvWRSleepers" />

<h2>Tight Ends</h2>
<asp:GridView runat="server" ID="gvTESleepers" />

<h2>Kickers</h2>
<asp:GridView runat="server" ID="gvKSleepers" />

<h2>Defenses</h2>
<asp:GridView runat="server" ID="gvDFSleepers" />


</asp:Content>

