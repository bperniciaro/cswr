//Bismillah


(function( $ ) {
 
  $.fn.CevherLink=function(options) {
 
        var element=$(this);    
        checkCookie(element);
          var defaults = {
          	 icon_src:'[x]',
             minimize:"[-]",
             maximize:"[+]",
             href: 'none',
             text:"",
             border:'1px black solid',
             corner:'0px',
             text_color:"black",
             time:false, 
             text_position:"top",
             text_font:"Arial",
             text_weight:"normal",
             background:"#fff",
             size:"14px",
             position:"bottom-rigth",
             popup_width:"300",
             popup_height:"55" ,
             language:"en_US",
             timer:10, 
             pause:"[ii]",
             start:"[>]",            
             middle_html:"",
             cookie_day:"1"     
           };
           var opts = $.extend(defaults, options);
           
      
           if(opts.popup_width<300)
           opts.popup_width=300;
           if(opts.popup_height<55)
           opts.popup_height=55;
           
           //boyuk divin parametrleri
           element.css({
            "position":"fixed",
            "z-index":"999999999",
            "width":opts.popup_width,
            "min-height":opts.popup_height,
            "border":opts.border,
            "background":opts.background,
            "border-radius":opts.corner,
            "-moz-border-radius": opts.corner,
            "azimuth-webkit-border-radius": opts.corner,
            "-khtml-border-radius": opts.corner,
            "overflow": "hidden",
            "visibility":"hidden"
            
            });
            
           if(opts.position!="bottom-right"&&opts.position!="top-right"&&opts.position!="bottom-left"&&opts.position!="top-left")  
           {
           opts.position="bottom-right";
           }
           
var iframe= "<iframe id='cevherlink' src=\"http://www.facebook.com/plugins/likebox.php?locale="+opts.language+"&href="+opts.href+"&amp;width=292&amp;colorscheme=light&amp;show_faces=false&amp;border_color&amp;stream=false&amp;header=false&amp;height=88\" scrolling=\"no\" frameborder=\"0\" style=\"border:none; overflow:hidden; width:292px; min-height:62px; max-height:90px;backgorund:#ccc;\" allowTransparency=\"true\"></iframe>";     
var text="<div id='cevher_text'>"+opts.text+"</div>";
var close="<div id='cevher_close'></div>";
var minimize="<div id='cevher_minimize'></div>";
var maximize="<div id='cevher_maximize'></div>";
var pause="<div id='cevher_pause' check='1' style='position:absolute;bottom:5px;right:5px;cursor:pointer;color:#555'></div>";
var start="<div id='cevher_start' check='0' style='position:absolute;bottom:5px;right:5px;display:none;cursor:pointer;color:#555'></div>";
var extra_iframe="<div id='cevher_middle'>"+opts.middle_html+"</div>";
var bottom_image="<div id='cevher_link_timer' style='position:absolute;bottom:3px;right:30px;'>"+opts.timer+"</div>";

element.html(text);
element.find("#cevher_text").css({
    
    "max-width":opts.popup_width,
    "max-height":opts.popup_height,
    "color":opts.text_color,
    "margin":"2px auto",
    "font-size":opts.size,
    "font-family":opts.text_font,
    "font-weight":opts.text_weight,
    "padding":"4px 30px 4px 4px"
});


if(opts.text_position=="bottom"){
  $(iframe+extra_iframe+close+minimize+maximize).insertBefore('#cevher_text'); 
   element.append(bottom_image+start+pause);  
}

else{
element.append(extra_iframe+iframe+bottom_image+start+pause+close+minimize+maximize);    
}

element.find("#cevher_close").css({
  "position":"absolute",
  "right":"10px",
  "top":"5px",
  "cursor":"pointer"
});

element.find("#cevher_minimize").css({
  "position":"absolute",
  "right":"30px",
  "top":"5px",
  "cursor":"pointer",
  "z-index":"9999"
});
element.find("#cevher_maximize").css({
  "position":"absolute",
  "right":"10px",
  "top":"30px",
  "cursor":"pointer",
  "display":"none",
  "z-index":"99999"
});
 $("#cevherlink,#cevher_middle").load(
 function(){ 
    

          var count = opts.timer;
          
          if(!count||count==0||isInteger(count)==0)
          oley=0;
          else{
            
          oley=1;
var interval = setInterval(timedCount, 1000);}

    if(oley==0){
            element.find("#cevher_pause,#cevher_start,#cevher_link_timer").css({"display":"none"});
            
    }

    
    var def_width=element.outerWidth();
    var def_height=element.outerHeight();
   checkImage(opts.icon_src,"cevher_close",defaults.icon_src,element);
   checkImage(opts.minimize,"cevher_minimize",defaults.minimize,element);
   checkImage(opts.maximize,"cevher_maximize",defaults.maximize,element);
   
   checkImage(opts.pause,"cevher_pause",defaults.pause,element);
   checkImage(opts.start,"cevher_start",defaults.start,element);
  
   element.css({"visibility":"visible"});
            var uz=-element.outerWidth();
  
            var pos=opts.position;
            var x=pos.substring(pos.indexOf("-")+1,pos.length);
            var y=pos.substring(0,pos.indexOf("-"));
            if(y=="bottom")
         element.css({"bottom":"20px"}); 
            if(y=="top")
         element.css({"top":"20px"}); 
            
            if(x=="right")
            {
            element.css({"right":uz}).animate({
                "right":"50px"
            },500).animate({
           "right":"10px"
            },500).animate({
           "right":"30px"
            },500);           
            }
         
            if(x=="left")
            {
            element.css({"left":uz}).animate({
                "left":"50px"
            },500).animate({
           "left":"10px"
            },500).animate({
           "left":"30px"
            },500);         
            }
       
         element.find("#cevher_close").live({
        
            click:function(){ 
               
              username=Math.floor(Math.random()*11);
  if (username!=null && username!="")
    {
    setCookie("username",username,opts.cookie_day);
    }
                 if(x=="right")
            {
            element.animate({
                "right":"50px"
            },500).animate({
           "right":uz
            },500,function(){
                element.remove();
            });            
            }
         
            if(x=="left")
            {
            element.animate({
                "left":"50px"
            },500).animate({
           "left":uz
            },500,function(){
                element.remove();
            });         
            }
          //  
            }
         }
                  
         ); 
         
         
         element.find("#cevher_minimize").live(
         {
            click:function(){ 
                
                if(oley==1){
                clearInterval(interval);}
        
              element.find("#cevher_text,#cevher_middle,#cevher_link_timer,#cevherlink,#cevher_minimize,#cevher_pause,#cevher_start").animate({"opacity":"0"}, { queue:true, duration:500 });
              element.animate({"height":"60px","width":"35px"},500,function(){
        
                
                element.find("#cevher_maximize").css({"display":"block"});
          element.find("#cevher_text,#cevher_middle,#cevher_link_timer,#cevherlink,#cevher_minimize,#cevher_pause,#cevher_start").css({"display":"none"});
              });
            }
         }
         );
         
         element.find("#cevher_maximize").live(
         {
            click:function(){
          
        
                 
              element.find("#cevher_text,#cevher_middle,#cevherlink,#cevher_minimize").css({"display":"block"}).animate({"opacity":"1"}, { queue:true, duration:500 });
              element.animate({"height":def_height+"px","width":def_width+"px"},500,function(){
                   if(oley==1){  
          if(element.find("#cevher_pause").attr("check")=='1'){
                     
  
       interval = setInterval(timedCount, 1000);
       element.find("#cevher_pause,#cevher_link_timer").css({"display":"block"});
                }
                else
                 element.find("#cevher_start,#cevher_link_timer").css({"display":"block"});
                element.find("#cevher_pause,#cevher_start,#cevher_link_timer,"). css({"opacity":"1"});
                 }
                element.find("#cevher_maximize").css({"display":"none"});
         
              });
            }
         }
         ); 
         
 

function timedCount(e) {
    if(oley==1){
   count--;
   element.find("#cevher_link_timer").text(count);
   if (count == 0) {
       
      clearInterval(interval);  // Stops the timer
           if(x=="right")
            {
            element.animate({
                "right":"50px"
            },500).animate({
           "right":uz
            },500,function(){
                element.remove();
            });            
            }
         
            if(x=="left")
            {
            element.animate({
                "left":"50px"
            },500).animate({
           "left":uz
            },500,function(){
                element.remove();
            });         
            }
   }}
}
   element.find("#cevher_pause").live('click',function() {
    

   
        clearInterval(interval);
       element.find("#cevher_pause").css({"display":"none"}).attr({"check":"0"});
       element.find("#cevher_start").css({"display":"block"}).attr({"check":"1"});
       
});      
         
 element.find("#cevher_start").live('click',function() {
    

   
       
  
       interval = setInterval(timedCount, 1000);
       element.find("#cevher_pause").css({"display":"block"}).attr({"check":"1"});
       element.find("#cevher_start").css({"display":"none"}).attr({"check":"0"}); 
       
});
 }
 );
};
 function getCookie(c_name)
{
var i,x,y,ARRcookies=document.cookie.split(";");
for (i=0;i<ARRcookies.length;i++)
  {
  x=ARRcookies[i].substr(0,ARRcookies[i].indexOf("="));
  y=ARRcookies[i].substr(ARRcookies[i].indexOf("=")+1);
  x=x.replace(/^\s+|\s+$/g,"");
  if (x==c_name)
    {
    return unescape(y);
    }
  }
}

 function addDays(theDate, days) {
   return new Date(theDate.getTime() + days * 24 * 60 * 60 * 1000);
 }

function setCookie(c_name,value,exdays) {

  //var test = addDays(new Date(), 5);

  var exdate = addDays(new Date(), exdays)
  var c_value=escape(value) + ((exdays==null) ? "" : "; expires="+exdate.toUTCString());
  document.cookie=c_name + "=" + c_value;
}

  //function setCookie(c_name, value, exdays) {
  //  var exdate = new Date();
  //  exdate.setDate(exdate.getDate() + exdays);
  //  var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
  //  document.cookie = c_name + "=" + c_value;
  //}

function checkCookie(element)
{
var username=getCookie("username");
if (username!=null && username!="")
  {
  
  element.remove();
  
  }
return false;
}
  function isInteger(s) {
  return (s.toString().search(/^-?[0-9]+$/) == 0);
}
    function checkImage(src,id,icon,element) {
  var img = new Image();
  img.onload = function() {
      var image="<img src='"+src+"' width='16' height='16'/>";
     $("#"+id).html(image);  
  };
  img.onerror = function() {
   
     element.find("#"+id).html(icon);  
  };

  img.src = src; // fires off loading of image
}

})(jQuery);