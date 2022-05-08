using System.Configuration;
using System.Web.Configuration;

namespace BP.CheatSheetWarRoom
{
  /// <summary>
  /// Summary description for ConfigSection
  /// </summary>
  public class CheatSheetWarRoomSection : ConfigurationSection
  {


    [ConfigurationProperty("version")]
    public string Version
    {
      get { return base["version"].ToString().ToLower(); }
      set { base["version"] = value; }
    }


    [ConfigurationProperty("applicationState", DefaultValue = "local")]
    public string ApplicationState
    {
      get { return base["applicationState"].ToString().ToLower(); }
      set { base["applicationState"] = value; }
    }

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

    [ConfigurationProperty("defaultCacheDuration", DefaultValue = "600")]
    public int DefaultCacheDuration
    {
      get { return (int)base["defaultCacheDuration"]; }
      set { base["defaultCacheDuration"] = value; }
    }

    [ConfigurationProperty("enableAdvertisements", DefaultValue = "true")]
    public bool EnableAdvertisements
    {
      get { return (bool)base["enableAdvertisements"]; }
      set { base["enableAdvertisements"] = value; }
    }

    [ConfigurationProperty("enableSocialMedia", DefaultValue = "true")]
    public bool EnableSocialMedia
    {
      get { return (bool)base["enableSocialMedia"]; }
      set { base["enableSocialMedia"] = value; }
    }

    [ConfigurationProperty("forceMinified", DefaultValue = "false")]
    public bool ForceMinified
    {
      get { return (bool)base["forceMinified"]; }
      set { base["forceMinified"] = value; }
    }
    
    [ConfigurationProperty("forceUnMinified", DefaultValue = "false")]
    public bool ForceUnMinified
    {
      get { return (bool)base["forceUnMinified"]; }
      set { base["forceUnMinified"] = value; }
    }

    [ConfigurationProperty("simulateDowntime", DefaultValue = "false")]
    public bool SimulateDowntime
    {
      get { return (bool)base["simulateDowntime"]; }
      set { base["simulateDowntime"] = value; }
    }

    [ConfigurationProperty("showTopBannerAd", DefaultValue = "false")]
    public bool ShowTopBannerAd
    {
      get { return (bool)base["showTopBannerAd"]; }
      set { base["showTopBannerAd"] = value; }
    }

    [ConfigurationProperty("showTrafficPop", DefaultValue = "false")]
    public bool ShowTrafficPop
    {
      get { return (bool)base["showTrafficPop"]; }
      set { base["showTrafficPop"] = value; }
    }

    [ConfigurationProperty("contactForm", IsRequired=true)]
    public ContactFormElement ContactForm
    {
      get { return (ContactFormElement)base["contactForm"]; }
    }


    [ConfigurationProperty("sheets", IsRequired = true)]
    public SheetsElement Sheets
    {
      get { return (SheetsElement)base["sheets"]; }
    }

  }

  /// <summary>
  /// CONTACT ELEMENT
  /// </summary>
  public class ContactFormElement : ConfigurationElement
  {

    [ConfigurationProperty("mailSubject", DefaultValue="Mail from CSWR: {0}")]
    public string MailSubject
    {
      get { return (string)base["mailSubject"]; }
      set { base["mailSubject"] = value; }
    }

    [ConfigurationProperty("mailTo", IsRequired=true)]
    public string MailTo
    {
      get { return (string)base["mailTo"]; }
      set { base["mailTo"] = value; }
    }

    [ConfigurationProperty("mailCC")]
    public string MailCC
    {
      get { return (string)base["mailCC"]; }
      set { base["mailCC"] = value; }
    }

  }


    

  public class SheetsElement : ConfigurationElement
  {

    [ConfigurationProperty("connectionStringName")]
    public string ConnectionStringName
    {
      get { return (string)base["connectionStringName"]; }
      set { base["connectionStringName"] = value; }
    }

    public string ConnectionString
    {
      get
      {
        // Return the base class' ConnectionString property.
        // The name of the connection string to use is retrieved from the site's 
        // custom config section and is used to read the setting from the <connectionStrings> section
        // If no connection string name is defined for the <articles> element, the
        // parent section's DefaultConnectionString prop is used.
        string connStringName = (string.IsNullOrEmpty(this.ConnectionStringName) ?
           Globals.CSWRSettings.DefaultConnectionStringName : this.ConnectionStringName);
        return WebConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
      }
    }

    [ConfigurationProperty("providerType", DefaultValue = "BP.CheatSheetWarRoom.DAL.SqlClient.SqlSheetsProvider")]
    public string ProviderType
    {
      get {return (string)base["providerType"]; }
      set { base["providerType"] = value; }
    }

    [ConfigurationProperty("pageSize", DefaultValue = "5")]
    public string PageSize
    {
      get { return (string)base["pageSize"]; }
      set { base["pageSize"] = value; }
    }

    [ConfigurationProperty("enableCaching", DefaultValue = "true")]
    public bool EnableCaching
    {
      get { return (bool)base["enableCaching"]; }
      set { base["enableCaching"] = value; }
    }


    [ConfigurationProperty("cacheDuration")]
    public int CacheDuration
    {
      get
      {
        int duration = (int)base["cacheDuration"];
        return (duration > 0 ? duration : Globals.CSWRSettings.DefaultCacheDuration);
      }
      set { base["cacheDuration"] = value; }
    }


    [ConfigurationProperty("defaultQBsPerSheet", DefaultValue = "35")]
    public int DefaultQBsPerSheet
    {
      get { return (int)base["defaultQBsPerSheet"]; }
      set { base["defaultQBsPerSheet"] = value; }
    }

    [ConfigurationProperty("defaultQBStatCodes", DefaultValue = "GAM,PAYD,PATD,RUYD,RUTD,INT,FUM,TFP,FPPG")]
    public string DefaultQBStatCodes
    {
      get { return (string)base["defaultQBStatCodes"]; }
      set { base["defaultQBStatCodes"] = value; }
    }



    [ConfigurationProperty("defaultRBsPerSheet", DefaultValue = "74")]
    public int DefaultRBsPerSheet
    {
      get { return (int)base["defaultRBsPerSheet"]; }
      set { base["defaultRBsPerSheet"] = value; }
    }

    [ConfigurationProperty("defaultRBStatCodes", DefaultValue = "GAM,RUYD,RUTD,REYD,RETD,FUM,TFP,FPPG")]
    public string DefaultRBStatCodes
    {
      get { return (string)base["defaultRBStatCodes"]; }
      set { base["defaultRBStatCodes"] = value; }
    }



    [ConfigurationProperty("defaultWRsPerSheet", DefaultValue = "74")]
    public int DefaultWRsPerSheet
    {
      get { return (int)base["defaultWRsPerSheet"]; }
      set { base["defaultWRsPerSheet"] = value; }
    }

    [ConfigurationProperty("defaultWRStatCodes", DefaultValue = "GAM,RUYD,RUTD,REYD,RETD,FUM,TFP,FPPG")]
    public string DefaultWRStatCodes
    {
      get { return (string)base["defaultWRStatCodes"]; }
      set { base["defaultWRStatCodes"] = value; }
    }



    [ConfigurationProperty("defaultTEsPerSheet", DefaultValue = "35")]
    public int DefaultTEsPerSheet
    {
      get { return (int)base["defaultTEsPerSheet"]; }
      set { base["defaultTEsPerSheet"] = value; }
    }

    [ConfigurationProperty("defaultTEStatCodes", DefaultValue = "GAM,RUYD,RUTD,REYD,RETD,FUM,TFP,FPPG")]
    public string DefaultTEStatCodes
    {
      get { return (string)base["defaultTEStatCodes"]; }
      set { base["defaultTEStatCodes"] = value; }
    }



    [ConfigurationProperty("defaultKsPerSheet", DefaultValue = "32")]
    public int DefaultKsPerSheet
    {
      get { return (int)base["defaultKsPerSheet"]; }
      set { base["defaultKsPerSheet"] = value; }
    }

    [ConfigurationProperty("defaultKStatCodes", DefaultValue = "GAM,MAFG,MIFG,MAXP,MIXP,TFP,FPPG")]
    public string DefaultKStatCodes
    {
      get { return (string)base["defaultKStatCodes"]; }
      set { base["defaultKStatCodes"] = value; }
    }


    [ConfigurationProperty("defaultDEFsPerSheet", DefaultValue = "32")]
    public int DefaultDEFsPerSheet
    {
      get { return (int)base["defaultDEFsPerSheet"]; }
      set { base["defaultDEFsPerSheet"] = value; }
    }

    [ConfigurationProperty("defaultDEFStatCodes", DefaultValue = "GAM,FREC,INT,SACK,DTD,PA,TFP,FPPG")]
    public string DefaultDFStatCodes
    {
      get { return (string)base["defaultDEFStatCodes"]; }
      set { base["defaultDFStatCodes"] = value; }
    }

    [ConfigurationProperty("defaultDRsPerSheet", DefaultValue = "32")]
    public int DefaultDRsPerSheet
    {
      get { return (int)base["defaultDRsPerSheet"]; }
      set { base["defaultDRsPerSheet"] = value; }
    }

    [ConfigurationProperty("defaultDRStatCodes", DefaultValue = "RANK,PNTS,BHND,STRT,POLE,WINS,WNGS,AFP,TP5,TP10")]
    public string DefaultDRStatCodes
    {
      get { return (string)base["defaultDRStatCodes"]; }
      set { base["defaultDRStatCodes"] = value; }
    }

    [ConfigurationProperty("defaultSportCode", DefaultValue = "FOO")]
    public string DefaultSportCode
    {
      get { return (string)base["defaultSportCode"]; }
      set { base["defaultSportCode"] = value; }
    }




  }

}