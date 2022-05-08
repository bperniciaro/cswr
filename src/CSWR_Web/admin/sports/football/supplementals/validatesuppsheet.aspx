<%@ Page Title="Validate Supplemental Sheet" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="validatesuppsheet.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.ValidateSuppSheet" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

<asp:ScriptManager runat="server" />

<div class="validateSuppSheetPage">

<%--Put the entire form in an update panel--%>
<asp:UpdateProgress ID="upUpdateProgress" AssociatedUpdatePanelID="upValidationContainer" runat="server" DynamicLayout="false" DisplayAfter="0">
  <ProgressTemplate>      
    <div id="ajaxLoaderOverlay" class="ajaxOverlay" style="width:575px;margin-top:280px;">
      <asp:Image runat="server" ImageUrl="~/Images/Animations/googlerotate.gif"/>
    </div>      
  </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel runat="server" ID="upValidationContainer">
  <ContentTemplate>

    <%--Left Container--%>
    <div class="lContainer ajaxFormContainer">

      <cswr:MessageBox runat="server" ID="mbStatus" />

      <strong>Position:</strong>
      <asp:DropDownList runat="server" ID="ddlPosition" OnSelectedIndexChanged="ddlPosition_SelectedIndexChanged" AutoPostBack="true">
      </asp:DropDownList>

      <p>
        Identify players whose rank differentiates from CSWR by at least
        <asp:DropDownList runat="server" id="ddlDifferential" AutoPostBack="true" 
          onselectedindexchanged="ddlDifferential_SelectedIndexChanged" />. 
      </p>


      <asp:Panel runat="server" id="panNoSuggestions" Visible="false">
        Your sheet has been completely validated and we have no further suggestions.
      </asp:Panel>

      <h3 runat="server" id="headerRankedTooHigh">
        Ranked Too High
        <asp:Image runat="server" ImageUrl="~/Images/Layout/validation/rankedtoohigh-big.gif" />
      </h3>

      <asp:GridView runat="server" ID="gvRankedTooHigh" AutoGenerateColumns="false" SkinID="RankedTooHigh"
          onrowdatabound="gvRankedTooHigh_RowDataBound" CssClass="standardGrid"
        onrowcommand="gvRankedTooHigh_RowCommand">
        <Columns>
          <%--Name--%>
          <asp:TemplateField HeaderText="Name" ControlStyle-Width="175">
            <ItemTemplate>
              <asp:Label runat="server" ID="labFullNameLastFirst" />        
            </ItemTemplate>
          </asp:TemplateField>
          <%--Team--%>
          <asp:TemplateField HeaderText="Team">
            <ItemTemplate>
              <asp:Label runat="server" ID="labTeamAbbreviation" />        
            </ItemTemplate>
          </asp:TemplateField>
          <%--CSWR Rank--%>
          <asp:TemplateField HeaderText="CSWR Rank" HeaderStyle-CssClass="shortHeaderWidth">
            <ItemTemplate>
              <asp:Label runat="server" ID="labCSWRRank" />        
            </ItemTemplate>
          </asp:TemplateField>
          <%--CBS Rank--%>
          <asp:TemplateField HeaderText="CBS Rank" HeaderStyle-CssClass="shortHeaderWidth">
            <ItemTemplate>
              <asp:Label runat="server" ID="labCBSRank" />        
            </ItemTemplate>
          </asp:TemplateField>
          <%--DIFF--%>
          <asp:TemplateField HeaderText="Rank DIFF" HeaderStyle-CssClass="shortHeaderWidth">
            <ItemTemplate>
              <asp:Label runat="server" ID="labRankDiff" />        
            </ItemTemplate>
          </asp:TemplateField>
          <%--Demote Player--%>
          <asp:TemplateField HeaderText="Demote Player">
            <ItemTemplate>
              <asp:Button runat="server" ID="butDemotePlayer" CommandName="DemotePlayer"/>
            </ItemTemplate>
          </asp:TemplateField>
          <%--Research Player--%>
          <asp:TemplateField HeaderText="Res">
            <ItemTemplate>
              <asp:HyperLink runat="server" ID="hlResearchPlayer" CssClass="googleNews"  Target="_blank">
              </asp:HyperLink>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>



      <h3 runat="server" id="headerRankedTooLow">
        Ranked Too Low
        <asp:Image runat="server" ImageUrl="~/Images/Layout/validation/rankedtoolow-big.gif" />
      </h3>

      <asp:GridView runat="server" ID="gvRankedTooLow" AutoGenerateColumns="false" SkinID="RankedTooLow"
          onrowdatabound="gvRankedTooLow_RowDataBound" CssClass="standardGrid"
        onrowcommand="gvRankedTooLow_RowCommand">
        <Columns>
          <%--Name--%>
          <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
              <asp:Label runat="server" ID="labFullNameLastFirst"  ControlStyle-Width="175"/>        
            </ItemTemplate>
          </asp:TemplateField>
          <%--Team--%>
          <asp:TemplateField HeaderText="Team">
            <ItemTemplate>
              <asp:Label runat="server" ID="labTeamAbbreviation" />        
            </ItemTemplate>
          </asp:TemplateField>
          <%--CSWR Rank--%>
          <asp:TemplateField HeaderText="CSWR Rank" HeaderStyle-CssClass="shortHeaderWidth">
            <ItemTemplate>
              <asp:Label runat="server" ID="labCSWRRank" />        
            </ItemTemplate>
          </asp:TemplateField>
          <%--CBS Rank--%>
          <asp:TemplateField HeaderText="CBS Rank" HeaderStyle-CssClass="shortHeaderWidth">
            <ItemTemplate>
              <asp:Label runat="server" ID="labCBSRank" />        
            </ItemTemplate>
          </asp:TemplateField>
          <%--DIFF--%>
          <asp:TemplateField HeaderText="Rank DIFF" HeaderStyle-CssClass="shortHeaderWidth">
            <ItemTemplate>
              <asp:Label runat="server" ID="labRankDiff" />        
            </ItemTemplate>
          </asp:TemplateField>
          <%--Promote Player--%>
          <asp:TemplateField HeaderText="Promote Player">
            <ItemTemplate>
              <asp:Button runat="server" ID="butPromotePlayer" CommandName="PromotePlayer"/>
            </ItemTemplate>
          </asp:TemplateField>
          <%--Research Player--%>
          <asp:TemplateField HeaderText="Res">
            <ItemTemplate>
              <asp:HyperLink runat="server" ID="hlResearchPlayer" CssClass="googleNews"  Target="_blank">
              </asp:HyperLink>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>


      <h3 runat="server" id="headerPlayersToAdd">
        Consider Adding
        <asp:Image runat="server" ImageUrl="~/Images/Layout/validation/addplayer.gif" />
      </h3>
      
      <asp:GridView runat="server" ID="gvPlayersToAdd" AutoGenerateColumns="false" SkinID="ConsiderAdding"
        OnRowDataBound="gvPlayersToAdd_RowDataBound" DataKeyNames="PlayerID" CssClass="standardGrid"
        onrowcommand="gvPlayersToAdd_RowCommand">
        <Columns>
          <%--Name--%>
          <asp:TemplateField HeaderText="Name" ControlStyle-Width="175">
            <ItemTemplate>
              <asp:Label runat="server" ID="labFullNameLastFirst" />
            </ItemTemplate>
          </asp:TemplateField>
          <%--Team--%>
          <asp:TemplateField HeaderText="Team">
            <ItemTemplate>
              <asp:Label runat="server" ID="labTeamAbbreviation" />
            </ItemTemplate>
          </asp:TemplateField>
          <%--CBS Rank--%>
          <asp:TemplateField HeaderText="CBS Rank" HeaderStyle-CssClass="shortHeaderWidth">
            <ItemTemplate>
              <asp:Label runat="server" ID="labCBSRank" />
            </ItemTemplate>
          </asp:TemplateField>
          <%--Add Player--%>
          <asp:TemplateField HeaderText="Add Player">
            <ItemTemplate>
              <asp:Button runat="server" ID="butInsertPlayer" CommandName="InsertPlayer" CommandArgument="2"/>
              <asp:Button runat="server" ID="butAddPlayer" CommandName="AddPlayer" Text="Add to End of Sheet" CommandArgument="1" ToolTip="Click to add this player to the end of your sheet."/>
            </ItemTemplate>
          </asp:TemplateField>
          <%--Research Player--%>
          <asp:TemplateField HeaderText="Res">
            <ItemTemplate>
              <asp:HyperLink runat="server" ID="hlResearchPlayer" CssClass="googleNews"  Target="_blank">
              </asp:HyperLink>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>


      <h3 runat="server" id="headerPlayersToRemove">
        Consider Removing
        <asp:Image runat="server" ImageUrl="~/Images/Layout/validation/considerremoving_big.gif" />
      </h3>
      <asp:GridView runat="server" ID="gvPlayersToRemove" AutoGenerateColumns="false" 
        SkinID="ConsiderRemoving" onrowdatabound="gvPlayersToRemove_RowDataBound" CssClass="standardGrid" 
        onrowcommand="gvPlayersToRemove_RowCommand">
        <Columns>
          <%--Name--%>
          <asp:TemplateField HeaderText="Name" ControlStyle-Width="175">
            <ItemTemplate>
              <asp:Label runat="server" ID="labFullNameLastFirst" />        
            </ItemTemplate>
          </asp:TemplateField>
          <%--Team--%>
          <asp:TemplateField HeaderText="Team">
            <ItemTemplate>
              <asp:Label runat="server" ID="labTeamAbbreviation" />        
            </ItemTemplate>
          </asp:TemplateField>
          <%--CBS Rank--%>
          <asp:TemplateField HeaderText="CSWR Rank" HeaderStyle-CssClass="shortHeaderWidth">
            <ItemTemplate>
              <asp:Label runat="server" ID="labCSWRRank" />        
            </ItemTemplate>
          </asp:TemplateField>
          <%--Remove Player--%>
          <asp:TemplateField HeaderText="Remove Player">
            <ItemTemplate>
              <asp:Button runat="server" ID="butRemovePlayer" CommandName="RemovePlayer" Text="Remove Player"/>
            </ItemTemplate>
          </asp:TemplateField>
          <%--Research Player--%>
          <asp:TemplateField HeaderText="Res">
            <ItemTemplate>
              <asp:HyperLink runat="server" ID="hlResearchPlayer" CssClass="googleNews"  Target="_blank">
              </asp:HyperLink>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>

    </div>  <!-- close leftcontainer -->

    <%--Right Container--%>
    <div class="rContainer">

      <asp:Repeater runat="server" ID="repSheetPlayers" 
        onitemdatabound="repSheetPlayers_ItemDataBound">
        <ItemTemplate>
          <asp:Panel runat="server" ID="panPlayerContainer" CssClass="playerContainer">
            <asp:Label runat="server" id="labPlayerRank" CssClass="bold" />&nbsp;
            <asp:Label runat="server" id="labPlayerName" /> 
            <asp:Label runat="server" ID="labTeamAbbreviation" /> 
            <asp:Label runat="server" ID="labDiff" CssClass="bold" />
            <%--Ranked Too High--%>
            <asp:Image runat="server" ID="imaRankedTooHigh" ImageUrl="~/Images/Layout/validation/rankedtoohigh-small.gif" Visible="false" />
            <%--Ranked Too Low--%>
            <asp:Image runat="server" ID="imaRankedTooLow" ImageUrl="~/Images/Layout/validation/rankedtoolow-small.gif" Visible="false" />
            <%--Player To Remove--%>
            <asp:Image runat="server" ID="imaPlayerToRemove" ImageUrl="~/Images/Layout/validation/considerremoving_small.gif" Visible="false" />
          </asp:Panel>
        </ItemTemplate>
      </asp:Repeater>

    </div>  <!-- close rightContainer -->

    <div style="clear:both;"/>

        </ContentTemplate>
      </asp:UpdatePanel>



</div>

  <script type="text/javascript">

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_beginRequest(BeginRequestHandler);
    prm.add_endRequest(EndRequestHandler);

    function BeginRequestHandler(sender, args) {
      $('.ajaxFormContainer').addClass("ajaxFormFaded");
    }

    function EndRequestHandler(sender, args) {
      $('.ajaxFormContainer').removeClass("ajaxFormFaded");
    }

</script>

</asp:Content>

