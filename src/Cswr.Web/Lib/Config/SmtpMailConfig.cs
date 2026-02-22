namespace Cswr.Web.Lib.Config;

    /// <summary>
    /// A class for mapping to configuration elements related to Smtp Mail Servers.
    /// </summary>
    public class SmtpMailConfig
    {
        /// <summary>
        /// The appsettings section name.
        /// </summary>
        public const string Section = "SmtpMailConfig";

        /// <summary>
        /// Gets or sets the username user to login to the smtp server.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password used to login to the smtp server.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the host to specify when logging into the smtp server.
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the port number of the smtp server.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable ssl on an smtp connection.
        /// </summary>
        public bool EnableSsl { get; set; } = true;
    }
