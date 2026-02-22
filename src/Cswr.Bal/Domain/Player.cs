// <copyright file="Player.cs" company="Fornits Web Solutions">
// Copyright (c) Fornits Web Solutions. All rights reserved.
// </copyright>

using Cswr.Globals;

namespace Cswr.Bal.Domain;

/// <summary>
/// Represents a sports player.
/// </summary>
public class Player
{
    /// <summary>
    /// Gets or sets the unique id for a player.
    /// </summary>
    public int PlayerId { get; set; }

    /// <summary>
    /// Gets or sets the unique code representing the player's sport.
    /// </summary>
    public string SportCode { get; set; } = null!;

    /// <summary>
    /// Gets or sets the unique code representing the player's position.
    /// </summary>
    public string PositionCode { get; set; } = null!;

    /// <summary>
    /// Gets or sets the first name of the player.
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Gets the full name of the player.
    /// </summary>
    public string FullName
    {
        get
        {
            string fullName;
            if (!this.IsDefensiveTeamPlayer)
            {
                if ((this.MiddleName != string.Empty) && (this.MiddleName != "X"))
                {
                    fullName = this.FirstName + " " + this.MiddleName + " " + this.LastName;
                }
                else
                {
                    fullName = this.FirstName + " " + this.LastName;
                }
            }
            else
            {
                fullName = this.FirstName;
            }
            return fullName;
        }
    }

    /// <summary>
    /// Gets or sets the last name of the player.
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the middle name of the player.
    /// </summary>
    public string MiddleName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the unique code representing hte player's team.
    /// </summary>
    public string TeamCode { get; set; } = null!;

    /// <summary>
    /// Gets or sets the player's number.
    /// </summary>
    public short Number { get; set; }

    /// <summary>
    /// Gets or sets the player's first year in the league.
    /// </summary>
    public DateTime FirstYear { get; set; }

    /// <summary>
    /// Gets a value indicating whether player represents a defense.
    /// </summary>
    public bool IsDefensiveTeamPlayer
    {
        get
        {
            if (this.PositionCode == Global.DefensePositionCode)
            {
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// Gets or sets the unique id used to map the player's stats to their database record.
    /// </summary>
    public int? StatMapId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the player is retired.
    /// </summary>
    public bool Retired { get; set; }

    /// <summary>
    /// Gets or sets the birth date of the player, or null if the player's birth date is not known.
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Gets or sets the twitter username of the player, or null if the player's twitter username is not known.
    /// </summary>
    public string? TwitterUsername { get; set; }


}
