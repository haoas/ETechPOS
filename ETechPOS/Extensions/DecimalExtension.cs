using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETech.Extensions
{
    public static class DecimalExtension
    {
        public static string ToStringN2(this Decimal value)
        {
            return value.ToString("N2");
        }
        public static string ToStringG29(this Decimal value)
        {
            return value.ToString("G29");
        }
    }
}
