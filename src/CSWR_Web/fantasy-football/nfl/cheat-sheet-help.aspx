<%@ Page Language="C#" MasterPageFile="~/MasterPages/Sport.master" Theme="Web20" AutoEventWireup="true" 
  CodeFile="cheat-sheet-help.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.CheatSheetCreation" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage" 
  Title="Guide to Using our Fantasy Cheat Sheet Creation Interface" 
  MetaDescription="A comprehensive guide to all of the features packed into our custom cheat sheet interface."
  CanonicalUrl="http://www.cheatsheetwarroom.com/fantasy-football/nfl/cheat-sheet-help.aspx"
%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <div class="sheetHelpPage">
  
    <h1>Football Cheat Sheet Help</h1>
  
    <!-- Intro -->  
    <div class="intro">
      <p>
        Our 
        <asp:HyperLink runat="server" NavigateUrl="~/fantasy-football/nfl/create/custom-sheet.aspx">free fantasy football cheat sheet creation</asp:HyperLink>
        tools allow users to create informative cheat sheets in a fun and intuitive manner.  
        We provide a wide-range of functionality in our football cheat sheets: ranking players using drag &amp; drop, supplemental NFL player rankings 
        from other sources (to compare against your own rankings), important player information (name, team, position, number, 
        <asp:HyperLink runat="server" NavigateUrl="https://www.cheatsheetwarroom.com/blog/football/bye-weeks" Text="bye week" />,
        and experience), relevant 
        statistics for each fantasy position, fantasy point output calculations, a configurable note for each player, and even tags to easily mark a player
        as a bust, sleeper, or injured.  The purpose of this page is to explain all of this functionality in detail.    
      </p>
    
      <p>
        Please note that to take advantage of all advanced functionality outlined below, you must first 
        <asp:Hyperlink runat="server" NavigateUrl="~/access/register.aspx">register</asp:Hyperlink>.
      </p>

      <ol>
        <li>
          <a href="#systemRequirements">System Requirements</a>
        </li>
        <li>
          <a href="#applicationPerformance">Application Performance</a>
        </li>
        <li>
          <a href="#registration">Registration</a>
        </li>
        <li>
          <a href="#sheetCreation">Cheat Sheet Creation</a>
        </li>
        <li>
          <a href="#editSheet">Editing a Sheet</a>
        </li>
        <li>
          <a href="#rankingPlayers">Ranking Players</a>
        </li>
        <li>
          <a href="#searchNews">Search Player News</a>
        </li>
        <li>
          <a href="#supplementalRankings">Supplemental Rankings</a>
        </li>
        <li>
          <a href="#playerInformation">Player Information</a>
        </li>
        <li>
          <a href="#configurableTags">Configurable Tags</a>
        </li>
        <li>
          <a href="#playerStatistics">Player Statistics</a>
        </li>
        <li>
          <a href="#configurableNote">Configurable Note</a>
        </li>
        <li>
          <a href="#printableSheets">Printable Sheets</a>
        </li>
      </ol>

    </div>  
    

    <!-- System Requirements -->
    <div class="subSection">
      <h2 id="systemRequirements">System Requirements</h2>  
      <p>
        This application has been tested in all major web browsers but for best performance we <strong>strongly recommend</strong> that you use the 
        <asp:Hyperlink runat="server" NavigateUrl="http://www.mozilla.com/en-US/firefox/" Target="_blank">Firefox web browser</asp:Hyperlink>
        .  Firefox is <strong>much 
        more responsive</strong> when 
        <a href="#rankingPlayers">ranking players</a>
        (as opposed to Internet Explorer, where you have to hold down your mouse button for much longer when selecting a player).  
        A high-speed internet connection is beneficial, but not required.  You must have JavaScript and cookies enabled to use this application because of 
        its interactive nature and the heavy use of <a href="http://en.wikipedia.org/wiki/AJAX" target="_blank">AJAX</a>.  
      </p>
    </div>




    <!-- Application Performance -->
    <div class="subSection">
      <h2 id="applicationPerformance">Application Performance</h2>
      <p>
        Because of the nature of the web, the cheat sheet interface will load and function slower as the number of players on your cheat sheet increases.  
        The degradation in performance is why only a modest number of players are added to your sheet when it is initially created.  However, as a 
        <a href="#registration">registered user</a>
        you are free to add as many players as you wish to your sheets, but keep in mind application responsiveness will degrade.  
      </p>  
      
      <p>  
        When reordering your players,
        some browsers (especially Internet Explorer) force you to hold down your mouse button for a period of time before you're able to successfully 
        <a href="#rankingPlayers">grab a player for reordering</a>.
        In Internet Explorer, as the number of players on your sheet increases, the time required to hold down your mouse button for player selection also increases.  This is a big
        reason why we recommend the
        <asp:Hyperlink runat="server" NavigateUrl="http://www.mozilla.com/en-US/firefox/" Target="_blank">Firefox web browser</asp:Hyperlink>
        as there is <strong>zero delay </strong> when selecting a player.
      </p>
    </div>



    <!-- Registration -->    
    <div class="subSection">
      <h2 id="registration">Registration</h2>  
      <p>
        In order for us to remember who you are (and more importantly which cheat sheets belong to you), you must 
        <asp:Hyperlink runat="server" NavigateUrl="~/access/register.aspx">register for a free account</asp:Hyperlink>.
        After you register, the application will remember which sheets belong to you and make them accessible each time you return to the site.  If you do 
        not register, any cheat sheets you have edited will be lost when you leave the site.  
      </p>
      
      Registration also unlocks the ablity for advanced cheat sheet features:

      <ul>
        <li>
          The ability to create any number of cheat sheets for any number of fantasy football leagues.
        </li>
        <li>
          The ability to add and remove players from your cheat sheet.
        </li>
        <li>
          The ability to generate a 
          <asp:HyperLink runat="server" NavigateUrl="~/fantasy-football/nfl/free/printable/offense/cheat-sheet-with-roster.aspx" Target="_blank">printable football cheat sheet</asp:HyperLink>
          using your custom rankings.
        </li>
        <li>
          The ability to export your cheat sheet rankings to a spreadsheet.
        </li>
        <li>
          The ability to re-sort your players using either statistics or supplemental 
          <asp:Hyperlink runat="server" NavigateURl="~/fantasy-football/nfl/free/rankings/player-rankings.aspx" Target="_blank">NFL football player rankings</asp:Hyperlink>.
        </li>
      </ul>
    </div>
    
    <!-- Cheat Sheet Creation -->
    <div class="subSection">
      <h2 id="sheetCreation">Cheat Sheet Creation</h2>
      <p>
        When you initially create a fantasy football cheat sheet, all of the available data and functionality will be integrated into your sheet 
        automatically.  The number of players that are initially added to your sheet will depend on the fantasy position on which your sheet is based.  The
        number of players chosen reflects the maximum number of player (for each respective position) that can be exported to our single-page,
        <asp:Hyperlink runat="server" NavigateUrl="~/fantasy-football/nfl/free/printable/offense/cheat-sheet-with-roster.aspx" Target="_blank">printable fantasy football cheat sheet</asp:Hyperlink>
        with roster area.  Number of players added to each football sheet by default are based on position:
      </p>
    
      <ul>
        <li><strong>Quarterback:</strong> 40 Players</li>
        <li><strong>Running Back:</strong> 75 Players</li>
        <li><strong>Wide Receiver:</strong> 75 Players</li>
        <li><strong>Tight End:</strong> 35 Players</li>
        <li><strong>Kicker:</strong> 32 Players</li>
        <li><strong>Defense:</strong> 32 Players</li>
      </ul>
    </div>

    <!-- Editing a Sheet -->
    <div class="subSection">
      <h2 id="editSheet">Editing a Sheet</h2>
      <p>
        If you are a registered user, you can edit your cheat sheet's properties at any time.  As a registered user, you can 
        freely add and remove players from your cheat sheet as desired.  While the cheat sheet names for visitor sheets are fixed, you can
        re-name your cheat sheets however you choose.  Finally, through the 'Edit Sheet' functionality, you can re-sort your players at any time
        using either statistics or a supplemental source.
      </p>
    </div>

    <!-- Rankings Players -->
    <div class="subSection">
      <h2 id="rankingPlayers">Ranking Players</h2>
      <p>
        The most important aspect of cheat sheet creation is the ability to easily manipulate your player rankings.  To change a 
        player’s ranking you first need to position your cursor over the <em>drag handle</em> (the portion of the player template with a football 
        texture), then <strong>click and hold down the mouse button</strong>.  The length of time you have to hold down your mouse button when selecting a player 
        varies widely based on both your web browser and the number
        of players in your cheat sheet.  For instance, Firefox (whichs is the browser we strongly recommend) responds immediately regardless of the 
        number of players in your sheet, while in IE the responsiveness is slower and decreases as the number of players on your sheet increases.
      </p>
    
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-selecting-player.gif" AlternateText="Selecting a football player to rank." />
      </div>
    
      <p>      
        After <strong>holding down your mouse button</strong> for some period of time, the player template will become <em>draggable</em>.  Still holding down the mouse button,
        you should now be able to drag the player to a different position on your cheat sheet. The position where the player will be 
        dropped should be highlighted as your move your cursor up and down.
      </p>
  
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-dragging-player.gif" AlternateText="Dragging a football player to a different position." />
      </div>
    </div>

    <!-- Search Player News -->
    <div class="subSection">
      <h2 id="searchNews">Search Player News</h2>
      <p>
        When creating cheat sheets, it is imperative that you stay current on player news.  The NFL preseason is a very volatile time and any tidbit of information
        you can garner on a player will give you a critical edge.  To simplify searching for player news, we provide a Google New Search icon to each
        player template.  To perform a new search for a particular player, simply click on the newspaper icon for the player in question.
      </p>
      
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-news-search.gif" AlternateText="Searching fantasy football player news." />
      </div>
    </div>

    <!-- Supplemental Rankings -->
    <div class="subSection">
    <h2 id="supplementalRankings">Supplemental Rankings</h2>
      <p>
        It is often worthwhile to double-check your rankings against NFL player rankings other reputable sources and this is the idea behind 
        <em>supplemental rankings</em>. The numbers on the left side of each player template comprise rankings from those supplemental sources.
      </p>  

      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-supplementals.gif" AlternateText="Football player template with supplemental rankings." />
      </div>

      <p>
        If you hover your mouse above the magnifying glass next to the supplemental rankings area, a popup will appear which will explain which source
        created the respective ranking.
      </p>  
    
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-supplemental-popup.gif" AlternateText="Football player template with supplemental popup." />
      </div>
    </div>
    
    <!-- Player Information -->
    <div class="subSection">
      <h2 id="playerInformation">Player Information</h2>
      <p>
        Each player template includes critical information about that player: name, position, number, team, experience, and his team’s bye week (when made
        available).     
      </p>
 
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-player-info.gif" AlternateText="Football player template with supplemental popup." />
      </div>
    </div>
    

    <!-- Configurable Tags -->
    <div class="subSection">
      <h2 id="configurableTags">Configurable Tags</h2>
      <p>
        In fantasy football, there are common <em>attributes</em> that we can sometimes assign to a player, two of the most common being 
        <strong>sleeper</strong> and <strong>bust</strong>.  Because these attributes are so common, we created a simple way to <em>tag</em> a player with one of
        these attributes (as opposed to manually typing 'sleeper' or 'bust' in the player's 
        <a href="#configurableNote">configurable note area</a>).
        We also threw in a <em>injured</em> tag so you can easily spot those players who are hobbled. Tagging a player as a <em>sleeper</em>, <em>bust</em>, or 
        <em>injured</em> is as simple as clicking on the 'Sleeper', 'Bust', or 'Injured' buttons under a player's name.  To 'un-tag' a player, you can simply 
        click on the tag again to de-activate it.  
      </p>
    
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-tag-player.gif" AlternateText="Football player tagging as sleeper, bust, or injured." />
      </div>
      
      <p>  
        When you generate your 
        <asp:Hyperlink runat="server" NavigateUrl="~/fantasy-football/nfl/free/printable/offense/cheat-sheet-with-roster.aspx">printable football cheat sheet</asp:Hyperlink>,
        any tag that you activated on your cheat sheet will be transferred and placed next to the respective player for easy reference.    
      </p>
    </div>


    <!-- Relevant Statistics -->
    <div class="subSection">
      <h2 id="playerStatistics">Player Statistics</h2>
      <p>
        Our primary player templates display the following <strong>summary statistics</strong> from the previous year to easily compare players.  These statistics
        are generated based on fantasy point output using the standard
        <a href="https://www.cheatsheetwarroom.com/blog/fantasy-football/leagues/scoring-systems">fantasy football scoring</a> system,
        but <abbr title="Points Per Reception">PPR</abbr> sheets will be available before the 2013 season begins.  Our goal is to eventually support
        all of the common fantasy scoring systems.
      </p>
      <ul>
        <li>Total Fantasy Points (<abbr title="Total Fantasy Points">TFP</abbr>)</li>
        <li>Total Fantasy Points Rank</li>
        <li>Fantasy Points Per Game (<abbr title="Fantasy Points Per Game">FPPG</abbr>)</li>
        <li>Fantasy Points Per Game Rank</li>
      </ul>

      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-summary-statistics.gif" AlternateText="These fantasy stats total fantasy point output based on your league's scoring rules." />
      </div>
      
      <p>
        If you need to drill-down deeper into a player's individual statistics, simply hover your mouse over the magnifying glass next to the respective
        player's summary stats and a popup will appear containing detailed stats.
      </p>

      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-individual-statistics.gif" AlternateText="This popup generated NFL player individual statistics." />
      </div>



    </div>    




    <!-- Configurable Note -->
    <div class="subSection">
      <h2 id="configurableNote">Configurable Note</h2>

      <p>
        Because we cannot predict all of the attributes that you value in a player, we have integrated an editable note area into each player template.  This 
        is meant to serve as a general catch-all for any information that you want to add for a particular player.  To add a custom note about a player,
        click the 'plus' icon in the respective player template.  
      </p>
    
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-create-custom-note.gif" AlternateText="Create a custom note about an NFL player." />
      </div>
    </div>      
    
    
    <!-- Printable Sheets -->
    <div class="subSection">
    
      <h2 id="printableSheets">Generating a Printable Sheet</h2>
  
      <p>
        When you have completed your player rankings, it is time to generate a printable fantasy football cheat sheet for your draft.  This is the final document 
        that will integrate all of your player rankings (other important information) into a condensed, organized sheet for reference.  
      </p>

      <p>
        The first question you must ask yourself is if you want all of your positions integrated into a single cheat sheet, or if you want to 
        print-out one sheet per position.  If you choose the multi-sheet option, you will be able to add more information to your cheat sheet,
        such as your configurable note or player rankings from the previous season.  With the single-sheet option, you get the convenience of only
        having to reference a single sheet.  
      </p>

      <h3>Printing a Position-Specific Sheet</h3>

      <p>
        To print a position-specific cheat sheet, click on the 'print' icon underneath the cheat sheet dropdown in the sheet header.  
      </p>

      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-position-printable.gif" AlternateText="Generated a printable cheat sheet for a single fantasy position." />
      </div>

      <p>
        This will generate a printable sheet which contains your rankings for the respective position.  We provide a
        <a href="http://www.cheatsheetwarroom.com/fantasy-football/nfl/free/printable/offense/quarterbacks-cheat-sheet.aspx">free, printable position-specific cheat sheet</a>  
        which looks identical to the printable cheat sheet you'll be generating.
      </p> 

      <h3>Printing a Single Sheet with all Positions</h3>

      <p>
        To print a single cheat sheet with all of your fantasy positions included, click on the '1-Sheet Printable' link in the sheet header.
      </p>

      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-one-sheet-printable.gif" AlternateText="Generated a printable cheat sheet for a single fantasy position." />
      </div>

      <p>
        After you click the '1-Sheet Printable' link, you will be takent to a page where you can configure how your cheat sheet should be build.  You'll
        indicate the cheat sheets to use and the format you prefer.
      </p>

      <div class="indent">

        <h3>Choosing Ranking Sources</h3>
  
        <p>
          Our application allows you to create player rankings based on each of the major fantasy football positions (QB, RB, WR, TE, K, DEF).  You can even 
          create multiple sheets based a single position if you wish (this would be useful if you were preparing for multiple drafts in leagues with different scoring
          configurations).  When configuring your printable sheet, any fantasy positions for which you have created a single sheet 
          will be populated automatically next to their respective position.
        </p>

        <div class="imageContainer">
          <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-select-print-positions.gif" AlternateText="Choosing positions for fantasy football cheat sheet." />
        </div>
  
        <p>
          If you have created multiple sheets for a particular position, you will have to choose which sheet to be referenced when the printable sheet is generated.  
        </p>
        
        <div class="imageContainer">
          <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-select-print-sheet.gif" AlternateText="Choosing positions for fantasy football cheat sheet." />
        </div>
        
        <p>          
          The positions for which you have not created a sheet for will be populated by NFL player rankings from one of the available supplemental sources.
          This will allow you to create a complete cheat sheet, even if you don't have time to address all positions yourself.
        </p>
        
        <div class="imageContainer">
          <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-select-supplemental-source.gif" AlternateText="Choosing positions for fantasy football cheat sheet." />
        </div>

        <h3>Choosing Your Printed Sheet Format</h3>
        <p>
          There are currently two formats to choose from when generating your printable cheat sheet.  Both sheet formats display player rankings based on 
          your specified configuration and also a printable roster area to enter your draft picks.  The difference between the two formats is that one 
          integrates the roster area into a single sheet while the other provides the roster area on a separate sheet.  The single sheet solution has 
          a smaller number of players listed and you will need to examine both formats to decide which format is right for you.  
        </p>
  
        <p>
          If your league rules dictate small roster sizes, then the single-sheet 
          solution may work best.  But if your league configuration calls for a larger roster size (15+ players) it would probably be safer to utilize the 
          format which offers a larger list of players and places the roster area on a second sheet.  Both sheet formats are previewed below.

        </p>
      </div> <!-- close indent -->
      
      <div class="printed">
      
        <table>
          <tr>
            <th>
              Printed Sheet Format 1
            </th>
            <th>
              Printed Sheet Format 2
            </th>
          </tr>
          <tr>
            <td>
              <cswr:HoverImage runat="server" ID="hiCheatSheetWithRoster" CaptionText="Cheat sheet with roster." 
                SmallImageURL="~/Images/UserControls/HoverImage/cheatsheetwithroster-small.gif" BigImageURL="~/Images/UserControls/HoverImage/cheatsheetwithroster-big.gif" />  
            </td>
            <td>
              <table>
                <tr>
                  <td>
                    <cswr:HoverImage runat="server" ID="hiCheatSheetWithoutRosterPage1" CaptionText="Cheat Sheet Without Roster" 
                      SmallImageURL="~/Images/UserControls/HoverImage/cheatsheetwithoutroster-small.gif" BigImageURL="~/Images/UserControls/HoverImage/cheatsheetwithoutroster-big.gif" />  
                  </td>
                  <td>
                    <cswr:HoverImage runat="server" ID="hiCheatSheetWithoutRosterPage2" CaptionText="Roster and Custom Notes" 
                      SmallImageURL="~/Images/UserControls/HoverImage/cheatsheetwithoutrosterpage2-small.gif" BigImageURL="~/Images/UserControls/HoverImage/cheatsheetwithoutrosterpage2-big.gif" />  
                  </td>
                </tr>
              </table>
            </td>
          
          </tr>
        </table>
      
      </div> <!-- close printed sheets -->
    
    

    </div> <!-- close subSection -->
    
        
  </div> <!-- close help page -->
    

  
  


</asp:Content>

