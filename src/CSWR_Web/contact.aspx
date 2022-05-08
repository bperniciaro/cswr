<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveOneCol.master" Theme="Web20" AutoEventWireup="true" CodeFile="contact.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.Contact" Title="Contact Cheat Sheet War Room with Feedback" 
  MetaDescription="If you have any questions or comments please let us know using this contact form."
  CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage" CanonicalUrl="https://www.cheatsheetwarroom.com/contact.aspx"
%>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>



<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">


  <asp:Panel runat="server" ID="panContainer" CssClass="contactPage" style="max-width:550px;">

    <div class="row">

      <div class="col-md-12">

        <h1>Contact us with any questions or comments</h1>
        <p>
          If you have any questions or comments, please let me know about it using the contact form below.  Alternatively, you can contact me at the physical or
          email addresses listed below. 
        </p>

        <br />

        <asp:Panel runat="server" ID="panStatus" Visible="false">
          <asp:Label runat="server" ID="labStatus" />
        </asp:Panel>

        <asp:Panel runat="server" ID="panFirstForm" class="form-horizontal">

          <%--Name--%>
          <div class="form-group">
            <label class="control-label col-sm-4">
              <span class="required">(required)</span> Your Name
            </label>
            <div class="col-sm-8">
              <asp:TextBox runat="server" ID="tbName" Width="200"></asp:TextBox>
              <asp:RequiredFieldValidator runat="server" ID="rfvRequiredName" Display="dynamic" SetFocusOnError="true" ControlToValidate="tbName"
                ErrorMessage="<img src='Images/error.gif' alt='Your Name is required.' title='Your Name is required' />" 
                ToolTip="Your Name is required."> 
                <img src="Images/error.gif" alt="Your Name is required" title="Your Name is required." />
              </asp:RequiredFieldValidator>
            </div> <!-- close col-sm-8 -->
          </div>  <!-- close your name div -->

          <%--Email--%>
          <div class="form-group">
            <label class="control-label col-sm-4">
              <span class="required">(required)</span> Your Email
            </label>
            <div class="col-sm-8">
              <asp:TextBox runat="server" ID="tbEmail" Width="200"></asp:TextBox>
              <asp:RequiredFieldValidator runat="server" ID="rfvRequiredEmail" Display="dynamic" SetFocusOnError="True" ControlToValidate="tbEmail"
                ErrorMessage="<img src='Images/error.gif' alt='Your Email is required.' title='Your Email is required' />" 
                ToolTip="Your EMail is required."> 
                <img src="Images/error.gif" alt="Your EMail is required" title="Your EMail is required." />
              </asp:RequiredFieldValidator> 
              <asp:RegularExpressionValidator runat="server" ID="revEmailPattern" Display="Dynamic" SetFocusOnError="True" ControlToValidate="tbEmail"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                ErrorMessage="<img src='Images/error.gif' alt='You must provide a valid email address.' title='You must provide a valid email address.' />" 
                ToolTip="You must provide a valid email address."> 
                <img src="Images/error.gif" alt="You must provide a valid email address" title="You must provide a valid email address." />
              </asp:RegularExpressionValidator>
            </div> <!-- close col-sm-8 -->
          </div>  <!-- close your name div -->

          <%--Subject--%>
          <div class="form-group">
            <label class="control-label col-sm-4">
              Subject
            </label>
            <div class="col-sm-8">
              <asp:DropDownList runat="server" ID="ddlEmailType">
                <asp:ListItem Text="General Comment" Value="General Comment" />
                <asp:ListItem Text="General Question" Value="General Question" />
                <asp:ListItem Text="Report an Error" Value="Report an Error" />
                <asp:ListItem Text="Advertising Query" Value="Advertising Query" />
                <asp:ListItem Text="Other" Value="Other" />
              </asp:DropDownList>
            </div> <!-- close col-sm-8 -->
          </div>  <!-- close your name div -->

          <%--Title--%>
          <div class="form-group">
            <label class="control-label col-sm-4">
              Title
            </label>
            <div class="col-sm-8">
              <asp:TextBox runat="server" ID="tbTitle" Width="200"></asp:TextBox>
            </div> <!-- close col-sm-8 -->
          </div>  <!-- close your name div -->

          <%--Body--%>
          <div class="form-group">
            <label class="control-label col-sm-4">
              <span class="required">(required)</span> Body
            </label>
            <div class="col-sm-8">
              <asp:TextBox runat="server" ID="tbBody" TextMode="MultiLine" Height="100" Width="300"></asp:TextBox>
              <asp:RequiredFieldValidator runat="server" ID="rfvRequiredBody" Display="Dynamic" SetFocusOnError="True" ControlToValidate="tbBody"
                ErrorMessage="<img src='Images/error.gif' alt='Body is required.' title='Body is required' />" 
                ToolTip="Body is required."> 
                <img src="Images/error.gif" alt="Body is required" title="Body is required." />
              </asp:RequiredFieldValidator>
            </div> <!-- close col-sm-8 -->
          </div>  <!-- close your name div -->
          
          <%--Title--%>
          <div class="form-group">
            <label class="control-label col-sm-4">
              <span class="required">(required)</span> What is 4 + 3?
            </label>
            <div class="col-sm-8">
               <asp:TextBox runat="server" ID="tbSumCheck" />
              <asp:RequiredFieldValidator runat="server" ID="rfvRequiredSum" Display="Dynamic" SetFocusOnError="True" ControlToValidate="tbBody"
                ErrorMessage="<img src='Images/error.gif' alt='Sum is required.' title='Sum is required' />" 
                ToolTip="Sum is required."> 
                <img src="Images/error.gif" alt="Sum is required" title="Sum is required." />
                </asp:RequiredFieldValidator>
            </div> <!-- close col-sm-8 -->
          </div>  <!-- close your name div -->

          <%--Captcha--%>
          <div class="form-group">
            <label class="control-label col-sm-4">
              
            </label>
            <div class="col-sm-8">

              <div id="dvCaptcha"></div>
              <asp:TextBox ID="txtCaptcha" runat="server" Style="display: none" />
              <asp:RequiredFieldValidator ID = "rfvCaptcha" ErrorMessage="Captcha validation is required." ControlToValidate="txtCaptcha"
                runat="server" ForeColor="Red" Display = "Dynamic" />

            </div> <!-- close col-sm-8 -->
          </div>  <!-- close your name div -->

          <%--Submit Button--%>
          <div class="form-group">
            <div class="col-xs-12">
              <asp:Button runat="server" ID="butSubmit" Text="Send" OnClick="txtSubmit_Click" CssClass="btn btn-primary" />
            </div>
          </div>

        </asp:Panel>


        <asp:Panel runat="server" ID="panSecondForm" Visible="false" style="padding:20px;0px;">
          <div class="row">
            <div class="col-md-12">
              <h3>Validation Question</h3>
              What is the last name of the Saints' starting QB?
              <asp:TextBox runat="server" ID="tbPlayerName" Width="75"></asp:TextBox>
              <asp:Button runat="server" ID="butSubmitName" Text="Submit" OnClick="butSubmitName_Click" />
            </div>
          </div>
        </asp:Panel>

   
    <br /><br /><br /><br /><br /><br /><br /><br />
    <h2>Mailings</h2>
    <br />
    <p>Brad Perniciaro</p>
    <p>8852 Kipapa Way</p>
    <p>Diamondhead, MS</p>
    <p>39525</p>

    <br />
    <h2>Email</h2>
    <br />
    <p>brad@cheatsheetwarroom + dotcom</p>

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
                      url: "contact.aspx/VerifyCaptcha",
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


  </asp:Panel>  <!-- Contact Page -->


</asp:Content>


