<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveTwoCol.master" AutoEventWireup="true" CodeFile="running-backs.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.RunningBackADPRankings" 
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
    of running backs for the <asp:Literal runat="server" ID="litSeasonCode" /> fantasy football season.
    <strong>Running Back ADP</strong> is calculated continuously by referencing the most recently updated running back cheat sheets.
  </p>

  <cswr:CSWRRanking runat="server" ID="cswrRanking" SportCode="FOO" PositionCode="RB" />

</asp:Content>

