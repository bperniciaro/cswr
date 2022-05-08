<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="manageexceptions.aspx.cs" 
Inherits="BP.CheatSheetWarRoom.UI.Admin.Health.ManageExceptions" Title="Manage Exceptions - Administration" %>
<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<%@ Register Src="~/usercontrols/MessageBox.ascx" TagName="MessageBox" TagPrefix="cswr" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">

<cswr:MessageBox runat="server" ID="mbResult" />

<asp:Button runat="server" ID="butDeleteAllExceptions" 
    Text="Delete All Exceptions" OnClientClick="return confirm('Are you sure?');" 
    onclick="butDeleteAllExceptions_Click" />


<asp:GridView ID="EventLog" runat="server" AllowPaging="True" AllowSorting="True" SkinID="Professional" AutoGenerateColumns="False" PageSize="50"  
  DataKeyNames="EventId" DataSourceID="EventLogDataSource" OnRowDataBound="EventLog_RowDataBound" Width="100%">
  <Columns>
    <asp:TemplateField HeaderText="Time">
      <ItemTemplate>
        <asp:Label runat="server" ID="labEventTime" style="white-space:nowrap;" />
      </ItemTemplate>
    </asp:TemplateField>
    <%--<asp:BoundField DataField="EventTime" HeaderText="Time" SortExpression="EventTime"  />--%>
    <asp:BoundField DataField="EventCode" HeaderText="Code" SortExpression="EventCode" />
    <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" />
    <asp:BoundField DataField="RequestUrl" HeaderText="Url" SortExpression="RequestUrl" />
    <asp:CommandField SelectText="Details" ShowSelectButton="True" />
  </Columns>
</asp:GridView>

<asp:DetailsView ID="LogEntryDetails" runat="server" AutoGenerateRows="False" 
    SkinID="Professional" DataKeyNames="EventId" 
    DataSourceID="EventLogDetailsDataSource" EnableViewState="False" 
    ondatabound="LogEntryDetails_DataBound" 
    ondatabinding="LogEntryDetails_DataBinding">
  <Fields>
    <asp:BoundField DataField="RequestUrl" HeaderText="RequestUrl" SortExpression="RequestUrl" />

    <asp:TemplateField>
      <ItemTemplate>
        <asp:Literal runat="server" ID="litDetails" />
      </ItemTemplate>
    </asp:TemplateField>

    <asp:BoundField DataField="EventId" HeaderText="EventId" ReadOnly="True" SortExpression="EventId" />
    <asp:BoundField DataField="EventTimeUtc" HeaderText="EventTimeUtc" SortExpression="EventTimeUtc" />
    <asp:BoundField DataField="EventTime" HeaderText="EventTime" SortExpression="EventTime" />
    <asp:BoundField DataField="EventType" HeaderText="EventType" SortExpression="EventType" />
    <asp:BoundField DataField="EventSequence" HeaderText="EventSequence" SortExpression="EventSequence" />
    <asp:BoundField DataField="EventOccurrence" HeaderText="EventOccurrence" SortExpression="EventOccurrence" />
    <asp:BoundField DataField="EventCode" HeaderText="EventCode" SortExpression="EventCode" />
    <asp:BoundField DataField="EventDetailCode" HeaderText="EventDetailCode" SortExpression="EventDetailCode" />
    <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" />
    <asp:BoundField DataField="ApplicationPath" HeaderText="ApplicationPath" SortExpression="ApplicationPath" />
    <asp:BoundField DataField="ApplicationVirtualPath" HeaderText="ApplicationVirtualPath" SortExpression="ApplicationVirtualPath" />
    <asp:BoundField DataField="MachineName" HeaderText="MachineName" SortExpression="MachineName" />
    <asp:BoundField DataField="ExceptionType" HeaderText="ExceptionType" SortExpression="ExceptionType" />

  </Fields>
</asp:DetailsView>



<asp:SqlDataSource ID="EventLogDetailsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"
  SelectCommand="SELECT EventId, EventTimeUtc, EventTime, EventType, EventSequence, EventOccurrence, EventCode, EventDetailCode, Message, ApplicationPath, ApplicationVirtualPath, MachineName, RequestUrl, ExceptionType, Details FROM aspnet_WebEvent_Events WHERE (EventId = @EventId)">
  <SelectParameters>
    <asp:ControlParameter ControlID="EventLog" Name="EventId" PropertyName="SelectedValue" />
  </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="EventLogDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"
  SelectCommand="SELECT * FROM aspnet_WebEvent_Events ORDER BY EventTime DESC">
</asp:SqlDataSource>

</asp:Content>

