<%@ Page Title="Overall User Accuracy" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" 
  CodeFile="useroverallaccuracy.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.UserOverallAccuracy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

  <br /><br />

  <div style="width:900px;">

    <asp:GridView runat="server" CssClass="overallAccuracyGrid display" ID="gvOverallUserAccuracy" AutoGenerateColumns="false" 
        OnRowDataBound="gvOverallUserAccuracy_RowDataBound" OnPreRender="gvOverallUserAccuracy_PreRender">
      <Columns>
        <asp:BoundField DataField="Rank" HeaderText="Rank" />
        <asp:BoundField DataField="Username" HeaderText="User" />

        <asp:TemplateField HeaderText="QB Score">
          <ItemTemplate>
            <asp:HyperLink runat="server" ID="hlSheetLink"/>
          </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="RBScore" HeaderText="RB Score" />
        <asp:BoundField DataField="WRScore" HeaderText="WR Score" />
        <asp:BoundField DataField="TEScore" HeaderText="TE Score" />
        <asp:BoundField DataField="KScore" HeaderText="K Score" />
        <asp:BoundField DataField="DFScore" HeaderText="DF Score" />
        <asp:BoundField DataField="OverallScore" HeaderText="Overall Score" />
      </Columns>
    </asp:GridView>

  </div>

  <script type="text/javascript">

    $(document).ready(function () {
      //$(".gvv").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
      $('.overallAccuracyGrid').dataTable();
    });

  </script>




</asp:Content>

