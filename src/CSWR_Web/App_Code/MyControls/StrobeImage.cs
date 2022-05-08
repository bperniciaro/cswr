using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BP.CheatSheetWarRoom.MyControls
{
  public class StrobeImage                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         : WebControl
  {
    private string _serviceArgument = String.Empty;
    public string ServiceArgument
    {
      get { return _serviceArgument; }
      set { _serviceArgument = value; }
    }

    private string _imageURL = String.Empty;
    public string ImageURL
    {
      get { return _imageURL; }
      set { _imageURL = value; }
    }

    private bool _state = false;
    public bool State
    {
      get { return _state; }
      set { _state = value; }
    }

    protected override void Render(HtmlTextWriter output)
    {
      // outer hyperlink
      HyperLink myLink = new HyperLink();
      myLink.ID = this.ClientID;
      myLink.Attributes.Add("onclick", "ChangeAndUpdate(this, '" + ServiceArgument + "');"); 
      if (State == true)
      {
        myLink.CssClass = "tagActive";
      }
      else
      {
        myLink.CssClass = "tagInactive";
      }

      // set the appropriate tooltip
      string[] arguments = ServiceArgument.Split('-');


      switch (arguments[arguments.Length - 1])
      {
        case "sleeper":
          myLink.ToolTip = (State) ? "Click to indicate this player is not a Sleeper." : "Click to indicate this player is a Sleeper.";
          break;
        case "bust":
          myLink.ToolTip = (State) ? "Click to indicate this player is not a Bust." : "Click to indicate this player is a Bust.";
          break;
        case "injured":
          myLink.ToolTip = (State) ? "Click to indicate this player is not Injured." : "Click to indicate this player is Injured.";
          break;
      }
      
      // 'active' image
      Image strobeImage = new Image();
      strobeImage.ImageUrl = ImageURL;


      myLink.Controls.Add(strobeImage);
      myLink.RenderControl(output);

    }

  }
}
