using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETech.FormatDesigner
{
    public static class LParsers
    {
        public static Decimal ToRoundedDecimal(this string str)
        {
            decimal decimalvalue;
            str = str.Replace(",", "");
            bool isNum = decimal.TryParse(str, out decimalvalue);
            return (isNum) ? decimalvalue : 0;
        }
    }
}
