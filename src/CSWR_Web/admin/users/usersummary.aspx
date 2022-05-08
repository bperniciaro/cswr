<%@ Page Title="Users Summary Information" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" 
    CodeFile="usersummary.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.Admin.Users.UserSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

  <h1>Summary User Info</h1>

    <%--Stat Summary--%>
    <div class="stats">
      <p>
        <strong>Total registered users:</strong> <asp:Literal runat="server" ID="litTotalUsers"></asp:Literal>
      </p>
      <p>
        <strong>Users online now:</strong> <asp:Literal runat="server" ID="litOnlineUsers"></asp:Literal>
      </p>
      <p>
        <strong>Registrations Today:</strong> <asp:Literal runat="server" ID="litRegistrationsToday"></asp:Literal>
      </p>
    </div>

    <br />
    <asp:GridView runat="server" ID="gvOnlineUsers" AutoGenerateColumns="false" CssClass="standardGrid" SkinID="Professional">
      <Columns>
        <asp:BoundField HeaderText="UserName" DataField="UserName" />
        <asp:BoundField HeaderText="Email" DataField="Email" />
        <asp:BoundField HeaderText="CreationDate" DataField="CreationDate" />
      </Columns>    
    </asp:GridView>
    <br />
    <br />

</asp:Content>

