namespace Cswr.Web.Lib.Config;


    public class SocialLoginConfig
    {
        /// <summary>
        /// The appsettings section name.
        /// </summary>
        public const string Section = "SocialLoginConfig";

        public IEnumerable<SocialProvider> SocialProviders { get; set; } = new List<SocialProvider>();

        public SocialProvider Facebook { get; set; } = new SocialProvider();

        public class SocialProvider
        {
            public string ClientId { get; set; } = string.Empty;
            public string ClientSecret { get; set; } = string.Empty;
        }
    }
