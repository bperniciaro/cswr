using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class CheatSheetItem
    {
        public int CheatSheetId { get; set; }
        public int PlayerId { get; set; }
        public short Seqno { get; set; }
        public bool? SleeperTag { get; set; }
        public bool? BustTag { get; set; }
        public bool? InjuredTag { get; set; }
        public string Note { get; set; }

        public virtual CheatSheet CheatSheet { get; set; }
        public virtual Player Player { get; set; }
    }
}
