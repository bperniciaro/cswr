<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="managesupplementalreviews.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.ManageSupplementalReviews" %>
<%@ Register Src="~/admin/usercontrols/SupplementalReviewEditor.ascx" TagPrefix="cswr" TagName="SupplementalReviewEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminContent" Runat="Server">


   <cswr:SupplementalReviewEditor runat="server" ID="sreSupplementalReviewEditor" SportCode="RAC" />


</asp:Content>

