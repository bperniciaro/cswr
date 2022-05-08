<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayPalButton.aspx.cs" Inherits="test_PayPalButton" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
  <title></title>
</head>
<body>

<form action="https://www.paypal.com/cgi-bin/webscr" method="post" target="_top">
  <!-- custom variable representing -->

  <input runat="server" type="hidden" id="myHiddenField" />
  <input type="hidden" name="cmd" value="_s-xclick">
  <input type="hidden" name="hosted_button_id" value="QHC2HSKZ72RFA">
  <input type="image" src="https://www.paypalobjects.com/en_US/i/btn/btn_buynowCC_LG.gif" border="0" name="submit" alt="PayPal - The safer, easier way to pay online!">
  <img alt="" border="0" src="https://www.paypalobjects.com/en_US/i/scr/pixel.gif" width="1" height="1">
</form>

</body>
</html>
