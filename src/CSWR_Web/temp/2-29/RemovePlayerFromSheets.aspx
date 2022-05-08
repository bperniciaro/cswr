<%@ Page Title="Remove Duplicate Player" Language="C#" MasterPageFile="~/MasterPages/Frame.master" AutoEventWireup="true" CodeFile="RemovePlayerFromSheets.aspx.cs" Inherits="temp_2_29_RemovePlayerFromSheets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">
  
  <h1>Remove Duplicate Player</h1>
  <p>This page is helpful when needing to remove a player from ALL sheets, such as a player that is deemed to be a duplicate.</p>

  PlayerID to Keep: <asp:TextBox runat="server" id="tbKeeperPlayerId"></asp:TextBox>
  <br/>
  PlayerID to Delete: <asp:TextBox runat="server" id="tbGonerPlayerId"></asp:TextBox>
  
  
  <br/><br/>
  <asp:CheckBox runat="server" id="cbCheatSheets" Text="Cheat Sheets"/>
  <asp:CheckBox runat="server" id="cbSuppSheets" Text="Supp Sheets"/>
  <asp:CheckBox runat="server" id="cbArchivedSheets" Text="Archived Sheets"/>
  <br/>
  <asp:Button runat="server" ID="butSubmit" OnClick="butSubmit_Click" Text="Process Players"/>

</asp:Content>

