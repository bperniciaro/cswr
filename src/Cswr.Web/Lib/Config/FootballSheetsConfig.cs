namespace Cswr.Web.Lib.Config;

    /// <summary>
    /// A class for mapping to configuration elements related to football cheat sheets.
    /// </summary>
    public class FootballSheetsConfig
    {
        /// <summary>
        /// The appsettings section name.
        /// </summary>
        public const string Section = "FootballSheetsConfig";

        /// <summary>
        /// Gets or sets the default number of quarterbacks to include in a football cheat sheet.
        /// </summary>
        public int DefaultQbCount { get; set; }

        /// <summary>
        /// Gets or sets the default number of running backs to include in a football cheat sheet.
        /// </summary>
        public int DefaultRbCount { get; set; }

        /// <summary>
        /// Gets or sets the default number of wide receivers to include in a football cheat sheet.
        /// </summary>
        public int DefaultWrCount { get; set; }

        /// <summary>
        /// Gets or sets the default number of tight ends to include in a football cheat sheet.
        /// </summary>
        public int DefaultTeCount { get; set; }

        /// <summary>
        /// Gets or sets the default number of kickers to include in a football cheat sheet.
        /// </summary>
        public int DefaultKCount { get; set; }

        /// <summary>
        /// Gets or sets the default number of defenses to include in a football cheat sheet.
        /// </summary>
        public int DefaultDefCount { get; set; }
    }
