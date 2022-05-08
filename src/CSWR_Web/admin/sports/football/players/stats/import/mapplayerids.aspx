<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="mapplayerids.aspx.cs" 
    Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.MapPlayerIDs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

  <div class="mapPlayerIDsPage">

  <h2>Map PlayerIDs</h2>

  <p style="color:#666;font-size:12px;">
    Only the <strong>StatMap w/o Players</strong> link and the <strong>Load StatMapIDs</strong> button are tied to the weekly spreadsheet.
  </p>
  <p>
    Before adding an <em>unmapped</em> player to the database, search current players to ensure they don't exist under a slightly different name
  </p>
  <br />

  <%--Season--%>
  Season: <asp:DropDownList runat="server" ID="ddlSeasons" DataTextField="SeasonCode" 
    DataValueField="SeasonCode" AutoPostBack="true" ondatabound="ddlSeasons_DataBound" />



  <%--Week--%>
  Week: <asp:DropDownList runat="server" ID="ddlWeek" AutoPostBack="true" onselectedindexchanged="ddlWeek_SelectedIndexChanged"></asp:DropDownList>
    <asp:Label runat="server" ID="labErrorMessage" Text="File not Found" style="color:Red;" />


  <br />
  <br />

  <%--Navigation--%>
    <ul>
      <li title="All Players lists all players who aren't retired and whose first year was before the current year.">
        <%--Season Players--%>
        <asp:LinkButton runat="server" ID="lbSeasonPlayers" Text="All Players" onclick="lbSeasonPlayers_Click" CommandName="AllPlayers"/>
        <asp:Label runat="server" ID="labSeasonPlayers" Text="All Players" Visible="false" CssClass="bold"/>
      </li>
      <li title="This list contains all players who are considered active but for whom no StatMapID is provided.  These players should probably be retired.">
        <%--Players without StatMapID--%>
        <asp:LinkButton runat="server" ID="lbNoStatMapID" CommandName="NoStatMapID" Text="Players w/o StatMapID" onclick="lbNoStatMapID_Click" />
        <asp:Label runat="server" ID="labNoStatMapID" Text="Players w/o StatMapID" Visible="false" CssClass="bold"/>
      </li>
      <li title="This list contains all spreadsheet players whose StatMapID cannot be found in the database.  This means that the player doesn't exist in the database, or they do but don't have a StatMapId">
        <%--StatMaps without Players--%>
        <asp:LinkButton runat="server" ID="lbStatMapNoPlayer" Text="StatMaps w/o Players" CommandName="NoPlayer" onclick="lbStatMapNoPlayer_Click"/>
        <asp:Label runat="server" ID="labStatMapNoPlayer" Text="StatMaps w/o Players" Visible="false" CssClass="bold"/>
        <asp:ImageButton runat="server" ID="ibRefreshPage" ImageUrl="~/Images/Icons/refresh.png" OnClick="lbStatMapNoPlayer_Click" /> 
      </li>
    </ul>


  <br />

  <%--Load StatMapIDs--%>
  <asp:Button runat="server" ID="butLoadIDs" Text="Load StatMapIDs" OnClick="butLoadStatMapIDs_Click"
    ToolTip="This button will spin through the spreadsheet players and, if their StatMapID isn't found, it will attempt to map them to the database using first/last name match." />

  <%--Clear all Stat Map IDs--%>
  <asp:Button runat="server" ID="butClearStatMapIDs" Text="Clear StatMapIDs" onclick="butClearStatMapIDs_Click" Enabled="false" />

  <asp:Label runat="server" ID="labTotalPlayers" /> Players
  <br />
  <asp:Label runat="server" ID="labMappedPlayers" />


  <br /><br />

  <%--Player Grid--%>

  <asp:GridView runat="server" ID="gvPlayers" AutoGenerateColumns="false" onrowcancelingedit="gvPlayers_RowCancelingEdit" 
    DataKeyNames="PlayerID" onrowcommand="gvPlayers_RowCommand" onrowcreated="gvPlayers_RowCreated" 
    onrowediting="gvPlayers_RowEditing" onrowupdating="gvPlayers_RowUpdating" onrowdatabound="gvPlayers_RowDataBound">
    <Columns>
      <%--Full Name--%>
      <asp:BoundField DataField="FullNameLastFirst" HeaderText="Name" ReadOnly="true" />
      <%--Position--%> 
      <asp:BoundField DataField="PositionCode" HeaderText="Position" ReadOnly="true" />
      <%--PlayerStatMapID--%>
      <asp:TemplateField HeaderText="StatMapID">
        <ItemTemplate>
          <asp:Label runat="server" ID="labStatMapID" />
        </ItemTemplate>      
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="tbStatMapID" Width="100" />
        </EditItemTemplate>
      </asp:TemplateField>
      <%--Actions--%>
      <asp:TemplateField>
        <ItemTemplate>
          <asp:ImageButton runat="server" ID="ibUpdate" CommandName="Update" ToolTip="Click to update this player's statmapid." ImageUrl="~/images/gridviewbuttons/Update.GIF" />
          <asp:ImageButton runat="server" ID="ibCancel" CommandName="Cancel" ToolTip="Click to cancel." ImageUrl="~/images/gridviewbuttons/Cancel.GIF" />
          <asp:ImageButton runat="server" ID="ibEdit" CommandName="Edit" ToolTip="Click to edit this player's statmapid." ImageUrl="~/images/gridviewbuttons/Edit.GIF" />
        </ItemTemplate>
      </asp:TemplateField>
    </Columns>
  </asp:GridView>
  
  <%--StatMapIds Without Players--%>
  <asp:Repeater runat="server" ID="repStatMapIdsWithoutPlayers" 
      onitemdatabound="repStatMapIdsWithoutPlayers_ItemDataBound">
    <ItemTemplate>
      <p>
        <asp:Label runat="server" ID="labCounter" />
        <asp:Label runat="server" ID="labFullNameLastFirst" />
        <asp:Label runat="server" ID="labTeamAbbreviation" />
        <asp:Label runat="server" ID="labPosition" />
        <asp:Label runat="server" ID="labStatMapID" />
        <%--Link to automatically search for this player through Google "I feel lucky"--%>
        <asp:HyperLink runat="server" ID="hlPlayerProfileLink" Target="_blank" CssClass="profile">
          <asp:Image runat="server" ImageUrl="~/Images/GridViewButtons/nfldotcom.gif" />
        </asp:HyperLink>
          <asp:HyperLink runat="server" ID="hlTwitterProfileLink" Target="_blank" CssClass="profile">
              <asp:Image runat="server" ImageUrl="~/Images/GridViewButtons/Twitter.gif" />
          </asp:HyperLink>

      </p>
    </ItemTemplate>
  </asp:Repeater>
  
  </div>


</asp:Content>

