using System;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for PregoHyperlink
/// </summary>
/// 
namespace BP.CheatSheetWarRoom.MyControls
{
  public class PregoHyperlink : HyperLink
  {

    private string _serviceArgument = String.Empty;
    public string ServiceArgument
    {
      get { return _serviceArgument; }
      set { _serviceArgument = value; }
    }
  
  }
}