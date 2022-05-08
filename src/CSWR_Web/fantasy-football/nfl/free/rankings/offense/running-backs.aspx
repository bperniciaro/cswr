<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveTwoCol.master" Theme="Web20" AutoEventWireup="true" 
  CodeFile="running-backs.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.RunningBackRankings" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage" 
  CanonicalUrl="https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/rankings/offense/running-backs.aspx"
%>
<%@ Register Src="~/usercontrols/sports/CSWRRankings.ascx" TagName="CSWRRankings" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <h1><asp:Literal runat="server" ID="litPageTitle" /></h1>

  <cswr:CSWRRankings runat="server" ID="cswrRanking" SportCode="FOO" PositionCode="RB"/>

</asp:Content>

