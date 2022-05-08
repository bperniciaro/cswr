<%@ Page Language="C#" AutoEventWireup="true" Theme="" CodeFile="cheatsheetwithroster.aspx.cs" 
  EnableViewState="false" Inherits="BP.CheatSheetWarRoom.UI.CheatSheetWithRosterMultiple" %>
<%@ Register Src="~/usercontrols/GoogleAnalytics.ascx" TagName="GoogleAnalytics" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/sports/football/cheatsheet/NoteSummary.ascx" TagName="NoteSummary" TagPrefix="cswr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="https://www.w3.org/1999/xhtml" >
<head runat="server">
  <title>Printable Cheat Sheet With Roster</title>
  <meta http-equiv="content-language" content="en=us">
  <cswr:GoogleAnalytics runat="server" />
  <link rel="stylesheet" type="text/css" href="~/styles/print.css" media="print" />
  <!-- this page is only for registered members, so it should not be indexed -->
  <meta name="robots" content="NOINDEX,NOFOLLOW" />
</head>
<body style="background:none;">

<form id="form1" runat="server">

<cswr:MessageBox runat="server" ID="mbStatus" MessageType="ERROR" />

<asp:Panel runat="server" ID="panCheatSheetContianer" CssClass="fooPrint1SheetWithRoster">

  <table class="topContainer">
    <tr>
      <td>
        <!-- Column 1 -->
        <table class="position">
          <asp:Repeater runat="server" ID="repColumn1" OnItemDataBound="repPosition_ItemDataBound">
            <ItemTemplate>
              <tr>
                <%--Rank--%>
                <td class="numbers">
                  <asp:Label runat="server" ID="labRank" />
                </td>
                <%--CheckBox--%>
                <td style="width:1%;">
                  <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" AlternateText="Checkbox to indicate player has been chosen." /> 
                </td>
                <%--Player, Bye, and Tags--%>
                <td>
                  <%--Name--%>
                  <asp:Literal runat="server" ID="litPlayerName" />
                  <%--Team Abbreviation--%>
                  <asp:Label runat="server" ID="labTeamPositionBye" CssClass="team"/>
 		              <%--Tags--%>
		              <asp:Image runat="server" ID="imaSleeperTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" Visible="false"/>
		              <asp:Image runat="server" ID="imaBustTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" Visible="false"/>
		              <asp:Image runat="server" ID="imaInjuredTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" Visible="false"/>
                </td>
              </tr>
            </ItemTemplate>
          </asp:Repeater>
        </table>
      </td>
      <td class="centerColumn">
	      <table class="centerTable">
		      <%--Logo--%>
		      <tr>
		        <td>
			        <div class="logo">
			          <asp:HyperLink runat="server" ID="hlHomePage" NavigateUrl="~/" ToolTip="Click to navigate back to the Cheat Sheet War Room home page.">
			            <asp:Image runat="server" ImageUrl="~/Images/Layout/printlogo.gif" AlternateText="Home Page Logo" />
			          </asp:HyperLink>			        
			        </div>
			      </td>
		      </tr>
          <%--Roster--%>
		      <tr>
		        <td>
              <table class="fooRosterForm">
                <tr>
                  <th colspan="2" class="rosterHeader">My Roster</th>    
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
		              Rank 
                  <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" AlternateText="Checkbox to indicate player has been chosen." /> 
		              Player Name
		              <span class="team">(Team|Position|Bye)</span>
		              
		              <span class="tags">Tags:</span>
                  <asp:Image runat="server" ID="imaSleeperTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" CssClass="imagePad"/> Sleeper
  		            <asp:Image runat="server" ID="imaBustTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" CssClass="imagePad"/>  Bust
  		            <asp:Image runat="server" ID="imaInjuredTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" CssClass="imagePad"/>  Injured		            
		            </p>
		          </div>
		        </td>
		      </tr>
		    </table>
		    <table>
		      <tr>
		        <td>
		          <%--Column 2--%>
              <table class="position">
                <asp:Repeater runat="server" ID="repColumn2" OnItemDataBound="repPosition_ItemDataBound">
                  <ItemTemplate>
                    <tr>
                      <%--Rank--%>
                      <td class="numbers">
                        <asp:Label runat="server" ID="labRank" />
                      </td>
                      <%--CheckBox--%>
                      <td style="width:1%;">
                        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" AlternateText="Checkbox to indicate player has been chosen." /> 
                      </td>
                      <%--Player, Bye, and Tags--%>
                      <td>
                        <%--Name--%>
                        <asp:Literal runat="server" ID="litPlayerName" />
                        <%--Team Abbreviation--%>
                        <asp:Label runat="server" ID="labTeamPositionBye" CssClass="team"/>
  		                  <%--Tags--%>
  		                  <asp:Image runat="server" ID="imaSleeperTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" Visible="false"/>
  		                  <asp:Image runat="server" ID="imaBustTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" Visible="false"/>
  		                  <asp:Image runat="server" ID="imaInjuredTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" Visible="false"/>
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
              <%--Column 3--%>
              <table class="position">
                <asp:Repeater runat="server" ID="repColumn3" OnItemDataBound="repPosition_ItemDataBound">
                  <ItemTemplate>
                    <tr>
                      <%--Rank--%>
                      <td class="numbers">
                        <asp:Label runat="server" ID="labRank" />
                      </td>
                      <%--CheckBox--%>
                      <td style="width:1%;">
                        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" AlternateText="Checkbox to indicate player has been chosen." /> 
                      </td>
                      <%--Player, Bye, and Tags--%>
                      <td>
                        <%--Name--%>
                        <asp:Literal runat="server" ID="litPlayerName" />
                        <%--Team Abbreviation--%>
                        <asp:Label runat="server" ID="labTeamPositionBye" CssClass="team"/>
  		                  <%--Tags--%>
  		                  <asp:Image runat="server" ID="imaSleeperTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" Visible="false"/>
  		                  <asp:Image runat="server" ID="imaBustTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" Visible="false"/>
  		                  <asp:Image runat="server" ID="imaInjuredTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" Visible="false"/>
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
        <!-- Column 4 -->
        <table class="position">
          <asp:Repeater runat="server" ID="repColumn4" OnItemDataBound="repPosition_ItemDataBound">
            <ItemTemplate>
              <tr>
                <%--Rank--%>
                <td class="numbers">
                  <asp:Label runat="server" ID="labRank" />
                </td>
                <%--CheckBox--%>
                <td style="width:1%;">
                  <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/checkboximage.gif" AlternateText="Checkbox to indicate player has been chosen." /> 
                </td>
                <%--Player, Bye, and Tags--%>
                <td>
                  <%--Name--%>
                  <asp:Literal runat="server" ID="litPlayerName" />
                  <%--Team Abbreviation--%>
                  <asp:Label runat="server" ID="labTeamPositionBye" CssClass="team"/>
 		              <%--Tags--%>
		              <asp:Image runat="server" ID="imaSleeperTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/sleepertag.gif" Visible="false"/>
		              <asp:Image runat="server" ID="imaBustTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/busttag.gif" Visible="false"/>
		              <asp:Image runat="server" ID="imaInjuredTag" ImageUrl="~/Images/Sports/Football/PrintableSheet/injuredtag.gif" Visible="false"/>
                </td>
              </tr>
            </ItemTemplate>
          </asp:Repeater>
        </table>
	    </td>
    </tr>
  </table>

</asp:Panel>  <%-- close panCheatSheetContianer--%>

<asp:Panel runat="server" ID="panSecondPage" class="fooSecondPage">

  <cswr:NoteSummary runat="server" ID="nsNoteSummary" />

</asp:Panel>  <!-- close secondPage -->




</form>
</body>
</html>
