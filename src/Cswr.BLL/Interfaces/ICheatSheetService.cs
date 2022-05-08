using System.Collections.Generic;
using Cswr.DAL.Models;

namespace Cswr.BLL
{
    public interface ICheatSheetService
    {
        IEnumerable<CheatSheet> GetCheatSheets();

        //IEnumerable<CheatSheet> GetUserSheets();

    }
}
