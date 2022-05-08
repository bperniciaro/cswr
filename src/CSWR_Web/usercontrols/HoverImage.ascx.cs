using System;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class HoverImage : System.Web.UI.UserControl
  {

    public string SmallImageURL
    {
      set {
        imaSmallImage.ImageUrl = value; 
      }
    }

    public string BigImageURL
    {
      set { 
        imaBigImage.ImageUrl = value; 
      }
    }

    public string CaptionText
    {
      set
      {
        litCaptionText.Text = value;
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
  }
}