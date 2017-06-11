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
using EulerExchangeAppDev.Models.ViewModels;

namespace EulerExchangeAppDev.Controllers
{
    public class JewelryCategoriesController : Controller
    {
        private masterEntities db = new masterEntities();
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();

        // GET: JewelryCategories
        public ActionResult Index(int? page)
        {
            List<JewelryCategoriesViewModel> JewelryCategoriesViewModel = new List<JewelryCategoriesViewModel>();
            List<JewelryCategories> JewelryCategories = db.JewelryCategories.ToList();
            Mapper.Map(JewelryCategories, JewelryCategoriesViewModel);

            //var ringsViewModel = Mapper.Map<List<Rings>, List<RingsViewModel>>(rings);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(JewelryCategoriesViewModel.ToPagedList(pageNumber, pageSize));
        }

        // GET: JewelryCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JewelryCategories jewelryCategoriesViewModel = db.JewelryCategories.Find(id);
            if (jewelryCategoriesViewModel == null)
            {
                return HttpNotFound();
            }
            return View(jewelryCategoriesViewModel);
        }

        // GET: JewelryCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JewelryCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] JewelryCategories jewelryCategories)
        {
            if (ModelState.IsValid)
            {
                db.JewelryCategories.Add(jewelryCategories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jewelryCategories);
        }

        // GET: JewelryCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JewelryCategories jewelryCategories = db.JewelryCategories.Find(id);
            if (jewelryCategories == null)
            {
                return HttpNotFound();
            }
            return View(jewelryCategories);
        }

        // POST: JewelryCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] JewelryCategoriesViewModel jewelryCategoriesViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jewelryCategoriesViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jewelryCategoriesViewModel);
        }

        // GET: JewelryCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JewelryCategories jewelryCategories = db.JewelryCategories.Find(id);
            if (jewelryCategories == null)
            {
                return HttpNotFound();
            }
            return View(jewelryCategories);
        }

        // POST: JewelryCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JewelryCategories jewelryCategories = db.JewelryCategories.Find(id);
            db.JewelryCategories.Remove(jewelryCategories);
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
    }
}
