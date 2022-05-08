<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="RACSheetItemTemplate.ascx.cs"
    Inherits="BP.CheatSheetWarRoom.UI.UserControls.RACSheetItemTemplate"  %>
<%@ Register Namespace="BP.CheatSheetWarRoom.MyControls" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/Sports/Racing/CheatSheet/RACNoteEditor.ascx" TagName="RACNoteEditor" TagPrefix="cswr"  %>

<asp:Panel runat="server" ID="panPlayerTemplate" CssClass="racCheatSheetItemTemplateControl">

  <!--Left Side-->
  <div class="leftSubBlock">

    <!-- Container for ranking number -->
    <div class="rankContainer">
      <span class="seqNo"></span>
    </div>

    <!-- Drag handle -->
    <div class="dragContainer"></div>

    <!-- Supplemental Rankings -->
    <asp:Panel runat="server" ID="panSuppRankingContainer" CssClass="suppContainer">
      <%--ADP Ranking--%>
      <div class="numberContainer">
        <asp:Label runat="server" ID="labADP"/><asp:Label runat="server" ID="labADPDecimal" CssClass="decimal"/>
      </div>
      <%--CSWR Ranking--%>
      <div class="numberContainer">
        <asp:Label runat="server" ID="labCSWRRank" /><asp:Label runat="server" ID="labCSWRRankDecimal" CssClass="cswrDecimal"/>
      </div>
    </asp:Panel>

    <asp:Panel runat="server" ID="panMapGlassContainer" CssClass="magGlassContainer">
      <asp:HiddenField runat="server" ID="hfADP"/>
      <asp:HiddenField runat="server" ID="hfCSWR"/>
      <asp:Image runat="server" ID="imaSupplementalMagGlass" ImageUrl="~/Images/Sports/Racing/UserControls/SheetItemTemplates/magglass_yellow.gif" AlternateText="" CssClass="magGlass" />
    </asp:Panel>

    <!--Driver-->
    <div class="driverContainer">
      <%--Driver Name--%>
      <div class="driverName">
        <asp:Label runat="server" ID="labDriverName" />
      </div>
      <%--Driver Extras--%>
      <div class="driverExtras">
        <%--Experience--%>
        <div class="experience">
          <asp:Label runat="server" ID="labExpYears" /><asp:Label runat="server" ID="labExpYearsSingular" Text="yr exp" /> <asp:Label runat="server" ID="labExpYearsPlural" Text="yrs exp" /><asp:Label runat="server" ID="labExpRookie" Text="Rookie" />,
          <asp:Label runat="server" ID="labCarMake" /> #<asp:Label runat="server" ID="labCarNumber" />
        </div>
      </div>
    </div>  <!-- close driver container -->

    <%--Buttons--%>
    <div class="buttons">
      <div>
        <asp:HyperLink runat="server" ID="hlGoogleSearch" CssClass="googleSearch" Target="_blank" />
        <asp:HyperLink runat="server" ID="hlGoogleNews" CssClass="googleNews" Target="_blank" />      
      </div>  
      <div>
        <asp:HyperLink runat="server" ID="hlTwitter" CssClass="twitter" style="display:none;"/>
        <asp:HyperLink runat="server" ID="hlDummyLink" CssClass="dummyLink"/>
        <asp:HyperLink runat="server" ID="hlIFantasyRace" CssClass="iFantasyRace" Visible="false" Target="_blank"/>
      </div>
    </div>



  </div>  <!-- close leftSubBlock -->
  
  
  <div class="rightSubBlock">
  
    <!-- Note Simulator -->
    <asp:Panel runat="server" ID="panNote" CssClass="noteContainer">
      <cswr:RACNoteEditor runat="server" ID="neNoteEditor" />
    </asp:Panel>

    <!-- Stats  -->
    <div class="statsContainer">
  
      <table class="fluidTable">
        <tr>
        
          <td>
            <!--Rank-->
            <div class="statContainer notLast">
              <div class="statName">
                Rank
              </div>
              <div class="statValue">
                <asp:Label runat="server" ID="labRank" />
              </div>
            </div>
          </td>
        
        
          <td>
            <!--Points-->
            <div class="statContainer notLast">
              <div class="statName">
                Points
              </div>
              <div class="statValue">
                <asp:Label runat="server" ID="labPoints" />
              </div>
            </div>
          </td>  
        
          <td>
            <!--Wins-->
            <div class="statContainer notLast">
              <div class="statName">
                Wins
              </div>
              <div class="statValue">
                <asp:Label runat="server" ID="labWins" />
              </div>
            </div>
          </td>

          <td>
            <!--AFP-->
            <div class="statContainer notLast">
              <div  class="statName">
                AFP
              </div>
              <div class="statValue">
                <asp:Label runat="server" ID="labAFP" />
              </div>
            </div>
          </td>

          <td>
            <!--Top 10-->
            <div class="statContainer">
              <div class="statName">
                Top10
              </div>
              <div class="statValue">
                <asp:Label runat="server" ID="labTop10" />
              </div>
            </div>
            <div class="statExamination">
              <asp:HiddenField runat="server" ID="hfStatSeasonCode" EnableViewState="false" />
              <asp:HiddenField runat="server" ID="hfPlayerID" EnableViewState="false" />
              <asp:HiddenField runat="server" ID="hfPlayerName" EnableViewState="false" />
              <asp:Image runat="server" ID="imaStatsMagGlass" CssClass="magGlass" ImageUrl="~/Images/Sports/Racing/UserControls/SheetItemTemplates/magglass_blue.gif" AlternateText="" />
            </div>
          </td>
        </tr>
      </table>
    </div>  <!-- close statContainer -->
  </div>  <!-- close rightSubBlock -->
</asp:Panel>





