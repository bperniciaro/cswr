<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RACNoteEditor.ascx.cs" Inherits="BP.CheatSheetWarRoom.UI.UserControls.RACNoteEditor" %>
<%@ Register Namespace="BP.CheatSheetWarRoom.MyControls" TagPrefix="cswr" %>



<div class="racNoteEditor">

  <div class="buttonContainer">
    <asp:Image runat="server" ID="imaAdd" ImageUrl="~/Images/UserControls/NoteEditor/newadd.gif" ToolTip="Click to add a note."/><asp:Image runat="server" ID="imaEdit" ImageUrl="~/Images/UserControls/NoteEditor/Edit.gif" ToolTip="Click to edit this note." /><cswr:PregoHyperlink runat="server" ID="phSave" ImageUrl="~/Images/UserControls/NoteEditor/Save.gif" ToolTip="Click to save this note." /><asp:Image runat="server" ID="imaCancel" EnableViewState="false" ImageUrl="~/Images/UserControls/NoteEditor/Cancel.gif" ToolTip="Click to cancel your changes." /><cswr:PregoHyperlink runat="server" ID="phDelete" ImageUrl="~/Images/UserControls/NoteEditor/Delete.gif" ToolTip="Click to delete this note." />
  </div>
  
  <%--This container holds the portion of the control that scrolls--%>
  <div class="scrollContainer">
  
    <%--View--%>
    <asp:Panel runat="server" ID="panViewNote" CssClass="viewNoteContainer">
      <asp:Label runat="server" ID="labNote"/><asp:HiddenField runat="server" ID="hfFullNote" EnableViewState="false" /><asp:Image runat="server" ID="imaNoteSummary" ImageUrl="~/Images/Icons/Note_Summary.gif" EnableViewState="false"/>
    </asp:Panel>
    
    <%--Create/Edit--%>
    <asp:Panel runat="server" ID="panEditNote" CssClass="editNoteContainer">
      <asp:TextBox runat="server" ID="tbEditNote" TextMode="multiLine" EnableViewState="false"></asp:TextBox>
    </asp:Panel>

  </div>  
    
</div>  
  
