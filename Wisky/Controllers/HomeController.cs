using AutoMapper.QueryableExtensions;
using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using DSS.Data.Models.Entities.Services;
using DSS.Controllers;

namespace Wisky.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            var user = System.Web.HttpContext.Current.User;
            ViewBag.Title = "Digital Signage System";
            ViewBag.FullName = user.Identity.Name;
            Session["username"] = user.Identity.Name;
            Session["userID"] = user.Identity.GetUserId(); //GetUserId() only can be used when using Microsoft.AspNet.Identity;
            //Get Size Account
            IAspNetUserService aspNetUserService = DependencyUtils.Resolve<IAspNetUserService>();
            var users = Helper.GetCurrentUser();
            var accountList = aspNetUserService.GetAccountsByBrandId(users.BrandID);
            var size = accountList.Count;
            ViewBag.accountList = size;
            return View();
        }


    }
}
