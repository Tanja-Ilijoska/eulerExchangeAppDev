using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EulerExchangeAppDev.Models;
using AutoMapper;
using System.Security.Claims;
using EulerExchangeAppDev.DataAccess;
using System.IO;
using PagedList;

namespace EulerExchangeAppDev.Controllers
{
    public class EngagementRingsController : Controller
    {
        private masterEntities db = new masterEntities();
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();

        // GET: EngagementRings
        public ActionResult Index(int? page)
        {
            List<EngagementRingsViewModel> engagementRingsViewModel = new List<EngagementRingsViewModel>();
            List<EngagementRings> engagementRings = db.EngagementRings.ToList();
            Mapper.Map(engagementRings, engagementRingsViewModel);

            //var ringsViewModel = Mapper.Map<List<Rings>, List<RingsViewModel>>(rings);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(engagementRingsViewModel.ToPagedList(pageNumber, pageSize));
        }

        // GET: EngagementRings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EngagementRings engagementRings = db.EngagementRings.Find(id);
            if (engagementRings == null)
            {
                return HttpNotFound();
            }
            return View(engagementRings);
        }

        // GET: EngagementRings/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "ContactPersonName");
            return View();
        }

        // POST: EngagementRings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EngagementRingsViewModel rings, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                EngagementRings ring = new EngagementRings();
                var claimsIdentity = User.Identity as ClaimsIdentity;

                UserInfo userInfo = new UserInfo(db);

                Companies company = userInfo.getLoggedCompanyId(claimsIdentity);
                ring.Companies = company;

                Mapper.Map(rings, ring);
                db.EngagementRings.Add(ring);
                db.SaveChanges();

                //save images
                SaveImages(files, company, ring);

                return RedirectToAction("Index");
            }

            return View(rings);
        }

        // GET: EngagementRings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EngagementRings engagementRings = db.EngagementRings.Find(id);
            if (engagementRings == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "ContactPersonName", engagementRings.CompanyId);
            return View(engagementRings);
        }

        // POST: EngagementRings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Number,Decription,Size,Radius,Circumference,Carat,Price,Weight,CompanyId")] EngagementRings engagementRings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(engagementRings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "ContactPersonName", engagementRings.CompanyId);
            return View(engagementRings);
        }

        // GET: EngagementRings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EngagementRings engagementRings = db.EngagementRings.Find(id);
            if (engagementRings == null)
            {
                return HttpNotFound();
            }
            return View(engagementRings);
        }

        // POST: EngagementRings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EngagementRings engagementRings = db.EngagementRings.Find(id);
            db.EngagementRings.Remove(engagementRings);
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

        public ActionResult SaveImages(IEnumerable<HttpPostedFileBase> files, Companies company, EngagementRings ring)
        {
            // The Name of the Upload component is "files"
            if (files != null)
            {
                int i = 1;
                foreach (var file in files)
                {

                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = Path.GetFileName(file.FileName);
                    var webPath = "DataImages/EngagementRings/" + company.Id + "/engagementRing" + ring.Id;
                    var physicalPath = Server.MapPath("/") + webPath;

                    if (!System.IO.Directory.Exists(physicalPath))
                    {
                        System.IO.Directory.CreateDirectory(physicalPath);
                    }

                    // The files are not actually saved in this demo
                    physicalPath = physicalPath + "/" + i + "." + file.FileName.Split('.')[1];
                    webPath = webPath + "/" + i + "." + file.FileName.Split('.')[1];

                    file.SaveAs(physicalPath);
                    //physicalPath = physicalPath.Replace(" ", "%20");
                    EngagementRingsImageURL imageURL = new EngagementRingsImageURL();
                    imageURL.ImageURL = webPath;
                    ring.EngagementRingsImageURL.Add(imageURL);
                    i++;
                }
            }


            db.SaveChanges();

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

        public ActionResult GetImage(EngagementRingsImageURL item)
        {
            if (item == null)
                return null;

            string path = Server.MapPath("/") + item.ImageURL;
            if (!System.IO.File.Exists(path))
                return null;

            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            return File(imageByteData, "image");
        }
    }
}
