<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPages/Sport.master" Theme="Web20" AutoEventWireup="true" 
  CodeFile="custom-sheet.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.FantasyRacingCustomSheet" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage" 
  Title="Custom Fantasy Racing Cheat Sheet"
  MetaDescription="Use this free, custom fantasy racing cheat sheet to easily create your fantasy NASCAR driver rankings using drag and drop."
  CanonicalUrl="https://www.cheatsheetwarroom.com/fantasy-racing/nascar/create/custom-sheet.aspx"
%>
<%@ Register Src="~/usercontrols/Sports/Racing/CheatSheet/RACSheetItemTemplate.ascx" TagName="RACSheetItemTemplate" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/ads/AdGenerator.ascx" TagName="AdGenerator" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/navigation/SheetVisitorNavigation.ascx" TagName="SheetVisitorNavigation" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/navigation/SheetCreationSheetLevelNavigation.ascx" TagName="SheetCreationSheetLevelNavigation" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/navigation/SheetCreationManageLevelNavigation.ascx" TagName="SheetCreationManageLevelNavigation" TagPrefix="cswr" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">

  <asp:ScriptManager runat="server" EnablePageMethods="true" />

  <div class="customSheetContainer">

    <%--Navigation--%>
    <cswr:SheetCreationManageLevelNavigation runat="server" ID="scmlnNavigation" CurrentStage="CUSTOMSHEET" SportCode="RAC" />
  
    <%--Visitor Buttons--%>
    <cswr:SheetVisitorNavigation runat="server" ID="svnNavigation" SportCode="RAC" />
   
    <%--No Sheets Configured--%>
    <cswr:MessageBox runat="server" id="mbNoSheets" WidthPercentage="50"/>
    
    <%--Sheet Header Controls--%>
    <asp:Panel runat="server" ID="panSheetHeaderControls" CssClass="topControlsContainer">

      <table class="layoutContainer">
        <tr>
          <%--Left Side--%>
          <td class="leftSide">

            <%--Top Sheet Controls--%>
            <div class="sheetControls">
                
              <%--Dropdown--%>
              <div class="dropDownContainer">
                <asp:DropDownList runat="server" ID="ddlAvailableSheets" DataTextField="SheetName" DataValueField="CheatSheetID" Width="200" 
                OnSelectedIndexChanged="ddlAvailableSheets_SelectedIndexChanged" AutoPostBack="True" OnDataBound="ddlAvailableSheets_DataBound"></asp:DropDownList>
              </div>

              <%--Buttons--%>
              <asp:Panel runat="server" ID="panSheetButtons" CssClass="buttons">
                <cswr:SheetCreationSheetLevelNavigation runat="server" ID="scplnPageLevelNavigation" SportCode="RAC" />
              </asp:Panel>
    
            </div>  <!-- close sheetControls -->

            <%--Blog Controls--%>
            <!-- close blogControls -->

          </td> <!-- close left side -->

          <%--Right Side--%>
          <td class="rightSide">
              
          </td>  <!-- close right side -->
        </tr>

      </table>
 
    </asp:Panel>  <!-- close topControlsContainer -->

    <!-- The actual racing cheat sheet -->
    <div class="racingList">
      <asp:Repeater runat="server" ID="repDriverSheet" OnItemDataBound="repDriverSheet_ItemDataBound">
        <HeaderTemplate>
          <ul id="sortable">
        </HeaderTemplate>
        <ItemTemplate>
          <li runat="server" id="liDriverItem">
            <cswr:RACSheetItemTemplate runat="server" ID="rsitRACSheetItemTemplate" />
          </li>
        </ItemTemplate>
        <FooterTemplate>
          </ul>
        </FooterTemplate>
      </asp:Repeater>
    </div> <!-- close racingList -->

  </div>


 <div class="sheetHelpContainer" id="cheatSheetHelp">
  
    <h1>Racing Cheat Sheet Help</h1>
  
    <!-- Intro -->  
    <div class="intro">
      <p>
        Our 
        <asp:HyperLink runat="server" NavigateUrl="~/fantasy-racing/nascar/create/custom-sheet.aspx">free fantasy racing cheat sheet creation</asp:HyperLink>
        tools allow users to create informative cheat sheets in a fun and intuitive manner.  
        We provide a wide-range of functionality in our racing cheat sheets: ranking drivers using drag &amp; drop, supplemental NASCAR driver rankings 
        to compare against your own rankings, important driver information (name, sponsor, car number, and years of experience), relevant 
        statistics, driver twitter feeds, and a configurable note for each driver.  The purpose of this page is to explain all of this functionality in detail.    
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
          <a href="#rankingDrivers">Ranking Drivers</a>
        </li>
        <li>
          <a href="#supplementalRankings">ADP & Supplemental Rankings</a>
        </li>
        <li>
          <a href="#driverInformation">Driver Information</a>
        </li>
        <li>
          <a href="#twitterFeeds">Twitter Feeds</a>
        </li>
        <li>
          <a href="#relevantStatistics">Relevant Statistics</a>
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
        <asp:Hyperlink runat="server" NavigateUrl="https://www.mozilla.com/en-US/firefox/" Target="_blank">Firefox web browser</asp:Hyperlink>.
        Firefox is <strong>much 
        more responsive</strong> when 
        <a href="#rankingPlayers">ranking players</a>
        (as opposed to IE, where you have to hold down your mouse button for much longer when selecting a driver).  
        A high-speed internet connection is beneficial, but not required.  You must have JavaScript and cookies enabled to use this application because of 
        its interactive nature and the heavy use of AJAX.  
      </p>
    </div>




    <!-- Application Performance -->
    <div class="subSection">
      <h2 id="applicationPerformance">Application Performance</h2>
      <p>
        Because of the nature of the web, the cheat sheet interface will load and function slower as the number of players on your cheat sheet increases.  
        The degradation in performance is why only a modest number of drivers are added to your sheet when it is initially created.  However, as a 
        <a href="#registration">registered user</a>
        you are free to add as many drivers as you wish to your sheets, but keep in mind application responsiveness will degrade.  
      </p>  
      
      <p>  
        When reordering your drivers,
        some browsers (especially Internet Explorer) force you to hold down your mouse button for a period of time before you're able to successfully 
        <a href="#rankingPlayers">grab a driver for reordering</a>.
        In IE, as the number of drivers on your sheet increases, the time required to hold down your mouse button for driver selection also increases.  This is a big
        reason why we recommend the
        <asp:Hyperlink runat="server" NavigateUrl="https://www.mozilla.com/en-US/firefox/" Target="_blank">Firefox web browser</asp:Hyperlink>
        as there is <strong>zero delay </strong> when selecting a driver.
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
          The ability to create any number of cheat sheets for any number of fantasy racing leagues.
        </li>
        <li>
          The ability to add and remove drivers from your cheat sheet.
        </li>
        <li>
          The ability to generate a 
          <asp:HyperLink runat="server" NavigateUrl="~/fantasy-racing/nascar/free/printable/drivers-cheat-sheet.aspx" Target="_blank">printable racing cheat sheet</asp:HyperLink>
          using your custom rankings.
        </li>
        <li>
          The ability to export your cheat sheet rankings to a spreadsheet.
        </li>
        <li>
          The ability to re-sort your drivers using either statistics or supplemental 
          <asp:Hyperlink runat="server" NavigateURl="~/fantasy-racing/nascar/free/rankings/drivers.aspx" Target="_blank">NASCAR player rankings</asp:Hyperlink>.
        </li>
      </ul>
    </div>
    
    <!-- Cheat Sheet Creation -->
    <div class="subSection">
      <h2 id="sheetCreation">Cheat Sheet Creation</h2>
      <p>
        When you initially create a fantasy racing cheat sheet, all of the available data and functionality will be integrated into your sheet 
        automatically.  Initially, 32 drivers will be added to your sheet.
      </p>
    </div>

    <!-- Editing a Sheet -->
    <div class="subSection">
      <h2 id="editSheet">Editing a Sheet</h2>
      <p>
        If you are a registered user, you can edit your cheat sheet's properties at any time.  As a registered user, you can 
        freely add and remove drivers from your cheat sheet as desired.  While the cheat sheet names for visitor sheets are fixed, you can
        re-name your cheat sheets however you choose.  Finally, through the 'Edit Sheet' functionality, you can re-sort your drivers at any time
        using either statistics or a supplemental source.
      </p>
    </div>

    <!-- Rankings Drivers -->
    <div class="subSection">
      <h2 id="rankingDrivers">Ranking Drivers</h2>
      <p>
        The most important aspect of cheat sheet creation is the ability to easily manipulate your driver rankings.  To change a 
        driver’s ranking you first need to position your cursor over the <em>drag handle</em> (the portion of the driver template with the checkered
        pattern), then <strong>click and hold down the mouse button</strong>.  The length of time you have to hold down your mouse button when selecting a driver 
        varies widely based on both your web browser and the number
        of drivers in your cheat sheet.  For instance, Firefox (whichs is the browser we strongly recommend) responds immediately regardless of the 
        number of drivers in your sheet, while in Internet Explorer the responsiveness is much slower and decreases as the number of drivers on your sheet increases.
      </p>
    
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Racing/Help/racing-cheat-sheet-selecting-player.gif" AlternateText="Selecting a NASCAR driver to rank." />
      </div>
    
      <p>      
        After <strong>holding down your mouse button</strong> for some period of time, the player template will become <em>draggable</em>.  Still holding down the mouse button,
        you should now be able to drag the player to a different position on your cheat sheet. The position where the player will be 
        dropped should be highlighted.
      </p>
  
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Racing/Help/racing-cheat-sheet-dragging-player.gif" AlternateText="Dragging a NASCAR driver to a different position." />
      </div>
    </div>

    <!-- ADP & Supplemental Rankings -->
    <div class="subSection">
    <h2 id="supplementalRankings">ADP & Supplemental Ranking</h2>


      <p>
        When creating your own rankings it is often useful to compare you rankings against others.  One way we keep track of driver value is through
        <strong>Average Draft Position (ADP)</strong> calculation.  <abbr title="Average Draft Position">ADP</abbr> is just that, the average position
        that each driver is ranked across our fantasy racing sheets.  The <abbr title="Average Draft Position">ADP</abbr> is the top number listed
        next to your own rank.   
      </p>  

      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Racing/Help/racing-cheat-sheet-adp.gif" AlternateText="Racing cheat sheet with ADP calculation." />
      </div>

      <p>
        In addition , we also list our own 
        <asp:HyperLink runat="server" NavigateUrl="~/fantasy-racing/nascar/free/rankings/drivers.aspx">NASCAR driver rankings</asp:HyperLink>
        which are displayed right below <abbr title="Average Draft Position">ADP</abbr>.
      </p>  

      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Racing/Help/racing-cheat-sheet-cswr-rank.gif" AlternateText="Racing cheat sheet with CSWR rank." />
      </div>

      <p>
        If you hover your mouse over the magnifying class icon next to our spplemental rankings, a popup will appear which explains the value that
        each number represents.
      </p>  

      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Racing/Help/racing-cheat-sheet-supp-popup.gif" AlternateText="Racing cheat sheet with supplemental ranking popup." />
      </div>

    </div>
    
    
    
    <!-- Driver Information -->
    <div class="subSection">
      <h2 id="driverInformation">Driver Information</h2>
      <p>
        Each driver template includes critical information about that driver: car number, driver name, driver experience (in years), and car make.     
      </p>
 
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Racing/Help/racing-cheat-sheet-driver-info.gif" AlternateText="Racing cheat sheet with driver information." />
      </div>
    </div>
    

    <!-- Configurable Tags -->
    <div class="subSection">
      <h2 id="twitterFeeds">Twitter Feeds</h2>
      <p>
        Twitter has emerged as one of the best resources for delivering current driver news, and unlike media outlets, tweets come directly from
        the drivers themselves.  We have integrated Twitter into our racing sheets so that you can quickly view the latest tweets from each driver.
        To view a drivers most recent tweets, simply click on the Twitter icon.  
      </p>
    
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Racing/Help/racing-cheat-sheet-twitter-icon.gif" AlternateText="Racing cheat sheet with twitter icon." />
      </div>
      
      <p>  
       After clicking on the Twitter icon, a new page will be generated which contains that driver's latest tweets.  
      </p>
      
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Racing/Help/driver-twitter-feeds.gif" AlternateText="Latest tweets for a particular driver." />
      </div>
      
    </div>


    <!-- Relevant Statistics -->
    <div class="subSection">
      <h2 id="#relevantStatistics">Relevant Statistics</h2>
      <p>
        Each of our sheets integrates important driver statistics to assist in your rankings.
      </p>  
    
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Racing/Help//racing-cheat-sheet-driver-stats.gif" AlternateText="Racing cheat sheet with driver statistics." />
      </div>
    
      <p>
        In addition to the statistics visible in the driver template, you can view even more statistics by hovering your mouse over the magnifying
        glass icon right after that statistics area.
      </p>

      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Racing/Help//racing-cheat-sheet-driver-stats-popup.gif" AlternateText="Racing cheat sheet with driver statistics in popup." />
      </div>
    </div>    




    <!-- Configurable Note -->
    <div class="subSection">
      <h2 id="configurableNote">Configurable Note</h2>

      <p>
        Because we cannot predict all of the attributes that you value in a driver, we have integrated an editable note area into each driver template.  This 
        is meant to serve as a general catch-all for any information that you want to add for a particular driver.  
      </p>
    
      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Racing/Help//racing-cheat-sheet-custom-note.gif" AlternateText="Racing cheat sheet with custom note." />
      </div>
    </div>      
   
    
    
    
    <!-- Printable Sheets -->
    <div class="subSection">
    
      <h2 id="printableSheets">Generating a Printable Sheet</h2>
  
      <p>
        Generating a printable fantasy NASCAR racing cheat sheet to bring to your fantasy racing draft is as simple as clicking on the 'Printable Sheet' link
        at the top of your sheet. 
      </p>

      <div class="imageContainer">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Racing/Help/racing-cheat-sheet-click-printable.gif" AlternateText="Racing cheat sheet with printable option." />
      </div>

      <p>
        After clicking the 'Printable Sheet' link, a new page will be generated with a printable version of your cheat sheet.  This printable includes up to
         
      </p>

    </div> <!-- close subSection -->
    
        
  </div> <!-- close help page -->

  <!-- This is the JQuery to show the source of the supplemental rankings -->
  <script type="text/javascript">
    
    // Create the tooltips only on document load
    $(document).ready(function () {

      // Get the position code for later user
      var positionCode = $("#jqueryFields input").val();

      /* **************************/
      /*    Supplemental Popup    */
      /****************************/
      $('.magGlassContainer img').each(function () {

        $(this).qtip({
          content: {
            text: 'Loading...',
            title:
            {
              text: "Supplemental Rankings"
            },
            ajax: {
              url: 'qtipsources/suppsources.aspx',
              data: { adp: $(this).prev().prev().attr('value'), cswr: $(this).prev().attr('value') },
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
            classes: 'ui-tooltip-racing ui-tooltip-shadow',
            width: 170,
            height: 65
          }
        }); /* close qtip */

      });  /* close supplemental popup each */


      /* ******************/
      /*    Stat Popup    */
      /********************/
      $('.statsContainer img.magGlass').each(function () {

        $(this).qtip({
          content: {
            text: 'Loading...',
            title:
            {
              /* Build the title dynamically to have player name and statistics year */
              text: $(this).prev().attr('value') + " " + $(this).prev().prev().prev().attr('value') + " Statistics"
            },
            ajax: {
              url: 'qtipsources/driverstats.aspx',
              data: { playerid: $(this).prev().prev().attr('value'), statseasoncode: $(this).prev().prev().prev().attr('value') },
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
            classes: 'ui-tooltip-racing ui-tooltip-shadow',
            width: 260,
            height: 106
          }
        }); /* close qtip */

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
            startIndex = ui.item.index();
          },
          stop: function (event, ui) {
            // At the end of the drag, if item is in different , update the database
            var cheatSheetID = $('#<%= ddlAvailableSheets.ClientID %>').val();
            var newIndex = ui.item.index();
            if (startIndex != newIndex) {
              moveListItem(cheatSheetID, startIndex, newIndex);
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
            //showVoodooJavaScriptError(xhr, ajaxOptions, thrownError);
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
