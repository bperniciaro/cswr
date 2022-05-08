using System;
using System.Collections.Generic;

#nullable disable

namespace Cswr.DAL.Models
{
    public partial class SportSetting
    {
        public int SportSettingId { get; set; }
        public string SportCode { get; set; }
        public string SettingCode { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }

        public virtual SportCode SportCodeNavigation { get; set; }
    }
}
