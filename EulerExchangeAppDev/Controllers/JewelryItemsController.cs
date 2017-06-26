using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EulerExchangeAppDev.Models;
using EulerExchangeAppDev.Models.ViewModels;
using AutoMapper;
using System.Security.Claims;
using EulerExchangeAppDev.DataAccess;
using System.IO;

namespace EulerExchangeAppDev.Controllers
{
    public class JewelryItemsController : Controller
    {
        private masterEntities db = new masterEntities();
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();

        // GET: JewelryItems
        public ActionResult Index()
        {
            List<JewelryItemsViewModel> JewelryItemsViewModel = new List<JewelryItemsViewModel>();
            List<JewelryItems> JewelryItems = db.JewelryItems.ToList();
            Mapper.Map(JewelryItems, JewelryItemsViewModel);

            return View(JewelryItemsViewModel);
        }

        // GET: JewelryItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JewelryItems jewelryItemsViewModel = db.JewelryItems.Find(id);
            if (jewelryItemsViewModel == null)
            {
                return HttpNotFound();
            }
            return View(jewelryItemsViewModel);
        }

        // GET: JewelryItems/Create
        public ActionResult Create()
        {
            JewelryItemsViewModel model = new JewelryItemsViewModel();
            model.CompanyList = new SelectList(db.Companies, "ID", "CompanyName");
            model.CategoryJewelryList = new SelectList(db.JewelryCategories, "ID", "Name");

            return View(model);
        }

        // POST: JewelryItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JewelryItemsViewModel jewelryItemsViewModel, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                JewelryItems jewelryItems = new JewelryItems();

                var claimsIdentity = User.Identity as ClaimsIdentity;

                UserInfo userInfo = new UserInfo(db);
            
                Mapper.Map(jewelryItemsViewModel, jewelryItems);
                Companies company = userInfo.getLoggedCompanyId(claimsIdentity);
                jewelryItems.Companies = company;

                db.JewelryItems.Add(jewelryItems);
                db.SaveChanges();

                //save images
                SaveImages(files, company, jewelryItems);
                
                return RedirectToAction("Index");
            }

            return View(jewelryItemsViewModel);
        }

        // GET: JewelryItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JewelryItems   jewelryItemsViewModel = db.JewelryItems.Find(id);
            if (jewelryItemsViewModel == null)
            {
                return HttpNotFound();
            }
            return View(jewelryItemsViewModel);
        }

        // POST: JewelryItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Weight,Price,Millem,Carat,Pieces,Size,Length,Thick,Wide,Comment,CompanyId,CategoryJewelryId")] JewelryItemsViewModel jewelryItemsViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jewelryItemsViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jewelryItemsViewModel);
        }

        // GET: JewelryItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JewelryItems jewelryItemsViewModel = db.JewelryItems.Find(id);
            if (jewelryItemsViewModel == null)
            {
                return HttpNotFound();
            }
            return View(jewelryItemsViewModel);
        }

        // POST: JewelryItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JewelryItems jewelryItemsViewModel = db.JewelryItems.Find(id);
            db.JewelryItems.Remove(jewelryItemsViewModel);
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

        public ActionResult SaveImages(IEnumerable<HttpPostedFileBase> files, Companies company, JewelryItems jewelryItems)
        {
            // The Name of the Upload component is "files"
            if (files != null)
            {
                int i = 1;
                var jewleryCategoriesName = db.Set<JewelryCategories>().Where(x => x.Id == jewelryItems.CategoryJewelryId).SingleOrDefault().Name;

                foreach (var file in files)
                {

                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = Path.GetFileName(file.FileName);
                    var webPath = "DataImages/JewelryItems/" + company.Id + "/"+ jewleryCategoriesName + "/" + jewelryItems.Id;
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
                    JewelryItemsImageURL jewelryItemImageURL = new JewelryItemsImageURL();
                    jewelryItemImageURL.ImageURL = webPath;
                    jewelryItems.JewelryItemsImageURL.Add(jewelryItemImageURL);
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

        public ActionResult GetImage(JewelryItemsImageURL item)
        {
            if (item == null)
                return null;

            string path = Server.MapPath("/") + item.ImageURL;
            if (!System.IO.File.Exists(path))
                return null;

            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            return File(imageByteData, "image");
        }

        public ActionResult GetCompanyImage(CompaniesViewModel company)
        {
            if (company == null)
                return null;

            string path = Server.MapPath("/") + company.ImageURL;
            if (!System.IO.File.Exists(path))
                return null;

            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            return File(imageByteData, "image");
        }
    }
}

