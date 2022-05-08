<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="FOONoteEditor.ascx.cs" Inherits="BP.CheatSheetWarRoom.UI.UserControls.FOONoteEditor" %>
<%@ Register Namespace="BP.CheatSheetWarRoom.MyControls" TagPrefix="cswr" %>



<div class="fooNoteEditor">

  <div class="buttonContainer">
    <asp:Image runat="server" ID="imaAdd" ImageUrl="~/Images/UserControls/FOONoteEditor/newadd.gif" ToolTip="Click to add a note." EnableViewState="false"/><asp:Image runat="server" ID="imaEdit" ImageUrl="~/Images/UserControls/FOONoteEditor/Edit.gif" ToolTip="Click to edit this note." EnableViewState="false" /><cswr:PregoHyperlink runat="server" ID="phSave" ImageUrl="~/Images/UserControls/FOONoteEditor/Save.gif" ToolTip="Click to save this note." EnableViewState="false" /><asp:Image runat="server" ID="imaCancel" EnableViewState="false" ImageUrl="~/Images/UserControls/FOONoteEditor/Cancel.gif" ToolTip="Click to cancel your changes." /><cswr:PregoHyperlink runat="server" ID="phDelete" ImageUrl="~/Images/UserControls/FOONoteEditor/Delete.gif" ToolTip="Click to delete this note." EnableViewState="false" />
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
  
