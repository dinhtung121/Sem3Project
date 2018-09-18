
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
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public JsonResult GetEmployee(DTParameters param)
        {
            
            EmployeeViewStore employee = new EmployeeViewStore();
            employee.PageIndex = param.Start / param.Length + 1;
            employee.PageSize = param.Length;
            if (param.Search.Value == null)
            {
                employee.Search = "%%";
            }
            else
            {
                employee.Search = "%" + param.Search.Value + "%";
            }
            employee.Order = param.SortOrder;
            EmployeeViewStore categories = new EmployeeModels().GetEmployeeByPage(employee.Search, employee.Order, employee.PageIndex, employee.PageSize);

            DTResult<EmployeeDTO> final = new DTResult<EmployeeDTO>()
            {
                draw = param.Draw,
                data = categories.Employee.ToList(),
                recordsFiltered = categories.RecordCount,
                recordsTotal = categories.Employee.Count
            };
            return Json(final);

        }

        public ActionResult Details(int id)
        {
            var data = new EmployeeModels().GetEmployeeById(id);
            return PartialView(data);
        }
        public ActionResult Edit(int id)
        {

            ViewBag.ID = id;
            ViewBag.listAdmin = new EmployeeModels().GetAllEmployee().Where(s => !s.Title.Contains("Sales Representative")).ToList();

            return PartialView();
        }

        public JsonResult GetEmployeeByID(int id)
        {
            EmployeeDTO employee = new EmployeeModels().GetEmployeeById(id);

            return Json(new
            {
                employee.Address,
                employee.BirthDate,
                employee.City,
                employee.Country,
                employee.EmployeeID,
                employee.Extension,
                employee.FirstName,
                employee.HireDate,
                employee.HomePhone,
                employee.LastName,
                employee.Notes,
                employee.Photo,
                employee.PhotoPath,
                employee.PostalCode,
                employee.Region,
                employee.ReportsTo,
                employee.Title,
                employee.TitleOfCourtesy

            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {

            ViewBag.listAdmin = new EmployeeModels().GetAllEmployee().Where(s => !s.Title.Contains("Sales Representative")).ToList();
            return PartialView();
        }

        [HttpPost]
        public ActionResult Insert()
        {
            HttpPostedFileBase file = Request.Files[0];
            EmployeeDTO employee = new EmployeeDTO();
            employee.FirstName = Request.Params["FirstName"];
            employee.LastName= Request.Params["LastName"];
            employee.HireDate = DateTime.Parse(Request.Params["HireDate"]);
            employee.BirthDate = DateTime.Parse(Request.Params["BirthDate"]);
            employee.TitleOfCourtesy = Request.Params["TitleOfCourtesy"];
            employee.Title = Request.Params["Title"];
            employee.Address = Request.Params["Address"];
            employee.City = Request.Params["City"];
            employee.Country = Request.Params["Country"];
            employee.Region = Request.Params["Region"];
            employee.ReportsTo = int.Parse(Request.Params["ReportsTo"]);
            employee.PostalCode = Request.Params["PostalCode"];
            employee.HomePhone = Request.Params["HomePhone"];
            employee.Extension = Request.Params["Extension"];
            employee.Notes = Request.Params["Notes"];

            bool check = new EmployeeModels().PostNewEmployee(employee);

            if (check)
            {
                EmployeeDTO lastCate = new EmployeeModels().GetAllEmployee().LastOrDefault();
                var fileName = "";
                var imageLink = @"~/Upload/Employee/";

                if (file != null)
                {

                    fileName = Path.GetFileName(file.FileName);
                    string[] splitName = fileName.Split('.');
                    fileName = "employee" + lastCate.EmployeeID + "." + splitName[1];
                    file.SaveAs(HttpContext.Server.MapPath(imageLink) + fileName);
                }
                lastCate.PhotoPath = fileName;
                bool checkImage = new EmployeeModels().PutEmployee(lastCate);
                return Json(new { Ok = true });
            }
            else
            {
                return Json(new { Ok = false });
            }
        }


        public ActionResult Update()
        {

            int cateId = int.Parse(Request.Form["EmployeeID"]);
            EmployeeDTO employee = new EmployeeModels().GetEmployeeById(cateId);

            employee.FirstName = Request.Params["FirstName"];
            employee.LastName = Request.Params["LastName"];
            employee.HireDate = DateTime.Parse(Request.Params["HireDate"]);
            employee.BirthDate = DateTime.Parse(Request.Params["BirthDate"]);
            employee.TitleOfCourtesy = Request.Params["TitleOfCourtesy"];
            employee.Title = Request.Params["Title"];
            employee.Address = Request.Params["Address"];
            employee.City = Request.Params["City"];
            employee.Country = Request.Params["Country"];
            employee.Region = Request.Params["Region"];
            employee.ReportsTo = int.Parse(Request.Params["ReportsTo"]);
            employee.PostalCode = Request.Params["PostalCode"];
            employee.HomePhone = Request.Params["HomePhone"];
            employee.Extension = Request.Params["Extension"];
            employee.Notes = Request.Params["Notes"];

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                var fileName = "";
                var imageLink = @"~/Upload/Employee/";
                fileName = Path.GetFileName(file.FileName);
                string[] splitName = fileName.Split('.');
                fileName = "employee" + employee.EmployeeID + "." + splitName[1];
                file.SaveAs(HttpContext.Server.MapPath(imageLink) + fileName);
                employee.PhotoPath = fileName;
            }
            bool check = new EmployeeModels().PutEmployee(employee);
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

            string imageLink = new EmployeeModels().GetEmployeeById(id).PhotoPath;
            bool check = new EmployeeModels().DeleteEmployee(id);
            if (check)
            {

                var filePath = Server.MapPath("~/Upload/Employee/" + imageLink);
                FileInfo file = new FileInfo(filePath);
                if (file.Exists)
                {
                    file.Delete();
                }
                return Json(new { Ok = true });
            }
            else
            {
                return Json(new { Ok = false });
            }
        }
    }
}