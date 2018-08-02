using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSS.Data.Models.Entities.Services
{
    public partial interface IAspNetUserService
    {
        List<AspNetUser> GetAccountsByBrandId(int brandId);
        AspNetUser GetAccountsByUserName(string userName);
    }

    public partial class AspNetUserService
    {
        DSS.Data.Models.Entities.Repositories.IAspNetUserRepository
            accountRepository = DependencyUtils
                .Resolve<Repositories.IAspNetUserRepository>();

        public List<AspNetUser> GetAccountsByBrandId(int brandId)
        {
            List<AspNetUser> result = null;
            result = accountRepository
                .Get(a => a.BrandID == brandId)
                .ToList();
            return result;
        }
        public AspNetUser GetAccountsByUserName(string userName)
        {
            AspNetUser result = null;
            result = accountRepository
                .Get(a => a.UserName.Equals(userName))
                .FirstOrDefault();
            return result;
        }
    }
}
