using System;
using System.Text;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class MessageBox : System.Web.UI.UserControl
  {

    public MessageType MessageType  
    {
      set {
        switch ((MessageType)value)
        {
          case MessageType.ERROR:
            tdMessageCell.Attributes["class"] = "tableInside error";
            this.Visible = true;
            SetMessage();
            break;
          case MessageType.SUCCESS:
            tdMessageCell.Attributes["class"] = "tableInside success";
            this.Visible = true;
            SetMessage();
            break;
          case MessageType.WARNING:
            tdMessageCell.Attributes["class"] = "tableInside warning";
            this.Visible = true;
            SetMessage();
            break;
          case MessageType.INSTRUCTIONS:
            tdMessageCell.Attributes["class"] = "tableInside instructions";
            this.Visible = true;
            SetMessage();
            break;
          case MessageType.NONE:
            this.Visible = false;
            break;
        }
      }
    }

    private StringBuilder _message = new StringBuilder();
    public StringBuilder Message
    {
      get
      {
        return _message;
      }
      set
      {
        _message = value;
        SetMessage();
      }
    }

    private int _width = 0;
    public int WidthPercentage
    {
      get
      {
        if (_width == 0)
        {
          return 50;
        }
        else
        {
          return _width;
        }
      }
      set
      {
        _width = value;
        ConfigureWidths();
      }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
      //SetMessage();
    }


    public void SetMessage()
    {
      this.Visible = false;
      if (this.Message != null)
      {
        if (this.Message.ToString() != String.Empty)
        {
          litMessage.Text = this.Message.ToString();
          this.Visible = true;
        }
      }
    }


    private void ConfigureWidths()
    {
      // configure the widths of the display cells
      int totalSideWidth = 100 - this.WidthPercentage;
      int eachSideWidth = totalSideWidth / 2;
      int centerWidth = 100 - totalSideWidth;

      // set the width of the table outer cells
      tdLeftSide.Attributes.Add("style", "width:" + eachSideWidth.ToString() + "%;");
      tdRightSide.Attributes.Add("style", "width:" + eachSideWidth.ToString() + "%;");
    }

  }
}