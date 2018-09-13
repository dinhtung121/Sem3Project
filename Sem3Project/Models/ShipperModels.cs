using Sem3Project.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Project.Models
{
    public class ShipperModels
    {
        private NorthwindEntities db = new NorthwindEntities();

        public List<ShipperDTO> GetAllEmployee()
        {
            List<ShipperDTO> list = db.Shippers.Select(e => new ShipperDTO()
            {
                ShipperID = e.ShipperID,
                CompanyName = e.CompanyName,
                Phone = e.Phone
            }).ToList();

            return list;
        }

        public ShipperDTO GetEmployeeById(int id)
        {
            ShipperDTO list = db.Shippers.Where(s => s.ShipperID == id).Select(e => new ShipperDTO()
            {
                ShipperID = e.ShipperID,
                CompanyName = e.CompanyName,
                Phone = e.Phone

            }).FirstOrDefault();
            return list;
        }
    }
}