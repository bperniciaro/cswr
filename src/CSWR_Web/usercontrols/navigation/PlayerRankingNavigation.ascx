<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PlayerRankingNavigation.ascx.cs" Inherits="BP.CheatSheetWarRoom.UI.UserControls.PlayerRankingNavigation" %>


<div class="container-fluid">
  <div class="thirdLevelNav">
    <ul runat="server" class="nav navbar-nav" id="ulNavigationList">
      <asp:Repeater ID="repMenu" runat="server" EnableViewState="false" onitemdatabound="repMenu_ItemDataBound">
        <ItemTemplate>
          <li runat="server" id="item">
            <asp:HyperLink runat="server" ID="hlPositionLink" CssClass="btn btn-primary"/>
          </li>
        </ItemTemplate>
      </asp:Repeater>
    </ul>
  </div>
</div>

<script>
  $("#hamburger-3").click(function(){
    $(this).toggleClass("is-active");
    $("#bs-example-navbar-collapse-3").toggleClass("is-active");
    $('#bs-example-navbar-collapse-3').slideToggle("400");
  });
</script>
