<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="ranksupplementalplayers.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.RankSupplementalPlayers" Title="Rank Supplemental Players" %>
<%@ Register Src="~/usercontrols/Sports/SheetItemManager.ascx" TagName="SheetItemManager" TagPrefix="uc2" %>
<%@ Register Src="~/usercontrols/Sports/Football/CheatSheet/FOOSheetItemTemplate.ascx" TagName="FOOSheetItemTemplate" TagPrefix="cswr" %>
<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphAdminContent" Runat="Server">

  <asp:ScriptManager runat="server" EnablePageMethods="true" />

  <div class="rankSuppPlayersPage">

    <h2>Rank Supplemental Players</h2>

    <asp:Panel runat="server" ID="panSupplementalSheet" CssClass="customSheetContainer">

      <%--Sheet Title--%>
      <asp:Panel runat="server" ID="panSheetTitle" CssClass="sheetTitleContainer">
        <asp:Label runat="server" ID="labSourceTitle" CssClass="sheetTitle"></asp:Label>
        <asp:HyperLink runat="server" ID="hlEditSuppSheet" Text="edit" /> 
        <asp:HyperLink runat="server" ID="hlValidateSuppSheet" Text="validate" Target="_blank" /> 
      </asp:Panel>

      <div class="positionContainer">
        <asp:DropDownList runat="server" ID="ddlPositions" AutoPostBack="true" DataValueField="PositionCode" DataTextField="Name" 
          onselectedindexchanged="ddlPositions_SelectedIndexChanged"/>
      </div>
 
      <!-- The actual racing cheat sheet -->
      <asp:Panel runat="server" ID="panFootballSheet" CssClass="footballList">
        <asp:Repeater runat="server" ID="repFootballSheet" OnItemDataBound="repFootballSheet_ItemDataBound">
          <HeaderTemplate>
            <ul id="sortable">
          </HeaderTemplate>
          <ItemTemplate>
            <li runat="server" id="liPlayerItem">
              <cswr:FOOSheetItemTemplate runat="server" ID="fssiTemplate" SheetType="SuppSheet" EnableViewState="false"/>
            </li>
          </ItemTemplate>
          <FooterTemplate>
            </ul>
          </FooterTemplate>
        </asp:Repeater>
      </asp:Panel>

      <%--Sheet Title--%>
      <asp:Panel runat="server" ID="panSheetTitle2" CssClass="sheetTitleContainer">
        <asp:Label runat="server" ID="labSourceTitle2" CssClass="sheetTitle"></asp:Label>
        <asp:Label runat="server" ID="labPositionTitle2" CssClass="sheetTitle"></asp:Label>
        <asp:HyperLink runat="server" ID="hlEditSuppSheet2" Text="edit" /> 
        <asp:HyperLink runat="server" ID="hlValidateSuppSheet2" Text="validate"  Target="_blank"/> 
      </asp:Panel>

    </asp:Panel>

  </div>

  <br />
  <br />
  <div>
    <%--Mobile Controls--%>
    <asp:DropDownList runat="server" ID="ddlAllSheetPlayers" DataTextField="FullNameLastFirst" DataValueField="PlayerID"></asp:DropDownList>
    <asp:DropDownList runat="server" ID="ddlNewPositions"></asp:DropDownList>
    <asp:Button runat="server" ID="butMovePlayer" Text="Move Player" 
      onclick="butMovePlayer_Click" />
  </div>



  <%--Hidden field used by JQuery to populate links to supplemental rankings--%>
  <div id="jqueryFields">
    <asp:HiddenField runat="server" ID="hfPositionCode" EnableViewState="false"/>
    <asp:HiddenField runat="server" ID="hfSheetID" EnableViewState="false" />
    <asp:HiddenField runat="server" ID="hfStatSeason" EnableViewState="false" />
  </div>


  <script type="text/javascript">


    // Create the tooltips only on document load
    $(document).ready(function () {


      // Get the position code for later user
      var positionCode = $("#jqueryFields #<%= hfPositionCode.ClientID %>").val();
      // Get the PPRLeague settings to we know how to configure the stat popup
      var pprLeague = false;
      // Get the SheetID so we know which players to query for the team player rankings
      var sheetID = $("#jqueryFields #<%= hfSheetID.ClientID %>").val();
      // Get the StatSeason so we know which year to display in the headers
      var statSeason = $("#jqueryFields #<%= hfStatSeason.ClientID %>").val();

      /* ******************/
      /*   Source Popup   */
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
            },
            ajax: {
              url: '../../../../fantasy-football/nfl/create/qtipsources/suppsources.aspx',
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
      /*    Team Rankings Popup    */
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
        var sheetType = itemProperties[6];
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
              url: '../../../../fantasy-football/nfl/create/qtipsources/teamplayerranks.aspx',
              data: { playerid: playerID, sheetid: sheetID, sheettype: sheetType },
              type: 'get',
              once: false
            }
          },
          show: {
            delay: 250
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
            width: 250
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

        //alert("itemPropertiesField:" + itemPropertiesField);

        /* some players won't have stats (rookies), so we need to check for magGlass existance for all players */
        $(this).find('img.magGlass').each(function () {

          //alert("magglass found");

          $(this).qtip({
            content: {
              text: 'Loading...',
              title:
              {
                /* Build the title dynamically to have player name and statistics year */
                text: playerName + " " + statSeason + " Statistics"
              },
              ajax: {
                url: '../../../../fantasy-football/nfl/create/qtipsources/playerstats.aspx',
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
              height: 154
            }
          }); /* close qtip */

        }); /* close find */

      }); /* close each */


      /* ******************/
      /*    Status Popup    */
      /********************/
      templateCounter = 0;  // a counter to mark which player I'm processing
      $('.statusIconContainer').each(function () {

        // get a reference to the composite field in the player template
        var itemPropertiesField = $(".itemPropertiesContainer:eq(" + templateCounter + ") input[type=hidden]").val();
        templateCounter++;

        // break the composite field into its constituent parts
        var itemProperties = itemPropertiesField.split('_');
        var statusSuppInfo = itemProperties[7];
        var statusSuppInfoTitle = itemProperties[8];


        /* some players won't have stats (rookies), so we need to check for magGlass existance for all players */
        $(this).find('.statusIcon').each(function () {

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


        }); /* close statusIcon each */


      }); /* close statsContainer each */



      /* Re-Sequence numbering */
      reSequenceList();

      /* JQuery Reorder Code */
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
            var supplementalSheetID = $('#<%= hfSheetID.ClientID %>').val();
            var newIndex = ui.item.index();
            if (oldIndex != newIndex) {
              moveListItem(supplementalSheetID, oldIndex, newIndex);
            }
            reSequenceList();
          }
        });
        //$("#sortable").disableSelection();
      });



      function moveListItem(supplementalSheetID, oldIndex, newIndex) {
        $.ajax({
          type: "POST",
          url: "ranksupplementalplayers.aspx/ReorderItems",
          data: "{'supplementalSheetID':'" + supplementalSheetID + "'," + "'oldIndex':'" + oldIndex + "'," + "'newIndex':'" + newIndex + "'}",
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

