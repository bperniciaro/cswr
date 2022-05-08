<%@ Page Language="C#" MasterPageFile="~/MasterPages/NoSport.master" Theme="Web20" AutoEventWireup="true" CodeFile="faq.aspx.cs" 
  Inherits="BP.CheatSheetWarRoom.UI.FAQ" CodeFileBaseClass="BP.CheatSheetWarRoom.UI.BasePage"
  Title="Frequently Asked Questions at Cheat Sheet War Room" 
  MetaDescription="Here are answers to some of the frequent questions that we receive."
  CanonicalUrl="http://www.cheatsheetwarroom.com/faq.aspx"
  %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">

<div class="faqPage">

  <h1>FAQs</h1>

  <ol class="questions">
    <li>
      <a href="#ReorderHelp">How do I reorder my players?</a>
    </li>
    <li>
      <a href="#Mobile">Do you have a mobile application?</a>
    </li>
    <li>
      <a href="#Feature">How do I suggest a new feature?</a>
    </li>
  </ol>

  <h2>FAQ Answers</h2>
  
  <ol class="answers">
    <li>
      <p class="reQuestion">
        <a id="ReorderHelp">How do I reorder my players?</a>
      </p>
      <p>
        If you're creating a fantasy football cheat sheet, check out the instructions at the buttom of the 
        <asp:HyperLink runat="server" NavigateUrl="fantasy-football/nfl/create/custom-sheet.aspx#cheatSheetHelp">custom football cheat sheet interface</asp:HyperLink>.
        If you're creating a fantasy racing cheat sheet, check out the instructions at the bottom of the
          <asp:HyperLink runat="server" NavigateUrl="~/fantasy-racing/nascar/create/custom-sheet.aspx#cheatSheetHelp">custom racing cheat sheet interface</asp:HyperLink>.
        If you are still having problems, 
        <asp:HyperLink runat="server" NavigateUrl="~/Contact.aspx?Type=Error" Text="contact us" />
        and tell us about the browser you're using and its version number.
        We'll take a closer look at the problem and get back to you.
      </p>
    </li>

    <li>
      <p class="reQuestion">
        <a id="Mobile">Do you have a mobile application?</a>
      </p>
      <p>
        Our eventual goal is to create a <strong>responsive design</strong> which makes reordering players easier on devices with smaller screens.  In the meantime, you can still
        rank players on all touch-screen devices, but you'll need to zoom-in to a certain level of magnitude in order to make <em>grabbing</em> a drag handle easier.
      </p>
    </li>

    <li>
      <p class="reQuestion">
        <a id="Feature">How do I suggest a new feature?</a>
      </p>
      <p>
        If you would like to share your ideas on new features please use our 
        <asp:HyperLink runat="server" ID="HyperLink1" NavigateUrl="~/Contact.aspx?Type=Comment" Text="contact form" /> or post a new thread on the

        <asp:Hyperlink runat="server" ID="hlFeatureRequest" NavigateUrl="http://www.cheatsheetwarroom.com/community/Feature-Request-f8.aspx" Text="feature request forum"/>.
      </p>
    </li>

  </ol>
  



</div>


</asp:Content>

