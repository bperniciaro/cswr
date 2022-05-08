<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestTrafficPopSinglePage.aspx.cs" Inherits="test_TestTrafficPopSinglePage" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <link type="text/css" rel="stylesheet" href="../styles/local/fb-traffic-pop.css">
  <script type="text/javascript" src="../scripts/local/jquery-1.11.0.min.js"></script>
  <script type="text/javascript" src="http://connect.facebook.net/en_US/all.js#xfbml=1"></script>
  <script type="text/javascript" src="../scripts/local/fb-traffic-pop.js"></script>
  <script type="text/javascript">

	$(document).ready(function(){		
					
		$().facebookTrafficPop({
			timeout: 999,
			delay: 0,
      title: "Test Pop-up",
      // <img src="https://www.cheatsheetwarroom.com/Images/Sports/Football/articles/byeweeks/nfl-bye-weeks-2017.png"/>

      message: "&lt;div style='background-color:orange;padding-bottom:20px;' &gt; Share or Like this page, then click on the ring to see what happens next. &lt;/div&gt; &lt;center&gt;  &lt;img  src='https://www.cheatsheetwarroom.com/Images/addons/trafficpop/fantasyjocksring.jpg' /&gt; &lt;/center&gt; ",


      //message: 'There should be an image below here before<span></span>after <center><img src="https://www.cheatsheetwarroom.com/Images/Sports/Football/articles/byeweeks/nfl-bye-weeks-2017.png" border="0" style="margin:10px 0px;" /></center>.  After the center tag.',
      url: "https://www.cheatsheetwarroom.com/fantasy-football/nfl/create/custom-sheet.aspx",
      share_url: 'https://www.cheatsheetwarroom.com/fantasy-football/nfl/create/custom-sheet.aspx',
			closeable: true
		});	
		
	});

</script></head>
<body>
     <!-- REST OF PAGE CONTENT -->
    
    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis condimentum, nisl eget porta tempus, enim diam blandit sem, sed dignissim velit dolor ultricies lorem. Maecenas gravida lacinia semper. Quisque placerat leo id ante congue nec elementum ipsum euismod. Sed suscipit, dolor ut posuere pharetra, dolor massa dictum mauris, vel egestas arcu tellus vel massa. Mauris pellentesque placerat ornare. Nulla sed nisl eros. Vivamus malesuada adipiscing magna vel mollis. Quisque blandit tempus augue, quis euismod ligula ornare eget. Maecenas id dui est, sed rhoncus orci. Vivamus sed nibh metus. Vivamus scelerisque libero vel augue pretium eget ullamcorper magna vulputate. Nam lorem odio, interdum nec congue non, posuere sit amet dui. Mauris at mollis enim. Maecenas erat nisl, varius in placerat eu, elementum in odio. Nam quis tortor ac nunc venenatis mattis. Praesent quis rutrum leo.</p>
   
    
</body>
</html>
