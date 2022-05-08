<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="manageusers.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.Admin.Users.ManageUsers" Title="Manage Users" %>
<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphAdminContent" Runat="Server">

  <div class="manageUsersPage">
  
    <h1>Manage Users</h1>
    
    <%--Letter Links--%>
    <asp:Repeater runat="server" ID="repAlphabet" OnItemCommand="repAlphabet_ItemCommand">
      <ItemTemplate>
        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Container.DataItem %>' CommandArgument='<%# Container.DataItem %>' CssClass="linkLetter"></asp:LinkButton>
      </ItemTemplate>
    </asp:Repeater>
    <br />

    <%--Search Criteria--%>
    <p>Use the controls below to search users by partial username or e-mail:</p>
    <div style="margin:6px 0px;">
      <asp:DropDownList runat="server" ID="ddlSearchType">
        <asp:ListItem Text="UserName" Selected="true"></asp:ListItem>
        <asp:ListItem Text="E-mail"></asp:ListItem>
      </asp:DropDownList>
      contains
      <asp:TextBox runat="server" ID="tbSearchText"></asp:TextBox>
      <asp:Button runat="server" ID="butSearch" Text="Search" OnClick="butSearch_Click" />
    </div>

    <%--Currently displayed Users--%>
    <asp:GridView ID="gvUsers" runat="server" Width="100%" PagerSettings-Position="TopAndBottom" 
      AutoGenerateColumns="false" DataKeyNames="UserName" OnRowCreated="gvUsers_RowCreated" 
      OnRowDeleting="gvUsers_RowDeleting" CssClass="manageUsersGrid"
      AllowPaging="true" PageSize="20" onrowdatabound="gvUsers_RowDataBound" 
      onpageindexchanging="gvUsers_PageIndexChanging">
      <PagerStyle Font-Size="Medium" />
      <Columns>
        <asp:BoundField HeaderText="UserName" DataField="UserName" />
        <asp:HyperLinkField HeaderText="E-mail" DataTextField="Email" DataNavigateUrlFormatString="mailto:{0}" DataNavigateUrlFields="Email" />
        <%--<asp:BoundField HeaderText="Created" DataField="CreationDate" DataFormatString="{0:MM/dd/yy h:mm tt}" />--%>

        
        <asp:TemplateField HeaderText="Created">
          <ItemTemplate>
            <asp:Label runat="server" ID="labCreated" />
          </ItemTemplate>
        </asp:TemplateField>


        <asp:TemplateField HeaderText="Last Activity">
          <ItemTemplate>
            <asp:Label runat="server" ID="labLastActivity" />
          </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Subscribed">
          <ItemTemplate>
            <asp:CheckBox runat="server" id="cbSubscribed" Enabled="false" />
          </ItemTemplate>
        </asp:TemplateField>
        <asp:CheckBoxField HeaderText="Approved" DataField="IsApproved" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"></asp:CheckBoxField>
        <asp:HyperLinkField Text="&lt;img src='../../Images/GridViewButtons/Edit.gif'" DataNavigateUrlFields="UserName" DataNavigateUrlFormatString="EditUser.aspx?Username={0}"  />
        <asp:ButtonField ButtonType="Image" CommandName="Delete" ImageUrl="~/Images/GridViewButtons/Delete.gif" Text="Button" />
      </Columns>
      <EmptyDataTemplate>
       No users found for the specified criteria
      </EmptyDataTemplate>
    </asp:GridView>
  
  </div>


</asp:Content>

