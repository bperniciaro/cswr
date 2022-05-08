<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="edituser.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.Admin.Users.EditUser" Title="Cheat Sheet War Room Administration - Edit User" %>
<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<%@ Register Src="~/usercontrols/UserProfile.ascx" TagName="UserProfile" TagPrefix="bp" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphAdminContent" Runat="Server">

<div class="editUserPage">

  <cswr:MessageBox runat="server" id="mbStatus" WidthPercentage="50" />

  <%--User Summary--%>
  <div class="section">
  
    <h2>User Summary</h2>

    <table>
      <tr>
        <td>Username:</td>
        <td><asp:Literal runat="server" ID="litUserName"></asp:Literal></td>
      </tr>
      <tr>
        <td>Email:</td>
        <td><asp:Hyperlink runat="server" ID="hypEmail"></asp:Hyperlink></td>
      </tr>
      <tr>
        <td>Registered:</td>
        <td><asp:Literal runat="server" ID="litRegistered"></asp:Literal></td>
      </tr>
      <tr>
        <td>Last Login:</td>
        <td><asp:Literal runat="server" ID="litLastLogin"></asp:Literal></td>
      </tr>
      <tr>
        <td>Last Activity:</td>
        <td><asp:Literal runat="server" ID="litLastActivity"></asp:Literal></td>
      </tr>
      <tr>
        <td>Online Now:</td>
        <td><asp:CheckBox runat="server" ID="cbOnlineNow" Enabled="False" /></td>
      </tr>
      <tr>
        <td>Approved:</td>
        <td><asp:CheckBox runat="server" ID="cbApproved" Enabled="False" AutoPostBack="True" OnCheckedChanged="cbApproved_CheckChanged" /></td>
      </tr>
      <tr>
        <td>Locked Out:</td>
        <td><asp:CheckBox runat="server" ID="cbLockedOut" AutoPostBack="True" OnCheckedChanged="cbLockedOut_CheckChanged" /></td>
      </tr> 
    </table>
  </div>

  <%--User Role--%>
  <div class="section">
    
    <h2>Edit a user's role</h2>


    <table>
      <tr>
        <td>
          <asp:CheckBoxList runat="server" ID="cblRoles" RepeatColumns="5" CellSpacing="4" CssClass="checkBoxList"></asp:CheckBoxList>
        </td>
        <td>
          <asp:Button runat="server" ID="butUpdateRoles" Text="Update" OnClick="butUpdateRoles_Click" CssClass="updateRolesButton" />
        </td>
      </tr>
      <tr>
        <td colspan="2">
          <asp:Label runat="server" ID="labRolesFeedbackOK" SkinID="FeedbackOK" Text="Roles updated successfully" Visible="False"></asp:Label>
        </td>
      </tr>
      <tr>
        <td colspan="2">
          <span style="margin-left:5px;">Create a new role:</span>
          <asp:TextBox runat="server" ID="tbNewRole"></asp:TextBox>
          <asp:Button runat="server" ID="butCreateRole" Text="Create" ValidationGroup="CreateRole" OnClick="butCreateRole_Click" />
        </td>
      </tr>
      <tr>
        <td colspan="2">
          <asp:RequiredFieldValidator runat="server" ID="rfvNewRoleRequired" ControlToValidate="tbNewRole" SetFocusOnError="true" Display="dynamic"
            ErrorMessage="Role name is required" ValidationGroup="CreateRole"></asp:RequiredFieldValidator>
        </td>
      </tr>
    </table>

  </div>

  <%--User Profile--%>
  <div class="section">

    <bp:UserProfile runat="server" ID="upUserProfile" />
    <table>
      <tr>
        <td>
          <asp:Label runat="server" id="labProfileFeedbackOK" SkinID="feedbackOK" Text="Profile updated successfully" Visible="False"></asp:Label>
          <asp:Button runat="server" ID="butUpdateProfile" Text="Update" OnClick="butUpdateProfile_Click" CausesValidation="true" />
        </td>
      </tr>
    </table>  
  </div>

</div>

</asp:Content>

