using EulerExchangeAppDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EulerExchangeAppDev.DataAccess
{
    public class CompanyTypeManager
    {
        public static List<CompanyType> GetAll()
        {
            using (masterEntities context = new masterEntities())
            {
                return context.CompanyType.OrderBy(x => x.Type).ToList();
            }
        }

        public static CompanyType GetByID(int id)
        {
            using (masterEntities context = new masterEntities())
            {
                return context.CompanyType.Where(x => x.Id == id).First();
            }
        }

        public static List<CompanyType> GetForType(int id)
        {
            using (masterEntities context = new masterEntities())
            {
                return context.Companies.Where(x => x.Id == id).First().CompanyType.ToList();
            }
        }
    }
}