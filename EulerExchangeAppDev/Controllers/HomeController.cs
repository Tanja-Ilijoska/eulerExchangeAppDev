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
    public class Offer : IComparable
    {
        public JewelryItemsViewModel jewelryItem;
        public DiscountsViewModel discount;
        public PromotionsViewModel promotion;

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Offer offer = obj as Offer;
            if (offer != null)
            {
                //here some mathematical calculation
                return -1;
            }
            else
                throw new ArgumentException("Object is not a Temperature");
        }
    }
    public class HomeController : Controller
    {
        private masterEntities db = new masterEntities();
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();

        
        [Authorize]
        public ActionResult Index()
        {
            ModelList modelList = new ModelList();

            List<JewelryItemsViewModel> JewelryItemsViewModel = new List<JewelryItemsViewModel>();
            List<JewelryItems> JewelryItems = db.JewelryItems.ToList();
            Mapper.Map(JewelryItems, JewelryItemsViewModel);

            //List<DiscountsViewModel> DiscountsViewModel = new List<Models.DiscountsViewModel>();
            //List<Discounts> Discounts = db.Discounts.ToList();
            //Mapper.Map(Discounts, DiscountsViewModel);

            List<PromotionsViewModel> PromotionsViewModel = new List<Models.PromotionsViewModel>();
            List<Promotions> Promotions = db.Promotions.ToList();
            Mapper.Map(Promotions, PromotionsViewModel);

            List<Offer> offers = new List<Offer>();

            foreach (JewelryItemsViewModel item in JewelryItemsViewModel)
            {
                Offer offer = new Offer();
                offer.jewelryItem = item;
                offers.Add(offer);
            }

            //foreach (DiscountsViewModel item in DiscountsViewModel)
            //{
            //    Offer offer = new Offer();
            //    offer.discount = item;
            //    offers.Add(offer);
            //}

            foreach (PromotionsViewModel item in PromotionsViewModel)
            {
                Offer offer = new Offer();
                offer.promotion = item;
                offers.Add(offer);
            }

            // offers.Sort();
            var rnd = new Random();

            modelList.add(offers.OrderBy(item => rnd.Next()).ToList(), "offers");
            
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
                if (value != 0)
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