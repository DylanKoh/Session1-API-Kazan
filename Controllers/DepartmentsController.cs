using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Session1_API_Kazan;

namespace Session1_API_Kazan.Controllers
{
    public class DepartmentsController : Controller
    {
        private Session1Entities db = new Session1Entities();

        public DepartmentsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // POST: Departments
        [HttpPost]
        public ActionResult Index()
        {
            return new JsonResult { Data = db.Departments.ToList() };
        }

        public ActionResult GetDepartmentLocations(long DepartmentID)
        {
            return new JsonResult { Data = db.DepartmentLocations.Where(x => x.DepartmentID == DepartmentID && x.EndDate == null).Select(x => x).ToList() };
        }

        public ActionResult DepartmentLocations()
        {
            return new JsonResult { Data = db.Departments.ToList() };
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
