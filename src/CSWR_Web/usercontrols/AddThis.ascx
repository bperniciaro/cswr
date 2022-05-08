<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddThis.ascx.cs" Inherits="usercontrols_AddThis" %>


<asp:Literal runat="server" ID="litAddThis" EnableViewState="false" Visible="false"/>


<asp:Panel runat="server" ID="panAsyncScript" Visible="false">
  <script type="text/ecmascript">
    // This allows for asynchronous loading of the AddThis script so that the page loads faster
    $(document).ready(function () {
      addthis.init();
    });
  </script>
</asp:Panel>
