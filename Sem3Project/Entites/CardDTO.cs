using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem3Project.Entites
{
    public class CardDTO
    {
        public int CardId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> ProductNumber { get; set; }
        public string CustomerId { get; set; }

        public virtual CustomerDTO Customer { get; set; }
        public virtual ProductDTO Product { get; set; }
    }
}
