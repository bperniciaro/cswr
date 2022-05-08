<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Frame.master" AutoEventWireup="true" CodeFile="ASPPQuery.aspx.cs" Inherits="test_Forum_Queryforum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

<style>
.msg a {
    color: inherit;
    text-decoration: none;
}
.messagelink {text-shadow:none;}
</style>

<div class="left overflowHidden" style="width: 49%; padding-right: 1%;">
<h3>Most Recent Discussions</h3>
<div id="new">&nbsp;</div>
</div>
<div class="left overflowHidden" style="width: 49%;">
<h3>Active / Hot Discussions</h3>
<div id="active">&nbsp;</div>
</div>
<script type="text/javascript">// <![CDATA[
  $(document).ready(function () {
    alert("1");
    $('#new').load('../../forum/ws/TopNav.aspx?t=RecentPosts .newActiveList');    
    alert("2");
    $('#active').load('../../forum/ws/TopNav.aspx?t=Active .newActiveList');
    alert("3");
  });
// ]]></script>

</asp:Content>

