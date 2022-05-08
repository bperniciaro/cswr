using System;
using System.Web.UI;

namespace BP.CheatSheetWarRoom.UI.UserControls
{

  public partial class RACNoteEditor : System.Web.UI.UserControl
  {

    private const int MAX_VISIBLE_NOTE_LENGTH = 35;

    /// <summary>
    /// This properly allows the containing class to set the note field
    /// </summary>
    private string _note = String.Empty;
    public string Note
    {
      get
      {
        return (ViewState["Note"] == null) ? String.Empty : ViewState["Note"].ToString();
      }
      set
      {
        ViewState["Note"] = value;
      }
    }

    /// <summary>
    /// The ID of the cheat sheet where the Note Editor resides
    /// </summary>
    private int _cheatSheetID = 0;
    public int CheatSheetID
    {
      get { return _cheatSheetID; }
      set { _cheatSheetID = value; }
    }

    /// <summary>
    /// The ID of the player whose note is being edited
    /// </summary>
    private int _playerID = 0;
    public int PlayerID
    {
      get { return _playerID; }
      set { _playerID = value; }
    }

    public void BuildControl()
    {
      // only show as much of the note that can be visible
      if (this.Note.Length > MAX_VISIBLE_NOTE_LENGTH)
      {
        labNote.Text = this.Note.TruncateAtWord(MAX_VISIBLE_NOTE_LENGTH);
        imaNoteSummary.ToolTip = this.Note;
      }
      else
      {
        labNote.Text = this.Note;
        imaNoteSummary.CssClass = "invisible";
      }
      hfFullNote.Value = this.Note;

      string commandArgument = this.CheatSheetID.ToString() + "-" + this.PlayerID.ToString();
      phDelete.ServiceArgument = commandArgument;
      phSave.ServiceArgument = commandArgument;

      imaCancel.CssClass = "invisible";
      phSave.CssClass = "invisible";

      // configure the initial visibility
      if (labNote.Text == String.Empty)
      {
        panViewNote.CssClass = "invisible";
        imaAdd.CssClass = "visible";
        panEditNote.CssClass = "invisible";
        imaEdit.CssClass = "invisible";
        phDelete.CssClass = "invisible";
      }
      else
      {
        //labNote.Text = _cheatSheetItem.Note;
        panViewNote.CssClass = "visible viewNoteContainer";
        imaAdd.CssClass = "invisible";
        panEditNote.CssClass = "invisible";
        phDelete.CssClass = "visible";
        imaEdit.CssClass = "visible";
      }

      /* hook up the button events */
      // Edit 
      imaEdit.Attributes.Add("onclick", "EditNote(\"" + labNote.ClientID + "\", \"" + panViewNote.ClientID + "\", \"" + panEditNote.ClientID + "\", \"" + tbEditNote.ClientID + "\", \"" + phDelete.ClientID + "\", \"" + imaEdit.ClientID + "\", \"" + phSave.ClientID + "\", \"" + imaCancel.ClientID + "\", \"" + hfFullNote.ClientID + "\");");
      // Cancel 
      imaCancel.Attributes.Add("onclick", "CancelEdit(\"" + panEditNote.ClientID + "\", \"" + panViewNote.ClientID + "\", \"" + imaAdd.ClientID + "\", \"" + labNote.ClientID + "\", \"" + phDelete.ClientID + "\", \"" + imaEdit.ClientID + "\", \"" + imaCancel.ClientID + "\", \"" + phSave.ClientID + "\");");
      // Add 
      imaAdd.Attributes.Add("onclick", "AddNote(\"" + imaAdd.ClientID + "\", \"" + panEditNote.ClientID + "\", \"" + tbEditNote.ClientID + "\", \"" + imaCancel.ClientID + "\", \"" + phSave.ClientID + "\", \"" + imaAdd.ClientID + "\");");
      // Save 
      phSave.Attributes.Add("onclick", "SaveNote(\"" + phSave.ServiceArgument + "\", \"" + tbEditNote.ClientID + "\", \"" + panEditNote.ClientID + "\", \"" + panViewNote.ClientID + "\", \"" + labNote.ClientID + "\", \"" + imaEdit.ClientID + "\", \"" + phSave.ClientID + "\", \"" + imaAdd.ClientID + "\", \"" + imaCancel.ClientID + "\", \"" + phDelete.ClientID + "\", \"" + hfFullNote.ClientID + "\", \"" + imaNoteSummary.ClientID + "\", \"" + MAX_VISIBLE_NOTE_LENGTH.ToString() + "\");");
      // Delete 
      phDelete.Attributes.Add("onclick", "return DeleteNote(\"" + phDelete.ServiceArgument + "\", \"" + panViewNote.ClientID + "\", \"" + imaAdd.ClientID + "\", \"" + labNote.ClientID + "\", \"" + panEditNote.ClientID + "\", \"" + imaEdit.ClientID + "\", \"" + phSave.ClientID + "\", \"" + phDelete.ClientID + "\", \"" + tbEditNote.ClientID + "\");");

    }




    protected void ibDelete_Click(object sender, ImageClickEventArgs e)
    {
      imaAdd.CssClass = "visible";
      phDelete.CssClass = "invisible";
      imaEdit.CssClass = "invisible";
      labNote.Text = String.Empty;
    }


  }
}