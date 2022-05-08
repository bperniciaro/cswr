<%@ Page Language="C#" MasterPageFile="~/MasterPages/Sport.master" Theme="Web20" AutoEventWireup="true" CodeFile="links.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.FantasyRacingLinks" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
  Title="Our Favorite Fantasy NASCAR Racing Links Around the Web" 
  MetaDescription="This page contains links to various fantasy NASCAR racing websites categorized by type."
  CanonicalUrl="https://www.cheatsheetwarroom.com/fantasy-racing/nascar/free/links.aspx"
  %>
  
<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

<div class="linksPage">

  <h1>Fantasy Racing Links</h1>


  <div id="accordion">

    <h2 class="header">Websites</h2>
  
    <div class="groupContainer">

      <div class="linkContainer">
        <asp:HyperLink runat="server" NavigateUrl="http://dirtdrivers.com" Text="dirtdrivers.com"  rel="nofollow"/>
        <p class="description">
          Serving the Dirt Track Racing Community in the Central Hub of America.  
        </p>
      </div>

      <div class="linkContainer">
        <asp:HyperLink runat="server" NavigateUrl="http://awesomeracefans.com" Text="Awesome Race Fans" Target="_blank"  rel="nofollow"/>
        <p class="description">
          Where awesome race fans come for news.
        </p>
      </div>
          
      <div class="linkContainer">
        <asp:HyperLink runat="server" NavigateUrl="http://www.speedwaymedia.com/" Text="Speedway Media" Target="_blank"  rel="nofollow"/>
        <p class="description">
          A comprehensive online resource for motorsports information including Sprint Cup Series, Nationwide Series, 
          Camping World Truck Series, and more.
        </p>
      </div>

      <div class="linkContainer">
        <asp:HyperLink runat="server" NavigateUrl="http://www.racingjunky.com/" Text="Racing Junky" Target="_blank"  rel="nofollow"/>
        <p class="description">
          NASCAR Sprint Cup and Nationwide Series news, views, and commendary. 
        </p>
      </div>

      <div class="linkContainer">
        <asp:HyperLink runat="server" NavigateUrl="http://www.gonascargo.com/" Text="Go NASCAR Go" Target="_blank"  rel="nofollow"/>
        <p class="description">
          News, race and driver info at GoNascarGo.com. 
        </p>
      </div>

      <div class="linkContainer">
        <asp:HyperLink runat="server" NavigateUrl="http://ifantasyrace.com/" Text="iFantasyRace" Target="_blank"  rel="nofollow"/>
        <p class="description">
          Driver grades, fantasy news, blog content, and an assortment of other information about the world of NASCAR. 
        </p>
      </div>

      <div class="linkContainer">
        <asp:HyperLink runat="server" NavigateUrl="http://www.p10select.com/" Text="P10 Select Fantasy Racing" Target="_blank"  rel="nofollow"/>
        <p class="description">
          P10 Select is a free fantasy racing league which supports leagues based on Nascar® Sprint Cup, Nationwide and Camping World Truck series.
        </p>
      </div>

      <div class="linkContainer">
        <asp:HyperLink runat="server" NavigateUrl="http://www.racingjunky.com/" Text="Racing Junky" Target="_blank"  rel="nofollow"/>
        <p class="description">
          NASCAR, SPRINT Cup, and Nationwide Series news, views, and commentary. 
        </p>
      </div>
 
    </div>  <!-- close groupContainer -->

    <h2>Blogs</h2>

    <div class="groupContainer">

      <div class="linkContainer">
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="http://chicksview.blogspot.com/" Text="Chick's View"  rel="nofollow"/>
        <p class="description">
          NASCAR racing blog from a chick's point of view.
        </p>
      </div>

    </div>  <!-- groupContainer -->
    
    
    <h2>Other</h2>

    <div class="groupContainer">

      <div class="linkContainer">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://www.bestsynthetic.com/" Text="AMSOIL Synthetic Motor Oil"  rel="nofollow"/>
        <p class="description">
          AMSOIL was the FIRST synthetic motor oil and is still the best! Guaranteed for 25,000 miles.
        </p>
      </div>

    </div>  <!-- groupContainer -->
    

</div>  <!-- close Accordion -->
    
</div>


</asp:Content>

