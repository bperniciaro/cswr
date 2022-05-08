<%@ Page Title="Power Users" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="powerusers.aspx.cs" 
    Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.PowerUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

  <h1>Power Users</h1>

  <asp:GridView runat="server" ID="gvPowerUsers" SkinID="Professional" AutoGenerateColumns="false" 
    OnRowDataBound="gvPowerUsers_RowDataBound">
    <Columns>
      <%--Link to User Profile--%>
      <asp:TemplateField HeaderText="Username">
        <ItemTemplate>
          <asp:HyperLink runat="server" ID="hlUsername" NavigateUrl='<%# Eval("UserName", "~/admin/users/edituser.aspx?Username={0}") %>' Text='<%# Eval("UserName") %>' ></asp:HyperLink>    
        </ItemTemplate>
      </asp:TemplateField>
      <%--Session Count--%>
      <asp:BoundField DataField="SessionCount" HeaderText="Sessions" />
      <%--Email Link--%>
      <asp:TemplateField HeaderText="Email">
        <ItemTemplate>
          <asp:HyperLink runat="server" ID="hlEmail"></asp:HyperLink>    
        </ItemTemplate>
      </asp:TemplateField>
      <%--Indicates if the user is a moderator--%>
      <asp:TemplateField HeaderText="IsMod">
        <ItemTemplate>
          <asp:Label runat="server" ID="labIsModerator" />    
        </ItemTemplate>
      </asp:TemplateField>
    </Columns>
  </asp:GridView>

  <asp:Label runat="server" ID="labEmail" />

</asp:Content>

