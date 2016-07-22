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
using PagedList;
using System.Security.Claims;
using EulerExchangeAppDev.DataAccess;
using System.IO;

namespace EulerExchangeAppDev.Controllers
{
    public class NecklacesController : Controller
    {
        private masterEntities db = new masterEntities();
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();

        // GET: Necklaces
        public ActionResult Index(int? page)
        {
            List<NecklacesViewModel> NecklacesViewModel = new List<NecklacesViewModel>();
            List<Necklaces> Necklaces = db.Necklaces.ToList();
            Mapper.Map(Necklaces, NecklacesViewModel);
            
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(NecklacesViewModel.ToPagedList(pageNumber, pageSize));
        }

        // GET: Necklaces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Necklaces Necklaces = db.Necklaces.Find(id);
            if (Necklaces == null)
            {
                return HttpNotFound();
            }
            return View(Necklaces);
        }

        // GET: Necklaces/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "ContactPersonName");
            return View();
        }

        // POST: Necklaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NecklacesViewModel necklaces, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                Necklaces necklace = new Necklaces();
                var claimsIdentity = User.Identity as ClaimsIdentity;

                UserInfo userInfo = new UserInfo(db);

                Companies company = userInfo.getLoggedCompanyId(claimsIdentity);
                necklace.Companies = company;

                Mapper.Map(necklaces, necklace);
                db.Necklaces.Add(necklace);
                db.SaveChanges();

                //save images
                SaveImages(files, company, necklace);

                return RedirectToAction("Index");
            }

            return View(necklaces);
        }

        // GET: Necklaces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Necklaces Necklaces = db.Necklaces.Find(id);
            if (Necklaces == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "ContactPersonName", Necklaces.CompanyId);
            return View(Necklaces);
        }

        // POST: Necklaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Number,Decription,Size,Length,Circumference,Carat,Price,Weight,CompanyId")] Necklaces Necklaces)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Necklaces).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "ContactPersonName", Necklaces.CompanyId);
            return View(Necklaces);
        }

        // GET: Necklaces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Necklaces Necklaces = db.Necklaces.Find(id);
            if (Necklaces == null)
            {
                return HttpNotFound();
            }
            return View(Necklaces);
        }

        // POST: Necklaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Necklaces Necklaces = db.Necklaces.Find(id);
            db.Necklaces.Remove(Necklaces);
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

        public ActionResult SaveImages(IEnumerable<HttpPostedFileBase> files, Companies company, Necklaces necklace)
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
                    var webPath = "DataImages/Necklaces/" + company.Id + "/necklace" + necklace.Id;
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
                    NecklacesImageURL imageURL = new NecklacesImageURL();
                    imageURL.ImageURL = webPath;
                    necklace.NecklacesImageURL.Add(imageURL);
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

        public ActionResult GetImage(NecklacesImageURL item)
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
