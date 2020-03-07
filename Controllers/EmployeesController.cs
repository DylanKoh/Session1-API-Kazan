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
    public class EmployeesController : Controller
    {
        private Session1Entities db = new Session1Entities();
        public EmployeesController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }
        // POST: Employees
        [HttpPost]
        public ActionResult Index()
        {
            return new JsonResult { Data = db.Employees.ToList() };
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
