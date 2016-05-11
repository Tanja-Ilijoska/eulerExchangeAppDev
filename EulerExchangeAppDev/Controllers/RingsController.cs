using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EulerExchangeAppDev.DBContex;
using System.IO;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.Resources;
using AutoMapper;
using EulerExchangeAppDev.Models;

namespace EulerExchangeAppDev.Controllers
{
    public class RingsController : Controller
    {
        private masterEntities db = new masterEntities();

        // GET: Rings
        public ActionResult Index()
        {
            return View(db.Rings.ToList());
        }

        // GET: Rings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rings rings = db.Rings.Find(id);
            rings.GemstoneType = db.GemstoneType.Find(id);
            if (rings == null)
            {
                return HttpNotFound();
            }
            return View(rings);
        }

        // GET: Rings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RingsViewModel rings)
        {
            IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
            if (ModelState.IsValid)
            {
                Rings ring = new Rings();
                Mapper.Map(rings, ring);
                db.Rings.Add(ring);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rings);
        }

        // GET: Rings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rings rings = db.Rings.Find(id);
            rings.GemstoneType = db.GemstoneType.Find(id);
            if (rings == null)
            {
                return HttpNotFound();
            }
            return View(rings);
        }

        // POST: Rings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Number,Decription,Size,Radius,Circumference,GemstoneID")] Rings rings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rings).State = EntityState.Modified;
                db.SaveChanges();           
                return RedirectToAction("Index");
            }
            return View(rings);
        }

        // GET: Rings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rings rings = db.Rings.Find(id);
            if (rings == null)
            {
                return HttpNotFound();
            }
            return View(rings);
        }

        // POST: Rings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rings rings = db.Rings.Find(id);
            db.Rings.Remove(rings);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Async()
        {
            return View();
        }

        public ActionResult Save(IEnumerable<HttpPostedFileBase> files)
        {
            // The Name of the Upload component is "files"
            if (files != null)
            {
                foreach (var file in files)
                {
                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = Path.GetFileName(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                    // The files are not actually saved in this demo
                    // file.SaveAs(physicalPath);
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult Remove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                
                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        // System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

    }
}
