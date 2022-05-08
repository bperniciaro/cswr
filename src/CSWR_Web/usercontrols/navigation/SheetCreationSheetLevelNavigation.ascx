<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SheetCreationSheetLevelNavigation.ascx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.UserControls.SheetCreationSheetLevelNavigation" %>

<div class="sheetCreationPageLevelNavigation">

    <%--Member Buttons--%>
    <asp:Panel runat="server" ID="panMemberButtons">
      <asp:HyperLink runat="server" ID="hlEditSheet" CssClass="btn btn-default" ToolTip="Click to edit this cheat sheet.  By editing your sheet you can add/remove players, change the name, & re-sort players.">
        <i class="fa fa-edit"></i>
      </asp:HyperLink>
      <asp:HyperLink runat="server" ID="hlValidateSheet" CssClass="btn btn-default" ToolTip="Click to validate your sheet.  Through validation we will compare your rankings against ours and provide some suggestions for improvement.">
        <i class="fa fa-check"></i>
      </asp:HyperLink>
      <asp:LinkButton runat="server" ID="lbExport" CssClass="btn btn-default" onclick="lbExport_Click" ToolTip="Click to generate a spreadsheet version of this cheat sheet.">
        <i class="fa fa-file-excel-o"></i>
      </asp:LinkButton>
      <asp:HyperLink runat="server" ID="hlPrintSheet" CssClass="btn btn-default" Target="_blank" ToolTip="Click to generate a position-specific, printable version of this cheat sheet.">
        <i class="fa fa-print"></i>
      </asp:HyperLink>
    </asp:Panel>

    <%--Faded Visitor Sheets--%>
    <asp:Panel runat="server" ID="panVisitorButtons" CssClass="fadedVisitor">
      <asp:HyperLink runat="server" ID="hlDisabledEdit" CssClass="btn btn-default disabled" ToolTip="Register to edit your sheet."><i class="fa fa-edit"></i></asp:HyperLink>
      <asp:HyperLink runat="server" ID="hlDisabledValidate" CssClass="btn btn-default disabled" ToolTip="Register to validate your sheet."><i class="fa fa-check"></i></asp:HyperLink>
      <asp:HyperLink runat="server" ID="hlDisabledExport" CssClass="btn btn-default disabled" ToolTip="Register to export your sheet."><i class="fa fa-file-excel-o"></i></asp:HyperLink>
      <asp:HyperLink runat="server" ID="hlDisabledPrint" CssClass="btn btn-default disabled" ToolTip="Register to print your sheet."><i class="fa fa-print"></i></asp:HyperLink>
    </asp:Panel>
  </div>

  <!-- This is the JQuery to show the source of the supplemental rankings -->
  <script type="text/javascript">

    // Create the tooltips only on document load
    $(document).ready(function () {


      // Match all link elements with href attributes within the content div
      $('.fadedVisitor').qtip(
      {
        content: 'Register for free to enable advanced features.', // Give it some content, in this case a simple string
        show: {
          delay: 250
        },
        position: {
          my: 'topLeft',
          at: 'bottomRight'
        },
        hide:
        {
          event: 'mouseout',
          fixed: true
        }
      });

      /* **************************/
      /*    Supplemental Popup    */
      /****************************/
      /*
      $('.fadedVisitor').each(function () {

        $(this).qtip({
          content: {
            text: 'Loading...',
            title:
            {
              text: "Supplemental Rankings"
            },
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
        }); 

      });  
      */
    });

  </script>


