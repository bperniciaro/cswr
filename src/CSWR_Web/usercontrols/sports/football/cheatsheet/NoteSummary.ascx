<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NoteSummary.ascx.cs" Inherits="BP.CheatSheetWarRoom.UI.UserControls.NoteSummary" %>

<asp:Panel runat="server" ID="panNotesContainer" CssClass="noteSummaryControl">

  <%--Player Notes--%>
  <asp:Panel runat="server" ID="panAllPlayerNotes" Visible="false">
    <h2>Player Notes</h2>
    <asp:Repeater runat="server" ID="repAllPlayerNotes" onitemdatabound="repNotes_ItemDataBound">
      <ItemTemplate>
        <p>
          <asp:Label runat="server" ID="labRank"></asp:Label>:
          <asp:Label runat="server" ID="labPlayerName" CssClass="bold"></asp:Label> - 
          <asp:Label runat="server" ID="labNote" CssClass="note"></asp:Label>
        </p>
      </ItemTemplate>
    </asp:Repeater>
  </asp:Panel>
    
  <%--Quarterback Notes--%>
  <asp:Panel runat="server" ID="panQBNotes" Visible="false">
    <h2>Quarterback Notes</h2>
    <asp:Repeater runat="server" ID="repQBNotes" onitemdatabound="repNotes_ItemDataBound">
      <ItemTemplate>
        <p>
          <asp:Label runat="server" ID="labRank"></asp:Label>:
          <asp:Label runat="server" ID="labPlayerName" CssClass="bold"></asp:Label> - 
          <asp:Label runat="server" ID="labNote" CssClass="note"></asp:Label>
        </p>
      </ItemTemplate>
    </asp:Repeater>
  </asp:Panel>

  <%--Running Back Notes--%>
  <asp:Panel runat="server" ID="panRBNotes" Visible="false">
    <h2>Running Back Notes</h2>
    <asp:Repeater runat="server" ID="repRBNotes" onitemdatabound="repNotes_ItemDataBound">
      <ItemTemplate>
        <p>
          <asp:Label runat="server" ID="labRank"></asp:Label>: 
          <asp:Label runat="server" ID="labPlayerName" CssClass="bold"></asp:Label> -
          <asp:Label runat="server" ID="labNote" CssClass="note"></asp:Label>
        </p>
      </ItemTemplate>
    </asp:Repeater>
  </asp:Panel>

  <%--Wide Receiver Notes--%>
  <asp:Panel runat="server" ID="panWRNotes" Visible="false">
    <h2>Wide Receiver Notes</h2>
    <asp:Repeater runat="server" ID="repWRNotes" onitemdatabound="repNotes_ItemDataBound">
      <ItemTemplate>
        <p>
          <asp:Label runat="server" ID="labRank"></asp:Label>:
          <asp:Label runat="server" ID="labPlayerName" CssClass="bold"></asp:Label> - 
          <asp:Label runat="server" ID="labNote" CssClass="note"></asp:Label>
        </p>
      </ItemTemplate>
    </asp:Repeater>
  </asp:Panel>

  <%--Tight End Notes--%>
  <asp:Panel runat="server" ID="panTENotes" Visible="false">
    <h2>Tight End Notes</h2>
    <asp:Repeater runat="server" ID="repTENotes" onitemdatabound="repNotes_ItemDataBound">
      <ItemTemplate>
        <p>
          <asp:Label runat="server" ID="labRank"></asp:Label>:
          <asp:Label runat="server" ID="labPlayerName" CssClass="bold"></asp:Label> - 
          <asp:Label runat="server" ID="labNote" CssClass="note"></asp:Label>
        </p>
      </ItemTemplate>
    </asp:Repeater>
  </asp:Panel>

  <%--Kicker Notes--%>
  <asp:Panel runat="server" ID="panKNotes" Visible="false">
    <h2>Kicker Notes</h2>
    <asp:Repeater runat="server" ID="repKNotes" onitemdatabound="repNotes_ItemDataBound">
      <ItemTemplate>
        <p>
          <asp:Label runat="server" ID="labRank"></asp:Label>:
          <asp:Label runat="server" ID="labPlayerName" CssClass="bold"></asp:Label> -
          <asp:Label runat="server" ID="labNote" CssClass="note"></asp:Label>
        </p>
      </ItemTemplate>
    </asp:Repeater>
  </asp:Panel>

  <%--Kicker Notes--%>
  <asp:Panel runat="server" ID="panDFNotes" Visible="false">
    <h2>Defense Notes</h2>
    <asp:Repeater runat="server" ID="repDFNotes" onitemdatabound="repNotes_ItemDataBound">
      <ItemTemplate>
        <p>
          <asp:Label runat="server" ID="labRank"></asp:Label>:
          <asp:Label runat="server" ID="labPlayerName" CssClass="bold"></asp:Label> - 
          <asp:Label runat="server" ID="labNote" CssClass="note"></asp:Label>
        </p>
      </ItemTemplate>
    </asp:Repeater>
  </asp:Panel>

</asp:Panel>  <!-- close notesContainer -->
