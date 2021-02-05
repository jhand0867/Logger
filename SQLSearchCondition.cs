using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class SQLSearchCondition
    {
        /// <summary>
        /// Represents a row from the advanced search grid
        /// holds strings for 
        /// </summary>
        private string fieldName;
        private string condition;
        private string fieldValue;
        private string andOr;
        private string fieldOutput;

        public string FieldName { get => fieldName; set => fieldName = value; }
        public string Condition { get => condition; set => condition = value; }
        public string FieldValue { get => fieldValue; set => this.fieldValue = value; }
        public string AndOr { get => andOr; set => andOr = value; }
        public string FieldOutput { get => fieldOutput; set => fieldOutput = value; }

        public SQLSearchCondition()
        {
            AndOr = "";
            FieldName = "";
            Condition = "";
            FieldValue = "";
            FieldOutput = "";
        }

        public SQLSearchCondition(string _andOr, string _fieldName, string _condition, string _value, string _output)
        {
            AndOr = _andOr; 
            FieldName = _fieldName;
            Condition = _condition;
            FieldValue = _value;
            FieldOutput = _output;
        }

    }
}
