<%@ Page Title="2019 NFL Bye Weeks, Schedules, & Statistical Analysis" Language="C#" MasterPageFile="~/MasterPages/Sport.master" AutoEventWireup="true" 
  CodeFile="bye-weeks-mark-up.aspx.cs" Inherits="BP.CheatSheetWarRoom.UI.ByeWeeks" UseAuthorship="true" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
  MetaDescription="2019 NFL bye week schedules by week & division in various printable formats.  We also analyze historical team records following a bye week." %>
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
          <h1>2018 NFL Bye Week Schedules with Graphics and Statistical History</h1>

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
      Fantasy football owners pay special attention to bye weeks as it helps them analyze 
      <a href="https://www.cheatsheetwarroom.com/blog/fantasy-football/draft-boards">fantasy draft boards</a> and make smart selections on draft day.
  </p>
  <p class="intro"> 
    The purpose of this article is to be a <strong>comprehensive guide to the 2018 NFL
    bye week schedule</strong>.  To put the 2018 bye week schedule into perspective, I also provide 
    a statistical analysis of team performance following their bye weeks for the last five 
    seasons (2013 through 2017).
  </p>
  
  <div class="toc">
    <h2>Contents</h2>
    <table>
      <tr>
        <td>
          <p class="header">Bye Week Schedules & Graphics</p>
          <p><a href="#byeWeekInfographic">2018 Full Season Bye Weeks</a></p>
          <p><a href="#byeWeeksByWeek">2018 Bye Weeks by Week</a></p>
          <p><a href="#byeWeeksByDivision">2018 Bye Weeks by Division</a></p>
        </td>
        <td>
          <p class="header">Bye Week Statistics (2012-2016)</p>
          <p><a href="#overallWinningPercentage">Record of Teams Returning from Bye</a></p>
          <p><a href="#bestWeeksToPlay">Best Weeks to Play Post Bye Week</a></p>
          <p><a href="#bestAndWorstTeams">Team Performance After Bye Weeks</a></p>
          <%--<p><a href="#teamRecordsAfterBye">Team-Specific Game Results After Byes</a></p>--%>
        </td>
      </tr>
    </table>  

  </div>
  
  <h2>2019 NFL Bye Weeks</h2>
    
<br/>
    
  <table class="simpleTable">
      <tr>
          <th>Week</th>
          <th>Teams</th>
      </tr>
      <tr>
          <td><strong>Week 4</strong></td>
          <td>
              <a href="#jetsByeWeeks">New York Jets</a>,
              <a href="#49ersByeWeeks">San Francisco 49ers</a>
          </td>
      </tr>
      <tr>
          <td><strong>Week 5</strong></td>
          <td>
              <a href="#bearsByeWeeks">Detroit Lions</a>,
              <a href="#dolphinsByeWeeks">Miami Dolphins</a>
          </td>
      </tr>
      <tr>
          <td><strong>Week 6</strong></td>
          <td>
              <a href="#billsByeWeeks">Buffalo Bills</a>,
              <a href="#saintsByeWeeks">Chicago Bears</a>,
              <a href="#coltsByeWeeks">Indianapolis Colts</a>,
              <a href="#raidersByeWeeks">Oakland Raiders</a>
          </td>
      </tr>
      <tr>
          <td><strong>Week 7</strong></td>
          <td>
              <a href="#packersByeWeeks">Carolina Panthers</a>,
              <a href="#brownsByeWeeks">Cleveland Browns</a>,
              <a href="#steelersByeWeeks">Pittsburgh Steelers</a>,
              <a href="#bucaneersByeWeeks">Tampa Bay Buccaneers</a>
          </td>
      </tr>
      <tr>
          <td><strong>Week 8</strong></td>
          <td>
              <a href="#ravensByeWeeks">Baltimore Ravens</a>,
              <a href="#cowboysByeWeeks">Dallas Cowboys</a>
          </td>
      </tr>
      <tr>
          <td><strong>Week 9</strong></td>
          <td>
              <a href="#falconsByeWeeks">Atlanta Falcons</a>,
              <a href="#bengalsByeWeeks">Cincinnatti Bengals</a>,  
              <a href="#ramsByeWeeks">Los Angeles Rams</a>,
              <a href="#saintsByeWeeks">New Orleans Saints</a>
          </td>
      </tr>
      <tr>
          <td><strong>Week 10</strong></td>
          <td>
              <a href="#broncosByeWeeks">Denver Broncos</a>,
              <a href="#texansByeWeeks">Houston Texans</a>,
              <a href="#jaguarsByeWeeks">Jacksonville Jaguars</a>,
              <a href="#patriotsByeWeeks">New England Patriots</a>,
              <a href="#eaglesByeWeeks">Philadelphia Eagles</a>,
              <a href="#redskinsByeWeeks">Washington Redskins</a>
          </td>
      </tr>
      <tr>
          <td><strong>Week 11</strong></td>
          <td>
              <a href="#packersByeWeeks">Green Bay Packers</a>,
              <a href="#giantsByeWeeks">New York Giants</a>,
              <a href="#seahawksByeWeeks">Seattle Seahawks</a>,
              <a href="#titansByeWeeks">Tennessee Titans</a>
          </td>
      </tr>
      <tr>
          <td><strong>Week 12</strong></td>
          <td>
              <a href="#cardinalsByeWeeks">Arizona Cardinals</a>,
              <a href="#chiefsByeWeeks">Kansas City Chiefs</a>,
              <a href="#chargersByeWeeks">Los Angeles Chargers</a>,
              <a href="#vikingsByeWeeks">Minnesota Vikings</a>
          </td>
      </tr>
  </table>
    

  <br/>
  <a name="byeWeekGraphics"></a>
  <h2>Team-Specific Bye Weeks and Graphics</h2>

    <h3>Arizona Cardinals Bye Week 2019</h3>

    <p>
      Lorem ipsum dolor sit amet, usu civibus democritum ad, debet discere consequat ei mel, ei ferri concludaturque duo. Primis ponderum voluptaria nec ut. Case graeco inimicus an eam, est alia adhuc ex. Mea enim doming neglegentur ex, an graece recusabo consetetur mea. Sea ea aeque putent cotidieque.
    </p>

    <h3>Atlanta Falcons Bye Week 2019</h3>
    <h3>Baltimore Ravens Bye Week 2019</h3>
    <h3>Buffalo Bills Bye Week 2019</h3>
    <h3>Carolina Panthers Bye Week 2019</h3>
    <h3>Chicago Bears Bye Week 2019</h3>
    <h3>Cincinnati Bengals Bye Week 2019</h3>
    <h3>Cleveland Browns Bye Week 2019</h3>
    <h3>Dallas Cowboys Bye Week 2019</h3>
    <h3>Denver Broncos Bye Week 2019</h3>
    <h3>Detroit Lions Bye Week 2019</h3>
    <h3>Green Bay Packers Bye Week 2019</h3>
    <h3>Houston Texans Bye Week 2019</h3>
    <h3>Indianapolis Colts Bye Week 2019</h3>
    <h3>Jacksonville Jaguars Bye Week 2019</h3>
    <h3>Kansas City Chiefs Bye Week 2019</h3>
    <h3>Miami Dolphins Bye Week 2019</h3>
    <h3>Minnesota Vikings Bye Week 2019</h3>
    <h3>New England Patriots Bye Week 2019</h3>
    <h3>New Orleans Saints Bye Week 2019</h3>
    <h3>New York Giants Bye Week 2019</h3>
    <h3>New York Jets Bye Week 2019</h3>
    <h3>Oakland Raiders Bye Week 2019</h3>
    <h3>Philadelphia Eagles Bye Week 2019</h3>
    <h3>Pittsburgh Steelers Bye Week 2019</h3>
    <h3>St. Louis Rams Bye Week 2019</h3>
    <h3>Los Angeles Chargers Bye Week 2019</h3>
    <h3>San Francisco 49ers Bye Week 2019</h3>
    <h3>Seattle Seahawks Bye Week 2019</h3>
    <h3>Tampa Bay Buccaneers Bye Week 2019</h3>
    <h3>Tennessee Titans Bye Week 2019</h3>
    <h3>Washington Redskins Bye Week 2019</h3>

  
  
  <br/>
  <a name="byeWeekGraphics"></a>
  <h2>Bye Week Schedules and Sharable Graphics</h2>

  <p>
    I created several bye week graphics to represent the 2018 bye week schedule in various formats:
  </p>

  <ol>
    <li><a href="#byeWeeksFullSeason">Full Season Bye Weeks</a>: Provides a bird's eye view of the bye weeks over the entire NFL season.</li>
    <li><a href="#byeWeeksByWeek">Bye Schedule by Week:</a> For those tracking byes on an NFL schedule.</li>
    <li><a href="#byeWeeksByDivision">Bye Weeks by Division:</a> Easily see when each team (and their division opponents) gets their break.</li>
  </ol>


  <a name="byeWeekInfographic"></a>
  <h3>Full Season Bye Weeks</h3>

  <p>
    This graphic a high-level view of how 
      <asp:HyperLink runat="server" NavigateUrl="~/Images/Sports/Football/articles/byeweeks/nfl-bye-weeks-2019.png">2019 bye weeks</asp:HyperLink>
      are assigned to teams from Week 4 through Week 12. 
  </p>
  
  <p> 
    The 2017 NFL season had a bit of a shake-up, whereby the 
      <a href="#dolphinsByeWeeks">Miami Dolphins</a>
      and 
      <a href="#buccaneersByeWeeks">Tampa Bay Buccaneers</a>
      had their bye weeks moved to the
      first week of the season due to Hurricane Irma.</p>
    <p>In 2018, things are back to normal with bye weeks spanning Week 4 through Week 12
      of the NFL regular season.  During those 9 weeks of NFL byes, there will be either 2 teams, 4 teams, or 6 teams on a bye.
  </p>

  <div style="width:750px;margin:30px auto 20px auto;text-align:center;">
    <asp:Image runat="server" CssClass="downloadable" ImageUrl="~/Images/Sports/Football/articles/byeweeks/nfl-bye-weeks-2019.png" AlternateText="2019 NFL Bye Weeks Infographic" />
  </div>
    
  
  
<div class="byDivision" style="display: none;">

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
        </table>
      </div>
    </div>
  </div>

 </div>  <!-- close byeWksByDivisionContainer -->
    
    

  <a name="byeWeeksByWeek"></a>
  <h3>2018 Bye Weeks by Week</h3>

  <p>
    This section presents 
    <asp:HyperLink runat="server" NavigateUrl="~/Images/Sports/Football/articles/byeweeks/nfl-bye-weeks-by-week-2018.gif">bye weeks by week</asp:HyperLink>
    according to the 2018 NFL schedule.    
  </p>

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
      <tr class="byes">
        <td class="leftCol">Week 4</td>
        <td class="byeTeams">

          <table>
            <tr>

              <td>
                  <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Jets.png" AlternateText="New York Jets Helmet" />
                  <a href="#jetsByeWeeks">New York Jets</a>
              </td>
              <td>
                  <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/49ers.png" AlternateText="San Francisco 49ers Helmet" />
                  <a href="#49ersByeWeeks">San Francisco 49ers</a>
              </td>
              <td>
              </td>
            </tr>
          </table>
        </td>
      </tr>

      <!--Week 5-->
      <tr class="byes">
        <td class="leftCol">Week 5</td>
        <td class="byeTeams">
          <table>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Lions.png"  AlternateText="Detroit Lions Helmet" />
                <a href="#lionsByeWeeks">Detroit Lions</a>
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Dolphins.png" AlternateText="Miami Dolphins Helmet" />
                <a href="#dolphinsByeWeeks">Miami Dolphins</a>
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
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Bills.png" AlternateText="Buffalo Bills Helmet" />
                <a href="#billsByeWeeks">Buffalo Bills</a>
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Bears.png" AlternateText="Chicago Bears Helmet" />
                <a href="#saintsByeWeeks">Chicago Bears</a>
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Colts.png" AlternateText="Indianapolis Colts Helmet" />
                <a href="#coltsByeWeeks">Indianapolis Colts</a>
              </td>
            </tr>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Raiders.png"  AlternateText="Oakland Raiders Helmet" />
                <a href="#raidersByeWeeks">Oakland Raiders</a>
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
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Panthers.png" AlternateText="Carolina Panthers Helmet" />
                <a href="#panthersByeWeeks">Carolina Panthers</a>
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Browns.png" AlternateText="Cleveland Browns Helmet" />
                <a href="#brownsByeWeeks">Cleveland Browns</a>
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Steelers.png"  AlternateText="Pittsburgh Steelers Helmet" />
                <a href="#pittsburghByeWeeks">Pittsburgh Steelers</a>
              </td>
            </tr>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Buccaneers.png" AlternateText="Tampa Bay Buccaneers Helmet" />
                <a href="#falconsByeWeeks">Tampa Bay Buccaneers</a>
              </td>
                <td></td>
                <td></td>
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
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Ravens.png" AlternateText="Baltimore Ravens Helmet" />
                <a href="#ravensByeWeeks">Baltimore Ravens</a>
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Cowboys.png"  AlternateText="Dallas Cowboys Helmet" />
                <a href="#cowboysByeWeeks">Dallas Cowboys</a>
              </td>
              <td>
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
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Falcons.png"  AlternateText="Atlanta Falcons Helmet" />
                <a href="#falconsByeWeeks">Atlanta Falcons</a>
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Bengals.png" AlternateText="Cincinnati Bengals Helmet" />
                <a href="#bengalsByeWeeks">Cincinnati Bengals</a>
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Rams.png"  AlternateText="Los Angeles Rams Helmet" />
                <a href="#ramsByeWeeks">Los Angeles Rams</a>
              </td>
            </tr>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Saints.png"  AlternateText="New Orleans Saints Helmet" />
                <a href="#saintsByeWeeks">New Orleans Saints</a>
              </td>
              <td>
              </td>
              <td>
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
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Broncos.png" AlternateText="Denver Broncos Helmet" />
                <a href="#broncosByeWeeks">Denver Broncos</a>
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Texans.png" AlternateText="Houston Texans Helmet" />
                <a href="#texansByeWeeks">Houston Texans</a>
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Jaguars.png" AlternateText="Jacksonville Jaguars Helmet" />
                <a href="#jaguarsByeWeeks">Jacksonville Jaguars</a>
              </td>
            </tr>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Patriots.png" AlternateText="New England Patriots" />
                <a href="#patriotsByeWeeks">New England Patriots</a>
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Eagles.png" AlternateText="Philadelphia Eagles Helmet" />
                <a href="#eaglesByeWeeks">Philadelphia Eagles</a>
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Redskins.png" AlternateText="Washington Redskins Helmet" />
                <a href="#redskinsByeWeeks">Washington Redskins</a>
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
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Packers.png"  AlternateText="Green Bay Packers Helmet" />
                <a href="#packersByeWeeks">Green Bay Packers</a>
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Giants.png" AlternateText="New York Giants Helmet" />
                <a href="#giantsByeWeeks">New York Giants</a>
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Seahawks.png"  AlternateText="Seattle Seahawks Helmet" />
                <a href="#seahawksByeWeeks">Seattle Seahawks</a>
              </td>
            </tr>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Titans.png"  AlternateText="Tennessee Titans Helmet" />
                <a href="#titansByeWeeks">Tennessee Titans</a>
              </td>
                <td></td>
                <td></td>
            </tr>
          </table>
        </td>
      </tr>
      <!--Week 12-->
      <tr class="byes">
        <td class="leftCol">Week 12</td>
        <td class="byeTeams">

          <table>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Cardinals.png" AlternateText="Arizona Cardinals Helmet" />
                <a href="#cardinalsByeWeeks">Arizona Cardinals</a>
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Chiefs.png"  AlternateText="Kansas City Chiefs Helmet" />
                <a href="#chiefsByeWeeks">Kansas City Chiefs</a>
              </td>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Chargers.png"  AlternateText="Los Angeles Chargers Helmet" />
                <a href="#chargersByeWeeks">Los Angeles Chargers</a>
              </td>
            </tr>
            <tr>
              <td>
                <asp:Image runat="server" Width="40" ImageUrl="~/Images/Sports/Football/articles/byeweeks/helmets/Vikings.png" AlternateText="Minnesota Vikings Helmet" />
                <a href="#vikingsByeWeeks">Minnesota Vikings</a>
              </td>
              <td></td>
              <td></td>
            </tr>
          </table>

        </td>
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
 
    </div>
  

<br/>
<a name="byeWeeksByDivision"></a>
<h3> n vision</h3>

<p>
  For those who are more interested in bye weeks for intra-division or intra-conference play, this is the image for you. In this graphic 
    
    <asp:HyperLink runat="server" NavigateUrl="~/Images/Sports/Football/articles/byeweeks/nfl-bye-weeks-by-division-2018.gif">bye weeks are grouped by divison</asp:HyperLink> 
    and ultimately conference.
</p>
  
<p>
  For some historical perspective, the graphic also shows each team's bye week from last year (as a 
  smaller, faded number next to the 2018 bye week).  One anomaly of the 2018 season is that <strong>all AFC East teams are on a bye the 
    same week</strong>, Week 11.
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
                  <td class="dCol1">10</td>
                  <td class="dCol2">8</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Bengals -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">9</td>
                  <td class="dCol2">9</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Browns -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">11</td>
                  <td class="dCol2">7</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Steelers -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">7</td>
                  <td class="dCol2">7</td>
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
                  <td class="dCol1">11</td>
                  <td class="dCol2">6</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Dolphins -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">11</td>
                  <td class="dCol2">5</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Patriots -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">11</td>
                  <td class="dCol2">10</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Jets -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">11</td>
                  <td class="dCol2">4</td>
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
                  <td class="dCol1">10</td>
                  <td class="dCol2">10</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Colts -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">9</td>
                  <td class="dCol2">6</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Jaguars -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">9</td>
                  <td class="dCol2">10</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Titans -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">8</td>
                  <td class="dCol2">11</td>
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
                  <td class="dCol1">10</td>
                  <td class="dCol2">10</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Chiefs -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">12</td>
                  <td class="dCol2">12</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Raiders -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">7</td>
                  <td class="dCol2">6</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Chargers -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">9</td>
                  <td class="dCol2">12</td>
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
                  <td class="dCol1">5</td>
                  <td class="dCol2">6</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Lions -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">6</td>
                  <td class="dCol2">5</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Packers -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">7</td>
                  <td class="dCol2">11</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Vikings -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">10</td>
                  <td class="dCol2">12</td>
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
                  <td class="dCol1">8</td>
                  <td class="dCol2">8</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Giants -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">9</td>
                  <td class="dCol2">11</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Eagles -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">9</td>
                  <td class="dCol2">10</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Redskins -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">4</td>
                  <td class="dCol2">10</td>
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
                  <td class="dCol1">8</td>
                  <td class="dCol2">9</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Panthers -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">4</td>
                  <td class="dCol2">7</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Saints -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">6</td>
                  <td class="dCol2">9</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Bucs -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">5</td>
                  <td class="dCol2">7</td>
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
                  <td class="dCol2">12</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Rams -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">12</td>
                  <td class="dCol2">9</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- 49ers -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">11</td>
                  <td class="dCol2">4</td>
                  <td class="dCol3"></td>
                </tr>
              </table>
            </td>
            <!-- Seahawks -->
            <td>
              <table>
                <tr>
                  <td class="dCol1">7</td>
                  <td class="dCol2">11</td>
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
    <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/articles/byeweeks/bye-week-legend-2018.gif" AlternateText="Legend of Bye Week Terms" />
  </div>

</div>  <!-- close byeWksByDivisionContainer -->


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
  Over the last five years, from 2013 through the 2017 season, teams returning from a bye week have a <strong>collective record of 85-75</strong>.  
  That's 85 wins and 75 losses for a winning percentage of 53.125%.
</p>

<p>
  This trend of teams returning from a bye week having a winning percentage slightly higher than .500 has remained consistent for the last few years.
  It supports the belief that teams with fresh legs have a <em>small advantage</em> over teams that didn't get a recent week off.
</p>

<div class="winPercentage">
  <table>
    <tr>
      <td class="wins">
        84 Wins <span>(52.5%)</span>
      </td>
      <td class="losses">
        76 Losses <span>(47.5%)</span>
      </td>
      <%--<td class="ties"></td>--%>
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
  <p>
      2018 is the first time in several years that no team has a perfect 5-0 record returning from a bye.
  </p>

<h4>Best Teams After a Bye Week</h4>

  <table>
    <tr>
      <td>

        <ol>

          <li>Seattle Seahawks (4-1) </li>
          <li>Miami Dolphins (4-1)</li>
          <li>Detroit Lions (4-1)</li>
          <li>Dallas Cowboys (4-1)</li>
          <li>Arizona Cardinals (4-1) </li>
          <li>Baltimore Ravens (3-2)</li>
          <li>Buffalo Bills (3-2) </li>
          <li>Houston Texans (3-2)</li>

        </ol>

      </td>
      <td>

        <ol start="9">
          <li>Indianapolis Colts (3-2)</li>
          <li>Jacksonville Jaguars (3-2)</li>
          <li>Tennessee Titans (3-2)</li>
          <li>Denver Broncos (3-2)</li>
          <li>Kansas City Chiefs (3-2)</li>
          <li>Oakland Raiders (3-2)</li>
          <li>Green Bay Packers (3-2)</li>
          <li>Philadelphia Eagles (3-2)</li>


        </ol>
      </td>
    </tr>
  </table>


  <h4>Worst Teams After a Bye Week</h4>


  <table>
    <tr>
      <td>
        <ol start="17">
          <li>Atlanta Falcons (3-2)</li>
          <li>Carolina Panthers (3-2) </li>
          <li>New Orleans Saints (3-2)</li>
          <li>Los Angeles Rams (3-2)</li>
          <li>New England Patriots (3-2)</li>
          <li>Cincinnati Bengals (2-3)</li>
          <li>Pittsburgh Steelers (2-3)</li>
          <li>Los Angeles Chargers (2-3) </li>

        </ol>
      </td>
      <td>
        <ol start="25">
          <li>Minnesota Vikings (2-3)</li>
          <li>New York Giants (2-3)</li>
          <li>Washington Redskins (2-3)</li>
          <li>Tampa Bay Buccaneers (2-3)</li>
          <li>Cleveland Browns (1-4)</li>
          <li>New York Jets (1-4) </li>
          <li>Chicago Bears (1-4)</li>
          <li>San Francisco 49ers (0-5)</li>
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
  the graph below that teams which returned from their byes on Week 2 through Week 10 have a winning percentage at or over .500.
</p>
  
<p>
  Between Week 11 and Week 14, teams are either .500 or have a collective losing record when returning from a bye.  
</p>

<div class="bestWeeksContainer">

  <h4>Win/Loss Ratio by Weeks of Teams Returning from a Bye (2013-2017)</h4>
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

          <tr>
            <td>Week 2</td>
            <td>2</td>
            <td>0</td>
            <td>0</td>
            <td>1.0</td>
          </tr>

          <tr class="alt">
            <td>Week 5</td>
            <td>6</td>
            <td>6</td>
            <td>0</td>
            <td>.500</td>
          </tr>

          <tr>
            <td>Week 6</td>
            <td>10</td>
            <td>6</td>
            <td>0</td>
            <td>.625</td>
          </tr>

          <tr class="alt">
            <td>Week 7</td>
            <td>8</td>
            <td>5</td>
            <td>0</td>
            <td>.615</td>
          </tr>

          <tr>
            <td>Week 8</td>
            <td>7</td>
            <td>8</td>
            <td>0</td>
            <td>.467</td>
          </tr>

          <tr class="alt">
            <td>Week 9</td>
            <td>12</td>
            <td>10</td>
            <td>0</td>
            <td>.545</td>
          </tr>
          <tr>
            <td>Week 10</td>
            <td>15</td>
            <td>15</td>
            <td>0</td>
            <td>.500</td>
          </tr>
          <tr class="alt">
            <td>Week 11</td>
            <td>13</td>
            <td>9</td>
            <td>0</td>
            <td>.591</td>
          </tr>
          <tr>
            <td>Week 12</td>
            <td>8</td>
            <td>14</td>
            <td>0</td>
            <td>.364</td>
          </tr>
          <tr class="alt">
            <td>Week 13</td>
            <td>2</td>
            <td>2</td>
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
        <asp:Image runat="server" ImageUrl="~/Images/Sports/Football/articles/byeweeks/post-bye-win-loss-2018.gif" Width="500"
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

<asp:HyperLink runat="server" CssClass="imageLink" NavigateUrl="~/Images/Sports/Football/articles/byeweeks/afc-bye-week-records-2018.gif">View Image</asp:HyperLink>

<div class="teamHistoricRecordsRow">
  <h4 class="division afc">AFC North</h4>

  <div class="column">
    <table class="division">
      <tr>
        <th colspan="4" class="teamLogo"><a class="ravens" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="ravensByeWeeks">BAL Ravens (4-1)</th>
      </tr>
      <tr class="win">
        <td>'18</td>
        <td>Wk 11</td>
        <td>Win</td>
        <td>24-21 vs CIN</td>
      </tr>
      <tr class="win">
        <td>'17</td>
        <td>Wk 11</td>
        <td>Win</td>
        <td>23-0 vs GB</td>
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
</table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="bengals" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="bengalsByeWeeks">CIN Bengals (1-4)</th>
      </tr>
      <tr class="loss">
        <td>'18</td>
        <td>Wk 10</td>
        <td>Loss</td>
        <td>14-51 vs NO</td>
      </tr>
      <tr class="loss">
        <td>'17</td>
        <td>Wk 7</td>
        <td>Loss</td>
        <td>14-29 vs PIT</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="browns" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="brownsByeWeeks">CLU Browns (2-3)</th>
      </tr>
      <tr class="win">
        <td>'18</td>
        <td>Wk 12</td>
        <td>Win</td>
        <td>35-20 vs CIN</td>
      </tr>
      <tr class="loss">
        <td>'17</td>
        <td>Wk 10</td>
        <td>Loss</td>
        <td>24-38 vs DET</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="steelers" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="steelersByeWeeks">PIT Steelers (2-3)</th>
      </tr>
      <tr class="win">
        <td>'18</td>
        <td>Wk 8</td>
        <td>Win</td>
        <td>33-18 vs CLE</td>
      </tr>
      <tr class="win">
        <td>'17</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>20-17 vs IND</td>
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
        <th colspan="4" class="teamTitle" id="billsByeWeeks">BUF Bills (3-2)</th>
      </tr>
      <tr class="win">
        <td>'18</td>
        <td>Wk 12</td>
        <td>Win</td>
        <td>24-21 vs JAC</td>
      </tr>
      <tr class="win">
        <td>'17</td>
        <td>Wk 7</td>
        <td>Win</td>
        <td>30-7 vs TB</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="dolphins" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="dolphinsByeWeeks">MIA Dolphins (3-2)</th>
      </tr>
      <tr class="loss">
        <td>'18</td>
        <td>Wk 12</td>
        <td>Loss</td>
        <td>24-27 vs IND</td>
      </tr>
      <tr class="win">
        <td>'17</td>
        <td>Wk 2</td>
        <td>Win</td>
        <td>19-17 vs LAC</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="patriots" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="patriotsByeWeeks">NE Patriots (4-1)</th>
      </tr>
      <tr class="win">
        <td>'18</td>
        <td>Wk 12</td>
        <td>Win</td>
        <td>27-13 vs NYJ</td>
      </tr>
      <tr class="win">
        <td>'17</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>41-16 vs DEN</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="jets" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="jetsByeWeeks">NY Jets (1-4)</th>
      </tr>
      <tr class="loss">
        <td>'18</td>
        <td>Wk 12</td>
        <td>Loss</td>
        <td>13-27 vs NYJ</td>
      </tr>
      <tr class="loss">
        <td>'17</td>
        <td>Wk 12</td>
        <td>Loss</td>
        <td>27-35 vs CAR</td>
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
        <th colspan="4" class="teamTitle" id="texansByeWeeks">HOU Texans (4-1)</th>
      </tr>
      <tr class="win">
        <td>'18</td>
        <td>Wk 11</td>
        <td>Win</td>
        <td>23-21 vs WAS</td>
      </tr>
      <tr class="loss">
        <td>'17</td>
        <td>Wk 8</td>
        <td>Loss</td>
        <td>38-41 vs SEA</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamTitle"><a class="colts" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="coltsByeWeeks">IND Colts (3-2)</th>
      </tr>
      <tr class="win">
        <td>'18</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>29-26 vs JAC</td>
      </tr>
      <tr class="loss">
        <td>'17</td>
        <td>Wk 12</td>
        <td>Loss</td>
        <td>16-20 vs TEN</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="jaguars" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="jaguarsByeWeeks">JAC Jaguars (2-3)</th>
      </tr>
      <tr class="loss">
        <td>'18</td>
        <td>Wk 10</td>
        <td>Loss</td>
        <td>26-29 vs IND</td>
      </tr>
      <tr class="win">
        <td>'17</td>
        <td>Wk 9</td>
        <td>Win</td>
        <td>23-7 vs CIN</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="titans" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="titansByeWeeks">TEN Titans (3-2)</th>
      </tr>
      <tr class="win">
        <td>'18</td>
        <td>Wk 9</td>
        <td>Win</td>
        <td>28-14 vs DAL</td>
      </tr>
      <tr class="win">
        <td>'17</td>
        <td>Wk 9</td>
        <td>Win</td>
        <td>23-20 vs BAL</td>
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
        <th colspan="4" class="teamTitle" id="broncosByeWeeks">DEN Broncos (3-2)</th>
      </tr>
      <tr class="win">
        <td>'18</td>
        <td>Wk 11</td>
        <td>Win</td>
        <td>23-22 vs LAC</td>
      </tr>
      <tr class="loss">
        <td>'17</td>
        <td>Wk 6</td>
        <td>Loss</td>
        <td>10-23 vs NYG</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="chiefs" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="chiefsByeWeeks">KC Chiefs (4-1)</th>
      </tr>
      <tr class="win">
        <td>'18</td>
        <td>Wk 13</td>
        <td>Win</td>
        <td>40-33 vs OAK</td>
      </tr>
      <tr class="loss">
        <td>'17</td>
        <td>Wk 11</td>
        <td>Loss</td>
        <td>9-12 vs NYG</td>
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
</table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="raiders" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="raidersByeWeeks">OAK Raiders (2-3)</th>
      </tr>
      <tr class="loss">
        <td>'18</td>
        <td>Wk 11</td>
        <td>Loss</td>
        <td>9-12 vs NYG</td>
      </tr>
      <tr class="loss">
        <td>'17</td>
        <td>Wk 11</td>
        <td>Loss</td>
        <td>8-33 vs NEP</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="chargers" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="chargersByeWeeks">LA Chargers (3-2)</th>
      </tr>
      <tr class="win">
        <td>'18</td>
        <td>Wk 9</td>
        <td>Win</td>
        <td>25-17 vs SEA</td>
      </tr>
      <tr class="loss">
        <td>'17</td>
        <td>Wk 10</td>
        <td>Loss</td>
        <td>17-20 vs JAC</td>
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
    </table>
  </div>
</div>  <!-- close teamHistoricRecordsRow -->




<asp:HyperLink runat="server" CssClass="imageLink" NavigateUrl="~/Images/Sports/Football/articles/byeweeks/nfc-bye-week-records-2018.gif">View Image</asp:HyperLink>

<div class="teamHistoricRecordsRow">
  <h4 class="division nfc">NFC North</h4>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="bears" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="bearsByeWeeks">CHI Bears (0-5)</th>
      </tr>
      <tr class="loss">
        <td>'18</td>
        <td>Wk 6</td>
        <td>Loss</td>
        <td>28-31 vs MIA</td>
      </tr>
      <tr class="loss">
        <td>'17</td>
        <td>Wk 10</td>
        <td>Loss</td>
        <td>16-23 vs GB</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="lions" style="margin:auto;" /></th>
      </tr>
      <tr class="teamName">
        <th colspan="4" class="teamTitle" id="lionsByeWeeks">DET Lions (4-1)</th>
      </tr>
      <tr class="win">
        <td>'18</td>
        <td>Wk 8</td>
        <td>Win</td>
        <td>32-21 vs MIA</td>
      </tr>
      <tr class="loss">
        <td>'17</td>
        <td>Wk 8</td>
        <td>Loss</td>
        <td>15-20 vs PIT</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="packers" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="packersByeWeeks">GB Packers (2-3)</th>
      </tr>
      <tr class="loss">
        <td>'18</td>
        <td>Wk 8</td>
        <td>Loss</td>
        <td>27-29 vs LAR</td>
      </tr>
      <tr class="loss">
        <td>'17</td>
        <td>Wk 9</td>
        <td>Loss</td>
        <td>17-30 vs DET</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="vikings" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="vikingsByeWeeks">MIN Vikings (2-3)</th>
      </tr>
      <tr class="loss">
        <td>'18</td>
        <td>Wk 11</td>
        <td>Loss</td>
        <td>20-25 vs CHI</td>
      </tr>
      <tr class="win">
        <td>'17</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>38-30 vs WAS</td>
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
        <th colspan="4" class="teamTitle" id="cowboysByeWeeks">DAL Cowboys (3-2)</th>
      </tr>
      <tr class="loss">
        <td>'18</td>
        <td>Wk 9</td>
        <td>Loss</td>
        <td>14-28 vs TEN</td>
      </tr>
      <tr class="win">
        <td>'17</td>
        <td>Wk 7</td>
        <td>Win</td>
        <td>40-10 vs SF</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="giants" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="giantsByeWeeks">NY Giants (2-3)</th>
      </tr>
      <tr class="win">
        <td>'18</td>
        <td>Wk 10</td>
        <td>Win</td>
        <td>27-23 vs 49ers</td>
      </tr>
      <tr class="loss">
        <td>'17</td>
        <td>Wk 9</td>
        <td>Loss</td>
        <td>17-51 vs LAR</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="eagles" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="eaglesByeWeeks">PHI Eagles (2-3)</th>
      </tr>
      <tr class="loss">
        <td>'18</td>
        <td>Wk 10</td>
        <td>Loss</td>
        <td>20-27 vs DAL</td>
      </tr>
      <tr class="win">
        <td>'17</td>
        <td>Wk 11</td>
        <td>Win</td>
        <td>37-9 vs DAL</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="redskins" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="redskinsByeWeeks">WAS Redskins (2-3)</th>
      </tr>
      <tr class="loss">
        <td>'18</td>
        <td>Wk 5</td>
        <td>Loss</td>
        <td>19-43 vs NO</td>
      </tr>
      <tr class="win">
        <td>'17</td>
        <td>Wk 6</td>
        <td>Win</td>
        <td>26-24 vs SF</td>
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
        <th colspan="4" class="teamTitle" id="falconsByeWeeks">ATL Falcons (3-2)</th>
      </tr>
      <tr class="win">
        <td>'18</td>
        <td>Wk 9</td>
        <td>Win</td>
        <td>38-14 vs WAS</td>
      </tr>
      <tr class="loss">
        <td>'17</td>
        <td>Wk 6</td>
        <td>Loss</td>
        <td>17-20 vs MIA</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="panthers" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="panthersByeWeeks">CAR Panthers (4-1)</th>
      </tr>
      <tr class="win">
        <td>'18</td>
        <td>Wk 5</td>
        <td>Win</td>
        <td>33-31 vs NYG</td>
      </tr>
      <tr class="win">
        <td>'17</td>
        <td>Wk 12</td>
        <td>Win</td>
        <td>35-27 vs NYJ</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="saints" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="saintsByeWeeks">NO Saints (3-2)</th>
      </tr>
      <tr class="win">
        <td>'18</td>
        <td>Wk 7</td>
        <td>Win</td>
        <td>24-23 vs BAL</td>
      </tr>
      <tr class="win">
        <td>'17</td>
        <td>Wk 6</td>
        <td>Win</td>
        <td>52-38 vs DET</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="bucs" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="buccaneersByeWeeks">TB Buccaneers (2-3)</th>
      </tr>
      <tr class="loss">
        <td>'18</td>
        <td>Wk 6</td>
        <td>Loss</td>
        <td>34-39 vs ATL</td>
      </tr>
      <tr class="win">
        <td>'17</td>
        <td>Wk 2</td>
        <td>Win</td>
        <td>29-7 vs CHI</td>
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
        <th colspan="4" class="teamTitle" id="cardinalsByeWeeks">ARI Cardinals (3-2)</th>
      </tr>
      <tr class="loss">
        <td>'18</td>
        <td>Wk 10</td>
        <td>Loss</td>
        <td>14-26 vs KC</td>
      </tr>
      <tr class="win">
        <td>'17</td>
        <td>Wk 9</td>
        <td>Win</td>
        <td>20-10 vs SF</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="rams" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="ramsByeWeeks">LA Rams (3-2)</th>
      </tr>
      <tr class="win">
        <td>'18</td>
        <td>Wk 13</td>
        <td>Win</td>
        <td>30-16 vs DET</td>
      </tr>
      <tr class="win">
        <td>'17</td>
        <td>Wk 9</td>
        <td>Win</td>
        <td>51-17 vs NYG</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="fortyNiners" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="49ersByeWeeks">SF 49ers (0-5)</th>
      </tr>
      <tr class="loss">
        <td>'18</td>
        <td>Wk 12</td>
        <td>Loss</td>
        <td>9-27 vs TB</td>
      </tr>
      <tr class="loss">
        <td>'17</td>
        <td>Wk 12</td>
        <td>Loss</td>
        <td>13-24 vs SEA</td>
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
    </table>
  </div>
  <div class="column">
    <table>
      <tr>
        <th colspan="4" class="teamLogo"><a class="seahawks" style="margin:auto;" /></th>
      </tr>
      <tr>
        <th colspan="4" class="teamTitle" id="seahawksByeWeeks">SEA Seahawks (4-1)</th>
      </tr>
      <tr class="win">
        <td>'18</td>
        <td>Wk 8</td>
        <td>Win</td>
        <td>28-14 vs DET</td>
      </tr>
      <tr class="win">
        <td>'17</td>
        <td>Wk 7</td>
        <td>Win</td>
        <td>24-7 vs NYG</td>
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
    
    
<%--<div>
    <h2>References</h2>
    <h3>New Orleans Saints Imagery</h3>
    
    By Keith Allison (Flickr) [<a href="https://creativecommons.org/licenses/by-sa/2.0">CC BY-SA 2.0</a>], <a href="https://commons.wikimedia.org/wiki/File%3ADrew_Brees_2015.jpg">via Wikimedia Commons</a>
    By Keith Allison (Flickr) [CC BY-SA 2.0 (https://creativecommons.org/licenses/by-sa/2.0)], via Wikimedia Commons
    
    <h3>Atlant Falcons</h3>
    https://vimeo.com/186102452
    By Keith Allison (Flickr) [CC BY-SA 2.0 (https://creativecommons.org/licenses/by-sa/2.0)], via Wikimedia Commons
    <h3>Denver Broncos</h3>

</div>--%>

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
          $this.wrap('<a href="' + $this.attr('src') + '" download />');
      });
  </script>

</asp:Content>

