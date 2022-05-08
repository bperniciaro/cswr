<%@ Page Title="Summary Stats" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="summarystats.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.Admin.SummaryStats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

<div class="summaryStatsPage">

  <h1>Summary Stats</h1>

  
  <h2>Live Stats</h2>

  <table style="width:800px;">
    <tr>
      <td>

        <h3>Feature Usage</h3>

        <strong>Sleeper usage percentage:</strong>  <asp:Label runat="server" ID="labSleeperUsagePercentage" />
        <br />
        <strong>Bust usage percentage:</strong>  <asp:Label runat="server" ID="labBustUsagePercentage" />
        <br />
        <strong>Note usage percentage:</strong>  <asp:Label runat="server" ID="labNoteUsagePercentage" />

      </td>
    </tr>

  </table>

  <br />


  <h2>Archived Yearly Stats</h2>

  <p style="color:#939393">These stats are calculated 'by hand' so that we can delete old cheat sheets when needed.</p>

  <%--Fantasy Football--%>
  <table class="archived">
    <%--Heading--%>
    <tr>
      <th colspan="13">Fantasy Football</th>
    </tr>
    <%--SubHeading--%>
    <tr>
      <th></th>
      <th colspan="6">
        Offseason (Jan 1st 2013 - Sep 4th 2013)
      </th>
      <th colspan="6">
        Inseason (Sep 5th 2013 - Dec 31st 2013)
      </th>
    </tr>
    <tr>
      <th></th>
      <th rowspan="2">Sleeper %</th>
      <th rowspan="2">Bust %</th>
      <th rowspan="2">Note %</th>
      <th colspan="3">Sheets Created</th>
      <th rowspan="2">Sleeper %</th>
      <th rowspan="2">Bust %</th>
      <th rowspan="2">Note %</th>
      <th colspan="3">Sheets Created</th>
    </tr>
    <tr>
      <th></th>
      <th>Users</th>
      <th>Standard</th>
      <th>PPR</th>
      <th>Users</th>
      <th>Standard</th>
      <th>PPR</th>
    </tr>
    <tr>
      <td>2013</td>
      <td>12%</td>
      <td>4%</td>
      <td>7%</td>
      <td>3135</td>
      <td>10196</td>
      <td>1280</td>
      <td>5.7%</td>
      <td>3.3%</td>
      <td>3.7%</td>
      <td>245</td>
      <td>234</td>
      <td>11</td>
    </tr>
  </table>

  <br />

  <%--Fantasy Racing--%>
  <table class="archived">
    <%--Heading--%>
    <tr>
      <th colspan="2">Fantasy Racing</th>
    </tr>
    <%--SubHeading--%>
    <tr>
      <th></th>
      <th>User Sheets</th>
    </tr>
    <tr>
      <td>2013</td>
      <td>64</td>
    </tr>
    <tr>
      <td>2014</td>
      <td></td>
    </tr>




  </table>

  <hr />

</div>


</asp:Content>

