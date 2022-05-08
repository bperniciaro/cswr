<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SupplementalReviewEditor.ascx.cs" Inherits="BP.CheatSheetWarRoom.UI.SupplementalReviewEditor" %>

<%--Sport--%>
<p>
  <asp:Label runat="server" ID="labSport" />
</p>
<%--SportSeason--%>
<p>
  <asp:DropDownList runat="server" ID="ddlSeason" DataTextField="SeasonCode" DataValueField="SeasonCode" AutoPostBack="true"
    onselectedindexchanged="ddlSeason_SelectedIndexChanged" />
</p>
<%--Supplemental Source--%>
<p>
  <asp:DropDownList runat="server" ID="ddlSupplementalSource"  AutoPostBack="true" DataTextField="Name" 
    DataValueField="SupplementalSourceID" onselectedindexchanged="ddlSupplementalSource_SelectedIndexChanged" />
</p>

<%--Add Stats--%>
<div class="buttonContainer" style="margin:7px 0px 5px 2px;">
  <asp:LinkButton runat="server" ID="lbAddReview" onclick="lbAddReview_Click">
    <asp:Image runat="server" ImageUrl="~/images/icons/add.gif" />Add Review
  </asp:LinkButton>
</div>


<%--Grid for entering review URLs--%>
<asp:GridView runat="server" ID="gvSupplementalReviews" AutoGenerateColumns="false"
  onrowdatabound="gvSupplementalReviews_RowDataBound" CssClass="standardGrid" SkinID="Professional" DataKeyNames="PlayerID"  
  ondatabound="gvSupplementalReviews_DataBound" 
  onrowcancelingedit="gvSupplementalReviews_RowCancelingEdit" 
  onrowcommand="gvSupplementalReviews_RowCommand" 
  onrowcreated="gvSupplementalReviews_RowCreated" 
  onrowdeleting="gvSupplementalReviews_RowDeleting" 
  onrowediting="gvSupplementalReviews_RowEditing" 
  onrowupdating="gvSupplementalReviews_RowUpdating">
    <Columns>
      <%--Name--%>
      <asp:TemplateField HeaderText="Name">
        <ItemStyle Width="200" />
        <ItemTemplate>
          <asp:Label runat="server" ID="labName" />
        </ItemTemplate>
        <EditItemTemplate>
          <asp:DropDownList runat="server" ID="ddlDrivers" DataValueField="PlayerID" DataTextField="FullNameLastFirst" />
          <asp:RequiredFieldValidator runat="server" ID="rfvDriver" ControlToValidate="ddlDrivers" ErrorMessage="Driver is required" ToolTip="Driver is required.">*</asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>   
      <%--Rank--%>   
      <asp:TemplateField HeaderText="URL">
        <ItemTemplate>
          <asp:Label runat="server" ID="labURL" />
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox runat="server" ID="tbReviewURL" Width="600"/>
          <asp:RequiredFieldValidator runat="server" ControlToValidate="tbReviewURL" 
            ErrorMessage="Rank is required." ToolTip="URL is required.">*</asp:RequiredFieldValidator>
        </EditItemTemplate>
      </asp:TemplateField>
      <%--Actions--%>       
      <asp:TemplateField>
        <ItemTemplate>
          <asp:ImageButton runat="server" id="ibAdd" CommandName="Insert" ToolTip="Click to add this review." ImageUrl="~/images/gridviewbuttons/Add.gif" OnCommand="ibAdd_Command"/>
          <asp:ImageButton runat="server" ID="ibUpdate" CommandName="Update" ToolTip="Click to update this review." ImageUrl="~/images/gridviewbuttons/Update.GIF" />
          <asp:ImageButton runat="server" ID="ibCancel" CommandName="Cancel" ToolTip="Click to cancel." ImageUrl="~/images/gridviewbuttons/Cancel.GIF" CausesValidation="false" />
          <asp:ImageButton runat="server" ID="ibEdit" CommandName="Edit" ToolTip="Click to edit this review." ImageUrl="~/images/gridviewbuttons/Edit.GIF" />
          <asp:ImageButton runat="server" id="ibDelete" CommandName="Delete" ToolTip="Click to delete this review." ImageUrl="~/images/gridviewbuttons/Delete.gif" OnClientClick="return window.confirm('Are you sure you want to delete this phone?');" />
        </ItemTemplate>
      </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
      No Reviews Available
    </EmptyDataTemplate>
</asp:GridView>