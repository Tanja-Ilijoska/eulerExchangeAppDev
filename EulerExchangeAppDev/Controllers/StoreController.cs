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

    public class StoreModel
    {
        public List<JewelryCategoriesViewModel> categories { get; set; }
        public List<JewelryItemsViewModel> jewelries { get; set; }
    }
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

            //ModelList modelList = new ModelList();

            List <JewelryCategories> categories = db.JewelryCategories.ToList();
            List<JewelryCategoriesViewModel> categoriesVM = new List<JewelryCategoriesViewModel>();
            Mapper.Map(categories, categoriesVM);
            //modelList.add(categoriesVM, "JewelryCategories");

            //Dictionary<String, List<JewelryItemsViewModel>> fullData = new Dictionary<String, List<JewelryItemsViewModel>>();

            /*
            for (int i = 0; i < categoriesVM.Count; i++)
            {
                List<JewelryItemsViewModel> dataVM = new List<JewelryItemsViewModel>();
                List<JewelryItems> data = db.JewelryItems.Where(x => x.CompanyId == company.Id).Where(x => x.CategoryJewelryId == (i + 1)).OrderByDescending(x => x.Id).ToList();
                Mapper.Map(data, dataVM);
                //modelList.add(dataVM, categoriesVM[i].Name);
                //fullData.Add(categoriesVM[i].Name, dataVM);
            }
            */

            List<JewelryItemsViewModel> jewelriesVM = new List<JewelryItemsViewModel>();
            List<JewelryItems> jewelries = db.JewelryItems.Where(x => x.CompanyId == company.Id).OrderBy(x => x.CategoryJewelryId).ThenByDescending(x => x.Id).ToList();
            Mapper.Map(jewelries, jewelriesVM);

            //Tuple<List<JewelryCategoriesViewModel>, Dictionary<String, List<JewelryItemsViewModel>>> tuple = new Tuple<List<JewelryCategoriesViewModel>, Dictionary<String, List<JewelryItemsViewModel>>>(categoriesVM, fullData);

            StoreModel storeModel = new StoreModel();
            storeModel.categories = categoriesVM;
            storeModel.jewelries = jewelriesVM;

            return View("Index", storeModel);
        }

        public ActionResult Discount(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            Discounts discount = db.Discounts.Find(id);

            if(discount == null)
                return RedirectToAction("Index", "Home");

            Companies company = discount.Companies;

            ViewBag.Discount = discount;
            ViewBag.Company = company;

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

            return View("Discount", modelList);
        }

        public ActionResult Promotion(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            Promotions promotion = db.Promotions.Find(id);

            if (promotion == null)
                return RedirectToAction("Index", "Home");

            Companies company = promotion.Companies;

            ViewBag.Promotion = promotion;
            ViewBag.Company = company;

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

            return View("Promotion", modelList);
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
