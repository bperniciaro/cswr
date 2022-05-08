<%@ Page Title="Players who are likely retired" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" 
  CodeFile="likelyretired.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.LikelyRetired" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

  <h1>
    Likely Retired Players
  </h1>

  <p>
    After a season has ended, you can use this page to isolate player who didn't record a single stat.  After researching each player
    you can 'retire' them if they are in fact retired.
  </p>

  <asp:GridView runat="server" ID="gvLikelyRetired" AutoGenerateColumns="false" SkinID="Professional" OnRowDataBound="gvLikelyRetired_RowDataBound">
    <Columns>
      <asp:TemplateField>
        <ItemTemplate>
          <asp:CheckBox runat="server" id="cbOpenNFLProfiles"/>
        </ItemTemplate>
      </asp:TemplateField>   
      <asp:BoundField DataField="FullNameLastFirst"  HeaderText="Full Name" />
      <asp:BoundField DataField="TeamCode" HeaderText="Team" />
      <asp:BoundField DataField="PositionCode" HeaderText="Position" />
      <%--NFL Profile Link--%>
      <asp:TemplateField>
        <ItemTemplate>
          <asp:HyperLink runat="server" ID="hlPlayerProfileLink" Target="_blank" CssClass="profile">
            <asp:Image runat="server" ImageUrl="~/Images/GridViewButtons/nfldotcom.gif" />
          </asp:HyperLink>
        </ItemTemplate>
      </asp:TemplateField>
      <%--Retire--%>
      <asp:TemplateField>
        <ItemTemplate>

          <asp:LinkButton runat="server" ID="ibRetire" Text="Retire"       
        CommandArgument='<%# Eval("PlayerID") %>' OnCommand="ibRetire_Command" ToolTip="Click to indicate that this person is retired."/>
        </ItemTemplate>
      </asp:TemplateField>

    </Columns>
  </asp:GridView>

</asp:Content>

