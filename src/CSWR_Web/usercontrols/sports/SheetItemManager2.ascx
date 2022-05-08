<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SheetItemManager2.ascx.cs" Inherits="BP.CheatSheetWarRoom.UI.UserControls.SheetItemManager2" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagPrefix="cswr" TagName="MessageBox" %>


<div class="sheetItemManagerControl2">


<asp:UpdateProgress ID="upUpdateProgress" AssociatedUpdatePanelID="upSheetPlayers" runat="server" DynamicLayout="false" DisplayAfter="0">
  <ProgressTemplate>      
    <div id="ajaxLoaderOverlay" class="ajaxOverlay" style="margin-top:100px;">
      <asp:Image runat="server" ImageUrl="~/Images/Animations/googlerotate.gif"/>
    </div>      
  </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel runat="server" ID="upSheetPlayers">
  <ContentTemplate>
    <div class="ajaxFormContainer">
          
      <%--<div class="container-fluid">--%>
        <div class="row">
          <div class="col-md-12">
            <%--Status Message--%>
            <asp:Panel runat="server" ID="panStatusMessage" CssClass="alert alert-info">
                 <asp:Label runat="server" ID="labStatusMessage" />
            </asp:Panel>
          </div>

          <div class="col-sm-4 text-center" style="padding:20px 0px;">
            <h3><asp:Literal runat="server" ID="litSheetTitle"></asp:Literal></h3>
            <div style="padding-bottom:5px;">
              <asp:DropDownList runat="server" ID="ddlSortSheetPlayers" AutoPostBack="True" OnSelectedIndexChanged="ddlSortSheetPlayers_SelectedIndexChanged" CssClass="small" Width="200">
                <asp:ListItem Text="Sort by Name" Value="Name"></asp:ListItem>
                <asp:ListItem Text="Sort by Sheet Order" Value="Rank"></asp:ListItem>
              </asp:DropDownList>
              
            </div>
            <asp:ListBox ID="lbSheetPlayers" runat="server" Rows="10" CssClass="listBox" SelectionMode="Multiple" Width="200"></asp:ListBox>
            <div>
              <span class="totalText">Sheet <asp:Label runat="server" ID="labItemType" />:</span> <asp:Label ID="labTotalSheetPlayers" runat="server" CssClass="totalText"></asp:Label>
            </div>
          </div>

          <div class="col-sm-4 text-center" style="padding:30px 30px;">
            <asp:Button runat="server" ID="butAddPlayer" Text="Add Player(s) to Sheet" CssClass="btn btn-primary" OnClick="butAddPlayer_Click"  />
            <br />
            <br />
            <asp:Button runat="server" ID="butRemovePlayer" Text="Remove Player(s) from Sheet" CssClass="btn btn-primary" OnClick="butRemovePlayer_Click" />
            <%--<asp:ImageButton runat="server" ID="ibAddPlayer" ImageUrl="~/Images/UserControls/SheetItemManager/ArrowLeft.gif" OnClick="ibAddPlayer_Click" ToolTip="Click to move the designated player from the Player Pool to the Cheat Sheet." />--%>
            <%--<asp:ImageButton runat="server" ID="ibRemovePlayer" ImageUrl="~/Images/UserControls/SheetItemManager/ArrowRight.gif" OnClick="ibRemovePlayer_Click" ToolTip="Click to move the designated player from the Cheat Sheet back into the Play Pool."/>--%>
          </div>

          <div class="col-sm-4 text-center" style="padding:20px 0px;">
            <h3><asp:Literal runat="server" ID="litPlayerPoolTitle"/></h3>
            <div style="padding-bottom:5px;">
              <asp:DropDownList runat="server" ID="ddlSortPoolPlayers" AutoPostBack="true" OnSelectedIndexChanged="ddlSortPoolPlayers_SelectedIndexChanged" 
                CssClass="small" AppendDataBoundItems="true" width="200">
                <asp:ListItem Text="Sort by Name" Value="Name-ASC" />
              </asp:DropDownList>
            </div>
            <asp:ListBox ID="lbPlayerPool" runat="server" Rows="10" CssClass="listBox" SelectionMode="Multiple" With="200"></asp:ListBox>
            <div>
              <span class="totalText">Available <asp:Label runat="server" ID="labItemType2" />:</span> <asp:Label ID="labTotalAvailablePlayers" runat="server" CssClass="totalText"></asp:Label>
            </div>
          </div>
        </div>
     <%-- </div>--%>

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



