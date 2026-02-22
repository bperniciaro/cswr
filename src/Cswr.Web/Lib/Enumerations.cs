namespace Cswr.Web.Lib
{
    public static class Enumerations
    {
        /// <summary>
        /// Contains the possible draft formats.
        /// </summary>
        public enum DraftFormat
        {
            /// <summary>
            /// Represents a draft using the auction format.
            /// </summary>
            Auction,

            /// <summary>
            /// Represents a draft implementing a serpentine format.
            /// </summary>
            Serpentine,
        }

        /// <summary>
        /// Contains the possible scoring styles.
        /// </summary>
        public enum ScoringStyle
        {
            /// <summary>
            /// Represents standard scoring.
            /// </summary>
            Standard,

            /// <summary>
            /// Represents points per reception scoring.
            /// </summary>
            Ppr,
        }

        /// <summary>
        /// Contains the possible user messaging levels.
        /// </summary>
        public enum MessageLevel
        {
            /// <summary>
            /// Represents success.
            /// </summary>
            Success,

            /// <summary>
            /// Represents a warning.
            /// </summary>
            Warning,

            /// <summary>
            /// Represents an error.
            /// </summary>
            Error,

            /// <summary>
            /// Represents an informational message.
            /// </summary>
            Info,
        }
    }
}
