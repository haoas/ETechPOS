using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETech.cls
{
    public struct RoundedDecimal
    {
        private Decimal VALUE;
        public static implicit operator Decimal(RoundedDecimal value)
        {
            return value.VALUE;
        }
        public static implicit operator RoundedDecimal(Decimal value)
        {
            RoundedDecimal _ReturnValue = new RoundedDecimal();
            //_ReturnValue.VALUE = Math.Round(value, 2, MidpointRounding.AwayFromZero);
            _ReturnValue.VALUE = Math.Truncate(value * 100) / 100;
            return _ReturnValue;
        }
        public override string ToString()
        {
            return VALUE.ToString();
        }
    }
}
