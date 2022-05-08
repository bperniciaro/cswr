<%@ Page Title="Fantasy Football User Accuracy Leaderboard" Language="C#" MasterPageFile="~/MasterPages/Sport.master" 
    AutoEventWireup="true" CodeFile="overall-leaderboard.aspx.cs"  CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
  Inherits="BP.CheatSheetWarRoom.UI.FantasyFootballLeaderboard" 
  CanonicalUrl="https://www.cheatsheetwarroom.com/fantasy-football/nfl/accuracy/users/overall-leaderboard.aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">
  
  
  <%--Page Title--%>
  <h1 style="padding-bottom: 20px;"><asp:Literal runat="server" ID="litPageTitle"/></h1>
  
  <%--Season Buttons--%>
  <div style="padding-bottom: 20px;">
    <asp:Repeater runat="server" id="repSeasons" OnItemDataBound="repSeasons_ItemDataBound">
      <ItemTemplate>
        <asp:Button runat="server" ID="butSeasonButton" OnClick="butSeason_Click"/>
      </ItemTemplate>
    </asp:Repeater>
  </div>

  <%--Into Paragraph--%>
  <p style="padding-bottom:20px;">
    This page lists the <strong>most accurate</strong> 
    <abbr title="Cheat Sheet War Room">CSWR</abbr>
    users for the 
    2017
    fantasy football season.  Each of the six primary offensive positions are graded and scores are awarded based from 1-100 based on the
    accuracy of the respective positional cheat sheet.  Positional scores are then summed to determine an overall score.
    Click on any positional score to view the respective sheet and a player by player analysis for the grade cheat sheet.
  </p>
  
  <div id="loading" style="margin-top:140px;text-align:center">
    <asp:Image runat="server" ImageUrl="~/Images/Animations/googlerotate.gif"/>
  </div>      


  <%--Seasonal Leaderboard Grid--%>
  <asp:GridView runat="server" CssClass="overallAccuracyGrid"  ID="gvOverallUserAccuracy" AutoGenerateColumns="false" style="display:none;"
      OnRowDataBound="gvOverallUserAccuracy_RowDataBound" OnPreRender="gvOverallUserAccuracy_PreRender">
    <Columns>
      <asp:BoundField DataField="Rank" HeaderText="Rank" />
      <asp:BoundField DataField="Username" HeaderText="User" />

      <%--QB Score--%>
      <asp:TemplateField HeaderText="QB Score">
        <ItemTemplate>
          <asp:HyperLink runat="server" ID="hlQBSheetLink"/>
        </ItemTemplate>
      </asp:TemplateField>

      <%--RB Score--%>
      <asp:TemplateField HeaderText="RB Score">
        <ItemTemplate>
          <asp:HyperLink runat="server" ID="hlRBSheetLink"/>
        </ItemTemplate>
      </asp:TemplateField>

      <%--WR Score--%>
      <asp:TemplateField HeaderText="WR Score">
        <ItemTemplate>
          <asp:HyperLink runat="server" ID="hlWRSheetLink"/>
        </ItemTemplate>
      </asp:TemplateField>

      <%--TE Score--%>
      <asp:TemplateField HeaderText="TE Score">
        <ItemTemplate>
          <asp:HyperLink runat="server" ID="hlTESheetLink"/>
        </ItemTemplate>
      </asp:TemplateField>

      <%--K Score--%>
      <asp:TemplateField HeaderText="K Score">
        <ItemTemplate>
          <asp:HyperLink runat="server" ID="hlKSheetLink"/>
        </ItemTemplate>
      </asp:TemplateField>

      <%--DST Score--%>
      <asp:TemplateField HeaderText="DF Score">
        <ItemTemplate>
          <asp:HyperLink runat="server" ID="hlDSTSheetLink"/>
        </ItemTemplate>
      </asp:TemplateField>

      <%--Overall Score--%>
      <asp:TemplateField HeaderText="Overall Score">
        <ItemTemplate>
          <asp:Label runat="server" ID="labOverallScore" CssClass="bold"></asp:Label>          
        </ItemTemplate>
      </asp:TemplateField>

    </Columns>
  </asp:GridView>

  <script type="text/javascript">

    $(document).ready(function () {


      

      $(".overallAccuracyGrid").dataTable({
        "bProcessing": true,
        "fnInitComplete": function() {
          $("#loading").hide();
          $(".overallAccuracyGrid").show();
          this.fnAdjustColumnSizing(); // for header redraw 
        },
        "aoColumns": [
                    null,
                    null,
                    { "orderSequence": ["desc", "asc"] },
                    { "orderSequence": ["desc", "asc"] },
                    { "orderSequence": ["desc", "asc"] },
                    { "orderSequence": ["desc", "asc"] },
                    { "orderSequence": ["desc", "asc"] },
                    { "orderSequence": ["desc", "asc"] },
                    { "orderSequence": ["desc", "asc"] }
        ]

      });
    });

  </script>


</asp:Content>

