using System;
using System.Data;
using System.Configuration;
using System.Web.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


namespace BP.CheatSheetWarRoom
{
  /// <summary>
  /// Summary description for ConfigSection
  /// </summary>
  public class ForumSection : ConfigurationSection
  {


    /// <summary>
    /// This property is decorated with the ConfigurationPropery attribute to indicate that it needs to
    /// be filled with settings read from the webconfig file
    /// </summary>
    [ConfigurationProperty("defaultConnectionStringName", DefaultValue = "LocalSqlServer")]
    public string DefaultConnectionStringName
    {
      get { return (string)base["defaultConnectionStringName"]; }
      set { base["defaultConnectionStringName"] = value; }
    }

  }

}