using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EulerExchangeAppDev.Models;
using EulerExchangeAppDev.Models.ViewModels;
using System.Security.Claims;
using EulerExchangeAppDev.DataAccess;
using AutoMapper;

namespace EulerExchangeAppDev.Controllers
{
    public class GoldBullionOffersController : Controller
    {
        private masterEntities db = new masterEntities();
        IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();

        // GET: GoldBullionOffers
        public ActionResult Index()
        {
            var goldBullionOffers = db.GoldBullionOffers.Include(g => g.Companies);
            return View(goldBullionOffers.ToList());
        }

        // GET: GoldBullionOffers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoldBullionOffers goldBullionOffers = db.GoldBullionOffers.Find(id);
            if (goldBullionOffers == null)
            {
                return HttpNotFound();
            }
            return View(goldBullionOffers);
        }

        // GET: GoldBullionOffers/Create
        public ActionResult Create()
        {/*
            if (ModelState.IsValid)
            {
                
                GoldBullionOffers goldBullionOffer = new GoldBullionOffers();

                var claimsIdentity = User.Identity as ClaimsIdentity;

                UserInfo userInfo = new UserInfo(db);

                Mapper.Map(goldBullionOfferViewModel, goldBullionOffer );
                Companies company = userInfo.getLoggedCompanyId(claimsIdentity);
                goldBullionOffer.Companies = company;

                db.GoldBullionOffers.Add(goldBullionOffer);
                db.SaveChanges();
                return RedirectToAction("Index");
                
            
            }
            
            return View(goldBullionOfferViewModel);
            */

            GoldBullionOfferViewModel goldBullionOfferViewModel = new GoldBullionOfferViewModel();
            return View(goldBullionOfferViewModel);
        }

        // POST: GoldBullionOffers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GoldBullionOfferViewModel goldBullionOfferViewModel)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                UserInfo userInfo = new UserInfo(db);
                GoldBullionOffers goldBullionOffers = new GoldBullionOffers();

                Mapper.Map(goldBullionOfferViewModel, goldBullionOffers);
                Companies company = userInfo.getLoggedCompanyId(claimsIdentity);
                goldBullionOffers.Companies = company;
                db.GoldBullionOffers.Add(goldBullionOffers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(goldBullionOfferViewModel);
        }

        // GET: GoldBullionOffers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoldBullionOffers goldBullionOffers = db.GoldBullionOffers.Find(id);
            if (goldBullionOffers == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "ContactPersonName", goldBullionOffers.CompanyId);
            return View(goldBullionOffers);
        }

        // POST: GoldBullionOffers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CompanyId,Price,Weight,Carat,DateTime")] GoldBullionOffers goldBullionOffers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goldBullionOffers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "ContactPersonName", goldBullionOffers.CompanyId);
            return View(goldBullionOffers);
        }

        // GET: GoldBullionOffers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoldBullionOffers goldBullionOffers = db.GoldBullionOffers.Find(id);
            if (goldBullionOffers == null)
            {
                return HttpNotFound();
            }
            return View(goldBullionOffers);
        }

        // POST: GoldBullionOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GoldBullionOffers goldBullionOffers = db.GoldBullionOffers.Find(id);
            db.GoldBullionOffers.Remove(goldBullionOffers);
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

        [HttpPost]
        public ActionResult bid(int id, decimal value)
        {
            GoldBullionOffers goldBullionOffers = db.GoldBullionOffers.Find(id);

            GoldBullionOfferBids lastBid = db.GoldBullionOfferBids.Where(b => b.GoldBullionOfferId == id).OrderByDescending(b => b.Id).FirstOrDefault();
            if (lastBid != null)
                value += lastBid.Price;
            else
                value += (decimal) goldBullionOffers.Price;

            GoldBullionOfferBids bid = new GoldBullionOfferBids();
            
            UserInfo userInfo = new UserInfo(db);
            var claimsIdentity = User.Identity as ClaimsIdentity;
            Companies company = userInfo.getLoggedCompanyId(claimsIdentity);

            bid.GoldBullionOfferId = id;
            bid.CompanyId = company.Id;
            bid.Price = value;

            db.GoldBullionOfferBids.Add(bid);
            db.SaveChanges();
            
            return Json( new { result = value }, "plain/text");
        }
    }
}
