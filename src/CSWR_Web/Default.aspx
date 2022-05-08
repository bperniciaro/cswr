<%@ Page Language="C#" MasterPageFile="~/MasterPages/ResponsiveOneCol.master" Theme="Web20" AutoEventWireup="true" CodeFile="Default.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.Default2" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
  Title="Fantasy Football Advice & Draft Preparation Tools"
  MetaDescription="Create free, customized fantasy cheat sheets for fantasy football."
    CanonicalUrl="https://www.cheatsheetwarroom.com"
%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <div class="homePage container-fluid">

    <div class="row" style="width:90%;margin:auto;margin-bottom:50px;">

      <h1>Create <strong>customized player rankings</strong> on an interactive, cheat sheet.  Then, generate a printable cheat sheet to take to your fantasy draft.</h1>

      <div class="col-md-6">

        <div class="stepContainer heading">
          STEP 1
        </div>
        <!--Step Instructions-->
        <div>
          <h3 class="heading">CREATE CUSTOM PLAYER RANKINGS</h3>
        </div>
        <!--Dividor-->
        <asp:Image runat="server" ImageUrl="~/Images/Layout/step1dasheddividor.gif" AlternateText="Creating custom player rankings is simple and intuitive." />
        <!--Instructions List-->
        <ul>
          <li>
            <span class="bullet"></span> Drag and drop players on a custom, interactive fantasy cheat sheet
          </li>
          <li>
            <span class="bullet"></span> View stats in all formats including
            <abbr title="Points Per Reception">PPR</abbr> and standard
              <strong>fantasy football scoring</strong>
          </li>
        </ul>       
        <%--Button--%>
        <div style="text-align:center">
          <asp:Button runat="server" PostBackUrl="~/fantasy-football/nfl/create/custom-sheet.aspx" Text="Create Custom Cheat Sheet" CssClass="btn btn-primary" />
        </div>

      </div>
      <div class="col-md-6">

        <div class="stepContainer heading">
          STEP 2
        </div>
        <!--Step Instructions-->
        <div>
          <h3 class="heading">GENERATE A PRINTABLE CHEAT SHEET</h3>
        </div>
        <!--Dividor-->
        <asp:Image runat="server" ImageUrl="~/Images/Layout/step2dasheddividor.gif" AlternateText="Print cheat sheets incorporate important data from your cheat sheet" />
        <!--Instructions List-->
        <ul>
          <li>
            <span class="bullet"></span> Your rankings get added into your
            <strong>printable fantasy football cheat sheets</strong>
          </li>
          <li>
            <span class="bullet"></span> Choose the print format that best suits your fantasy league's draft format
          </li>
        </ul>
        <%--Button--%>
        <div style="text-align:center">
          <asp:Button runat="server" PostBackUrl="~/fantasy-football/nfl/free/printable/offense/cheat-sheet-with-roster.aspx" Text="Generate Printable Cheat Sheet" CssClass="btn btn-primary" />
        </div>
      </div>
    </div>
 
    <div class="blogPostsContainer">

      <h2>Some of our Top-Rated Blog Posts</h2>

      <%--Row 1--%>
      <div class="row">
        <div class="col-lg-6">
          <div class="articleContainer">
            <asp:Image runat="server" ID="imaLineupOptimizers" ImageUrl="~/Images/Layout/responsive/home/best-dfs-lineup-optimizer.jpg" AlternateText="DFS Lineup Optimizers"/>
            <asp:HyperLink runat="server" NavigateUrl="https://www.cheatsheetwarroom.com/blog/dfs/research/dfs-lineup-optimizer">DFS Lineup Optimizers</asp:HyperLink>
          </div>
        </div>
         <div class="col-lg-6">
           <div class="articleContainer">
              <asp:Image runat="server" ID="imgBestDraftBoards" ImageUrl="~/Images/Layout/responsive/home/best-fantasy-football-draft-boards.jpg" AlternateText="Fantasy Football Draft Boards"/>
              <asp:HyperLink runat="server" NavigateUrl="https://www.cheatsheetwarroom.com/blog/fantasy-football/draft/boards">Fantasy Football Draft Boards</asp:HyperLink>
           </div>
        </div>
      </div>

      <%--Row 2--%>
      <div class="row">
        <div class="col-lg-6">
          <div class="articleContainer">
            <asp:Image runat="server" ID="imaBestAdvicesites" ImageUrl="~/Images/Layout/responsive/home/best-fantasy-football-advice-sites.gif" AlternateText="Fantasy Football Advice Sites"/>
            <asp:HyperLink runat="server" NavigateUrl="https://www.cheatsheetwarroom.com/blog/fantasy-football/site/best-advice">Fantasy Football Advice Sites</asp:HyperLink>
          </div>
        </div>
        <div class="col-lg-6">
          <div class="articleContainer">
            <asp:Image runat="server" ID="imaBestDraftSoftwre" ImageUrl="~/Images/Layout/responsive/home/best-fantasy-football-draft-software.jpg" AlternateText="Fantasy Football Draft Software"/>
            <asp:HyperLink runat="server" NavigateUrl="https://www.cheatsheetwarroom.com/blog/fantasy-football/draft/software">Fantasy Football Draft Software</asp:HyperLink>
          </div>
        </div>
      </div>

      <%--Row 3--%>
      <div class="row">
        <div class="col-lg-6">
          <div class="articleContainer">
            <asp:Image runat="server" ID="Image1" ImageUrl="~/Images/Layout/responsive/home/best-fantasy-football-money-leagues.jpg" AlternateText="Fantasy Football Money Leagues"/>
            <asp:HyperLink runat="server" NavigateUrl="https://www.cheatsheetwarroom.com/blog/fantasy-football/leagues/money-league-cash-payouts">Fantasy Football Money Leagues</asp:HyperLink>
          </div>
        </div>
        <div class="col-lg-6">
          <div class="articleContainer">
            <asp:Image runat="server" ID="Image2" ImageUrl="~/Images/Layout/responsive/home/best-fantasy-football-tools.gif" AlternateText="Fantasy Football Tools"/>
            <asp:HyperLink runat="server" NavigateUrl="https://www.cheatsheetwarroom.com/blog/fantasy-football/tools">Best Fantasy Football Tools</asp:HyperLink>
          </div>
        </div>
      </div>

      <%--Row 4--%>
      <div class="row">
        <div class="col-lg-6">
          <div class="articleContainer">
            <asp:Image runat="server" ID="Image3" ImageUrl="~/Images/Layout/responsive/home/fantasy-football-prize-ideas.jpg" AlternateText="Fantasy Prize Ideas"/>
            <asp:HyperLink runat="server" NavigateUrl="https://www.cheatsheetwarroom.com/blog/fantasy-football/prizes">Fantasy Prize Ideas</asp:HyperLink>
          </div>
        </div>
        <div class="col-lg-6">
          <div class="articleContainer">
            <asp:Image runat="server" ID="Image4" ImageUrl="~/Images/Layout/responsive/home/fantasy-football-team-names.jpg" AlternateText="Fantasy Football Team Names"/>
            <asp:HyperLink runat="server" NavigateUrl="https://www.cheatsheetwarroom.com/blog/fantasy-football/names">Fantasy Football Team Names</asp:HyperLink>
          </div>
        </div>
      </div>


    </div>
            
  </div>


</asp:Content>

