using System;
using System.Collections.Generic;

namespace Cswr.Dal.Models;

public partial class SheetsSetting
{
    public string SettingCode { get; set; } = null!;

    public string SettingName { get; set; } = null!;

    public string SettingValue { get; set; } = null!;
}
