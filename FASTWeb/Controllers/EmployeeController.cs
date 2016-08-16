using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using FASTService.Model;
using FASTService.Process;
using FASTService.Enum;
using FASTWeb.Models.Employee;
using System.Web;
using System.IO;

namespace FASTWeb.Controllers
{
    public class EmployeeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            EmployeeManagementProcess employeeManagement = new EmployeeManagementProcess();
            var EmployeeList = employeeManagement.GetAllEmployee().Select(k => new EmployeeViewModel 
            {
                FirstName = k.FirstName,
                MiddleName = k.MiddleName,
                LastName = k.LastName,
                EmployeeID = k.EmployeeID,
                Gender = k.Gender,
                Position = k.Position,
                PhoneNumber = k.PhoneNumber,
                EmailAddress = k.EmailAddress,
                Department = k.Department,
                CompanyName = k.CompanyName,
                Status = k.Status,
            }).ToList();
            return View(EmployeeList);
        }

        [Authorize]
        public ActionResult ShowDepartmentEmployee(int departmentID)
        {
            EmployeeManagementProcess employeeManagement = new EmployeeManagementProcess();
            DepartmentProcess deptProcess = new DepartmentProcess();
            var EmployeeList = employeeManagement.GetAllEmployee().Where(i => i.DepartmentID == departmentID).Select(k => new EmployeeViewModel
            {
                FirstName = k.FirstName,
                MiddleName = k.MiddleName,
                LastName = k.LastName,
                EmployeeID = k.EmployeeID,
                Gender = k.Gender,
                Position = k.Position,
                PhoneNumber = k.PhoneNumber,
                EmailAddress = k.EmailAddress,
                Department = k.Department,
                CompanyName = k.CompanyName,
                Status = k.Status,
            }).ToList();

            ViewBag.Department = deptProcess.GetDepartmentDetail(departmentID).GroupName;


            return View("Index",EmployeeList);
        }

        [Authorize]
        public ActionResult NewEmployee()
        {
            DepartmentProcess department = new DepartmentProcess();
            PositionProcess position = new PositionProcess();
            ViewBag.Departments = department.GetDepartments();
            ViewBag.Positions = position.GetPositions();
            return View(new EmployeeViewModel());
        }

        [Authorize]
        [HttpPost]
        public ActionResult NewEmployee(EmployeeViewModel model)
        {
           
            EmployeeModel emodel = new EmployeeModel();
            emodel.EmployeeID = model.EmployeeID;
            emodel.FirstName = model.FirstName;
            emodel.MiddleName = model.MiddleName;
            emodel.LastName = model.LastName;
            emodel.PhoneNumber = model.PhoneNumber;
            emodel.EmailAddress = model.EmailAddress;
            emodel.Gender = model.Gender;
            emodel.PositionID = model.PositionID;
            emodel.DepartmentID = model.DepartmentID;
            if (model.Status_booleanVal)
                emodel.Status = 1;
            else
                emodel.Status = 0;

            if (model.CompanyName != null)
            {
                emodel.IsCompany = 1;
                emodel.CompanyName = model.CompanyName;
            }
            EmployeeManagementProcess employeeManagement = new EmployeeManagementProcess();
            bool success = employeeManagement.NewEmployee(emodel);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult EditEmployee(int mod)
        {
            int EmployeeID = mod;

            DepartmentProcess department = new DepartmentProcess();
            PositionProcess position = new PositionProcess();
            ViewBag.Departments = department.GetDepartments();
            ViewBag.Positions = position.GetPositions();

            EmployeeManagementProcess employeeManagement = new EmployeeManagementProcess();
            var EmployeeDetails = employeeManagement.GetAllEmployee().Where(k => k.EmployeeID == EmployeeID).Select(k => new EmployeeViewModel
            {
                FirstName = k.FirstName,
                MiddleName = k.MiddleName,
                LastName = k.LastName,
                EmployeeID = k.EmployeeID,
                Gender = k.Gender,
                Position = k.Position,
                PositionID = k.PositionID,
                PhoneNumber = k.PhoneNumber,
                EmailAddress = k.EmailAddress,
                Department = k.Department,
                DepartmentID = k.DepartmentID,
                IsCompany = k.IsCompany,
                CompanyName = k.CompanyName,
                Status = k.Status,
                Status_booleanVal = k.Status == 1 ? true:false
            }).FirstOrDefault();

            return View(EmployeeDetails);
            
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditEmployee(EmployeeViewModel model)
        {
            EmployeeModel emodel = new EmployeeModel();
            emodel.EmployeeID = model.EmployeeID;
            emodel.FirstName = model.FirstName;
            emodel.MiddleName = model.MiddleName;
            emodel.LastName = model.LastName;
            emodel.PhoneNumber = model.PhoneNumber;
            emodel.EmailAddress = model.EmailAddress;
            emodel.Gender = model.Gender;
            emodel.PositionID = model.PositionID;
            emodel.DepartmentID = model.DepartmentID;
            if (model.Status_booleanVal)
                emodel.Status = 1;
            else
                emodel.Status = 0;

            if (model.CompanyName != null)
            {
                emodel.IsCompany = 1;
                emodel.CompanyName = model.CompanyName;
            }
            EmployeeManagementProcess employeeManagement = new EmployeeManagementProcess();
            bool success = employeeManagement.UpdateEmployee(emodel);

            return RedirectToAction("MyAssets", "Employee");
        }

        [Authorize]
        public ActionResult MyAssets()
        {
            FASTService.Process.EmployeeManagementProcess employeeProcess = new EmployeeManagementProcess();
            //FASTService.Process.
            int employeeID = 0;
            Int32.TryParse(User.Identity.Name, out employeeID);

            FASTService.Model.EmployeeDashboardViewModel dashModel =
                  employeeProcess.GetEmployeeCompleteProfile(employeeID);

            



            TempData["dash"] = dashModel;

            return View(dashModel);

        }

        [HttpGet]
        public ActionResult BulkUpload()
        {
            return View();
        }

        public ActionResult UploadFile()
        {
            foreach (string upload in Request.Files)
            {
                if (!(Request.Files[upload] != null && Request.Files[upload].ContentLength > 0)) continue;
                string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/";
                string filename = Path.GetFileName(Request.Files[upload].FileName);

                // If Upload folder is not yet existing, this code will create that directory.
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                Request.Files[upload].SaveAs(Path.Combine(path, filename));
                // File is now Saved on the Upload folder of the the solution

                // Add paring code here
                
                // Add bulk upload code here
            }
            return View("BulkUpload");
        }

        public FileResult GetTemplate()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Templates/SampleExcelTemplate.xlsx";
            byte[] fileBytes = System.IO.File.ReadAllBytes(@path);
            string fileName = "Template.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }

}