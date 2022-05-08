<%@ Page Title="Create New Fantasy Racing Cheat Sheet" Language="C#" MasterPageFile="~/MasterPages/Sport.master" 
  AutoEventWireup="true" CodeFile="newsheet.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.NewSheet" 
  CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage" MetaRobotsText="NOINDEX,FOLLOW" %>
<%@ MasterType VirtualPath="~/MasterPages/Sport.master" %>
<%@ Register Src="~/usercontrols/navigation/SheetCreationManageLevelNavigation.ascx" TagName="SheetCreationManageLevelNavigation" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">


  <asp:ScriptManager runat="server" />

<%--Creation Navigation--%> 
<cswr:SheetCreationManageLevelNavigation runat="server" ID="scmlnNavigation" CurrentStage="NEWSHEET" SportCode="RAC" />

<%--Main Container--%>
<div class="createSheetPage">

  <div class="racForm">
    
    <%--Put the entire form in an update panel--%>
    <asp:UpdatePanel runat="server" ID="upUpdatePanel">
      <ContentTemplate>

        <%--Maximum Sheets Message--%>
        <asp:Panel runat="server" ID="panMaximumSheets" Visible="false">
          <p class="warning">
            You have already created the maximum number of sheets <span class="bold">(12)</span>.  If you feel you need more sheets, please 
            <asp:HyperLink runat="server" NavigateUrl="~/Contact.aspx?Type=GeneralQuestion" Text="contact the site administrator" />
            to discuss your particular needs.  Alternatively, you can delete one or more of your current sheets through the 
            <asp:HyperLink runat="server" NavigateUrl="~/fantasy-football/nfl/create/managesheets.aspx" Text="manage sheets" />
            page.
          </p>
        </asp:Panel>

        <%--Standard Interface--%>
        <asp:Panel runat="server" ID="panStandardInterface" CssClass="formContainer">

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
              <td class="leftCol">
                <span class="required">(required)</span>
                Sheet Name 
              </td>
              <td>
                <asp:TextBox runat="server" ID="tbSheetName" MaxLength="50" Width="175"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvNameRequired" ControlToValidate="tbSheetName" Display="Dynamic" SetFocusOnError="true"
                  ErrorMessage="<img src='../../../Images/error.gif' alt='Sheet Name is required.' title='Sheet Name is required.' />" 
                  ToolTip="Sheet Name is required."></asp:RequiredFieldValidator> 
              </td>
            </tr>
            <%--Initial Player Order--%>
            <tr runat="server" id="trInitialPlayerOrder">
              <td class="leftCol">
                Initial Driver Order
              </td>
              <td>
                <asp:RadioButtonList runat="server" ID="rblSortTypes" RepeatLayout="Flow">
                  <%--CSWR Rankings--%>
                  <asp:ListItem Selected="True">
                    Use <abbr title="Cheat Sheet War Room">CSWR</abbr> Rankings
                  </asp:ListItem>
                  <%--Sort By Stats--%>
                  <asp:ListItem Value="Stats" />
                </asp:RadioButtonList>
              </td>
            </tr>

          </table>
        </asp:Panel>

        <%--Submit Button--%>
        <div class="buttonContainer">
          <asp:Button runat="server" ID="butSubmit" Text="Create New Sheet" OnClick="butSubmit_Click" />
        </div>

        <%--This control is shown during AJAX requests to the server--%>      
        <div id="updateProgressDiv" style="display:none;height:40px;width:40px;">
          <asp:Image runat="server" ImageUrl="~/Images/Animations/googlerotate.gif" />
        </div>

      </ContentTemplate>
    </asp:UpdatePanel>


    </div>

  </div>
  
    <!-- This is the JQuery to show the source of the supplemental rankings -->
  <script type="text/javascript">

    // Create the tooltips only on document load
    $(document).ready(function () {
      $('.ajaxLoaded').qtip()
    });

  </script>

  <%--Positions--%>  
  <asp:ObjectDataSource runat="server" ID="odsPositions" SelectMethod="GetPositions" TypeName="BP.CheatSheetWarRoom.BLL.Sheets.Position">
    <SelectParameters>
      <asp:Parameter Name="sportCode" Type="String" DefaultValue="FOO" />
    </SelectParameters>
  </asp:ObjectDataSource>

</asp:Content>

