<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TagTest.aspx.cs" Inherits="test_SocialTags_TagTest" %>
<!DOCTYPE html>
<!-- Update your html tag to include the itemscope and itemtype attributes. -->
<html itemscope itemtype="http://schema.org/Article">
<head runat="server">
  <title>Page title</title>
  <!-- standard meta tags -->
  <meta name="description" content="Comprehensive nfl player rankings for the top players in the 2014 fantasy football season." />
  
  <!-- Schema.org mark-up for Google Plus -->
  <!-- Image size is 347x194 -->
  <meta itemprop="name" content="NFL Player Rankings" />  <!-- verified -->
  <meta itemprop="description" content="Comprehensive NFL player rankings of the top players for the 2014 fantasy football season." />  <!-- verified -->
  <meta itemprop="image" content="https://www.cheatsheetwarroom.com/images/socialsharing/nfl-player-rankings.gif" />  <!-- verified -->

  <!-- Twitter Card data -->
  <!-- Looks right in preview tool, but card is not loaded into Twitter production -->
  <meta name="twitter:card" content="summary">  <!-- verified -->
  <meta name="twitter:site" content="@cswarroom">  <!-- verified -->
  <meta name="twitter:creator" content="@bradperniciaro">  <!-- verified -->
  <meta name="twitter:domain" content="https://www.cheatsheetwarroom.com">  <!-- verified -->
  <meta name="twitter:title" content="NFL Player Rankings - Twitter Title">  <!-- verified -->
  <meta name="twitter:description" content="Twitter page description">  <!-- verified -->
  <!-- preview 60px x 60px, full size 375px x 375px -->
  <meta name="twitter:image" content="https://www.cheatsheetwarroom.com/images/socialsharing/nfl-player-rankings.gif"> 

  <!-- Open Graph data -->
  <!-- using page title and meta description -->
  <meta property="og:type" content="article" />
  <meta property="og:url" content="https://www.cheatsheetwarroom.com/test/socialtags/tagtest.aspx" />
  <meta property="og:image" content="https://www.cheatsheetwarroom.com/images/socialsharing/nfl-player-rankings.gif" />


  <meta property="og:title" content="Facebook Title" />
  <meta property="og:description" content="Facebook description" />
  <meta property="og:site_name" content="Cheat Sheet War Room" />
  <meta property="fb:admins" content="1426393719" /> 

</head>
<body>
<form id="form1" runat="server">


  <p>
    This is the test content on an actual page.

  </p>


</form>
</body>
</html>
