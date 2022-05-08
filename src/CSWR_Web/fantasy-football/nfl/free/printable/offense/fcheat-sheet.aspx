<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" Theme="" 
  CodeFile="fcheat-sheet.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.RandomCheatSheet" %>
<%@ Register Src="~/usercontrols/GoogleAnalytics.ascx" TagName="GoogleAnalytics" TagPrefix="cswr" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
  <link href="https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/printable/offense/cheat-sheet-without-roster.aspx" rel="canonical" />
  <meta http-equiv="content-language" content="en=us">
  <meta name="robots" content="NOINDEX,NOFOLLOW" />
  <link rel="stylesheet" type="text/css" href="~/styles/print.css" media="print" />
  <cswr:GoogleAnalytics runat="server" />
</head>
<body style="background:White;">

<form id="form1" runat="server">

<div class="fooPrint1SheetWithRoster">

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
			            <asp:Image runat="server" ID="imaLogo" ImageUrl="~/Images/Layout/fprintlogo.gif" AlternateText="Cheat Sheet War Room Logo." />
			          </asp:HyperLink>
			        </div>
			  </td>
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

</form>
</body>
</html>
