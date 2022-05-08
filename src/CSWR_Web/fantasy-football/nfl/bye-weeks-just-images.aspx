<%@ Page Title="2017 NFL Bye Weeks, Schedules, & Statistical Analysis" Language="C#" MasterPageFile="~/MasterPages/Sport.master" AutoEventWireup="true" 
  CodeFile="bye-weeks-just-images.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.ByeWeeks" UseAuthorship="true" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
  MetaDescription="2017 NFL bye week schedules by week & division in various printable formats.  We also analyze historical team records following a bye week." %>
<%@ Register Src="~/usercontrols/AddThis.ascx" TagName="AddThis" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

<div class="byeWeeksPage">

  <div class="articleHeader" >
  
    <table>
      <tr>
        <td class="bustCell">
          <asp:Image runat="server" ImageUrl="~/Images/blogimages/brad-bust.jpg" AlternateText="Profile Picture of Brad Perniciaro"/>
        </td>
        <td class="rightColumn">
          <h1>2017 NFL Bye Week Schedules with Graphics and Statistical History</h1>

          <table>
            <tr>
              <td>
                <p>
                  <span class="tagline">Posted by Brad Perniciaro</span>
                  <asp:HyperLink runat="server" NavigateUrl="https://plus.google.com/113268984320725042818?rel=author">
                    <asp:Image runat="server" ImageUrl="~/Images/blogimages/googleplus.gif" />
                  </asp:HyperLink>
                </p>
                <p>
                  Updated <em><asp:Literal runat="server" ID="litUpdateDate" /></em>
                </p>

              </td>
              <td style="vertical-align:top;text-align:right;width:350px;">
              </td>
            </tr>

          </table>

        </td>
      </tr>
    </table>
  </div>

  <div class="headerImage">
      <asp:Image runat="server" ID="imaNFLByeWeeks" ImageUrl="~/Images/Sports/Football/articles/byeweeks/nfl-bye-weeks-header.jpg" />
   
  </div>
  
  <p class="intro">
    Bye weeks are an essential component of the NFL season as they give teams a much-needed break during the grueling, 16-game season.    
    Although bye weeks are not the most interesting part of the season, they do have a sizable impact on the league and associated industries like
    fantasy football.  
  </p>

  <p class="intro"> 
    The purpose of this article is to be a <strong>comprehensive guide to the 2017 NFL
    bye week schedule</strong>.  To put the 2017 bye week schedule into perspective, I also provide 
    a statistical analysis of team performance following their bye weeks for the last five 
    seasons (2012 through 2016).
  </p>
  
  <div class="toc">
    <h2>Contents</h2>
    <table>
      <tr>
        <td>
          <p class="header">Bye Week Schedules & Graphics</p>
          <p><a href="#byeWeekInfographic">2017 Full Season Bye Weeks</a></p>
          <p><a href="#byeWeeksByWeek">2017 Bye Weeks by Week</a></p>
          <p><a href="#byeWeeksByDivision">2017 Bye Weeks by Division</a></p>
        </td>
        <td>
          <p class="header">Bye Week Statistics (2012-2016)</p>
          <p><a href="#overallWinningPercentage">Record of Teams Returning from Bye</a></p>
          <p><a href="#bestWeeksToPlay">Best Weeks to Play Post Bye Week</a></p>
          <p><a href="#bestAndWorstTeams">Team Performance After Bye Weeks</a></p>
          <p><a href="#teamRecordsAfterBye">Team-Specific Game Results After Byes</a></p>
        </td>
      </tr>
    </table>  

  </div>
    

  <a name="byeWeekGraphics"></a>
  <h2>Bye Week Schedules and Sharable Graphics</h2>

  <p>
    I created several bye week graphics to represent the 2017 bye week schedule in various formats:
  </p>

  <ol>
    <li><strong>Full Season Bye Weeks Infographic:</strong> Provides a bird's eye view of the bye weeks over the entire NFL season.</li>
    <li><strong>Bye Weeks on NFL Weekly Schedule:</strong> For those tracking byes on an NFL schedule.</li>
    <li><strong>Bye Weeks by Division:</strong> Easily see when each team (and their division opponents) gets their break.</li>
  </ol>


  <a name="byeWeekInfographic"></a>
  <h3>Full Season Bye Weeks</h3>

  <p>
    The following graphic is a high-level view of how bye weeks are assigned to teams 
    between week 5 and week 11 of the 2017 NFL regular season. 
  </p>
  
  <p> 
    Unlike the 2016 NFL season, the <strong>number of teams on a bye each week is very erratic in 2017</strong>.    
    The number of teams on a bye range anywhere from two teams in week 7 all the way to six teams on
    weeks 8 and 11.
  </p>
  
  <p>
    2017 is also different from the last few years in that all of the bye weeks for the 2017 are compressed into a seven week timeframe.
    <strong>This hasn't happened since 2011</strong> when all bye weeks were schedule between weeks 5 and week 11 of the regular season.
  </p>

  <div style="width:750px;margin:30px auto 20px auto;text-align:center;">
    <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/articles/byeweeks/nfl-bye-weeks-2017v2.png" Width="735px" AlternateText="2017 NFL Bye Weeks Infographic" />
  </div>

  <a name="byeWeeksByWeek"></a>
  <h3>2017 Bye Weeks by Week</h3>

  <p>
    This section presents bye weeks according to the <strong>17 week NFL schedule</strong>.  If you're looking for the teams that are on a bye for a particular week,
    then this is the most straightforward format.  
  </p>
  <br />

  <div style="text-align:center">
    <asp:Image runat="server" ID="imaByeWeeksbyWeek" Width="900" ImageUrl="~/Images/Sports/Football/articles/byeweeks/nfl-bye-weeks-by-week-2017.gif" 
      AlternateText="Bye Weeks by NFL Weekly Schedule" />
  </div>

<br/>
<br/>
<a name="byeWeeksByDivision"></a>
<h3>2017 Bye Weeks by Division</h3>

<p>
  For those who are more interested in bye weeks for intra-division or intra-conference play, this is the graphic for you.  <strong>Teams are
  grouped by NFL division and ultimately conference</strong>.  
</p>
  
<p>
  For some historical perspective, the graphic also shows each team's bye week from the 2016 season.  This bye week is shown as a 
  smaller, faded number next to the 2017 bye week.  
</p>


<div style="text-align:center">
  <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/articles/byeweeks/nfl-bye-weeks-by-division-2017.gif" AlternateText="NFL Bye Weeks by Conference and Division" />
</div>

<br/>
<br/>
<h2>NFL Team Bye Week Summaries</h2>

<p>The following sections detail bye week information by NFL team.  Each team-specific section will break down 2017 bye week, team record following a bye week, and
    corresponding ranking compared to other NFL teams.
</p>

   <h3>New Orleans Saints Bye Week Breakdown</h3>
    
   <p>The <strong>2017 New Orleans Saints bye week</strong> is Week 5</p>

  <%-- <h4>Saints' performance following their NFL bye week</h4>--%>

   <p>In the last five yaers, the Saints are 3-2 in their games folowing a bye week.  That's tied for 4th place in the NFL.  In 2017 the Saints play the Caroline Panthers
      after returning from their Week 5 Bye.
   </p>
   <p>
      In the last five years, the Saints are 4-3 when playing a team that is returning from a bye week.  This year they play the Atlanta Falcons
       after their Week 8 bye.
   </p>

   <table>
       <tr>
           <td>

           </td>
           <td>

           </td>
           <td>

           </td>
       </tr>
   </table>

<br/>
<br/>
<h2>Bye Week History & Statistics</h2>

<p>
  NFL handicappers pay special attention to NFL bye weeks in order to identify <strong>bye week trends</strong>.  Instinct tells us that 
  teams returning from   a bye week should have a better winning percentage than their opponents, primarily because they've had a 
  week to rest while their opponents likely have not.
</p>
  
<p>
  But is it true?
</p>

<p>
  Let's analyze league-wide and teams-specific statistics related to <strong>games played following a team's bye-week</strong>.  In order to 
  maintain some level of recency, stats only consider games played in the last five years.
</p>


<a name="overallWinningPercentage"></a>
<h3>Winning Percentage of Teams Returning from Bye Week</h3>

<p>
  Over the last five years, from 2012 through the 2016 season, teams returning from a bye week have a <strong>collective record of 83-75-2</strong>.  
  That's 83 wins, 75 losses, and 2 ties for a winning percentage of 51.9%.
</p>

<p>
  This trend of teams returning from a bye week having a winning percentage slightly higher than .500 has remained consistent for the last few years.
  It supports the belief that teams with fresh legs have a <em>small advantage</em> over teams that didn't get a recent week off.
</p>

<div class="winPercentage">
  <table>
    <tr>
      <td class="wins">
        83 Wins <span>(51.9%)</span>
      </td>
      <td class="losses">
        75 Losses <span>(48.1%)</span>
      </td>
      <td class="ties"></td>
    </tr>
  </table>

</div>

<br/>
<a name="bestAndWorstTeams"></a>
<h3>Team-Specific Performance Following a Bye Week</h3>
<div class="teamPerformance">

  <p>
  It stands to reason that some teams perform better than others when returning from a bye.  The following table 
  lists each NFL team's record for <strong>games played after their bye week</strong> over the last five years.</p>

<h4>Best Teams After a Bye Week</h4>

  <table>
    <tr>
      <td>

        <ol>
          <li>Detroit Lions (5-0)</li>
          <li>Green Bay Packers (4-1)</li>
          <li>Atlanta Falcons (4-1)</li>
          <li>Denver Broncos (4-1)</li>
          <li>Indianapolis Colts (4-1)</li>
          <li>Miami Dolphins (4-1)</li>
          <li>Houston Texans (4-1)</li>
          <li>Baltimore Ravens (3-2)</li>
        </ol>

      </td>
      <td>

        <ol start="9">
          <li>New Orleans Saints (3-2)</li>
          <li>New York Giants (3-2)</li>
          <li>New England Patriots (3-2)</li>
          <li>Seattle Seahawks (3-2) </li>
          <li>Dallas Cowboys (3-2)</li>
          <li>Kansas City Chiefs (3-2)</li>
          <li>Arizona Cardinals (3-2) </li>
          <li>Oakland Raiders (3-2)</li>
        </ol>
      </td>
    </tr>
  </table>


  <h4>Worst Teams After a Bye Week</h4>


  <table>
    <tr>
      <td>
        <ol start="17">
          <li>Los Angeles Rams (2-2-1)</li>
          <li>Pittsburgh Steelers (2-3)</li>
          <li>Chicago Bears (2-3)</li>
          <li>Philadelphia Eagles (2-3)</li>
          <li>Tampa Bay Buccaneers (2-3)</li>
          <li>Jacksonville Jaguars (2-3)</li>
          <li>Cincinnati Bengals (2-3)</li>
          <li>Los Angeles Chargers (2-3) </li>
        </ol>
      </td>
      <td>
        <ol start="25">
          <li>Tennessee Titans (2-3)</li>
          <li>Carolina Panthers (2-3) </li>
          <li>Buffalo Bills (2-3) </li>
          <li>Washington Redskins (2-3)</li>
          <li>New York Jets (1-4) </li>
          <li>Cleveland Browns (1-4)</li>
          <li>Minnesota Vikings (1-4)</li>
          <li>San Francisco 49ers (0-4-1)</li>
        </ol>
      </td>
    </tr>
  </table>

 </div>

<br />
<a name="bestWeeksToPlay"></a>
<h3>The Best Weeks to Play Following a Bye Week</h3>

<p>
  While it is true that some teams play better following a bye, it is also true that teams as a whole <strong>perform better on certain weeks
  following a bye</strong>.  The following table and chart detail the collective record of teams playing on certain weeks which
  follow their bye week (again, over the <em>last five years</em>).
</p>

<p>
  The trend over the last five years is that <strong>teams with the earliest bye weeks perform best</strong> in the game following their bye week.  You can see from
  the graph below that teams which return from their byes on Week 5 through Week 8 have a winning percentage well over .500.
</p>
  
<p>
  Between Week 9 and Week 14, teams are either .500 or have a collective losing record when returning from a bye (except for Week 10).  
  Strangely, Week 10 has historically been the best week to play when returning from a bye (from 2011 to 2016).
</p>

<div class="bestWeeksContainer">

  <h4>Win/Loss Ratio by Weeks of Teams Returning from a Bye (2012-2016)</h4>
  <table class="outer">
    <tr>
      <td class="leftCell">

        <table class="inner">
          <tr>
            <th>Week</th>
            <th>Wins</th>
            <th>Losses</th>
            <th>Tie</th>
            <th>W-L Ratio</th>
          </tr>
          <tr class="alt">
            <td>Week 5</td>
            <td>8</td>
            <td>6</td>
            <td>0</td>
            <td>.571</td>
          </tr>
          <tr>
            <td>Week 6</td>
            <td>11</td>
            <td>7</td>
            <td>0</td>
            <td>.611</td>
          </tr>
          <tr class="alt">
            <td>Week 7</td>
            <td>8</td>
            <td>6</td>
            <td>0</td>
            <td>.571</td>
          </tr>
          <tr>
            <td>Week 8</td>
            <td>9</td>
            <td>7</td>
            <td>0</td>
            <td>.563</td>
          </tr>
          <tr class="alt">
            <td>Week 9</td>
            <td>10</td>
            <td>12</td>
            <td>0</td>
            <td>.455</td>
          </tr>
          <tr>
            <td>Week 10</td>
            <td>16</td>
            <td>10</td>
            <td>2</td>
            <td>.615</td>
          </tr>
          <tr class="alt">
            <td>Week 11</td>
            <td>10</td>
            <td>12</td>
            <td>0</td>
            <td>.455</td>
          </tr>
          <tr>
            <td>Week 12</td>
            <td>7</td>
            <td>11</td>
            <td>0</td>
            <td>.389</td>
          </tr>
          <tr class="alt">
            <td>Week 13</td>
            <td>3</td>
            <td>3</td>
            <td>0</td>
            <td>.500</td>
          </tr>
          <tr>
            <td>Week 14</td>
            <td>1</td>
            <td>1</td>
            <td>0</td>
            <td>.500</td>
          </tr>
        </table>


      </td>
      <td class="rightCell">
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/articles/byeweeks/post-bye-win-loss-2017.gif" Width="500"
          AlternateText="Graph of NFL Team Win/Loss Ratio Following a Bye Week" />
      </td>
    </tr>
  </table>

</div>

<br/><br />
<a name="teamRecordsAfterBye"></a>
<h3>Team-Specific Game Results Following a Bye Week</h3>

  <p>
    The following section deatils the results of every post-bye-week game over the last five years.  Teams are
    grouped by conference and division for easy identification.     
  </p>
  
  <p>
    To make it simpler to visualize game results, <strong>wins are highlighted in green</strong>, <strong>losses are highlighted in red</strong>, 
    and <strong>ties are highlighted in yellow</strong>.
  </p>
  <br />

  <div style="text-align:center">
    <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/articles/byeweeks/afc-bye-week-records-2017.gif" AlternateText="AFC Conference Post Bye Week Records" />
  </div>

  <br /><br />

  <div style="text-align:center">
    <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/articles/byeweeks/nfc-bye-week-records-2017.gif" AlternateText="NFC Conference Post Bye Week Records" />
  </div>

<br/>

 <p>I want to make this the greatest bye week resource on the web.  If you have any ideas of new bye week statistics or analysis that would
   be useful, tell me about it in the comments below.
 </p>

 <br /><br />
<div>
  <cswr:AddThis runat="server" ID="addThisArticleFooter" UseSmallIcons="true" />
</div>

<div style="clear:both;"></div>
<br /><br />



    

</div>  <!-- close bye weeks page -->




  <script type="text/javascript">
    $('textarea').click(function () {
      // the select() function on the DOM element will do what you want
      this.select();
    });
  </script>

</asp:Content>

