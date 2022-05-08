<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PlayerManager.ascx.cs" Inherits="BP.CheatSheetWarRoom.UI.UserControls.PlayerManager" %>


  <div class="manageFootballPlayersPage">

    <table style="width:650px;">
      <tr>
        <td>
          <!-- Left Side -->
          <table style="width:360px;">
            <%--Select Retired--%>
            <tr>
              <td>
                <asp:Label runat="server" ID="labRetired" AssociatedControlID="cbRetired" Text="Include Retired" CssClass="bold"></asp:Label>
              </td>
              <td>
                <asp:CheckBox runat="server" ID="cbRetired" AutoPostBack="true" 
                  oncheckedchanged="cbRetired_CheckedChanged"/>
              </td>
            </tr>
            <%--Select Season--%>
            <tr>
              <td>
                <asp:Label ID="Label1" runat="server" AssociatedControlID="ddlSportSeasons" Text="Select Season" CssClass="bold"></asp:Label>
              </td>
              <td>
                <asp:DropDownList runat="server" id="ddlSportSeasons" DataTextField="SeasonCode" DataValueField="SeasonCode" AutoPostBack="true" OnSelectedIndexChanged="ddlSportSeasons_SelectedIndexChanged" OnDataBound="ddlSportSeasons_DataBound"></asp:DropDownList>
                <span style="font-size:10px;">(when players are relevant)</span>
              </td>
            </tr>
            <%--Select Team--%>
            <tr runat="server" id="trTeamRow">
              <td>
                <asp:Label runat="server" ID="labSelectTeam" AssociatedControlID="ddlTeams" Text="Select Team" CssClass="bold"></asp:Label>
              </td>
              <td>
                <asp:DropDownList runat="server" ID="ddlTeams" DataTextField="FullTeamName" DataValueField="TeamCode" AutoPostBack="true" OnSelectedIndexChanged="ddlTeams_SelectedIndexChanged"></asp:DropDownList> 
              </td>
            </tr>
          </table>
        </td>
        <td style="vertical-align:bottom;font-size:12px;text-align:right;">
          <strong>Last Name</strong>
          <asp:TextBox runat="server" ID="tbLastName" Width="110" />
          <asp:Button runat="server" ID="butSearch" Text="Search" CausesValidation="false" 
            onclick="butSearch_Click"/>
        </td>
      </tr>
    </table>
  
    <table>
      <tr>
        <%--Grid--%>
        <td style="vertical-align:top;width:750px;">

          <asp:Label runat="server" ID="labNoPlayersFound" Visible="false" Text="No players found" style="color:Red;" />

          <%--Player Grid--%>
          <asp:GridView runat="server" ID="gvPlayers" SkinID="Professional" 
            AutoGenerateColumns="False" AllowPaging="True" CssClass="standardGrid"
            AllowSorting="True" DataKeyNames="PlayerID"  Width="700"
            OnRowCreated="gvPlayers_RowCreated" OnRowDeleted="gvPlayers_RowDeleted" PageSize="200"
            OnSelectedIndexChanged="gvPlayers_SelectedIndexChanged" 
            OnRowDataBound="gvPlayers_RowDataBound" 
            onrowdeleting="gvPlayers_RowDeleting" ondatabound="gvPlayers_DataBound">
            <Columns>
              <%--LastName--%>
              <asp:BoundField DataField="LastName" HeaderText="Last" SortExpression="LastName"/>
              <%--FirstName--%>
              <asp:BoundField DataField="FirstName" HeaderText="First"/>
              <%--MiddleName--%>
              <asp:BoundField DataField="MiddleName" HeaderText="Middle"/>
              <%--Team--%>
              <asp:BoundField DataField="TeamCode" HeaderText="Team"/>
              <%--Position--%>
              <asp:BoundField DataField="PositionCode" HeaderText="Pos"/>
              <%--Number--%>
              <asp:BoundField DataField="Number" HeaderText="Number"/>
              <%--StatMapID--%>
              <asp:BoundField DataField="StatMapID" HeaderText="StatMapID"/>
              <%--Experience--%>
              <asp:TemplateField HeaderText="Experience">
                <ItemTemplate>
                  <asp:Label runat="server" id="labExperience"></asp:Label>
                </ItemTemplate>
              </asp:TemplateField>
              <%--Retired--%>
              <asp:BoundField DataField="Retired" HeaderText="Retired" />
              <%--Player Profile Link--%>
              <asp:TemplateField>
                <ItemTemplate>
                  <asp:HyperLink runat="server" ID="hlPlayerProfileLink" Target="_blank" CssClass="profile">
                    <asp:Image runat="server" ImageUrl="~/Images/GridViewButtons/nfldotcom.gif" />
                  </asp:HyperLink>
                </ItemTemplate>
              </asp:TemplateField>
              <asp:CommandField ButtonType="Image" SelectText="Click to edit this player." SelectImageUrl="~/Images/GridViewButtons/Edit.gif" ShowSelectButton="True" />
              <asp:CommandField ButtonType="Image" DeleteText="Click to delete this player." DeleteImageUrl="~/Images/GridViewButtons/Delete.gif" ShowDeleteButton="True" />
            </Columns>
          </asp:GridView>
        </td>

        <%--Details--%>
        <td style="vertical-align:top;">


          <%--Player Details--%>
          <asp:DetailsView runat="server" ID="dvPlayerDetails" AutoGenerateRows="False" 
            DataKeyNames="PlayerID" SkinID="Professional" Width="450" 
            AutoGenerateEditButton="True" AutoGenerateInsertButton="True" 
            HeaderText="Source Details" DefaultMode="Insert" OnItemCommand="dvPlayerDetails_ItemCommand" 
            OnItemInserted="dvPlayerDetails_ItemInserted" OnItemUpdated="dvPlayerDetails_ItemUpdated"   
            OnDataBound="dvPlayerDetails_DataBound" OnItemInserting="dvPlayerDetails_ItemInserting" 
            OnItemUpdating="dvPlayerDetails_ItemUpdating" onmodechanging="dvPlayerDetails_ModeChanging">
            <HeaderTemplate>
              <asp:Label runat="server" ID="labDetailsHeader"></asp:Label>
            </HeaderTemplate>
            <Fields>
               <%--First Name--%>
              <asp:TemplateField HeaderText="First" >
                <EditItemTemplate>
                  <asp:TextBox runat="server" ID="tbFirstName" MaxLength="50" Width="150"></asp:TextBox>
                  <asp:RequiredFieldValidator runat="server" ID="rfvFirstNameRequired" ControlToValidate="tbFirstName" Display="Dynamic" SetFocusOnError="True" 
                    Text="First Name field is required" Tooltip="The first name field is required"></asp:RequiredFieldValidator>
                </EditItemTemplate>
              </asp:TemplateField>
              <%--Middle Name--%>
              <asp:TemplateField HeaderText="Middle" >
                <EditItemTemplate>
                  <asp:TextBox runat="server" ID="tbMiddleName" MaxLength="50" Width="150"></asp:TextBox>
                </EditItemTemplate>
              </asp:TemplateField>
              <%--Last Name--%>
              <asp:TemplateField HeaderText="Last" >
                <EditItemTemplate>
                  <asp:TextBox runat="server" ID="tbLastName" MaxLength="50" Width="150"></asp:TextBox>
                  <asp:RequiredFieldValidator runat="server" ID="rfvLastNameRequired" ControlToValidate="tbLastName" Display="Dynamic" SetFocusOnError="True" 
                    Text="Last Name field is required" Tooltip="The last name field is required"></asp:RequiredFieldValidator>
                </EditItemTemplate>
              </asp:TemplateField>
              <%--Team--%>
              <asp:TemplateField HeaderText="Team">
                <EditItemTemplate>
                  <asp:DropDownList runat="server" ID="ddlTeams" DataTextField="FullTeamName" DataValueField="TeamCode"></asp:DropDownList>
                  <asp:RequiredFieldValidator runat="server" ID="rfvTeamRequired" ControlToValidate="ddlTeams" Display="Dynamic" SetFocusOnError="True" Text="Team is required"
                    Tooltip="The Team is required" InitialValue="0"></asp:RequiredFieldValidator>
                </EditItemTemplate>
              </asp:TemplateField>
              <%--Position--%>
              <asp:TemplateField HeaderText="Position">
                <EditItemTemplate>
                  <asp:DropDownList runat="server" ID="ddlPositions" DataTextField="PositionCode" DataValueField="PositionCode"></asp:DropDownList>
                  <asp:RequiredFieldValidator runat="server" ID="rfvPositionRequired" ControlToValidate="ddlPositions" Display="Dynamic" SetFocusOnError="True" Text="Position is required"
                    Tooltip="The Position is required" InitialValue="0"></asp:RequiredFieldValidator>
                </EditItemTemplate>
              </asp:TemplateField>
              <%--Number--%>
              <asp:TemplateField HeaderText="Number" >
                <EditItemTemplate>
                  <asp:TextBox runat="server" ID="tbNumber" MaxLength="3" Width="30"></asp:TextBox>
                  <asp:RequiredFieldValidator runat="server" ID="rfvNumberRequired" ControlToValidate="tbNumber" Display="Dynamic" SetFocusOnError="True" 
                    Text="Number field is required" Tooltip="The number field is required"></asp:RequiredFieldValidator>
                </EditItemTemplate>
              </asp:TemplateField>
              <%--Experience--%>
              <asp:TemplateField HeaderText="Experience" >
                <EditItemTemplate>
                  <asp:TextBox runat="server" ID="tbExperience" MaxLength="2" Width="30"></asp:TextBox> years
                  <asp:RequiredFieldValidator runat="server" ID="rfvExperienceRequired" ControlToValidate="tbExperience" Display="Dynamic" SetFocusOnError="True" 
                  Text="Experience field is required" Tooltip="The experience field is required"></asp:RequiredFieldValidator>
                </EditItemTemplate>
              </asp:TemplateField>
              <%--StatMapID--%>
              <asp:TemplateField HeaderText="StatMapID">
                <EditItemTemplate>
                  <asp:TextBox runat="server" ID="tbStatMapId" MaxLength="32" Width="130"></asp:TextBox>
                </EditItemTemplate>
              </asp:TemplateField>
              <%--Retired--%>
              <asp:TemplateField HeaderText="Retired" >
                <EditItemTemplate>
                  <asp:CheckBox runat="server" ID="cbRetired" Text="Retired" />
                </EditItemTemplate>
              </asp:TemplateField>  
            </Fields>
            <EmptyDataTemplate>
              No players found
            </EmptyDataTemplate>
          </asp:DetailsView>


        </td>
      </tr>

      </table>
    
  </div>
  
