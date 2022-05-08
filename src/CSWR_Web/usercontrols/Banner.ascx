<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Banner.ascx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.NewBanner" %>

  <!--             -->
  <!--   Banner    -->
  <!--             -->
  <div id="banner">
    <div class="innertube">
    
      <!-- Logo -->
      <div class="logoContainer">
        <asp:HyperLink runat="server" ID="hlLogoLink">
          <asp:Image runat="server" ImageUrl="~/Images/Layout/blogsizedlogo.gif" AlternateText="Fantasy cheat sheets for fantasy football and fantasy racing." />
        </asp:HyperLink>
      </div>

      <!-- standard banner navigation -->      
      <div id="secondaryNav">
        <ul>

          <%--Unauthenticated Users--%>

          <%--Join Us--%>
          <li runat="server" id="liJoinUs"> 
            <i class="fa fa-check-square-o"></i>
            <asp:HyperLink runat="server" id="hlJoinUs" NavigateUrl="~/access/register.aspx">Join us now!</asp:HyperLink>
          </li>
          <%--Login--%>
          <li runat="server" id="liLogin"> 
            <i class="fa fa-sign-in"></i>
            <asp:HyperLink runat="server" id="hlLogin">Log in 
<%--              /
              <i class="fa fa-facebook-square"></i>
              <i class="fa fa-twitter-square"></i>
              <i class="fa fa-google-plus-square"></i>--%>
            </asp:HyperLink>
          </li>

          <%--Administration--%>
          <li runat="server" id="liAdminItem" visible="false">
            <asp:HyperLink runat="server" NavigateUrl="~/admin/dashboard.aspx">
              <i class="fa fa-wrench"></i> 
              Admin
            </asp:HyperLink>
          </li>
          <%--Moderator--%>
          <li runat="server" id="liModItem" visible="false"> 
            <asp:HyperLink runat="server" NavigateUrl="~/admin/mods/managefooplayers.aspx">
              <i class="fa fa-wrench"></i> 
              Moderator
            </asp:HyperLink>
          </li>
          <%--Public Profile--%>
          <li runat="server" id="liPublicProfile"> 
            <asp:HyperLink runat="server" id="hlPublicProfile" NavigateUrl="~/editprofile.aspx">
              <i class="fa fa-user"></i> 
              <asp:Literal runat="server" ID="litProfileLink" />
            </asp:HyperLink>
          </li>
          <%--User Control Panel--%>
          <%--<li runat="server" id="liUserControlPanel"> 
            <asp:HyperLink runat="server" id="hlUserControlPanel">
              <i class="fa fa-cog"></i> 
              User Control Panel
              <asp:Literal runat="server" ID="litControlPanel" />
            </asp:HyperLink>
          </li>--%>
          <%--Logout--%>
          <li runat="server" id="liLogoutItem">
            <asp:LinkButton runat="server" ID="lbBannerLogout" onclick="lbBannerLogout_Click" CausesValidation="false" EnableViewState="false">
              <i class="fa fa-sign-out"></i> Log out
            </asp:LinkButton>
          </li>
        </ul>
      </div> <!-- close secondaryNav -->

    </div>  <!-- close innerTube -->
  </div>  <!-- close banner -->