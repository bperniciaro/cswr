<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveOneCol.master" Theme="Web20" AutoEventWireup="true" CodeFile="editprofile.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.EditProfile" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage" 
  Title="Edit My Profile" 
  MetaDescription="Use this page to configure your personal profile settings."
  %>
<%@ MasterType VirtualPath="~/MasterPages/Sport.master" %>  
<%@ Register Src="~/usercontrols/UserProfile.ascx" TagName="UserProfile" TagPrefix="bp" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">

  <div class="profilePage">

    <cswr:MessageBox runat="server" id="mbStatus" WidthPercentage="50" />

    <div class="profileSec">
   
      <h2>Update your profile</h2>

      <div class="profileContainer">
        <bp:UserProfile ID="upUserProfile" runat="server"/>
        <div class="buttonContainer">
          <asp:Button runat="server" ID="btnUpdate" ValidationGroup="EditProfile" Text="Update Profile" OnClick="btnUpdate_Click" CssClass="button" />
        </div> 
      </div>
    </div>

    <div class="passwordSec">

      <h2>Change Your Password</h2>
      <asp:ChangePassword ID="cpChangePassword" runat="server" onsendingmail="cpChangePassword_SendingMail" OnChangedPassword="cpChangePassword_ChangedPassword">
        <ChangePasswordTemplate>         
          <table>
            <%--Password--%>
            <tr>
              <td>
                <span class="required">(required)</span> <asp:Label runat="server" CssClass="label" ID="labCurrentPassword" AssociatedControlID="CurrentPassword" Text="Current password:" /> 
              </td>
              <td>
                <asp:TextBox ID="CurrentPassword" TextMode="Password" runat="server" CssClass="textbox"></asp:TextBox>
              </td>
              <td>
                <asp:RequiredFieldValidator ID="valRequireCurrentPassword" runat="server" ControlToValidate="CurrentPassword" SetFocusOnError="true" Display="Dynamic"
                  ErrorMessage="<img src='Images/error.gif' alt='Current Password is required.' title='Current Password is required' />" 
                  ToolTip="Current Password is required." ValidationGroup="cpChangePassword">
                  <img src="Images/error.gif" alt="Current Password is required" title="Current Password is required." />
                </asp:RequiredFieldValidator>
               </td>            
            </tr>
            <%--New Password--%>
            <tr>
              <td class="fieldname">
                <span class="required">(required)</span> <asp:Label runat="server" CssClass="label" ID="lblNewPassword" AssociatedControlID="NewPassword" Text="New Password:" /> 
              </td>
              <td>
                <asp:TextBox ID="NewPassword" TextMode="Password" runat="server"  CssClass="textbox"></asp:TextBox>
              </td>
              <td>
                <asp:RequiredFieldValidator ID="valRequireNewPassword" runat="server" ControlToValidate="NewPassword" SetFocusOnError="true" Display="Dynamic"
                  ErrorMessage="<img src='Images/error.gif' alt='New Password is required.' title='New Password is required' />" 
                  ToolTip="New Password is required." ValidationGroup="cpChangePassword">
                  <img src="Images/error.gif" alt="New Password is required" title="New Password is required." />
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="valPasswordLength" runat="server" ControlToValidate="NewPassword" SetFocusOnError="true" Display="Dynamic"
                   ValidationExpression="\w{5,}" 
                  ErrorMessage="<img src='Images/error.gif' alt='New Password must be at least 6 characters long.' title='New Password must be at least 6 characters long.' />" 
                  ToolTip="New Password must be at least 6 characters long." ValidationGroup="cpChangePassword">
                  <img src="Images/error.gif" alt="New Password must be at least 6 characters long." title="New Password must be at least 6 characters long." />
                 </asp:RegularExpressionValidator>
              </td>            
            </tr>
            <%--Confirm Password--%>
            <tr>
              <td class="fieldname">
                <span class="required">(required)</span> <asp:Label runat="server" CssClass="label" ID="lblConfirmPassword" AssociatedControlID="ConfirmNewPassword" Text="Confirm password:" /> 
              </td>
              <td>
                <asp:TextBox ID="ConfirmNewPassword" TextMode="Password" runat="server" CssClass="textbox"></asp:TextBox>
              </td>
              <td>
                <asp:RequiredFieldValidator ID="valRequireConfirmNewPassword" runat="server" ControlToValidate="ConfirmNewPassword" SetFocusOnError="true" Display="Dynamic"
                  ErrorMessage="<img src='Images/error.gif' alt='Confirm Password is required.' title='Confirm Password is required' />" 
                  ToolTip="Confirm Password is required." ValidationGroup="cpChangePassword">
                  <img src="Images/error.gif" alt="Confirm Password is required" title="Confirm Password is required." />
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="valComparePasswords" runat="server" ControlToCompare="NewPassword"
                  ControlToValidate="ConfirmNewPassword" SetFocusOnError="true" Display="Dynamic" ErrorMessage="The Confirm Password must match the New Password entry."
                  ValidationGroup="cpChangePassword">*</asp:CompareValidator>
              </td>            
            </tr>
            <%--Button Container--%>
            <tr>
              <td colspan="3" style="text-align:right">
                <div>
                  <asp:Label ID="FailureText" runat="server" EnableViewState="False" CssClass="changePasswordFailed" /> 
                </div>
                <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword" CssClass="button" Text="Change Password" ValidationGroup="cpChangePassword" />
              </td>
            </tr>
         </table>
         <asp:ValidationSummary runat="server" ID="valChangePasswordSummary" ValidationGroup="cpChangePassword" ShowMessageBox="false" ShowSummary="false" />
       </ChangePasswordTemplate>
       <SuccessTemplate>
         <asp:Label runat="server" ID="lblSuccess" CssClass="changePasswordSuccess" Text="Your password has been successfully changed." />
       </SuccessTemplate>
       <MailDefinition
         BodyFileName="~/TextFiles/ChangePasswordMail.txt"
         From="admin@cheatsheetwarroom.com"
         Subject="Cheat Sheet War Room: Password Changed">
       </MailDefinition>
     </asp:ChangePassword>
   </div>
  
   <br /><br /><br /><br />
   <br /><br /><br /><br />
       
  </div>

</asp:Content>

