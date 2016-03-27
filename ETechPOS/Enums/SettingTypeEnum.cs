using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ETech.Enums
{
    public enum SettingType
    {
        [Description("Point Of Sale")]
        Pos = 1,
        [Description("Inventory")]
        Inventory = 2
    }
}
