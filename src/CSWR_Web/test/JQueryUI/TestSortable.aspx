<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NoSport.master" AutoEventWireup="true" CodeFile="TestSortable.aspx.cs" Inherits="test_JQueryUI_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

<style type="text/css">
#sortable { list-style-type: none; margin: 0; padding: 0; width: 60%; }
#sortable li { margin: 0 5px 5px 5px; padding: 5px; font-size: 1.2em; height: 1.5em; }
html>body #sortable li { height: 1.5em; line-height: 1.2em; }
.ui-state-highlight { height: 1.5em; line-height: 1.2em; }
    
.handle {float:left;background-color:Orange;height:1.5em;width:10px;}
</style>



  <ul id="sortable">
    <li class="ui-state-default" id="1"><span class="handle"></span> SeqNo:<span class="seqNo"></span> ID 1</li>
    <li class="ui-state-default" id="2"><span class="handle"></span> SeqNo:<span class="seqNo"></span> ID 2</li>
    <li class="ui-state-default" id="3"><span class="handle"></span> SeqNo:<span class="seqNo"></span> ID 3</li>
    <li class="ui-state-default" id="4"><span class="handle"></span> SeqNo:<span class="seqNo"></span> ID 4</li>
    <li class="ui-state-default" id="5"><span class="handle"></span> SeqNo:<span class="seqNo"></span> ID 5</li>
    <li class="ui-state-default" id="6"><span class="handle"></span> SeqNo:<span class="seqNo"></span> ID 6</li>
    <li class="ui-state-default" id="7"><span class="handle"></span> SeqNo:<span class="seqNo"></span> ID 7</li>
  </ul>


<script type="text/javascript">
  $(document).ready(function () {

    reSequenceList();

    $(function () {

      var startIndex = 0;

      $('#sortable').sortable({
        axis: "y",
        placeholder: "ui-state-highlight",
        handle: '.handle',
        start: function (event, ui) {
          // Get the start index so no database call
          // is made if item is dropped in the same order
          startIndex = ui.item.index() + 1;
        },
        stop: function (event, ui) {
          // At the end of the drag, if item is in different ordinal position,
          // update the database using the moveListItem function
          var targetItemID = ui.item[0].id;
          var newIndex = ui.item.index() + 1;
          //alert("targetItemID:" + targetItemID + " newIndex:" + newIndex);
          if (startIndex != newIndex) {
            alert("targetItemID:" + targetItemID + " startIndex:" + startIndex + " newIndex:" + newIndex);
            moveListItem(targetItemID, startIndex, newIndex);
          }
          reSequenceList();
        }
      });
      //$("#sortable").disableSelection();
    });



    function moveListItem(targetItemID, oldIndex, newIndex) {
      $.ajax({
        type: "POST",
        url: "TestSortable.aspx/ReorderItems",
        data: "{'targetItemID':'" + targetItemID + "'," + "'oldIndex':'" + oldIndex + "'," + "'newIndex':'" + newIndex + "'}",
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
      var sequenceCounter = 0;
      $(".seqNo").each(function () {
        $(this).text(sequenceCounter++);
      });
    }

  });                /* close document.ready */
</script>














</asp:Content>

