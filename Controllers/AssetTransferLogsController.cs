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
