<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="scraperankings.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.ScrapeRankings" Title="Scrape Rankings" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

  <div class="scrapeRankingsPage">

    <br /><br />

    <asp:DropDownList runat="server" ID="ddlSeasons" DataTextField="SeasonCode" DataValueField="SeasonCode" AutoPostBack="true" 
    ondatabound="ddlSeasons_DataBound" /> 

    <h2>Scrape Rankings</h2>
  
    <%--Status Message--%>
    <asp:Panel runat="server" ID="panSuccessPanel" CssClass="success">
      <asp:Label runat="server" ID="labResult" />
    </asp:Panel>

    <%--Heading--%>
    <div style="padding:10px 0px;">
        <strong><asp:Literal runat="server" ID="litRankingTitle" /> Rankings</strong> (<asp:HyperLink runat="server" ID="hlViewSupplementalSheet" Text="view rankings" Target="_blank" Visible="false"/>)
    </div>
  

    <%--Posible Sources--%>
    <asp:Panel runat="server" ID="panSourcesPanel">
      <table>
      <%--Supplemental Source--%>
        <tr>
          <td>Select Source</td>
          <td>
            <asp:DropDownList runat="server" ID="ddlSupplementalSources">
              <asp:ListItem Text="Select Source" Value="0" />
              <asp:ListItem Text="NFL" Value="NFL" />
              <asp:ListItem Text="CBS Sports" Value="CBS" />
            </asp:DropDownList>
          </td>
        </tr>
        <%--Position--%>
        <tr>
          <td>Select Position</td>
          <td>
            <asp:DropDownList runat="server" ID="ddlPositions">
              <asp:ListItem Text="Select Position" Value="0" />
              <asp:ListItem Text="QB" Value="QB" />
              <asp:ListItem Text="RB" Value="RB" />
              <asp:ListItem Text="WR" Value="WR" />
              <asp:ListItem Text="TE" Value="TE" />
              <asp:ListItem Text="K" Value="K" />
              <asp:ListItem Text="DF" Value="DF" />
            </asp:DropDownList>
          </td>
        </tr>
        <%--Buttons--%>
        <tr>
          <td colspan="2">
            <asp:Button runat="server" ID="butGrabRankings" Text="Poll Rankings" OnClick="butGrabRankings_Click" CausesValidation="false" /> 
          </td>
        </tr>
      </table>
    </asp:Panel>

    <%--Drop Down Options--%>
    <asp:Panel runat="server" ID="panRankingsPanel" style="padding-left:30px;" Visible="false">
  
      <table>
        <asp:Repeater runat="server" ID="repRankings" OnItemDataBound="repRankings_ItemDataBound">
          <ItemTemplate>
            <tr>
              <td>
                <%--Rank--%>
                <asp:Label runat="server" ID="labRank" />.
                
                <%--Player Found, so Store Value in Hidden Field--%>
                <asp:Literal runat="server" ID="litFoundPlayer" />
                <asp:HiddenField runat="server" ID="hfFoundPlayer" />
                
                <%--Player Not Found, so Display DropDown--%>
                <asp:Label runat="server" ID="labSheetPlayer" />
                <%--Rookie Designation--%>
                <asp:Label runat="server" ID="labRookieDesignation" style="color:Blue;font-size:18px;font-weight:bold;" Visible="false">R</asp:Label>
                <%--Dropdown Choices--%>
                <asp:DropDownList runat="server" ID="ddlPlayers" AppendDataBoundItems="true">
                  <asp:ListItem Text="Select Player" Value="0" />
                </asp:DropDownList>
                <%--New Team--%>
                <asp:Label runat="server" ID="labWrongTeam" style="color:red;font-size:10px;"/>
                <%--Required Field--%>
                <asp:RequiredFieldValidator runat="server" ID="rfvSeasonRequired" ControlToValidate="ddlPlayers" SetFocusOnError="True" 
                  Text="You must rank this player." Tooltip="Players are required" InitialValue="0"></asp:RequiredFieldValidator>
              </td>
            </tr>
          </ItemTemplate>
        </asp:Repeater>
      </table>
      <div style="text-align:center;">
        <asp:Button runat="server" ID="butCommitOrder" Text="Commit Rankings" OnClick="butCommitOrder_Click" />
      </div>
    </asp:Panel>
  </div>

</asp:Content>

