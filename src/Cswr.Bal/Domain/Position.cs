// <copyright file="Position.cs" company="Fornits Web Solutions">
// Copyright (c) Fornits Web Solutions. All rights reserved.
// </copyright>

namespace Cswr.Bal.Domain;

/// <summary>
/// Represents a position in a sport.
/// </summary>
public class Position
{
    /// <summary>
    /// Gets or sets the unique code identifying the position.
    /// </summary>
    public string PositionCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the position.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the abbreviation of the position.
    /// </summary>
    public string Abbreviation { get; set; } = string.Empty;
}
