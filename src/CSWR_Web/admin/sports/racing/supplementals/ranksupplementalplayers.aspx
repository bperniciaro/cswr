<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="ranksupplementalplayers.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.Racing.RankSupplementalPlayers" Title="Rank Supplemental Players" %>
<%@ Register Src="~/usercontrols/sports/racing/cheatsheet/RACSheetItemTemplate.ascx" TagName="RACSheetItemTemplate" TagPrefix="cswr" %>
<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphAdminContent" Runat="Server">

  <div class="rankSuppPlayersPage">

    <h2>Rank Supplemental Players</h2>

    <asp:Panel runat="server" ID="panSupplementalSheet" CssClass="customSheetContainer">

      <%--Sheet Title--%>
      <asp:Panel runat="server" ID="panSheetTitle" CssClass="sheetTitleContainer">
        <asp:Label runat="server" ID="labSourceTitle" CssClass="sheetTitle"></asp:Label>:
        <asp:HyperLink runat="server" ID="hlEditSuppSheet" Text="edit" /> 
        <asp:HiddenField runat="server" ID="hfSupplementalSheetID" />
      </asp:Panel>
 

       <!-- The actual racing cheat sheet -->
        <div class="racingList">
          <asp:Repeater runat="server" ID="repDriverSheet" OnItemDataBound="repDriverSheet_ItemDataBound">
            <HeaderTemplate>
              <ul id="sortable">
            </HeaderTemplate>
            <ItemTemplate>
              <li runat="server" id="liDriverItem">
                <cswr:RACSheetItemTemplate runat="server" ID="rsitRACSheetItemTemplate" CurrentItemType="SuppSheet" />
              </li>
            </ItemTemplate>
            <FooterTemplate>
              </ul>
            </FooterTemplate>
          </asp:Repeater>
        </div> <!-- close racingList -->


      <%--Sheet Title--%>
      <asp:Panel runat="server" ID="panSheetTitle2" CssClass="sheetTitleContainer">
        <asp:Label runat="server" ID="labSourceTitle2" CssClass="sheetTitle"></asp:Label>:
        <asp:Label runat="server" ID="labPositionTitle2" CssClass="sheetTitle"></asp:Label>
        <asp:HyperLink runat="server" ID="hlEditSuppSheet2" Text="edit" /> 
      </asp:Panel>

    </asp:Panel>

  </div>

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
              url: '../../../../fantasy-racing/nascar/create/qtipsources/suppsources.aspx',
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
              url: '../../../../fantasy-racing/nascar/create/qtipsources/driverstats.aspx',
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
            oldIndex = ui.item.index();
          },
          stop: function (event, ui) {
            // At the end of the drag, if item is in different , update the database
            var supplementalSheetID = $('#<%= hfSupplementalSheetID.ClientID %>').val();
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
            //alert("success");
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

