using System;
using System.Collections.Generic;
using System.Text;

namespace LibBD
{
    public class DataCollection
    {
        public string Name { get; set; }
        public Types FieldType { get; set; }
        public object Value 
        {
            get
            {
                if (this.valueHasQuotes())
                    return $"'{this.Value}'";
                return this.Value;
            }
            set => this.Value = value;
            
        }
        ///CONSTRUCTOR
        ///Create a field that will help to MAP a COLUMN in a BD table.
        ///This code will be used to execute BD actions
        ///

        public DataCollection(string name, Types fieldType, object value) 
        {
            Name = name;
            FieldType = fieldType;
            Value = value;
        }
        public bool valueHasQuotes()
        {
            //INT,
            //TINYINT,
            //DOUBLE,
            //FLOAT,
            return !(this.FieldType == Types.INT || this.FieldType == Types.DOUBLE || this.FieldType == Type.TINYINT);
        }
    }
}
