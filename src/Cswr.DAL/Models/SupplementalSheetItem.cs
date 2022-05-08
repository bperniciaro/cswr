using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class SupplementalSheetItem
    {
        public int SupplementalSheetId { get; set; }
        public int PlayerId { get; set; }
        public int Seqno { get; set; }
        public bool? SleeperTag { get; set; }
        public bool? BustTag { get; set; }
        public string Note { get; set; }

        public virtual Player Player { get; set; }
        public virtual SupplementalSheet SupplementalSheet { get; set; }
    }
}
