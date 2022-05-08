<%@ Page Language="C#" Theme="Web20" AutoEventWireup="true" CodeFile="quarterbacks-cheat-sheet.aspx.cs" Inherits="fantasy_football_nfl_free_printable_offense_quarterback_cheat_sheet" %>
<%@ Register Src="~/usercontrols/Sports/CSWRFreePrintPositionalSheetTemplate.ascx" TagName="CSWRFreePrintPositionalSheetTemplate" TagPrefix="cswr"%>
<%@ Register Src="~/usercontrols/GoogleAnalytics.ascx" TagName="GoogleAnalytics" TagPrefix="cswr" %>

<!doctype html>
<html lang="en">
<head runat="server">
  <link href="https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/printable/offense/quarterbacks-cheat-sheet.aspx" rel="canonical" />
  <link rel="stylesheet" type="text/css" href="~/styles/print.css" media="print" />
  <!-- standard for HTML 5 document -->
  <meta charset="utf-8">  
  <!-- For Bing SEO -->
  <meta http-equiv="content-language" content="en-us"> 
  <!-- Google Publisher -->
  <link rel="publisher" href="https://plus.google.com/+Cheatsheetwarroom" />
  <!-- Shiv to make older ID browsers style HTML5 elements -->
  <!--[if IE]><script src="https://cdnjs.cloudflare.com/ajax/libs/html5shiv/3.7.3/html5shiv.js"></script><![endif]-->  
  <!-- Google Analytics tags -->
  <cswr:GoogleAnalytics runat="server"/>
  
  <!-- Schema.org mark-up for Google Plus -->
  <!-- Image size is 347x194 -->
  <meta runat="server" id="schemaOrgName" itemprop="name" content="Printable Quarterbacks Cheat Sheet"/>
  <meta runat="server" id="schemaOrgDescription" itemprop="description" content="A printable, single-page quarterbacks cheat sheet for your fantasy football draft" /> 
  <meta runat="server" id="schemaOrgImage" itemprop="image" content="https://www.cheatsheetwarroom.com/images/socialsharing/printable/printable-quarterbacks-cheat-sheet.jpg" /> 

  <!-- Twitter Card data -->
  <!-- Looks right in preview tool, but card is not loaded into Twitter production -->
  <meta name="twitter:card" content="summary">  <!-- static -->
  <meta name="twitter:site" content="@cswarroom">  <!-- static -->
  <meta name="twitter:creator" content="@bradperniciaro">  <!-- static -->
  <meta name="twitter:domain" content="https://www.cheatsheetwarroom.com">  <!-- static -->
  <meta name="twitter:title" runat="server"  id="twitterTitle" content="Printable Quarterbacks Cheat Sheet">  <!-- dynamic -->
  <meta name="twitter:description" runat="server" id="twitterDescription" content="A printable, single-page quarterbacks cheat sheet for your fantasy football draft">  <!-- dynamic -->
  <meta name="twitter:url" runat="server" id="twitterUrl" content="https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/printable/offense/quarterbacks-cheat-sheet.aspx">
  <!-- preview 60px x 60px, full size 375px x 375px -->
  <meta name="twitter:image" content="https://www.cheatsheetwarroom.com/images/socialsharing/printable/printable-quarterbacks-cheat-sheet.jpg">  <!-- dynamic -->

  <!-- Open Graph data -->
  <!-- using page title and meta description -->
  <meta property="og:type" content="article" />  <!-- static -->
  <meta property="og:site_name" content="Cheat Sheet War Room" />  <!-- static -->
  <meta property="fb:admins" content="1426393719" />  <!-- static -->
  <meta property="og:url" content="https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/printable/offense/quarterbacks-cheat-sheet.aspx" />
  <meta property="og:image" content="https://www.cheatsheetwarroom.com/images/socialsharing/printable/printable-quarterbacks-cheat-sheet.jpg"/>
  <meta property="og:title" content="Printable Quarterbacks Cheat Sheet"/>
  <meta property="og:description" content="A printable, single-page quarterbacks cheat sheet for your fantasy football draft"/>


</head>
<body style="background:White;">
<form id="form1" runat="server">

<cswr:CSWRFreePrintPositionalSheetTemplate runat="server" SportCode="FOO" PositionCode="QB" />

</form>
</body>
</html>
