<%@ Page Language="C#" MasterPageFile="~/MasterPages/Sport.master" AutoEventWireup="true" CodeFile="driver-adp-rankings.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.NascarDriverADPRankings" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
  CanonicalUrl="https://www.cheatsheetwarroom.com/fantasy-racing/nascar/free/rankings/driver-adp-rankings.aspx"
%>
<%@ Register Src="~/usercontrols/sports/CSWRRankings.ascx" TagName="CSWRRanking" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <h1><asp:Literal runat="server" ID="litPageTitle" /></h1>

  <asp:Panel runat="server" ID="panADPIntro" style="padding:15px 0px 20px 0px;">
    <p>
      These rankings represent the 
      
      <asp:Label runat="server" ID="labADPTimeframe" />
      
      <strong>average draft position</strong>
      (<abbr title="Average Draft Position">ADP</abbr>) 
      of NASCAR drivers for the 

      <asp:Label runat="server" ID="labCurrentSeason" /> fantasy racing season.
      
      <asp:Label runat="server" ID="labClosingStatement" />
    </p>
  </asp:Panel>

  <cswr:CSWRRanking runat="server" ID="cswrRanking" SportCode="RAC" PositionCode="DR" />

</asp:Content>

