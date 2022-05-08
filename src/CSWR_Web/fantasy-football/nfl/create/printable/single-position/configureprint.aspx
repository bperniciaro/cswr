<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveTwoCol.master" Theme="Web20" AutoEventWireup="true" 
  CodeFile="configureprint.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.ConfigurePrintSingle" 
  CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage" MetaRobotsText="NOINDEX,FOLLOW"
  Title="Configurable Printable Cheat Sheet" MaintainScrollPositionOnPostback="true" %>
<%@ MasterType VirtualPath="~/MasterPages/Sport.master" %>
<%@ Register Src="~/usercontrols/navigation/SheetCreationManageLevelNavigation.ascx" TagName="SheetCreationManageLevelNavigation" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

<asp:ScriptManager runat="server" />

<div class="configPrintPage">
  
  <%--Member Buttons--%>
  <cswr:SheetCreationManageLevelNavigation runat="server" ID="scmlnNavigation" CurrentStage="PRINTSHEET" />

  <asp:UpdateProgress ID="upUpdateProgress" AssociatedUpdatePanelID="upUpdatePanel" runat="server" DynamicLayout="false" DisplayAfter="0">
    <ProgressTemplate>      
      <div id="ajaxLoaderOverlay" class="ajaxOverlay" style="margin-top:400px;">
        <asp:Image runat="server" ImageUrl="~/Images/Animations/googlerotate.gif"/>
      </div>      
    </ProgressTemplate>
  </asp:UpdateProgress>

  <asp:UpdatePanel runat="server" ID="upUpdatePanel">
    <ContentTemplate>

      <div class="ajaxFormContainer">

        <div class="alert alert-info">
          <h2>Step 1:  Select Sheets</h2>
          <p>
            This page allows you to <strong>combine your single-position sheets</strong> so that you can generate a single, printable sheet which includes all
            of your positional rankings.  Custom sheets based on more than one position <strong>cannot be printed
            through this page</strong>.
          </p>
          <p>
            To configure your printable sheet, you must first select the custom cheat sheets to be combined.  If you have not created a cheat
            sheet for a particular position, you must <strong>choose an <em>expert ranking</em></strong> to complete 
            your sheet for printing purposes.
          </p>
        </div>

  <div class="table-responsive">
      <table class="posTable table table-bordered table-condensed" >
        <%--Quarterbacks--%>
        <tr>
          <td class="position">
            QBs <asp:Label runat="server" ID="labQBSheetType" />
          </td>
          <td class="sheetName">
            <asp:Label runat="server" ID="labSingleQBSheet" Visible="False" />
            <asp:DropDownList runat="server" ID="ddlQBSheets" Visible="False" 
              AutoPostBack="true" onselectedindexchanged="ddlQBSheets_SelectedIndexChanged"/>
            <asp:DropDownList runat="server" ID="ddlAvailableQBSources" Visible="False" 
              OnDataBound="ddlAvailableQBSources_DataBound" AutoPostBack="true" 
              onselectedindexchanged="ddlAvailableQBSources_SelectedIndexChanged" />
            <asp:Label runat="server" ID="labQBSheetSourceMessage" Visible="false" CssClass="suppInfo"></asp:Label>
          </td>
        </tr>
        <%--Running Backs--%>
        <tr class="alternatingRow">
          <td class="position">
            RBs <asp:Label runat="server" ID="labRBSheetType" />
          </td>
          <td class="sheetName">
            <asp:Label runat="server" ID="labSingleRBSheet" Visible="False" />
            <asp:DropDownList runat="server" ID="ddlRBSheets" Visible="False" 
              AutoPostBack="true" onselectedindexchanged="ddlRBSheets_SelectedIndexChanged"/>
            <asp:DropDownList runat="server" ID="ddlAvailableRBSources" Visible="False" OnDataBound="ddlAvailableRBSources_DataBound" 
            onselectedindexchanged="ddlAvailableRBSources_SelectedIndexChanged" AutoPostBack="true" />      
            <asp:Label runat="server" ID="labRBSheetSourceMessage" Visible="false" CssClass="suppInfo"></asp:Label>
          </td>
        </tr>
        <%--Wide Receivers--%>
        <tr>
          <td class="position">
            WRs <asp:Label runat="server" ID="labWRSheetType" />
          </td>
          <td class="sheetName">
            <asp:Label runat="server" ID="labSingleWRSheet" Visible="False" />
            <asp:DropDownList runat="server" ID="ddlWRSheets" Visible="False" 
              AutoPostBack="true" onselectedindexchanged="ddlWRSheets_SelectedIndexChanged"/>
            <asp:DropDownList runat="server" ID="ddlAvailableWRSources" Visible="False" OnDataBound="ddlAvailableWRSources_DataBound" 
            onselectedindexchanged="ddlAvailableWRSources_SelectedIndexChanged" AutoPostBack="true" />      
            <asp:Label runat="server" ID="labWRSheetSourceMessage" Visible="false" CssClass="suppInfo"></asp:Label>
          </td>
        </tr>
        <%--Tight Ends--%>
        <tr>
          <td class="position">
            TEs <asp:Label runat="server" ID="labTESheetType" />
          </td>
          <td class="sheetName">
            <asp:Label runat="server" ID="labSingleTESheet" Visible="False" />
            <asp:DropDownList runat="server" ID="ddlTESheets" Visible="False" 
              AutoPostBack="true" onselectedindexchanged="ddlTESheets_SelectedIndexChanged"/>
            <asp:DropDownList runat="server" ID="ddlAvailableTESources" Visible="False" OnDataBound="ddlAvailableTESources_DataBound" 
            onselectedindexchanged="ddlAvailableTESources_SelectedIndexChanged" AutoPostBack="true" />      
            <asp:Label runat="server" ID="labTESheetSourceMessage" Visible="false" CssClass="suppInfo"></asp:Label>
          </td>
        </tr>
        <%--Kickers--%>
        <tr>
          <td class="position">
            Ks <asp:Label runat="server" ID="labKSheetType" />
          </td>
          <td class="sheetName">
            <asp:Label runat="server" ID="labSingleKSheet" Visible="False" />
            <asp:DropDownList runat="server" ID="ddlKSheets" Visible="False" 
              AutoPostBack="true" onselectedindexchanged="ddlKSheets_SelectedIndexChanged"/>
            <asp:DropDownList runat="server" ID="ddlAvailableKSources" Visible="False" OnDataBound="ddlAvailableKSources_DataBound" 
             onselectedindexchanged="ddlAvailableKSources_SelectedIndexChanged" AutoPostBack="true" />      
            <asp:Label runat="server" ID="labKSheetSourceMessage" Visible="false" CssClass="suppInfo"></asp:Label>
          </td>
        </tr>
        <%--Defense--%>
        <tr>
          <td class="position">
            DFs <asp:Label runat="server" ID="labDFSheetType" />
          </td>
          <td class="sheetName">
            <asp:Label runat="server" ID="labSingleDEFSheet" Visible="False" />
            <asp:DropDownList runat="server" ID="ddlDEFSheets" Visible="False" 
              AutoPostBack="true" onselectedindexchanged="ddlDEFSheets_SelectedIndexChanged"/>
            <asp:DropDownList runat="server" ID="ddlAvailableDEFSources" Visible="False" OnDataBound="ddlAvailableDEFSources_DataBound" 
              onselectedindexchanged="ddlAvailableDFSources_SelectedIndexChanged" AutoPostBack="true" />      
            <asp:Label runat="server" ID="labDFSheetSourceMessage" Visible="false" CssClass="suppInfo"></asp:Label>
          </td>
        </tr>
      </table>
  </div>



      <div class="alert alert-info">
        <h2>Step 2: Choose Format</h2>
        <p>
          The print format you should choose is based on the <strong>number of teams of your league</strong>.
          If your league has 12 or more teams, the first option is best as it prints 
          more players from your sheets than the 10-team option. A
          printable roster area (and any player-specific notes you may have created) are then printed on a separate sheet.
        </p>
        <p>
          If your league has 10 or fewer teams, then the second option should be adequate.  This option prints fewer players, but 
          it integrates a roster area right into the sheet itself.  Player notes are printed on a second sheet.
        </p>
      </div>


      <div class="option">
  
        <p>
          <asp:RadioButton runat="server" ID="rbNoRoster" GroupName="SheetFormat" Checked="true" 
            Text=" 12 or more teams" CssClass="printCategory" AutoPostBack="true" 
            oncheckedchanged="rbNoRoster_CheckedChanged" />
        </p>
        <p class="formatDescription">
          This format reserves maximum space for player rankings to ensure enough players are listed to complete your draft.  A second page
          includes a roster area to enter your draft picks and also lists any custom player notes you have added to your referenced sheets.
        </p>

<%--        <table>
          <tr>
            <td class="halfWidth">
              
              <table>
                <tr>
                  <td class="leftSideSheetCell">
                    <cswr:HoverImage runat="server" ID="hiCheatSheetWithoutRosterPage1" CaptionText="Cheat Sheet Without Roster" 
                    SmallImageURL="~/Images/UserControls/HoverImage/cheatsheetwithoutroster-small.gif" BigImageURL="~/Images/UserControls/HoverImage/cheatsheetwithoutroster-big.gif" />  
                  </td>
                  <td>
                    <h3>Page 1: Rankings</h3>
                    <p>Total players per position:</p>
                    <ul>
                      <li>Runningbacks: 74</li>
                      <li>Wide Receivers: 74</li>
                      <li>Quarterbacks: 35</li>
                      <li>Tight Ends: 35</li>
                      <li>Kickers: 29</li>
                      <li>Defenses: 29</li>
                    </ul>
                  </td>
                </tr>
              </table>
              
            </td>
            <td class="halfWidth">

              <table>
                <tr>
                  <td class="rightSideSheetCell">
                    <cswr:HoverImage runat="server" ID="hiCheatSheetWithoutRosterPage2" CaptionText="Roster and Custom Notes" 
                    SmallImageURL="~/Images/UserControls/HoverImage/cheatsheetwithoutrosterpage2-small.gif" BigImageURL="~/Images/UserControls/HoverImage/cheatsheetwithoutrosterpage2-big.gif" />  
                  </td>
                  <td>
                    <h3>Page 2: Roster & Notes</h3>
                    <p>
                      Use the provided roster area to enter your draft picks.  Custom player notes that you have created are also listed.
                    </p>
                  </td>
                </tr>
              </table>
              
            </td>
          </tr>
        </table>  <!-- layout table -->--%>

      </div>  <!-- close mainOption -->
  
  
      <div class="option">
  
        <p>
          <asp:RadioButton runat="server" ID="rbIntegratedRoster" 
            GroupName="SheetFormat" Text=" 10 of fewer teams" CssClass="printCategory" AutoPostBack="true"
            oncheckedchanged="rbIntegratedRoster_CheckedChanged"/>
        </p>
        <p class="formatDescription">
          This format integrates a roster area into the sheet itself for convenience and also includes your custom
          player notes on a second sheet.
        </p>

<%--        <table>
          <tr>
            <td class="halfWidth">
              
              <table>
                <tr>
                  <td class="leftSideSheetCell">
                    <cswr:HoverImage runat="server" ID="hiCheatSheetWithRoster" CaptionText="Cheat sheet with roster." 
                      SmallImageURL="~/Images/UserControls/HoverImage/cheatsheetwithroster-small.gif" BigImageURL="~/Images/UserControls/HoverImage/cheatsheetwithroster-big.gif" />  
                  </td>
                  <td>
                    <h3>Page 1: Rankings</h3>
                    <p>Total players per position:</p>
                    <ul>
                      <li>Running Backs: 50</li>
                      <li>Wide Receivers: 50</li>
                      <li>Quarterbacks: 42</li>
                      <li>Tight Ends: 42</li>
                      <li>Kickers: 23</li>
                      <li>Defenses: 23</li>
                    </ul>
                  </td>
                </tr>
              </table>
              
            </td>
            <td class="halfWidth">

              <table>
                <tr>
                  <td class="rightSideSheetCell">
                    <cswr:HoverImage runat="server" ID="hiCheatSheetWithRosterPage2" CaptionText="Custom notes on second page." 
                      SmallImageURL="~/Images/UserControls/HoverImage/cheatsheetwithrosterpage2-small.gif" 
                      BigImageURL="~/Images/UserControls/HoverImage/cheatsheetwithrosterpage2-big.gif" />  
                  </td>
                  <td>
                    <h3>Page 2: Custom Notes</h3>
                    <p>
                      If you have added any custom player notes to your cheat sheets, they will be included on a second page for reference.
                    </p>
                  </td>
                </tr>
              </table>
              
            </td>
          </tr>
        </table>  <!-- layout table -->--%>

      </div>  <!-- close mainOption -->


      <div class="buttonContainer">
        <asp:HyperLink runat="server" ID="hlGenerateSheet" Target="_blank" >
          <asp:Image runat="server" ImageUrl="~/Images/Layout/generateprintablebutton.gif" />
        </asp:HyperLink>
      </div>

      </div>
    </ContentTemplate>
  </asp:UpdatePanel>
  




</div>  <!-- close configureFootballPrintPage -->

  <script type="text/javascript">


    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_beginRequest(BeginRequestHandler);
    prm.add_endRequest(EndRequestHandler);

    function BeginRequestHandler(sender, args) {
      $('.ajaxFormContainer').addClass("ajaxFormFaded");
    }

    function EndRequestHandler(sender, args) {
      $('.ajaxFormContainer').removeClass("ajaxFormFaded");
    }

</script>

</asp:Content>

