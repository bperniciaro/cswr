<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveTwoCol.master" AutoEventWireup="true" 
  CodeFile="cheat-sheets.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.PrintableCheatSheets" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
  CanonicalUrl="https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/printable/cheat-sheets.aspx"
  MetaRobotsText="NOINDEX,FOLLOW" 
%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">


<div class="freeFOOPrintablesPage">

  <h1 style="font-size:22px;"><asp:Literal runat="server" ID="litPageHeader" /></h1>

  <p>
    These free, printable fantasy football cheat sheets list our top
    <asp:HyperLink runat="server" ID="hlRankingsLink" NavigateUrl="~/fantasy-football/nfl/free/rankings/player-rankings.aspx"/>
    in a variety of different formats.  Each printable draft sheet is regularly <em>updated and current</em>.  You can choose from
    <a href="#allPositionsCheatSheet">cheat sheets with all positions</a> or
    <a href="#onePositionCheatSheet">position-specific cheat sheets</a>
  </p>

  <p>
    Is your league full of lazy owners who won't take the time to create their own custom cheat sheets?  Then generate a
    <asp:Hyperlink runat="server" ID="hlFakeSheetPage" NavigateUrl="~/fantasy-football/nfl/free/printable/fake-cheat-sheet.aspx">fake fantasy football cheat sheet</asp:Hyperlink>,
    lay them around your war room, and watch as these losers put more and more embarassing picks onto the 
      <a href="https://www.cheatsheetwarroom.com/blog/fantasy-football/draft/boards">fantasy football board</a>.  
  </p>

  <asp:Panel runat="server" ID="panVisitorMessage" style="padding-bottom:10px;">
    But why would you rely on the rankings of some random website when you can create your own 
    <strong>printable fantasy football cheat sheets</strong> for free using drag and drop?  Just click the button below and
    you can begin creating your own customized rankings on an interactive, web-based cheat sheet.
  </asp:Panel>

  <a name="allPositionsCheatSheet"></a>

  <h2>
    Single Sheet Printables with All Positions
  </h2>

  <p>
    If you’re the type of owner who likes to have all of your rankings in front of you, without the hassle of shuffling through stacks of paper, 
    then these options are for you.  Instead of having all of your rankings spread across multiple, printable fantasy football cheat sheets, 
    all of the primary fantasy football positions are listed on a single page for easy reference.
  </p>

      <div class="container-fluid">
        <div class="row singlePositionSheetsContainer">

          <%--10 team printable all positions--%>
          <div class="col-md-6">

            <div class="position">
              <h3>Printable Cheat Sheet with Roster</h3>

              <div class="row">
                <div class="col-sm-6">
                  This printable cheat sheet includes all major offensive positions on a single sheet.
                      <asp:HyperLink runat="server" NavigateUrl="https://www.cheatsheetwarroom.com/blog/football/bye-weeks" Text="NFL bye weeks" CssClass="rankpagelink"/>,
                      teams, 
                      <asp:Label runat="server" ID="labSingleSheetWithRosterYear" />
                      player rankings, and a roster area to enter each of your fantasy draft picks are all included.  This sheet is
                      <strong>best suited for leagues with 10 or fewer teams</strong>.
                </div>
                <div class="col-sm-6" style="text-align:center;padding-top:10px;">
                  <asp:HyperLink runat="server" Target="_blank" NavigateUrl="~/Images/Sports/Football/PrintableSheet/screenshots/printablefantasyfootballcheatsheetwithroster.gif" ToolTip="Click to view a sample screenshot of a single-page printable cheat sheet with a roster area.">
                    <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/screenshots/cheatsheetwithroster-small.gif" AlternateText="Printable Sheet With Roster - Single Page" />
                    </asp:HyperLink>
                    <div style="font-size:10px;">(click to view sample)</div>
                </div>
              </div>
              <div class="buttonContainer">
                <asp:Button runat="server" ID="lbPrintableCheatSheetWithRoster" PostBackUrl="~/fantasy-football/nfl/free/printable/offense/cheat-sheet-with-roster.aspx" CssClass="btn btn-primary"
                      Text="Generate Single Sheet /w Roster"  ToolTip="Click to generate a free, printable, single-page cheat sheet for leagues with 10 of fewer teams."/>
              </div>
            </div>

          </div>

          <%--12 team printable all positions--%>
          <div class="col-md-6">

            <div class="position">

              <h3>Printable Cheat Sheet without Roster</h3>

              <div class="row">
                <div class="col-sm-6">
                  <p>
                      This printable cheat sheet includes all major offensive positions (QB, RB, WR, TE, K, DF) on a single sheet.
                      Bye weeks, teams, and 
                      <asp:Label runat="server" ID="Label1" />
                      player rankings, are all included. This sheet is
                      <strong>best suited for leagues with more than 10 teams</strong>.
                    </p>
                </div>
                <div class="col-sm-6" style="text-align:center;padding-top:10px;"">
                  <asp:HyperLink runat="server" Target="_blank" NavigateUrl="~/Images/Sports/Football/PrintableSheet/screenshots/printablefantasyfootballcheatsheetwithoutroster.gif" ToolTip="Click to view a sample screenshot of a single-page printable cheat sheet without a roster area.">
                      <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/screenshots/cheatsheetwithoutroster-small.gif" AlternateText="Printable Sheetout With Roster - Single Page" />
                    </asp:HyperLink>
                    <div style="font-size:10px;">(click to view sample)</div>

                </div>
              </div>
              <div class="buttonContainer">
                <asp:Button runat="server" PostBackUrl="~/fantasy-football/nfl/free/printable/offense/cheat-sheet-without-roster.aspx" CssClass="btn btn-primary"
                                     Text="Generate Single Sheet w/o Roster"  ToolTip="Click to generate a free, printable, single-page cheat sheet for leagues with more than 10 teams."/>
              </div>

            </div>

          </div>
        </div>
      </div>  <!-- close containerFluid -->


  <a name="onePositionCheatSheet"></a>

  <h2>
    Position-Specific, Printable Fantasy Football  Cheat Sheets
  </h2>

  <p>
    Some fantasy owners want room to add notes to their printable fantasy football cheat sheets.  If this is your preference, use the links below
    to generate printable cheat sheets which are position-specific.  Each sheet will have ample room to add supplemental 
    information to aid in your draft preparation.
  </p>


  <div class="container-fluid singlePositionSheetsContainer">
    <%--Row 1--%>
    <div class="row">
      <div class="col-md-6">
        <div class="position">
          <h3>Printable Quarterbacks Sheet</h3>
          <div class="row">
            <div class="col-sm-6">
              <p style="font-style:italic;font-size:13px;">
                    This printable cheat includes our top 
                    <asp:Label runat="server" ID="labMaxQBs" />
                    QBs for <asp:Label runat="server" ID="labQBsYear" />.  In addition to 
                    <asp:HyperLink runat="server" CssClass="rankpagelink" NavigateUrl="~/fantasy-football/nfl/free/rankings/offense/quarterbacks.aspx" Text="quarterback rankings" />
                    each sheet includes 
                    <asp:Label runat="server" ID="labQBDescription" />
                  </p>
            </div>
            <div class="col-sm-6" style="text-align:center;padding-top:10px;"">
              <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/positions/quarterback-sheet.gif" />
            </div>
          </div>
          <div class="buttonContainer">
             <asp:Button runat="server" PostBackUrl="~/fantasy-football/nfl/free/printable/offense/quarterbacks-cheat-sheet.aspx" Target="_blank" CssClass="btn btn-primary"
                  Text="Generate QB Cheat Sheet" ToolTip="Click to generate a free printable quarterbacks cheat sheet for your fantasy draft." />
          </div>
        </div>
      </div>
      <div class="col-md-6">
        <div class="position">
          <h3>Printable Running Backs Sheet</h3>

          <div class="row">
            <div class="col-sm-6">
               <p style="font-style:italic;font-size:13px;">
                  This printable cheat includes our top 
                  <asp:Label runat="server" ID="labMaxRBs" />
                  RBs for <asp:Label runat="server" ID="labRBsYear" />.  In addition to 
                  <asp:HyperLink runat="server" CssClass="rankpagelink" NavigateUrl="~/fantasy-football/nfl/free/rankings/offense/running-backs.aspx" Text="running back rankings" />
                  each sheet includes 
                  <asp:Label runat="server" ID="labRBDescription" />
                </p>
            </div>
            <div class="col-sm-6" style="text-align:center;padding-top:10px;"">
              <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/positions/running-back-sheet.gif" />
            </div>
          </div>
          <div class="buttonContainer">
            <asp:Button runat="server" PostBackUrl="~/fantasy-football/nfl/free/printable/offense/running-backs-cheat-sheet.aspx" Target="_blank" CssClass="btn btn-primary"
                  Text="Generate RB Cheat Sheet"  ToolTip="Click to generate a free printable running backs cheat sheet for your fantasy draft."/>
          </div>
        </div>

      </div>
    </div>  <!-- close row -->

    <%--Row 2--%>
    <div class="row singlePositionSheetsContainer">
      <div class="col-md-6">
        <div class="position">
           <h3>Printable Wide Receivers Sheet</h3>

           <div class="row">
            <div class="col-sm-6">
              <p style="font-style:italic;font-size:13px;">
                  This printable cheat includes our top 
                  <asp:Label runat="server" ID="labMaxWRs" />
                  WRs for <asp:Label runat="server" ID="labWRsYear" />.  In addition to 
                  <asp:HyperLink runat="server" CssClass="rankpagelink" NavigateUrl="~/fantasy-football/nfl/free/rankings/offense/wide-receivers.aspx" Text="wide receiver rankings" />
                  each sheet includes 
                  <asp:Label runat="server" ID="labWRDescription" />
                </p>
            </div>
            <div class="col-sm-6" style="text-align:center;padding-top:10px;"">
              <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/positions/wide-receiver-sheet.gif" />
            </div>
          </div>
          <div class="buttonContainer">
            <asp:Button ID="HyperLink6" runat="server" PostBackUrl="~/fantasy-football/nfl/free/printable/offense/wide-receivers-cheat-sheet.aspx" Target="_blank" CssClass="btn btn-primary"
                  Text="Generate WR Cheat Sheet"  ToolTip="Click to generate a free printable wide receivers cheat sheet for your fantasy draft." />
          </div>
        </div>
      </div>
      <div class="col-md-6">

        <div class="position">
        <h3>Printable Tight Ends Sheet</h3>
          <div class="row">
            <div class="col-sm-6">
              <p style="font-style:italic;font-size:13px;">
                  This printable cheat includes our top 
                  <asp:Label runat="server" ID="labMaxTEs" />
                  TEs for <asp:Label runat="server" ID="labTEsYear" />.  In addition to 
                  <asp:HyperLink runat="server" CssClass="rankpagelink" NavigateUrl="~/fantasy-football/nfl/free/rankings/offense/tight-ends.aspx" Text="tight end rankings" />
                  each sheet includes 
                  <asp:Label runat="server" ID="labTEDescription" />
                </p>
            </div>
            <div class="col-sm-6" style="text-align:center;padding-top:10px;"">
              <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/positions/tight-end-sheet.gif" />
            </div>
          </div>
          <div class="buttonContainer">
            <asp:Button runat="server" PostBackUrl="~/fantasy-football/nfl/free/printable/offense/tight-ends-cheat-sheet.aspx" Target="_blank" CssClass="btn btn-primary" 
                  Text="Generate TE Cheat Sheet"  ToolTip="Click to generate a free printable tight ends cheat sheet for your fantasy draft." />
          </div>
        </div>
      </div>
    </div>  <!-- close row -->

    <%--Row 3--%>
    <div class="row singlePositionSheetsContainer">
      <div class="col-md-6">
        <div class="position">
          <h3>Printable Kickers Sheet</h3>

          <div class="row">
            <div class="col-sm-6">
              <p style="font-style:italic;font-size:13px;">
                  This printable cheat includes our top 
                  <asp:Label runat="server" ID="labMaxKs" />
                  kickers for <asp:Label runat="server" ID="labKsYear" />.  In addition to 
                  <asp:HyperLink runat="server" CssClass="rankpagelink" NavigateUrl="~/fantasy-football/nfl/free/rankings/offense/kickers.aspx" Text="kicker rankings" />
                  each sheet includes 
                  <asp:Label runat="server" ID="labKDescription" />
                </p>
            </div>
            <div class="col-sm-6" style="text-align:center;padding-top:10px;"">
              <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/positions/kicker-sheet.gif" />
            </div>
          </div>
          <div class="buttonContainer">
            <asp:Button runat="server" PostBackUrl="~/fantasy-football/nfl/free/printable/offense/kickers-cheat-sheet.aspx" Target="_blank" CssClass="btn btn-primary" 
                  Text="Generate K Cheat Sheet"  ToolTip="Click to generate a free printable kickers cheat sheet for your fantasy draft." />
          </div>
        </div>


      </div>
      <div class="col-md-6">

        <div class="position">
          <h3>Printable Defenses Sheet</h3>

          <div class="row">
            <div class="col-sm-6">
               <p style="font-style:italic;font-size:13px;">
                  This printable cheat includes our top 
                  <asp:Label runat="server" ID="labMaxDFs" />
                  defenses <asp:Label runat="server" ID="labDFsYear" />.  In addition to 
                  <asp:HyperLink runat="server" CssClass="rankpagelink" NavigateUrl="~/fantasy-football/nfl/free/rankings/offense/defenses.aspx" Text="defense & ST rankings" />
                  each sheet includes 
                  <asp:Label runat="server" ID="labDFDescription" />
                </p>
            </div>
            <div class="col-sm-6" style="text-align:center;padding-top:10px;">
              <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/positions/defense-special-teams-sheet.gif" />
            </div>
          </div>
          <div class="buttonContainer">
            <asp:Button runat="server" PostBackUrl="~/fantasy-football/nfl/free/printable/offense/defenses-cheat-sheet.aspx" Target="_blank" CssClass="btn btn-primary" 
                  Text="Generate DF Cheat Sheet"  ToolTip="Click to generate a free printable defenses cheat sheet for your fantasy draft." />
          </div>
        </div>

      </div>
    </div>  <!-- close row -->
  </div>  <!-- close containerFluid -->


  </div>


</asp:Content>

