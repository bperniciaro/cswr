using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class CheatSheetPosition
    {
        public int CheatSheetId { get; set; }
        public string PositionCode { get; set; }

        public virtual CheatSheet CheatSheet { get; set; }
    }
}
