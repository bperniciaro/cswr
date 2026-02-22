// <copyright file="IPlayerReader.cs" company="Fornits Web Solutions">
// Copyright (c) Fornits Web Solutions. All rights reserved.
// </copyright>
using Cswr.Bal.Domain;
using Cswr.Bal.Lib;

namespace Cswr.Bal.Services.Interfaces.Readers;

/// <summary>
/// Defines an interface for retriving information about players.
/// </summary>
public interface IPlayerReader
{
    /// <summary>
    /// Retrieves all players that are not currently in the specified cheat sheet.
    /// </summary>
    /// <param name="cheatSheetId">The unique id of the target cheat sheet.</param>
    /// <returns>A collection of non-retired players, including rookies, that are not in the specified sheet.</returns>
    Task<Result<IEnumerable<Player>>> GetPlayersNotInSheet(int cheatSheetId);
}
