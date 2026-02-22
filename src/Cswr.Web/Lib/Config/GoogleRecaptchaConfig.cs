namespace Cswr.Web.Lib.Config;

    /// <summary>
    /// A class for mapping to configuration elements related to Google Recaptcha component.
    /// </summary>
    public class GoogleRecaptchaConfig
    {
        /// <summary>
        /// The appsettings section name.
        /// </summary>
        public const string Section = "GoogleRecaptchaConfig";

        /// <summary>
        /// Gets or sets a value for the Recaptcha secret key.
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value for the Recaptcha site key.
        /// </summary>
        public string SiteKey { get; set; } = string.Empty;
    }
