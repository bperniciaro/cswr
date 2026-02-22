namespace Cswr.Web.Lib.Config;

    /// <summary>
    /// A class for mapping to configuration elements related to UI presentation.
    /// </summary>
    public class CswrConfig
    {
        /// <summary>
        /// The appsettings section name.
        /// </summary>
        public const string Section = "CswrConfig";

        /// <summary>
        /// Gets or sets the version of the application.
        /// </summary>
        public string AppVersion { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether to use minified files when debugging locally.
        /// </summary>
        public bool ForceMinified { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to make the application appear "down" to all users except me.
        /// </summary>
        public bool SimulateDownTime { get; set; }
    }
