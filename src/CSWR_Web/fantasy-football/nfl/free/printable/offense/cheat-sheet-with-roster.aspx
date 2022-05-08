<%@ Page Language="C#" AutoEventWireup="true" Theme="Web20" EnableViewState="False" CodeFile="cheat-sheet-with-roster.aspx.cs" 
    Inherits="BP.CheatSheetWarRoom.UI.PrintableCheatSheetWithRoster" %>
<%@ Register Src="~/usercontrols/GoogleAnalytics.ascx" TagName="GoogleAnalytics" TagPrefix="cswr" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
  <link href="https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/printable/offense/cheat-sheet-with-roster.aspx" rel="canonical" />
  <meta http-equiv="content-language" content="en=us">
  <link rel="stylesheet" type="text/css" href="~/styles/print.css" media="print" />
  <cswr:GoogleAnalytics runat="server" />
</head>
<body style="background:none;">

<form id="form1" runat="server">

<div class="fooPrint1SheetWithRoster">

  <table class="topContainer">
    <tr>
      <td>
        <!-- Running Backs -->
        <table class="position">
          <tr><th colspan="3">Running Backs</th></tr>
        
          <asp:Repeater runat="server" ID="repRunningBacks" OnItemDataBound="repPositions_ItemDataBound">
            <ItemTemplate>
              <tr>
                <%--Rank--%>
                <td class="numbers">
                  <asp:Label runat="server" ID="labRank" />
                </td>
                <%--CheckBox--%>
                <td style="width:1%;">
                <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" AlternateText="Check to mark a player off of your cheat sheets" />
                </td>
                <%--Player, Bye, and Tags--%>
                <td>
                  <%--Name--%>
                  <asp:Literal runat="server" ID="litPlayerName" />
                  <%--Team Abbreviation--%>
                  <asp:Label runat="server" ID="labTeamAbbreviation" CssClass="team"/>
                  <%--Bye--%>
		              <asp:Label runat="server" ID="labByeWeek" CssClass="byeWeek" ToolTip="This is the player's bye week."/>
 		              <%--Tags--%>
		              <asp:Image runat="server" ID="imaSleeperTag" AlternateText="Indicates the cheat sheet player is a potential sleeper." ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" Visible="false"/>
		              <asp:Image runat="server" ID="imaBustTag" AlternateText="Indicates the cheat sheet player is a potential bust." ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" Visible="false"/>
		              <asp:Image runat="server" ID="imaInjuredTag" AlternateText="Indicates the cheat sheet player is injured." ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" Visible="false"/>
                </td>
              </tr>
            </ItemTemplate>
          </asp:Repeater>
        </table>
	      <%--Kickers--%>
        <table class="position">
          <tr>
            <th colspan="3">Kickers</th>
          </tr>
          <asp:Repeater runat="server" ID="repKickers" OnItemDataBound="repPositions_ItemDataBound">
            <ItemTemplate>
              <tr>
                <%--Rank--%>
                <td class="numbers">
                  <asp:Label runat="server" ID="labRank" />
                </td>
                <%--CheckBox--%>
                <td style="width:1%;">
                  <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" AlternateText="Checkbox for marking off a player." />
                </td>
                <%--Player, Bye, and Tags--%>
                <td>
                  <%--Name--%>
                  <asp:Literal runat="server" ID="litPlayerName" />
                  <%--Team Abbreviation--%>
                  <asp:Label runat="server" ID="labTeamAbbreviation" CssClass="team" />
                  <%--Bye--%>
  		            <asp:Label runat="server" ID="labByeWeek" ToolTip="This is the player's bye week." CssClass="byeWeek"/>
  		            <%--Tags--%>
  		            <asp:Image runat="server" ID="imaSleeperTag" AlternateText="Indicates the cheat sheet player is a potential sleeper." ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" Visible="false"/>
  		            <asp:Image runat="server" ID="imaBustTag" AlternateText="Indicates the cheat sheet player is a potential bust." ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" Visible="false"/>
  		            <asp:Image runat="server" ID="imaInjuredTag" AlternateText="Indicates the cheat sheet player is injured." ImageUrl="~/Images/Sports/Football/PrintableSheet/injured.gif" Visible="false"/>
                </td>
              </tr>
            </ItemTemplate>
            <%--<FooterTemplate>
              </tr>
            </FooterTemplate>--%>
          </asp:Repeater>
        </table>
        
        
        
      </td>
      <td class="centerColumn">
	      <table class="centerTable">
	        <%--Logo--%>
		      <tr>
		        <td>
			        <div class="logo">
  		          <asp:HyperLink runat="server" ID="hlHomePage" NavigateUrl="~/" ToolTip="Click to navigate to the Cheat Sheet War Room home page">
			            <asp:Image runat="server" ID="imaPrintLogo" ImageUrl="~/Images/Layout/printlogo.gif" AlternateText="Printable Fantasy Football Cheat Sheet Logo" />
			          </asp:HyperLink>
			        </div>
              <div class="c2A">
                Why use someone else's rankings when you can create your own 
                <asp:HyperLink runat="server" NavigateUrl="~/fantasy-football/nfl/create/custom-sheet.aspx">custom fantasy football cheat sheet</asp:HyperLink>
                for free?
              </div>
			      </td>
		      </tr>
		      <%--Roster--%>
		      <tr>
		        <td>
              <table class="fooRosterForm">
                <tr>
                  <th colspan="2" class="rosterHeader"><h1>Free, Printable Fantasy Football Cheat Sheet</h1></th>    
                </tr>
                <tr>
                  <th class="positionHeader">Running Backs</th>
                  <th class="positionHeader">Wide Receivers</th>    
                </tr>
                <tr>
                  <td>
                    <table class="rosterLeft">
                      <tr>
                        <th class="genericHeader">Name</th>
                        <th class="genericHeader smallCol">Bye</th>
                      </tr>
                      <tr>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td colspan="2"></td>
                      </tr>
                    </table>
                  </td>
                  <td>
                    <table class="rosterRight">
                      <tr>
                        <th class="genericHeader" style="border-left:1px solid black;">Name</th>
                        <th class="genericHeader smallCol">Bye</th>
                      </tr>
                      <tr>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td colspan="2"></td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <th class="positionHeader">Quarterbacks</th>
                  <th class="positionHeader">Tight Ends</th>    
                </tr>
                <tr>
                  <td>
                    <table class="rosterLeft">
                      <tr>
                        <th class="genericHeader">Name</th>
                        <th class="genericHeader smallCol">Bye</th>
                      </tr>
                      <tr>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td colspan="2"></td>
                      </tr>
                    </table>
                  </td>
                  <td>
                    <table class="rosterRight">
                      <tr>
                        <th class="genericHeader" style="border-left:1px solid black;">Name</th>
                        <th class="genericHeader smallCol">Bye</th>
                      </tr>
                      <tr>
                        <td colspan="2"></td>
                      </tr>
                      <tr>
                        <td colspan="2"></td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <th class="positionHeader">Kickers</th>
                  <th class="positionHeader">Defenses</th>    
                </tr>
                <tr>
                  <td>
                    <table class="rosterLeft">
                      <tr>
                        <th class="genericHeader">Name</th>
                        <th class="genericHeader smallCol">Bye</th>
                      </tr>
                      <tr>
                        <td colspan="2"></td>
                      </tr>
                    </table>
                  </td>
                  <td>
                    <table class="rosterRight">
                      <tr>
                        <th class="genericHeader" style="border-left:1px solid black;">Name</th>
                        <th class="genericHeader smallCol">Bye</th>
                      </tr>
                      <tr>
                        <td colspan="2"></td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
			      </td>
		      </tr>
		      <%--Legend--%>
		      <tr>
		        <td>
		          <div class="legend">
		            <div class="header">
		              Legend
		            </div>
		            <p>
		              <span class="player">Player:</span>
		              Rank 
                  <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" AlternateText="Checkbox for marking off a player." />
		              Player Name
		              <span class="team">(Team)</span>
		              <span class="byeWeek">[Bye]</span>
		              
		              <span class="tags">Tags:</span>
                  <asp:Image runat="server" ID="imaSleeperTag" AlternateText="Indicates the cheat sheet player is a potential sleeper." ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" CssClass="imagePad"/> Sleeper
  		            <asp:Image runat="server" ID="imaBustTag" AlternateText="Indicates the cheat sheet player is a potential bust." ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" CssClass="imagePad"/>  Bust
  		            <asp:Image runat="server" ID="imaInjuredTag" AlternateText="Indicates the cheat sheet player is injured." ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" CssClass="imagePad"/>  Injured		            
		            </p>
		          </div>
		        </td>
		      </tr>
		      
		    </table>
		    <table>
		      <tr>
		        <td>
		          <%--Quarterbacks--%>
              <table class="position">
                <tr>
                  <th colspan="3">Quarterbacks</th>
                </tr>
                <asp:Repeater runat="server" ID="repQuarterbacks" OnItemDataBound="repPositions_ItemDataBound">
                  <ItemTemplate>
                    <tr>
                      <%--Rank--%>
                      <td class="numbers">
                        <asp:Label runat="server" ID="labRank" />
                      </td>
                      <%--CheckBox--%>
                      <td style="width:1%;">
                        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" AlternateText="Checkbox for marking off a player." />
                      </td>
                      <%--Player, Bye, and Tags--%>
                      <td>
                        <%--Name--%>
                        <asp:Literal runat="server" ID="litPlayerName" />
                        <%--Team Abbreviation--%>
                        <asp:Label runat="server" ID="labTeamAbbreviation" CssClass="team" />
                        <%--Bye--%>
  		                  <asp:Label runat="server" ID="labByeWeek" ToolTip="This is the player's bye week." CssClass="byeWeek"/>
  		                  <%--Tags--%>
  		                  <asp:Image runat="server" ID="imaSleeperTag" AlternateText="Indicates the cheat sheet player is a potential sleeper." ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" Visible="false"/>
  		                  <asp:Image runat="server" ID="imaBustTag" AlternateText="Indicates the cheat sheet player is a potential bust." ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" Visible="false"/>
  		                  <asp:Image runat="server" ID="imaInjuredTag" AlternateText="Indicates the cheat sheet player is injured." ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" Visible="false"/>
                      </td>
                    </tr>
                  </ItemTemplate>
                  <%--<FooterTemplate>
                    </tr>
                  </FooterTemplate>--%>
                </asp:Repeater>
              </table>	
		        </td>
		        <td>
              <%--Tight Ends--%>
              <table class="position">
                <tr>
                  <th colspan="3">Tight Ends</th>
                </tr>
                <asp:Repeater runat="server" ID="repTightEnds" OnItemDataBound="repPositions_ItemDataBound">
                  <ItemTemplate>
                    <tr>
                      <%--Rank--%>
                      <td class="numbers">
                        <asp:Label runat="server" ID="labRank" />
                      </td>
                      <%--CheckBox--%>
                      <td style="width:1%;">
                        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" AlternateText="Checkbox for marking off a player." />
                      </td>
                      <%--Player, Bye, and Tags--%>
                      <td>
                        <%--Name--%>
                        <asp:Literal runat="server" ID="litPlayerName" />
                        <%--Team Abbreviation--%>
                        <asp:Label runat="server" ID="labTeamAbbreviation" CssClass="team"/>
                        <%--Bye--%>
  		                  <asp:Label runat="server" ID="labByeWeek" ToolTip="This is the player's bye week." CssClass="byeWeek"/>
  		                  <%--Tags--%>
  		                  <asp:Image runat="server" ID="imaSleeperTag" AlternateText="Indicates the cheat sheet player is a potential sleeper." ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" Visible="false"/>
  		                  <asp:Image runat="server" ID="imaBustTag" AlternateText="Indicates the cheat sheet player is a potential bust." ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" Visible="false"/>
  		                  <asp:Image runat="server" ID="imaInjuredTag" AlternateText="Indicates the cheat sheet player is injured." ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" Visible="false"/>
                      </td>
                    </tr>
                  </ItemTemplate>
                  <%--<FooterTemplate>
                    </tr>
                  </FooterTemplate>--%>
                </asp:Repeater>
              </table>	
		        </td>
		      </tr>
		    </table>
		  </td>  
	    <td>
        <!-- Wide Receivers -->
        <table class="position">
          <tr><th colspan="3">Wide Receivers</th></tr>
        
          <asp:Repeater runat="server" ID="repWideReceivers" OnItemDataBound="repPositions_ItemDataBound">
            <ItemTemplate>
              <tr>
                <%--Rank--%>
                <td class="numbers">
                  <asp:Label runat="server" ID="labRank" />
                </td>
                <%--CheckBox--%>
                <td style="width:1%;">
                  <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" AlternateText="Checkbox for marking off a player." />
                </td>
                <%--Player, Bye, and Tags--%>
                <td>
                  <%--Name--%>
                  <asp:Literal runat="server" ID="litPlayerName" />
                  <%--Team Abbreviation--%>
                  <asp:Label runat="server" ID="labTeamAbbreviation" CssClass="team" />
                  <%--Bye--%>
		              <asp:Label runat="server" ID="labByeWeek" ToolTip="This is the player's bye week." CssClass="byeWeek"/>
 		              <%--Tags--%>
		              <asp:Image runat="server" ID="imaSleeperTag" AlternateText="Indicates the cheat sheet player is a potential sleeper." ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" Visible="false"/>
		              <asp:Image runat="server" ID="imaBustTag" AlternateText="Indicates the cheat sheet player is a potential bust." ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" Visible="false"/>
		              <asp:Image runat="server" ID="imaInjuredTag" AlternateText="Indicates the cheat sheet player is injured." ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" Visible="false"/>
                </td>
              </tr>
            </ItemTemplate>
            <%--<FooterTemplate>
              </tr>
            </FooterTemplate>--%>
          </asp:Repeater>
        </table>
	      <%--Defense--%>
        <table class="position">
          <tr>
            <th colspan="3">Defenses</th>
          </tr>
          <asp:Repeater runat="server" ID="repDefenses" OnItemDataBound="repPositions_ItemDataBound">
            <ItemTemplate>
              <tr>
                <%--Rank--%>
                <td class="numbers">
                  <asp:Label runat="server" ID="labRank" />
                </td>
                <%--CheckBox--%>
                <td style="width:1%;">
                  <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" AlternateText="Checkbox for marking off a player." />
                </td>
                <%--Player, Bye, and Tags--%>
                <td>
                  <%--Name--%>
                  <asp:Literal runat="server" ID="litPlayerName" />
                  <%--Team Abbreviation--%>
                  <asp:Label runat="server" ID="labTeamAbbreviation" CssClass="team" />
                  <%--Bye--%>
  		            <asp:Label runat="server" ID="labByeWeek" ToolTip="This is the player's bye week." CssClass="byeWeek"/>
  		            <%--Tags--%>
  		            <asp:Image runat="server" ID="imaSleeperTag" AlternateText="Indicates the cheat sheet player is a potential sleeper." ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" Visible="false"/>
  		            <asp:Image runat="server" ID="imaBustTag" AlternateText="Indicates the cheat sheet player is a potential bust." ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" Visible="false"/>
  		            <asp:Image runat="server" ID="imaInjuredTag" AlternateText="Indicates the cheat sheet player is injured." ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" Visible="false"/>
                </td>
              </tr>
            </ItemTemplate>
            <%--<FooterTemplate>
              </tr>
            </FooterTemplate>--%>
          </asp:Repeater>
        </table>
	    </td>
    </tr>
  </table>
  
</div>

<div class="fooSecondPage">
  
  <div class="contentConatiner">
    <h2>How this printable fantasy football cheat sheet was generated</h2>
    <p>
      This <strong>printable fantasy football cheat sheet</strong> for <asp:Literal runat="server" id="litDraftYear1"></asp:Literal> was geneated dynamically based on the NFL player rankings from cheatsheetwarroom.com.  
      It includes our free printable rankings <em>by position</em>, making it simple to pick the top players.
    </p>
     
    <p>These printable NFL fantasy football rankings go beyond standard printable rankings to include a few unique features.  Most printable draft sheets are simply a static list of players, but
      here are some of the feaures we use to spice-up our printable sheets.
    </p>

    <h3>1.  A blank roster area to enter your picks</h3>
    <p>
       You can use the <strong>blank roster sheet</strong> at the top of this printable sheet to enter your picks as you make your <asp:Literal runat="server" id="litDraftYear2"></asp:Literal> draft selections at your fantasy draft.  This
       way you can manage your entire draft in one place and easily see how your roster is built as you draft.
    </p>
    <p>
      We even reserve space in the roster sheet to add each player's bye week.
    </p>
    
    <h3>2.  Bye week listed next to each player</h3>
    <p>
      You'll notice that each player listed in this draft cheat sheet also lists their bye week.  You can easily transfer the bye week to the roster area to be sure
      you don't draft too many players at the same position with the same bye week.
    </p>
    
    <h3>3.  Printable player tags for player statuses</h3>
    <p>
      In the legend you can see that there are <em>sleeper</em>, <em>bust</em>, and <em>injured</em> icons that can be added to the printable sheet.  This makes it easy to see which
      players we believe should receive special attention as you're contemplating your picks.
    </p>
    
    <h2>How to create your own printable rankings</h2>

    <p>
      Our goal at Cheat Sheet War Room is to provide fantasy football 
      enthusiasts with a simple, intuitive, and fun way to create printable fantasy football cheat sheets using our free tools.
      Our interactive, web-based, 
      <a href="https://www.cheatsheetwarroom.com/fantasy-football/nfl/create/custom-sheet.aspx">custom fantasy football cheat sheet tool</a>
      integrates supplemental NFL player rankings, 
      configurable tags (sleeper, bust, and injured), statistics, and even an editable player note.
    </p>
    
    <p>After you use our tool to configure your custom rankings, you can generate your own printable NFL rankings (just like this sheet) with the simple click of a button.</p> 
    
    <h2>Check out our comprehensive fantasy football guides</h2>
    
    <p>If you haven't noticed by now, our site is completely free.  If you want to show us some love for the free tools we offer, check out some of the guides we've written:
    </p>
    
    <ul>
      <li><a href="https://www.cheatsheetwarroom.com/blog/fantasy-football/prizes/championship-belts">The Best Fantasy Football Championship Belts for 2018</a></li>
      <li><a href="https://www.cheatsheetwarroom.com/blog/fantasy-football/draft/boards">Fantasy Football Draft Board Buying Guide</a></li>
      <li><a href="https://www.cheatsheetwarroom.com/blog/fantasy-football/prizes/championship-rings">The Top Fantasy Championship Rings</a></li>
      <li><a href="https://www.cheatsheetwarroom.com/blog/football/nfl-game-pass-review">My 2019 Game Pass Review</a></li>
    </ul>
  </div>
</div>







</form>
</body>
</html>
