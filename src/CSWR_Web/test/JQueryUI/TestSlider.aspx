<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Frame.master" AutoEventWireup="true" CodeFile="TestSlider.aspx.cs" Inherits="test_JQueryUI_TestSlider" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <link rel="stylesheet" href="//code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css">

  <style type="text/css">
    #slider {width:300px;font-size:10px;}
    .ui-slider-range { background:red; }  
    .ui-slider-handle { border-color: #ef2929; }  
  </style>

   Randomize players in groups of: <input type="text" id="amount" style="border:0; color:#f6931f; font-weight:bold;" />
  <div id="slider"></div>

  <br />

  <asp:HiddenField runat="server" ID="hfGroupSize" Value="5" />
  <asp:Button runat="server" ID="butRandomize" Text="Randomize" onclick="butRandomize_Click" />



  <script type="text/javascript">

    alert("1");

    // Create the tooltips only on document load
    $(document).ready(function () {

      $("#slider").slider({
        range: "min",
        min: 5,
        max: 20,
        value: $('#<%=hfGroupSize.ClientID%>').val(),
        slide: function (event, ui) {
          $("#amount").val(ui.value);
          $("#hfGroupSize").val(ui.value);
        }
      });
      $("#amount").val($("#slider").slider("value"));
      $("#hfGroupSize").val($("#slider").slider("value"));
    });

    alert("2");

  </script>
</asp:Content>

