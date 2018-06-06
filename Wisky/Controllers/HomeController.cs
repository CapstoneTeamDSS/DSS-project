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
            return View();
        }

    }
}