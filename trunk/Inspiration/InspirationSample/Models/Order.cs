using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InspirationSample.Models
{
    public class Order
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}