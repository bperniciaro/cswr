<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="upgradedusers.aspx.cs" 
    Inherits="BP.CheatSheetWarRoom.UI.Admin.Users.UpgradedUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

  <h1>Upgraded Users</h1>

  <h2>Upgrade a User</h2>

  Username
  <asp:TextBox runat="server" ID="tbUsername" />
  <asp:Button runat="server" ID="butUpgrade" Text="Upgrade" OnClick="butUpgrade_Click" />

  <h2>Currently Upgraded Users</h2>

  <%--Incomplete Field Grid--%>
  <div style="width:900px;">
  <asp:GridView runat="server" ID="gvUpgradedUsers" OnPreRender="gvUpgradedUsers_PreRender"
    CssClass="incompleteGrid display test" AutoGenerateColumns="false" 
    ondatabound="gvUpgradedUsers_DataBound" OnRowDataBound="gvUpgradedUsers_RowDataBound">
    <Columns>
      <%--Username--%>
      <asp:TemplateField HeaderText="Username">
        <ItemTemplate>
          <asp:Label runat="server" ID="labUserName" />
        </ItemTemplate>
      </asp:TemplateField>
      <%--Upgrade Type--%>
      <asp:TemplateField HeaderText="Type">
        <ItemTemplate>
          <asp:Label runat="server" ID="labUpgradeType" />
        </ItemTemplate>
      </asp:TemplateField>

      <asp:BoundField DataField="ActivationDate" HeaderText="Activation" />
    </Columns>
    <EmptyDataTemplate>
      You have no incomplete fields with unreported acreage.
    </EmptyDataTemplate>
  </asp:GridView>
  </div>

  <br />

  <asp:Panel runat="server" ID="panDataTablesScriptContainer">

    <script type="text/javascript" charset="utf-8">

      $(document).ready(function () {

        $('.test').dataTable(
        {
          /* we're using JQueryUI for our theme */
          "bJQueryUI": true,
          /* we want to use full numbers in our paging controls */
          "sPaginationType": "full_numbers"
          /* we want to sort all columns from left to right, array is zero-based */
     /*     "aaSorting": [[0, "asc"], [1, "asc"], [2, "asc"], [3, "asc"], [4, "asc"]],*/
          /* ensure checkbox and 'action' columns aren't sortable or filterable */
   /*       "aoColumnDefs": [
                            { "bSearchable": false, "bVisible": true, "bSortable": false, "aTargets": [5] }
          ],*/
          /* change the default 'search label' to 'filter' */
      /*    "oLanguage": {
            "sSearch": "Filter reports:"
          },*/
          /* save the state of controls */
        /*  "bStateSave": true */
        });

      });
    </script>
  </asp:Panel>
  
</asp:Content>

