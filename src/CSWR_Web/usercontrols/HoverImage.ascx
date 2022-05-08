<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HoverImage.ascx.cs" Inherits="BP.CheatSheetWarRoom.UI.UserControls.HoverImage" %>


<div class="hoverImageControl">  
  <div>
    <a class="thumb">
      <asp:Image runat="server" ID="imaSmallImage"/>
	    <span>
	      <asp:Image runat="server" ID="imaBigImage"/>
	      <br />
        <asp:Literal runat="server" ID="litCaptionText" />
	    </span>
    </a>
  </div>
  <span class="caption">
    mouse-over to enlarge 
  </span>
</div>