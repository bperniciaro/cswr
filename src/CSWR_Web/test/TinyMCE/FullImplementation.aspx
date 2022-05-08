<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Frame.master" AutoEventWireup="true" CodeFile="FullImplementation.aspx.cs" 
  Inherits="TinyMCE" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <asp:ScriptManager runat="server"/>
  
  
  <asp:UpdatePanel runat="server" id="upUpdatPanel">
    <ContentTemplate>

      <asp:TextBox runat="server" id="tbHtmlEditor" TextMode="MultiLine">
        Default editor text
      </asp:TextBox>
      
      <asp:Dropdownlist runat="server" ID="ddlTest" AutoPostBack="true" OnSelectedIndexChanged="ddlTest_SelectedIndexChanged">
        <Items>
           <asp:ListItem Text="A"></asp:ListItem>
           <asp:ListItem Text="B"></asp:ListItem>
        </Items>
      </asp:Dropdownlist>

      <asp:Button runat="server" ID="butSaveEditorContent" OnClick="butSaveEditorContent_Click" Text="Save Html Content"/>      

    </ContentTemplate>
  </asp:UpdatePanel>

  
  
  <script type="text/javascript">

      $(document).ready(function () {
        /* initial load of editor */
        LoadTinyMCE();
      });

      /* wire-up an event to re-add the editor */     
      Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler_Page);

      /* fire this event to remove the existing editor and re-initialize it*/
      function EndRequestHandler_Page(sender, args) {
        //1. Remove the existing TinyMCE instance of TinyMCE
        tinymce.remove( "#<%=tbHtmlEditor.ClientID%>");
        //2. Re-init the TinyMCE editor
        LoadTinyMCE();
      }

      function BeforePostback() {
        tinymce.triggerSave();
      }
        
      function LoadTinyMCE() {

        /* initialize the TinyMCE editor */
        tinymce.init({
          selector: "#<%=tbHtmlEditor.ClientID%>",
          plugins: "link, autolink",
          default_link_target: "_blank",
          toolbar: "undo redo | bold italic | link unlink | cut copy paste | bullist numlist",
          menubar: false,
          statusbar: false
        });
      }

  </script>




</asp:Content>

