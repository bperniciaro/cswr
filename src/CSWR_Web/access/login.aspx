<%@ Page Title="Login to Cheat Sheet War Room" Language="C#" MasterPageFile="~/MasterPages/ResponsiveOneCol.master" Theme="Web20"
  AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.UserLogin" 
  CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
  MetaDescription="Login to gain access to advanced cheat sheet features."
  MetaRobotsText="NOINDEX,FOLLOW"
  %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">


<div class="loginPage">

  <h1>Login</h1>

  <cswr:MessageBox runat="server" ID="mbLoginError" MessageType="ERROR" Visible="false" WidthPercentage="25"/>

  <%--Logon--%>
  <asp:LoginView runat="server" ID="lvLoginView">
    <AnonymousTemplate>

      <p>
        Login to access your customized cheat sheets, or 
        <asp:HyperLink runat="server" NavigateUrl="~/access/register.aspx">register</asp:HyperLink>
        for free.
      </p>

      <asp:Login runat="server" ID="logLogin" FailureAction="Refresh"  
        OnLoggedIn="logLogin_OnLoggedIn" OnLoginError="logLogin_OnLoggedError">
        <LayoutTemplate>
          <table class="login">
            <%-- Username --%>
            <tr>
              <td class="labelCell" style="height: 34px">
                <asp:Label ID="labUserLabel" runat="server" CssClass="loginLabel" AssociatedControlID="UserName">Username</asp:Label>
              </td>
              <td style="height: 34px">
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" CssClass="errorAsterisk"
                  ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="vgLogin">*</asp:RequiredFieldValidator>
                <asp:TextBox ID="UserName" runat="server" CssClass="textBox"></asp:TextBox>
              </td>
            </tr>
            <%--Password--%>
            <tr>
              <td class="labelCell">
                <asp:Label ID="labPasswordLabel" runat="server" CssClass="loginLabel" AssociatedControlID="Password">Password</asp:Label>
              </td>
              <td>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" CssClass="errorAsterisk"
                  ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="vgLogin">*</asp:RequiredFieldValidator>
                <asp:TextBox ID="Password" runat="server" TextMode="Password"  CssClass="textBox"></asp:TextBox>
              </td>
            </tr>
            <%--Submit & Remember--%>
            <tr>
              <td>
              </td>
              <td class="rememberMeAndPassword">
                <asp:CheckBox ID="RememberMe" runat="server" CssClass="loginCheckBox" /> <asp:Label ID="labRememberLabel" runat="server" CssClass="rememberLabel">remember me</asp:Label>
              </td>
            </tr>
            <%--Register & Forgotten Password--%>
            <tr>
              <td>
              </td>
              <td class="buttonCell">
                <table style="border-collapse:collapse;width:100%">
                  <tr>
                    <td>
                      <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Login" ValidationGroup="vgLogin" CssClass="loginSubmit" />
                    </td>
                    <td>
                      <asp:HyperLink runat="server" ID="hlForgottenPassword" NavigateUrl="~/access/passwordrecovery.aspx" Text="forgotten password?" CssClass="forgot" style="display:block;"></asp:HyperLink>
                      <asp:HyperLink runat="server" ID="hlForgottenUsername" NavigateUrl="~/access/usernamerecovery.aspx" Text="forgotten username?" CssClass="forgot" style="display:block;"></asp:HyperLink>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>

          </table>
        </LayoutTemplate>
      </asp:Login>
    </AnonymousTemplate>
    <LoggedInTemplate>
      <div class="loggedInContainer">
        <p>
          <asp:LoginName runat="server" ID="lnUserName" FormatString="Hi {0}!" CssClass="helloMessage" />
        </p>
        <p class="links">
          <asp:HyperLink runat="server" ID="hlAdminInterface" NavigateUrl="~/admin/dashboard.aspx" Visible="false" Text="Admin" />
          <asp:HyperLink runat="server" ID="hlProfile" Text="Edit Profile" NavigateUrl="~/EditProfile.aspx"></asp:HyperLink>
          <asp:LoginStatus runat="server" ID="lsLoginStatus" OnLoggedOut="lsLoginStatus_OnLoggedOut" />
        </p>
        </div>
      </LoggedInTemplate>
  </asp:LoginView>


</div>

</asp:Content>

