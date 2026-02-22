// <copyright file="ICheatSheetReader.cs" company="Fornits Web Solutions">
// Copyright (c) Fornits Web Solutions. All rights reserved.
// </copyright>
using Cswr.Bal.Domain;
using Cswr.Bal.Lib;
using System.Numerics;

namespace Cswr.Bal.Services.Interfaces.Readers;

/// <summary>
/// Defines an interface for retriving information about cheat sheets.
/// </summary>
public interface ICheatSheetReader
{

    /// <summary>
    /// Retrieves all cheat sheets belonging to a specific user.
    /// </summary>
    /// <param name="userName">The username of the user for which to retrieve sheets.</param>
    /// <returns>All sheets belonging to the specified user.</returns>
	Task<Result<IEnumerable<CheatSheet>>> GetCheatSheets(string userName);

    /// <summary>
    /// Retrieves the requested sheet.
    /// </summary>
    /// <param name="cheatSheetId">The unique identifier of the target sheet.</param>
    /// <returns>The target sheet specified, or null if it doesn't exist.</returns>
	Task<Result<CheatSheet>> GetCheatSheet(int cheatSheetId);
}
