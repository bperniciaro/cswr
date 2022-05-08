using System;
using System.Web.UI.WebControls;

namespace BP.CheatSheetWarRoom.UI.Admin.Sheets
{
  public partial class ManageSupplementalSources : BasePage
  {

    /// <summary>
    /// If we select a category, put the details view in edit mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSupplementalSources_SelectedIndexChanged(object sender, EventArgs e)
    {
      dvSourceDetails.ChangeMode(DetailsViewMode.Edit);
    }

    /// <summary>
    /// When a row is created, we add an onclick even to the delete button to double-check intent
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSupplementalSources_RowCreated(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        ImageButton deleteButton = e.Row.Cells[4].Controls[0] as ImageButton;
        deleteButton.OnClientClick = "if (confirm('Are you sure you want to delete this source?  If you delete this source any sheets associated with this source will also be deleted.')==false)  return false; ";
      }
    }

    /// <summary>
    /// If we delete a row, rebind the supplemental sources and put the details view back in insert mode
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSupplementalSources_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
      gvSupplementalSources.SelectedIndex = -1;
      gvSupplementalSources.DataBind();
      dvSourceDetails.ChangeMode(DetailsViewMode.Insert);
    }


    /// <summary>
    /// If we insert a new item into the details view we need to deselect the currrent item in the grid view and rebind it
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dvSourceDetails_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
      gvSupplementalSources.SelectedIndex = -1;
      gvSupplementalSources.DataBind();
    }


    /// <summary>
    /// If we update an item in the details view we need to deselect the current item in the grid view and rebind it
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dvSourceDetails_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
    {
      gvSupplementalSources.SelectedIndex = -1;
      gvSupplementalSources.DataBind();
    }

    /// <summary>
    /// If we cancel the source view we need to deselect the current item in the gridview and rebind it
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dvSourceDetails_ItemCommand(object sender, DetailsViewCommandEventArgs e)
    {
      if (e.CommandName == "Cancel")
      {
        gvSupplementalSources.SelectedIndex = -1;
        gvSupplementalSources.DataBind();
      }
    }


    protected void dvSourceDetails_DataBound(object sender, EventArgs e)
    {
      Label labHeader = (Label)dvSourceDetails.FindControl("labHeader");

      if (dvSourceDetails.CurrentMode == DetailsViewMode.Edit)
      {
        labHeader.Text = "Edit Source";
      }
      else if (dvSourceDetails.CurrentMode == DetailsViewMode.Insert)
      {
        labHeader.Text = "Insert Source";
      }
    }

  
}
}