using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Project.Entites
{
    public class ShipperViewStore
    {
        public List<ShipperDTO> Shippers { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
        public string Search { get; set; }
        public string Order { get; set; }
    }
}