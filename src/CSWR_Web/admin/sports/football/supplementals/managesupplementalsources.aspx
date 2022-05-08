<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="managesupplementalsources.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.ManageSupplementalSources" Title="Manage Supplemental Sources - Cheat Sheet War Room" %>
<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<%@ Register Src="~/usercontrols/FileUploader.ascx" TagName="FileUploader" TagPrefix="cswr" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphAdminContent" Runat="Server">

<div class="manageSupplementalSourcesPage">

  <%--Supplemental Source Grid--%>
  <asp:GridView ID="gvSupplementalSources" runat="server" AutoGenerateColumns="False" DataSourceID="odsSupplementalSources" AllowPaging="True" 
    DataKeyNames="SupplementalSourceID" OnRowCreated="gvSupplementalSources_RowCreated" OnRowDeleted="gvSupplementalSources_RowDeleted" AllowSorting="true" 
    OnSelectedIndexChanged="gvSupplementalSources_SelectedIndexChanged" SkinID="Professional">
    <Columns>
      <asp:ImageField DataImageUrlField="ImageURL" HeaderText="Logo" />
      <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
      <asp:BoundField DataField="Abbreviation" HeaderText="Abbreviation" SortExpression="Abbreviation" />
      <%--<asp:BoundField DataField="Url" HeaderText="Url" SortExpression="Url"  />--%>
      <%--Edit Source--%>
      <asp:CommandField ButtonType="image" SelectText="Click to edit this source." SelectImageUrl="~/Images/GridViewButtons/Edit.gif" ShowSelectButton="True" />
      <%--Delete Source--%>
      <asp:CommandField ButtonType="image" DeleteText="Click to delete this source." DeleteImageUrl="~/Images/GridViewButtons/Delete.gif" ShowDeleteButton="True" />
    </Columns>
  </asp:GridView>
  
  <%--Source Details--%>
  <asp:DetailsView runat="server" ID="dvSourceDetails" AutoGenerateRows="False" DataSourceID="odsSupplementalSourceDetails" DataKeyNames="SupplementalSourceID"
    AutoGenerateEditButton="true" AutoGenerateInsertButton="True" HeaderText="Source Details" DefaultMode="Insert" HorizontalAlign="center" SkinID="Professional"
    OnItemCommand="dvSourceDetails_ItemCommand" OnItemInserted="dvSourceDetails_ItemInserted" OnItemUpdated="dvSourceDetails_ItemUpdated" OnDataBound="dvSourceDetails_DataBound">
    <HeaderTemplate>
      <asp:Label runat="server" ID="labHeader"></asp:Label>
    </HeaderTemplate>
    <Fields>
      <%--Name--%>
      <asp:TemplateField HeaderText="Name" >
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="tbName" Text='<%# Bind("Name") %>' MaxLength="64" Width="97%"></asp:TextBox>
          <asp:RequiredFieldValidator runat="server" ID="rfvNameRequired" ControlToValidate="tbName" Display="Dynamic" SetFocusOnError="True" Text="Name field is required"
            Tooltip="The name field is required"></asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>
      <%--Abbreviation--%>
      <asp:TemplateField HeaderText="Abbreviation" >
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="tbAbbreviation" Text='<%# Bind("Abbreviation") %>' MaxLength="4" Width="100"></asp:TextBox>
          <asp:RequiredFieldValidator runat="server" ID="rfvAbbreviationRequired" ControlToValidate="tbAbbreviation" Display="Dynamic" SetFocusOnError="True" Text="Abbreviation field is required"
            Tooltip="The abbreviation field is required"></asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>
      <%--URL--%>
      <asp:TemplateField  HeaderText="Url" >
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="tbUrl" Text='<%# Bind("Url") %>' MaxLength="200" Width="97%"></asp:TextBox>
          <asp:RequiredFieldValidator runat="server" ID="rfvUrlRequired" ControlToValidate="tbUrl" Display="Dynamic" SetFocusOnError="True" Text="Url field is required"
            Tooltip="The Url field is required"></asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>
      <%--ImageURL--%>
      <asp:TemplateField  HeaderText="Image Url" >
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="tbImageUrl" Text='<%# Bind("ImageURL") %>' MaxLength="128" Width="97%"></asp:TextBox>
          <asp:RequiredFieldValidator runat="server" ID="rfvImageUrlRequired" ControlToValidate="tbImageUrl" Display="Dynamic" SetFocusOnError="True" Text="Image Url field is required"
            Tooltip="The Image Url field is required"></asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>
    </Fields>
  </asp:DetailsView>
  
  <%--Used to upload a logo--%>
  <cswr:FileUploader runat="server" ID="fuFileUploader" />
  
  
  
  
  
  <%--Supplemental Source Data--%>
  <asp:ObjectDataSource ID="odsSupplementalSources" runat="server" SelectMethod="GetSupplementalSources" DeleteMethod="DeleteSupplementalSource"
    TypeName="BP.CheatSheetWarRoom.BLL.Sheets.SupplementalSource" SortParameterName="orderBy">
  </asp:ObjectDataSource>

  <%--Supplemental Source Details--%>
  <asp:ObjectDataSource ID="odsSupplementalSourceDetails" runat="server" SelectMethod="GetSupplementalSource"
    TypeName="BP.CheatSheetWarRoom.BLL.Sheets.SupplementalSource" InsertMethod="InsertSupplementalSource" UpdateMethod="UpdateSupplementalSource">
    <SelectParameters>
      <asp:ControlParameter ControlID="gvSupplementalSources" Name="supplementalSourceID"
        PropertyName="SelectedValue" Type="Int32" />
    </SelectParameters>
  </asp:ObjectDataSource>
  
</div>

</asp:Content>

