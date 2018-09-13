using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem3Project.Entites
{
    public class OrderDetailsDTO
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        public virtual OrderDTO Order { get; set; }
        public virtual ProductDTO Product { get; set; }
    }
}
