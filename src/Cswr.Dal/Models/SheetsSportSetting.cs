using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsSportSetting
{
    public int SportSettingId { get; set; }

    public string SportCode { get; set; } = null!;

    public string SettingCode { get; set; } = null!;

    public string SettingName { get; set; } = null!;

    public string SettingValue { get; set; } = null!;

    public virtual SheetsSportCode SportCodeNavigation { get; set; } = null!;
}
