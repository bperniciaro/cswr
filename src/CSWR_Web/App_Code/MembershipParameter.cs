using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BP.CheatSheetWarRoom.NonUIControls
{
  public class MembershipParameter : Parameter
  {
    protected override object Evaluate(HttpContext context, Control control)
    {
      MembershipUser user = Membership.GetUser();
      return typeof(MembershipUser).GetProperty(propertyName).GetValue(user, null);
    }

    string propertyName;
    public string PropertyName
    {
      get { return propertyName; }
      set { propertyName = value; }
    }
  }
}