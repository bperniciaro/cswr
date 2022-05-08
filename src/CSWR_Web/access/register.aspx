<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveOneCol.master" Theme="Web20" AutoEventWireup="true" CodeFile="register.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.Register" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage" 
  Title="Register for a free account at Cheat Sheet War Room"
  MetaDescription="Register to gain access to all of our cheat sheet creation tools."
  CanonicalUrl="http://www.cheatsheetwarroom.com/access/register.aspx"
%>
<%@ MasterType VirtualPath="~/MasterPages/Sport.master" %>
<%@ Register Assembly="Recaptcha" Namespace="Recaptcha" TagPrefix="recaptcha" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">

<div class="registerPage">

  <h1>Register for Free</h1>
  <p runat="server" id="pIntro">
    Registering allows us to remember which sheets belong to you. 
  </p>

  <cswr:MessageBox runat="server" ID="mbStatus" WidthPercentage="60" />

  <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" 
    CreateUserButtonText="Register" CreateUserButtonStyle-CssClass="button"  
    OnActiveStepChanged="cuwCreateUserWizard_ActiveStepChanged" 
    StepNextButtonText="Next" OnCreatedUser="cuwCreateUserWizard_OnCreatedUser"
    ActiveStepIndex="0"
    OnSendingMail="CreateUserWizard1_OnSendingMail"
    oncreateusererror="cuwCreateUserWizard_CreateUserError">
        
    <WizardSteps>

      <asp:CreateUserWizardStep ID="cuwsCreateUserStep" runat="server">
        <ContentTemplate>
          <table border="0" class="tableForm">
            <%--Username--%>
            <tr>
              <td class="labelCell">
                <span class="required">(required)</span> <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username</asp:Label> 
              </td>
              <td>
                <asp:TextBox ID="UserName" runat="server" CssClass="textBox" Width="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" Display="Dynamic" 
                  ErrorMessage="<img src='Images/error.gif' alt='Username is required.' title='Username is required' />" 
                  ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">
                  <asp:Image runat="server" ImageUrl="~/Images/error.gif" AlternateText="Username is required"/>
                </asp:RequiredFieldValidator>
              </td>
            </tr>
            <%--Password--%>
            <tr>
              <td class="labelCell">
                <span class="required">(required)</span> <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password</asp:Label> 
              </td>
              <td>
                <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="textBox" Width="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                  ErrorMessage="<img src='Images/error.gif' alt='Password is required.' title='Password is required' />" 
                  ToolTip="Password is required." ValidationGroup="CreateUserWizard1">
                  <asp:Image runat="server" ImageUrl="~/Images/error.gif" AlternateText="Password is required" />
                </asp:RequiredFieldValidator>
              </td>
            </tr>
            <%--Confirm Password--%>
            <tr>
              <td class="labelCell">
                <span class="required">(required)</span> <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password</asp:Label> 
              </td>
              <td>
                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" CssClass="textBox" Width="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                  ErrorMessage="<img src='Images/error.gif' alt='Confirm Password is required.' title='Confirm Password is required' />" ToolTip="Confirm Password is required."
                  ValidationGroup="CreateUserWizard1">
                  <asp:Image runat="server" ImageUrl="~/Images/error.gif" AlternateText="Confirm Password is required" />
                </asp:RequiredFieldValidator>
              </td>
            </tr>
            <%--Email--%>
            <tr>
              <td class="labelCell">
                <span class="required">(required)</span> <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail</asp:Label> 
              </td>
              <td>
                <asp:TextBox ID="Email" runat="server" CssClass="textBox" Width="200"></asp:TextBox>
                <%--Check Email Pattern--%>
                <asp:RegularExpressionValidator runat="server" ID="revEmailPattern" Display="Dynamic" SetFocusOnError="True" 
                  ControlToValidate="Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                  ToolTip="You must provide a valid email address." ValidationGroup="CreateUserWizard1"> 
                  <asp:Image ID="Image1" runat="server" ImageUrl="~/images/error.gif" EnableViewState="false" />
                </asp:RegularExpressionValidator>
                <%--Email is Required--%>
                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                  ErrorMessage="<img src='Images/error.gif' alt='E-mail is required.' title='E-mail is required' />" 
                  ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">
                  <asp:Image runat="server" ImageUrl="~/Images/error.gif" AlternateText="Email is required" />
                </asp:RequiredFieldValidator>
              </td>
            </tr>
            <tr>
              <td class="labelCell"><strong>First Name</strong>
              </td>
              <td>
                <asp:Textbox runat="server" ID="tbFirstName" CssClass="textBox" />
              </td>
            </tr>
            <tr>
              <td class="labelCell"><strong>Freebies, tips, & promos via email</strong>
              </td>
              <td>
                <asp:CheckBox runat="server" ID="cbSubscribe" Checked="true" />
              </td>
            </tr>
            <tr>
              <td class="cAlign" colspan="2">
                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                  ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                  ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
              </td>
            </tr>
            <tr>
              <td class="cAlign" colspan="2" style="color: red">
                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False">
                </asp:Literal>
              </td>
            </tr>
            <tr>
              <td colspan="2">
                <div id="dvCaptcha" style="margin:auto;width:300px;">
                </div>
                <asp:TextBox ID="txtCaptcha" runat="server" Style="display: none" />
                <asp:RequiredFieldValidator ID = "rfvCaptcha" ErrorMessage="Captcha is required"  
                  ControlToValidate="txtCaptcha" ValidationGroup="CreateUserWizard1" ToolTip="Captcha is required."
                  runat="server" ForeColor="Red" Display="Dynamic">
                </asp:RequiredFieldValidator>
                <br />
              </td>
            </tr>
          </table>
        </ContentTemplate>
      </asp:CreateUserWizardStep>
      <%--Profile Step--%>
      <asp:WizardStep ID="wsUserProfileStep" runat="server" StepType="Finish">
        <div class="title extraTitle">
          <strong>Configure Profile</strong> 
        </div>
        <div style="text-align:left;padding-left:4px;">
          <cswr:UserProfile ID="upUserProfile" runat="server" HideEmailAddress="true" />
        </div>
     </asp:WizardStep>
    </WizardSteps>
    <MailDefinition
      BodyFileName="~/TextFiles/RegisterMail.txt"
      From="accounts@cheatsheetwarroom.com"
      Subject="Your new account at Cheat Sheet War Room">
    </MailDefinition>
  </asp:CreateUserWizard>

</div>

<script type="text/javascript" src="https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit" async defer></script>

<script type="text/javascript">
var onloadCallback = function () {
    grecaptcha.render('dvCaptcha', {
        'sitekey': '<%=ReCaptcha_Key %>',
        'callback': function (response) {
            $.ajax({
                type: "POST",
                url: "register.aspx/VerifyCaptcha",
                data: "{response: '" + response + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var captchaResponse = jQuery.parseJSON(r.d);
                  if (captchaResponse.success) {
                    //alert("success");
                        //$("[id*=txtCaptcha]").val(captchaResponse.success);
                        $('[id*=<%=CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("txtCaptcha").ClientID%>]').val(captchaResponse.success);
                        $("[id*=<%=CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("rfvCaptcha").ClientID%>]").hide();
                  } else {
                    //alert("error");
                        $("[id*=<%=CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("txtCaptcha").ClientID%>]").val("");
                        $("[id*=<%=CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("rfvCaptcha").ClientID%>]").show();
                        var error = captchaResponse["error-codes"][0];
                        $("[id*=<%=CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("rfvCaptcha").ClientID%>]").html("RECaptcha error. " + error);
                    }
                }
            });
        }
    });
};
</script>
</asp:Content>
