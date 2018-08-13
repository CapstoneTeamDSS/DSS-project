using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DSS.Annotaion
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return false;
            }
            var userManager = HttpContext
                .Current.GetOwinContext()
                .GetUserManager<Wisky.ApplicationUserManager>();
            var user = userManager.FindByEmailAsync(value as string);
            Task.WaitAll(new[] { user });
            if (user.Result == null)
            {
                return true;
            }
            return false;
        }
    }
    public class UniqueUserNameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return false;
            }
            var userManager = HttpContext
                .Current.GetOwinContext()
                .GetUserManager<Wisky.ApplicationUserManager>();
            var user = userManager.FindByNameAsync(value as string);
            Task.WaitAll(new[] { user });
            if (user.Result == null)
            {
                return true;
            }
            return false;
        }
    }
}