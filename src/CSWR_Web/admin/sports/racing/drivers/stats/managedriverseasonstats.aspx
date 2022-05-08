<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" 
  CodeFile="managedriverseasonstats.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.Admin.Sheets.ManageDriverSeasonStats" %>
<%@ Register Src="~/admin/usercontrols/SuppSheetManager.ascx" TagName="SuppSheetManager" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">


<div class="manageDriverSeasonStats">

  <h2>Manage Driver Season Stats</h2>

  <%--Select Season--%>
  <asp:DropDownList runat="server" ID="ddlSeasons"  DataTextField="Name" AutoPostBack="true" 
    DataValueField="SeasonCode" ondatabound="ddlSeasons_DataBound" 
    onselectedindexchanged="ddlSeasons_SelectedIndexChanged"/>

  <%--Add Stats--%>
  <div class="buttonContainer" style="margin:7px 0px 5px 2px;">
    <asp:LinkButton runat="server" ID="lbAddStat" onclick="lbAddStat_Click">
      <asp:Image runat="server" ImageUrl="~/images/icons/add.gif" />Add Stat
    </asp:LinkButton>
    <p style="margin:10px 0px 0px 0px;">
      <asp:Label runat="server" ID="labNoStats" Visible="false" Text="No stats configured." />
    </p>
  </div>

  <asp:GridView runat="server" ID="gvDrivers" AutoGenerateColumns="False" Width="850" 
    CssClass="standardGrid" SkinID="Professional" DataKeyNames="PlayerID"  
    onrowdatabound="gvDrivers_RowDataBound" 
    onrowupdating="gvDrivers_RowUpdating" ondatabound="gvDrivers_DataBound" onrowcommand="gvDrivers_RowCommand" 
    onrowcreated="gvDrivers_RowCreated" 
    onrowcancelingedit="gvDrivers_RowCancelingEdit" 
    onrowdeleting="gvDrivers_RowDeleting" onrowediting="gvDrivers_RowEditing">
    <Columns>
      <%--Name--%>
      <asp:TemplateField HeaderText="Name">
        <ItemStyle Width="200" />
        <ItemTemplate>
          <asp:Label runat="server" ID="labName" />
        </ItemTemplate>
        <EditItemTemplate>
          <asp:DropDownList runat="server" ID="ddlDriverName" DataValueField="PlayerID" DataTextField="FullNameLastFirst" />
          <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDriverName" InitialValue="0" 
            ErrorMessage="Driver Name is required." ToolTip="Driver Name is required.">*</asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>   
      <%--Rank--%>   
      <asp:TemplateField HeaderText="Rank">
        <ItemTemplate>
          <asp:Label runat="server" ID="labRank" />
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="tbRank" Width="30"/>
          <asp:RequiredFieldValidator runat="server" ControlToValidate="tbRank" 
            ErrorMessage="Rank is required." ToolTip="Rank is required.">*</asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>
      <%--Points--%>   
      <asp:TemplateField HeaderText="Points">
        <ItemTemplate>
          <asp:Label runat="server" ID="labPoints" />
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="tbPoints" Width="30"/>
          <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPoints" 
            ErrorMessage="Points are required." ToolTip="Points are required.">*</asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>
      <%--Behind--%>   
      <asp:TemplateField HeaderText="Behind">
        <ItemTemplate>
          <asp:Label runat="server" ID="labBehind" />
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="tbBehind" Width="30"/>
          <asp:RequiredFieldValidator runat="server" ControlToValidate="tbBehind" 
            ErrorMessage="Behind is required." ToolTip="Behind is required.">*</asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>
      <%--Starts--%>   
      <asp:TemplateField HeaderText="Starts">
        <ItemTemplate>
          <asp:Label runat="server" ID="labStarts" />
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="tbStarts" Width="20"/>
          <asp:RequiredFieldValidator runat="server" ControlToValidate="tbStarts" 
            ErrorMessage="Starts is required." ToolTip="Starts is required.">*</asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>
      <%--Poles--%>   
      <asp:TemplateField HeaderText="Poles">
        <ItemTemplate>
          <asp:Label runat="server" ID="labPoles" />
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="tbPoles" Width="20"/>
          <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPoles" 
            ErrorMessage="Poles are required." ToolTip="Poles are required.">*</asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>
      <%--Wins--%>   
      <asp:TemplateField HeaderText="Wins">
        <ItemTemplate>
          <asp:Label runat="server" ID="labWins" />
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="tbWins" Width="20"/>
          <asp:RequiredFieldValidator runat="server" ControlToValidate="tbWins" 
            ErrorMessage="Wins are required." ToolTip="Wins are required.">*</asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>
      <%--TP5--%>   
      <asp:TemplateField HeaderText="TP5">
        <ItemTemplate>
          <asp:Label runat="server" ID="labTop5Finishes" />
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="tbTop5Finishes" Width="20"/>
          <asp:RequiredFieldValidator runat="server" ControlToValidate="tbTop5Finishes" 
            ErrorMessage="TP5 is required." ToolTip="TP5 is required.">*</asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>
      <%--TP10--%>   
      <asp:TemplateField HeaderText="TP10">
        <ItemTemplate>
          <asp:Label runat="server" ID="labTop10Finishes" />
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="tbTop10Finishes" Width="20"/>
          <asp:RequiredFieldValidator runat="server" ControlToValidate="tbTop10Finishes" 
            ErrorMessage="TP10 is required." ToolTip="TP10 is required.">*</asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>
      <%--Winnings--%>   
      <asp:TemplateField HeaderText="Winnings">
        <ItemTemplate>
          <asp:Label runat="server" ID="labWinnings" />
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="tbWinnings" Width="60"/>
          <asp:RequiredFieldValidator runat="server" ControlToValidate="tbWinnings" 
            ErrorMessage="Winnings are required." ToolTip="Winnings are required.">*</asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>
      <%--AFP--%>   
      <asp:TemplateField HeaderText="AFP">
        <ItemTemplate>
          <asp:Label runat="server" ID="labAverageFinishPosition" />
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="tbAverageFinishPosition" Width="30"/>
          <asp:RequiredFieldValidator runat="server" ControlToValidate="tbAverageFinishPosition" 
            ErrorMessage="AFP is required." ToolTip="AFP is required.">*</asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>
      <%--ADP--%>   
      <asp:TemplateField HeaderText="ADP">
        <ItemTemplate>
          <asp:Label runat="server" ID="labADP" />
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="tbADP" Width="30"/>
        </EditItemTemplate>
      </asp:TemplateField>
      <%--Actions--%>       
      <asp:TemplateField>
        <ItemTemplate>
          <asp:ImageButton runat="server" id="ibAdd" CommandName="Insert" ToolTip="Click to add this phone to your collection." ImageUrl="~/images/gridviewbuttons/Add.gif" OnCommand="ibAdd_Command"/>
          <asp:ImageButton runat="server" ID="ibUpdate" CommandName="Update" ToolTip="Click to update this phone." ImageUrl="~/images/gridviewbuttons/Update.GIF" />
          <asp:ImageButton runat="server" ID="ibCancel" CommandName="Cancel" ToolTip="Click to cancel." ImageUrl="~/images/gridviewbuttons/Cancel.GIF" CausesValidation="false" />
          <asp:ImageButton runat="server" ID="ibEdit" CommandName="Edit" ToolTip="Click to edit this phone." ImageUrl="~/images/gridviewbuttons/Edit.GIF" />
          <asp:ImageButton runat="server" id="ibDelete" CommandName="Delete" ToolTip="Click to delete this phone." ImageUrl="~/images/gridviewbuttons/Delete.gif" OnClientClick="return window.confirm('Are you sure you want to delete this phone?');" />
        </ItemTemplate>
      </asp:TemplateField>
    </Columns>
  </asp:GridView>
  
  


</div>

</asp:Content>

