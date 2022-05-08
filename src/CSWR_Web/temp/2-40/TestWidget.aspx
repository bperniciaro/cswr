<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Responsive.master" AutoEventWireup="true" CodeFile="TestWidget.aspx.cs" Inherits="temp_2_40_TestWidget" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <script type="text/javascript">
    function getProducts() {
        $.getJSON("api/products",
            function (data) {
                $('#products').empty(); // Clear the table body.

                // Loop through the list of products.
                $.each(data, function (key, val) {
                    // Add a table row for the product.
                    var row = '<td>' + val.Name + '</td><td>' + val.Price + '</td>';
                    $('<tr/>', { html: row })  // Append the name.
                        .appendTo($('#products'));
                });
            });
        }

        $(document).ready(getProducts);
  </script>


</asp:Content>

