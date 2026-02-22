using Cswr.Bal.Domain;
using Cswr.Bal.Lib;

/// <summary>
/// Defines an interface for saving information about cheat sheets.
/// </summary>

namespace Cswr.Bal.Services.Interfaces.Writers
{
    /// <summary>
    /// Defines an interface for saving information about cheat sheets.
    /// </summary>
    public interface ICheatSheetWriter
    {
        /// <summary>
        /// Saves a cheat sheet to the database.
        /// </summary>
        /// <param name="cheatSheet">The target cheat sheet to save.</param>
        /// <returns>True if the sheet was saved successfully, false otherwise.</returns>
        Task<Result<bool>> UpdateCheatSheet(CheatSheet cheatSheet);

        /// <summary>
        /// Saves a collection of new items to a sheet.
        /// </summary>
        /// <param name="cheatSheet">The target cheat sheet to save.</param>
        /// <param name="playersToAdd">A collection of unique playerIds representing players to add to the sheet.</param>
        /// <returns>True if all items were saved successfully, false otherwise.</returns>
        Task<Result<bool>> AddItems(CheatSheet cheatSheet, List<int> playersToAdd);
    }
}
