using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Tools.Database
{
    class Parameter
    {     

        internal object Value { get; private set; }
        internal ParameterDirection Direction { get; private set; }

        internal Parameter(object value, ParameterDirection direction)
        {
            Value = value;
            Direction = direction;
        }
    }
}
