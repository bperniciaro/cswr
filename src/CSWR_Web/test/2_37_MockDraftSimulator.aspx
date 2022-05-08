<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Frame.master" AutoEventWireup="true" CodeFile="2_37_MockDraftSimulator.aspx.cs" Inherits="temp_2_37_MockDraftSimulator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

  <div><style>.cswlink{color:#08c}.cswlink:hover{color: #005580;cursor:pointer;}</style><div style="float:left;margin-right:30px;margin-bottom:20px;text-align:left;"><div style="color:#000;font-size:20px;font-weight:800;">MOCK DRAFT SIMULATOR</div>
<div style="font-size:13px;margin:10px 0 20px 30px;text-align:left;color:#212121"><img src="//cdn.fantasypros.com/csw/images/checkmark_blue.png" style="height:13px;width:13px;border:0;vertical-align:-2px;margin:0 10px;"/>Instant mock drafts<br/><img src="//cdn.fantasypros.com/csw/images/checkmark_blue.png" style="height:13px;width:13px;border:0;vertical-align:-2px;margin:0 10px;"/>Custom league settings<br/><img src="//cdn.fantasypros.com/csw/images/checkmark_blue.png" style="height:13px;width:13px;border:0;vertical-align:-2px;margin:0 10px;"/>Expert pick suggestions</div>
<img onclick="launchMock()" class="cswlink" style="margin-left:5px;border:0;width:250px;" src="//cdn.fantasypros.com/csw/images/CTA_Start_Your_Draft_smaller.png"/>
<script>
function closeMock(autoStart){document.body.removeChild(document.getElementById('cswOverlay'));}
function launchMock(autoStart){
var windowH = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;
var scrollTop = document.body.scrollTop || document.documentElement.scrollTop;
var divY=scrollTop ? scrollTop + 30 : 30;
var div=document.createElement("div");
div.id='cswOverlay';div.style.zIndex="2147483647";div.style.position="absolute";div.style.left="5%";div.style.top=(divY +30)+"px";div.style.width="90%";div.style.height=""+(windowH-80)+"px";div.style.background="lightgray";div.style.color="white";div.style.padding="30px 5px 5px 5px";
var cswHTML="<"+"span style='position:absolute;left:20px;top:7px;font-size: 16px;color: black;font-weight:bold;'>DRAFT SIMULATOR";
cswHTML+="<"+"span style='margin-left:10px;font-size: 12px;color: black;font-weight:bold'>by FantasyPros<"+"/"+"span>";
    cswHTML += "<" + "a" +" style='margin-left:100px;font-size: 14px;color: blue;font-weight:bold;vertical-align:1px' href='https://www.cheatsheetwarroom.com/blog/go/fantasypros-sub' target='_blank' rel='noindex nofollow' ";
cswHTML+="title='Want to get this advice during your real draft? You can!'>";
cswHTML+="Use this tool during your real draft! Learn more »<"+"/"+"a><"+"/"+"span>";
cswHTML+="<"+"a"+" style='position:absolute;right:20px;top:7px;font-size: 14px;color: black;font-weight:bold;' href='javascript:void(0)' onclick='closeMock()'>CLOSE<"+"/"+"a>";
cswHTML+="<"+"iframe border='0' style='border:0;' width='100%' height='100%' src='https://draftwizard.fantasypros.com/simulator/start.jsp?sport=nfl&iframe=overlay&partner=WarRoom";
if(autoStart){cswHTML+='&autoStart=Y';}
cswHTML+="'><"+"/"+"iframe>";
div.innerHTML=cswHTML;
document.body.appendChild(div);
}</script>
</div>
<div style="float:left;width: 250px;text-align:center">
<img onclick="launchMock(true)" title="Start Mock" class="cswlink" style="border:0;width:250px" src="//cdn.fantasypros.com/csw/images/dw_mock_preview_nfl.png"/>
<div style="font-size:12px;margin-top:10px;margin-bottom:20px;color:#212121;;">powered by <a target="_blank" href="https://www.fantasypros.com/?partner=WarRoom" style="color:#2796DF;font-weight:bold">FantasyPros</div>
</div><div style="clear:both"></div>
</div>



</asp:Content>

