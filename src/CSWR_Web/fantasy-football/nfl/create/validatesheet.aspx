<%@ Page Title="Validate Sheet" Language="C#" MasterPageFile="~/MasterPages/ResponsiveTwoCol.master" AutoEventWireup="true" CodeFile="validatesheet.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.ValidateCheatSheet" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/navigation/SheetCreationManageLevelNavigation.ascx" TagName="SheetCreationManageLevelNavigation" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

<asp:ScriptManager runat="server" />

<asp:Panel runat="server" CssClass="validateSheetPage">

<cswr:MessageBox runat="server" ID="mbWrongUser" WidthPercentage="50" />


<asp:UpdateProgress ID="upUpdateProgress" AssociatedUpdatePanelID="upValidationContainer" runat="server" DynamicLayout="false" DisplayAfter="0">
  <ProgressTemplate>      
    <div id="ajaxLoaderOverlay" class="ajaxOverlay" style="margin-top:300px;">
      <asp:Image runat="server" ImageUrl="~/Images/Animations/googlerotate.gif"/>
    </div>      
  </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel runat="server" ID="upValidationContainer">
<ContentTemplate>

  <div class="ajaxFormContainer">

    <%--Navigation--%>
    <cswr:SheetCreationManageLevelNavigation runat="server" ID="scmlnNavigation" CurrentStage="EDITSHEET" SportCode="FOO" />

    <asp:Panel runat="server" CssClass="header">

      <%--Sheet--%>
      <p>
        <strong>Validating Sheet:</strong> 
            <asp:HyperLink runat="server" ID="hlSheetName" CssClass="bold" />
      </p>
      <%--Players--%>
      <p>
        <strong>Total Players:</strong> 
            <asp:Label runat="server" ID="labTotalPlayers" />
      </p>
      <%--Configuration--%>
      <p>
        <strong>Display Players:</strong>
            If your rank differentiates from 
            <abbr title="Cheat Sheet War Room">CSWR</abbr> rank
            by 
            <asp:DropDownList runat="server" id="ddlDifferential" AutoPostBack="true" onselectedindexchanged="ddlDifferential_SelectedIndexChanged" />
            or more.          
      </p>
    </asp:Panel>  <!-- close header -->

    <hr />

    <%--Status Message--%>
    <asp:Panel runat="server" ID="panStatus" CssClass="alert alert-success">
      <asp:Label runat="server" ID="labStatus"></asp:Label>
    </asp:Panel>


    <asp:Panel runat="server" CssClass="row">

      <%--Left Column--%>
      <div class="col-sm-8">

          <h3 runat="server" id="headerRankedTooHigh" class="firstHeader">
            <asp:Image runat="server" ImageUrl="~/Images/Layout/validation/rankedtoohigh-big.gif" />
            Ranked Too High
            <asp:Label runat="server" ID="labRankedTooHighCount" CssClass="count"></asp:Label>
          </h3>

          <asp:GridView runat="server" ID="gvRankedTooHigh" AutoGenerateColumns="false" SkinID="RankedTooHigh"
              onrowdatabound="gvRankedTooHigh_RowDataBound" CssClass="standardGrid"
            onrowcommand="gvRankedTooHigh_RowCommand" 
            ondatabound="gvRankedTooHigh_DataBound">
            <Columns>
              <%--Name--%>
              <asp:TemplateField HeaderText="Player" HeaderStyle-CssClass="nameHeader">
                <ItemTemplate>
                  <table style="border-collapse:collapse;">
                    <tr>
                      <td>
                        <asp:Label runat="server" ID="labFullNameLastFirst" />
                        <asp:Label runat="server" ID="labTeamAbbreviation" CssClass="teamAbbreviation" />    
                      </td>
                      <td>
                        <asp:HyperLink runat="server" ID="hlResearchPlayer" CssClass="googleNews"  Target="_blank" />
                      </td>
                    </tr>                  
                  </table>
                </ItemTemplate>
              </asp:TemplateField>
              <%--User Rank--%>
              <asp:TemplateField HeaderText="Your Rank" HeaderStyle-CssClass="rankHeader">
                <ItemTemplate>
                  <asp:Label runat="server" ID="labUserRank" />        
                </ItemTemplate>
              </asp:TemplateField>
              <%--CSWR Rank--%>
              <asp:TemplateField>
                <HeaderStyle CssClass="rankHeader" />
                <HeaderTemplate>
                  <abbr title="Cheat Sheet War Room">CSWR</abbr> Rank
                </HeaderTemplate>
                <ItemTemplate>
                  <asp:Label runat="server" ID="labCSWRRank" />        
                </ItemTemplate>
              </asp:TemplateField>
              <%--DIFF--%>
              <asp:TemplateField HeaderText="Rank DIFF" HeaderStyle-CssClass="rankHeader">
                <ItemTemplate>
                  <asp:Label runat="server" ID="labRankDiff" />        
                </ItemTemplate>
              </asp:TemplateField>
              <%--Demote Player--%>
              <asp:TemplateField HeaderText="Demote" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:Button runat="server" ID="butDemotePlayer" CommandName="DemotePlayer" CssClass="buttonLength"/>
                </ItemTemplate>
              </asp:TemplateField>
            </Columns>
          </asp:GridView>



          <h3 runat="server" id="headerRankedTooLow">
            <asp:Image runat="server" ImageUrl="~/Images/Layout/validation/rankedtoolow-big.gif" />
            Ranked Too Low
            <asp:Label runat="server" ID="labRankedTooLowCount" CssClass="count"></asp:Label>
          </h3>

          <asp:GridView runat="server" ID="gvRankedTooLow" AutoGenerateColumns="false" SkinID="RankedTooLow"
              onrowdatabound="gvRankedTooLow_RowDataBound" CssClass="standardGrid"
            onrowcommand="gvRankedTooLow_RowCommand" 
            ondatabound="gvRankedTooLow_DataBound">
            <Columns>
              <%--Name--%>
              <asp:TemplateField HeaderText="Player" HeaderStyle-CssClass="nameHeader">
                <ItemTemplate>
                  <table style="border-collapse:collapse;">
                    <tr>
                      <td>
                        <asp:Label runat="server" ID="labFullNameLastFirst" />
                        <asp:Label runat="server" ID="labTeamAbbreviation" CssClass="teamAbbreviation" />    
                      </td>
                      <td>
                        <asp:HyperLink runat="server" ID="hlResearchPlayer" CssClass="googleNews"  Target="_blank" />
                      </td>
                    </tr>                  
                  </table>
                </ItemTemplate>
              </asp:TemplateField>
              <%--Your Rank--%>
              <asp:TemplateField HeaderText="Your Rank" HeaderStyle-CssClass="rankHeader">
                <ItemTemplate>
                  <asp:Label runat="server" ID="labUserRank" />        
                </ItemTemplate>
              </asp:TemplateField>
              <%--CSWR Rank--%>
              <asp:TemplateField>
                <HeaderStyle CssClass="rankHeader" />
                <HeaderTemplate>
                  <abbr title="Cheat Sheet War Room">CSWR</abbr> Rank
                </HeaderTemplate>
                <ItemTemplate>
                  <asp:Label runat="server" ID="labCSWRRank" />        
                </ItemTemplate>
              </asp:TemplateField>
              <%--DIFF--%>
              <asp:TemplateField HeaderText="Rank DIFF" HeaderStyle-CssClass="rankHeader">
                <ItemTemplate>
                  <asp:Label runat="server" ID="labRankDiff" />        
                </ItemTemplate>
              </asp:TemplateField>
              <%--Promote Player--%>
              <asp:TemplateField HeaderText="Promote" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:Button runat="server" ID="butPromotePlayer" CommandName="PromotePlayer" CssClass="buttonLength"/>
                </ItemTemplate>
              </asp:TemplateField>
            </Columns>
          </asp:GridView>


          <h3 runat="server" id="headerPlayersToAdd">
            <asp:Image runat="server" ImageUrl="~/Images/Layout/validation/addplayer.gif" />
            Consider Adding
            <asp:Label runat="server" ID="labPlayersToAddCount" CssClass="count"></asp:Label>
          </h3>
      
          <asp:GridView runat="server" ID="gvPlayersToAdd" AutoGenerateColumns="false" SkinID="ConsiderAdding"
            OnRowDataBound="gvPlayersToAdd_RowDataBound" DataKeyNames="PlayerID" CssClass="standardGrid"
            onrowcommand="gvPlayersToAdd_RowCommand" ondatabound="gvPlayersToAdd_DataBound">
            <Columns>
              <%--Name--%>
              <asp:TemplateField HeaderText="Player" HeaderStyle-CssClass="nameHeader">
                <ItemTemplate>
                  <table style="border-collapse:collapse;">
                    <tr>
                      <td>
                        <asp:Label runat="server" ID="labFullNameLastFirst" />
                        <asp:Label runat="server" ID="labTeamAbbreviation" CssClass="teamAbbreviation" />    
                      </td>
                      <td>
                        <asp:HyperLink runat="server" ID="hlResearchPlayer" CssClass="googleNews"  Target="_blank" />
                      </td>
                    </tr>                  
                  </table>
                </ItemTemplate>
              </asp:TemplateField>
              <%--CSWR Rank--%>
              <asp:TemplateField>
                <HeaderStyle CssClass="rankHeader" />
                <HeaderTemplate>
                  <abbr title="Cheat Sheet War Room">CSWR</abbr> Rank
                </HeaderTemplate>
                <ItemTemplate>
                  <asp:Label runat="server" ID="labCSWRRank" />
                </ItemTemplate>
              </asp:TemplateField>
              <%--Add Player--%>
              <asp:TemplateField HeaderText="Add Player">
                <ItemTemplate>
                  <asp:Button runat="server" ID="butInsertPlayer" CommandName="InsertPlayer" CssClass="buttonLength"/>
                  <asp:Button runat="server" ID="butAddPlayer" CommandName="AddPlayer" CssClass="buttonLength" Text="Add to End" ToolTip="Click to add this player to the end of your sheet."/>
                </ItemTemplate>
              </asp:TemplateField>
            </Columns>
          </asp:GridView>


          <h3 runat="server" id="headerPlayersToRemove">
            <asp:Image runat="server" ImageUrl="~/Images/Layout/validation/considerremoving_big.gif" />
            Consider Removing
            <asp:Label runat="server" ID="labPlayersToRemoveCount" CssClass="count"></asp:Label>
          </h3>
          <asp:GridView runat="server" ID="gvPlayersToRemove" AutoGenerateColumns="false" 
            SkinID="ConsiderRemoving" onrowdatabound="gvPlayersToRemove_RowDataBound" CssClass="standardGrid" 
            onrowcommand="gvPlayersToRemove_RowCommand" ondatabound="gvPlayersToRemove_DataBound">
            <Columns>
              <%--Name--%>
              <asp:TemplateField HeaderText="Player" HeaderStyle-CssClass="nameHeader">
                <ItemTemplate>
                  <table style="border-collapse:collapse;">
                    <tr>
                      <td>
                        <asp:Label runat="server" ID="labFullNameLastFirst" />
                        <asp:Label runat="server" ID="labTeamAbbreviation" CssClass="teamAbbreviation" />    
                      </td>
                      <td>
                        <asp:HyperLink runat="server" ID="hlResearchPlayer" CssClass="googleNews"  Target="_blank" />
                      </td>
                    </tr>                  
                  </table>
                </ItemTemplate>
              </asp:TemplateField>
              <%--User Rank--%>
              <asp:TemplateField HeaderText="Your Rank" HeaderStyle-CssClass="rankHeader">
                <ItemTemplate>
                  <asp:Label runat="server" ID="labUserRank" />        
                </ItemTemplate>
              </asp:TemplateField>
              <%--CSWR Rank--%>
              <asp:TemplateField>
                <HeaderStyle CssClass="rankHeader" />
                <HeaderTemplate>
                  <abbr title="Cheat Sheet War Room">CSWR</abbr> Rank
                </HeaderTemplate>
                <ItemTemplate>
                  <asp:Label runat="server" ID="labCSWRRank" />
                </ItemTemplate>
              </asp:TemplateField>
              <%--Remove Player--%>
              <asp:TemplateField HeaderText="Remove Player">
                <%--<ItemStyle HorizontalAlign="Center" />--%>
                <ItemTemplate>
                  <asp:Button runat="server" ID="butRemovePlayer" CommandName="RemovePlayer" Text="Remove" CssClass="buttonLength"/>
                  <asp:Button runat="server" ID="butDemotePlayer" CommandName="DemotePlayer" CssClass="buttonLength"/>
                </ItemTemplate>
              </asp:TemplateField>
            </Columns>
          </asp:GridView>

        
      </div>
      <div class="col-sm-4 rankings">
        

        <h3 runat="server" id="headerYourRankings">Your Rankings</h3>

        <asp:Repeater runat="server" ID="repSheetPlayers" 
          onitemdatabound="repSheetPlayers_ItemDataBound">
          <ItemTemplate>
            <asp:Panel runat="server" ID="panPlayerContainer" CssClass="playerContainer">
              <asp:Label runat="server" id="labPlayerRank" CssClass="bold" />&nbsp;
              <asp:Label runat="server" id="labPlayerName" /> 
              <asp:Label runat="server" ID="labTeamAbbreviation" CssClass="team" /> 
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

      </div>
    </asp:Panel>

  </div>  <!-- close AjaxFormContainer -->

  </ContentTemplate>
</asp:UpdatePanel>



</asp:Panel>

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

