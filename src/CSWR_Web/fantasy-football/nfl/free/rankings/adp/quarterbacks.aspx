<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveTwoCol.master" AutoEventWireup="true" CodeFile="quarterbacks.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.QuarterbackADPRankings" 
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
    of quarterbacks for the <asp:Literal runat="server" ID="litSeasonCode" /> fantasy football season.
    <strong>Quarterback ADP</strong> is calculated continuously by referencing the most recently updated quarterback cheat sheets.
  </p>

  <cswr:CSWRRanking runat="server" ID="cswrRanking" SportCode="FOO" PositionCode="QB" />


</asp:Content>

