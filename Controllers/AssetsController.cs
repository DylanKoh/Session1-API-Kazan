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
    public class AssetsController : Controller
    {
        private Session1Entities db = new Session1Entities();

        public AssetsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // POST: Assets
        public ActionResult Index()
        {
            var assets = db.Assets;
            return new JsonResult { Data = assets.ToList() };
        }

        // POST: Assets/GetView
        [HttpPost]
        public ActionResult GetView()
        {
            var assets = (from x in db.Assets
                          select new
                          {
                              AssetName = x.AssetName,
                              AssetSN = x.AssetSN,
                              DepartmentName = x.DepartmentLocation.Department.Name,
                              WarrantyDate = x.WarrantyDate,
                              AssetGroup = x.AssetGroup.Name
                          });
            return new JsonResult { Data = assets.ToList() };
        }

        // POST: Assets/Details/5
        [HttpPost]
        public ActionResult Details(string assetSN)
        {
            var asset = (from x in db.Assets
                         where x.AssetSN == assetSN
                         select new
                         {
                             AssetName = x.AssetName,
                             AssetSN = x.AssetSN,
                             DepartmentName = x.DepartmentLocation.Department.Name,
                             LocationName = x.DepartmentLocation.Location.Name,
                             AccountableParty = x.Employee.FirstName + " " + x.Employee.LastName,
                             AssetGroup = x.AssetGroup.Name,
                             AssetDescription = x.Description,
                             WarrantyDate = x.WarrantyDate
                         }).FirstOrDefault();
            if (asset == null)
            {
                return Json("Asset does not exist!!");
            }
            return Json(asset);
        }


        // POST: Assets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,AssetSN,AssetName,DepartmentLocationID,EmployeeID,AssetGroupID,Description,WarrantyDate")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                db.Assets.Add(asset);
                db.SaveChanges();
                return Json("Asset created successful!");
            }
            return Json("Unable to create asset!");
        }

        // POST: Assets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,AssetSN,AssetName,DepartmentLocationID,EmployeeID,AssetGroupID,Description,WarrantyDate")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asset).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Edit asset successful!");
            }
            return Json("Unable to edit asset!");
        }

        // POST: Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Asset asset = db.Assets.Find(id);
            db.Assets.Remove(asset);
            db.SaveChanges();
            return Json("Delete asset successful!");
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
