<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserProfile.ascx.cs" Inherits="BP.CheatSheetWarRoom.UI.UserControls.UserProfile" %>

<div class="userProfileControl">

  <h3>Personal</h3>
  <table>
    <tr>
      <td class="labelCell">First Name</td>
      <td>
        <asp:Textbox runat="server" ID="tbFirstName" CssClass="textbox"></asp:Textbox>
      </td>
    </tr>
    <tr runat="server" id="trEmailAddress">
      <td class="labelCell">
        <span class="required">(required)</span>
        Email Address
      </td>
      <td>
        <asp:Textbox runat="server" ID="tbEmailAddress" CssClass="textbox"></asp:Textbox>
        <%--Check Email Pattern--%>
        <asp:RegularExpressionValidator runat="server" ID="revEmailPattern" Display="Dynamic" SetFocusOnError="True"
          ControlToValidate="tbEmailAddress" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
          ToolTip="You must provide a valid email address." ValidationGroup="EditProfile"> 
          <asp:Image runat="server" ImageUrl="~/images/error.gif" EnableViewState="false" />
        </asp:RegularExpressionValidator>
        <%--Email is Required--%>
        <asp:RequiredFieldValidator runat="server" ID="rfvEmailRequired" ControlToValidate="tbEmailAddress"
          ErrorMessage="<img src='Images/error.gif' alt='E-mail is required.' title='Email Address is required' />" 
          ToolTip="Email Address is required." ValidationGroup="EditProfile">
          <asp:Image runat="server" ImageUrl="~/Images/error.gif" AlternateText="Email Address is required" ToolTip="Email Address is required" />
        </asp:RequiredFieldValidator>
      </td>
    </tr>
    <tr runat="server" id="trEmailAddress2">
      <td class="labelCell">
        Confirm Email Change
      </td>
      <td>
        <asp:Textbox runat="server" ID="tbEmailAddress2" CssClass="textbox"></asp:Textbox>
        <%--Check Email Pattern--%>
        <asp:RegularExpressionValidator runat="server" ID="revEmailPattern2" Display="Dynamic" SetFocusOnError="True"
          ControlToValidate="tbEmailAddress2" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
          ToolTip="You must provide a valid email address." ValidationGroup="EditProfile"> 
          <asp:Image runat="server" ImageUrl="~/images/error.gif" EnableViewState="false" />
        </asp:RegularExpressionValidator>
        <%--Email is Required--%>
        <%--<asp:RequiredFieldValidator runat="server" ID="rfvEmailRequired2" ControlToValidate="tbEmailAddress2"
          ErrorMessage="<img src='Images/error.gif' alt='E-mail is required.' title='Email Address is required' />" 
          ToolTip="Email Address is required." ValidationGroup="EditProfile">
          <asp:Image runat="server" ImageUrl="~/Images/error.gif" AlternateText="Email Address is required" ToolTip="Email Address is required" />
        </asp:RequiredFieldValidator>--%>
      </td>
    </tr>
 </table>

 

  <h3 class="nonTop">Preferences</h3>
  <table>
    <tr>
      <td colspan="2">
        <div class="instructions" style="width:325px;">
          We send out 2-3 emails per year to inform you of new features that are available, no spamming.  View 
          <asp:HyperLink runat="server" NavigateUrl="~/Images/Layout/sampleemail.gif" Target="_blank">sample</asp:HyperLink>
        </div>
      </td>
    </tr>
    <tr>
      <td class="labelCell">Application Updates</td>
      <td>
        <asp:DropDownList runat="server" ID="ddlNewsletter" CssClass="dropdown">
          <asp:ListItem Text="Subscribe" Value="1" Selected="True"></asp:ListItem>
          <asp:ListItem Text="Do not Subscribe" Value="0"></asp:ListItem>
        </asp:DropDownList>
      </td>
    </tr>
  </table>

</div>

