using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EulerExchangeAppDev.Models;
using EulerExchangeAppDev.Models.ViewModels;
using AutoMapper;
using EulerExchangeAppDev.DataAccess;
using System.Security.Claims;
using System.Net;

namespace EulerExchangeAppDev.Controllers
{
    public class PortfolioController : Controller
    {
        private masterEntities db = new masterEntities();
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();

        // GET: Portfolio
        public ActionResult Index()
        {
            UserInfo userInfo = new UserInfo(db);
            var claimsIdentity = User.Identity as ClaimsIdentity;
            Companies company = userInfo.getLoggedCompanyId(claimsIdentity);

            ModelList modelList = new ModelList();

            List<JewelryCategories> categories = db.JewelryCategories.ToList();
            List<JewelryCategoriesViewModel> categoriesVM = new List<JewelryCategoriesViewModel>();
            Mapper.Map(categories, categoriesVM);
            modelList.add(categoriesVM, "JewelryCategories");
            
            for(int i=0;i< categoriesVM.Count;i++)
            {
                List<JewelryItemsViewModel> dataVM = new List<JewelryItemsViewModel>();
                List<JewelryItems> data = db.JewelryItems.Where(x => x.CompanyId == company.Id).Where(x => x.CategoryJewelryId == (i+1)).OrderByDescending(x => x.Id).ToList();
                Mapper.Map(data, dataVM);
                modelList.add(dataVM, categoriesVM[i].Name);
            }

            return View("Index", modelList);
        }

        // GET: Portfolio/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Portfolio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Portfolio/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Portfolio/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Portfolio/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Portfolio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Portfolio/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: JewelryItems/Edit/5
        public ActionResult EditItem(int? id)
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
    }
}
