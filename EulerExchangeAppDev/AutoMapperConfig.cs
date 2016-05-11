using AutoMapper;
using EulerExchangeAppDev.DBContex;
using EulerExchangeAppDev.Models;
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

                cfg.CreateMap<Rings, RingsViewModel>().ReverseMap();
            });
        }
    }



}