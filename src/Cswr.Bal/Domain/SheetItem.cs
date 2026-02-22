// <copyright file="SheetItem.cs" company="Fornits Web Solutions">
// Copyright (c) Fornits Web Solutions. All rights reserved.
// </copyright>

namespace Cswr.Bal.Domain;

/// <summary>
/// Represents an item within a sheet.
/// </summary>
public abstract class SheetItem
{
	/// <summary>
	/// Initializes a new instance of the <see cref="SheetItem"/> class.
	/// </summary>
	/// <param name="playerId">The unique id of the player associated with the sheet item.</param>
	/// <param name="seqno">The sequence number for ordering sheet items.</param>
	/// <param name="note">The configurable note for an item.</param>
	/// <param name="bustTag">Indicates whether the player is considered a bust.</param>
	/// <param name="sleeperTag">Indicates whether the player is considered a sleeper.</param>
	/// <param name="injuredTag">Indicates whether the player is marked as injured.</param>
	public SheetItem(int playerId, int seqno, string note, bool? bustTag, bool? sleeperTag)
    {
        this.PlayerId = playerId;
        this.Seqno = seqno;
        this.Note = note;
        this.BustTag = bustTag;
        this.SleeperTag = sleeperTag;
    }

    /// <summary>
    /// Gets or sets the unique id of the player associated with the sheet item.
    /// </summary>
	public int PlayerId { get; set; }

    /// <summary>
    /// Gets or sets the sequence number for ordering sheet items.
    /// </summary>
	public int Seqno { get; set; }

    /// <summary>
    /// Gets or sets the configurable note for an item.
    /// </summary>
	public string Note { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the player is considered a bust.
    /// </summary>
	public bool? BustTag { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the player is considered a sleepers.
	/// </summary>
	public bool? SleeperTag { get; set; }
}
