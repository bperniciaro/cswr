<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveTwoCol.master" Theme="Web20" AutoEventWireup="true" CodeFile="player-rankings.aspx.cs" 
    Inherits="BP.CheatSheetWarRoom.UI.NFLPlayerRankings" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage" 
    CanonicalUrl="https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/rankings/player-rankings.aspx"
%>
<%@ MasterType VirtualPath="~/MasterPages/Sport.master" %>
<%@ Register Src="~/usercontrols/Navigation/PlayerRankingNavigation.ascx" TagName="PlayerRankingNavigation" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/Sports/Football/PlayerRankings/CSWRFOORankingItemTemplate.ascx" TagPrefix="cswr" TagName="CSWRFOORankingItemTemplate" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

<div class="rankingNFLPlayersPage">


  <h1><asp:Literal runat="server" ID="litMainHeader" /> </h1>

  <cswr:PlayerRankingNavigation runat="server" />

  <asp:Image runat="server" ID="imaPlayerRankings" ImageUrl="~/Images/Layout/playerrankings/nfl-player-rankings.jpg" AlternateText="NFL Player Rankings" style="float: right; margin-bottom: 15px; margin-left: 15px;"/>

  <p class="intro">
    Developing <strong>accurate fantasy football player rankings</strong> is a key component of creating reliable fantasy football cheat sheets,
    and as we know your cheat sheet could make or break your fantasy football draft.  In modern fantasy football leagues, 
    almost any football position can be added to your fantasy league scoring configuration (even defensive players).  
  </p>
  
  <p>
    Regardless of 
    your league’s configuration, you’ll need to be sure to have accurate NFL player rankings for each 
    position that is required to complete your fantasy roster.  Otherwise, how would you know how to make picks for your 
      <a href="https://www.cheatsheetwarroom.com/blog/fantasy-football/draft/boards">fantasy football draft boards</a>
      
      as the season approachs?
      
      Here are some factors we consider when developing our fantasy football player rankings:
  </p>

  <ul>
    <li>
      <strong>Statistics</strong>- 
      Obviously the stats from the previous season are most critical but as the season progresses current statistics become more relevant.  
    </li>
    <li>
      <strong>Fantasy Point Output</strong>- 
      Understand how each player’s statistics translate to point output based on your 
      <asp:HyperLink runat="server" NavigateUrl="https://www.cheatsheetwarroom.com/blog/fantasy-football/leagues/best-settings">fantasy league configuration</asp:HyperLink>.
      Just as statistic/point-values vary from league to league, a player's ranking will likewise rise or fall based on league settings.
    </li>
    <li>
      <strong>Games Played</strong>- 
      The number of games a player actually played in during the previous season is an oft-forgotten statistic.  
      Would you rather draft a player that put up 80 fantasy points in 16 games, or a player who put up 60 fantasy points in 8 games?  This is why 
      <abbr title="Fantasy Points Per Game">FPPG</abbr>
      output is a key statistic to follow (and also why it is integrated into all of our sheets by default).
    </li> 
    <li>
      <strong>Team</strong>- 
      The team a player plays for should definitely factor into their ranking, especially if the player has changed 
      teams.  Those teams with high-powered offenses will have players who put up more fantasy points, so remember to factor in the team when you configure
      your rankings.
    </li>
    <li>
      <strong>Experience</strong>- 
      The factor that experience plays in your NFL player rankings is heavily dependent on the position being analyzed;
      a productive kicker will have a much longer career than a productive running back.  
      Compare each player’s stats to their experience to ensure they are not on the downside of their career. 
    </li>
    <li>
      <strong>Position on Depth Chart</strong>- 
      As training camp battles progress, a clearer picture of each team’s depth chart should arise.  Know which 
      players have cemented a starting job and which players are still battling to move up the depth chart.
    </li>
    <li>
      <strong>Injury Concerns</strong>– 
      Football players are constantly battling injuries, but some injuries are worse than others.  Those players with 
      injury concerns should slide down your player rankings; how far they slide will depend on the severity (and sometimes the frequency) of their injury.
    </li>
  </ul>
 
  <p>
    There are many different factors to consider when configuring your <asp:Literal runat="server" ID="litCurrentSeason1" />
    NFL player rankings.  The key to success is examining the relevant 
    factors and properly applying them to the position and player being analyzed.  We hope that the rankings provided will help you create 
    reliable and accurate fantasy football cheat sheets in preparation for your <asp:Literal runat="server" ID="litCurrentSeason2" />
    fantasy football draft.  Below are our top 5 players at each 
    position named in the 
    <asp:HyperLink runat="server" NavigateUrl="s">standard fantasy football scoring</asp:HyperLink> system.
    To view all rankings for a particular position please click the links above.
  </p>

  <br />

  <%--QBs--%>
  <div class="topPlayersContainer">
    <%--Title and Link--%>
    <table>
      <tr>
        <td>
          <h2>Top 10 Quarterbacks</h2>
        </td>
        <td>
          <asp:Hyperlink runat="server" ID="hlQBRankings" NavigateUrl="~/fantasy-football/nfl/free/rankings/offense/quarterbacks.aspx" Text="(view all)" style="vertical-align: middle;"/>
        </td>
      </tr>
    </table>
    <%--Players--%>
    <asp:Repeater runat="server" ID="repQBRankings" OnItemDataBound="cswrRanking_ItemDataBound">
      <ItemTemplate>
        <cswr:CSWRFOORankingItemTemplate runat="server" id="fooRankingItemTemplate" />
      </ItemTemplate>
    </asp:Repeater>
  </div>

  <%--RBs--%>
  <div class="topPlayersContainer">
    <%--Title and Link--%>
    <table>
      <tr>
        <td>
          <h2>Top 10 Running Backs</h2>
        </td>
        <td>
          <asp:Hyperlink runat="server" ID="Hyperlink1" NavigateUrl="~/fantasy-football/nfl/free/rankings/offense/running-backs.aspx" Text="(view all)" style="vertical-align: middle;"/>
        </td>
      </tr>
    </table>
    <%--Players--%>
    <asp:Repeater runat="server" ID="repRBRankings" OnItemDataBound="cswrRanking_ItemDataBound">
      <ItemTemplate>
        <cswr:CSWRFOORankingItemTemplate runat="server" id="fooRankingItemTemplate" />
      </ItemTemplate>
    </asp:Repeater>
  </div>  
  

  <%--WRs--%>
  <div class="topPlayersContainer">
    <%--Title and Link--%>
    <table>
      <tr>
        <td>
          <h2>Top 10 Wide Receivers</h2>
        </td>
        <td>
          <asp:Hyperlink runat="server" ID="Hyperlink2" NavigateUrl="~/fantasy-football/nfl/free/rankings/offense/wide-receivers.aspx" Text="(view all)" style="vertical-align: middle;"/>
        </td>
      </tr>
    </table>
    <%--Players--%>
    <asp:Repeater runat="server" ID="repWRRankings" OnItemDataBound="cswrRanking_ItemDataBound">
      <ItemTemplate>
        <cswr:CSWRFOORankingItemTemplate runat="server" id="fooRankingItemTemplate" />
      </ItemTemplate>
    </asp:Repeater>
  </div>

  <%--TEs--%>
  <div class="topPlayersContainer">
    <%--Title and Link--%>
    <table>
      <tr>
        <td>
          <h2>Top 10 Tight Ends</h2>
        </td>
        <td>
          <asp:Hyperlink runat="server" ID="Hyperlink3" NavigateUrl="~/fantasy-football/nfl/free/rankings/offense/tight-ends.aspx" Text="(view all)" style="vertical-align: middle;"/>
        </td>
      </tr>
    </table>
    <%--Players--%>
    <asp:Repeater runat="server" ID="repTERankings" OnItemDataBound="cswrRanking_ItemDataBound">
      <ItemTemplate>
        <cswr:CSWRFOORankingItemTemplate runat="server" id="fooRankingItemTemplate" />
      </ItemTemplate>
    </asp:Repeater>
  </div>

  <%--Ks--%>
  <div class="topPlayersContainer">
    <%--Title and Link--%>
    <table>
      <tr>
        <td>
          <h2>Top 10 Kickers</h2>
        </td>
        <td>
          <asp:Hyperlink runat="server" ID="Hyperlink4" NavigateUrl="~/fantasy-football/nfl/free/rankings/offense/kickers.aspx" Text="(view all)" style="vertical-align: middle;"/>
        </td>
      </tr>
    </table>
    <%--Players--%>
    <asp:Repeater runat="server" ID="repKRankings" OnItemDataBound="cswrRanking_ItemDataBound">
      <ItemTemplate>
        <cswr:CSWRFOORankingItemTemplate runat="server" id="fooRankingItemTemplate" />
      </ItemTemplate>
    </asp:Repeater>
  </div>

  <%--DFs--%>
  <div class="topPlayersContainer">
    <%--Title and Link--%>
    <table>
      <tr>
        <td>
          <h2>Top 10 Defenses/Special Teams</h2>
        </td>
        <td>
          <asp:Hyperlink runat="server" ID="Hyperlink5" NavigateUrl="~/fantasy-football/nfl/free/rankings/offense/defenses.aspx" Text="(view all)" style="vertical-align: middle;"/>
        </td>
      </tr>
    </table>
    <%--Players--%>
    <asp:Repeater runat="server" ID="repDFRankings" OnItemDataBound="cswrRanking_ItemDataBound">
      <ItemTemplate>
        <cswr:CSWRFOORankingItemTemplate runat="server" id="fooRankingItemTemplate" />
      </ItemTemplate>
    </asp:Repeater>
  </div>


</div>
  



</asp:Content>

