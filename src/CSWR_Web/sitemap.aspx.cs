using System.Web;
using System.Web.UI.WebControls;

namespace BP.CheatSheetWarRoom.UI
{
  public partial class Sitemap : BasePage
  {

    protected void tvSiteMapTree_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
    {
      SiteMapNode boundNode = (SiteMapNode)e.Node.DataItem;
      e.Node.ToolTip = boundNode.Description;
      if (boundNode["link"] != null)
      {
        e.Node.SelectAction = TreeNodeSelectAction.None;
      }
      if (boundNode["showNode"] == "false")
      {
        e.Node.Parent.ChildNodes.Remove(e.Node);
      }

    }
}
}