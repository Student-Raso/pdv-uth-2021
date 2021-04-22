using System;
using System.Collections.Generic;
using System.Text;

namespace LibBD
{
    public class SearchCollection
    {
        public SearchCollection(string name, CriteriaOperator @operator, object value, bool isVarchar, LogicOperator logicContinue)
        {
            Name = name;
            Operator = @operator;
            Value = value;
            IsVarchar = isVarchar;
            LogicContinue = logicContinue;
        }

        public string Name { get; set; }
        public CriteriaOperator Operator { get; set; }

        public object Value { get; set; }

        public bool IsVarchar { get; set; }
        public LogicOperator LogicContinue { get; set; }

        public string resolveOperator() 
        {
            string res = "";
            switch (this.Operator)
            {
                case CriteriaOperator.EQUALS:
                    res = "=";
                    break;
                case CriteriaOperator.NOT_EQUALS:
                    res = "!=";
                    break;
                case CriteriaOperator.GREATER_THAN:
                    res = ">";
                    break;
                case CriteriaOperator.LESSER_THAN:
                    res = "<";
                    break;
                case CriteriaOperator.GREAT_THAN_EQ:
                    res = ">=";
                    break;
                case CriteriaOperator.LESS_THAN_EQ:
                    res = "<=";
                    break;
                case CriteriaOperator.LIKE:
                    res = " like ";
                    break;
            }
            return res;
        }

        public string varcharValue()
        {
            if (this.IsVarchar)
                return $"'{this.Value}'";
            return this.Value.ToString();
        }

        public string resolveLogicContinue()
        {
            string res = "";
            switch (this.LogicContinue)
            {
                case LogicOperator.AND:
                    res= "AND";
                break;
                case LogicOperator.OR:
                    res= "OR";
                    break;
                case LogicOperator.NOTHING:
                    res= " ";
                    break;
            }
            return res;
        }
    }
}
