using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InspirationSample.Models
{
    public class OrderItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public string OtherField1 { get; set; }
        public string OtherField2 { get; set; }
        public string OtherField3 { get; set; }
        public string OtherField4 { get; set; }
    }
}