using System.Collections.Generic;
using System.Linq;
using Cswr.DAL.Models;

namespace Cswr.BLL
{
    public class CheatSheetService : ICheatSheetService
    {

        private readonly CswrDbContext _cswrDbContext;

        public CheatSheetService(CswrDbContext cswrDbContext)
        {
            _cswrDbContext = cswrDbContext;
        }

        public IEnumerable<CheatSheet> GetCheatSheets()
        {

            return _cswrDbContext.CheatSheets.Where(c => c.Username == "Brad Perniciaro").ToList();

        }
    }
}
