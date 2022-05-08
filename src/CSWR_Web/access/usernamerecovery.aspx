<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveOneCol.master" Theme="Web20" AutoEventWireup="true" CodeFile="usernamerecovery.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.UsernameRecovery" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
  Title="Recover your Cheat Sheet War Room Username" 
  MetaDescription="Use this page to recover your username if you've forgotten it.  You must answer your security question"
  CanonicalUrl="https://www.cheatsheetwarroom.com/access/usernamerecovery.aspx"
MetaRobotsText="NOINDEX,FOLLOW"
%>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">

  <div class="usernameRecoveryPage">

    <h1>Recover your Username</h1>
  
    <br />
    <cswr:MessageBox runat="server" ID="mbMessageBox" />

    <asp:Panel runat="server" ID="panControls">

    <p class="instructions">If you forgot your username, you can use this page to have it sent to the email address you provided upon registration.</p>

    <div class="controlsContainer">
      <table>
        <tr>
          <td>Email:</td>
          <td>
            <asp:TextBox runat="server" ID="tbEmailAddress" Width="250"></asp:TextBox>
            <%--Email must be Valid--%>
            <asp:RegularExpressionValidator runat="server" ID="revEmailPattern" Display="Dynamic" SetFocusOnError="True" 
              ControlToValidate="tbEmailAddress" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="usernameRecovery"
              ToolTip="You must provide a valid email address.">You must provide a valid email address
            </asp:RegularExpressionValidator>
            <%--Email is Required--%>
            <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="tbEmailAddress" ValidationGroup="usernameRecovery"
              ErrorMessage="E-mail is required" ToolTip="E-mail is required." ForeColor="Red"> E-mail is required
            </asp:RequiredFieldValidator>
          </td>
        </tr>
        <tr>
          <td colspan="2">
            <br />
             <div id="dvCaptcha"></div>
            <asp:TextBox ID="txtCaptcha" runat="server" Style="display: none" />
            <asp:RequiredFieldValidator ID = "rfvCaptcha" ErrorMessage="Captcha validation is required." ControlToValidate="txtCaptcha"
                runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="usernameRecovery"/>
            <br />
          </td>
        </tr>
        <tr>
          <td></td>
          <td>
            <asp:Button runat="server" ID="butSubmit" onclick="butSubmit_Click" Text="Submit" ValidationGroup="usernameRecovery" />
          </td>
        </tr>
      </table>
    </div>

    
    </asp:Panel>
      
  </div>

      <script type="text/javascript" src="https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit" async defer></script>
    <script type="text/javascript">
      var onloadCallback = function () {
          grecaptcha.render('dvCaptcha', {
              'sitekey': '<%=ReCaptcha_Key %>',
              'callback': function (response) {
                  $.ajax({
                      type: "POST",
                      url: "usernamerecovery.aspx/VerifyCaptcha",
                      data: "{response: '" + response + "'}",
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      success: function (r) {
                          var captchaResponse = jQuery.parseJSON(r.d);
                        if (captchaResponse.success) {
                              $("[id*=txtCaptcha]").val(captchaResponse.success);
                              $("[id*=rfvCaptcha]").hide();
                          } else {
                              $("[id*=txtCaptcha]").val("");
                              $("[id*=rfvCaptcha]").show();
                              var error = captchaResponse["error-codes"][0];
                              $("[id*=rfvCaptcha]").html("RECaptcha error. " + error);
                          }
                      }
                  });
              }
          });
      };
    </script>




</asp:Content>
