using Sem3Project.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sem3Project.Models
{
    public class ShipperModels
    {
        private NorthwindEntities db = new NorthwindEntities();

        public List<ShipperDTO> GetAllShipper()
        {
            List<ShipperDTO> list = db.Shippers.Select(e => new ShipperDTO()
            {
                ShipperID = e.ShipperID,
                CompanyName = e.CompanyName,
                Phone = e.Phone
            }).ToList();

            return list;
        }

        public ShipperDTO GetShipperById(int id)
        {
            ShipperDTO list = db.Shippers.Where(s => s.ShipperID == id).Select(e => new ShipperDTO()
            {
                ShipperID = e.ShipperID,
                CompanyName = e.CompanyName,
                Phone = e.Phone
            }).FirstOrDefault();
            return list;
        }

        public ShipperViewStore GetShipperByPage(string search, string order, int pageIndex, int pageSize)
        {
            ShipperViewStore shipper = new ShipperViewStore();
            shipper.PageIndex = pageIndex;
            shipper.PageSize = pageSize;
            shipper.Search = search;
            shipper.Order = order;

            SqlParameter paramPageSearch = new SqlParameter("@PageSearch", search);
            SqlParameter paramOrderColumn = new SqlParameter("@SortOrder", order);
            SqlParameter paramPageIndex = new SqlParameter("@PageIndex", pageIndex);
            SqlParameter paramPageSize = new SqlParameter("@PageSize", pageSize);
            SqlParameter paramRecordCount = new SqlParameter("@RecordCount", shipper.RecordCount);
            paramRecordCount.Direction = ParameterDirection.Output;
            var listResult = db.Database.SqlQuery<Shipper>("EXEC [dbo].[usp_MaintainShipperPage] @PageSearch, @SortOrder, @PageIndex, @PageSize, @RecordCount = @RecordCount OUTPUT", paramPageSearch, paramOrderColumn, paramPageIndex, paramPageSize, paramRecordCount).ToList();
            shipper.RecordCount = (int)paramRecordCount.Value;
            shipper.Shippers = listResult.Select(e => new ShipperDTO()
            {
                ShipperID = e.ShipperID,
                CompanyName = e.CompanyName,
                Phone = e.Phone
            }).ToList();

            return shipper;
        }

        public bool PostNewShipper(ShipperDTO shipper)
        {
            Shipper shipperInsert = new Shipper()
            {
                ShipperID = shipper.ShipperID,
                CompanyName = shipper.CompanyName,
                Phone = shipper.Phone

            };
            db.Shippers.Add(shipperInsert);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PutShipper(ShipperDTO shipper)
        {
            Shipper shipperInsert = db.Shippers.FirstOrDefault(s => s.ShipperID == shipper.ShipperID);

                shipperInsert.CompanyName = shipper.CompanyName;
                shipperInsert.Phone = shipper.Phone;


            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteShipper(int id)
        {
            Shipper shipper = db.Shippers.FirstOrDefault(s => s.ShipperID == id);
            if (shipper != null)
            {
                Order order = db.Orders.FirstOrDefault(s => s.ShipVia == id);

                if(order != null)
                {
                    Order_Detail details = db.Order_Details.FirstOrDefault(s => s.OrderID == order.OrderID);
                    if (details != null)
                    {
                        db.Order_Details.Remove(details);
                        db.Orders.Remove(order);

                    }
                }
                
                db.Shippers.Remove(shipper);
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }



    }
}