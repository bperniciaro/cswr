// <copyright file="ICheatSheetItemReader.cs" company="Fornits Web Solutions">
// Copyright (c) Fornits Web Solutions. All rights reserved.
// </copyright>
using Cswr.Bal.Domain;
using Cswr.Bal.Lib;

namespace Cswr.Bal.Services.Interfaces.Readers;

/// <summary>
/// Defines an interface for retriving information about cheat sheets.
/// </summary>
public interface ICheatSheetItemReader
{

    /// <summary>
    /// Retrieves all cheat sheet items for a specific sheet.
    /// </summary>
    /// <param name="cheatSheetId">The unique id of the target sheet.</param>
    /// <returns>All items in the target sheet.</returns>
	Task<Result<IEnumerable<CheatSheetItem>>> GetCheatSheetItems(int cheatSheetId);

}
