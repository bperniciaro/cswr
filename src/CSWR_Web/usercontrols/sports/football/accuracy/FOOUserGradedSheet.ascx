<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FOOUserGradedSheet.ascx.cs" Inherits="UserControls.Sports.Football.Accuracy.FooUserGradedSheet" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>

  <cswr:MessageBox runat="server" ID="mbStatus" />

<div class="fooUserGradedSheet">

  <asp:Panel runat="server" ID="panHeader">
    
    <%--Header--%>
    <asp:Panel runat="server" ID="panTitle">
      <h1 style="margin:-10px 0 0 0;">
        <asp:Literal runat="server" ID="litUsername"></asp:Literal>
        <asp:Literal runat="server" ID="litSeasonCode"></asp:Literal>
        <asp:Literal runat="server" ID="litPosition"></asp:Literal>
        Cheat Sheet
      </h1>
    </asp:Panel>
    
    <%--Season Buttons--%>
    <asp:Repeater runat="server" ID="repSeasons" OnItemDataBound="repSeasons_OnItemDataBound">
      <ItemTemplate>
        <span runat="server" id="spSeasonButtonContainer">
          <asp:Button runat="server" ID="butSeason"/>
        </span>
      </ItemTemplate>
    </asp:Repeater>

    <%--Positional Buttons--%>  
    <div id="positionalButtons" class="positionButtonsContainer">
      <%--QB Button--%>
      <span runat="server" id="spQBButtonContainer">
        <asp:Button runat="server" ID="butQBSheet" Text="QB" CommandName="Position" CommandArgument="QB" CssClass="btn btn-default" />
      </span>
      <%--RB Button--%>
      <span runat="server" id="spRBButtonContainer">
        <asp:Button runat="server" ID="butRBSheet" Text="RB" CommandName="Position" CommandArgument="RB" CssClass="btn btn-default"/>
      </span>
      <%--WR Button--%>
      <span runat="server" id="spWRButtonContainer">
        <asp:Button runat="server" ID="butWRSheet" Text="WR" CommandName="Position" CommandArgument="WR" CssClass="btn btn-default"/>
      </span>
      <%--TE Button--%>
      <span runat="server" id="spTEButtonContainer">
        <asp:Button runat="server" ID="butTESheet" Text="TE" CommandName="Position" CommandArgument="TE" CssClass="btn btn-default"/>
      </span>
      <%--K Button--%>
      <span runat="server" id="spKButtonContainer">
        <asp:Button runat="server" ID="butKSheet" Text="K" CommandName="Position" CommandArgument="K" CssClass="btn btn-default"/>
      </span>      
      <%--DF Button--%>
      <span runat="server" id="spDFButtonContainer">
        <asp:Button runat="server" ID="butDFSheet" Text="DF" CommandName="Position" CommandArgument="DF" CssClass="btn btn-default"/>
      </span>
    </div>

    <asp:Panel runat="server" ID="panNoSheetFound" Visible="False">
      No sheet was found for this user.
    </asp:Panel>

  </asp:Panel>
  
 
  <h2><asp:Label runat="server" ID="labSheetName" CssClass="italic"/> Cheat Sheet</h2>

  <%--Ranking--%>
  <asp:Panel runat="server" ID="panSheetRank" CssClass="rankDescription">
    <p>
      Ranked 
      <strong><asp:Literal runat="server" ID="litRank"/></strong>
      out of 
      <asp:Literal runat="server" ID="litTotalSheetsGraded"/> 
      graded
      <asp:Literal runat="server" ID="litPositionName"/> 
      sheets 
      (<strong><asp:Literal runat="server" ID="litScore" /></strong> percentile).
    </p>
  </asp:Panel>
   

  <asp:GridView runat="server" ID="gvUserSheet" CssClass="userGradedPositionGrid" AutoGenerateColumns="false" OnRowDataBound="gvUserSheet_RowDataBound" 
                ShowFooter="true" OnPreRender="gvUserSheet_PreRender" OnDataBound="gvUserSheet_DataBound">
    <Columns>
      <%--User Rank--%>
      <asp:BoundField DataField="Seqno"/>
      <%--Player Name--%>
      <asp:TemplateField HeaderText="Player">
        <ItemTemplate>
          <asp:Label runat="server" ID="labPlayerName"></asp:Label>          
        </ItemTemplate>
      </asp:TemplateField>

      <%--Rank--%>
      <asp:TemplateField>
        <ItemTemplate>
          <asp:Label runat="server" ID="labRank" />
        </ItemTemplate>
      </asp:TemplateField>
      <%--Difference--%>
      <asp:TemplateField HeaderText="User Diff">
        <ItemTemplate>
          <asp:Label runat="server" ID="labUserRankDifference" />
        </ItemTemplate>
      </asp:TemplateField>
      <%--Average Difference for this Player--%>
      <asp:TemplateField HeaderText="Avg Diff">
        <ItemTemplate>
          <asp:Label runat="server" ID="labAverageRankDifference" />
        </ItemTemplate>
      </asp:TemplateField>
      <%--Difference Between Average Difference and user Difference--%>
      <asp:TemplateField HeaderText="User Vs Avg">
        <ItemTemplate>
          <asp:Label runat="server" ID="labUserVersusAverage" />
        </ItemTemplate>
      </asp:TemplateField>
    </Columns>
  </asp:GridView>

  </div>


<script type="text/javascript">

  $(document).ready(function () {

    $(".userGradedPositionGrid").dataTable({
      "bPaginate": false,
      "bSort": true
    });

  });



</script>
