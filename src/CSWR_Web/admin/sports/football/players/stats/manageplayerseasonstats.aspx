<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="manageplayerseasonstats.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.ManagePlayerSeasonStats" Title="Manage Stats - Cheat Sheet War Room" %>
<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

  <h2>Manage Player Season Stats</h2>

  <div class="managePlayerSeasonStatsPage">
    
    <table>
      <%--Select Season--%>
      <tr>
        <td>
          <asp:Label runat="server" ID="labSelectSeason" AssociatedControlID="ddlSeasons" Text="Select Season" CssClass="bold"/>
        </td>
        <td>
          <asp:DropDownList runat="server" ID="ddlSeasons" DataTextField="SeasonCode" 
            DataValueField="SeasonCode" AutoPostBack="true" 
            onselectedindexchanged="ddlSeasons_SelectedIndexChanged"/>
        </td>
      </tr>
      <%--Select Team--%>
      <tr>
        <td>
          <asp:Label runat="server" ID="labSelectTeam" AssociatedControlID="ddlTeams" Text="Select Team" CssClass="bold"/>
        </td>
        <td>
          <asp:DropDownList runat="server" ID="ddlTeams" DataTextField="FullTeamName" 
            DataValueField="TeamCode" AutoPostBack="true" 
            onselectedindexchanged="ddlTeams_SelectedIndexChanged">
          </asp:DropDownList>
        </td>
      </tr>
      <%--Select Position--%>
      <tr>
        <td>
          <asp:Label runat="server" ID="labPositions" AssociatedControlID="repPositions" Text="Select Position" CssClass="bold"/>
        </td>
        <td>
          <asp:Repeater runat="server" ID="repPositions" 
            onitemdatabound="repPositions_ItemDataBound" 
            onitemcommand="repPositions_ItemCommand">
            <HeaderTemplate>
              <ul>
            </HeaderTemplate>
            <ItemTemplate>
              <li>
                <asp:LinkButton runat="server" ID="lbPosition" />
              </li>
            </ItemTemplate>
            <FooterTemplate>
              </ul>
            </FooterTemplate>
          </asp:Repeater>
        </td>
      </tr>
    </table>
    
    <%--Quarterbacks--%>
    <asp:GridView runat="server" ID="gvQuarterbacks" AutoGenerateColumns="false" Visible="false" 
      onrowdatabound="gvQuarterbacks_RowDataBound" DataKeyNames="PlayerID" Width="650" CssClass="standardGrid"
      OnRowCommand="gvQuarterbacks_RowCommand" SkinID="Professional" 
      onrowcreated="gvPosition_RowCreated" 
      onrowcancelingedit="gvQuarterbacks_RowCancelingEdit" 
      onrowediting="gvQuarterbacks_RowEditing" 
      onrowupdating="gvQuarterbacks_RowUpdating">
      <Columns>
        <%--Name--%>
        <asp:BoundField DataField="Name" ReadOnly="true" HeaderText="Name"/>
        <%--GAM--%>
        <asp:TemplateField HeaderText="GAM">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labGAM" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbGAM" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--PAYD--%>
        <asp:TemplateField HeaderText="PAYD">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labPAYD" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbPAYD" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--PATD--%>
        <asp:TemplateField HeaderText="PATD">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labPATD" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbPATD" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--RUYD--%>
        <asp:TemplateField HeaderText="RUYD">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labRUYD" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbRUYD" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--RUTD--%>
        <asp:TemplateField HeaderText="RUTD">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labRUTD" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbRUTD" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--INT--%>
        <asp:TemplateField HeaderText="INT">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labINT" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbINT" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--FUM--%>
        <asp:TemplateField HeaderText="FUM">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labFUM" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbFUM" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--TFP--%>
        <asp:TemplateField HeaderText="TFP">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labTFP" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbTFP" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--TFPR--%>
        <asp:TemplateField HeaderText="TFPR">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labTFPR" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbTFPR" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--FPPG--%>
        <asp:TemplateField HeaderText="FPPG">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labFPPG" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbFPPG" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--FPGR--%>
        <asp:TemplateField HeaderText="FPGR">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labFPGR" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbFPGR" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--Actions--%>
        <asp:TemplateField>
          <ItemTemplate>
            <asp:ImageButton runat="server" ID="ibUpdate" CommandName="Update" ToolTip="Click to update this player's stats." ImageUrl="~/images/gridviewbuttons/Update.GIF" />
            <asp:ImageButton runat="server" ID="ibCancel" CommandName="Cancel" ToolTip="Click to cancel." ImageUrl="~/images/gridviewbuttons/Cancel.GIF" />
            <asp:ImageButton runat="server" ID="ibEdit" CommandName="Edit" ToolTip="Click to edit this player." ImageUrl="~/images/gridviewbuttons/Edit.GIF" />
          </ItemTemplate>
        </asp:TemplateField>
      </Columns>
    </asp:GridView>



    <%--RBs WRs TEs--%>
    <asp:GridView runat="server" ID="gvRBWRTE" AutoGenerateColumns="false" Visible="false" Width="670" CssClass="standardGrid"
      onrowdatabound="gvRBWRTE_RowDataBound" DataKeyNames="PlayerID" OnRowCommand="gvRBWRTE_RowCommand" 
      SkinID="Professional" onrowcreated="gvPosition_RowCreated" onrowcancelingedit="gvRBWRTE_RowCancelingEdit" 
      onrowediting="gvRBWRTE_RowEditing" onrowupdating="gvRBWRTE_RowUpdating">
      <Columns>
        <%--Name--%>
        <asp:BoundField DataField="Name" ReadOnly="true" HeaderText="Name"/>
        <%--GAM--%>
        <asp:TemplateField HeaderText="GAM">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labGAM" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbGAM" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--RUYD--%>
        <asp:TemplateField HeaderText="RUYD">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labRUYD" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbRUYD" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--RUTD--%>
        <asp:TemplateField HeaderText="RUTD">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labRUTD" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbRUTD" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--REYD--%>
        <asp:TemplateField HeaderText="REYD">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labREYD" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbREYD" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--RETD--%>
        <asp:TemplateField HeaderText="RETD">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labRETD" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbRETD" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--FUM--%>
        <asp:TemplateField HeaderText="FUM">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labFUM" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbFUM" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--TFP--%>
        <asp:TemplateField HeaderText="TFP">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labTFP" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbTFP" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--TFPR--%>
        <asp:TemplateField HeaderText="TFPR">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labTFPR" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbTFPR" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--FPPG--%>
        <asp:TemplateField HeaderText="FPPG">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labFPPG" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbFPPG" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--FPGR--%>
        <asp:TemplateField HeaderText="FPGR">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labFPGR" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbFPGR" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>

        <%--TFPP--%>
        <asp:TemplateField HeaderText="TFPP">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labTFPP" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbTFPP" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>

        <%--TPPR--%>
        <asp:TemplateField HeaderText="TPPR">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labTPPR" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbTPPR" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>

        <%--FPGP--%>
        <asp:TemplateField HeaderText="FPGP">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labFPGP" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbFPGP" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>

        <%--FPPR--%>
        <asp:TemplateField HeaderText="FPPR">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labFPPR" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbFPPR" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--Actions--%>
        <asp:TemplateField>
          <ItemTemplate>
            <asp:ImageButton runat="server" ID="ibUpdate" CommandName="Update" ToolTip="Click to update this player's stats." ImageUrl="~/images/gridviewbuttons/Update.GIF" />
            <asp:ImageButton runat="server" ID="ibCancel" CommandName="Cancel" ToolTip="Click to cancel." ImageUrl="~/images/gridviewbuttons/Cancel.GIF" />
            <asp:ImageButton runat="server" ID="ibEdit" CommandName="Edit" ToolTip="Click to edit this player." ImageUrl="~/images/gridviewbuttons/Edit.GIF" />
          </ItemTemplate>
        </asp:TemplateField>
      </Columns>
    </asp:GridView>





    <%--Kickers--%>
    <asp:GridView runat="server" ID="gvKickers" AutoGenerateColumns="false" Visible="false" 
      onrowdatabound="gvKickers_RowDataBound" DataKeyNames="PlayerID"
      OnRowCommand="gvKickers_RowCommand" SkinID="Professional" Width="555" CssClass="standardGrid"
      onrowcreated="gvPosition_RowCreated" 
      onrowcancelingedit="gvKickers_RowCancelingEdit" 
      onrowediting="gvKickers_RowEditing" 
      onrowupdating="gvKickers_RowUpdating">
      <Columns>
        <%--Name--%>
        <asp:BoundField DataField="Name" ReadOnly="true" HeaderText="Name"/>
        <%--GAM--%>
        <asp:TemplateField HeaderText="GAM">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labGAM" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbGAM" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--MAFG--%>
        <asp:TemplateField HeaderText="MAFG">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labMAFG" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbMAFG" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--MIFG--%>
        <asp:TemplateField HeaderText="MIFG">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labMIFG" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbMIFG" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--MAXP--%>
        <asp:TemplateField HeaderText="MAXP">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labMAXP" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbMAXP" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--MIXP--%>
        <asp:TemplateField HeaderText="MIXP">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labMIXP" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbMIXP" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--TFP--%>
        <asp:TemplateField HeaderText="TFP">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labTFP" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbTFP" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--TFPR--%>
        <asp:TemplateField HeaderText="TFPR">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labTFPR" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbTFPR" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--FPPG--%>
        <asp:TemplateField HeaderText="FPPG">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labFPPG" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbFPPG" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--FPGR--%>
        <asp:TemplateField HeaderText="FPGR">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labFPGR" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbFPGR" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--Actions--%>
        <asp:TemplateField>
          <ItemTemplate>
            <asp:ImageButton runat="server" ID="ibUpdate" CommandName="Update" ToolTip="Click to update this player's stats." ImageUrl="~/images/gridviewbuttons/Update.GIF" />
            <asp:ImageButton runat="server" ID="ibCancel" CommandName="Cancel" ToolTip="Click to cancel." ImageUrl="~/images/gridviewbuttons/Cancel.GIF" />
            <asp:ImageButton runat="server" ID="ibEdit" CommandName="Edit" ToolTip="Click to edit this player." ImageUrl="~/images/gridviewbuttons/Edit.GIF" />
          </ItemTemplate>
        </asp:TemplateField>
      </Columns>
    </asp:GridView>





    <%--Defenses--%>
    <asp:GridView runat="server" ID="gvDefenses" AutoGenerateColumns="false" Visible="false" 
      onrowdatabound="gvDefenses_RowDataBound" DataKeyNames="PlayerID" CssClass="standardGrid"
      OnRowCommand="gvDefenses_RowCommand" SkinID="Professional" 
      onrowcreated="gvPosition_RowCreated" 
      onrowcancelingedit="gvDefenses_RowCancelingEdit" Width="700" 
      onrowediting="gvDefenses_RowEditing" 
      onrowupdating="gvDefenses_RowUpdating">
      <Columns>
        <%--Name--%>
        <asp:BoundField DataField="Name" ReadOnly="true" HeaderText="Name"/>
        <%--GAM--%>
        <asp:TemplateField HeaderText="GAM">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labGAM" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbGAM" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--FREC--%>
        <asp:TemplateField HeaderText="FREC">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labFREC" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbFREC" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--INT--%>
        <asp:TemplateField HeaderText="INT">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labINT" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbINT" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--SACK--%>
        <asp:TemplateField HeaderText="SACK">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labSACK" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbSACK" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--DTD--%>
        <asp:TemplateField HeaderText="DTD">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labDTD" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbDTD" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--PA--%>
        <asp:TemplateField HeaderText="PA">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labPA" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbPA" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--TFP--%>
        <asp:TemplateField HeaderText="TFP">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labTFP" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbTFP" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--TFPR--%>
        <asp:TemplateField HeaderText="TFPR">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labTFPR" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbTFPR" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--FPPG--%>
        <asp:TemplateField HeaderText="FPPG">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labFPPG" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbFPPG" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--FPGR--%>
        <asp:TemplateField HeaderText="FPGR">
          <ItemStyle Width="50px" />
          <ItemTemplate>
            <asp:Label runat="server" ID="labFPGR" />
          </ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox runat="server" ID="tbFPGR" Width="35" />
          </EditItemTemplate>
        </asp:TemplateField>
        <%--Actions--%>
        <asp:TemplateField>
          <ItemTemplate>
            <asp:ImageButton runat="server" ID="ibUpdate" CommandName="Update" ToolTip="Click to update this player's stats." ImageUrl="~/images/gridviewbuttons/Update.GIF" />
            <asp:ImageButton runat="server" ID="ibCancel" CommandName="Cancel" ToolTip="Click to cancel." ImageUrl="~/images/gridviewbuttons/Cancel.GIF" />
            <asp:ImageButton runat="server" ID="ibEdit" CommandName="Edit" ToolTip="Click to edit this player." ImageUrl="~/images/gridviewbuttons/Edit.GIF" />
          </ItemTemplate>
        </asp:TemplateField>
      </Columns>
    </asp:GridView>





  </div>

  
  
</asp:Content>

