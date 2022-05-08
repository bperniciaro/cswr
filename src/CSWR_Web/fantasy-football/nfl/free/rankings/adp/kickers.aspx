<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveTwoCol.master" AutoEventWireup="true" CodeFile="kickers.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.KickerADPRankings" 
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
    of kickers for the <asp:Literal runat="server" ID="litSeasonCode" /> fantasy football season.
    <strong>Kicker ADP</strong> is calculated continuously by referencing the most recently updated kicker cheat sheets.
  </p>

  <cswr:CSWRRanking runat="server" ID="cswrRanking" SportCode="FOO" PositionCode="K" />

</asp:Content>

