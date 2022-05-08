<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FOOSheetItemTemplate.ascx.cs" Inherits="BP.CheatSheetWarRoom.UI.UserControls.FOOSheetItemTemplate" %>
<%@ Register Namespace="BP.CheatSheetWarRoom.MyControls" TagPrefix="simg" %>
<%@ Register Src="~/usercontrols/Sports/Football/CheatSheet/FOONoteEditor.ascx" TagName="FOONoteEditor" TagPrefix="cswr" %>


<asp:Panel runat="server" ID="panPlayerTemplate" CssClass="fOOCheatSheetItemTemplateControl" EnableViewState="false">

  <a runat="server" id="playerAnchor"></a>     

  <!-- Container for ranking number -->
  <div class="rankContainer">
    <span class="seqNo"></span>
  </div>

  <!-- Drag handle -->
  <asp:Panel runat="server" ID="panDragHandle">
    <div class="handle1"></div>             
    <div class="handle2"></div>             
  </asp:Panel>

  <%--"CheatSheetID_PlayerID_PlayerName_StatSeason" (for cheat sheets), SupplementalSheetID_PlayerID_PlayerName_StatSeason" (for supplemental sheets) --%>
  <div class="itemPropertiesContainer">
    <asp:HiddenField runat="server" ID="hfItemProperties"/>
  </div>

  <!-- Supplemental Ranking Container -->
  <asp:Panel runat="server" ID="panSuppRankingContainer" CssClass="suppRankingContainer">
    <table>
	    <tr>
        <td class="supplementalRankSection">

          <asp:Label runat="server" ID="labSuppRanking1" CssClass="suppData"/>
          <asp:Label runat="server" ID="labSuppRanking2" CssClass="suppData"/>
          <div class="supp3Container">
            <asp:Label runat="server" ID="labSuppRanking3" CssClass="supp3"/>
          </div>

          <div class="magGlassContainer">
            <asp:Image runat="server" ID="imaSuppMagGlass" ImageUrl="~/Images/Sports/Football/UserControls/SheetItemTemplates/magglass_brown.gif"/>
          </div>
	      </td>
	    </tr>
	  </table>
  </asp:Panel>

  <!-- Player Section -->
  <asp:Panel runat="server" ID="panPlayerContainer">

    <table class="playerTable">
      <tr class="playerInfoRow">
        <td rowspan="2" class="helmetCell">
          <asp:HyperLink runat="server" ID="hlHelmetLink" rel="nofollow" CssClass="helmetLink" Target="_blank"></asp:HyperLink>
        </td>
        <td class="playerCell">
          <asp:Label runat="server" ID="labPlayerName" CssClass="playerName"/>
          <asp:Label runat="server" ID="labPlayerPosition" CssClass="playerPosition" />
        </td>
      </tr>
      <tr class="teamInfoRow">
        <td class="teamCell" colspan="2">
          <%--Team Info--%>
          <asp:Label runat="server" ID="labTeamInfo" />
          <%--Age--%>
          <asp:Label runat="server" ID="labAge" />
          <%--Bye Week--%>
          <asp:HyperLink runat="server" ID="hlByeWeek" NavigateUrl="https://www.cheatsheetwarroom.com/blog/football/bye-weeks" Target="_blank" CssClass="byeWeek" />
        </td>
      </tr>
      <tr>
        <td colspan="2" class="tagsCell">
          <table>
            <tr>
              <td class="sleeperCell">
                <simg:StrobeImage runat="server" ID="siSleeperTag" ImageURL="~/Images/Sports/Football/UserControls/SheetItemTemplates/Tags/sleepertag.gif"/>
              </td>
              <td class="bustCell">
                <simg:StrobeImage runat="server" ID="siBustTag" ImageURL="~/Images/Sports/Football/UserControls/SheetItemTemplates/Tags/busttag.gif"/>
              </td>
              <td class="injuredCell">
                <simg:StrobeImage runat="server" ID="siInjuredTag" ImageURL="~/Images/Sports/Football/UserControls/SheetItemTemplates/Tags/injuredtag.gif"/>
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </asp:Panel>  <!-- close playerContainer -->

 
  <div class="featureIconsContainer">
    <div class="columnContainer1">
      <asp:HyperLink runat="server" ID="hlDepthChart" Target="_blank" rel="nofollow" CssClass="depth topFeature"/>
      <asp:HyperLink runat="server" ID="hlGoogleNewsSearch" Target="_blank" rel="nofollow" CssClass="googleNews topFeature"/>
    </div>
    <div class="columnContainer2">
      <asp:HyperLink runat="server" ID="hlTwitter" Target="_blank" rel="nofollow" CssClass="twitter bottomFeature" Visible="false" />
    </div>
  </div>
 
  <asp:Panel runat="server" ID="panStatsContainer" CssClass="statsContainer">
  
    <asp:Panel runat="server" ID="panValidStatsContainer">

      <div class="playerRankContainer">

        <div class="tfpContainer rankBlock">
          <div class="rank">
            <asp:Label runat="server" ID="labtfpRank" CssClass="rankValue"/>
            <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/UserControls/SheetItemTemplates/numbersymbolbackground.gif" AlternateText=""/>
          </div>  <!-- close rank -->
          <div class="title">
            <span><abbr runat="server" id="acrTFPTopLeftSide">TFP</abbr></span>
            <span>Rank</span>
          </div>  <!-- close title -->
        </div>  <!-- close tfpContainer -->

        <div class="fppgContainer rankBlock">
          <div class="rank">
            <asp:Label runat="server" ID="labfppgRank" CssClass="rankValue" />
            <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/UserControls/SheetItemTemplates/numbersymbolbackground.gif" AlternateText="" />
          </div>  <!-- close rank -->
          <div class="title">
            <span><abbr runat="server" id="acrFPPGTopRightSide">FPPG</abbr></span>
            <span>Rank</span>
          </div>  <!-- close title -->
        </div>  <!-- close fppgContainer -->

      </div>  <!-- close playerRankContainer -->

      <div class="summaryContainer">
        <div class="leftSide">
          <asp:Label runat="server" ID="labTFP" CssClass="stat"/> <abbr runat="server" id="acrTFPBottomLeftSide">TFP</abbr>  
        </div>
        <div class="rightSide">
          <asp:Label runat="server" ID="labFPPG" CssClass="stat" /> <abbr runat="server" id="acrFPPGBottomRightSide">FPPG</abbr>  
          <asp:Image runat="server" ID="imaStatMagGlass" CssClass="magGlass" ImageUrl="~/Images/Sports/Football/UserControls/SheetItemTemplates/magglass_brown.gif" />
        </div>
      </div>  <!-- close summaryContainer -->

    </asp:Panel>

    <asp:Panel runat="server" ID="panNoStatsContainer" CssClass="noStatsContainer" Visible="false">
    </asp:Panel>

  </asp:Panel>  <!-- close panStatsContainer -->

  <!-- Status Icon -->
  <div class="statusIconContainer">
    <asp:HyperLink runat="server" ID="hlStatusIcon"/>
  </div>

  <!-- Notes Section -->
  <div class="noteContainer">
    <cswr:FOONoteEditor runat="server" ID="fneFOONoteEditor" />
  </div>  <!-- noteContainer -->

</asp:Panel>



