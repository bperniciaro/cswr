<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FooPlayerStatusManager.ascx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.UserControls.FooPlayerStatusManager" %>
<%@ Register Src="FooPlayerStatusEditor.ascx" tagName="FooPlayerStatusEditor" tagPrefix="cswr" %>

<%--<asp:ScriptManager runat="server"></asp:ScriptManager>--%>

<asp:UpdatePanel runat="server" id="upUpdatePanel">
  <ContentTemplate>

    <%--Control to add or edit a status--%>
    <cswr:FooPlayerStatusEditor runat="server" ID="fpseStatusEditor"/>
    
    <asp:Button runat="server" ID="butSaveStatus" Text="Create Status" OnClick="butCreateStatus_Click"/>
 
    <br/><br/>

    <%--Grid for viewing statuses--%>
    <h2>All Player Statuses</h2>

    <asp:GridView runat="server" id="gvPlayerStatuses" AutoGenerateColumns="False"
      onrowdatabound="gvPlayerStatuses_RowDataBound" >
      <Columns>
        <%--Full Name--%>
        <asp:TemplateField HeaderText="Full Name">
          <ItemTemplate>
            <asp:Label runat="server" ID="labFullName"/>
          </ItemTemplate>
        </asp:TemplateField>      
        <%--Status--%>
        <asp:TemplateField HeaderText="Status">
          <ItemTemplate>
            <asp:Label runat="server" ID="labStatus"/>
          </ItemTemplate>
        </asp:TemplateField>      
        <%--Supp Info--%>
        <asp:TemplateField HeaderText="Supp Info">
          <ItemTemplate>
            <asp:Label runat="server" ID="labSuppInfo"/>
          </ItemTemplate>
        </asp:TemplateField>      
        <%--Count--%>
        <asp:TemplateField HeaderText="Count">
          <ItemTemplate>
            <asp:Label runat="server" ID="labCount"/>
          </ItemTemplate>
        </asp:TemplateField>      
        <%--Created By--%>
        <asp:TemplateField HeaderText="Created By">
          <ItemTemplate>
            <asp:Label runat="server" ID="labCreatedBy"/>
          </ItemTemplate>
        </asp:TemplateField>      
        <%--Created Timestamp--%>
        <asp:TemplateField HeaderText="Created Timestamp">
          <ItemTemplate>
            <asp:Label runat="server" ID="labCreatedTimestamp"/>
          </ItemTemplate>
        </asp:TemplateField>      
        
        <%--Modified By--%>
        <asp:TemplateField HeaderText="Modified By">
          <ItemTemplate>
            <asp:Label runat="server" ID="labModifiedBy"/>
          </ItemTemplate>
        </asp:TemplateField>      
        <%--Created Timestamp--%>
        <asp:TemplateField HeaderText="Modified Timestamp">
          <ItemTemplate>
            <asp:Label runat="server" ID="labModifiedTimestamp"/>
          </ItemTemplate>
        </asp:TemplateField>      

        

      </Columns>
    </asp:GridView>

  </ContentTemplate>

</asp:UpdatePanel>



