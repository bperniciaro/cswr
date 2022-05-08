<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CSWRRankings.ascx.cs" Inherits="BP.CheatSheetWarRoom.UI.UserControls.CSWRRankings" %>
<%@ Register Src="~/usercontrols/navigation/PlayerRankingNavigation.ascx" TagName="PlayerRankingsNavigation" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/Sports/Football/PlayerRankings/CSWRFOORankingItemTemplate.ascx" TagPrefix="cswr" TagName="CSWRFOORankingItemTemplate" %>
<%@ Register Src="~/usercontrols/ads/AdGenerator.ascx" TagName="AdGenerator" TagPrefix="cswr" %>
<%@ Register Src="~/usercontrols/Sports/Racing/PlayerRankings/CSWRRACRankingItemTemplate.ascx" TagName="CSWRRACRankingsItemTemplate" TagPrefix="cswr" %>

<div class="playerRankingContainer">
    
  <%--Navigation--%>
  <cswr:PlayerRankingsNavigation runat="server" ID="prnRankingsNavigation" />
  
  
 
  <%--Supplemental List--%>
<%--  <asp:Panel runat="server" ID="panSuppItemLising" CssClass="suppItemListingControl">

    <h2>
      <asp:Literal runat="server" ID="litSubHeading" />
    </h2>
  
    <div class="container">
  --%>
  <asp:Panel runat="server" ID="panSuppItemListing">
      <asp:Panel runat="server" ID="panAdContainer" CssClass="adContainer">
        <cswr:AdGenerator runat="server" Source="GOOGLE" Size="S468X60" Ad_Slot="2278632982" />
      </asp:Panel>
    
      <%--Call 2 Action--%>
      <asp:Panel runat="server" ID="panCall2Action" CssClass="call2Action">
        <div>
          <strong>Why base your draft on someone else's rankings?</strong>
        </div>
        <div>
          Create a 
          <asp:HyperLink runat="server" ID="hlCall2Action" />
          for free using drag and drop.
        </div>
      </asp:Panel>
    
      <asp:Repeater runat="server" ID="repCSWRRankings" OnItemDataBound="repCSWRRankings_ItemDataBound">
        <ItemTemplate>
          <cswr:CSWRFOORankingItemTemplate runat="server" id="pritFOORankingItemTemplate" />
          <cswr:CSWRRACRankingsItemTemplate runat="server" ID="pritRACRankingItemTemplate" />
        </ItemTemplate>
      </asp:Repeater>

  </asp:Panel>
 <%--   </div>

  </asp:Panel>--%>

  <asp:Panel runat="server" ID="panNoRecords" style="text-align:center;font-style:italic;">
    <asp:Label runat="server" ID="labNoRecords"></asp:Label>
  </asp:Panel>
 
</div>
