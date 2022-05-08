<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GoogleAnalytics.ascx.cs" Inherits="BP.CheatSheetWarRoom.UI.UserControls.GoogleAnalytics" %>

<!-- Google Analytics tags -->
<script type="text/javascript">
  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-4698345-1']);
  _gaq.push(['_trackPageview']);

  (function () {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();
</script>
