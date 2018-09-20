using Sem3Project.Entites;
using Sem3Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Sem3Project.Areas.Admin.Controllers
{
    public class ShipperController : Controller
    {
        // GET: Admin/Shipper
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetShipper(DTParameters param)
        {

            ShipperViewStore shipper = new ShipperViewStore();
            shipper.PageIndex = param.Start / param.Length + 1;
            shipper.PageSize = param.Length;
            if (param.Search.Value == null)
            {
                shipper.Search = "%%";
            }
            else
            {
                shipper.Search = "%" + param.Search.Value + "%";
            }
            shipper.Order = param.SortOrder;
            ShipperViewStore categories = new ShipperModels().GetShipperByPage(shipper.Search, shipper.Order, shipper.PageIndex, shipper.PageSize);

            DTResult<ShipperDTO> final = new DTResult<ShipperDTO>()
            {
                draw = param.Draw,
                data = categories.Shippers.ToList(),
                recordsFiltered = categories.RecordCount,
                recordsTotal = categories.Shippers.Count
            };
            return Json(final);

        }

        public ActionResult Details(int id)
        {
            return PartialView(new ShipperModels().GetShipperById(id));
        }
        public ActionResult Edit(int id)
        {

            ViewBag.ID = id;
            ViewBag.listAdmin = new ShipperModels().GetAllShipper().Where(s => !s.CompanyName.Contains("VKG")).ToList();

            return PartialView();
        }

        public JsonResult GetShipperByID(int id)
        {
            ShipperDTO shipper = new ShipperModels().GetShipperById(id);

            return Json(new
            {
                shipper.ShipperID,
                shipper.CompanyName,
                shipper.Phone

            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {

            ViewBag.listAdmin = new ShipperModels().GetAllShipper().Where(s => !s.CompanyName.Contains("VKG")).ToList();
            return PartialView();
            
        }

        [HttpPost]
        public ActionResult Insert()
        {
            //HttpPostedFileBase file = Request.Files[0];
            ShipperDTO shipper = new ShipperDTO();
            //shipper.ShipperID = int.Parse(Request.Params["ShipperID"]);
            shipper.CompanyName = Request.Params["CompanyName"];
            shipper.Phone = Request.Params["Phone"];

            bool check = new ShipperModels().PostNewShipper(shipper);
            if (check)
            {
                return Json(new { Ok = true });
            }
            else
            {
                return Json(new { Ok = false });
            }


        }


        public ActionResult Update()
        {

            int cateId = int.Parse(Request.Form["ShipperID"]);
            ShipperDTO shipper = new ShipperModels().GetShipperById(cateId);

            shipper.CompanyName = Request.Params["CompanyName"];
            shipper.Phone = Request.Params["Phone"];
            
            bool check = new ShipperModels().PutShipper(shipper);
            if (check)
            {
                return Json(new { Ok = true });
            }
            else
            {
                return Json(new { Ok = false });
            }
        }

        public ActionResult Delete(int id)
        {

            bool check = new ShipperModels().DeleteShipper(id);
            if (check)
            {
                return Json(new { Ok = true });

            }
            else
            {
                return Json(new { Ok = false });

            }


        }
    }
}