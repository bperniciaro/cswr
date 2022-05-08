<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FooPlayerStatusEditor.ascx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.UserControls.FooPlayerStatusEditor" %>

<asp:ScriptManager runat="server"></asp:ScriptManager>

<h2><asp:Literal runat="server" id="litEditorTitle"/></h2>

<table>
  <%--Player--%>
  <tr>
    <td style="width: 150px;">Player Name</td>
    <td>
      <asp:TextBox runat="server" id="tbPlayer" Width="250" CssClass="players"/>
    </td>
  </tr>
  <%--Status--%>
  <tr>
    <td>Player Status</td>
    <td>
      <asp:DropDownList runat="server" ID="ddlStatusCodes" DataValueField="StatusCode" DataTextField="Name"
        AutoPostBack="True" OnSelectedIndexChanged="ddlStatusCodes_SelectedIndexChanged" AppendDataBoundItems="true" OnDataBound="ddlStatusCodes_DataBound">
            
        <Items>
          <asp:ListItem Text="Select Status" Value="0"/>
        </Items>

      </asp:DropDownList>
    </td>
  </tr>

  <%--Supp Info--%>
  <tr runat="server" id="trSupplementalInfo">
    <td style="vertical-align:top;">
      <%--Supp Label--%>          
      <asp:Label runat="server" id="labSuppInfoLabel"></asp:Label>
      <%--Supp Help Popup Icon--%>
      <a href="#">
        <asp:Label runat="server" id="labSuppInfoHelp" CssClass="helpIcon"/>
      </a> 
    </td>
    <td>
      <%--MCEEditor--%> 
      <asp:TextBox runat="server" id="tbSupplementalInfo" TextMode="MultiLine" CssClass="editor">
        Easy (and free!) You should check out our premium features.
      </asp:TextBox> 
    </td>
  </tr>

  <%--Count Info--%>
  <tr runat="server" id="trCountInfo">
    <td style="vertical-align:top;">
      <%--Count Label--%>          
      <asp:Label runat="server" id="labCountLabel"></asp:Label>
      <%--Supp Help Popup Icon--%>
      <a href="#">
        <asp:Label runat="server" id="labCountInfoHelp" CssClass="helpIcon"/>
      </a> 
    </td>
    <td>
      <%--Count--%> 
      <asp:TextBox runat="server" id="tbCount"></asp:TextBox> 
    </td>
  </tr>

</table>





  <script type="text/javascript">

      $(document).ready(function () {
        LoadTinyMCE();
        /* turn a textbox into autocomplete */
        $(".players").autocomplete({
              source: '/handlers/playerautocomplete.ashx'
          });

      });

      var prm = Sys.WebForms.PageRequestManager.getInstance();
      prm.add_endRequest(function (sender, e) {
        /* turn a textbox into autocomplete */
        $(".players").autocomplete({
          source: '/handlers/playerautocomplete.ashx'
        });

        //1. Remove the existing TinyMCE instance of TinyMCE
        tinymce.remove("#<%=tbSupplementalInfo.ClientID%>");
        //2. Re-init the TinyMCE editor
        LoadTinyMCE();
      });

      function SaveTextBoxBeforePostBack() {
        tinymce.triggerSave();
      }
        
      function LoadTinyMCE() {
        /* wire-up the html editor */
        tinymce.init({
          selector: "#<%=tbSupplementalInfo.ClientID%>",
          plugins: "link, autolink",
          default_link_target: "_blank",
          toolbar: "undo redo | bold italic | link unlink | cut copy paste | bullist numlist",
          menubar: false,
          statusbar: false
        });
      }

  </script>

