<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="determinebusts.aspx.cs" Inherits="admin_sports_football_determinebusts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

<h1>Verified 2013 Busts</h1>

<h2>Quarterbacks</h2>
<asp:GridView runat="server" ID="gvQBBusts" />

<h2>Running Backs</h2>
<asp:GridView runat="server" ID="gvRBBusts" />

<h2>Wide Receivers</h2>
<asp:GridView runat="server" ID="gvWRBusts" />

<h2>Tight Ends</h2>
<asp:GridView runat="server" ID="gvTEBusts" />

<h2>Kickers</h2>
<asp:GridView runat="server" ID="gvKBusts" />

<h2>Defenses</h2>
<asp:GridView runat="server" ID="gvDFBusts" />


</asp:Content>

