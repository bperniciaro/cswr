<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveTwoCol.master" Theme="Web20" AutoEventWireup="true" 
  CodeFile="newsheet.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.NewSheet" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
  Title="Create New Fantasy Football Cheat Sheet" MetaRobotsText="NOINDEX,FOLLOW" %>
<%@ MasterType VirtualPath="~/MasterPages/Sport.master" %>
<%@ Register Src="~/usercontrols/navigation/SheetCreationManageLevelNavigation.ascx" TagName="SheetCreationManageLevelNavigation" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">

<asp:ScriptManager runat="server" />


<%--Navigation--%>
<cswr:SheetCreationManageLevelNavigation runat="server" ID="scmlnNavigation" CurrentStage="NEWSHEET" SportCode="FOO" />

<%--Main Container--%>
<asp:Panel runat="server"  CssClass="createSheetPage">

  <div class="row">

    <div class="col-md-12">

      <%--Instuctions--%> 
      <asp:Panel runat="server" ID="panInstructionsMessage" CssClass="alert alert-info">
        Create and configure a new custom sheet
      </asp:Panel>

      <%--Put the entire form in an update panel--%>
      <asp:UpdateProgress ID="upUpdateProgress" AssociatedUpdatePanelID="upUpdatePanel" runat="server" DynamicLayout="false" DisplayAfter="0">
        <ProgressTemplate>      
          <div id="ajaxLoaderOverlay" class="ajaxOverlay" style="margin-top:80px;">
            <asp:Image runat="server" ImageUrl="~/Images/Animations/googlerotate.gif"/>
          </div>      
        </ProgressTemplate>
      </asp:UpdateProgress>
      <asp:UpdatePanel runat="server" ID="upUpdatePanel">
        <ContentTemplate>
               
          <%--<asp:Panel runat="server" CssClass="container-fluid">--%>
            <%--<asp:Panel runat="server" CssClass="row">--%>
              <asp:Panel runat="server" CssClass="form-horizontal" style="padding:20px;">

                <h1>Create New Sheet</h1>
                <br />
                
                <%--Status Message--%>
                <asp:Panel runat="server" ID="panStatusMessage" CssClass="alert alert-error" Visible="false">
                  <asp:Label runat="server" ID="labStatusMessage" />
                </asp:Panel>

                <%--Maximum Sheets Message--%>
                <asp:Panel runat="server" ID="panMaximumSheets"  CssClass="alert alert-warning" Visible="false">
                    You have already created the maximum number of sheets <span class="bold">(12)</span>.  If you feel you need more sheets, please 
                    <asp:HyperLink runat="server" ID="hlGeneralQuestion" NavigateUrl="~/Contact.aspx?Type=GeneralQuestion" Text="contact the site administrator" />
                    to discuss your particular needs.  Alternatively, you can delete one or more of your current sheets through the 
                    <asp:HyperLink runat="server" ID="hlManageSheets" NavigateUrl="~/fantasy-football/nfl/create/managesheets.aspx" Text="manage sheets" />
                    page.
                </asp:Panel>

                <%--Sheet Name--%>
                <div class="form-group">
                  <label class="control-label col-sm-4">
                      <span class="required">(required)</span> Sheet Name 
                  </label>
                  <div class="col-sm-8">
                    <asp:TextBox runat="server" ID="tbSheetName" MaxLength="50" CssClass="sheetName"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvNameRequired" ControlToValidate="tbSheetName" Display="Dynamic" SetFocusOnError="true"
                      ErrorMessage="<img src='../../../Images/error.gif' alt='Sheet Name is required.' title='Sheet Name is required.' />" 
                      ToolTip="Sheet Name is required."></asp:RequiredFieldValidator> 
                  </div> <!-- close col-sm-8 -->
                </div>  <!-- close sheetname div -->

                <%--Position--%>
                <asp:Panel runat="server" ID="panSheetPositions" CssClass="form-group">
                  <label class="control-label col-sm-4">
                    <span class="required">(required)</span> Sheet Positions 
                  </label>
                  <div class="col-sm-8">
                    <asp:CheckBoxList runat="server" ID="cblPositions" DataTextField="PositionCode" DataValueField="PositionCode" CssClass="positionList positionBoxes" 
                       RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="cblPositions_SelectedIndexChanged"/>
                  </div>
                </asp:Panel>

                <%--Stat Season--%>
                <asp:Panel runat="server" ID="panStatsSeason" CssClass="form-group">
                  <label class="control-label col-sm-4">
                    <span class="required">(required)</span> Stats Season 
                  </label>
                  <div class="col-sm-8">
                    <asp:DropDownList runat="server" ID="ddlStatsSeasons" DataTextField="SeasonCode" DataValueField="SeasonCode" 
                       AutoPostBack="true" OnSelectedIndexChanged="ddlStatsSeasons_SelectedIndexChanged"></asp:DropDownList>
                  </div>
                </asp:Panel>

                <%--Scoring Configuration--%>
                <asp:Panel runat="server" ID="panScoringConfiguration" CssClass="form-group">
                  <label class="control-label col-sm-4">
                    <span class="required">(required)</span> Name 
                  </label>
                  <div class="col-sm-8">
                    <asp:RadioButton runat="server" ID="rbStandardScoring" AutoPostBack="true" GroupName="Scoring Configuration" 
                      Text="Standard Scoring" Checked="true" oncheckedchanged="rbStandardScoring_CheckedChanged" />
                    <asp:RadioButton runat="server" ID="rbPPRScoring" GroupName="Scoring Configuration" AutoPostBack="true"
                      Text="PPR" oncheckedchanged="rbPPRScoring_CheckedChanged"/>
                    </div>
                </asp:Panel>

                <%--Draft Type--%>
                <asp:Panel runat="server" ID="panDraftConfiguration" CssClass="form-group">
                  <label class="control-label col-sm-4">
                    Draft Configuration 
                  </label>
                  <div class="col-sm-8">
                    <asp:RadioButton runat="server" ID="rbSerpentine" GroupName="rbDraftConfiguration" Text="Serpentine" Checked="true" />
                    <asp:RadioButton runat="server" ID="rbAuction" GroupName="rbDraftConfiguration" Text="Auction" />
                  </div>
                </asp:Panel>

                <%--Standard Initial Player Order--%>
                <asp:Panel runat="server" ID="panInitialPlayerOrder" CssClass="form-group">
                  <label class="control-label col-sm-4">
                    Initial Player Order 
                  </label>
                  <div class="col-sm-8">
                    <asp:RadioButtonList runat="server" ID="rblSortTypes" onselectedindexchanged="rblSortTypes_SelectedIndexChanged" AutoPostBack="true">
                      <asp:ListItem Selected="True" Text="Use Cheat Sheet War Room Rankings" ></asp:ListItem>
                      <asp:ListItem Text="Use CBSSports Rankings" Enabled="false"></asp:ListItem>
                      <asp:ListItem Text="Sort By Statistic" Value="Stats" />
                    </asp:RadioButtonList>
                  </div>
                </asp:Panel>

                <%--PPR Initial Player Order--%>
                <asp:Panel runat="server" ID="panPprInitialPlayerOrder"  CssClass="form-group">
                  <label class="control-label col-sm-4">
                    Initial Player Order
                  </label>
                  <div class="col-sm-8" style="padding-top:7px;">
                    <asp:Literal runat="server" ID="litForcedTFPSeason"></asp:Literal> Total Fantasy Points
                  </div>
                </asp:Panel>

                <%--Standard Scoring Sorting--%>
                <asp:Panel runat="server" ID="panStandardScoringSorting" CssClass="form-group" Visible="false">
                  <label class="control-label col-sm-4">
                    Standard Stat Scoring 
                  </label>
                  <div class="col-sm-8">
                    <asp:RadioButton runat="server" ID="rbTFP" GroupName="StandardScoringStats" Checked="true" /><br />
                    <asp:RadioButton runat="server" ID="rbFPPG" GroupName="StandardScoringStats" />
                  </div>
                </asp:Panel>

                <%--PPR Stat Scoring--%>
                <asp:Panel runat="server" ID="panPprScoringSorting" CssClass="form-group" Visible="false">
                  <label class="control-label col-sm-4">
                    Stat Scoring 
                  </label>
                  <div class="col-sm-8">
                     <asp:RadioButton runat="server" ID="rbTFPP" GroupName="PPRStats" Checked="true" /><br />
                     <asp:RadioButton runat="server" ID="rbFPGP" GroupName="PPRStats" />
                  </div>
                </asp:Panel>

                <%--Submit Button--%>
                <asp:Panel runat="server" CssClass="form-group">
                  <div class="col-sm-offset-4 col-sm-8">
                    <asp:Button runat="server" ID="butSubmit" Text="Create New Sheet" CssClass="submitButton" OnClick="butSubmit_Click"/>
                  </div>
                </asp:Panel>

              </asp:Panel>  <!-- close form-horizontal -->

            <%--</asp:Panel> --%>  <!-- close row -->

          <%--</asp:Panel>--%>  <!-- close containerFluid -->


        </ContentTemplate>
      </asp:UpdatePanel>  <!-- close UpdatePanel -->


    </div>
 <%-- </div>--%>
    </div>


</asp:Panel>  <!-- close createSheetPage -->

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