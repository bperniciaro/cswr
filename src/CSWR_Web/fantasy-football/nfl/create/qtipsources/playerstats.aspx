<%@ Page Language="C#" AutoEventWireup="true" Theme="" CodeFile="playerstats.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.PlayerStats" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
  <title></title>
  <meta name="robots" content="NOINDEX,NOFOLLOW" />
</head>
<body>
<form id="form1" runat="server">

<%-- QTip Stat Popu --%>
<div class="qtipFOOStatPopup">
  <table class="outerTable">
    <tr>
      <th colspan="2" class="subTitle">
        Summary Statistics
      </th>
    </tr>
    <!-- Stat Summaries -->
    <tr class="summaryStatRow">
      <!-- Total Fantasy Points -->
      <td class="summaryStatCell">
        <div class="rankContainer">
          <%--Total Fantasy Points Rank--%>
          <span class="rank">
            #<asp:Label runat="server" ID="labTFPRank" />
          </span>
           Overall
          <%--Total Fantasy Points--%>
          <%--<span class="stat">
          </span>--%>
        </div>
        <div class="categoryContainer">
          Total Fantasy Points
          (<asp:Label runat="server" ID="labTFP"/>)
        </div>
      </td>
      <!-- Fantasy Points Per Game -->
      <td class="summaryStatCell">
        <div class="rankContainer">
          <%--Fantasy Points Per Game Rank--%>
          <span class="rank">
            #<asp:Label runat="server" ID="labFPPGRank" />
          </span> Overall
          <%--Fantasy Points Per Game--%>
          <%--<span class="stat">
          </span>--%>
        </div>
        <div class="categoryContainer">
          Fantasy Points Per Game
          (<asp:Label runat="server" ID="labFPPG" />)
        </div>
      </td>
    </tr>
    <tr>
      <th colspan="2" class="subTitle">
        Individual Statistics
      </th>
    </tr>
    <!-- Stats -->
    <tr>
      <td colspan="2" class="individual">
        
        <!-- -------------->
        <!-- Stat Tables -->
        <!-- -------------->

        <!-- QB Table -->
        <asp:Panel runat="server" ID="panQBTable" Visible="false">
          <table class="statTable">
            <tr>
              <th>
              </th>
              <th colspan="3" class="alternateColor">
                Passing
              </th>
              <th colspan="3">
                Rushing
              </th>
              <th class="alternateColor"></th>
            </tr>
            <tr>
              <th>
                GAM
              </th>
              <th class="alternateColor">
                Yd
              </th>
              <th class="alternateColor">
                TD
              </th>
              <th class="alternateColor">
                INT
              </th>
              <th class="alternateColor">
                Car
              </th>
              <th>
                Yd
              </th>
              <th>
                TD
              </th>
              <th class="alternateColor">
                Fum
              </th>
            </tr>
            <tr>
              <td>
                <asp:Label runat="server" ID="labQB_GAM" />
              </td>
              <td>
                <asp:Label runat="server" ID="labQB_PAYD" />
              </td>
              <td>
                <asp:Label runat="server" ID="labQB_PATD" />
              </td>
              <td>
                <asp:Label runat="server" ID="labQB_INT" />
              </td>
              <td>
                <asp:Label runat="server" ID="labQB_RUCA" />
              </td>
              <td>
                <asp:Label runat="server" ID="labQB_RUYD" />
              </td>
              <td>
                <asp:Label runat="server" ID="labQB_RUTD" />
              </td>
              <td>
                <asp:Label runat="server" ID="labQB_FUM" />
              </td>
            </tr>
          </table>
        </asp:Panel>  <!-- close panWRTable -->


        <!-- RB Table -->
        <asp:Panel runat="server" ID="panRBTable" Visible="false">
          <table class="statTable">
            <tr>
              <th>
              </th>
              <th colspan="3" class="alternateColor">
                Rushing
              </th>
              <th colspan="4">
                Receiving
              </th>
              <th class="alternateColor"></th>
            </tr>
            <tr>
              <th>
                GAM
              </th>
              <th class="alternateColor">
                Car
              </th>
              <th class="alternateColor">
                Yd
              </th>
              <th class="alternateColor">
                TD
              </th>
              <th>
                Tgt
              </th>
              <th>
                Rec
              </th>
              <th>
                Yd
              </th>
              <th>
                TD
              </th>
              <th class="alternateColor">
                Fum
              </th>
            </tr>
            <tr>
              <td>
                <asp:Label runat="server" ID="labRB_GAM" />
              </td>
              <td>
                <asp:Label runat="server" ID="labRB_RUCA" />
              </td>
              <td>
                <asp:Label runat="server" ID="labRB_RUYD" />
              </td>
              <td>
                <asp:Label runat="server" ID="labRB_RUTD" />
              </td>
              <td>
                <asp:Label runat="server" ID="labRB_RETR" />
              </td>
              <td>
                <asp:Label runat="server" ID="labRB_RECP" />
              </td>
              <td>
                <asp:Label runat="server" ID="labRB_REYD" />
              </td>
              <td>
                <asp:Label runat="server" ID="labRB_RETD" />
              </td>
              <td>
                <asp:Label runat="server" ID="labRB_FUM" />
              </td>
            </tr>
          </table>
        </asp:Panel>  <!-- close panWRTable -->

        <!-- WR Table -->
        <asp:Panel runat="server" ID="panWRTable" Visible="false">
          <table class="statTable">
            <tr>
              <th>
              </th>
              <th colspan="4" class="alternateColor">
                Receiving
              </th>
              <th colspan="2">
                Rushing
              </th>
              <th class="alternateColor"></th>
            </tr>
            <tr>
              <th>
                GAM
              </th>
              <th class="alternateColor">
                Tgt
              </th>
              <th class="alternateColor">
                Rec
              </th>
              <th class="alternateColor">
                Yd
              </th>
              <th class="alternateColor">
                TD
              </th>
              <th>
                Yd
              </th>
              <th>
                TD
              </th>
              <th class="alternateColor">
                Fum
              </th>
            </tr>
            <tr>
              <td>
                <asp:Label runat="server" ID="labWR_GAM" />
              </td>
              <td>
                <asp:Label runat="server" ID="labWR_RETR" />
              </td>
              <td>
                <asp:Label runat="server" ID="labWR_RECP" />
              </td>
              <td>
                <asp:Label runat="server" ID="labWR_REYD" />
              </td>
              <td>
                <asp:Label runat="server" ID="labWR_RETD" />
              </td>
              <td>
                <asp:Label runat="server" ID="labWR_RUYD" />
              </td>
              <td>
                <asp:Label runat="server" ID="labWR_RUTD" />
              </td>
              <td>
                <asp:Label runat="server" ID="labWR_FUM" />
              </td>
            </tr>
          </table>
        </asp:Panel>  <!-- close panWRTable -->

        <!-- TE Table -->
        <asp:Panel runat="server" ID="panTETable" Visible="false">
          <table class="statTable">
            <tr>
              <th>
              </th>
              <th colspan="4" class="alternateColor">
                Receiving
              </th>
              <th colspan="2">
                Rushing
              </th>
              <th class="alternateColor"></th>
            </tr>
            <tr>
              <th>
                GAM
              </th>
              <th class="alternateColor">
                Tgt
              </th>
              <th class="alternateColor">
                Rec
              </th>
              <th class="alternateColor">
                Yd
              </th>
              <th class="alternateColor">
                TD
              </th>
              <th>
                Yd
              </th>
              <th>
                TD
              </th>
              <th class="alternateColor">
                Fum
              </th>
            </tr>
            <tr>
              <td>
                <asp:Label runat="server" ID="labTE_GAM" />
              </td>
              <td>
                <asp:Label runat="server" ID="labTE_RETR" />
              </td>
              <td>
                <asp:Label runat="server" ID="labTE_RECP" />
              </td>
              <td>
                <asp:Label runat="server" ID="labTE_REYD" />
              </td>
              <td>
                <asp:Label runat="server" ID="labTE_RETD" />
              </td>
              <td>
                <asp:Label runat="server" ID="labTE_RUYD" />
              </td>
              <td>
                <asp:Label runat="server" ID="labTE_RUTD" />
              </td>
              <td>
                <asp:Label runat="server" ID="labTE_FUM" />
              </td>
            </tr>
          </table>
        </asp:Panel>  <!-- close panTETable -->

        <!-- K Table -->
        <asp:Panel runat="server" ID="panKTable" Visible="false">
          <table class="statTable">
            <tr>
              <th>
              </th>
              <th colspan="2" class="alternateColor">
                Field Goals
              </th>
              <th colspan="2">
                Xtra Points
              </th>
            </tr>
            <tr>
              <th>
                GAM
              </th>
              <th class="alternateColor">
                MAFG
              </th>
              <th class="alternateColor">
                MIFG
              </th>
              <th>
                MAXP
              </th>
              <th>
                MIXP
              </th>
            </tr>
            <tr>
              <td>
                <asp:Label runat="server" ID="labK_GAM" />
              </td>
              <td>
                <asp:Label runat="server" ID="labK_MAFG" />
              </td>
              <td>
                <asp:Label runat="server" ID="labK_MIFG" />
              </td>
              <td>
                <asp:Label runat="server" ID="labK_MAXP" />
              </td>
              <td>
                <asp:Label runat="server" ID="labK_MIXP" />
              </td>
            </tr>
          </table>
        </asp:Panel>  <!-- close panKTable -->


        <!-- DF Table -->
        <asp:Panel runat="server" ID="panDFTable" Visible="false">
          <table class="statTable">
            <tr>
              <th>
              </th>
              <th colspan="2" class="alternateColor">
                Turnovers
              </th>
              <th colspan="2">
                Scoring
              </th>
            </tr>
            <tr>
             <%-- <th>
                GAM
              </th>--%>
              <th>
                SACK
              </th>
              <th>
                INT
              </th>
              <th>
                FREC
              </th>
              <th>
                DTD
              </th>
              <th>
                PA
              </th>
            </tr>
            <tr>
              <%--<td>
                <asp:Label runat="server" ID="labDF_GAM" />
              </td>--%>
              <td>
                <asp:Label runat="server" ID="labDF_SACK" />
              </td>
              <td>
                <asp:Label runat="server" ID="labDF_INT" />
              </td>
              <td>
                <asp:Label runat="server" ID="labDF_FREC" />
              </td>
              <td>
                <asp:Label runat="server" ID="labDF_DTD" />
              </td>
              <td>
                <asp:Label runat="server" ID="labDF_PA" />
              </td>
            </tr>
          </table>
        </asp:Panel>  <!-- close panDFTable -->

      </td>
    </tr>

  </table>
  

</div>







</form>
</body>
</html>
