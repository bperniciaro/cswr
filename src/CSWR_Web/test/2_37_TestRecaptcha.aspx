<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Frame.master" AutoEventWireup="true" CodeFile="2_37_TestRecaptcha.aspx.cs" 
  Inherits="test_2_37_TestRecaptcha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

<asp:Label runat="server" ID="labSecret" />

<div id="dvCaptcha">
</div>

<asp:TextBox ID="txtCaptcha" runat="server" Style="display: none" />
<asp:RequiredFieldValidator ID = "rfvCaptcha" ErrorMessage="Captcha validation is required." ControlToValidate="txtCaptcha"
    runat="server" ForeColor = "Red" Display = "Dynamic" />
<br />
<br />
<asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btn_Click" />

<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
<script type="text/javascript" src="https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit" async defer></script>
<script type="text/javascript">
var onloadCallback = function () {
    grecaptcha.render('dvCaptcha', {
        'sitekey': '<%=ReCaptcha_Key %>',
        'callback': function (response) {
            $.ajax({
                type: "POST",
                url: "2_37_TestRecaptcha.aspx/VerifyCaptcha",
                data: "{response: '" + response + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var captchaResponse = jQuery.parseJSON(r.d);
                    if (captchaResponse.success) {
                        $("[id*=txtCaptcha]").val(captchaResponse.success);
                        $("[id*=rfvCaptcha]").hide();
                    } else {
                      alert("test");
                        $("[id*=txtCaptcha]").val("");
                        $("[id*=rfvCaptcha]").show();
                      var error = captchaResponse["error-codes"][0];
                      //alert("error: " + error);
                        $("[id*=rfvCaptcha]").html("RECaptcha error. " + error);
                    }
                }
            });
        }
    });
};
</script>

</asp:Content>

