using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.Resources;
using AutoMapper;
using EulerExchangeAppDev.Models;
using System.Security.Claims;
using EulerExchangeAppDev.DataAccess;

namespace EulerExchangeAppDev.Controllers
{
    public class RingsController : Controller
    {
        private masterEntities db = new masterEntities();
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();

        // GET: Rings
        public ActionResult Index()
        {
            List<RingsViewModel> ringsViewModel = new List<RingsViewModel>();
            List<Rings> rings = db.Rings.ToList();
            Mapper.Map(rings, ringsViewModel);

            //var ringsViewModel = Mapper.Map<List<Rings>, List<RingsViewModel>>(rings);
            return View(ringsViewModel);
        }

        // GET: Rings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rings rings = db.Rings.Find(id);
            //rings.GemstoneType = db.GemstoneType.Find(id);
            if (rings == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<Rings, RingsViewModel>(rings));
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
        public ActionResult Create(RingsViewModel rings, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                Rings ring = new Rings();
                var claimsIdentity = User.Identity as ClaimsIdentity;

                UserInfo userInfo = new UserInfo(db);

                Companies company = userInfo.getLoggedCompanyId(claimsIdentity);
                ring.Companies = company;
                
                Mapper.Map(rings, ring);
                db.Rings.Add(ring);
                db.SaveChanges();

                //save images
                SaveImages(files, company, ring);

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
            //rings.GemstoneType = db.GemstoneType.Find(id);
            if (rings == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<Rings,RingsViewModel>(rings));
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
            return View(Mapper.Map<Rings, RingsViewModel>(rings));
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

        public ActionResult SaveImages(IEnumerable<HttpPostedFileBase> files, Companies company, Rings ring)
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
                    var physicalPath = Server.MapPath("/")+"DataImages/Rings/" + company.Id + "/ring" + ring.Id;
                    var webPath = "DataImages/Rings/" + company.Id + "/ring" + ring.Id;

                    if (!System.IO.Directory.Exists(physicalPath))
                    {
                        System.IO.Directory.CreateDirectory(physicalPath);
                    }

                    // The files are not actually saved in this demo
                    physicalPath = physicalPath + "/" + i + "." + file.FileName.Split('.')[1];
                    webPath = webPath + "/" + i + "." + file.FileName.Split('.')[1];

                    file.SaveAs(physicalPath);
                    //physicalPath = physicalPath.Replace(" ", "%20");
                    ImageURL imageURL = new ImageURL();
                    imageURL.ImageURL1 = webPath;
                    ring.ImageURL.Add(imageURL);
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

        public ActionResult GetImage(ImageURL item)
        {
            if(item == null)
                return null;

            string path = Server.MapPath("/")+item.ImageURL1;
            if (!System.IO.File.Exists(path))
                return null;

            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            return File(imageByteData, "image");
        }
    }
}
