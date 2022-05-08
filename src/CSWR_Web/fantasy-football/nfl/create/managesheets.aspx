<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveTwoCol.master" Theme="Web20"AutoEventWireup="true" 
  CodeFile="managesheets.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.ManageSheets" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage" 
  Title="Manage Fantasy Football Cheat Sheets" MetaRobotsText="NOINDEX,FOLLOW" %>
<%@ MasterType VirtualPath="~/MasterPages/Sport.master" %>
<%@ Register Src="~/usercontrols/navigation/SheetCreationManageLevelNavigation.ascx" TagName="SheetCreationManageLevelNavigation" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <%--Navigation--%>
  <cswr:SheetCreationManageLevelNavigation runat="server" ID="scmlnNavigation" CurrentStage="MANAGESHEETS" />

  <%--Sheet Manager--%>
  <div class="manageSheetsPage">

    <div class="row">
      <div class="col-md-12">

        <%--Instuctions--%> 
        <asp:Panel runat="server" ID="panInstructionsMessage" CssClass="alert alert-info">
         This page lists all fantasy football sheets that you have created.  Use this page to edit, validate, open, print, or delete your sheets as needed.
        </asp:Panel>

        <%--No Sheets--%> 
        <asp:Panel runat="server" ID="panNoSheetsMessage" CssClass="alert alert-info">
          Please create your first <a href='newsheet.aspx'>football cheat sheet</a>.
        </asp:Panel>

        <div class="table-responsive">
          <asp:GridView runat="server" ID="gvCheatSheets" AutoGenerateColumns="False" SkinID="Football" PageSize="20" style="margin:auto;width:80%"
            OnRowDataBound="gvCheatSheets_RowDataBound" OnRowCreated="gvCheatSheets_RowCreated" DataKeyNames="CheatSheetID" CssClass="table table-bordered table-condensed" 
            HorizontalAlign="center" onrowdeleting="gvCheatSheets_RowDeleting" ondatabound="gvCheatSheets_DataBound">
            <Columns>
              <%--Sheet Name--%>
              <asp:BoundField DataField="SheetName" HeaderText="Sheet Name" SortExpression="SheetName" />
              <%--Season--%>
              <asp:BoundField DataField="StatsSeasonCode" HeaderText="Stats" SortExpression="StatSeasonCode"/>
              <%--Position--%>
              <asp:TemplateField HeaderText="Positions">
                <ItemTemplate>
                  <asp:Label runat="server" ID="labPositions" />
                </ItemTemplate>
              </asp:TemplateField>
              <%--PPR--%>
              <asp:TemplateField HeaderText="Scoring">
                <ItemTemplate>
                  <asp:Label runat="server" ID="labScoring" />
                </ItemTemplate>
              </asp:TemplateField>
              <%--Item Count--%>
              <asp:TemplateField HeaderText="Players">
                <ItemTemplate>
                  <asp:Label runat="server" ID="labItemCount" />
                </ItemTemplate>
              </asp:TemplateField>
              <%--Updated Timestamp--%>
              <asp:BoundField DataField="LastUpdated" HeaderText="Updated" SortExpression="LastUpdated" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />

              <%--Edit--%>
              <asp:TemplateField HeaderStyle-Width="15">
                <ItemTemplate>
                  <asp:HyperLink runat="server" ID="hlEditSheet" ToolTip="Click to edit the properties of this sheet.">
                    <asp:Image runat="server" ImageUrl="~/Images/GridViewButtons/Edit.gif" />
                  </asp:HyperLink>
                </ItemTemplate>
              </asp:TemplateField>

              <%--Validate--%>
              <asp:TemplateField HeaderStyle-Width="15">
                <ItemTemplate>
                  <asp:HyperLink runat="server" ID="hlValidateSheet" ToolTip="Click to validate your sheet.">
                    <asp:Image runat="server" ImageUrl="~/Images/GridViewButtons/Validate.GIF" />
                  </asp:HyperLink>
                  <asp:Image runat="server" ID="imaValidateSheetDisabled" ImageUrl="~/Images/GridViewButtons/Validate-Faded.GIF"/>
                </ItemTemplate>
              </asp:TemplateField>

              <%--Go to Sheet--%>
              <asp:TemplateField HeaderStyle-Width="15">
                <ItemTemplate>
                  <asp:HyperLink runat="server" ID="hlGoToSheet" ToolTip="Click to open this sheet.">
                    <asp:Image runat="server" ImageUrl="~/Images/GridViewButtons/Sheet.gif" />
                  </asp:HyperLink>
                </ItemTemplate>
              </asp:TemplateField>

              <%--Print Sheet--%>
              <asp:TemplateField HeaderStyle-Width="15">
                <ItemTemplate>
                  <asp:HyperLink runat="server" ID="hlPrintSheet" ToolTip="Click to generate a position-specific, printable version of this cheat sheet." >
                    <asp:Image runat="server" ImageUrl="~/Images/Layout/createsheetcreationpage/headercontrols/print.gif" AlternateText="Edit this cheat sheet" />
                  </asp:HyperLink>
                </ItemTemplate>
              </asp:TemplateField>

              <%--Delete--%>
              <asp:CommandField ButtonType="Image"  HeaderStyle-Width="15" DeleteText="Click to delete this sheet." DeleteImageUrl="~/Images/GridViewButtons/Delete.gif" ShowDeleteButton="True" />
            </Columns>
          </asp:GridView>
        </div>
      </div>
    </div>

</div>


</asp:Content>

