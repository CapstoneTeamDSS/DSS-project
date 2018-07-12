using DSS.Data.Models.Entities.Services;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSS.Controllers
{
    public class Helper
    {
        public static Data.Models.Entities.AspNetUser GetCurrentUser()
        {
            var userService = DependencyUtils.Resolve<IAspNetUserService>();
            var username = System.Web.HttpContext.Current.User.Identity.Name;
            var user = userService.FirstOrDefault(a => a.UserName == username);
            return user;
        }
    }
}