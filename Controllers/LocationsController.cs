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
    public class LocationsController : Controller
    {
        private Session1Entities db = new Session1Entities();
        public LocationsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }
        // GET: Locations
        public ActionResult Index()
        {
            return new JsonResult { Data = db.Locations.ToList() };
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
