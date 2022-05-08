<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveTwoCol.master" AutoEventWireup="true" CodeFile="defenses.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.DefenseADPRankings" 
%>
<%@ MasterType VirtualPath="~/MasterPages/Sport.master" %>
<%@ Register Src="~/usercontrols/sports/CSWRRankings.ascx" TagName="CSWRRanking" TagPrefix="cswr" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <h1><asp:Literal runat="server" ID="litPageTitle" /></h1>

  <p style="padding:15px 0px 15px 0px;">
    These rankings represent the 
    <asp:Label runat="server" ID="labADPTimeframe"></asp:Label>
    <em>average draft position</em>
    (<abbr title="Average Draft Position">ADP</abbr>) 
    of defensive teams for the <asp:Literal runat="server" ID="litSeasonCode" /> fantasy football season.
    <strong>Defense ADP</strong> is calculated continuously by referencing the most recently updated defense cheat sheets.
  </p>

  <cswr:CSWRRanking runat="server" ID="cswrRanking" SportCode="FOO" PositionCode="DF" />


</asp:Content>

