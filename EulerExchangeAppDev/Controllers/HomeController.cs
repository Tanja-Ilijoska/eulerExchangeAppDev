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
        [Authorize]
        public ActionResult Index()
        {


            ModelList modelList = new ModelList();
            
            List<JewelryItemsViewModel> JewelryItemViewModel = new List<JewelryItemsViewModel>();
            List<JewelryItems> JewelryItem = db.JewelryItems.OrderByDescending(x => x.Id).ToList();
            Mapper.Map(JewelryItem, JewelryItemViewModel);
            modelList.add(JewelryItemViewModel, "JewelryItems");
            
            List<JewelryMachinesViewModel> JewelryMachineViewModel = new List<JewelryMachinesViewModel>();
            List<JewelryMachines> JewelryMachine = db.JewelryMachines.ToList();
            Mapper.Map(JewelryMachine, JewelryMachineViewModel);
            modelList.add(JewelryMachineViewModel, "JewelryMachines");

            List<GoldBullionOfferViewModel> GoldBullionOffersViewModel = new List<GoldBullionOfferViewModel>();
            List<GoldBullionOffers> GoldBullionOffers = db.GoldBullionOffers.ToList();
            Mapper.Map(GoldBullionOffers, GoldBullionOffersViewModel);
            foreach(GoldBullionOfferViewModel offer in GoldBullionOffersViewModel)
            {
                // List<GoldBullionOfferBids> l = db.GoldBullionOfferBids.Where(bid => bid.GoldBullionOfferId == offer.Id).ToList();
                // if (l.Count() == 0)
                //     continue;
                decimal value = db.GoldBullionOfferBids.Where(bid => bid.GoldBullionOfferId == offer.Id).Select(bid => bid.Price).DefaultIfEmpty(0).Max();
                if (value != null && value != 0)
                    offer.Price = (float) value;
            }
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