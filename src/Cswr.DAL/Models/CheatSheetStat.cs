using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class CheatSheetStat
    {
        public int CheatSheetId { get; set; }
        public string StatCode { get; set; }

        public virtual CheatSheet CheatSheet { get; set; }
        public virtual StatCode StatCodeNavigation { get; set; }
    }
}
