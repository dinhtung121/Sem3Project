using Sem3Project.Entites;
using Sem3Project.Models;
using System;
using System.Collections.Generic;
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
        public JsonResult GetEmployee(DTParameters param)
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
                data = categories.Shipper.ToList(),
                recordsFiltered = categories.RecordCount,
                recordsTotal = categories.Shipper.Count
            };
            return Json(final);

        }

    }
}