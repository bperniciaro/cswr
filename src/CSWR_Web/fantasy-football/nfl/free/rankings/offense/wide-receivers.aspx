<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveTwoCol.master" Theme="Web20" AutoEventWireup="true" 
  CodeFile="wide-receivers.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.WideReceiverRankings" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage" 
  CanonicalUrl="https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/rankings/offense/wide-receivers.aspx"
%>
<%@ Register Src="~/usercontrols/sports/CSWRRankings.ascx" TagName="CSWRRanking" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <h1><asp:Literal runat="server" ID="litPageTitle" /></h1>

  <cswr:CSWRRanking runat="server" ID="cswrRanking" SportCode="FOO"  PositionCode="WR"/>

</asp:Content>

