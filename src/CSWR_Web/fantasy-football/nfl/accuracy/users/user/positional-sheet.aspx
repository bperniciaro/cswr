<%@ Page Title="User Quarterbacks Sheet Accuracy" Language="C#" MasterPageFile="~/MasterPages/Sport.master" 
    AutoEventWireup="true" CodeFile="positional-sheet.aspx.cs"  CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
  Inherits="NFL.Accuracy.Users.User.UserQuarterbacksCheatSheet" EnableEventValidation="false" 
MetaRobotsText="NOINDEX,FOLLOW"
CanonicalUrl="https://www.cheatsheetwarroom.com/fantasy-football/nfl/accuracy/users/user/positional-sheet.aspx"
%>


<%@ Register Src="~/usercontrols/sports/football/accuracy/FOOUserGradedSheet.ascx" TagName="FOOUserGradedSheet" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <cswr:FOOUserGradedSheet runat="server" ID="fooGradedSheet"/>


</asp:Content>

