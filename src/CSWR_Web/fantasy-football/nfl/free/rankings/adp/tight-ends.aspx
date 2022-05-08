<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveTwoCol.master" AutoEventWireup="true" CodeFile="tight-ends.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.TightEndADPRankings" 
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
    of tight ends for the <asp:Literal runat="server" ID="litSeasonCode" /> fantasy football season.
    <strong>Tight End ADP</strong> is calculated continuously by referencing the most recently updated tight end cheat sheets.
  </p>

  <cswr:CSWRRanking runat="server" ID="cswrRanking" SportCode="FOO" PositionCode="TE" />

</asp:Content>

