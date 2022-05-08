<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NoSport.master" AutoEventWireup="true" CodeFile="AjaxAnimationTest.aspx.cs" Inherits="test_AjaxAnimationTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <asp:ScriptManager runat="server" />

  <style type="text/css">
    .overlay {position:absolute;width:400px;height:200px;margin:150px auto 0px auto;text-align:center;}
    .faded {-moz-opacity: 0.5; /*Mozila Firefox*/
            -webkit-opacity: 0.5; /*Webkit Standards - For I.E*/
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=50)"; /*Microsoft Standards*/
            filter: alpha(opacity=50); /*Modern Browsers Use This*/
            opacity: 0.5; /*One More For Good Measurement*/
            }

  </style>

  <asp:Panel runat="server" ID="panFormContainer" style="width:400px;height:400px;margin:100px auto;border:1px solid black;">
    <asp:UpdateProgress ID="updProgress" AssociatedUpdatePanelID="upUpdatePanel" runat="server" DynamicLayout="false" DisplayAfter="0">
        <ProgressTemplate>      
          <div class="overlay">
            <asp:Image runat="server" ImageUrl="~/Images/Animations/googlerotate.gif"/>
          </div>      
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="upUpdatePanel">
      <ContentTemplate>
        <div id="counterForm" class="form">
          <asp:Label runat="server" ID="labCounter" Text="1"></asp:Label>
          <br />
          <asp:Button runat="server" ID="butSubmit" Text="Submit" OnClick="butSubmit_Click" />

          <ul>
            <li>A</li>
            <li>A</li>
            <li>A</li>
            <li>A</li>
            <li>A</li>
            <li>A</li>
            <li>A</li>
          </ul>
        </div>


      </ContentTemplate>
    </asp:UpdatePanel>
  </asp:Panel>

<script type="text/javascript">


  var prm = Sys.WebForms.PageRequestManager.getInstance();
  prm.add_beginRequest(BeginRequestHandler);
  prm.add_endRequest(EndRequestHandler);

  function BeginRequestHandler(sender, args) {
    $("#counterForm").addClass("faded");
  }

  function EndRequestHandler(sender, args) {
    $("#counterForm").removeClass("faded");
  }

</script>

</asp:Content>

