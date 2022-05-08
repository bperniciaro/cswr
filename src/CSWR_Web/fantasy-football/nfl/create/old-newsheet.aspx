<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveTwoCol.master" Theme="Web20" AutoEventWireup="true" 
  CodeFile="old-newsheet.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.NewSheet" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
  Title="Create New Fantasy Football Cheat Sheet" MetaRobotsText="NOINDEX,FOLLOW" %>
<%@ MasterType VirtualPath="~/MasterPages/Sport.master" %>
<%@ Register Src="~/usercontrols/navigation/SheetCreationManageLevelNavigation.ascx" TagName="SheetCreationManageLevelNavigation" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">

<asp:ScriptManager runat="server" />


<%--Navigation--%>
<cswr:SheetCreationManageLevelNavigation runat="server" ID="scmlnNavigation" CurrentStage="NEWSHEET" SportCode="FOO" />

<%--Main Container--%>
<div class="createSheetPage">

  <div class="fooForm sheetForm">
            
    <%--Put the entire form in an update panel--%>
    <asp:UpdateProgress ID="upUpdateProgress" AssociatedUpdatePanelID="upUpdatePanel" runat="server" DynamicLayout="false" DisplayAfter="0">
      <ProgressTemplate>      
        <div id="ajaxLoaderOverlay" class="ajaxOverlay" style="width:450px;margin-top:80px;">
          <asp:Image runat="server" ImageUrl="~/Images/Animations/googlerotate.gif"/>
        </div>      
      </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="upUpdatePanel">
      <ContentTemplate>

        <%--Status Message--%>
        <cswr:MessageBox runat="server" ID="mbMessage"/>

        <%--Maximum Sheets Message--%>
        <asp:Panel runat="server" ID="panMaximumSheets" Visible="false">
          <p class="warning">
            You have already created the maximum number of sheets <span class="bold">(12)</span>.  If you feel you need more sheets, please 
            <asp:HyperLink runat="server" ID="hlGeneralQuestion" NavigateUrl="~/Contact.aspx?Type=GeneralQuestion" Text="contact the site administrator" />
            to discuss your particular needs.  Alternatively, you can delete one or more of your current sheets through the 
            <asp:HyperLink runat="server" ID="hlManageSheets" NavigateUrl="~/fantasy-football/nfl/create/managesheets.aspx" Text="manage sheets" />
            page.
          </p>
        </asp:Panel>

        <%--Standard Interface--%>
        <asp:Panel runat="server" ID="panStandardInterface" CssClass="ajaxFormContainer">

          <%--Form Table--%>
          <table class="main">
          
            <%--Sheet Title--%>
            <tr>
              <th colspan="2">
                Configure New Sheet
              </th>            
            </tr>

            <%--Sheet Name--%>
            <tr class="alternatingRow">
              <td class="leftCol" style="vertical-align:middle;">
                <span class="required">(required)</span> Sheet Name 
              </td>
              <td>
                <asp:TextBox runat="server" ID="tbSheetName" MaxLength="50" CssClass="sheetName"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvNameRequired" ControlToValidate="tbSheetName" Display="Dynamic" SetFocusOnError="true"
                  ErrorMessage="<img src='../../../Images/error.gif' alt='Sheet Name is required.' title='Sheet Name is required.' />" 
                  ToolTip="Sheet Name is required."></asp:RequiredFieldValidator> 
              </td>
            </tr>

            <%--Sheet Positions--%>
            <tr runat="server" id="trPositions">
              <td class="leftCol" style="vertical-align:middle;">
                <span class="required">(required)</span> Sheet Position
              </td>
              <td class="rightCol positionBoxes">
                <asp:CheckBoxList runat="server" ID="cblPositions" DataTextField="PositionCode" DataValueField="PositionCode" CssClass="positionList" 
                   RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="cblPositions_SelectedIndexChanged"/>
              </td>
            </tr>

            <%--Stats Season--%>
            <tr runat="server" id="trStatSeason">
              <td class="leftCol">
                <asp:Image runat="server" CssClass="ajaxLoaded"  ImageUrl="~/Images/Icons/help.gif" ToolTip="This is the season on which the in-sheet statistics will be based." />
                Stats Season
              </td>
              <td>
                <asp:DropDownList runat="server" ID="ddlStatsSeasons" DataTextField="SeasonCode" DataValueField="SeasonCode" 
                   AutoPostBack="true" OnSelectedIndexChanged="ddlStatsSeasons_SelectedIndexChanged"></asp:DropDownList>
              </td>
            </tr>
            <%--Scoring Configuration--%>
            <tr runat="server" id="trScoringConfiguration">
              <td class="leftCol">
                <asp:Image runat="server" CssClass="ajaxLoaded" ImageUrl="~/Images/Icons/help.gif" ToolTip="In-sheet statistics will be based on your specific scoring configuration." />
                Scoring Configuration
              </td>
              <td>
                <asp:RadioButton runat="server" ID="rbStandardScoring" AutoPostBack="true" GroupName="Scoring Configuration" 
                  Text="Standard Scoring" Checked="true" oncheckedchanged="rbStandardScoring_CheckedChanged" />
                <asp:RadioButton runat="server" ID="rbPPRScoring" GroupName="Scoring Configuration" AutoPostBack="true"
                  Text="PPR" oncheckedchanged="rbPPRScoring_CheckedChanged"/>
              </td>
            </tr>
            <%--Draft Type--%>
            <tr>
              <td class="leftCol">
                Draft Configuration
              </td>
              <td>
                <asp:RadioButton runat="server" ID="rbSerpentine" GroupName="rbDraftConfiguration" Text="Serpentine" Checked="true" />
                <asp:RadioButton runat="server" ID="rbAuction" GroupName="rbDraftConfiguration" Text="Auction" />
              </td>
            </tr>
            <%--Initial Player Order--%>
            <tr runat="server" id="trSortTypes">
              <td class="leftCol">
                Initial Player Order
              </td>
              <td>
                <asp:RadioButtonList runat="server" ID="rblSortTypes" onselectedindexchanged="rblSortTypes_SelectedIndexChanged" 
                  AutoPostBack="true">
                  <%--CSWR Rankings--%>
                  <asp:ListItem Selected="True" Text="Use Cheat Sheet War Room Rankings" >
                  </asp:ListItem>
                  <%--CBSSports Rankings--%>
                  <asp:ListItem Text="Use CBSSports Rankings" Enabled="false">
                  </asp:ListItem>
                  <%--Sort By Stats--%>
                  <asp:ListItem Text="Sort By Statistic" Value="Stats" />
                </asp:RadioButtonList>
              </td>
            </tr>

            <%--Initial Player Order--%>
            <tr runat="server" id="trTFPOnly">
              <td class="leftCol">
                Initial Player Order
              </td>
              <td style="padding:6px;">
                <asp:Literal runat="server" ID="litForcedTFPSeason"></asp:Literal> Total Fantasy Points
              </td>
            </tr>


            <%--Standard Scoring Stat Sorting--%>
            <tr runat="server" id="trStandardScoringSortStat" visible="false">
              <td  class="leftCol">
                Stat to Sort By
              </td>
              <td>
                <table>
                  <tr>
                    <td>
                      <%--Total Fantasy Points--%>
                      <asp:RadioButton runat="server" ID="rbTFP" GroupName="StandardScoringStats" Checked="true" />
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <%--Fantasy Points Per Game--%>
                      <asp:RadioButton runat="server" ID="rbFPPG" GroupName="StandardScoringStats" />
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
            <%--PPR Stat Sorting--%>
            <tr runat="server" id="trPPRSortStat" visible="false">
              <td  class="leftCol">
                Stat to Sort By
              </td>
              <td>
                <table>
                  <tr>
                    <td>
                      <%--Total Fantasy Points PPR--%>
                      <asp:RadioButton runat="server" ID="rbTFPP" GroupName="PPRStats" Checked="true" />
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <%--Fantasy Points Per Game PPR--%>
                      <asp:RadioButton runat="server" ID="rbFPGP" GroupName="PPRStats" />
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </asp:Panel>

        <%--Submit Button--%>
        <div class="buttonContainer">
          <asp:Button runat="server" ID="butSubmit" Text="Create New Sheet" CssClass="submitButton" OnClick="butSubmit_Click"/>
        </div>

      </ContentTemplate>
    </asp:UpdatePanel>

    </div>

  </div>

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

<%--    $("#<%= butSubmit.ClientID %>").click(function () {
      alert("test");
      var checked = $(".positionBoxes input:checked").length > 0;
      if (!checked) {
        alert("Please check at least one checkbox");
        return false;
      }
      else {
        alert("You checked at least one checkbox")
      }
    });--%>


</script>


  </asp:Content>