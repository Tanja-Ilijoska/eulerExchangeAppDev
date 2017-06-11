using AutoMapper;
using EulerExchangeAppDev.Models;
using EulerExchangeAppDev.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EulerExchangeAppDev
{
   
    public static class AutoMapperConfig
    {
        public static MapperConfiguration MapperConfiguration;

        public static void RegisterMappings()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CompanyType, CompanyTypeViewModel>().ReverseMap();
                cfg.CreateMap<Companies, CompaniesViewModel>().ReverseMap();

                cfg.CreateMap<JewelryCategories, JewelryCategoriesViewModel>().ReverseMap();
                cfg.CreateMap<JewelryItems, JewelryItemsViewModel>().ReverseMap();

                /*cfg.CreateMap<Rings, RingsViewModel>().ReverseMap();
                cfg.CreateMap<EngagementRings, EngagementRingsViewModel>().ReverseMap();
                cfg.CreateMap<WeddingRings, WeddingRingsViewModel>().ReverseMap();
                cfg.CreateMap<Pendants, PendantsViewModel>().ReverseMap();
                cfg.CreateMap<Necklaces, NecklacesViewModel>().ReverseMap();
                cfg.CreateMap<Chains, ChainsViewModel>().ReverseMap();
                cfg.CreateMap<Bracelets, BraceletsViewModel>().ReverseMap();
                cfg.CreateMap<Earrings, EarringsViewModel>().ReverseMap();
                cfg.CreateMap<LightSets, LightSetsViewModel>().ReverseMap();
                cfg.CreateMap<SetsWatchesSunglasses, SetsWatchesSunglassesViewModel>().ReverseMap();*/

                cfg.CreateMap<JewelryMachines, JewelryMachinesViewModel>().ReverseMap();
                cfg.CreateMap<GoldBullionOffers, GoldBullionOfferViewModel>().ReverseMap();

                cfg.CreateMap<Promotions, PromotionsViewModel>().ReverseMap();
                cfg.CreateMap<Discounts, DiscountsViewModel>().ReverseMap();
            });
        }
    }



}