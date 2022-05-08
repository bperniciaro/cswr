using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BP.CheatSheetWarRoom.BLL.Sheets;

namespace BP.CheatSheetWarRoom.UI.UserControls
{
  public partial class NoteSummary : System.Web.UI.UserControl
  {

    public List<CheatSheetItem> AllPlayerNotes { get; set; }
    public List<CheatSheetItem> QBNotes { get; set; }
    public List<CheatSheetItem> RBNotes { get; set; }
    public List<CheatSheetItem> WRNotes { get; set; }
    public List<CheatSheetItem> TENotes { get; set; }
    public List<CheatSheetItem> KNotes { get; set; }
    public List<CheatSheetItem> DFNotes { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        LoadNotes();
      }
    }

    private void LoadNotes()
    {
      //Quarterbacks
      if (QBNotes != null)
      {
        if (QBNotes.Count > 0)
        {
          panQBNotes.Visible = true;
          repQBNotes.DataSource = QBNotes;
          repQBNotes.DataBind();
        }
      }
      //Running Backs
      if (RBNotes != null)
      {
        if (RBNotes.Count > 0)
        {
          panRBNotes.Visible = true;
          repRBNotes.DataSource = RBNotes;
          repRBNotes.DataBind();
        }
      }
      //Wide Receiver
      if (WRNotes != null)
      {
        if (WRNotes.Count > 0)
        {
          panWRNotes.Visible = true;
          repWRNotes.DataSource = WRNotes;
          repWRNotes.DataBind();
        }
      }
      //Tight Ends
      if (TENotes != null)
      {
        if (TENotes.Count > 0)
        {
          panTENotes.Visible = true;
          repTENotes.DataSource = TENotes;
          repTENotes.DataBind();
        }
      }
      //Kickers
      if (KNotes != null)
      {
        if (KNotes.Count > 0)
        {
          panKNotes.Visible = true;
          repKNotes.DataSource = KNotes;
          repKNotes.DataBind();
        }
      }
      //Defenses
      if (DFNotes != null)
      {
        if (DFNotes.Count > 0)
        {
          panDFNotes.Visible = true;
          repDFNotes.DataSource = DFNotes;
          repDFNotes.DataBind();
        }
      }
      //All Players
      if (AllPlayerNotes != null)
      {
        if (AllPlayerNotes.Count > 0)
        {
          panAllPlayerNotes.Visible = true;
          repAllPlayerNotes.DataSource = AllPlayerNotes;
          repAllPlayerNotes.DataBind();
        }
      }



    }

    protected void repNotes_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
      {
        SheetItem boundItem = (SheetItem)e.Item.DataItem;
        Label labPlayerName = (Label)e.Item.FindControl("labPlayerName");
        Label labNote = (Label)e.Item.FindControl("labNote");
        Label labRank = (Label)e.Item.FindControl("labRank");

        labRank.Text = boundItem.Seqno.ToString();
        labPlayerName.Text = boundItem.FullName;
        //labNote.Text = "\"" + boundItem.Note + "\"";
        labNote.Text = boundItem.Note;
      }
    }

  }
}