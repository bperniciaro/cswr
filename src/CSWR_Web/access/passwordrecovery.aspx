<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveOneCol.master" Theme="Web20" AutoEventWireup="true" CodeFile="passwordrecovery.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.PasswordRecovery" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
  Title="Recover your Cheat Sheet War Room Password" 
  MetaDescription="Use this page to recover your password if you've forgotten it.  You must answer your security question"
  CanonicalUrl="https://www.cheatsheetwarroom.com/access/passwordrecovery.aspx"
  MetaRobotsText="NOINDEX,FOLLOW"
  %>
<%@ MasterType VirtualPath="~/MasterPages/Sport.master" %>  

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">

  <div class="passwordRecoveryPage">

  <h1>Recover Your Password</h1>
  
  <p class="instructions">If you forgot your password, you can use this page to have it sent to the email address you provided upon registration.</p>

  <div class="controlsContainer">

    <asp:PasswordRecovery ID="prRecoverPassword" runat="server">
    <UserNameTemplate>
      <p>Step 1: Enter your username</p>
      <table>
        <tr>
          <td>Username:</td>
          <td>
            <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
          </td>
          <td>
            <asp:RequiredFieldValidator runat="server" ID="rfvUserNameRequired" ControlToValidate="UserName" Display="dynamic" SetFocusOnError="True"
              ErrorMessage="Username is required." ForeColor="Red" ValidationGroup="prRecoverPassword">Username is Required</asp:RequiredFieldValidator>
          </td>
        </tr>
        <tr>
          <td colspan="2">
            <br />
             <div id="dvCaptcha"></div>
            <asp:TextBox ID="txtCaptcha" runat="server" Style="display: none" />
            <asp:RequiredFieldValidator ID = "rfvCaptcha" ErrorMessage="Captcha validation is required." ControlToValidate="txtCaptcha"
                runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="prRecoverPassword"/>
            <br />
          </td>
        </tr>
        <tr>
          <td colspan="2">
            <asp:Label runat="server" id="labFailureText"></asp:Label>
            <asp:Button runat="server" ID="SubmitButton" CommandName="Submit" Text="Submit" 
              ValidationGroup="prRecoverPassword" onclick="SubmitButton_Click" />
          </td>
        </tr>
      </table>
    </UserNameTemplate>
    <SuccessTemplate>
      <asp:Label runat="server" ID="lblSuccess" Text="Your password has been sent to you."></asp:Label>
    </SuccessTemplate>
    <MailDefinition 
      BodyFileName="~/TextFiles/PasswordRecoveryMail.txt" 
      From="accounts@cheatsheetwarroom.com" 
      Subject="Your Cheat Sheet War Room Request">
    </MailDefinition>
  
  </asp:PasswordRecovery>

  </div>
  
  
  
  </div>


  <script type="text/javascript" src="https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit" async defer></script>
 <script type="text/javascript">
var onloadCallback = function () {
    grecaptcha.render('dvCaptcha', {
        'sitekey': '<%=ReCaptcha_Key %>',
        'callback': function (response) {
            $.ajax({
                type: "POST",
                url: "passwordrecovery.aspx/VerifyCaptcha",
                data: "{response: '" + response + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var captchaResponse = jQuery.parseJSON(r.d);
                  if (captchaResponse.success) {
                        $('[id*=<%=prRecoverPassword.UserNameTemplateContainer.FindControl("txtCaptcha").ClientID%>]').val(captchaResponse.success);
                        $("[id*=<%=prRecoverPassword.UserNameTemplateContainer.FindControl("rfvCaptcha").ClientID%>]").hide();
                  } else {
                        $("[id*=<%=prRecoverPassword.UserNameTemplateContainer.FindControl("txtCaptcha").ClientID%>]").val("");
                        $("[id*=<%=prRecoverPassword.UserNameTemplateContainer.FindControl("rfvCaptcha").ClientID%>]").show();
                        var error = captchaResponse["error-codes"][0];
                        $("[id*=<%=prRecoverPassword.UserNameTemplateContainer.FindControl("rfvCaptcha").ClientID%>]").html("RECaptcha error. " + error);
                    }
                }
            });
        }
    });
};
</script>



</asp:Content>
