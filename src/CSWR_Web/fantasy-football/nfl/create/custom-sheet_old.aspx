<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPages/Sport.master" Theme="Web20" AutoEventWireup="true" 
  CodeFile="custom-sheet_old.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.CustomSheet" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage" 
  Title="Custom Fantasy Football Rankings - Create a Drag & Drop Cheat Sheet" 
  MetaDescription="Easily create your custom fantasy football rankings on an interactive, drag & drop cheat sheet."
  CanonicalUrl="https://www.cheatsheetwarroom.com/fantasy-football/nfl/create/custom-sheet.aspx"
%>
<%@ Register Src="~/usercontrols/Sports/Football/CheatSheet/FOOSheetItemTemplate.ascx" TagName="FOOSheetItemTemplate" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/AddThis.ascx" TagName="AddThis" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/navigation/SheetCreationManageLevelNavigation.ascx" TagName="SheetCreationManageLevelNavigation" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/navigation/SheetCreationSheetLevelNavigation.ascx" TagName="SheetCreationSheetLevelNavigation" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/navigation/SheetVisitorNavigation.ascx" TagName="SheetVisitorNavigation" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/ads/AdGenerator.ascx" TagName="AdGenerator" TagPrefix="cswr" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">

    
    <style>
        a.fpIcon {background-color:orange;margin-top:-3px;}
    </style>


  <asp:ScriptManager runat="server" EnablePageMethods="true" />

  <div class="customSheetContainer">

    <%--Navigation--%>
    <cswr:SheetCreationManageLevelNavigation runat="server" ID="scmlnNavigation" CurrentStage="CUSTOMSHEET" SportCode="FOO" />

    <%--Visitor Buttons--%>
    <cswr:SheetVisitorNavigation runat="server" ID="svnNavigation" SportCode="FOO" />
 
    <%--No Sheets Configured--%>
    <cswr:MessageBox runat="server" id="mbNoSheets" WidthPercentage="50"/>
    
    <%--Sheet Header Controls--%>
    <asp:Panel runat="server" ID="panSheetHeaderControls" CssClass="topControlsContainer">

        <div class="alert alert-success text-center personalAd" style="width:80%;margin-left:auto;margin-right:auto; display: none;">

          <asp:Panel runat="server" ID="panMemberMessageContainer" Visible="false">
            <a target="_blank" href="https://www.facebook.com/CheatSheetWarRoom/">Like our Facebook page</a> and earn an <asp:Literal runat="server" ID="litAdditional" Visible="false">additional</asp:Literal> entry into our 
            drawing for a <strong>$100 shopping spree</strong> at 
            <a target="_blank" href="https://www.fantasyjocks.com/?rfsn=105771.96046&subid=contest-likefacebookpage">FantasyJocks.com</a> to be
            held on 8/31.
          </asp:Panel>

          <asp:Panel runat="server" ID="panVisitorMessageContainer" Visible="false">
            <asp:HyperLink runat="server" NavigateUrl="~/access/register.aspx">Register for free</asp:HyperLink>,
            create your first sheet, and automatically earn an entry into our drawing for a <strong>$100 shopping spree</strong> at
            <a target="_blank" href="https://www.fantasyjocks.com/?rfsn=105771.96046&subid=contest-register">FantasyJocks.com</a> to be held
            on 8/31.
          </asp:Panel>

          <br />

          <p>
            Fantasy Jocks sells 
            <asp:HyperLink runat="server" Target="_blank" NavigateUrl="https://www.cheatsheetwarroom.com/blog/fantasy-football/draft-boards">fantasy draft boards</asp:HyperLink>
            and a wide selection of fantasy prizes including
            <asp:HyperLink runat="server" Target="_blank" NavigateUrl="https://www.cheatsheetwarroom.com/blog/fantasy-football/prizes/championship-belts">fantasy football belts</asp:HyperLink>
            and
            <asp:HyperLink runat="server" Target="_blank" NavigateUrl="https://www.cheatsheetwarroom.com/blog/fantasy-football/prizes/championship-rings">fantasy football rings</asp:HyperLink>.
          </p>

        </div>

        <div class="sheetControls">

            <%--Dropdown--%>
            <div class="dropDownContainer">
              <asp:DropDownList runat="server" ID="ddlAvailableSheets" DataTextField="SheetName" DataValueField="CheatSheetID" Width="250" 
              OnSelectedIndexChanged="ddlAvailableSheets_SelectedIndexChanged" AutoPostBack="True" OnDataBound="ddlAvailableSheets_DataBound"></asp:DropDownList>
              <asp:Label runat="server" ID="labPPRLeague" Text="(PPR)" CssClass="ppr" Visible="false"></asp:Label>
            </div>

            <%--Buttons--%>
            <asp:Panel runat="server" ID="panSheetButtons" CssClass="buttons">
              <cswr:SheetCreationSheetLevelNavigation runat="server" ID="scplnPageLevelNavigation" SportCode="FOO" />
              <asp:HyperLink runat="server" ID="hlRegisterNow" NavigateUrl="~/access/register.aspx">Register to enable advanced features</asp:HyperLink>
            </asp:Panel>

        </div>  <!-- close sheetControls -->

        <%--<div class="alert alert-success alert-dismissible" style="margin-left:auto;margin-right:auto;width:70%;margin-top:40px;">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <p style="text-align:center;">
                Want to help support us while also getting an <strong>awesome deal</strong> on merchandise?
            </p>
            <p style="text-align:center;font-weight:bold;margin:20px 0px;font-size:18px;">
                Amazon Prime Day is coming (July 16th)  
            </p>
            <p style="text-align:center;">
                If you 
                <a target="_blank" rel="nofollow" href="https://www.amazon.com?&_encoding=UTF8&tag=cswr-customsheetmessage-20&linkCode=ur2&linkId=aa910c46e77a4025015ce7fe5603efd9&camp=1789&creative=9325">use this link to shop at Amazon</a><img src="//ir-na.amazon-adsystem.com/e/ir?t=cswr-customsheetmessage-20&l=ur2&o=1" width="1" height="1" border="0" alt="" style="border:none !important; margin:0px !important;" />,
                or sign-up for a 
                <a target="_blank" rel="nofollow" href="https://www.amazon.com/b?node=13887280011&ref_=assoc_tag_ph_1524210592968&_encoding=UTF8&camp=1789&creative=9325&linkCode=pf4&tag=cswr-customsheetmessage-20&linkId=df8ede099e37700de0fd93a99e92d4aa">
                    free 30-day trial Prime membership 
                </a>
                , we'll get a cut and use it to make this site <strong>even better</strong>.
            </p>
        </div>--%>

 
    </asp:Panel> <!-- close topControlsContainer -->

    <br/><br/>
    <!-- The actual racing cheat sheet -->
    
    <asp:Panel runat="server" ID="panFootballSheet" CssClass="footballList">
      <asp:Repeater runat="server" ID="repFootballSheet" OnItemDataBound="repFootballSheet_ItemDataBound">
        <HeaderTemplate>
          <ul id="sortable">
        </HeaderTemplate>
        <ItemTemplate>
          <li runat="server" id="liPlayerItem">
            <cswr:FOOSheetItemTemplate runat="server" ID="fssiTemplate" EnableViewState="false"/>
          </li>
        </ItemTemplate>
        <FooterTemplate>
          </ul>
        </FooterTemplate>
      </asp:Repeater>
    </asp:Panel>
  </div>  <!-- close customSheetContainer -->

  
  <div class="sheetHelpContainer" id="cheatSheetHelp">
  
    <h1>How to Create Custom Fantasy Football Rankings Using This Cheat Sheet </h1>
  
    <!-- Intro -->  
    <div class="intro">
      <p>
        This 
        <strong>custom fantasy football cheat sheet</strong>
        creation tool allows your to create informative fantasy cheat sheets in a fun and intuitive manner.  
        This customizable cheatsheet provides a wide-range of functionality including: 
      </p>
        
      <ul>
          <li><strong>Customized rankings</strong> using drag & drop</li>
          <li>Fantasy Rankings from other expert sites to compare against</li>
          <li>Player info such as name, team positions, number, age, & <asp:HyperLink runat="server" NavigateUrl="https://www.cheatsheetwarroom.com/blog/football/bye-weeks" Text="bye week" /></li>
          <li>Relevant statistics for each fantasy position</li>
          <li>Fantasy point output calculations for easy comparisons</li>
          <li>A configurable note for each player</li>
          <li>Tags to easily mark a player as a bust, sleeper, or injured</li>
      </ul>
    
    <h2>This football cheat sheet tool is completely free</h2>
      <p>
          It is absolutely free to use this custom football cheat sheet creator.  Without registering, you can rank fantasy players
          for all six primary fantasy football positions, view their statistics from the previous season, quickly access news
          on those players, and more.
      </p>
      <p>
        However, to take advantage of the full functionality available in this tool, you must first 
        <asp:Hyperlink runat="server" NavigateUrl="~/access/register.aspx">register for a free account</asp:Hyperlink>.
        I'll never ask you for money, this custom sheet is completely free.
      </p>
    
    <h2>Click on any of these links to jump to an explanation of that custom cheat sheet feature:</h2>
    <br/>

      <ol>
        <li>
          <a href="#registration">Registering for a free account</a>
        </li>
        <li>
          <a href="#sheetCreation">Creating a custom cheat sheet</a>
        </li>
        <li>
          <a href="#manageSheets">Managing your cheat sheets</a>
        </li>
        <li>
          <a href="#editSheet">Editing a cheat sheet's properties</a>
        </li>
        <li>
          <a href="#rankingPlayers">Ranking players on your custom sheet</a>
        </li>
        <li>
          <a href="#searchNews">Search players news from your sheet</a>
        </li>
        <li>
          <a href="#depthChartView">Utilize team depth chart view for quick team analysis</a>
        </li>
        <li>
          <a href="#expertRankings">View expert rankings as you edit your sheet</a>
        </li>
        <li>
          <a href="#sheetValidation">Validating your cheat shet against experts</a>
        </li>
        <li>
          <a href="#playerInformation">Quickly view important player information</a>
        </li>
        <li>
          <a href="#configurableTags">Utilize configurable tags for sleepers or busts</a>
        </li>
        <li>
          <a href="#playerStatistics">View detailed and summary player statistics</a>
        </li>
        <li>
          <a href="#configurableNote">Keep notes on any players in your sheet</a>
        </li>
        <li>
          <a href="#printableSheets">Create a draft-ready printable cheat sheet</a>
        </li>
      </ol>

    </div>  


    <!-- Registration -->    
    <div class="subSection">
      <h2 id="registration">Register for a free account!</h2>  
      <p>
        In order for us to remember who you are (and more importantly <strong>which fantasy cheat sheets belong to you</strong>), you must 
        <asp:Hyperlink runat="server" NavigateUrl="~/access/register.aspx">register for a free account</asp:Hyperlink>.
        After you register, the application will remember which sheets belong to you and make them accessible each time you return to the site.  If you do 
        not register, any cheat sheets you have created or edited will be lost when your session expires.  
      </p>
      
      <p>
          Registration also unlocks the ablity for advanced cheat sheet features:
      </p>

      <ul>
        <li>
          Create any number of <strong>customizable football cheat sheets</strong> for any number of fantasy football leagues
        </li>
        <li>
          Add and remove players from your cheat sheet
        </li>
        <li>
          Generate a 
          <asp:HyperLink runat="server" NavigateUrl="~/fantasy-football/nfl/free/printable/offense/cheat-sheet-with-roster.aspx" Target="_blank">printable football cheat sheet</asp:HyperLink>
          using your custom rankings.
        </li>
        <li>
          Export your football sheet rankings to a formatted spreadsheet for custom analysis
        </li>
        <li>
          Order your players using either statistics or supplemental 
          <asp:Hyperlink runat="server" NavigateURl="~/fantasy-football/nfl/free/rankings/player-rankings.aspx" Target="_blank"> rankings</asp:Hyperlink> from other experts.
        </li>
        <li>
          Validate your NFL cheat sheet against other fantasy experts 
        </li>
      </ul>
    </div>
    
    <!-- Cheat Sheet Creation -->
    <div class="subSection">
      <h2 id="sheetCreation">Creating your custom fantasy football cheat sheet</h2>
      
      <p>When you initially <strong>create a custom football cheat sheet</strong> using this tool, all of the available data and functionality will be 
        integrated into your sheet 
        automatically (assuming you're registered).  
        You can create custom sheets based on one fantasy position or combine multiple positions into a single cheat sheet.</p>

      <h3>Players added to a single position cheat sheet</h3>        

      <p>
        When you create a cheat sheet based on a single fantasy position, the number of players that are initially added to your sheet will depend 
        on the fantasy position on which your sheet is based.</p>
      
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/custom-sheet-one-position.gif" AlternateText="Single Position Cheat Sheet Configuration"/>
      </div>
      
      <p>The
        number of players chosen reflects the maximum number of players (for each respective position) that can be exported to our single-page,
        <asp:Hyperlink runat="server" NavigateUrl="~/fantasy-football/nfl/free/printable/offense/cheat-sheet-with-roster.aspx" Target="_blank">printable fantasy football cheat sheet</asp:Hyperlink>
        with roster area:
      </p>
    
      <ul>
        <li><strong>Quarterbacks:</strong> 40 Players</li>
        <li><strong>Running Backs:</strong> 75 Players</li>
        <li><strong>Wide Receivers:</strong> 75 Players</li>
        <li><strong>Tight Ends:</strong> 35 Players</li>
        <li><strong>Kickers:</strong> 32 Players</li>
        <li><strong>Defenses:</strong> 32 Players</li>
      </ul>
      
      <p>
        If you need more players than are added to a sheet by default, you can add as many players as you want by 
        <a href="#editSheet">editing your sheet</a>.
      </p>
      
      <h3>Custom sheets based on multiple positions</h3>
      
      <p>If you choose to create a customized football sheet based on multiple positions, the number of players added to your sheet is determined
        by positions chosen.</p>

      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/custom-sheet-multiple-positions.gif" AlternateText="Single Position Cheat Sheet Configuration"/>
      </div>
      
      <p>Simply add together the players that would be added on a single sheet based on the positions selected.
        The one exception is that we cap the maximum players that are added to a sheet by default at 200.  However,
        you can add as many players to your sheet as you choose, as we'll explain next.
      </p>
    </div>
    
    
    <!-- Editing a Sheet -->
    <div class="subSection">
      <h2 id="manageSheets">Quickly access features on the Manage Sheets page</h2>
      <p>
        The Manage Sheets page is a centralized location to access and configure each custom cheat sheet that you've created.  It's a quick
        way to determine 
        <strong>how many players</strong> are on your sheets, when they were 
        <strong>last updated</strong>, the 
        <strong>positions included</strong> in the sheet, etc.
     </p>
      
     <div class="imageContainer">
       <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/manage-custom-sheets-interface.gif" AlternateText="Manage Cheat Sheets Interface"/>
     </div>
      
      <p>
        You can also quickly access <strong>cheat sheet-specific features</strong> from the Manage Sheets page using the buttons beside each sheet:
      </p>

      <ul>
        <li>Access the 
          <a href="#editSheet">Edit Sheet page</a>
           
          for each cheatsheet</li>
        <li>
          <a href="#sheetValidation">Validate a sheet</a>
          against rankings from other experts
        </li>
        <li>Quickly jump to an existing sheet</li>
        <li>Generate a 
          <a href="#printableSheets">printable, position-specific version</a>
           of your sheet</li>
        <li>Delete one or more sheets</li>
      </ul>
      
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/manage-sheets-features.gif" AlternateText="Manage Cheat Sheets Features"/>
      </div>

    </div>    
    
    

    <!-- Editing a Sheet -->
    <div class="subSection">
      <h2 id="editSheet">Edit your cheatsheet to add or remove players</h2>
      <p>
        If you are a registered user, you can edit your cheat sheet settings at any time.  You'll 
        edit your sheet when you want to either rename your sheet or add/remove players from your customized sheet.
      </p>
      
      <p>To edit a fantasy cheat sheet, click the <em>edit button</em> under your sheet dropdown at the
        top of your sheet.
      </p>
      
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/edit-custom-sheet-icon.gif" AlternateText="Button to edit your custom cheat sheet"/>
      </div>
      
      <p>
        On the Edit Sheet page, you can change the sheet name at the top.  To add or remove players from your cheatsheet,
        simply select the player and click on the arrow to move them between your sheet and the player pool.
      </p>
      
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/edit-custom-sheet-form.gif" AlternateText="Form for editing your custom chestsheet properties."/>
      </div>
  
      <p>Other cheat sheet properties may be available depending on the time of the year and whether the NFL season has started.</p>

    </div>

    <!-- Rankings Players -->
    <div class="subSection">
      <h2 id="rankingPlayers">Ordering players on your cheat sheet</h2>
      <p>
        The <strong>most important feature of your custom cheat sheet</strong> is the ability to order your players.  To change a 
        player’s ranking you first need to position your cursor over the <em>drag handle</em> (the portion of the player template nex to the player
        rank containing the team colors), then click & hold down the left mouse button to select the player. 
      </p>
    
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-selecting-player.gif" AlternateText="Selecting a football player to rank." />
      </div>
    
      <p>      
        After clicking your left mouse button, the player template will become <em>draggable</em>.  Still holding down the mouse button,
        you should now be able to drag the player to a different position on your cheat sheet. The position where the player will be 
        dropped should be highlighted as your move your player up and down.
      </p>
  
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-dragging-player.gif" AlternateText="Dragging a football player to a different position." />
      </div>
      
      <p>When you change a player's ranking on your custom sheet, the change is saved automatically!</p>
    </div>

    <!-- Search Player News -->
    <div class="subSection">
      <h2 id="searchNews">Researching player news as you customize your sheet</h2>
      <p>
        When updating your NFL cheat sheets, it is imperative that you stay current on player news throughout the league.  The NFL preseason is a very 
        volatile time and any tidbit of information you can garner on a player will give you a critical edge.</p>
      
      <p>To simplify the process of searching for player news, we provide a Google News Search button to each
        player template.  Simply click on the newspaper button next to the player that you want to research.
      </p>
      
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-news-search.gif" AlternateText="Searching fantasy football player news." />
      </div>

      <p>After cliking on the news button, the most recent Google News Search results will be displayed in a separate window.</p>
      
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/cheat-sheet-search-results.gif" AlternateText="Google News player search results from cheat sheet."/>
      </div>
      

    </div>
    
    <!-- Depth Chart View -->
    <div class="subSection">
      <h2 id="depthChartView">Quickly evaulate players on a team with depth chart view</h2>
      <p>
        When considering how to rank a particular player on a cheat sheet, it's common to consider how other 
        players on the same team are ranked.  For instance, if I was trying to rank Alvin Kamara, I might want
        to quickly check where Mark Ingram is ranked on my sheet.
      </p>
      
      <p>I may even want to see how other experts have those two player ranked.  All of this information is made quicly available
        with the <strong>depth chart view</strong> feature.  Just hover your mouse over the <em>depth chart button</em> and you'll 
        see all of this data at a glance.
      </p>
      
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/cheat-sheet-depth-chart-feature.gif" AlternateText="Custom cheat sheet depth chart view" />
      </div>
      
      <p>You can even <em>jump to</em> the oter player within your cheat sheet by clicking on his name in the depth cart view popup.</p>
      
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/cheat-sheet-depth-chart-link.gif" AlternateText="Custom cheat sheet depth chart view" />
      </div>

    </div>


    <!-- Supplemental Rankings -->
    <div class="subSection">
    <h2 id="expertRankings">Viewing expert rankings from other sites within the sheet</h2>
      <p>
        It is often worthwhile to double-check your cheat sheet rankings against football sheets maintained by other fantasy experts. 
        In each of our player templates we include that player's ranking from one or more fantasy experts.
      </p>  

      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-supplementals.gif" AlternateText="Football player template with supplemental rankings." />
      </div>

      <p>
        If you hover your mouse above the magnifying glass next to the expert rankings area, a popup will appear which indicates which 
        expert created the ranking and a link to their cheat sheet.
      </p>  
    
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-supplemental-popup.gif" AlternateText="Football player template with supplemental popup." />
      </div>
      
      <p>This feature is a nice way to casually evaluate your cheat sheet rankings against others as you work on your sheet.  But as we'll see
        next there is much easier way to <strong>perform a sanity check of your entire sheet</strong> with the click of a button.
      </p>
    </div>
    


  <!-- Supplemental Rankings -->
  <div class="subSection">
    <h2 id="sheetValidation">Validate your custom sheet against industry experts</h2>
    <p>
      It's useful to compare your rankings against experts as you rank, but sometimes you want to quickly run
      a sanity check on <strong>your entire cheat sheet</strong> to ensure you're not too far off with any of your rankings.
    </p>  
    
    <p>
      In order to accomplish this, we provide a <strong>validate sheet </strong>feature.  To access this feature, simply click
      the checkmark button under your cheat sheet drop down.
    </p>
    
    <div class="imageContainer">
      <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/validate-sheet-button.gif" AlternateText="Button to validate a custom sheet."/>
    </div>
    
    <p>
      After you click the <em>validate sheet</em> button, you'll be taken to the Validate Sheet page where you can quickly see how your rankings
      stack-up against the expert your cheat sheet is based upon.  This page is packed with <strong>actionable data</strong> and
      <strong>quick correction buttons</strong> to easily fine-tune your sheet:
    </p>
    
    <ul>
      <li>View and adjust players you have <strong>ranked too high</strong></li>
      <li>View and adjust players you have <strong>ranked too low</strong></li>
      <li>Suggestions for players <strong>you should add</strong> to your sheet (with quick add buttons)</li>
      <li>Suggestions for players <strong>you should remove</strong> to your sheet (with quick remove buttons)</li>
      <li>A color-coded snapshot of your entire cheat sheet problematic players highlighted</li>
      <li>Variable validation sensitivity</li>
    </ul>

    <p>
    Here is a snapshot of the Validation Sheet page and the many features it contains.
    </p>
    
    <div class="imageContainer">
      <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/validate-cheat-sheet-page.gif" AlternateText="Interface for validating football cheat sheets"/>
    </div>

  </div>
    
    

    <!-- Player Information -->
    <div class="subSection">
      <h2 id="playerInformation">Player info integrated into each sheet</h2>
      <p>
        Each <em>player template</em> that comprises your cheat sheet includes useful information about that player:
      </p>

      <ul>
        <li><strong>Player Name</strong></li>
        <li><strong>Position</strong> - Useful if your sheet includes multiple positions</li>
        <li><strong>Number</strong> - Not very useful but pertinent</li>
        <li><strong>Team</strong> - Necessary for players that you're unfamiliar with</li>
        <li><strong>Experience</strong> - The number of years the player has played in the NFL</li>
        <li><strong>Bye Week</strong> - Click to view all bye weeks for all teams</li>
        <li><strong>Age</strong> - Useful because players start to decline at different ages</li>
      </ul>
 
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-player-info.gif" AlternateText="Football player template with supplemental popup." />
      </div>
    </div>
    

    <!-- Configurable Tags -->
    <div class="subSection">
      <h2 id="configurableTags">Easily mark a player a sleeper, bust, or injured</h2>
      <p>
        In fantasy football, there are common <em>attributes</em> that we frequently assign to a player, two of the most common being 
        <strong>sleeper</strong> and <strong>bust</strong>.  Because these attributes are so common, we created a simple way to 
        <em>tag</em> a player with one of
        these attributes (as opposed to manually typing 'sleeper' or 'bust' in the player's 
        <a href="#configurableNote">configurable note area</a>).
      </p>
      <p>
        We also threw in a <em>injured</em> tag so you can easily spot those players who are hobbled. Tagging a player as a <em>sleeper</em>, <em>bust</em>, or 
        <em>injured</em> is as simple as clicking on the 'Sleeper', 'Bust', or 'Injured' buttons under a player's name.  To 'un-tag' a player, you can simply 
        click on the tag again to de-activate it.  
      </p>
    
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-tag-player.gif" AlternateText="Football player tagging as sleeper, bust, or injured." />
      </div>
      
      <p>  
        When you generate any 
        <a href="#printableSheets">printable version</a>
        of your football cheat sheet,
        tags that you activated on your cheat sheet will be transferred and placed next to the respective player for easy reference.    
      </p>
      <br/>
      
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/tags-in-printable-cheat-sheet.gif" AlternateText="Player Tag in Printable Sheet"/>
      </div>
    </div>
    


    <!-- Relevant Statistics -->
    <div class="subSection">
      <h2 id="playerStatistics">Condensed and actionable players statistics</h2>
      <p>
        Player templates display the following <strong>summary statistics</strong> from the previous year to easily compare players.  These statistics
        are generated based on fantasy point output using the standard
        <a href="https://www.cheatsheetwarroom.com/blog/fantasy-football/leagues/scoring-systems">fantasy football scoring</a> system.
      </p>
      <ul>
        <li>Total Fantasy Points (<abbr title="Total Fantasy Points">TFP</abbr>)</li>
        <li>Total Fantasy Points Rank</li>
        <li>Fantasy Points Per Game (<abbr title="Fantasy Points Per Game">FPPG</abbr>)</li>
        <li>Fantasy Points Per Game Rank</li>
      </ul>
      
      <p>
        The reason we rank players based on <strong>Total Fantasy Points</strong> and <strong>Total Fantasy Points per Game</strong> is so
        that you can easily compare players in your sheet <em>at a glance</em> and <em>in the proper context</em>.</p>
      
      <p>For instance, we can see below that Desean Watson
        was only the 25th ranked QB in total fantasy points, but <strong>he was the top QB in the league</strong> in 2017 when you consider his 
        fantasy point per game
        output.  That type of data is critical to know when you're manipulating your sheet.
      </p>
      <br/>
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
      <h2 id="configurableNote">Keep notes on players as you configure your sheet</h2>

      <p>
        Because we all have our own criteria for determining a player's rank, we have integrated an 
        <strong>editable note area</strong> into each player template.  This 
        is meant to serve as a general catch-all for any information that you want to note for a particular player.</p>
      
      <p>This information will be preserved in your sheet and available when you generate a printable version of your sheet.
        To add a custom note about a player, click the <em>pencil button</em> in the respective player template.  
      </p>
    
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-create-custom-note.gif" AlternateText="Create a custom note about an NFL player." />
      </div>
    </div>      
    
    
    <!-- Printable Sheets -->
    <div class="subSection">
    
      <h2 id="printableSheets">Generating a printable cheat sheet for your draft</h2>
  
      <p>
        When you have completed your player rankings, it is time to generate a printable fantasy football cheat sheet for your draft.  
        This is the final sheet that will integrate all of your player rankings (as well as tags, notes, etc) into a condensed, organized football sheet.  
      </p>

      <p>
          As you're placing your picks on your league's 
          <a href="https://www.cheatsheetwarroom.com/blog/fantasy-football/draft/boards">fantasy football draft board</a>,
          use your cheat sheet to help dictate your picks.
      </p>

      <p>
        When deciding on the format for your printable rankings, the first question you must ask yourself is if you want all of your positions 
        integrated into a <strong>single cheat sheet</strong>, or if you want to 
        print-out <strong>one sheet per position</strong>.</p>
      
      <p>If you choose the multi-sheet option (one position per sheet), you will be able to add more information to your cheat sheet,
        such as your configurable note or player rankings from the previous season.  With the single-sheet option, you get the convenience of only
        having to reference a single sheet- but notes will be included on a second sheet.  
      </p>

      <h3>Quickly generate a position-specific sheet</h3>

      <p>
        To generate a position-specific cheat sheet, click on the 'print' button underneath the cheat sheet dropdown in the sheet header.  
      </p>

      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-position-printable.gif" AlternateText="Generated a printable cheat sheet for a single fantasy position." />
      </div>

      <p>
        This will generate a printable sheet which contains your rankings for the respective position.  If you want to see what 
        a positional cheat sheet looks like, check out our 
        <a target="_blank" href="https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/printable/offense/quarterbacks-cheat-sheet.aspx">free QB cheat sheet</a>  
        which looks identical to the printable cheat sheet you'll be generating.
      </p> 
      
      <p>
        The one big bonus of printing position-specific sheets is that we'll also add any notes you've added about a player right next to
        their name in your printable football sheet:
      </p>
      
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/position-sheet-player-note.gif" AlternateText="Player Note in Positional Cheat  Sheet"/>
      </div>

      <h3>Printing a single cheat sheet with all positional rankings</h3>

      <p>
        To print a single cheat sheet with all of your fantasy positions included, click on the '1-Sheet Printable' link in the sheet header.
      </p>

      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-one-sheet-printable.gif" AlternateText="Generated a printable cheat sheet for a single fantasy position." />
      </div>

      <p>
        After you click the <em>1-Sheet Printable</em> option, you will be taken to a page where you can configure 
        <strong>how your printable cheat sheet should be formatted</strong>.
        You'll indicate which individual customized cheat sheets to be included (in case you've created multiple sheets based on
        the same position for various
        fantasy leagues) and the printable format you prefer.
      </p>

      <div class="indent">

        <h3>Choosing Ranking Sources</h3>
  
        <p>
          Our application allows you to create cheat sheets based on each of the major fantasy football positions (QB, RB, WR, TE, K, DST).  You can even 
          create multiple sheets based a single position if you wish (this would be useful if you were preparing for multiple drafts in leagues with different scoring
          configurations).</p>
       
        <p>When configuring your printable sheet, any fantasy positions for which you have created only one sheet 
          will be populated automatically next to their respective position.  For these you don't have to do anything.
        </p>

        <div class="imageContainer">
          <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-select-print-positions.gif" AlternateText="Choosing positions for fantasy football cheat sheet." />
        </div>
  
        <p>
          If you have created multiple sheets for a particular position, you will have to choose which
          of those sheets to be referenced when the printable sheet is generated.  
        </p>
        
        <div class="imageContainer">
          <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-select-print-sheet.gif" AlternateText="Choosing positions for fantasy football cheat sheet." />
        </div>
        
        <p>          
          The positions for which you have not created a sheet for will be populated by NFL player rankings from one of the available expert sources.
          This will allow you to create a complete printable cheat sheet, even if you didn't create your own rankings for a few positions.
        </p>
        
        <div class="imageContainer">
          <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/Help/football-cheat-sheet-select-supplemental-source.gif" AlternateText="Choosing positions for fantasy football cheat sheet." />
        </div>

        <h3>Choosing your preferred printed sheet format</h3>
        <p>
          There are currently two formats to choose from when generating your printable cheat sheet.  Both sheet formats display player rankings based on 
          your specified cheat sheets and also include a printable roster area to enter your draft picks.</p>
        
        <p>The difference between the two formats is that one 
          integrates the roster area into a single sheet print-out while the other provides the roster area on a separate sheet.  
          Thus, the
          <strong> single sheet solution has a smaller number of players</strong> and the 
          <strong> multi-sheet solution has a larger number of players</strong>. 
        </p>
        <p>
           You will need to examine both formats to decide which format is right for you depending on the number of
          players chosen at your draft. 
          If your league rules dictate small roster sizes, then the 
          <strong>single-sheet solution may work best</strong>.
        </p>
        <p>
          But if your league configuration calls for a larger roster size (15+ players), with 12 or more teams, it would probably be 
          safer to utilize the multi-sheet option which offers a larger list of players and places the roster area on a second sheet.  
          Both sheet formats are previewed below.
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
  

    

  <%--Hidden field used by JQuery to populate links to supplemental rankings--%>
  <div id="jqueryFields">
    <asp:HiddenField runat="server" ID="hfPositionCode" EnableViewState="false"/>
    <asp:HiddenField runat="server" ID="hfPPRLeague" EnableViewState="false" />
    <asp:HiddenField runat="server" ID="hfSheetID" EnableViewState="false" />
    <asp:HiddenField runat="server" ID="hfStatSeason" EnableViewState="false" />
    <asp:HiddenField runat="server" ID="hfDepthChartPopupWidth" EnableViewState="false" />
  </div>



  <!-- This is the JQuery to show the source of the supplemental rankings -->
  <script type="text/javascript">
    
    // Create the tooltips only on document load
    $(document).ready(function () {

      // Get the position code for later user
      var positionCode = $("#jqueryFields #<%= hfPositionCode.ClientID %>").val();
      // Get the PPRLeague settings to we know how to configure the stat popup
      var pprLeague = $("#jqueryFields #<%= hfPPRLeague.ClientID %>").val();
      // Get the SheetID so we know which players to query for the team player rankings
      var sheetID = $("#jqueryFields #<%= hfSheetID.ClientID %>").val();
      // Get the StatSeason so we know which year to display in the headers
      var statSeason = $("#jqueryFields #<%= hfStatSeason.ClientID %>").val();
      // Get the width of the depth chart button popup, based on positions in sheet
      var depthChartPopupWidth = $("#jqueryFields #<%= hfDepthChartPopupWidth.ClientID %>").val();


      /* ******************/
      /*    Source Popup    */
      /********************/

      var templateCounter = 0;  // a counter to mark which player I'm processing
      $('.supplementalRankSection img').each(function () {

        // get a reference to the composite field in the player template
        var itemPropertiesField = $(".itemPropertiesContainer:eq(" + templateCounter + ") input[type=hidden]").val();
        templateCounter++;

        // break the composite field into its constituent parts
        var itemProperties = itemPropertiesField.split('_');
        var cswrRank = itemProperties[2];
        var cbsRank = itemProperties[3];
        var adp = itemProperties[4];
        var mascot = itemProperties[5];
        var cssMascot = mascot.toLowerCase();
        if (mascot == '49ers') {
            cssMascot = 'fortyNiners';
        }

        $(this).qtip({
          content: {
            text: 'Loading...',
            title:
            {
              text: "Supplemental Rankings"
            }
            ,
            ajax: {
              url: 'qtipsources/suppsources.aspx',
              data: { adp: adp, cswr: cswrRank, cbs: cbsRank, positionCode: positionCode },
              type: 'get'
            }
          },
          show: {
            delay: 250  /* Delay slightly so that 'skimming over' a popup trigger doesn't issue an unnecessary AJAX request */
          }, 
          position: {
            my: 'leftMiddle',
            at: 'rightMiddle'
          },
          hide:
          {
            event: 'mouseout',
            fixed: true
          },
          style:
          {
            classes: "ui-tooltip-football ui-tooltip-" + cssMascot + " ui-tooltip-shadow",
            width: 150,
            height: 80
          }
        }); /* close qtip */

      });  /* close each */



      /* ******************/
      /*    Depth  Chart  Feature buttons    */
      /********************/
      templateCounter = 0;  // a counter to mark which player I'm processing
      $('.featureIconsContainer .depth').each(function () {

        // get a reference to the composite field in the player template
        var itemPropertiesField = $(".itemPropertiesContainer:eq(" + templateCounter + ") input[type=hidden]").val();
        templateCounter++;

        // break the composite field into its constituent parts
        var itemProperties = itemPropertiesField.split('_');
        var playerID = itemProperties[0];
        var sheetType = itemProperties[6];
        var mascot = itemProperties[5];
        var cssMascot = mascot.toLowerCase();
        if (mascot == '49ers') {
            cssMascot = 'fortyNiners';
        }

        $(this).qtip({
          content: {
            text: 'Loading...',
            title:
            {
              /* Build the title dynamically to have player name and statistics year */
                text: "Your " + mascot + " " + positionCode + " Rankings"
            },
            ajax: {
              url: 'qtipsources/teamplayerranks.aspx',
              data: { playerid: playerID, sheetid: sheetID, sheettype: sheetType },
              type: 'get',
              once: false
            }
          },
          show: {
            delay: depthChartPopupWidth
          },
          position: {
            my: 'leftMiddle',
            at: 'rightMiddle'
          },
          hide:
          {
            event: 'mouseout',
            fixed: true
          },
          style:
          {
            classes: "ui-tooltip-football ui-tooltip-" + cssMascot + " ui-tooltip-shadow",
            width: 240
          }
        });

      });    /* close each */


      /* ****************************************/
      /*    Player Previous Year Stats Popup    */
      /* ****************************************/
      templateCounter = 0;  // a counter to mark which player I'm processing
      $('.statsContainer').each(function () {

        // get a reference to the composite field in the player template
        var itemPropertiesField = $(".itemPropertiesContainer:eq(" + templateCounter + ") input[type=hidden]").val();
        templateCounter++;

        // break the composite field into its constituent parts
        var itemProperties = itemPropertiesField.split('_');
        var playerID = itemProperties[0];
        var playerName = itemProperties[1];
        var mascot = itemProperties[5].toLowerCase();
        if (mascot == '49ers') {
          mascot = 'fortyNiners';
        }

        /* some players won't have stats (rookies), so we need to check for magGlass existance for all players */
        $(this).find('img.magGlass').each(function () {

          $(this).qtip({
            content: {
              text: 'Loading...',
              title:
              {
                /* Build the title dynamically to have player name and statistics year */
                text: playerName + " " + statSeason + " Statistics"
              },
              ajax: {
                url: 'qtipsources/playerstats.aspx',
                data: { playerid: playerID, statseasoncode: statSeason, pprLeague: pprLeague },
                type: 'get'
              }
            },
            show: {
              delay: 250  /* Delay slightly so that 'skimming over' a popup trigger doesn't issue an unnecessary AJAX request */
            },
            position: {
              my: 'leftMiddle',
              at: 'rightMiddle'
            },
            hide:
            {
              event: 'mouseout',
              fixed: true
            },
            style:
            {
              classes: "ui-tooltip-football ui-tooltip-" + mascot + " ui-tooltip-shadow",
              width: 360,
              height: 148
            }
          }); /* close qtip */

        }); /* close find each */


      }); /* close statsContainer each */




      /* ******************/
      /*    Status Popup    */
      /********************/
      templateCounter = 0;  // a counter to mark which player I'm processing
      $('.statusbuttonContainer').each(function () {

        // get a reference to the composite field in the player template
        var itemPropertiesField = $(".itemPropertiesContainer:eq(" + templateCounter + ") input[type=hidden]").val();
        templateCounter++;

        // break the composite field into its constituent parts
        var itemProperties = itemPropertiesField.split('_');
        var statusSuppInfo = itemProperties[7];
        var statusSuppInfoTitle = itemProperties[8];


        /* some players won't have stats (rookies), so we need to check for magGlass existance for all players */
        $(this).find('.statusbutton').each(function () {

          $(this).qtip({

            content: {
              text: statusSuppInfo,
              title:
              {
                text: statusSuppInfoTitle
              }
            },
            show: {
              delay: 250  /* Delay slightly so that 'skimming over' a popup trigger doesn't issue an unnecessary AJAX request */
            },
            position: {
              my: 'leftMiddle',
              at: 'rightMiddle'
            },
            hide:
            {
              event: 'mouseout',
              fixed: true
            },
            style: {
              classes: 'ui-tooltip-instructional',
              width: 180,
              height: 80
            }
          }); /* close qTip */


        }); /* close statusbutton each */


      }); /* close statsContainer each */


      /* JQuery Reorder Code */
      reSequenceList();

      $(function () {

        var startIndex = 0;

        $('#sortable').sortable({
          placeholder: "ui-state-highlight",
          handle: '.dragContainer',
          start: function (event, ui) {
            // Get the start index of the item being dragged
            oldIndex = ui.item.index();
          },
          stop: function (event, ui) {
            // At the end of the drag, if item is in different , update the database
            var cheatSheetID = $('#<%= ddlAvailableSheets.ClientID %>').val();

            var newIndex = ui.item.index();
            if (oldIndex != newIndex) {
              moveListItem(cheatSheetID, oldIndex, newIndex);
            }
            reSequenceList();
          }
        });
        //$("#sortable").disableSelection();
      });



      function moveListItem(cheatSheetID, oldIndex, newIndex) {
        $.ajax({
          type: "POST",
          url: "custom-sheet.aspx/ReorderItems",
          data: "{'cheatSheetID':'" + cheatSheetID + "'," + "'oldIndex':'" + oldIndex + "'," + "'newIndex':'" + newIndex + "'}",
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function (data) {
          },
          error: function (xhr, ajaxOptions, thrownError) {
            alert("error:" + thrownError);
          }
        });
      }

      function reSequenceList() {
        var sequenceCounter = 1;
        $(".seqNo").each(function () {
          $(this).text(sequenceCounter++);
        });
      }
      

      

    });


</script>


</asp:Content>
