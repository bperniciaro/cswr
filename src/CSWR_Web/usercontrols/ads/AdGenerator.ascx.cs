using System;
using System.Text;
using System.Web.UI.WebControls;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class AdGenerator : System.Web.UI.UserControl
  {
    public enum AdSource { GOOGLE, ADBRITE }
    public enum AdSize {S120X600, S125X125, S160X600, S200x200, S234x60, S250x250, S160X90, S300X250, S468X60, S728X90}


    public string Ad_Slot { get; set; }
    public AdSource Source { get; set; }
    public AdSize Size { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (Globals.CSWRSettings.EnableAdvertisements)
      {
        try
        {
          LoadAdvertisement();
        }
        catch (Exception ex)
        {
          int i = 0;
        }
      }
    }

    private void LoadAdvertisement()
    {
      // Determine Source
      switch (Source)
      {
        case AdSource.GOOGLE:
          panGoogleAd.Visible = true;

          // start building ad script
          Literal litAdSlot = new Literal();
          StringBuilder sbAdSlot = new StringBuilder();
          sbAdSlot.Append("<script async src=\"//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js\"></script>\n");
          sbAdSlot.Append("  <!-- Home Skyscraper Wide -->\n");
          string adStyleTag = "display:inline-block;";

          // load ad size
          switch (Size)
          {
            case AdSize.S125X125:
              adStyleTag += "width:125px;height:125px;";
              break;
            case AdSize.S120X600:

              break;
            case AdSize.S160X600:
              adStyleTag += "width:160px;height:600px;";
              break;
            case AdSize.S200x200:
              break;
            case AdSize.S234x60:
              break;
            case AdSize.S250x250:
              break;
            case AdSize.S160X90:
              break;
            case AdSize.S468X60:
              adStyleTag += "width:468px;height:60px;";
              break;
            case AdSize.S728X90:
              adStyleTag += "width:728px;height:90px;";
              break;
          }

          sbAdSlot.Append("  <ins class=\"adsbygoogle\" style=\"" + adStyleTag + "\" \n");
          sbAdSlot.Append("    data-ad-client=\"ca-pub-3703083047268836\" \n");
          sbAdSlot.Append("    data-ad-slot=\"" + this.Ad_Slot + "\"></ins> \n");
          
       
          // complete ad script
          sbAdSlot.Append("<script>\n");
          sbAdSlot.Append("(adsbygoogle = window.adsbygoogle || []).push({}); \n");
          sbAdSlot.Append("</script>\n");
          
          // add the script to the appropriate panel
          litAdSlot.Text = sbAdSlot.ToString();
          panGoogleAd.Controls.Add(litAdSlot);
          break;
        case AdSource.ADBRITE:

          // Determine Size
          //switch (Size)
          //{
          //  case AdSize.S300X250:
          //    panAdbrite300x250.Visible = true;
          //    break;
          //  case AdSize.S468X60:
          //    panAdbrite468x60.Visible = true;
          //    break;
          //}
          break;
      }

    }


  }
}