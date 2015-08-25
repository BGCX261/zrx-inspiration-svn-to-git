using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inspiration.Core.Data;

namespace Inspiration.Model
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
    }
}
