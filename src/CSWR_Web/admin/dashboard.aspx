<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" 
Inherits="BP.CheatSheetWarRoom.UI.Admin.Users.Dashboard" 
  Title="Administration Dashboard" %>
<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>  
  
<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

<asp:ScriptManager runat="server" />

<div class="adminSummaryPage">

  <h1>Administration Dashboard</h1>

  <!-- Left Side -->
  <div class="leftSide">

    <%--Sheet Statistics--%>
    <div class="sheets">
      <h2>Sheet Summary</h2>

      <table>
        <tr>
          
          <td>

            <%--Global Sheets--%>
            <table>
              <tr>
                <th colspan="2">Global Sheets</th>
              </tr>
              <tr>
                <td><strong>Total Sheets</strong></td>
                <td><asp:Literal runat="server" ID="litTotalSheets"></asp:Literal></td>
              </tr>
              <tr>
                <td><strong>Total Member Sheets</strong></td>
                <td><asp:Literal runat="server" ID="litTotalMemberSheets"></asp:Literal></td>
              </tr>
              <tr>
                <td><strong>Total Visitor Sheets</strong></td>
                <td><asp:Literal runat="server" ID="litTotalVisitorSheets"></asp:Literal></td>
              </tr>
            </table>

          </td>
          <td>

            <%--Football Sheets--%>
            <table>
              <tr>
                <th colspan="2" class="football">Football Sheets</th>
              </tr>
              <tr>
                <td><strong>Total Sheets</strong></td>
                <td><asp:Literal runat="server" ID="litTotalFootballSheets"></asp:Literal></td>
              </tr>
              <tr>
                <td><strong>Total Member Sheets</strong></td>
                <td><asp:Literal runat="server" ID="litTotalFootballMemberSheets"></asp:Literal></td>
              </tr>
              <tr>
                <td><strong>Total Visitor Sheets</strong></td>
                <td><asp:Literal runat="server" ID="litTotalFootballVisitorSheets"></asp:Literal></td>
              </tr>
            </table>
          
          </td>
          <td>

            <%--Racing Sheets--%>
            <table>
              <tr>
                <th colspan="2" class="racing">Racing Sheets</th>
              </tr>
              <tr>
                <td><strong>Total Sheets</strong></td>
                <td><asp:Literal runat="server" ID="litTotalRacingSheets"></asp:Literal></td>
              </tr>
              <tr>
                <td><strong>Total Member Sheets</strong></td>
                <td><asp:Literal runat="server" ID="litTotalRacingMemberSheets"></asp:Literal></td>
              </tr>
              <tr>
                <td><strong>Total Visitor Sheets</strong></td>
                <td><asp:Literal runat="server" ID="litTotalRacingVisitorSheets"></asp:Literal></td>
              </tr>
            </table>
          
          </td>
        </tr>
      </table>


    </div>  <!-- close sheets -->

    <br />

    <%--Miscellaneous Operations--%>
      <div class="misc">
        <h2>Miscellaneous</h2>

        <%--Update Animation--%>
        <asp:UpdateProgress ID="uprSheetPlayers" runat="server" AssociatedUpdatePanelID="upCorruption">
          <ProgressTemplate>
            <div class="updateProgressBox" style="left:290px;margin-top:10px;">
              <asp:Image runat="server" ImageUrl="~/Images/Animations/rotating_arrow.gif" CssClass="floatLeft"/>
              <span style="margin-top:10px;">Processing....</span> 
            </div>
          </ProgressTemplate>
        </asp:UpdateProgress>


        <asp:UpdatePanel runat="server" ID="upCorruption">
          <ContentTemplate>
            <table>
              <tr>
                <th colspan="3">Correct Sheet Corruption</th>
              </tr>
              <tr>
                <td><strong>Football</strong></td>
                <td>
                  <asp:Label runat="server" ID="labFootballCorruptionCount" />
                </td>
                <td>
                  <asp:Button runat="server" ID="butCorrectFootballCorruption" Text="Fix" 
                    Visible="false" onclick="butCorrectFootballCorruption_Click" />
                </td>
              </tr>
              <tr>
                <td><strong>Racing</strong></td>
                <td>
                  <asp:Label runat="server" ID="labRacingCorruptionCount" />
                </td>
                <td>
                  <asp:Button runat="server" ID="butCorrectRacingCorruption" Text="Fix" Visible="false" />
                </td>
              </tr>
              <tr>
                <td colspan="3">
                  <asp:Button runat="server" ID="butCheckCorruption" Text="Check Corruption" 
                    onclick="butCheckCorruption_Click"/>
                </td>
              </tr>
            </table>
          </ContentTemplate>
        </asp:UpdatePanel>

      <br />

      <asp:Button runat="server" ID="butCalcRacingADP" Text="Calculate Racing ADP" 
        onclick="butCalcRacingADP_Click" />
  
      <br /><br />
  
      <asp:Button runat="server" ID="butClearCache" Text="Clear All Cache" 
        onclick="butClearCache_Click" />

      <br /><br />

      <asp:Button runat="server" ID="butClearSession" Text="Clear All Session Variables" 
        onclick="butClearSessions_Click" />


      </div>  <!-- close misc -->

  </div>  <!-- close leftSide -->

<%--  <!-- Right Side -->
  <div class="rightSide">

    <h2>User Stats</h2>

    <div class="stats">
      <table>
        <tr>
          <td>
            <strong>Total registered users:</strong> <asp:Literal runat="server" ID="litTotalUsers"></asp:Literal>
          </td>
          <td>
            <strong>Percentage Subscribed:</strong> <asp:Literal runat="server" ID="litPercentageSubscribed"></asp:Literal>
          </td>
        </tr>
        <tr>
          <td>
            <strong>Registrations Today:</strong> <asp:Literal runat="server" ID="litRegistrationsToday"></asp:Literal>
          </td>
          <td>
            <strong>Percentage Returned</strong> <asp:Literal runat="server" ID="litPercentageReturned"></asp:Literal>
          </td>
        </tr>
        <tr>
          <td>
            <strong>Registrations Yesterday:</strong> <asp:Literal runat="server" ID="litRegistrationsYesterday"></asp:Literal>
          </td>
          <td></td>
        </tr>
        <tr>
          <td>
            <strong>Users online now:</strong> <asp:Literal runat="server" ID="litOnlineUsers"></asp:Literal>
          </td>
          <td></td>
        </tr>
      </table>

    </div>

    <asp:GridView runat="server" ID="gvOnlineUsers" AutoGenerateColumns="false" CssClass="standardGrid" SkinID="Professional">
      <Columns>
        <asp:BoundField HeaderText="UserName" DataField="UserName" />
        <asp:BoundField HeaderText="Email" DataField="Email" />
        <asp:BoundField HeaderText="CreationDate" DataField="CreationDate" />
      </Columns>    
    </asp:GridView>

  </div> <!-- close rightSide -->--%>

  <div style="clear:both;"/>


</div>





</asp:Content>

