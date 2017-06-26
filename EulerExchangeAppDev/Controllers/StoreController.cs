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

namespace EulerExchangeAppDev.Controllers
{
    public class StoreController : Controller
    {
        private masterEntities db = new masterEntities();
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        // GET: Store
        public ActionResult Index(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Companies company = db.Companies.Find(id);

            ModelList modelList = new ModelList();

            List<JewelryCategories> categories = db.JewelryCategories.ToList();
            List<JewelryCategoriesViewModel> categoriesVM = new List<JewelryCategoriesViewModel>();
            Mapper.Map(categories, categoriesVM);
            modelList.add(categoriesVM, "JewelryCategories");

            for (int i = 0; i < categoriesVM.Count; i++)
            {
                List<JewelryItemsViewModel> dataVM = new List<JewelryItemsViewModel>();
                List<JewelryItems> data = db.JewelryItems.Where(x => x.CompanyId == company.Id).Where(x => x.CategoryJewelryId == (i + 1)).OrderByDescending(x => x.Id).ToList();
                Mapper.Map(data, dataVM);
                modelList.add(dataVM, categoriesVM[i].Name);
            }

            return View("Index", modelList);
        }

        // GET: Store/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Store/Create
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

        // GET: Store/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Store/Edit/5
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

        // GET: Store/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Store/Delete/5
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
    }
}
