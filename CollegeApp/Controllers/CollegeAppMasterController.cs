using CollegeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CollegeApp.Controllers
{
    public class CollegeAppMasterController : Controller
    {
        CollegeAppDBContext context = new CollegeAppDBContext();
        // GET: CollegeAppMaster
        public ActionResult Index()
        {
            return View();
        }

        #region "Department"
        public ActionResult Department()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult Department(Department dept)
        {
            if (ModelState.IsValid)
            {
                if (dept.DepartmentId > 0) // Edit Operation
                    context.Entry(dept).State = System.Data.Entity.EntityState.Modified;
                else
                    context.Departments.Add(dept);

                context.SaveChanges();
            }
            return AllDepartments();
        }

        [HttpGet]
        public JsonResult AllDepartments()
        {
            //return Json(context.Departments.Take(10).OrderByDescending(x => x.DepartmentId).ToList(), JsonRequestBehavior.AllowGet);
            return Json(context.Departments.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EditDepartments(int DepartmentId)
        {
            return Json(context.Departments.Where(p => p.DepartmentId == DepartmentId).First(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteDepartments(int DepartmentId)
        {
            var SectionsOfThisDept = context.DeptSections.Where(p => p.DepartmentId == DepartmentId).ToList();
            if (SectionsOfThisDept.Count > 0)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new ErrorMsgViewModel() { Error = "This Department Cannot be deleted as it is associated with some Sections" }, JsonRequestBehavior.AllowGet);
            }
            {
                var DelDept = context.Departments.Where(p => p.DepartmentId == DepartmentId).AsEnumerable();
                context.Departments.RemoveRange(DelDept);
                context.SaveChanges();

                return AllDepartments();
            }
        }
        #endregion

        #region"Section"
        public ActionResult Section()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult Section(DeptSectionViewModel DeptSect)
        {
            if (ModelState.IsValid)
            {

                context.DeptSections.Add(new DeptSection { Name = DeptSect.SectionName, DepartmentId = DeptSect.DepartmentId });
                context.SaveChanges();
            }
            return AllSections();
        }

        [HttpGet]
        public JsonResult AllSections()
        {
            var Sections = from p in context.DeptSections
                           join q in context.Departments on p.DepartmentId equals q.DepartmentId
                           select new DeptSectionViewModel
                           {
                               SectionId = p.DeptSectionId,
                               SectionName = p.Name,
                               DepartmentId = p.DepartmentId,
                               DepartmentName = q.Name
                           };
            //return Json(context.Departments.Take(10).OrderByDescending(x => x.DepartmentId).ToList(), JsonRequestBehavior.AllowGet);
            return Json(Sections.ToList(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region "Student"
        public ActionResult Student()
        {
            return PartialView();
        }

        [HttpGet]
        public JsonResult AllStudents()
        {
            var Students = new List<StudentViewModel>
            {
                new StudentViewModel() { StudentId =1, StudentName="Hi",DepartmentId=1,DepartmentName="CSE",DeptSectionId=1,SectionName="A Sect" },
                new StudentViewModel() { StudentId =1, StudentName="Hello",DepartmentId=1,DepartmentName="CSE",DeptSectionId=1,SectionName="B Sect",DateOfJoin=DateTime.Now }
            };
            return Json(Students, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}