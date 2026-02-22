// <copyright file="EditSheetViewModel.cs" company="Fornits Web Solutions">
// Copyright (c) Fornits Web Solutions. All rights reserved.
// </copyright>

namespace Cswr.Web.Models
{
    using Cswr.Web.Lib;

    /// <summary>
    /// A model used to edit a cheat sheet and manage items in that cheat sheet.
    /// </summary>
    public class EditSheetViewModel
    {
        /// <summary>
        /// Gets or sets the items that are currently in the sheet.
        /// </summary>
        public List<CheatSheetItemDetails> CurrentSheetItems { get; set; } = new ();

        /// <summary>
        /// Gets or sets the players that can be added to the sheet.
        /// </summary>
        public List<PlayerDetails> AvailablePlayers { get; set; } = new ();

        /// <summary>
        /// Gets or sets the unique id for the sheet.
        /// </summary>
        public int CheatSheetId { get; set; }

        /// <summary>
        /// Gets or sets the username of the user who created the sheet.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Gets or sets the season that the sheet is relevant.
        /// </summary>
        public string SeasonCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the sport on which the sheet is based.
        /// </summary>
        public string SportCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name given to the cheat sheet.
        /// </summary>
        public string SheetName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the season on which to base the stats of the sheet.
        /// </summary>
        public string StatsSeasonCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the sheet is based on a PPR league.
        /// </summary>
        public bool? PprLeague { get; set; } = false;

        /// <summary>
        /// Gets or sets the scoring style of a fantasy draft.
        /// </summary>
        public string ScoringStyle
        {
            get
            {
                // If no scoring style is set, default to Standard
                if (!this.PprLeague.HasValue)
                {
                    return Enumerations.ScoringStyle.Standard.ToString();
                }

                return this.PprLeague.Value ? Enumerations.ScoringStyle.Ppr.ToString() : Enumerations.ScoringStyle.Standard.ToString();
            }

            set
            {
                this.PprLeague = value == Enumerations.ScoringStyle.Ppr.ToString() ? true : false;
            }
        }

        /// <summary>
        /// Gets the possible draft styels for a fantasy draft.
        /// https://www.learnrazorpages.com/razor-pages/forms/radios
        /// </summary>
        public string[] ScoringStyles
        {
            get
            {
                return new[] { Enumerations.ScoringStyle.Standard.ToString(), Enumerations.ScoringStyle.Ppr.ToString() };
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the sheet's associated draft is auction-based.
        /// </summary>
        public bool? AuctionDraft { get; set; } = false;

        /// <summary>
        /// Gets or sets the draft style of a fantasy draft.
        /// </summary>
        public string DraftStyle
        {
            get
            {
                // If no draft style is set, default to Serpentine
                if (!this.AuctionDraft.HasValue)
                {
                    return Enumerations.DraftFormat.Serpentine.ToString();
                }

                return this.AuctionDraft.Value ? Enumerations.DraftFormat.Auction.ToString() : Enumerations.DraftFormat.Serpentine.ToString();
            }

            set
            {
                this.AuctionDraft = value == Enumerations.DraftFormat.Auction.ToString() ? true : false;
            }
        }

        /// <summary>
        /// Gets the possible draft styels for a fantasy draft.
        /// https://www.learnrazorpages.com/razor-pages/forms/radios
        /// </summary>
        public string[] DraftStyles
        {
            get
            {
                return new[] { Enumerations.DraftFormat.Serpentine.ToString(), Enumerations.DraftFormat.Auction.ToString() };
            }
        }

        /// <summary>
        /// A class for containing player details that are only needed by the associated view.
        /// </summary>
        public class PlayerDetails
        {
            /// <summary>
            /// Gets or sets the unique id of the player.
            /// </summary>
            public int PlayerId { get; set; }

            /// <summary>
            /// Gets or sets the full name of the player.
            /// </summary>
            public string FullName { get; set; } = string.Empty;
        }

        /// <summary>
        /// A class for containing cheat sheet item details that are only needed by the associated view.
        /// </summary>
        public class CheatSheetItemDetails
        {
            /// <summary>
            /// Gets or sets the unique id of the player.
            /// </summary>
            public int PlayerId { get; set; }

            /// <summary>
            /// Gets or sets the full name of the player.
            /// </summary>
            public string FullName { get; set; } = string.Empty;
        }

        public List<int> PlayersToAdd { get; set; } = new ();

        public List<int> PlayersToRemove { get; set; } = new();

    }
}
