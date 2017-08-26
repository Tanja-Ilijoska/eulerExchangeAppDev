using AutoMapper;
using EulerExchangeAppDev.DataAccess;
using EulerExchangeAppDev.Models;
using EulerExchangeAppDev.Models.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace EulerExchangeAppDev.Controllers
{
    public class OrderModel
    {
        public List<JewelryItemsViewModel> Jewelries { get; set; }
        public List<JewelryCategoriesViewModel> Categories { get; set; }

        public Dictionary<String, Decimal> Weight { get; set; }
        public Dictionary<String, Decimal> Price { get; set; }
        public Dictionary<String, Decimal> Quantity { get; set; }

        public OrderModel()
        {
            Jewelries = new List<JewelryItemsViewModel>();
            Categories = new List<JewelryCategoriesViewModel>();

            Weight = new Dictionary<string, decimal>();
            Price = new Dictionary<string, decimal>();
            Quantity = new Dictionary<string, decimal>();

            Weight.Add("Total", 0);
            Price.Add("Total", 0);
            Quantity.Add("Total", 0);
        }

        public void addItem(JewelryItemsViewModel item)
        {
            Jewelries.Add(item);
            if (Categories.Where(x => x.Id == item.JewelryCategories.Id).Count() == 0)
            {
                Categories.Add(item.JewelryCategories);
                Weight.Add(item.JewelryCategories.Name, 0);
                Price.Add(item.JewelryCategories.Name, 0);
                Quantity.Add(item.JewelryCategories.Name, 0);
            }
            Weight[item.JewelryCategories.Name] += item.Weight.Value * item.Quantity;
            Price[item.JewelryCategories.Name] += item.Price.Value * item.Quantity;
            Quantity[item.JewelryCategories.Name] += item.Quantity;

            Weight["Total"] += item.Weight.Value * item.Quantity;
            Price["Total"] += item.Price.Value * item.Quantity;
            Quantity["Total"] += item.Quantity;
        }


    }
    public class OrderController : Controller
    {
        private masterEntities db = new masterEntities();
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        // GET: Order
        public ActionResult View(StoreModel data)
        {
            OrderModel orderModel = new OrderModel();
            foreach (JewelryItemsViewModel item in data.jewelries)
            {
                if (item.Quantity > 0)
                {
                    //does not bind properly, get from database
                    JewelryItems jewelryItem = db.JewelryItems.Where(x => x.Id == item.Id).First();
                    Mapper.Map(jewelryItem, item);

                    orderModel.addItem(item);
                }
            }

            ModelState.Clear();

            return View(orderModel);
        }

        public class OrderIndexModel
        {
            public List<OrderViewModel> ordersRecieved;
            public List<OrderViewModel> ordersDelivered;
        }

        public ActionResult Index()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            UserInfo userInfo = new UserInfo(db);
            Companies company = userInfo.getLoggedCompanyId(claimsIdentity);

            List<Orders> ordersRecieved = db.Orders.Where(x => x.SupplierId == company.Id).ToList();
            List<Orders> ordersDelivered = db.Orders.Where(x => x.CustomerId == company.Id).ToList();

            List<OrderViewModel> ordersRecievedVM = new List<OrderViewModel>();
            List<OrderViewModel> ordersDeliveredVM = new List<OrderViewModel>();

            Mapper.Map(ordersRecieved, ordersRecievedVM);
            Mapper.Map(ordersDelivered, ordersDeliveredVM);

            OrderIndexModel orderIndexModel = new OrderIndexModel();

            orderIndexModel.ordersRecieved = ordersRecievedVM;
            orderIndexModel.ordersDelivered = ordersDeliveredVM;

            return View(orderIndexModel);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            Orders order = db.Orders.Where(x => x.Id == id).ToList().First();

            OrderModel orderModel = new OrderModel();

            foreach (var item in order.OrderJewelryItems)
            {
                JewelryItemsViewModel itemVM = new JewelryItemsViewModel();
                Mapper.Map(item.JewelryItems, itemVM);
                itemVM.Quantity = item.Quantity;
                orderModel.addItem(itemVM);
            }

            return View(orderModel);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(OrderModel data)
        {
            try
            {
                Orders order = new Orders();

                var claimsIdentity = User.Identity as ClaimsIdentity;
                UserInfo userInfo = new UserInfo(db);
                Companies company = userInfo.getLoggedCompanyId(claimsIdentity);

                order.CustomerId = company.Id;
                order.SupplierId = data.Jewelries.First().CompanyId;
                order.DateTime = DateTime.Now;
                //check for promotion

                db.Orders.Add(order);
                db.SaveChanges();

                OrderViewModel orderVM = new OrderViewModel();

                Mapper.Map(order, orderVM);

                foreach (JewelryItemsViewModel item in data.Jewelries)
                {
                    OrderJewelryItemsViewModel orderJewelryItemVM = new OrderJewelryItemsViewModel();
                    orderJewelryItemVM.JewelryItemId = item.Id;
                    orderJewelryItemVM.OrderId = order.Id;
                    orderJewelryItemVM.Price = item.Price;//change if promotion
                    orderJewelryItemVM.Quantity = item.Quantity;

                    OrderJewelryItems orderJewelryItems = new OrderJewelryItems();
                    Mapper.Map(orderJewelryItemVM, orderJewelryItems);
                    db.OrderJewelryItems.Add(orderJewelryItems);
                    db.SaveChanges();
                }


                return RedirectToAction("Index", "Order");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
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

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
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
