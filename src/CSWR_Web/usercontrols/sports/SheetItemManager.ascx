<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SheetItemManager.ascx.cs" Inherits="BP.CheatSheetWarRoom.UI.UserControls.SheetItemManager" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagPrefix="cswr" TagName="MessageBox" %>


<div class="sheetItemManagerControl">


<asp:UpdateProgress ID="upUpdateProgress" AssociatedUpdatePanelID="upSheetPlayers" runat="server" DynamicLayout="false" DisplayAfter="0">
  <ProgressTemplate>      
    <div id="ajaxLoaderOverlay" class="ajaxOverlay" style="width:450px;margin-top:100px;">
      <asp:Image runat="server" ImageUrl="~/Images/Animations/googlerotate.gif"/>
    </div>      
  </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel runat="server" ID="upSheetPlayers">
  <ContentTemplate>
    <div class="ajaxFormContainer">
          
      <cswr:MessageBox runat="server" ID="mbStatus"/>




      <%--Main control--%>
      <table>
        <tr>
          <%--Sheet Players--%>
          <td>
            <h3><asp:Literal runat="server" ID="litSheetTitle"></asp:Literal></h3>
            <asp:DropDownList runat="server" ID="ddlSortSheetPlayers" AutoPostBack="True" OnSelectedIndexChanged="ddlSortSheetPlayers_SelectedIndexChanged" CssClass="small">
              <asp:ListItem Text="Sort by Name" Value="Name"></asp:ListItem>
              <asp:ListItem Text="Sort by Sheet Order" Value="Rank"></asp:ListItem>
            </asp:DropDownList>
            <asp:ListBox ID="lbSheetPlayers" runat="server" Rows="10" CssClass="listBox" SelectionMode="Multiple"></asp:ListBox>
            <span class="totalText">Total Sheet <asp:Label runat="server" ID="labItemType" />:</span> <asp:Label ID="labTotalSheetPlayers" runat="server" CssClass="totalText"></asp:Label>
          </td>
          <%--Arrow Buttons--%>
          <td class="arrowCell">
            <asp:ImageButton runat="server" ID="ibAddPlayer" ImageUrl="~/Images/UserControls/SheetItemManager/ArrowLeft.gif" OnClick="ibAddPlayer_Click" ToolTip="Click to move the designated player from the Player Pool to the Cheat Sheet." />
            <asp:ImageButton runat="server" ID="ibRemovePlayer" ImageUrl="~/Images/UserControls/SheetItemManager/ArrowRight.gif" OnClick="ibRemovePlayer_Click" ToolTip="Click to move the designated player from the Cheat Sheet back into the Play Pool."/>
          </td>
          <%--Player Pool--%>
          <td>
            <h3><asp:Literal runat="server" ID="litPlayerPoolTitle"/></h3>
            <asp:DropDownList runat="server" ID="ddlSortPoolPlayers" AutoPostBack="true" OnSelectedIndexChanged="ddlSortPoolPlayers_SelectedIndexChanged" 
              CssClass="small" AppendDataBoundItems="true">
              <asp:ListItem Text="Sort by Name" Value="Name-ASC" />
              <%--<asp:ListItem Text="Sort by Total Fantasy Points" Value="TFP" />--%>
            </asp:DropDownList>
            <asp:ListBox ID="lbPlayerPool" runat="server" Rows="10" CssClass="listBox" SelectionMode="Multiple"></asp:ListBox>
            <span class="totalText">Total Available <asp:Label runat="server" ID="labItemType2" />:</span> <asp:Label ID="labTotalAvailablePlayers" runat="server" CssClass="totalText"></asp:Label>
          </td>
        </tr>
      </table>

    </div>
  </ContentTemplate>
</asp:UpdatePanel>

  <script type="text/javascript">


    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_beginRequest(BeginRequestHandler);
    prm.add_endRequest(EndRequestHandler);

    function BeginRequestHandler(sender, args) {
      $('.ajaxFormContainer').addClass("ajaxFormFaded");
    }

    function EndRequestHandler(sender, args) {
      $('.ajaxFormContainer').removeClass("ajaxFormFaded");
    }

</script>

</div>



