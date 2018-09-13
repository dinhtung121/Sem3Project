using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem3Project.Entites
{
    public class TerritoryDTO
    {
        public string TerritoryID { get; set; }
        public string TerritoryDescription { get; set; }
        public int RegionID { get; set; }

        public virtual RegionDTO Region { get; set; }
        public virtual ICollection<EmployeeDTO> Employees { get; set; }
    }
}
