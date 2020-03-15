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
    public class AssetTransferLogsController : Controller
    {
        private Session1Entities db = new Session1Entities();
        public AssetTransferLogsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // POST: AssetTransferLogs
        [HttpPost]
        public ActionResult Index()
        {
            var assetTransferLogs = db.AssetTransferLogs;
            return new JsonResult { Data = assetTransferLogs.ToList() };
        }


        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,AssetID,TransferDate,FromAssetSN,ToAssetSN,FromDepartmentLocationID,ToDepartmentLocationID")] AssetTransferLog assetTransferLog)
        {
            if (ModelState.IsValid)
            {
                db.AssetTransferLogs.Add(assetTransferLog);
                db.SaveChanges();
                return Json("Successfully transfer Asset!");
            }

            return Json("Unable to transfer Asset!");
        }

        [HttpPost]
        public ActionResult GetAssetLogs(long assetID)
        {
            var checkIfLogsExists = (from x in db.AssetTransferLogs
                                     where x.AssetID == assetID
                                     select x).FirstOrDefault();
            if (checkIfLogsExists != null)
            {
                var assetLogs = (from x in db.AssetTransferLogs
                                 where x.AssetID == assetID
                                 select new
                                 {
                                     TransferDate = x.TransferDate,
                                     FromLocation = db.DepartmentLocations.Where(y => y.ID == x.FromDepartmentLocationID).Select(y => y.Location.Name).FirstOrDefault(),
                                     FromAssetSN = x.FromAssetSN,
                                     ToLocation = db.DepartmentLocations.Where(y => y.ID == x.ToDepartmentLocationID).Select(y => y.Location.Name).FirstOrDefault(),
                                     ToAssetSN = x.ToAssetSN
                                 }).ToList();
                return new JsonResult { Data = assetLogs };
            }
            return Json("No details available");
            
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
