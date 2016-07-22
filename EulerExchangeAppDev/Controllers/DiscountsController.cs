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
    public class DiscountsController : Controller
    {
        private masterEntities db = new masterEntities();
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();

        // GET: Discounts
        public ActionResult Index(int? page)
        {
            List<DiscountsViewModel> DiscountsViewModel = new List<DiscountsViewModel>();
            List<Discounts> Discounts = db.Discounts.ToList();
            Mapper.Map(Discounts, DiscountsViewModel);

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(DiscountsViewModel.ToPagedList(pageNumber, pageSize));
        }

        // GET: Discounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discounts Discounts = db.Discounts.Find(id);
            if (Discounts == null)
            {
                return HttpNotFound();
            }
            return View(Discounts);
        }

        // GET: Discounts/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "ContactPersonName");
            return View();
        }

        // POST: Discounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DiscountsViewModel Discounts)
        {
            if (ModelState.IsValid)
            {
                Discounts Discount = new Discounts();
                var claimsIdentity = User.Identity as ClaimsIdentity;

                UserInfo userInfo = new UserInfo(db);

                Mapper.Map(Discounts, Discount);

                Companies company = userInfo.getLoggedCompanyId(claimsIdentity);
                Discount.Companies = company;
                db.Discounts.Add(Discount);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(Discounts);
        }

        // GET: Discounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discounts Discounts = db.Discounts.Find(id);
            if (Discounts == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "ContactPersonName", Discounts.CompanyId);
            return View(Discounts);
        }

        // POST: Discounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Discounts Discounts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Discounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Discounts);
        }

        // GET: Discounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discounts Discounts = db.Discounts.Find(id);
            if (Discounts == null)
            {
                return HttpNotFound();
            }
            return View(Discounts);
        }

        // POST: Discounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Discounts Discounts = db.Discounts.Find(id);
            db.Discounts.Remove(Discounts);
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
