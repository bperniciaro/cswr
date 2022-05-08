<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SuppSheetManager.ascx.cs" Inherits="BP.CheatSheetWarRoom.UI.UserControls.SuppSheetManager" %>



<div class="manageSupplementalSheetsPage">

  Season  <asp:DropDownList runat="server" ID="ddlSportSeason" DataTextField="SeasonCode" DataValueField="SeasonCode" 
    onselectedindexchanged="ddlSportSeason_SelectedIndexChanged" AutoPostBack="true"/>
  <br />
  <br />

  <%--Supplemental Sheets--%>
  <asp:GridView runat="server" ID="gvSupplementalSheets" AllowPaging="True" 
    AllowSorting="True" AutoGenerateColumns="False" DataSourceID="odsSupplementalSheets" 
    SkinID="Professional" DataKeyNames="SupplementalSheetID" CssClass="standardGrid"  
    OnRowCreated="gvSupplementalSheets_RowCreated"  OnRowDeleted="gvSupplementalSheets_RowDeleted" 
    OnSelectedIndexChanged="gvSupplementalSheets_SelectedIndexChanged" 
    PageSize="40" onrowdatabound="gvSupplementalSheets_RowDataBound" 
    ondatabound="gvSupplementalSheets_DataBound">
    <Columns>
      <%--Season--%>
      <asp:BoundField DataField="SeasonCode" HeaderText="Season" SortExpression="SeasonCode" />
      <%--Sport Name--%>
      <asp:TemplateField HeaderText="Sport" SortExpression="SportCode">
        <ItemTemplate>
          <asp:Label runat="server" ID="labSportName" />
        </ItemTemplate>
      </asp:TemplateField>
      <%--Supp Source Name--%>
      <asp:TemplateField HeaderText="Source" SortExpression="SuppSource">
        <ItemTemplate>
          <asp:Label runat="server" ID="labSuppSourceName" />
        </ItemTemplate>
      </asp:TemplateField>
      
      <%--<asp:BoundField DataField="SportCode" HeaderText="Sport" SortExpression="SportCode" />--%>
      <%--Source--%>
      <%--<asp:BoundField DataField="SuppSource" HeaderText="Source" SortExpression="SuppSource" />--%>
      <%--Position--%>      
      <asp:BoundField DataField="PositionCode" HeaderText="Position" SortExpression="PositionCode" />
      <%--Link--%>
      <asp:HyperLinkField  DataNavigateUrlFields="Url" Text="Sheet" HeaderText="Sheet URL" Target="_blank"/>
      <%--Update Timestamp--%>
      <asp:BoundField DataField="LastUpdated" HeaderText="Updated" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False"/>
      <%--Config Button--%>
      <asp:CommandField ButtonType="Image" SelectText="Click to edit this sheet." SelectImageUrl="~/Images/GridViewButtons/ConfigSheet.gif" ShowSelectButton="True"/>
      <%--Edit Button--%>
      <asp:TemplateField>
        <ItemTemplate>
          <asp:HyperLink runat="server" ID="hlEditSheet">
            <asp:Image runat="server" ImageUrl="~/Images/GridViewButtons/Edit.gif" />
          </asp:HyperLink>
        </ItemTemplate>
      </asp:TemplateField>
      <%--Link to 'Rank' Page--%>
      <asp:TemplateField>
        <ItemTemplate>
          <asp:HyperLink runat="server" ID="hlRankPage">
            <asp:Image runat="server" ImageUrl="~/Images/GridViewButtons/Sheet.gif" />
          </asp:HyperLink>
        </ItemTemplate>
      </asp:TemplateField>
      <%--Link to 'Validate' Page--%>
      <asp:TemplateField>
        <ItemTemplate>
          <asp:HyperLink runat="server" ID="hlValidateSheet">
            <asp:Image runat="server" ImageUrl="~/Images/GridViewButtons/Validate.gif" />
          </asp:HyperLink>
        </ItemTemplate>
      </asp:TemplateField>
      <%--Delete Button--%>
      <asp:CommandField ButtonType="Image" DeleteText="Click to delete this sheet." DeleteImageUrl="~/Images/GridViewButtons/Delete.gif" ShowDeleteButton="True" />
      
      <%--<asp:HyperLinkField Text="&lt;img src='../../../../Images/GridViewButtons/Sheet.gif' alt='Go To Supplemental Sheet'&gt;" DataNavigateUrlFormatString="RankSupplementalPlayers.aspx?ID={0}" DataNavigateUrlFields="SupplementalSheetID"/>--%>
    </Columns>
    <EmptyDataTemplate>
      There are no supplemental sheets to display.
    </EmptyDataTemplate>
  </asp:GridView>
  
  <br />
  
  <%--Sheet Details--%>
  <asp:DetailsView runat="server" ID="dvSheetDetails" AutoGenerateRows="False" DataSourceID="odsSupplementalSheet" DataKeyNames="SupplementalSheetID" DefaultMode="Insert"
    HeaderText="Sheet Details" SkinID="Professional" OnItemCommand="dvSheetDetails_ItemCommand" OnItemInserted="dvSheetDetails_ItemInserted" OnItemUpdated="dvSheetDetails_ItemUpdated" 
    OnDataBound="dvSheetDetails_DataBound" AutoGenerateEditButton="True" AutoGenerateInsertButton="True">
    <%--Header Text--%>
    <HeaderTemplate>
      <asp:Label runat="server" ID="labDetailsHeader"></asp:Label>
    </HeaderTemplate>
    <Fields>
      <%--Season--%>
      <asp:TemplateField HeaderText="Season" >
        <EditItemTemplate>
          <asp:DropDownList runat="server" ID="ddlSeasonCode" Enabled="false" DataSourceID="odsSeasons" DataTextField="SeasonCode" DataValueField="SeasonCode"></asp:DropDownList>
        </EditItemTemplate>
        <InsertItemTemplate>
          <asp:DropDownList runat="server" ID="ddlSeasonCode" DataSourceID="odsSeasons" DataTextField="SeasonCode" DataValueField="SeasonCode"></asp:DropDownList>
          <asp:RequiredFieldValidator runat="server" ID="rfvSeasonRequired" ControlToValidate="ddlSeasonCode" SetFocusOnError="True" Text="Source is required"
            Tooltip="The Season is required" InitialValue="0"></asp:RequiredFieldValidator>
        </InsertItemTemplate>
      </asp:TemplateField>
      <%--Sport--%>
      <asp:TemplateField HeaderText="Sport">
        <InsertItemTemplate>
          <asp:Label runat="server" ID="labSport" />
        </InsertItemTemplate>
        <EditItemTemplate>
          <asp:Label runat="server" ID="labSport" />
        </EditItemTemplate>
      </asp:TemplateField>
      <%--Source--%>
      <asp:TemplateField HeaderText="Source" >
        <EditItemTemplate>
          <asp:DropDownList runat="server" ID="ddlSourceCode" DataSourceID="odsSources" DataTextField="Name" DataValueField="SupplementalSourceID"></asp:DropDownList>
          <asp:RequiredFieldValidator runat="server" ID="rfvSourceRequired" ControlToValidate="ddlSourceCode" Display="Dynamic" SetFocusOnError="True" Text="Source is required"
            Tooltip="The Source is required" InitialValue="0"></asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>
      <%--Positions--%>
      <asp:TemplateField HeaderText="Position" >
        <EditItemTemplate>
          <asp:DropDownList runat="server" ID="ddlPositionCode" DataTextField="Abbreviation" DataValueField="PositionCode"></asp:DropDownList>
          <asp:RequiredFieldValidator runat="server" ID="rfvPositionRequired" ControlToValidate="ddlPositionCode" Display="Dynamic" SetFocusOnError="True" Text="Position is required"
            Tooltip="The Position is required" InitialValue="0"></asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>
      <%--URL--%>
      <asp:TemplateField  HeaderText="Sheet Url" >
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="tbUrl" MaxLength="200" Width="97%"></asp:TextBox>
          <asp:RequiredFieldValidator runat="server" ID="rfvUrlRequired" ControlToValidate="tbUrl" Display="Dynamic" SetFocusOnError="True" Text="Url field is required"
            Tooltip="The Url field is required"></asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>
      <%--Update Timestamp--%>
      <asp:TemplateField HeaderText="Updated">
        <EditItemTemplate>
          <asp:Calendar ID="calLastUpdated" runat="server" TodayDayStyle-BackColor="Gray" SelectedDayStyle-BackColor="Crimson"></asp:Calendar>  
        </EditItemTemplate>
      </asp:TemplateField>
      <%--Instructions--%>
      <asp:TemplateField>
        <EditItemTemplate>
          <div class="instructions">
            Upon creation, sheets are initially sorted using 'total fantasy points'.
          </div>
        </EditItemTemplate>
      </asp:TemplateField>
    </Fields>
  </asp:DetailsView>
  
  <%--Seasons--%>
  <asp:ObjectDataSource runat="server" ID="odsSeasons" SelectMethod="GetSportSeasons" 
    TypeName="BP.CheatSheetWarRoom.BLL.Sheets.SportSeason" onselecting="odsSeasons_Selecting">
    <SelectParameters>
      <asp:ControlParameter ControlID="ddlSportSeason" Name="sportCode" 
        PropertyName="SelectedValue" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  
  <%--Positions--%>
  <%--<asp:ObjectDataSource runat="server" ID="odsPositions" SelectMethod="GetPositions" TypeName="BP.CheatSheetWarRoom.BLL.Sheets.Position" 
    onselecting="odsPositions_Selecting">
    <SelectParameters>
      <asp:ControlParameter ControlID="ddlSportSeason" Name="sportCode" 
        PropertyName="SelectedValue" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>--%>
  
  <%--Sports--%>
  <asp:ObjectDataSource runat="server" ID="odsSports" SelectMethod="GetSports" TypeName="BP.CheatSheetWarRoom.BLL.Sheets.Sport"></asp:ObjectDataSource>
  
  <%--Supplemental Sources--%>
  <asp:ObjectDataSource runat="server" ID="odsSources" SelectMethod="GetSupplementalSources" 
    TypeName="BP.CheatSheetWarRoom.BLL.Sheets.SupplementalSource"></asp:ObjectDataSource>
  
  <%--Supplemental Sheets--%>  
  <asp:ObjectDataSource ID="odsSupplementalSheets" runat="server" 
    SelectMethod="GetSupplementalSheets" SortParameterName="orderBy"
    TypeName="BP.CheatSheetWarRoom.BLL.Sheets.SupplementalSheet" 
    DeleteMethod="DeleteSupplementalSheet" 
    onselecting="odsSupplementalSheets_Selecting">
    <DeleteParameters>
      <asp:Parameter Name="supplementalSheetID" Type="Int32" />
    </DeleteParameters>
    <SelectParameters>
      <asp:ControlParameter ControlID="ddlSportSeason" Name="seasonCode" 
        PropertyName="SelectedValue" Type="String" />
      <asp:Parameter DefaultValue="FOO" Name="sportCode" Type="String" />
      <asp:Parameter Name="orderBy" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  
  <%--Supplemental Sheet--%>
  <asp:ObjectDataSource ID="odsSupplementalSheet" runat="server" SelectMethod="GetSupplementalSheet" 
    TypeName="BP.CheatSheetWarRoom.BLL.Sheets.SupplementalSheet" 
    UpdateMethod="UpdateSupplementalSheet" 
    InsertMethod="CreateSupplementalSheet" 
    oninserting="odsSupplementalSheet_Inserting" 
    onupdating="odsSupplementalSheet_Updating">
    <SelectParameters>
      <asp:ControlParameter ControlID="gvSupplementalSheets" Name="supplementalSheetID"
        PropertyName="SelectedValue" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
      <asp:Parameter Name="supplementalSheetID" Type="Int32" />
      <asp:Parameter Name="seasonCode" Type="String" />
      <asp:Parameter Name="supplementalSourceID" Type="Int32" />
      <asp:Parameter Name="sportCode" Type="String" />
      <asp:Parameter Name="positionCode" Type="String" />
      <asp:Parameter Name="lastUpdated" Type="DateTime" />
      <asp:Parameter Name="url" Type="String" />
    </UpdateParameters>
    <InsertParameters>
      <asp:Parameter Name="supplementalSourceID" Type="Int32" />
      <asp:Parameter Name="seasonCode" Type="String" />
      <asp:Parameter Name="sportCode" Type="String" />
      <asp:Parameter Name="positionCode" Type="String" />
      <asp:Parameter Name="lastUpdated" Type="DateTime" />
      <asp:Parameter Name="url" Type="String" />
    </InsertParameters>
  </asp:ObjectDataSource>
  
  


  </div>
