<%@ Page Language="C#" MasterPageFile="~/MasterPages/Sport.master" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage" 
  AutoEventWireup="true" CodeFile="drivers.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.NascarDriverRankings" 
  CanonicalUrl="https://www.cheatsheetwarroom.com/fantasy-racing/nascar/free/rankings/drivers.aspx"
%>
<%@ Register Src="~/usercontrols/sports/CSWRRankings.ascx" TagName="CSWRRanking" TagPrefix="cswr" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <h1><asp:Literal runat="server" ID="litPageTitle" /></h1><br />

  <cswr:CSWRRanking runat="server" ID="cswrRanking" SportCode="RAC" PositionCode="DR" />

</asp:Content>

