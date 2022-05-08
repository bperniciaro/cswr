<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FooPlayerManager.ascx.cs" Inherits="BP.CheatSheetWarRoom.UI.UserControls.FooPlayerManager" %>

  <asp:Panel runat="server" ID="panManagePlayers" DefaultButton="butSearch">

      <asp:Button runat="server" ID="butOpenPlayerProfiles" Text="Open Player Profiles" CausesValidation="false" CssClass="submitButton" UseSubmitBehavior="false" />

        <table style="width:650px;">
          <tr>
            <td>
              <!-- Left Side -->
              <table style="width:360px;">
                <%--Retired--%>
                <tr>
                  <td>
                    <asp:Label runat="server" AssociatedControlID="cbRetired" Text="Include Retired" CssClass="bold"></asp:Label>
                  </td>
                  <td>
                    <asp:CheckBox runat="server" ID="cbRetired" AutoPostBack="true" OnCheckedChanged="cbRetired_CheckedChanged"/>
                  </td>
                </tr>
                <%--Season--%>
                <tr runat="server" id="trSeasonRow">
                  <td>
                    <asp:Label runat="server" AssociatedControlID="ddlSportSeasons" Text="Select Season" CssClass="bold"></asp:Label>
                  </td>
                  <td>
                    <asp:DropDownList runat="server" id="ddlSportSeasons" DataTextField="SeasonCode" DataValueField="SeasonCode" AutoPostBack="true" 
                      OnSelectedIndexChanged="ddlSportSeasons_SelectedIndexChanged" OnDataBound="ddlSportSeasons_DataBound"></asp:DropDownList>
                    <span style="font-size:10px;">(when players are relevant)</span>
                  </td>
                </tr>
                <%--Team--%>
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
              <asp:TextBox runat="server" ID="tbLastName" Width="110"/>
              <asp:Button runat="server" ID="butSearch" Text="Search" CausesValidation="false" OnClick="butSearch_Click" />
              <asp:ImageButton runat="server" ID="butClearSearch" ImageUrl="~/Images/GridViewButtons/Delete.gif" OnClick="butClearSearch_Click" />
            </td>
          </tr>
        </table>

        <%--Add Stats--%>
        <div class="buttonContainer" style="margin:7px 0px 5px 2px;">
          <asp:LinkButton runat="server" ID="lbAddPlayer" OnClick="lbAddPlayer_Click">
            <asp:Image runat="server" ImageUrl="~/images/icons/add.gif" />Add Player
          </asp:LinkButton>
          <asp:HyperLink runat="server" ID="hlRequestPlayerAddition" Text="Request Player Addition" NavigateUrl="mailto:admin@cheatsheetwarroom.com?subject=Add Player Request&body=Please add the following player..." />
          <p style="margin:10px 0px 0px 0px;">
            <asp:Label runat="server" ID="labNoStats" Visible="false" Text="No stats configured." />
          </p>
        </div>



        <asp:GridView runat="server" ID="gvPlayers" AutoGenerateColumns="False"
          CssClass="standardGrid" SkinID="Professional" DataKeyNames="PlayerID"  
          onrowdatabound="gvPlayers_RowDataBound" 
          onrowupdating="gvPlayers_RowUpdating" ondatabound="gvPlayers_DataBound" onrowcommand="gvPlayers_RowCommand" 
          onrowcreated="gvPlayers_RowCreated" 
          onrowcancelingedit="gvPlayers_RowCancelingEdit" 
          onrowdeleting="gvPlayers_RowDeleting" onrowediting="gvPlayers_RowEditing">
        <Columns>
          <%--Last Name--%>
          <asp:TemplateField HeaderText="Last">
            <ItemStyle Width="120" />
            <ItemTemplate>
              <asp:Label runat="server" ID="labLastName" />
            </ItemTemplate>
            <EditItemTemplate>
              <asp:Label runat="server" ID="labLastName" />
              <asp:TextBox runat="server" ID="tbLastName" MaxLength="50" Width="100" />
              <asp:RequiredFieldValidator runat="server" ID="rfvLastName" ControlToValidate="tbLastName" 
                ErrorMessage="Last Name is required." ToolTip="Last Name is required.">*</asp:RequiredFieldValidator>
            </EditItemTemplate>
          </asp:TemplateField>   
          <%--First Name--%>
          <asp:TemplateField HeaderText="First">
            <ItemStyle Width="120" />
            <ItemTemplate>
              <asp:Label runat="server" ID="labFirstName" />
            </ItemTemplate>
            <EditItemTemplate>
              <asp:Label runat="server" ID="labFirstName" />
              <asp:TextBox runat="server" ID="tbFirstName" MaxLength="50" Width="100"  />
              <asp:RequiredFieldValidator runat="server" ID="rfvFirstName" ControlToValidate="tbFirstName" 
                ErrorMessage="First Name is required." ToolTip="First Name is required.">*</asp:RequiredFieldValidator>
            </EditItemTemplate>
          </asp:TemplateField>   
          <%--Middle Name--%>
          <asp:TemplateField HeaderText="Middle">
            <ItemStyle Width="50" />
            <ItemTemplate>
              <asp:Label runat="server" ID="labMiddleName" />
            </ItemTemplate>
            <EditItemTemplate>
              <asp:Label runat="server" ID="labMiddleName" />
              <asp:TextBox runat="server" ID="tbMiddleName" MaxLength="75" Width="25" />
            </EditItemTemplate>
          </asp:TemplateField>   
          <%--Team Abbreviation--%>
          <asp:TemplateField HeaderText="Team">
            <ItemStyle Width="65" />
            <ItemTemplate>
              <asp:Label runat="server" ID="labTeamAbbreviation" />
            </ItemTemplate>
            <EditItemTemplate>
              <asp:DropDownList runat="server" ID="ddlTeams" DataValueField="TeamCode" DataTextField="Abbreviation" />
            </EditItemTemplate>
          </asp:TemplateField>   
          <%--Position--%>
          <asp:TemplateField HeaderText="Pos">
            <ItemStyle Width="65" />
            <ItemTemplate>
              <asp:Label runat="server" ID="labPosition" />
            </ItemTemplate>
            <EditItemTemplate>
              <asp:Label runat="server" ID="labPosition" />
              <asp:DropDownList runat="server" ID="ddlPositions" DataValueField="PositionCode" DataTextField="Abbreviation" />
            </EditItemTemplate>
          </asp:TemplateField>   
          <%--StatMapID--%>
          <asp:TemplateField HeaderText="StatMapID">
            <ItemTemplate>
              <asp:Label runat="server" ID="labStatMapID" />
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox runat="server" ID="tbStatMapID" MaxLength="5" Width="60" />
            </EditItemTemplate>
          </asp:TemplateField>   
          <%--Number--%>
          <asp:TemplateField HeaderText="Num">
            <ItemStyle Width="40" />
            <ItemTemplate>
              <asp:Label runat="server" ID="labNumber" />
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox runat="server" ID="tbNumber" MaxLength="2" Width="20" />
              <asp:RequiredFieldValidator runat="server" ControlToValidate="tbNumber" 
                ErrorMessage="Number is required." ToolTip="Number is required.">*</asp:RequiredFieldValidator>
            </EditItemTemplate>
          </asp:TemplateField>   
          <%--Experience--%>
          <asp:TemplateField HeaderText="Exp.">
            <ItemStyle Width="80" />
            <ItemTemplate>
              <asp:Label runat="server" ID="labExperienceInYears" />
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox runat="server" ID="tbExperienceInYears" MaxLength="2" Width="20"/>
              <asp:Label runat="server" ID="labYearsLabel" Text=" years" />
              <asp:RequiredFieldValidator runat="server" ControlToValidate="tbExperienceInYears" 
                ErrorMessage="Experience is required." ToolTip="Experience is required.">*</asp:RequiredFieldValidator>
            </EditItemTemplate>
          </asp:TemplateField>   
          <%--BirthDate--%>
          <asp:TemplateField HeaderText="BirthDate">
            <ItemStyle Width="95" />
            <ItemTemplate>
              <asp:Label runat="server" ID="labBirthDate" />
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox runat="server" ID="tbBirthDate" MaxLength="10" Width="75" />
              <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="tbBirthDate" 
                ErrorMessage="Birth Date is required." ToolTip="Birth Date is required.">*</asp:RequiredFieldValidator>--%>
            </EditItemTemplate>
          </asp:TemplateField>   
          <%--Twitter Username--%>
          <asp:TemplateField HeaderText="Twitter Username">
            <ItemStyle Width="150" />
            <ItemTemplate>
              <asp:Label runat="server" ID="labTwitterUsername" />
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox runat="server" ID="tbTwitterUsername" MaxLength="15" Width="140" />
            </EditItemTemplate>
          </asp:TemplateField>   
          <%--Retired--%>
          <asp:TemplateField HeaderText="Retired">
            <ItemStyle Width="40" />
            <ItemTemplate>
              <asp:Label runat="server" ID="labRetired" />
            </ItemTemplate>
            <EditItemTemplate>
              <asp:CheckBox runat="server" ID="cbRetired" />
            </EditItemTemplate>
          </asp:TemplateField>   
          <%--NFL Profile Link--%>
          <asp:TemplateField>
            <ItemTemplate>
              <asp:HyperLink runat="server" ID="hlPlayerNFLProfileLink" Target="_blank" CssClass="profile">
                <asp:Image runat="server" ImageUrl="~/Images/GridViewButtons/nfldotcom.gif" />
              </asp:HyperLink>
            </ItemTemplate>
          </asp:TemplateField>
          <%--Twitter Profile Link--%>
          <asp:TemplateField>
            <ItemTemplate>
              <asp:HyperLink runat="server" ID="hlPlayerTwitterProfileLink" Target="_blank" CssClass="profile">
                <asp:Image runat="server" ImageUrl="~/Images/GridViewButtons/Twitter.gif" />
              </asp:HyperLink>
            </ItemTemplate>
          </asp:TemplateField>
          <%--Actions--%>       
          <asp:TemplateField>
            <ItemStyle CssClass="nowrap" />
            <ItemTemplate>
              <asp:ImageButton runat="server" id="ibAdd" CommandName="Insert" ToolTip="Click to add this player." ImageUrl="~/images/gridviewbuttons/Add.gif" OnCommand="ibAdd_Command"/>
              <asp:ImageButton runat="server" ID="ibUpdate" CommandName="Update" ToolTip="Click to update this player." ImageUrl="~/images/gridviewbuttons/Update.GIF" CausesValidation="false"/>
              <asp:ImageButton runat="server" ID="ibCancel" CommandName="Cancel" ToolTip="Click to cancel." ImageUrl="~/images/gridviewbuttons/Cancel.GIF" CausesValidation="false" />
              <asp:ImageButton runat="server" ID="ibEdit" CommandName="Edit" ToolTip="Click to edit this player." ImageUrl="~/images/gridviewbuttons/Edit.GIF" />
              <asp:ImageButton runat="server" id="ibDelete" CommandName="Delete" ToolTip="Click to delete this player." ImageUrl="~/images/gridviewbuttons/Delete.gif" OnClientClick="return window.confirm('Are you sure you want to delete this player?');" />
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
          <strong>No players found</strong>
        </EmptyDataTemplate>
      </asp:GridView>

    </asp:Panel>

  <%--<cswr:PlayerManager runat="server" ID="pmPlayerManager" />--%>

  <script type="text/javascript">
    $(document).ready(function () {
      $('input.submitButton').click(function () {
        $('a.profile').each(function () {
          window.open($(this).attr('href'));
        });
      });
    });
  </script>

