using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspiration.Core.Enum
{
    public class EnumItem
    {
        public int Key { get; set; }
        public string Value { get; set; }
        public EnumItem(int key, string value)
        {
            Key = key;
            Value = value;
        }
    }

}
