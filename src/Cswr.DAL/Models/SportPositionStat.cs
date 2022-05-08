using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class SportPositionStat
    {
        public string SportCode { get; set; }
        public string PositionCode { get; set; }
        public string StatCode { get; set; }
        public byte Seqno { get; set; }

        public virtual PositionCode PositionCodeNavigation { get; set; }
        public virtual SportCode SportCodeNavigation { get; set; }
        public virtual StatCode StatCodeNavigation { get; set; }
    }
}
