// <copyright file="UiConfig.cs" company="Fornits Web Solutions">
// Copyright (c) Fornits Web Solutions. All rights reserved.
// </copyright>

namespace Cswr.Web.Lib.Config;

    /// <summary>
    /// A class for mapping to configuration elements related to UI presentation.
    /// </summary>
    public class UiConfig
    {
        /// <summary>
        /// The appsettings section name.
        /// </summary>
        public const string Section = "UiConfig";

        /// <summary>
        /// Gets or sets a value indicating whether to show advertisments in the user interface.
        /// </summary>
        public bool EnableAdvertisements { get; set; }

        /// <summary>
        /// Gets or sets a value indicating which sport should be shown to new users by default.
        /// </summary>
        public string DefaultSportsCode { get; set; } = string.Empty;
    }
