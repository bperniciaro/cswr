// <copyright file="CheatSheetItem.cs" company="Fornits Web Solutions">
// Copyright (c) Fornits Web Solutions. All rights reserved.
// </copyright>

namespace Cswr.Bal.Domain;

/// <summary>
/// Represents an item within a cheat sheet.
/// </summary>
public class CheatSheetItem : SheetItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CheatSheetItem"/> class.
    /// </summary>
    /// <param name="cheatSheetId">The unique identifier for a cheat sheet.</param>
    /// <param name="playerId">The unique id of the player associated with the cheat sheet.</param>
    /// <param name="seqno">The sequence number for ordering the item.</param>
    /// <param name="note">The custom note associated with the item.</param>
    /// <param name="bustTag">Indicates if a user has marked a player as a bust.</param>
    /// <param name="sleeperTag">Indicates if a user has marked a player as a sleeper.</param>
    /// <param name="injuredTag">Indicates if a user has marked a player as injured.</param>
    public CheatSheetItem(int cheatSheetId, int playerId, int seqno, string note, bool? bustTag, bool? sleeperTag, bool? injuredTag)
        : base(playerId, seqno, note, bustTag, sleeperTag)
    {
        this.CheatSheetId = cheatSheetId;
        this.InjuredTag = injuredTag;
    }

    /// <summary>
    /// Gets or sets the unique identifier for a cheat sheet.
    /// </summary>
    public int CheatSheetId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the player is marked as injured.
    /// </summary>
    public bool? InjuredTag { get; set; }

    /// <summary>
    /// Gets or sets the player associated with the cheat sheet item.
    /// </summary>
    public Player Player { get; set; }
}
