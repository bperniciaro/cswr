using System.Configuration;
using System.Web.Configuration;

namespace BP.CheatSheetWarRoom
{
  /// <summary>
  /// Summary description for ConfigSection
  /// </summary>
  public class BlogEngineSection : ConfigurationSection
  {

    /// <summary>
    /// This property is decorated with the ConfigurationPropery attribute to indicate that it needs to
    /// be filled with settings read from the webconfig file
    /// </summary>
    [ConfigurationProperty("defaultConnectionStringName", DefaultValue = "LocalBlogSqlServer")]
    public string DefaultConnectionStringName
    {
      get { return (string)base["defaultConnectionStringName"]; }
      set { base["defaultConnectionStringName"] = value; }
    }

    public string ConnectionString
    {
      get
      {
        return WebConfigurationManager.ConnectionStrings[Globals.BlogSettings.DefaultConnectionStringName].ConnectionString;
      }
    }

    [ConfigurationProperty("providerType", DefaultValue = "BP.CheatSheetWarRoom.DAL.SqlClient.SqlBlogProvider")]
    public string ProviderType
    {
      get { return (string)base["providerType"]; }
      set { base["providerType"] = value; }
    }

  }

}