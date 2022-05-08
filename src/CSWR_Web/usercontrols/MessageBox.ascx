<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MessageBox.ascx.cs" Inherits="BP.CheatSheetWarRoom.UI.UserControls.MessageBox" %>


<div class="messageBox">
  <table>
    <tr class="messageRow">
      <td runat="server" id="tdLeftSide"></td>
      <td runat="server" id="tdMessageCell">
        <asp:Literal runat="server" ID="litMessage" />
      </td>
      <td runat="server" id="tdRightSide"></td>
    </tr>
  </table>
</div>

