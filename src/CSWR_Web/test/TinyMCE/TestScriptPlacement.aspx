<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="TestScriptPlacement.aspx.cs" 
  Inherits="test_TinyMCE_TestScriptPlacement" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">
  
  <asp:TextBox runat="server" ID="tbDefault" TextMode="MultiLine" CssClass="default">
    Easy (and free!) You should check out our premium features.  
  </asp:TextBox>

  <br/><br/>
  
  <div style="width:400px; background-color: orange;">
    <asp:TextBox runat="server" id="tbTest" TextMode="MultiLine" CssClass="editor">
      Easy (and free!) You should check out our premium features.
    </asp:TextBox> 
  </div>

  <br/><hr/>

  <div>
    <asp:Literal runat="server" id="litOnPageContent"/>
  </div>

  <asp:Button runat="server" id="butSubmit" OnClick="butSubmit_Click" Text="Submit"/>
  
   <script type="text/javascript">

     tinymce.init({
       selector: 'textarea.default'
     });
   
     tinymce.init({
       selector: ".editor",
       plugins: "link, autolink",
       default_link_target: "_blank",
       toolbar: "undo redo | bold italic | link unlink | cut copy paste | bullist numlist",
       menubar: false,
       statusbar: false
     });

   </script>
  

</asp:Content>

