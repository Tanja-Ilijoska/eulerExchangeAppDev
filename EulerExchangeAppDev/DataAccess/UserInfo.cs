using EulerExchangeAppDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace EulerExchangeAppDev.DataAccess
{
    public class UserInfo
    {
        private masterEntities db;

        public UserInfo(masterEntities db)
        {
            this.db = db;
        }
        public string getLoggedUserId(ClaimsIdentity claimsIdentity)
        {
            if (claimsIdentity != null)
            {
                // the principal identity is a claims identity.
                // now we need to find the NameIdentifier claim
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    return userIdClaim.Value;
                }
            }

            return null;
        }

        public Companies getLoggedCompanyId(ClaimsIdentity claimsIdentity)
        {
            string userId = getLoggedUserId(claimsIdentity);

            Companies logggedCompany = db.Companies.ToList().Where(company => company.UserId == userId).First();

            return logggedCompany;
        }
    }
}