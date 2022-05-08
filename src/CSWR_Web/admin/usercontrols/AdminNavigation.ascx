<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminNavigation.ascx.cs" Inherits="BP.CheatSheetWarRoom.UI.UserControls.AdminNavigation" %>

<div class="adminNavigation">
  <div class="leftSide">


    <%--Main Admin--%>
    <asp:Panel runat="server" id="panMainMenu" CssClass="adminNavBar" style="float:left;">
      <ul>
        <li>
          <asp:HyperLink runat="server" ID="hl_Main_Admin" Text="Admin" NavigateUrl="~/admin/dashboard.aspx" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Main_ManageUsers" NavigateUrl="~/Admin/Users/ManageUsers.aspx" Text="Users" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Main_Football" NavigateUrl="~/admin/sports/football/players/manageplayers.aspx"  Text="Football"/>
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Main_Racing" NavigateUrl="~/admin/sports/racing/sheetsettings.aspx"  Text="Racing"/>
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Main_Health" NavigateUrl="~/Admin/Health/ManageExceptions.aspx" Text="Health" />
        </li>
      </ul>
      <br style="clear: left" />
    </asp:Panel>


    <%--Admin--%>
    <asp:Panel runat="server" ID="panAdminMenu" CssClass="adminNavBar" Visible="false">
      <ul>
        <li>
          <asp:HyperLink runat="server" ID="hl_Admin_Dashboard" NavigateUrl="~/admin/dashboard.aspx" Text="Summary" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Admin_SummaryStats" NavigateUrl="~/admin/summarystats.aspx" Text="Stats" />
        </li>
      </ul>
      <br style="clear: left" />
    </asp:Panel>  


    <%--Users--%>
    <asp:Panel runat="server" ID="panUsersMenu" CssClass="adminNavBar" Visible="false">
      <ul>
        <li>
          <asp:HyperLink runat="server" ID="hl_Users_ManageUsers" NavigateUrl="~/admin/users/manageusers.aspx" Text="Manage" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Users_UserSummary" NavigateUrl="~/admin/users/usersummary.aspx" Text="Summary" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Users_PowerUsers" NavigateUrl="~/admin/users/powerusers.aspx" Text="Power" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Users_Upgrade" NavigateUrl="~/admin/users/upgrade/upgradedUsers.aspx" Text="Upgrade" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Users_Email" NavigateUrl="~/admin/users/email/makeemaillists.aspx" Text="Email" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Users_GenerateNewPassword" NavigateUrl="~/admin/users/generateuserpassword.aspx" Text="Password" />
        </li>

      </ul>
      <br style="clear: left" />
    </asp:Panel>  


    <%-- Users Upgrade Sub-Navigation--%>
    <asp:Panel runat="server" ID="panUsersUpgradeSubMenu" CssClass="adminNavBar" Visible="false">
      <ul>
        <li>
          <asp:HyperLink runat="server" ID="hl_Users_Upgrade_UpgradedUsers" NavigateUrl="~/admin/users/upgrade/upgradedusers.aspx" Text="Upgraded Users" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Users_Upgrade_ManageVouchers" NavigateUrl="~/admin/users/upgrade/managevouchers.aspx" Text="Manage Vouchers" />
        </li>
         <li>
          <asp:HyperLink runat="server" ID="hl_Users_Upgrade_VoucherGenerator" NavigateUrl="~/admin/users/upgrade/vouchergenerator.aspx" Text="Voucher Generator" />
        </li>
         <li>
          <asp:HyperLink runat="server" ID="hl_Users_Upgrade_TestPayPal" NavigateUrl="~/admin/users/upgrade/testpaypal.aspx" Text="Test Paypal" />
        </li>
      </ul>
      <br style="clear: left" />
    </asp:Panel>  




    <%-- Users Email Sub-Navigation--%>
    <asp:Panel runat="server" ID="panUsersEmailSubMenu" CssClass="adminNavBar" Visible="false">
      <ul>
        <li>
          <asp:HyperLink runat="server" ID="hl_Users_Email_MakeEmailLists" NavigateUrl="~/admin/users/email/makeemaillists.aspx" Text="Make Email Lists" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Users_Email_ImportInvalidEmails" NavigateUrl="~/admin/users/email/importinvalidemails.aspx" Text="Import Invalid Emails" />
        </li>
      </ul>
      <br style="clear: left" />
    </asp:Panel>  




    <%--Football Players--%>
    <asp:Panel runat="server" ID="panFootballMenu" CssClass="adminNavBar" Visible="false">
      <ul>
        <li>
          <asp:HyperLink runat="server" ID="hl_Football_Players_ManagePlayers" NavigateUrl="~/admin/sports/football/players/manageplayers.aspx" Text="Players" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Football_Players_ManagePlayerStatuses" NavigateUrl="~/admin/sports/football/players/manageplayerstatuses.aspx" Text="Statuses" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Football_Players_SeasonStats" NavigateUrl="~/admin/sports/football/players/stats/manageplayerseasonstats.aspx" Text="Stats" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Football_Players_Supplementals" NavigateUrl="~/admin/sports/football/supplementals/managesupplementalsheets.aspx" Text="Supplementals" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Football_Players_UserSheets" NavigateUrl="~/admin/sports/football/usersheets/archiveusersheets.aspx" Text="User Sheets" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Football_Players_ADP" NavigateUrl="~/admin/sports/football/adp/adpmonitor.aspx" Text="ADP" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Football_Players_Settings" NavigateUrl="~/admin/sports/football/sheetsettings.aspx" Text="Settings" />
        </li>
      </ul>
      <br style="clear: left" />
    </asp:Panel>  



    <%--Football Players Sub-Navigation--%>
    <asp:Panel runat="server" ID="panFootballPlayersSubMenu" CssClass="adminNavBar" Visible="false">
      <ul>
        <li>
          <asp:HyperLink runat="server" ID="hl_Football_Players_Manage" NavigateUrl="~/admin/sports/football/players/manageplayers.aspx" Text="Manage" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Football_Players_LikelyRetired" NavigateUrl="~/admin/sports/football/players/likelyretired.aspx" Text="Likely Retired" />
        </li>
      </ul>
      <br style="clear: left" />
    </asp:Panel>  



    <%--Football Stats Sub-Navigation--%>
    <asp:Panel runat="server" ID="panFootballStatsSubMenu" CssClass="adminNavBar" Visible="false">
      <ul>
        <li>
          <asp:HyperLink runat="server" ID="hl_Football_Stats_Manage" NavigateUrl="~/admin/sports/football/players/stats/manageplayerseasonstats.aspx" Text="Manage" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Football_Stats_MapPlayerID" NavigateUrl="~/admin/sports/football/players/stats/import/mapplayerids.aspx" Text="MapIDs" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Football_Stats_Weekly_Import" NavigateUrl="~/admin/sports/football/players/stats/import/importweeklystats.aspx" Text="Import Weekly" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Football_Stats_Season_Import" NavigateUrl="~/admin/sports/football/players/stats/import/importseasonstats.aspx" Text="Import Seasonal" />
        </li>
      </ul>
      <br style="clear: left" />
    </asp:Panel>  


    <%--Football Supplementals Sub-Navigation--%>
    <asp:Panel runat="server" ID="panFootballSupplementalsSubMenu" CssClass="adminNavBar" Visible="false">
      <ul>
        <li>
          <asp:HyperLink runat="server" ID="hl_Football_Supplementals_Sheets" NavigateUrl="~/admin/sports/football/supplementals/managesupplementalsheets.aspx" Text="Sheets" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Football_Supplementals_Sources" NavigateUrl="~/admin/sports/football/supplementals/managesupplementalsources.aspx" Text="Sources" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Football_Supplementals_Scrape" NavigateUrl="~/admin/sports/football/supplementals/scraperankings.aspx" Text="Scrape" />
        </li>
      </ul>
      <br style="clear: left" />
    </asp:Panel>  


    <%--Football User Sheets Sub-Navigation--%>
    <asp:Panel runat="server" ID="panFootballUserSheetsSubMenu" CssClass="adminNavBar" Visible="false">
      <ul>
        <li>
          <asp:HyperLink runat="server" ID="hl_Football_UserSheets_ArchiveUserSheets" NavigateUrl="~/admin/sports/football/usersheets/archiveusersheets.aspx" Text="Archive" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Football_UserSheets_GradeUsers" NavigateUrl="~/admin/sports/football/usersheets/gradeusersheets.aspx" Text="Grade Users" />
        </li>
      </ul>
      <br style="clear: left" />
    </asp:Panel>  





    <%--Racing Drivers--%>
    <asp:Panel runat="server" ID="panRacingMenu" CssClass="adminNavBar" Visible="false">
      <ul>
        <li>
          <asp:HyperLink runat="server" ID="hl_Racing_Drivers_ManageDrivers" NavigateUrl="~/admin/sports/racing/drivers/managedrivers.aspx" Text="Drivers" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Racing_Drivers_SeasonStats" NavigateUrl="~/admin/sports/racing/drivers/stats/managedriverseasonstats.aspx" Text="Stats" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Racing_Drivers_Supplementals" NavigateUrl="~/admin/sports/racing/supplementals/managesupplementalsheets.aspx" Text="Supplementals" />
        </li>
      </ul>
      <br style="clear: left" />
    </asp:Panel>  

    <%--Racing Stats--%>
    <asp:Panel runat="server" ID="panRacingStatsMenu" CssClass="adminNavBar" Visible="false">
      <ul>
        <li>
          <asp:HyperLink runat="server" ID="hl_Racing_Stats_Yearly" NavigateUrl="~/admin/sports/racing/drivers/stats/managedriverseasonstats.aspx" Text="Season" />
        </li>
        <%--<li>
          <asp:HyperLink runat="server" ID="hl_Racing_Stats_Import" NavigateUrl="~/admin/sports/racing/drivers/stats/import/loadseasonstats.aspx" Text="Import" />
        </li>--%>
      </ul>
      <br style="clear: left" />
    </asp:Panel>  

    <%--Racing Supplementals--%>
    <asp:Panel runat="server" ID="panRacingSupplementalsMenu" CssClass="adminNavBar" Visible="false">
      <ul>
        <li>
          <asp:HyperLink runat="server" ID="hl_Racing_Supplementals_Sheets" NavigateUrl="~/admin/sports/racing/supplementals/manageSupplementalSheets.aspx" Text="Sheets" />
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hl_Racing_Supplementals_Reviews" NavigateUrl="~/admin/sports/racing/supplementals/managesupplementalreviews.aspx" Text="Reviews" />
        </li>
      </ul>
      <br style="clear: left" />
    </asp:Panel>  


  </div>
  <div class="rightSide">

    <div class="adminNavBar">
      <ul>
        <li>
          <asp:HyperLink runat="server" Text="Home" NavigateUrl="~/Default.aspx"/>
        </li>
        <li>
          <asp:HyperLink runat="server" Text="Foo Sheet" NavigateUrl="~/fantasy-football/nfl/create/custom-sheet.aspx" />
        </li>
        <li>
          <asp:HyperLink runat="server" Text="Rac Sheet" NavigateUrl="~/fantasy-racing/nascar/create/custom-sheet.aspx" />
        </li>
        <li>
          <asp:Button runat="server" ID="butClearCache" Text="Clear All Cache" onclick="butClearCache_Click" />
        </li>
      </ul>
    </div>

    <div class="adminNavBar">
      <ul>
        <li>
          <asp:HyperLink runat="server" ID="hlQBSheet" Text="QB"/>
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hlRBSheet" Text="RB"/>
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hlWRSheet" Text="WR"/>
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hlTESheet" Text="TE"/>
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hlKSheet" Text="K"/>
        </li>
        <li>
          <asp:HyperLink runat="server" ID="hlDFSheet" Text="DF"/>
        </li>
      </ul>
    </div>

  </div>
</div>



