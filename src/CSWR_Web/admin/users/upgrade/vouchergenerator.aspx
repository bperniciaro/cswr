<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="vouchergenerator.aspx.cs" 
    Inherits="BP.CheatSheetWarRoom.UI.Admin.Users.VoucherGenerator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

  <h1>Voucher Generator</h1>

  <h2>Criteria</h2>

  <table>
    <%--Number of Vouchers--%>
    <tr>
      <td>
        #Vouchers 
      </td>
      <td>
        <asp:TextBox runat="server" ID="tbVoucherCount" MaxLength="2" />
        <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="tbVoucherCount" ErrorMessage="Value must be a whole number" />      
      </td>
    </tr>
    <%--Campaign Tag--%>
    <tr>
      <td>
        Campaign Tag 
      </td>
      <td>
        <asp:TextBox runat="server" ID="tbCampaignTag" MaxLength="32" />
      </td>
    </tr>
    <%--Submit Button--%>
    <tr>
      <td colspan="2">
        <asp:Button runat="server" ID="butGenerateVouchers" Text="Generate Vouchers" OnClick="butGenerateVouchers_Click" />
      </td>
    </tr>
  </table>

  <br />

  <h2>Voucher Codes</h2>
  <asp:Label runat="server" ID="labVoucherCodes" />

</asp:Content>

