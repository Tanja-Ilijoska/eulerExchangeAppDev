using AutoMapper;
using EulerExchangeAppDev.Models;
using EulerExchangeAppDev.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EulerExchangeAppDev.Controllers
{
    public class HomeController : Controller
    {
        private masterEntities db = new masterEntities();
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        public ActionResult Index()
        {
            ModelList modelList = new ModelList();
            
            List<JewelryItemsViewModel> JewelryItemViewModel = new List<JewelryItemsViewModel>();
            List<JewelryItems> JewelryItem = db.JewelryItems.ToList();
            Mapper.Map(JewelryItem, JewelryItemViewModel);
            modelList.add(JewelryItemViewModel, "JewelryItem");
            
            List<JewelryMachinesViewModel> JewelryMachineViewModel = new List<JewelryMachinesViewModel>();
            List<JewelryMachines> JewelryMachine = db.JewelryMachines.ToList();
            Mapper.Map(JewelryMachine, JewelryMachineViewModel);
            modelList.add(JewelryMachineViewModel, "JewelryMachine");

            List<GoldBullionOfferViewModel> GoldBullionOffersViewModel = new List<GoldBullionOfferViewModel>();
            List<GoldBullionOffers> GoldBullionOffers = db.GoldBullionOffers.ToList();
            Mapper.Map(GoldBullionOffers, GoldBullionOffersViewModel);
            modelList.add(GoldBullionOffersViewModel, "GoldBullionOffers");

            ViewBag.currencyRates = DataAccess.CurrencyRate.getCurrencyRates();
 

            return View("Index", modelList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}