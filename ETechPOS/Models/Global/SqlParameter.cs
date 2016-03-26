using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ETech.Models.Global
{
    [Serializable]
    public class SqlParameter
    {
        public string Field
        {
            get;
            set;
        }
        public string Value
        {
            get;
            set;
        }
        public QueryCondition Condition
        {
            get;
            set;
        }
        public bool IsCommaCondition
        {
            get;
            set;
        }
        public bool AscendingOrder
        {
            get;
            set;
        }

        public SqlParameter(string field)
            : this(field, "")
        {
        }
        public SqlParameter(string field, object value)
            : this(field, value, QueryCondition.AND, true, true)
        {
        }
        public SqlParameter(string field, object value, bool ascendingOrder)
            : this(field, value, QueryCondition.AND, true, ascendingOrder)
        {
        }
        public SqlParameter(string field, object value, QueryCondition condition)
            : this(field, value, condition, false, true)
        {
        }
        public SqlParameter(string field, object value, QueryCondition condition, bool ascendingOrder)
            : this(field, value, condition, false, ascendingOrder)
        {
        }
        private SqlParameter(string field, object value, QueryCondition condition, bool isCommaCondition, bool ascendingOrder)
        {
            Field = field;
            Value = value.ToString();
            Condition = condition;
            IsCommaCondition = isCommaCondition;
            AscendingOrder = ascendingOrder;
        }
    }

    [Serializable]
    public class SqlParameters : List<SqlParameter>
    {
    }

    public enum QueryCondition
    {
        AND = 0,
        OR = 1
    }
}
