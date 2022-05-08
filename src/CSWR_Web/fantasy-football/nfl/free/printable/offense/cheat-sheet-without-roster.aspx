<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" Theme="" 
  CodeFile="cheat-sheet-without-roster.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.PrintableCheatSheetWithoutRoster" %>
<%@ Register Src="~/usercontrols/GoogleAnalytics.ascx" TagName="GoogleAnalytics" TagPrefix="cswr" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
  <link href="https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/printable/offense/cheat-sheet-without-roster.aspx" rel="canonical" />
  <meta http-equiv="content-language" content="en=us">
  <link rel="stylesheet" type="text/css" href="~/styles/print.css" media="print" />
  <cswr:GoogleAnalytics runat="server" />
</head>
<body style="background:none;">

<form id="form1" runat="server">

<div class="fooPrint1SheetNoRoster">

  <table class="topContainer">
    <tr>
      <td>
        <!-- Quarterbacks -->
        <table class="position">
        
          <tr><th colspan="3">Running Backs</th></tr>
        
          <asp:Repeater runat="server" ID="repRunningBacks" OnItemDataBound="repPosition_ItemDataBound">
            <ItemTemplate>
              <tr>
                <%--Rank--%>
                <td class="numbers">
                  <asp:Label runat="server" ID="labRank" />
                </td>
                <%--CheckBox--%>
                <td style="width:1%;">
                  <asp:Image runat="server" ID="imaCheckBox" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" AlternateText="Checkbox for Fantasy Player." />
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
		              <asp:Image runat="server" ID="imaSleeperTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" Visible="false" ToolTip="This tag signifies that the player is a sleeper."/>
		              <asp:Image runat="server" ID="imaBustTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" Visible="false" ToolTip="This tag signifies that the player is a bust."/>
		              <asp:Image runat="server" ID="imaInjuredTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" Visible="false" ToolTip="This tag signifies that the player is injured."/>
                </td>
              </tr>
            </ItemTemplate>
            <FooterTemplate>
              </tr>
            </FooterTemplate>
          </asp:Repeater>
        </table>
      </td>
      <td>
	    <table>
		    <tr>
		      <td colspan="2">
			          <div class="logo">
			            <asp:HyperLink runat="server" ID="hlHomePage" NavigateUrl="~/"  ToolTip="Click to navigate back to the Cheat Sheet War Room home page.">
			              <asp:Image runat="server" ID="imaLogo" ImageUrl="~/Images/Layout/printlogo.gif" AlternateText="Cheat Sheet War Room Logo." />
			            </asp:HyperLink>
			          </div>
			    </td>
		    </tr>
        <tr class="title">
          <th colspan="2" class="rosterHeader"><h1>Free, Printable Fantasy Football Cheat Sheet without Roster</h1></th>    
        </tr>
	    <%--Legend--%>
	    <tr>
	      <td colspan="2">
	        <div class="legend">
		        <div class="header">
		          Legend
		        </div>
		        <p>
		          <span class="player">Player:</span>
		          Rank 
              <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" />
		          Player Name
		          <span class="team">(Team)</span>
		          <span class="byeWeek">[Bye]</span>
		              
		          <span class="tags">Tags:</span>
              <asp:Image runat="server" ID="imaSleeperTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" CssClass="imagePad"/> Sleeper
  		        <asp:Image runat="server" ID="imaBustTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" CssClass="imagePad"/>  Bust
  		        <asp:Image runat="server" ID="imaInjuredTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" CssClass="imagePad"/>  Injured		            
		        </p>
		      </div>
		    </td>
	   </tr>
		  <!-- top two -->
		  <tr>
		    <td>
              <table class="position">


          <tr><th colspan="3">Quarterbacks</th></tr>
        
          <asp:Repeater runat="server" ID="repQuarterbacks" OnItemDataBound="repPosition_ItemDataBound">
            <ItemTemplate>
              <tr>
                <%--Rank--%>
                <td class="numbers">
                  <asp:Label runat="server" ID="labRank" />
                </td>
                <%--CheckBox--%>
                <td style="width:1%;">
                  <asp:Image runat="server" ID="imaCheckBox" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" AlternateText="Checkbox for Fantasy Player." />
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
		              <asp:Image runat="server" ID="imaSleeperTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" Visible="false" ToolTip="This tag signifies that the player is a sleeper."/>
		              <asp:Image runat="server" ID="imaBustTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" Visible="false" ToolTip="This tag signifies that the player is a bust."/>
		              <asp:Image runat="server" ID="imaInjuredTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" Visible="false" ToolTip="This tag signifies that the player is injured."/>
                </td>
              </tr>
            </ItemTemplate>
            <FooterTemplate>
              </tr>
            </FooterTemplate>
          </asp:Repeater>
        </table>	
			</td>
			<td>
      <!-- Wide Receivers -->
        <table class="position">
          <tr>
            <th colspan="3">Tight Ends</th>
          </tr>
          <asp:Repeater runat="server" ID="repTightEnds" OnItemDataBound="repPosition_ItemDataBound">
            <ItemTemplate>
              <tr>
                <%--Rank--%>
                <td class="numbers">
                  <asp:Label runat="server" ID="labRank" />
                </td>
                <%--CheckBox--%>
                <td style="width:1%;">
                <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" />
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
  		            <asp:Image runat="server" ID="imaSleeperTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" Visible="false" ToolTip="This tag signifies that the player is a sleeper."/>
  		            <asp:Image runat="server" ID="imaBustTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" Visible="false" ToolTip="This tag signifies that the player is a bust."/>
  		            <asp:Image runat="server" ID="imaInjuredTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" Visible="false" ToolTip="This tag signifies that the player is injured."/>
                </td>
              </tr>
            </ItemTemplate>
            <FooterTemplate>
              </tr>
            </FooterTemplate>
          </asp:Repeater>
              </table>
			</td>
		  </tr>
		  <!-- bottom two -->
		  <tr>
		    <td>
          <!-- Wide Receivers -->
          <table class="position">
          <tr>
            <th colspan="3">Kickers</th>
          </tr>
          <asp:Repeater runat="server" ID="repKickers" OnItemDataBound="repPosition_ItemDataBound">
            <ItemTemplate>
              <tr>
                <%--Rank--%>
                <td class="numbers">
                  <asp:Label runat="server" ID="labRank" />
                </td>
                <%--CheckBox--%>
                <td style="width:1%;">
                  <asp:Image runat="server" ID="imaCheckBox" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" AlternateText="Checkbox for Fantasy Player." />
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
  		            <asp:Image runat="server" ID="imaSleeperTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" Visible="false" ToolTip="This tag signifies that the player is a sleeper."/>
  		            <asp:Image runat="server" ID="imaBustTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" Visible="false" ToolTip="This tag signifies that the player is a bust."/>
  		            <asp:Image runat="server" ID="imaInjuredTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" Visible="false" ToolTip="This tag signifies that the player is injured."/>
                </td>
              </tr>
            </ItemTemplate>
            <FooterTemplate>
              </tr>
            </FooterTemplate>
          </asp:Repeater>
              </table>
			</td>
			<td>
              <!-- Wide Receivers -->
              <table class="position">
          <tr>
            <th colspan="3">Defenses</th>
          </tr>
          <asp:Repeater runat="server" ID="repDefenses" OnItemDataBound="repPosition_ItemDataBound">
            <ItemTemplate>
              <tr>
                <%--Rank--%>
                <td class="numbers">
                  <asp:Label runat="server" ID="labRank" />
                </td>
                <%--CheckBox--%>
                <td style="width:1%;">
                  <asp:Image runat="server" ID="imaCheckBox" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" AlternateText="Checkbox for Fantasy Player." />
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
  		            <asp:Image runat="server" ID="imaSleeperTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" Visible="false" ToolTip="This tag signifies that the player is a sleeper."/>
  		            <asp:Image runat="server" ID="imaBustTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" Visible="false" ToolTip="This tag signifies that the player is a bust."/>
  		            <asp:Image runat="server" ID="imaInjuredTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" Visible="false" ToolTip="This tag signifies that the player is injured."/>
                </td>
              </tr>
            </ItemTemplate>
            <FooterTemplate>
              </tr>
            </FooterTemplate>
          </asp:Repeater>
              </table>
			</td>
		  </tr>
		</table>
	  </td>
	  <td>
        <!-- Quarterbacks -->
        <table class="position">
          <tr><th colspan="3">Wide Receivers</th></tr>
        
          <asp:Repeater runat="server" ID="repWideReceivers" OnItemDataBound="repPosition_ItemDataBound">
            <ItemTemplate>
              <tr>
                <%--Rank--%>
                <td class="numbers">
                  <asp:Label runat="server" ID="labRank" />
                </td>
                <%--CheckBox--%>
                <td style="width:1%;">
                  <asp:Image runat="server" ID="imaCheckBox" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" AlternateText="Checkbox for Fantasy Player." />
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
		              <asp:Image runat="server" ID="imaSleeperTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" Visible="false" ToolTip="This tag signifies that the player is a sleeper."/>
		              <asp:Image runat="server" ID="imaBustTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" Visible="false" ToolTip="This tag signifies that the player is a bust."/>
		              <asp:Image runat="server" ID="imaInjuredTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" Visible="false" ToolTip="This tag signifies that the player is injured."/>
                </td>
              </tr>
            </ItemTemplate>
            <FooterTemplate>
              </tr>
            </FooterTemplate>
          </asp:Repeater>
        </table>
	  </td>
  </tr>
</table>

</div>

<div class="fooSecondPage">
  <p>
    This <strong>printable fantasy football cheat sheet</strong> is provided to you free of charge from cheatsheetwarroom.com.  Our goal is to provide fantasy football 
    enthusiasts with a simple and intuitive way to create fantasy cheat sheets.
  </p>
  <p>
    Our interactive, web-based fantasy football cheat sheets
    integrate supplemental NFL player rankings, 
    configurable tags (sleeper, bust, and injured), statistics, and even an editable player note.
  </p>
  <p>
    If you're looking for other ways to spice-up your <asp:Literal runat="server" id="litDraftYear"></asp:Literal> draft, we have some great suggestions when it comes to picking out a  
    <a href="https://www.cheatsheetwarroom.com/blog/fantasy-football/prizes/championship-belts" target="_blank">fantasy football championship belt</a>, a
    <a href="https://www.cheatsheetwarroom.com/blog/fantasy-football/draft/boards" target="_blank">fantasy football draft board</a>, or a 
    <a href="https://www.cheatsheetwarroom.com/blog/fantasy-football/prizes/championship-rings" target="_blank">fantasy football championship ring</a>.
  </p>
  <p>Also, since you're already in the mood, why not go ahead and watch any and all preseason games live at your draft?.  I wrote an 
    <a href="https://www.cheatsheetwarroom.com/blog/football/nfl-game-pass-review" target="_blank">NFL Game Pass review</a> 
    with all of the details.</p>
</div>
  

</form>
</body>
</html>
