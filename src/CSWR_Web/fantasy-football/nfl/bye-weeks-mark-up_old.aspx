<%@ Page Title="2017 NFL Bye Weeks, Schedules, & Statistical Analysis" Language="C#" MasterPageFile="~/MasterPages/Sport.master" AutoEventWireup="true" 
  CodeFile="bye-weeks-mark-up_old.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.ByeWeeks" UseAuthorship="true" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
  MetaDescription="2017 NFL bye week schedules by week & division in various printable formats.  We also analyze historical team records following a bye week." %>
<%@ Register Src="~/usercontrols/AddThis.ascx" TagName="AddThis" TagPrefix="cswr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

    <style>

        .byeWeekInfographics {margin-bottom:30px;}
        .byeWeekInfographics table {width:100%;}
        .byeWeekInfographics table td {width:50%;vertical-align: top;}
        .byeWeekInfographics table td h3 {text-align:center;}
        .byeWeekInfographics table td img {display:block;margin:auto;padding-bottom:30px;}
        .byeWeekInfographics table tr {padding-bottom:20px;}

        ul { padding-bottom: 20px;}
        ul li { font-size: 16px;}
    </style>

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
  
  <h2>2017 NFL Bye Weeks</h2>
    
  <ul>
    <li>
        <strong>Week 5:</strong>
        Saints, Falcons, Redskins, Broncos, Buccaneers <em>(moved)</em>, Dolphins <em>(moved)</em>
        <span style="font-style: italic; display: block;">Tamps Bay vs Miami game moved to Week 11 due to Hurricane Irma</span>
    </li>
    <li>
        <strong>Week 6:</strong>
        Cowboys, Seahawks, Bengals, Bills
    </li>
    <li>
        <strong>Week 7:</strong>
        Texans, Lions
    </li>
    <li>
        <strong>Week 8:</strong>
        Jaguars, Rams, Packers, Cardinals, Giants, Titans
    </li>
    <li>
        <strong>Week 9:</strong>
        Vikings, Bears, Chargers, Steelers, Browns, Patriots
    </li>
    <li>
        <strong>Week 10:</strong>
        Ravens, Eagles, Raiders, Chiefs
    </li>
    <li>
        <strong>Week 11:</strong>
        Panthers, 49ers, Jets, Colts 
    </li>
  </ul>

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
    <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/articles/byeweeks/nfl-bye-weeks-2017.png" Width="735px" AlternateText="2017 NFL Bye Weeks Infographic" />
  </div>

  <a name="byeWeeksByWeek"></a>
  <h3>2017 Bye Weeks by Week</h3>

  <p>
    This section presents bye weeks according to the <strong>17 week NFL schedule</strong>.  If you're looking for the teams that are on a bye for a particular week,
    then this is the most straightforward format.  
  </p>

  <p>Click below the image to view this table as an image that you can download.</p> 

  <div style="width:900px;margin:auto;text-align:center;">
      <table class="byWeek" style="text-align:left;">
      <tr>
        <th>Week</th>
        <th>Bye Weeks on NFL Schedule</th>
      </tr>
      <!--Week 1-->
      <tr>
        <td class="leftCol">Week 1</td>
        <td class="inactiveWeek">None</td>
      </tr>
      <!--Week 2-->
      <tr>
        <td class="leftCol">Week 2</td>
        <td class="inactiveWeek">None</td>
      </tr>
      <!--Week 3-->
      <tr>
        <td class="leftCol">Week 3</td>
        <td class="inactiveWeek">None</td>
      </tr>
      <!--Week 4-->
      <tr>
        <td class="leftCol">Week 4</td>
        <td class="inactiveWeek">None</td>
      </tr>
      <!--Week 5-->
      <tr class="byes">
        <td class="leftCol">Week 5</td>
        <td class="byeTeams">

          <table>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Saints.png" AlternateText="New Orleans Saints Helmet" />
                New Orleans Saints
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Falcons.png" AlternateText="Atlanta Falcons Helmet" />
                Atlanta Falcons
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Redskins.png" AlternateText="Washington Redskins Helmet" />
                Washington Redskins
              </td>
            </tr>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Broncos.png" AlternateText="Denver Broncos Helmet" />
                Denver Broncos
              </td>
              <td>

              </td>
              <td>

              </td>
            </tr>
          </table>
        </td>
      </tr>
      <!--Week 6-->
      <tr class="byes">
        <td class="leftCol">Week 6</td>
        <td class="byeTeams">

          <table>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Cowboys.png"  AlternateText="Dallas Cowboys Helmet" />
                Dallas Cowboys
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Seahawks.png"  AlternateText="Seattle Seahawks Helmet" />
                Seattle Seahawks
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Bengals.png"  AlternateText="Cincinnatti Bengals Helmet" />
                Cincinnati Bengals
              </td>
            </tr>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Bills.png"  AlternateText="Buffalo Bills Helmet" />
                Buffalo Bills
              </td>
              <td>

              </td>
              <td>

              </td>
            </tr>
          </table>

        </td>
      </tr>
      <!--Week 7-->
      <tr class="byes">
        <td class="leftCol">Week 7</td>
        <td class="byeTeams">

          <table>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Texans.png"  AlternateText="Houston Texans Helmet" />
                Houston Texans
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Lions.png"  AlternateText="Detroit Lions Helmet" />
                Detroit Lions
              </td>
              <td>

              </td>
            </tr>
          </table>

        </td>
      </tr>
      <!--Week 8-->
      <tr class="byes">
        <td class="leftCol">Week 8</td>
        <td class="byeTeams">
          <table>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Jaguars.png"  AlternateText="Jacksonville Jaguars Helmet" />
                Jacksonville Jaguars
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Rams.png"  AlternateText="Los Angeles Rams Helmet" />
                Los Angeles Rams
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Packers.png"  AlternateText="Green Bay Packers Rams Helmet" />
                Green Bay Packers
              </td>
            </tr>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Cardinals.png"  AlternateText="Arizona Cardinals Helmet" />
                Arizona Cardinals
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Giants.png"  AlternateText="New York Giants Helmet" />
                New York Giants
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Titans.png"  AlternateText="Tennessee Titans Helmet" />
                Tennessee Titans  
              </td>
            </tr>
          </table>
        </td>
      </tr>
      <!--Week 9-->
      <tr class="byes">
        <td class="leftCol">Week 9</td>
        <td class="byeTeams">

          <table>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Vikings.png" AlternateText="Minnesota Vikings Helmet" />
                Minnesota Vikings
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Bears.png" AlternateText="Chicago Bears Helmet" />
                Chicago Bears
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Chargers.png" AlternateText="Las Angeles Chargers Helmet" />
                Los Angeles Chargers
              </td>
            </tr>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Steelers.png" AlternateText="Pittsburgh Steelers Helmet" />
                Pittsburgh Steelers
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Browns.png" AlternateText="Cleveland Browns Helmet" />
                Cleveland Browns
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Patriots.png" AlternateText="New England Patriots Helmet" />
                New England Patriots
              </td>
            </tr>
          </table>


        </td>
      </tr>
      <!--Week 10-->
      <tr>
        <td class="leftCol">Week 10</td>
        <td class="byeTeams">
          <table>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Ravens.png" AlternateText="Baltimore Ravens Helmet" />
                Baltimore Ravens
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Eagles.png" AlternateText="Philadelphia Eagles Helmet" />
                Philadelphia Eagles
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Raiders.png" AlternateText="Oakland Raiders Helmet" />
                Oakland Raiders
              </td>
            </tr>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Chiefs.png" AlternateText="Kansas City Chiefs Helmet" />
                Kansas City Chiefs
              </td>
              <td>
              </td>
              <td>
              </td>
            </tr>
          </table>
        </td>
      </tr>
      <!--Week 11-->
      <tr>
        <td class="leftCol">Week 11</td>
        <td class="byeTeams">
          <table>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Panthers.png" AlternateText="Carolina Panthers Helmet" />
                Carolina Panthers
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/49ers.png" AlternateText="San Francisco 49ers Helmet" />
                San Francisco 49ers
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Buccaneers.png" AlternateText="Tampa Bay Buccaneers Helmet" />
                Tampa Bay Buccaneers
              </td>
            </tr>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Jets.png" AlternateText="New York Yets Helmet" />
                New York Jets
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Dolphins.png" AlternateText="Miami Dolphins Helmet" />
                Miami Dolphins
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Colts.png" AlternateText="Indianapolis Colts Helmet" />
                Indianapolic Colts
              </td>
            </tr>
          </table>
        </td>
      </tr>
      <!--Week 12-->
      <tr>
        <td class="leftCol">Week 12</td>
        <td class="inactiveWeek">None</td>
      </tr>
      <!--Week 13-->
      <tr>
        <td class="leftCol">Week 13</td>
        <td class="inactiveWeek">None</td>
      </tr>
      <!--Week 14-->
      <tr>
        <td class="leftCol">Week 14</td>
        <td class="inactiveWeek">None</td>
      </tr>
      <!--Week 15-->
      <tr>
        <td class="leftCol">Week 15</td>
        <td class="inactiveWeek">None</td>
      </tr>
      <!--Week 16-->
      <tr>
        <td class="leftCol">Week 16</td>
        <td class="inactiveWeek">None</td>
      </tr>
      <!--Week 17-->
      <tr>
        <td class="leftCol">Week 17</td>
        <td class="inactiveWeek">None</td>
      </tr>
    </table>
    <asp:HyperLink runat="server" CssClass="imageLink" NavigateUrl="~/Images/Sports/Football/articles/byeweeks/nfl-bye-weeks-by-week-2017.gif">Bye Weeks by NFL Schedule</asp:HyperLink>
  
    </div>
  

<br/>
<br/>
<a name="byeWeeksByDivision"></a>
<h3>2017 Bye Weeks by Division</h3>

<p>
  For those who are more interested in bye weeks for intra-division or intra-conference play, this is the graphic for you. Teams are
  grouped by NFL division and ultimately conference.  Feel free to 
  <asp:HyperLink runat="server" CssClass="imageLink" NavigateUrl="~/Images/Sports/Football/articles/byeweeks/nfl-bye-weeks-by-division-2017.gif">download this bye weeks by division</asp:HyperLink> image.
</p>
  
<p>
  For some historical perspective, the graphic also shows each team's bye week from last year (as a 
  smaller, faded number next to the 2017 bye week).  
</p>

    
<div class="byDivision">
    
  

  <!-- AFC Conference -->
  <div id="afcConference">
    <div class="confHeader">
      <h3>American Football Conference</h3>
      <img src="../../Images/Sports/Football/General/teams/afc_logo.gif" alt="American Football Conference Logo" />
    </div>
    <div class="divisions">
      <div class="division">
        <h4>AFC North</h4>
        <table>
          <tr>
            <!-- Ravens -->
            <td><a class="ravens" /></td>
            <!-- Bengals -->
            <td><a class="bengals" /></td>
            <!-- Browns -->
            <td><a class="browns" /></td>
            <!-- Steelers -->
            <td><a class="steelers" /></td>
          </tr>
          <tr class="WkNumberRow">
            <!-- Ravens -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">8</td>
                  <td class="dCol2">10</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Bengals -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">9</td>
                  <td class="dCol2">6</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Browns -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">13</td>
                  <td class="dCol2">9</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Steelers -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">8</td>
                  <td class="dCol2">9</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </div>
      <div class="division">
        <h4>AFC East</h4>
        <table>
          <tr>
            <!-- Bills -->
            <td><a class="bills" /></td>
            <!-- Dolphins -->
            <td><a class="dolphins" /></td>
            <!-- Patriots -->
            <td><a class="patriots" /></td>
            <!-- Jets -->
            <td><a class="jets" /></td>
          </tr>
          <tr class="WkNumberRow">
            <!-- Bills -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">10</td>
                  <td class="dCol2">6</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Dolphins -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">8</td>
                  <td class="dCol2">11</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Patriots -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">9</td>
                  <td class="dCol2">9</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Jets -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">11</td>
                  <td class="dCol2">11</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </div>
      <div class="division">
        <h4>AFC South</h4>
        <table>
          <tr>
            <!-- Texans -->
            <td><a class="texans" /></td>
            <!-- Colts -->
            <td><a class="colts" /></td>
            <!-- Jaguars -->
            <td><a class="jaguars" /></td>
            <!-- Titans -->
            <td><a class="titans" /></td>
          </tr>
          <tr class="WkNumberRow">
            <!-- Texans -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">9</td>
                  <td class="dCol2">7</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Colts -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">10</td>
                  <td class="dCol2">11</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Jaguars -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">5</td>
                  <td class="dCol2">8</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Titans -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">13</td>
                  <td class="dCol2">8</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </div>
      <div class="division">
        <h4>AFC West</h4>
        <table>
          <tr>
            <!-- Broncos -->
            <td><a class="broncos" /></td>
            <!-- Chiefs -->
            <td><a class="chiefs" /></td>
            <!-- Raiders -->
            <td><a class="raiders" /></td>
            <!-- Chargers -->
            <td><a class="chargers" /></td>
          </tr>
          <tr class="WkNumberRow">
            <!-- Broncos -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">11</td>
                  <td class="dCol2">5</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Chiefs -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">5</td>
                  <td class="dCol2">5</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Raiders -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">10</td>
                  <td class="dCol2">10</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Chargers -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">11</td>
                  <td class="dCol2">9</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </div>
    </div>  <!-- close divisions -->
  </div>

  <!-- NFC Conference -->
  <div id="nfcConference">
    <div class="confHeader">
      <h3>National Football Conference</h3>
      <img src="../../Images/Sports/Football/General/teams/nfc_logo.gif" alt="National Football Conference Logo" />
    </div>
    <div class="divisions">
      <div class="division">
        <h4>NFC North</h4>
        <table>
          <tr>
            <!-- Bears -->
            <td><a class="bears" /></td>
            <!-- Lions -->
            <td><a class="lions" /></td>
            <!-- Packers -->
            <td><a class="packers" /></td>
            <!-- Vikings -->
            <td><a class="vikings" /></td>
          </tr>
          <tr class="WkNumberRow">
            <!-- Bears -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">9</td>
                  <td class="dCol2">9</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Lions -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">10</td>
                  <td class="dCol2">7</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Packers -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">4</td>
                  <td class="dCol2">8</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Vikings -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">6</td>
                  <td class="dCol2">9</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </div>
      <div class="division">
        <h4>NFC East</h4>
        <table>
          <tr>
            <!-- Cowboys -->
            <td><a class="cowboys" /></td>
            <!-- Giants -->
            <td><a class="giants" /></td>
            <!-- Eagles -->
            <td><a class="eagles" /></td>
            <!-- Redskins -->
            <td><a class="redskins" /></td>
          </tr>
          <tr class="WkNumberRow">
            <!-- Cowboys -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">7</td>
                  <td class="dCol2">6</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Giants -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">8</td>
                  <td class="dCol2">8</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Eagles -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">4</td>
                  <td class="dCol2">10</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Redskins -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">9</td>
                  <td class="dCol2">5</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </div>
      <div class="division">
        <h4>NFC South</h4>
        <table>
          <tr>
            <!-- Falcons -->
            <td><a class="falcons" /></td>
            <!-- Panthers -->
            <td><a class="panthers" /></td>
            <!-- Saints -->
            <td><a class="saints" /></td>
            <!-- Bucs -->
            <td><a class="bucs" /></td>
          </tr>
          <tr class="WkNumberRow">
            <!-- Falcons -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">11</td>
                  <td class="dCol2">5</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Panthers -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">7</td>
                  <td class="dCol2">11</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Saints -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">5</td>
                  <td class="dCol2">5</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Bucs -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">6</td>
                  <td class="dCol2">11</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </div>
      <div class="division">
        <h4>NFC West</h4>
        <table>
          <tr>
            <!-- Cardinals -->
            <td><a class="cardinals" /></td>
            <!-- Rams -->
            <td><a class="rams" /></td>
            <!-- 49ers -->
            <td><a class="fortyNiners" /></td>
            <!-- Seahawks -->
            <td><a class="seahawks" /></td>
          </tr>
          <tr class="WkNumberRow">
            <!-- Cardinals -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">9</td>
                  <td class="dCol2">8</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Rams -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">8</td>
                  <td class="dCol2">8</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- 49ers -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">8</td>
                  <td class="dCol2">11</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Seahawks -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">5</td>
                  <td class="dCol2">6</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </div>
    </div>
  </div>

  <div class="byefooter">
    <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/articles/byeweeks/bye-week-legend-2017.gif" AlternateText="Legend of Bye Week Terms" />
  </div>

</div>  <!-- close byeWksByDivisionContainer -->

    

<h2>
    Team Bye Week Infographics /w Statistical History
</h2>

If you're looking for detailed bye week information about a particular page.    

<h3>
    <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/articles/byeweeks/nfldivisionheaders/afc-north-teams.gif" alt="AFC North Bye Weeks"/>
</h3>

     
<div class="byeWeekInfographics">
    <table>
        <tr>
            <td>
                <a name="ravensByeWeeks"></a>
                <h3>Baltimore Ravens Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/baltimore-ravens-bye-week.jpg" AlternateText="Baltimore Ravens Bye Weeks" />
            </td>
            <td>
                <a name="bengalsByeWeeks"></a>
                <h3>Cincinnati Bengals Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/cincinnati-bengals-bye-week.jpg" AlternateText="Cincinnati Bengals Bye Weeks" />
            </td>
        </tr>
        <tr>
            <td>
                <a name="brownsByeWeeks"></a>
                <h3>Cleveland Browns Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/cleveland-browns-bye-week.jpg" AlternateText="Cleveland Browns Bye Weeks" />
            </td>
            <td>
                <a name="steelersByeWeeks"></a>
                <h3>Pittsburgh Steelers Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/pittsburgh-steelers-bye-week.jpg" AlternateText="Pittsburgh Steelers Bye Weeks" />
            </td>
        </tr>
    </table>
</div>
    
    

<h3>
    <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/articles/byeweeks/nfldivisionheaders/afc-east-teams.gif" alt="AFC East Bye Weeks"/>
</h3>
    


<div class="byeWeekInfographics">
    <table>
        <tr>
            <td>
                <a name="billsByeWeeks"></a>
                <h3>Buffalo Bill Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/buffalo-bills-bye-week.jpg" AlternateText="Buffalo Bills Bye Weeks" />
            </td>
            <td>
                <a name="dolphinsByeWeeks"></a>
                <h3>Miami Dolphins Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/miami-dolphins-bye-week.jpg" AlternateText="Miami Dolphins Bye Weeks" />
            </td>
        </tr>
        <tr>
            <td>
                <a name="patriotsByeWeeks"></a>
                <h3>New England Patriots Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/new-england-patriots-bye-week.jpg" AlternateText="New England Patriots Bye Weeks" />
            </td>
            <td>
                <a name="jetsByeWeeks"></a>
                <h3>New York Jets Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/new-york-jets-bye-week.jpg" AlternateText="New York Jets Bye Weeks" />
            </td>
        </tr>
    </table>
</div>
    

<h3>
    <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/articles/byeweeks/nfldivisionheaders/afc-south-teams.gif" alt="AFC South Bye Weeks"/>
</h3>

    

<div class="byeWeekInfographics">
    <table>
        <tr>
            <td>
                <a name="texansByeWeeks"></a>
                <h3>Houston Texans Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/houston-texans-bye-week.jpg" AlternateText="Houston Texans Bye Weeks" />
            </td>
            <td>
                <a name="coltsByeWeeks"></a>
                <h3>Indianapolis Colts Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/indianapolis-colts-bye-week.jpg" AlternateText="Indianapolis Colts bye Weeks"/>
            </td>
        </tr>
        <tr>
            <td>
                <a name="jaguarsByeWeeks"></a>
                <h3>Jacksonville Jaguars Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/jacksonville-jaguars-bye-week.jpg" AlternateText="Jacksonville Jaguars Bye Weeks" />
            </td>
            <td>
                <a name="titansByeWeeks"></a>
                <h3>Tennessee Titans Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/tennessee-titans-bye-week.jpg" AlternateText="Tennessee Titans Bye Weeks" />
            </td>
        </tr>
    </table>
</div>    
    

<h3>
    <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/articles/byeweeks/nfldivisionheaders/afc-west-teams.gif" alt="AFC West Bye Weeks"/>
</h3>


<div class="byeWeekInfographics">
    <table>
        <tr>
            <td>
                <a name="broncosByeWeeks"></a>
                <h3>Denver Broncos Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/denver-broncos-bye-week.jpg" AlternateText="Denver Broncos Bye Weeks" />
            </td>
            <td>
                <a name="chiefsByeWeeks"></a>
                <h3>Kansas City Chiefs Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/kansas-city-chiefs-bye-week.jpg" AlternateText="Kansas City Chiefs Bye Weeks" />
            </td>
        </tr>
        <tr>
            <td>
                <a name="raidersByeWeeks"></a>
                <h3>Oakland Raiders Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/oakland-raiders-bye-week.jpg" AlternateText="Oakland Raiders Bye Weeks" />
            </td>
            <td>
                <a name="chargersByeWeeks"></a>
                <h3>Los Angeles Chargers Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/los-angeles-chargers-bye-week.jpg" AlternateText="Los Angeles Chargers Bye Weeks" />
            </td>
        </tr>
    </table>
</div>  
    
    

<h3>
    <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/articles/byeweeks/nfldivisionheaders/nfc-north-teams.gif" alt="NFC North Bye Weeks"/>
</h3>




<div class="byeWeekInfographics">
    <table>
        <tr>
            <td>
                <a name="bearsByeWeeks"></a>
                <h3>Chicago Bears Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/chicago-bears-bye-week.jpg" AlternateText="Chicago Bears Bye Weeks" />
            </td>
            <td style="width:50%;">
                <a name="lionsByeWeeks"></a>
                <h3>Detroit Lions Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/detroit-lions-bye-week.jpg" Text="Detroit Lions Bye Weeks" />
            </td>
        </tr>
        <tr>
            <td>
                <a name="packersByeWeeks"></a>
                <h3>Green Bay Packers Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/green-bay-packers-bye-week.jpg" Text="Green Bay Packers Bye Weeks" />
            </td>
            <td>
                <a name="vikingsByeWeeks"></a>
                <h3>Minnesota Vikings Bye Weeks</h3>
                <asp:Image runat="server"  CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/minnesota-vikings-bye-week.jpg" Text="Minnesota Vikings Bye Weeks" />
            </td>
        </tr>
    </table>
</div>



<h3>
    <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/articles/byeweeks/nfldivisionheaders/nfc-east-teams.gif" alt="NFC East Bye Weeks"/>
</h3>
   

<div class="byeWeekInfographics">
    <table>
        <tr>
            <td>
                <a name="cowboysByeWeeks"></a>
                <h3>Dallas Cowboys Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/dallas-cowboys-bye-week.jpg" AlternateText="Dallas Cowboys Bye Weeks" />
            </td>
            <td>
                <a name="giantsByeWeeks"></a>
                <h3>New York Giants Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/new-york-giants-bye-week.jpg" AlternateText="New York Giants Bye Weeks" />
            </td>
        </tr>
        <tr>
            <td>
                <a name="eaglesByeWeeks"></a>
                <h3>Phildelphia Eagles Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/philadelphia-eagles-bye-week.jpg" AlternateText="Philadelphia Tagles Bye Weeks" />
            </td>
            <td>
                <a name="redskinsByeWeeks"></a>
                <h3>Washington Redskins Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/washington-redskins-bye-week.jpg" AlternateText="Washington Redskins Bye Weeks" />
            </td>
        </tr>
    </table>
</div>

<h3>
    <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/articles/byeweeks/nfldivisionheaders/nfc-south-teams.gif" alt="NFC South Bye Weeks"/>
</h3>


<div class="byeWeekInfographics">
    <table>
        <tr>
            <td>
                <h3>Atlanta Cowboys Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/atlanta-falcons-bye-week.jpg" AlternateText="Atlanta Falcons Bye Weeks" />
            </td>
            <td>
                <h3>New Orleans Saints Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/new-orleans-saints-bye-week.jpg" AlternateText="New Orleans Saints Bye Weeks" />
            </td>
        </tr>
        <tr>
            <td>
                <h3>Tampa Bay Buccaneers Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/tampa-bay-buccaneers-bye-week.jpg" AlternateText="Tamps Bay Buccaneers Bye Weeks" />
            </td>
            <td>
                <h3>Carolina Panthers Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/carolina-panthers-bye-week.jpg" AlternateText="Carolina Panthers Bye Weeks" />
            </td>
        </tr>
    </table>
</div>

<h3>
    <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/articles/byeweeks/nfldivisionheaders/nfc-west-teams.gif" alt="NFC West Bye Week Infographics"/>
</h3>
 
<div class="byeWeekInfographics">
    <table>
        <tr>
            <td>
                <h3>Arizona Cardinals Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/arizona-cardinals-bye-week.jpg" AlternateText="Arizona Cardinals Bye Weeks"  />
            </td>
            <td>
                <h3>Los Angeles Rams Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/los-angeles-rams-bye-week.jpg" AlternateText="Los Angeles Rams Bye Weeks" />
            </td>
        </tr>
        <tr>
            <td>
                <h3>San Francisco 49ers Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/san-francisco-49ers-bye-week.jpg" AlternateText="San Francisco 49ers Bye Weeks" />
            </td>
            <td>
                <h3>Seattle Seahawk Bye Weeks</h3>
                <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/teaminfographics/seattle-seahawks-bye-week.jpg" AlternateText="Seattle Seahawks Bye Weeks" />
            </td>
        </tr>
    </table>
</div>





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
<h2>Team-Specific Game Results Following a Bye Week</h2>

  <p>
    The following section deatils the results of every post-bye-week game over the last five years.  Teams are
    grouped by conference and division for easy identification.     
  </p>
  
  <p>
    To make it simpler to visualize game results, <strong>wins are highlighted in green</strong>, <strong>losses are highlighted in red</strong>, 
    and <strong>ties are highlighted in yellow</strong>.
  </p>

<%--<asp:HyperLink runat="server" CssClass="imageLink" NavigateUrl="~/Images/Sports/Football/articles/byeweeks/post-bye-week-records-afc-3.gif">View Image</asp:HyperLink>--%>

<div class="teamHistoricRecordsRow">
  <h4 class="division afc">AFC North</h4>

  <div class="column">
    <table class="division">
      <tr>
        <th colspan="4" class="teamLogo"><a class="ravens" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">BAL Ravens (3-2)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 9</td>
        <td>Win</td>
        <td>21-14 vs STL</td>
      </tr>
      <tr class="loss">
        <td>'15</td>
        <td>Wk 10</td>
        <td>Loss</td>
        <td>20-22 vs JAC</td>
      </tr>
      <tr class="win">
        <td>'14</td>
        <td>Wk 12</td>
        <td>Win</td>
        <td>34-27 vs NO</td>
      </tr>
      <tr class="loss">
        <td>'13</td>
        <td>Wk 9</td>
        <td>Loss</td>
        <td>18-24 vs CLE</td>
      </tr>
      <tr class="win">
        <td>'12</td>
        <td>Wk 9</td>
        <td>Win</td>
        <td>25-15 vs CLE</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="bengals" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">CIN Bengals (2-3)</th>
      </tr>
      <tr class="loss">
        <td>'16</td>
        <td>Wk 10</td>
        <td>Loss</td>
        <td>20-21 vs NYG</td>
      </tr>
      <tr class="win">
        <td>'15</td>
        <td>Wk 8</td>
        <td>Win</td>
        <td>16-10 vs PIT</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 5</td>
        <td>Loss</td>
        <td>17-43 vs NE</td>
      </tr>
      <tr class="win">
        <td>'13</td>
        <td>Wk 13</td>
        <td>Win</td>
        <td>17-10 vs SD</td>
      </tr>
      <tr class="loss">
        <td>'12</td>
        <td>Wk 9</td>
        <td>Loss</td>
        <td>23-31 vs DEN</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="browns" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">CLU Browns (1-4)</th>
      </tr>
      <tr class="loss">
        <td>'16</td>
        <td>Wk 14</td>
        <td>Loss</td>
        <td>10-23 vs CIN</td>
      </tr>
      <tr class="loss">
        <td>'15</td>
        <td>Wk 12</td>
        <td>Loss</td>
        <td>27-33 vs BAL</td>
      </tr>
      <tr class="win">
        <td>'14</td>
        <td>Wk 5</td>
        <td>Win</td>
        <td>29-28 vs TEN</td>
      </tr>
      <tr class="loss">
        <td>'13</td>
        <td>Wk 11</td>
        <td>Loss</td>
        <td>20-41 vs CIN</td>
      </tr>
      <tr class="loss">
        <td>'12</td>
        <td>Wk 11</td>
        <td>Loss</td>
        <td>20-23 vs DAL</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="steelers" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">PIT Steelers (2-3)</th>
      </tr>
      <tr class="loss">
        <td>'16</td>
        <td>Wk 9</td>
        <td>Loss</td>
        <td>14-21 vs BAL</td>
      </tr>
      <tr class="loss">
        <td>'15</td>
        <td>Wk 12</td>
        <td>Loss</td>
        <td>30-39 vs SEA</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 13</td>
        <td>Loss</td>
        <td>32-35 vs NO</td>
      </tr>
      <tr class="win">
        <td>'13</td>
        <td>Wk 6</td>
        <td>Win</td>
        <td>19-6 vs NYJ</td>
      </tr>
      <tr class="win">
        <td>'12</td>
        <td>Wk 5</td>
        <td>Win</td>
        <td>16-14 vs PHI</td>
      </tr>
    </table>
  </div>

</div>  <!-- close teamHistoricRecordsRow -->

<div class="teamHistoricRecordsRow">
  <h4 class="division afc">AFC East</h4>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="bills" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">BUF Bills (2-3)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 11</td>
        <td>Win</td>
        <td>16-12 vs CIN</td>
      </tr>
      <tr class="win">
        <td>'15</td>
        <td>Wk 9</td>
        <td>Win</td>
        <td>33-17 vs MIA</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 10</td>
        <td>Loss</td>
        <td>13-17 vs KC</td>
      </tr>
      <tr class="loss">
        <td>'13</td>
        <td>Wk 13</td>
        <td>Loss</td>
        <td>31-34 vs ATL</td>
      </tr>
      <tr class="loss">
        <td>'12</td>
        <td>Wk 9</td>
        <td>Loss</td>
        <td>9-21 vs HOU</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="dolphins" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">MIA Dolphins (4-1)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 9</td>
        <td>Win</td>
        <td>27-23 vs NYJ</td>
      </tr>
      <tr class="win">
        <td>'15</td>
        <td>Wk 6</td>
        <td>Win</td>
        <td>38-10 vs TEN</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 6</td>
        <td>Loss</td>
        <td>24-27 vs GB</td>
      </tr>
      <tr class="win">
        <td>'13</td>
        <td>Wk 7</td>
        <td>Win</td>
        <td>23-21 vs BUF</td>
      </tr>
      <tr class="win">
        <td>'12</td>
        <td>Wk 8</td>
        <td>Win</td>
        <td>30-9 vs NYJ</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="patriots" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">NE Patriots (3-2)</th>
      </tr>
      <tr class="loss">
        <td>'16</td>
        <td>Wk 10</td>
        <td>Loss</td>
        <td>24-31 vs SEA</td>
      </tr>
      <tr class="win">
        <td>'15</td>
        <td>Wk 5</td>
        <td>Win</td>
        <td>30-6 vs DAL</td>
      </tr>
      <tr class="win">
        <td>'14</td>
        <td>Wk 11</td>
        <td>Win</td>
        <td>42-20 vs IND</td>
      </tr>
      <tr class="loss">
        <td>'13</td>
        <td>Wk 11</td>
        <td>Loss</td>
        <td>20-24 vs CAR</td>
      </tr>
      <tr class="win">
        <td>'12</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>37-31 vs BUF</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="jets" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">NY Jets (1-4)</th>
      </tr>
      <tr class="loss">
        <td>'16</td>
        <td>Wk 12</td>
        <td>Loss</td>
        <td>17-22 vs NE</td>
      </tr>
      <tr class="win">
        <td>'15</td>
        <td>Wk 6</td>
        <td>Win</td>
        <td>34-20 vs WAS</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 12</td>
        <td>Loss</td>
        <td>3-38 vs BUF</td>
      </tr>
      <tr class="loss">
        <td>'13</td>
        <td>Wk 11</td>
        <td>Loss</td>
        <td>14-37 vs BUF</td>
      </tr>
      <tr class="loss">
        <td>'12</td>
        <td>Wk 10</td>
        <td>Loss</td>
        <td>7-28 vs SEA</td>
      </tr>
    </table>
  </div>
</div>  <!-- close teamHistoricRecordsRow -->

<div class="teamHistoricRecordsRow">
  <h4 class="division afc">AFC South</h4>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="texans" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">HOU Texans (4-1)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>24-21 vs JAC</td>
      </tr>
      <tr class="win">
        <td>'15</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>10-6 vs CIN</td>
      </tr>
      <tr class="win">
        <td>'14</td>
        <td>Wk 11</td>
        <td>Win</td>
        <td>23-7 vs CLE</td>
      </tr>
      <tr class="loss">
        <td>'13</td>
        <td>Wk 9</td>
        <td>Loss</td>
        <td>24-27 vs IND</td>
      </tr>
      <tr class="win">
        <td>'12</td>
        <td>Wk 9</td>
        <td>Win</td>
        <td>21-9 vs BUF</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamTitle"><a class="colts" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">IND Colts (4-1)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 11</td>
        <td>Win</td>
        <td>24-17 vs TEN</td>
      </tr>
      <tr class="win">
        <td>'15</td>
        <td>Wk 11</td>
        <td>Win</td>
        <td>24-21 vs ATL</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 11</td>
        <td>Loss</td>
        <td>20-42 vs NE</td>
      </tr>
      <tr class="win">
        <td>'13</td>
        <td>Wk 9</td>
        <td>Win</td>
        <td>27-24 vs HOU</td>
      </tr>
      <tr class="win">
        <td>'12</td>
        <td>Wk 5</td>
        <td>Win</td>
        <td>30-27 vs GB</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="jaguars" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">JAC Jaguars (2-3)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 6</td>
        <td>Win</td>
        <td>17-16 vs CHI</td>
      </tr>
      <tr class="loss">
        <td>'15</td>
        <td>Wk 9</td>
        <td>Loss</td>
        <td>23-28 vs NYJ</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 12</td>
        <td>Loss</td>
        <td>3-23 vs IND</td>
      </tr>
      <tr class="win">
        <td>'13</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>29-27 vs TEN</td>
      </tr>
      <tr class="loss">
        <td>'12</td>
        <td>Wk 7</td>
        <td>Loss</td>
        <td>23-26 vs OAK</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="titans" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">TEN Titans (2-3)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 14</td>
        <td>Win</td>
        <td>13-10 vs DEN</td>
      </tr>
      <tr class="loss">
        <td>'15</td>
        <td>Wk 5</td>
        <td>Loss</td>
        <td>13-14 vs BUF</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 10</td>
        <td>Loss</td>
        <td>7-21 vs BAL</td>
      </tr>
      <tr class="win">
        <td>'13</td>
        <td>Wk 9</td>
        <td>Win</td>
        <td>28-21 vs STL</td>
      </tr>
      <tr class="loss">
        <td>'12</td>
        <td>Wk 12</td>
        <td>Loss</td>
        <td>19-24 vs JAC</td>
      </tr>
    </table>
  </div>
</div>  <!-- close teamHistoricRecordsRow -->

<div class="teamHistoricRecordsRow">
  <h4 class="division afc">AFC West</h4>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="broncos" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">DEN Broncos (4-1)</th>
      </tr>
      <tr class="loss">
        <td>'16</td>
        <td>Wk 12</td>
        <td>Loss</td>
        <td>27-30 vs KC</td>
      </tr>
      <tr class="win">
        <td>'15</td>
        <td>Wk 8</td>
        <td>Win</td>
        <td>29-10 vs GB</td>
      </tr>
      <tr class="win">
        <td>'14</td>
        <td>Wk 5</td>
        <td>Win</td>
        <td>41-20 vs ARI</td>
      </tr>
      <tr class="win">
        <td>'13</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>28-20 vs SD</td>
      </tr>
      <tr class="win">
        <td>'12</td>
        <td>Wk 8</td>
        <td>Win</td>
        <td>34-14 vs NO</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="chiefs" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">KC Chiefs (3-2)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 6</td>
        <td>Win</td>
        <td>26-10 vs OAK</td>
      </tr>
      <tr class="win">
        <td>'15</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>29-13 vs DEN</td>
      </tr>
      <tr class="win">
        <td>'14</td>
        <td>Wk 7</td>
        <td>Win</td>
        <td>23-20 vs SD</td>
      </tr>
      <tr class="loss">
        <td>'13</td>
        <td>Wk 11</td>
        <td>Loss</td>
        <td>17-27 vs DEN</td>
      </tr>
      <tr class="loss">
        <td>'12</td>
        <td>Wk 8</td>
        <td>Loss</td>
        <td>16-26 vs OAK</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="raiders" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">OAK Raiders (3-2)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 11</td>
        <td>Win</td>
        <td>27-20 vs HOU</td>
      </tr>
      <tr class="win">
        <td>'15</td>
        <td>Wk 7</td>
        <td>Win</td>
        <td>37-29 vs SD</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 6</td>
        <td>Loss</td>
        <td>28-31 vs SD</td>
      </tr>
      <tr class="win">
        <td>'13</td>
        <td>Wk 8</td>
        <td>Win</td>
        <td>21-18 vs PIT</td>
      </tr>
      <tr class="loss">
        <td>'12</td>
        <td>Wk 6</td>
        <td>Loss</td>
        <td>20-23 vs ATL</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="chargers" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">LA Chargers (2-3)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 12</td>
        <td>Win</td>
        <td>21-13 vs HOU</td>
      </tr>
      <tr class="loss">
        <td>'15</td>
        <td>Wk 11</td>
        <td>Loss</td>
        <td>3-33 vs KC</td>
      </tr>
      <tr class="win">
        <td>'14</td>
        <td>Wk 11</td>
        <td>Win</td>
        <td>13-6 vs OAK</td>
      </tr>
      <tr class="loss">
        <td>'13</td>
        <td>Wk 9</td>
        <td>Loss</td>
        <td>24-30 vs WAS</td>
      </tr>
      <tr class="loss">
        <td>'12</td>
        <td>Wk 8</td>
        <td>Loss</td>
        <td>6-7 vs CLE</td>
      </tr>
    </table>
  </div>
</div>  <!-- close teamHistoricRecordsRow -->




<asp:HyperLink runat="server" CssClass="imageLink" NavigateUrl="~/Images/Sports/Football/articles/byeweeks/post-bye-week-records-nfc-3.gif">View Image</asp:HyperLink>

<div class="teamHistoricRecordsRow">
  <h4 class="division nfc">NFC North</h4>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="bears" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">CHI Bears (2-3)</th>
      </tr>
      <tr class="loss">
        <td>'16</td>
        <td>Wk 10</td>
        <td>Loss</td>
        <td>10-36 vs TB</td>
      </tr>
      <tr class="loss">
        <td>'15</td>
        <td>Wk 8</td>
        <td>Loss</td>
        <td>20-23 vs MIN</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 10</td>
        <td>Loss</td>
        <td>14-55 vs GB</td>
      </tr>
      <tr class="win">
        <td>'13</td>
        <td>Wk 9</td>
        <td>Win</td>
        <td>27-20 vs GB</td>
      </tr>
      <tr class="win">
        <td>'12</td>
        <td>Wk 7</td>
        <td>Win</td>
        <td>13-7 vs DET</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="lions" style="margin:auto;" /></th>
      </tr>
      <tr class="teamName">
        <th colspan="4" class="teamTitle">DET Lions (5-0)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 11</td>
        <td>Win</td>
        <td>26-19 vs JAC</td>
      </tr>
      <tr class="win">
        <td>'15</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>18-16 vs GB</td>
      </tr>
      <tr class="win">
        <td>'14</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>20-16 vs MIA</td>
      </tr>
      <tr class="win">
        <td>'13</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>21-19 vs CHI</td>
      </tr>
      <tr class="win">
        <td>'12</td>
        <td>Wk 6</td>
        <td>Win</td>
        <td>26-23 vs PHI</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="packers" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">GB Packers (4-1)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 5</td>
        <td>Win</td>
        <td>23-16 vs NYG</td>
      </tr>
      <tr class="loss">
        <td>'15</td>
        <td>Wk 8</td>
        <td>Loss</td>
        <td>10-29 vs DEN</td>
      </tr>
      <tr class="win">
        <td>'14</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>55-14 vs CHI</td>
      </tr>
      <tr class="win">
        <td>'13</td>
        <td>Wk 5</td>
        <td>Win</td>
        <td>22-9 vs DET</td>
      </tr>
      <tr class="win">
        <td>'12</td>
        <td>Wk 11</td>
        <td>Win</td>
        <td>24-20 vs DET</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="vikings" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">MIN Vikings (1-4)</th>
      </tr>
      <tr class="loss">
        <td>'16</td>
        <td>Wk 7</td>
        <td>Loss</td>
        <td>10-21 vs PHI</td>
      </tr>
      <tr class="win">
        <td>'15</td>
        <td>Wk 6</td>
        <td>Win</td>
        <td>16-10 vs KC</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 11</td>
        <td>Loss</td>
        <td>13-21 vs CHI</td>
      </tr>
      <tr class="loss">
        <td>'13</td>
        <td>Wk 6</td>
        <td>Loss</td>
        <td>10-35 vs CAR</td>
      </tr>
      <tr class="loss">
        <td>'12</td>
        <td>Wk 12</td>
        <td>Loss</td>
        <td>7-45 vs GB</td>
      </tr>
    </table>
  </div>
</div>  <!-- close teamHistoricRecordsRow -->


<div class="teamHistoricRecordsRow">
  <h4 class="division nfc">NFC East</h4>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="cowboys" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">DAL Cowboys (3-2)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 8</td>
        <td>Win</td>
        <td>31-26 vs WAS</td>
      </tr>
      <tr class="loss">
        <td>'15</td>
        <td>Wk 7</td>
        <td>Loss</td>
        <td>20-27 vs NYG</td>
      </tr>
      <tr class="win">
        <td>'14</td>
        <td>Wk 12</td>
        <td>Win</td>
        <td>31-28 vs NYG</td>
      </tr>
      <tr class="win">
        <td>'13</td>
        <td>Wk 12</td>
        <td>Win</td>
        <td>24-21 vs NYG</td>
      </tr>
      <tr class="loss">
        <td>'12</td>
        <td>Wk 6</td>
        <td>Loss</td>
        <td>29-31 vs BAL</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="giants" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">NY Giants (3-2)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 9</td>
        <td>Win</td>
        <td>28-23 vs PHI</td>
      </tr>
      <tr class="loss">
        <td>'15</td>
        <td>Wk 12</td>
        <td>Loss</td>
        <td>14-20 vs WAS</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 9</td>
        <td>Loss</td>
        <td>24-40 vs IND</td>
      </tr>
      <tr class="win">
        <td>'13</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>24-20 vs OAK</td>
      </tr>
      <tr class="win">
        <td>'12</td>
        <td>Wk 12</td>
        <td>Win</td>
        <td>38-10 vs GB</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="eagles" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">PHI Eagles (2-3)</th>
      </tr>
      <tr class="loss">
        <td>'16</td>
        <td>Wk 5</td>
        <td>Loss</td>
        <td>23-24 vs DET</td>
      </tr>
      <tr class="win">
        <td>'15</td>
        <td>Wk 9</td>
        <td>Win</td>
        <td>33-27 vs DAL</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 8</td>
        <td>Loss</td>
        <td>20-24 vs ARI</td>
      </tr>
      <tr class="win">
        <td>'13</td>
        <td>Wk 13</td>
        <td>Win</td>
        <td>24-21 vs ARI</td>
      </tr>
      <tr class="loss">
        <td>'12</td>
        <td>Wk 8</td>
        <td>Loss</td>
        <td>17-30 vs ATL</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="redskins" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">WAS Redskins (2-3)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>26-20 vs MIN</td>
      </tr>
      <tr class="loss">
        <td>'15</td>
        <td>Wk 9</td>
        <td>Loss</td>
        <td>10-27 vs NE</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 11</td>
        <td>Loss</td>
        <td>7-27 vs TB</td>
      </tr>
      <tr class="loss">
        <td>'13</td>
        <td>Wk 6</td>
        <td>Loss</td>
        <td>16-31 vs DAL</td>
      </tr>
      <tr class="win">
        <td>'12</td>
        <td>Wk 11</td>
        <td>Win</td>
        <td>31-6 vs PHI</td>
      </tr>
    </table>
  </div>
</div>  <!-- close teamHistoricRecordsRow -->

<div class="teamHistoricRecordsRow">
  <h4 class="division nfc">NFC South</h4>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="falcons" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">ATL Falcons (4-1)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 12</td>
        <td>Win</td>
        <td>38-19 vs ARI</td>
      </tr>
      <tr class="loss">
        <td>'15</td>
        <td>Wk 11</td>
        <td>Loss</td>
        <td>21-24 vs IND</td>
      </tr>
      <tr class="win">
        <td>'14</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>27-17 vs TB</td>
      </tr>
      <tr class="win">
        <td>'13</td>
        <td>Wk 7</td>
        <td>Win</td>
        <td>31-23 vs TB</td>
      </tr>
      <tr class="win">
        <td>'12</td>
        <td>Wk 8</td>
        <td>Win</td>
        <td>30-17 vs PHI</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="panthers" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">CAR Panthers (2-3)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 8</td>
        <td>Win</td>
        <td>30-20 vs ARI</td>
      </tr>
      <tr class="win">
        <td>'15</td>
        <td>Wk 6</td>
        <td>Win</td>
        <td>27-23 vs SEA</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 13</td>
        <td>Loss</td>
        <td>13-31 vs MIN</td>
      </tr>
      <tr class="loss">
        <td>'13</td>
        <td>Wk 5</td>
        <td>Loss</td>
        <td>6-22 vs ARI</td>
      </tr>
      <tr class="loss">
        <td>'12</td>
        <td>Wk 7</td>
        <td>Loss</td>
        <td>14-19 vs DAL</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="saints" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">NO Saints (3-2)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 6</td>
        <td>Win</td>
        <td>41-38 vs CAR</td>
      </tr>
      <tr class="loss">
        <td>'15</td>
        <td>Wk 12</td>
        <td>Loss</td>
        <td>6-24 vs HOU</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 7</td>
        <td>Loss</td>
        <td>23-24 vs DET</td>
      </tr>
      <tr class="win">
        <td>'13</td>
        <td>Wk 8</td>
        <td>Win</td>
        <td>35-17 vs BUF</td>
      </tr>
      <tr class="win">
        <td>'12</td>
        <td>Wk 7</td>
        <td>Win</td>
        <td>35-28 vs TB</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="bucs" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">TB Buccaneers (2-3)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 7</td>
        <td>Win</td>
        <td>34-17 vs SF</td>
      </tr>
      <tr class="loss">
        <td>'15</td>
        <td>Wk 7</td>
        <td>Loss</td>
        <td>30-31 vs WAS</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 8</td>
        <td>Loss</td>
        <td>13-19 vs MIN</td>
      </tr>
      <tr class="loss">
        <td>'13</td>
        <td>Wk 6</td>
        <td>Loss</td>
        <td>20-31 vs PHI</td>
      </tr>
      <tr class="win">
        <td>'12</td>
        <td>Wk 6</td>
        <td>Win</td>
        <td>38-10 vs KC</td>
      </tr>
    </table>
  </div>
</div>  <!-- close teamHistoricRecordsRow -->

<div class="teamHistoricRecordsRow">
  <h4 class="division nfc">NFC West</h4>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="cardinals" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">ARI Cardinals (3-2)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>23-20 vs SF</td>
      </tr>
      <tr class="win">
        <td>'15</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>39-32 vs SEA</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 5</td>
        <td>Loss</td>
        <td>20-41 vs DEN</td>
      </tr>
      <tr class="win">
        <td>'13</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>27-24 vs HOU</td>
      </tr>
      <tr class="loss">
        <td>'12</td>
        <td>Wk 11</td>
        <td>Loss</td>
        <td>19-23 vs ATL</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="rams" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">LA Rams (2-2-1)</th>
      </tr>
      <tr class="loss">
        <td>'16</td>
        <td>Wk 9</td>
        <td>Loss</td>
        <td>10-13 vs CAR</td>
      </tr>
      <tr class="win">
        <td>'15</td>
        <td>Wk 7</td>
        <td>Win</td>
        <td>6-24 vs CLE</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 5</td>
        <td>Loss</td>
        <td>28-34 vs PHI</td>
      </tr>
      <tr class="win">
        <td>'13</td>
        <td>Wk 12</td>
        <td>Win</td>
        <td>42-21 vs CHI</td>
      </tr>
      <tr class="tie">
        <td>'12</td>
        <td>Wk 10</td>
        <td>Tie</td>
        <td>24-24 vs SF</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="fortyNiners" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">SF 49ers (0-4-1)</th>
      </tr>
      <tr class="loss">
        <td>'16</td>
        <td>Wk 9</td>
        <td>Loss</td>
        <td>23-41 vs NO</td>
      </tr>
      <tr class="loss">
        <td>'15</td>
        <td>Wk 11</td>
        <td>Loss</td>
        <td>13-29 vs SEA</td>
      </tr>
      <tr class="loss">
        <td>'14</td>
        <td>Wk 9</td>
        <td>Loss</td>
        <td>10-13 vs STL</td>
      </tr>
      <tr class="loss">
        <td>'13</td>
        <td>Wk 10</td>
        <td>Loss</td>
        <td>9-10 vs CAR</td>
      </tr>
      <tr class="tie">
        <td>'12</td>
        <td>Wk 10</td>
        <td>Tie</td>
        <td>24-24 vs STL</td>
      </tr>
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="seahawks" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle">SEA Seahawks (3-2)</th>
      </tr>
      <tr class="win">
        <td>'16</td>
        <td>Wk 6</td>
        <td>Win</td>
        <td>26-24 vs ATL</td>
      </tr>
      <tr class="loss">
        <td>'15</td>
        <td>Wk 10</td>
        <td>Loss</td>
        <td>32-39 vs ARI</td>
      </tr>
      <tr class="win">
        <td>'14</td>
        <td>Wk 5</td>
        <td>Win</td>
        <td>27-17 vs WAS</td>
      </tr>
      <tr class="win">
        <td>'13</td>
        <td>Wk 13</td>
        <td>Win</td>
        <td>34-7 vs SEA</td>
      </tr>
      <tr class="loss">
        <td>'12</td>
        <td>Wk 12</td>
        <td>Loss</td>
        <td>21-24 vs MIA</td>
      </tr>
    </table>
  </div>
</div>  <!-- close teamHistoricRecordsRow -->
<div style="clear:both;" />
<br/>

 <p>I'd like to make this the greatest bye week resource on the web.  If you have any ideas of new bye week statistics or analysis let me know.  If
   you run a blog and would like to reference this article, please feel free to use any of the images I've created.
 </p>
<div>
  <cswr:AddThis runat="server" ID="addThisArticleFooter" UseSmallIcons="true" />
</div>
    
    
<div>
    <h2>References</h2>
    <h3>New Orleans Saints</h3>
    By Keith Allison (Flickr) [<a href="https://creativecommons.org/licenses/by-sa/2.0">CC BY-SA 2.0</a>], <a href="https://commons.wikimedia.org/wiki/File%3ADrew_Brees_2015.jpg">via Wikimedia Commons</a>
    By Keith Allison (Flickr) [CC BY-SA 2.0 (https://creativecommons.org/licenses/by-sa/2.0)], via Wikimedia Commons
    <h3>Atlant Falcons</h3>
    https://vimeo.com/186102452
    By Keith Allison (Flickr) [CC BY-SA 2.0 (https://creativecommons.org/licenses/by-sa/2.0)], via Wikimedia Commons
    <h3>Denver Broncos</h3>

</div>

<div style="clear:both;"></div>
<br /><br />



    

</div>  <!-- close bye weeks page -->




  <script type="text/javascript">
    //$('textarea').click(function () {
    //  // the select() function on the DOM element will do what you want
    //  this.select();
    //  });

      $('img.downloadable').each(function () {
          var $this = $(this);
          $this.wrap('<a href="' + $this.attr('src') + '" download />')
      });
  </script>

</asp:Content>

