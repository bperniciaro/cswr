<%@ Page Language="C#" MasterPageFile="~/MasterPages/NoSport.master" Theme="Web20" AutoEventWireup="true" CodeFile="sitemap.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.Sitemap" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage" 
  Title="Navigation links to all of our pages"
  MetaDescription="Use our sitemap to quickly navigate to our various pages or learn more about the content we have to offer."
  CanonicalUrl="http://www.cheatsheetwarroom.com/sitemap.aspx"
%>


<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

<div class="siteMapPage">
  <h1>Cheat Sheet War Room Sitemap</h1>
  <asp:SiteMapDataSource runat="server" ID="smdsSiteMap"/>
  <asp:TreeView runat="server" ID="tvSiteMapTree" DataSourceID="smdsSiteMap" OnTreeNodeDataBound="tvSiteMapTree_TreeNodeDataBound"/>
</div>


</asp:Content>

