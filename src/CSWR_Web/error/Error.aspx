<%@ Page Language="C#" MasterPageFile="~/MasterPages/NoSport.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.Error" Title="Unexpected Error - Cheat Sheet War Room" 
  MetaDescription="An unexpected error has occured.  This event has been logged."
  MetaRobotsText="NOINDEX,FOLLOW" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
%>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>
<%@ MasterType VirtualPath="~/MasterPages/Sport.master" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <cswr:MessageBox runat="server" ID="mbErrorMessage" WidthPercentage="70" />
  
  If you would like to report this problem yourself, please use the 
	<asp:HyperLink runat="server" ID="lnkContact" Text="Contact" NavigateUrl="~/Contact.aspx?Type=Error" /> 
	page.


</asp:Content>

