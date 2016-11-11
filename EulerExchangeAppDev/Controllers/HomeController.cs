using AutoMapper;
using EulerExchangeAppDev.Models;
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

            List<JewelryMachinesViewModel> JewelryMachineViewModel = new List<JewelryMachinesViewModel>();
            List<JewelryMachines> JewelryMachine = db.JewelryMachines.ToList();
            Mapper.Map(JewelryMachine, JewelryMachineViewModel);

            ViewBag.currencyRates = DataAccess.CurrencyRate.getCurrencyRates();


            return View(JewelryMachineViewModel);
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