<%@ Page Title="Generate a Fake Fantasy Football Cheat Sheet" Language="C#" MasterPageFile="~/MasterPages/ResponsiveTwoCol.master" 
  AutoEventWireup="true" CodeFile="fake-cheat-sheet.aspx.cs" Inherits="fantasy_football_nfl_free_printable_fake_cheat_sheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">
  
 
<div class="fakeFOOPrintablePage">
  
  <h1>
    Fake Fantasy Football Cheat Sheet
  </h1>
  
  <div style="float: right; margin-bottom: 15px; margin-left: 15px;">
    <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/fakesheet/fake-fantasy-football-cheat-sheet.jpg" AlternateText="Fake Fantasy Football Cheat Sheet"/>
  </div>

  <p>
    Aren't you sick and tired of <em>that owner</em> who consistently shows up at your fantasy football draft unprepared and wanting to look at your
    <asp:Hyperlink runat="server" NavigateUrl="https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/printable/cheat-sheets.aspx" Text="printable cheat sheets"/>
    when making their draft selections?  Well my friends, this is the year you get your revenge!  
  </p>
  
  <p>  
    Using this tool you can easily generate a <strong>fake fantasy football cheat sheet</strong> to ensure this loser gets what he or she deserves.
    We start with our 
    <asp:Hyperlink runat="server" NavigateUrl="https://www.cheatsheetwarroom.com/fantasy-football/nfl/free/rankings/player-rankings.aspx">current NFL player rankings </asp:Hyperlink>
    and <em>randomize players in groups</em> according to how believable 
    you want the rankings to appear.  
  </p>  
  
  <p>
    Your goal should be to
    configure a sheet that is <em>believable enough to pass as a valid sheet</em>, but bad enough that the lazy owner will make 
    some major blunders. 
    The amount of randomization you want to apply will differ from league to league, which is why we allow you to configure this setting.
  </p>
  
  <p>
    If you like this tool then <strong>you'll love</strong> our free 
    <asp:Hyperlink runat="server" NavigateUrl="https://www.cheatsheetwarroom.com/fantasy-football/nfl/create/custom-sheet.aspx">custom cheat sheet creation tool</asp:Hyperlink>.
    Give it a test drive, you won't regret it!
  </p>
  
  <%--Believability Level--%>
  <table style="margin: 30px 0 10px 0">
    <tr>
      <td>
        <h2 style="margin: 0px; padding: 0px;">Step 1 - Set Believability Level</h2>
      </td>
      <td style="padding-left:10px">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/fakesheet/slider.gif" AlternateText="Slider to set believability level" />
      </td>
    </tr>
  </table>

  <p>
    Use this <em>believability slider</em> to adjust how randomized you'd like the rankings to appear.  We randomize players in groups,
    based on the group size specified.  The default group size is 5, which means the first 5 players of our rankings are randomized, then players
    ranked 6 to 10 are randomized, then 11 to 15, and so on.
  </p>  
     
  <p>   
    If you're in a league with complete novices, you may be able to get away with randomizing players in groups
    of 15 and 20.  If the owners in your league are somewhat informed, but just lazy, you probably want to stick with a group size of 5 to 10.  
    Feel free to experiment with these settings and re-generate sheets until you find a configuration that is just right for your league.
  </p>

  <p>
    Randomize players in groups of: <asp:Label runat="server" ID="labGroupSize" CssClass="groupSize"/>
  </p>

  <div id="slider" style="width:80%;margin:auto;margin-bottom:10px;"></div>

  <asp:HiddenField runat="server" ID="hfGroupSize" Value="5" />

  <div class="leftGuage">more believable</div>
  <div class="rightGuage">less believable</div>
  <div style="clear:both;"></div>

  <%--Lock Players--%>
  <table style="margin: 30px 0 10px 0">
    <tr>
      <td>
        <h2 style="margin: 0px; padding: 0px;">Step 2 - Lock Top Players</h2>
      </td>
      <td style="padding-left: 10px; vertical-align: top;">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/fakesheet/lock.gif" AlternateText="Lock the top players in the cheat sheet" />
      </td>
    </tr>
  </table>

  <p>
    Unless you're dealing with some real screwballs, you may want the exclude a few of top-ranked players from the randomization process.  This <strong>makes
    the fake cheat sheet more believable</strong> to most fantasy owners.  By default, we don't randomize the top 5 players at each fantasy position, but you can
    change this setting below.
  </p>
  
  Lock the top 
  <asp:DropDownList runat="server" ID="ddlIgnoreCount">
    <asp:ListItem Text="0" Value="0"></asp:ListItem>
    <asp:ListItem Text="1" Value="1"></asp:ListItem>
    <asp:ListItem Text="2" Value="2"></asp:ListItem>
    <asp:ListItem Text="3" Value="3"></asp:ListItem>
    <asp:ListItem Text="4" Value="4"></asp:ListItem>
    <asp:ListItem Text="5" Value="5" Selected="True"></asp:ListItem>
    <asp:ListItem Text="6" Value="6"></asp:ListItem>
    <asp:ListItem Text="7" Value="7"></asp:ListItem>
    <asp:ListItem Text="8" Value="8"></asp:ListItem>
    <asp:ListItem Text="9" Value="9"></asp:ListItem>
    <asp:ListItem Text="10" Value="10"></asp:ListItem>
  </asp:DropDownList>
  players

  <%--Lock Players--%>
  <table style="margin: 30px 0 10px 0">
    <tr>
      <td>
        <h2 style="margin: 0px; padding: 0px;">Step 3 - Bye Week Randomization</h2>
      </td>
      <td style="padding-left: 10px; vertical-align: top;">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/fakesheet/random.gif" AlternateText="Randomize bye weeks to create confusion" />
      </td>
    </tr>
  </table>
 
   <div class="byeWeekRandomizeStep">
    <p>
      To add insult to injury, you can also randomize 
      <asp:Hyperlink runat="server" ID="hlByeWeeks" NavigateUrl="https://www.cheatsheetwarroom.com/blog/football/bye-weeks"></asp:Hyperlink>
      if desired.
    </p>
    <asp:CheckBox runat="server" ID="cbRandomizeByes" Text="Randomize Bye Weeks" />
  </div>

  <%--Generate your Sheet--%>
  <table style="margin: 30px 0 10px 0">
    <tr>
      <td>
        <h2 style="margin: 0px; padding: 0px;">Step 4 - Generate your Sheet</h2>
      </td>
      <td style="padding-left: 10px; vertical-align: top;">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/fakesheet/sheet.gif" AlternateText="Generate your fake sheet" />
      </td>
    </tr>
  </table>

  <p>
    Click the button below to <strong>generate your fake cheat sheet</strong>.  This cheat sheet will look and feel almost identical to our real cheat sheets.
    The only discernable differences are the name of the file: <em>fcheat.sheet.aspx</em>, and a small asterisk in the logo on the printed page.  
  </p> 
    
  <p> We hope you won't be stupid enough to start drafting from your own fake cheat sheet, but we added these clues just to be on the safe side. 
  </p>

  <div class="buttonContainer">
    <asp:Button runat="server" ID="butGenerateRandomSheet" Text="Generate Fake Fantasy Football Cheat Sheet" OnClick="butGenerateRandomSheet_Click" />
  </div>

  <%--Generate your Sheet--%>
  <table style="margin: 30px 0 10px 0">
    <tr>
      <td>
        <h2 style="margin: 0px; padding: 0px;">Step 5 - Lay the Bait</h2>
      </td>
      <td style="padding-left: 10px; vertical-align: top;">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/PrintableSheet/fakesheet/hook.gif" AlternateText="Lay the cheat sheet as bait" />
      </td>
    </tr>
  </table>

  <p>
    After you have generated a fake cheat sheet simply print a few copies, lay them around your draft room,
    and wait for the fantasy posers to take the bait.  Try not to bust-out
    laughing when they proudly put the Riley Cooper sticker on the fantasy football
    <a href="https://www.cheatsheetwarroom.com/blog/fantasy-football/draft/boards" target="_blank">draft board,</a> ecstatic that he actually fell to them in the first round.   
  </p>

</div>  <!-- close fakeFOOPrintablePage -->

  <script type="text/javascript">

    // Create the tooltips only on document load
    $(document).ready(function () {

      $("#slider").slider({
        range: "min",
        min: 5,
        max: 20,
        value: $('#<%=hfGroupSize.ClientID%>').val(),
        slide: function (event, ui) {
          $('#<%=labGroupSize.ClientID%>').text(ui.value);
          $('#<%=hfGroupSize.ClientID%>').val(ui.value);
        }
      });
      $('#<%=labGroupSize.ClientID%>').text($("#slider").slider("value"));
      $('#<%=hfGroupSize.ClientID%>').val($("#slider").slider("value"));
    });

  </script>

  
  

</asp:Content>

