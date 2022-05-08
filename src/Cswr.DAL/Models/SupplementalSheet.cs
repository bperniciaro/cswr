using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class SupplementalSheet
    {
        public SupplementalSheet()
        {
            SupplementalSheetItems = new HashSet<SupplementalSheetItem>();
        }

        public int SupplementalSheetId { get; set; }
        public string SeasonCode { get; set; }
        public int SupplementalSourceId { get; set; }
        public string SportCode { get; set; }
        public string PositionCode { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Url { get; set; }

        public virtual PositionCode PositionCodeNavigation { get; set; }
        public virtual SeasonCode SeasonCodeNavigation { get; set; }
        public virtual SportCode SportCodeNavigation { get; set; }
        public virtual SupplementalSource SupplementalSource { get; set; }
        public virtual ICollection<SupplementalSheetItem> SupplementalSheetItems { get; set; }
    }
}
