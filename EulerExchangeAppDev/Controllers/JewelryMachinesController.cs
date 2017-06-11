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
    [Authorize]
    public class JewelryMachinesController : Controller
    {
        private masterEntities db = new masterEntities();
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();

        // GET: JewelryMachine
        public ActionResult Index(int? page)
        {
            List<JewelryMachinesViewModel> JewelryMachineViewModel = new List<JewelryMachinesViewModel>();
            List<JewelryMachines> JewelryMachine = db.JewelryMachines.ToList();
            Mapper.Map(JewelryMachine, JewelryMachineViewModel);

            //var ringsViewModel = Mapper.Map<List<Rings>, List<RingsViewModel>>(rings);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(JewelryMachineViewModel.ToPagedList(pageNumber, pageSize));
        }

        // GET: JewelryMachine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JewelryMachines JewelryMachine = db.JewelryMachines.Find(id);
            if (JewelryMachine == null)
            {
                return HttpNotFound();
            }
            return View(JewelryMachine);
        }

        // GET: JewelryMachine/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "ContactPersonName");
            return View();
        }

        // POST: JewelryMachine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JewelryMachinesViewModel machines, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                JewelryMachines machine = new JewelryMachines();
                var claimsIdentity = User.Identity as ClaimsIdentity;
                
                Mapper.Map(machines, machine);

                UserInfo userInfo = new UserInfo(db);
                Companies company = userInfo.getLoggedCompanyId(claimsIdentity);
                machine.Companies = company;

                db.JewelryMachines.Add(machine);
                db.SaveChanges();

                //save images
                SaveImages(files, company, machine);

                return RedirectToAction("Index");
            }

            return View(machines);
        }

        // GET: JewelryMachine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JewelryMachines JewelryMachine = db.JewelryMachines.Find(id);
            if (JewelryMachine == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "ContactPersonName", JewelryMachine.CompanyId);
            return View(JewelryMachine);
        }

        // POST: JewelryMachine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JewelryMachines JewelryMachines)
        {
            if (ModelState.IsValid)
            {
                db.Entry(JewelryMachines).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "ContactPersonName", JewelryMachines.CompanyId);
            return View(JewelryMachines);
        }

        // GET: JewelryMachine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JewelryMachines JewelryMachine = db.JewelryMachines.Find(id);
            if (JewelryMachine == null)
            {
                return HttpNotFound();
            }
            return View(JewelryMachine);
        }

        // POST: JewelryMachine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JewelryMachines JewelryMachine = db.JewelryMachines.Find(id);
            db.JewelryMachines.Remove(JewelryMachine);
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

        public ActionResult SaveImages(IEnumerable<HttpPostedFileBase> files, Companies company, JewelryMachines jewelryMachine)
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
                    var webPath = "DataImages/JewelryMachines/" + company.Id + "/JewelryMachine" + jewelryMachine.Id;
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
                    JewelryMachinesImageURL imageURL = new JewelryMachinesImageURL();
                    imageURL.ImageURL = webPath;
                    jewelryMachine.JewelryMachinesImageURL.Add(imageURL);
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

        public ActionResult GetImage(JewelryMachinesImageURL item)
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
