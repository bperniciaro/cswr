namespace Cswr.Web.Lib.Config;

    /// <summary>
    /// A class for mapping to configuration elements related to application caching.
    /// </summary>
    public class CacheConfig
    {
        /// <summary>
        /// The appsettings section name.
        /// </summary>
        public const string Section = "CacheConfig";

        /// <summary>
        /// Gets or sets a value indicating whether caching is enabled globally.
        /// </summary>
        public bool EnableCaching { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the numer of seconds that applicable database data should be cached
        /// </summary>
        public int CacheDurationSections { get; set; }
    }
