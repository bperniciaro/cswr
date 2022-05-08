<%@ Page Language="C#" MasterPageFile="~/MasterPages/Sport.master" AutoEventWireup="true" 
  CodeFile="cheat-sheets.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.FantasyFootballCheatSheets" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"  
  Title="Create Free NFL Cheat Sheets for your Fantasy Football Draft"
  MetaDescription="Our free fantasy football cheat sheets provide a wide array of features right inside the cheat sheet itself."
  CanonicalUrl="http://www.cheatsheetwarroom.com/fantasy-football/nfl/cheat-sheets.aspx"
%>
<%@ MasterType VirtualPath="~/MasterPages/Sport.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <div style="padding:0px 50px;">
  
    <h1>
      <asp:Image runat="server" ID="imaLandingHeaderImage" ImageUrl="~/Images/Layout/footballlandingimage.gif" AlternateText="Fantasy Football Cheat Sheets" />
    </h1>
              
    <p class="introArticle">
      Cheat Sheet War Room is a completely free online tool for creating customized
      <strong>fantasy football cheat sheets</strong>.  Our 
      <asp:HyperLink runat="server" NavigateUrl="~/fantasy-football/nfl/create/custom-sheet.aspx">custom fantasy football cheat sheet</asp:HyperLink>
      incorporates an assortment of useful information directly into the cheat sheet
      itself.  This allows you to easily create educated rankings without having to
      scour the web for player information or hassle with clumsy spreadsheets.  Each of
      our free, interactive fantasy NFL cheat sheets includes the following features:
    </p>

    <%--Call to Action--%>
    <asp:Panel runat="server" ID="panCreateSheetContainer" CssClass="c2a">
      <table>
        <tr>
          <td class="arrowCell">
            <asp:Image runat="server" ImageUrl="~/Images/Layout/landingpagearrows.gif" AlternateText="Football Cheat Sheet Creation" />
          </td>
          <td>
            <asp:HyperLink runat="server" NavigateUrl="~/access/register.aspx" CssClass="createButton">REGISTER TO GET STARTED</asp:HyperLink>
          </td>
        </tr>
        <tr>
          <td colspan="2" class="noThanks">
            <asp:HyperLink runat="server" ToolTip="Click to create a free football cheat sheet.  If you like it, register for an [always] free account!" NavigateUrl="~/fantasy-football/nfl/create/custom-sheet.aspx">No thanks, I want to try it first</asp:HyperLink>
          </td>
        </tr>
      </table>
    </asp:Panel>

    <h2>Football Cheat Sheet Features</h2>
              
    <div class="features">

      <table>
        <tr>
                  
          <!-- Left Side -->
          <td>
            <ul>
              <li>
                <h3>Simple Drag & Drop Interface</h3>
                <p>Just point, click, and drag players to create custom rankings</p>
              </li>
              <li>
                <h3>Supplemental Player Rankings</h3>
                <p>
                  Compare your rankings against supplemental
                  <asp:HyperLink runat="server" ID="hlPlayerRankings" NavigateUrl="~/fantasy-football/nfl/free/rankings/player-rankings.aspx"></asp:HyperLink>
                </p>
              </li>
              <li>
                <h3>Configurable Tags</h3>
                  <p>Easily tag a player as a <em>sleeper</em>, <em>bust</em>, or <em>injured</em> and transfer this to your
                  <asp:HyperLink runat="server" NavigateUrl="~/fantasy-football/nfl/free/printable/offense/cheat-sheet-with-roster.aspx" Target="_blank">printable cheat sheet</asp:HyperLink>
                </p>
              </li>
              <li>
                <h3>Configurable Notes</h3>
                <p>Add custom notes on each player to assist in your rankings</p>
              </li>
              <li>
                <h3>Player News Search</h3>
                <p>Search recent NFL player news with a custom search engine</p>
              </li>
            </ul>
          </td>
                    
          <!-- Right Side -->
          <td>
                    
            <ul>
              <li>
                <h3>Sheet Customization Tools</h3>
                <p>
                  Easily add players to your sheet, remove players from your sheet, or sort players based on key stats
                </p>
              </li>
              <li>
                <h3>Comprehensive Player Profiles</h3>
                <p>Each player template includes player name, team, years of experience, number, and bye week</p>
              </li>
              <li>
                <h3>Critical Statistics</h3>
                <p>Position-specific stats including point output based on the standard
                  <a href="https://www.cheatsheetwarroom.com/blog/fantasy-football/leagues/scoring-systems">fantasy football scoring system</a>
                </p>
              </li>
              <li>
                <h3>Export Capability</h3>
                <p>Export your rankings and all cheat sheet data to a spreadsheet for further processing</p>
              </li>
              <li>
                <h3>Social Bookmarking & Networking</h3>
                <p>Easily bookmark your sheets and share with your friends</p>
              </li>
            </ul>
                    
          </td>
        </tr>
      </table>

              
    </div> <!-- close landing -->
              
  </div>

</asp:Content>

