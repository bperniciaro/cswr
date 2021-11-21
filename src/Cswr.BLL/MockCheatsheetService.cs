using System.Collections.Generic;
using Cswr.DAL.Models;

namespace Cswr.BLL
{
    public class MockCheatsheetService : ICheatSheetService
    {
        public IEnumerable<CheatSheet> GetCheatSheets()
        {
            var cheatSheets = new List<CheatSheet>();

            var cheatSheet1 = new CheatSheet()
            {
                CheatSheetId = 1,

                SheetName = "CheatSheet1"
            };

            var cheatSheet2 = new CheatSheet()
            {
                CheatSheetId = 2,
                SheetName = "CheatSheet2"
            };

            cheatSheets.Add(cheatSheet1);
            cheatSheets.Add(cheatSheet2);
            return cheatSheets;
        }

    }
}
