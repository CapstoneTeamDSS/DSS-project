using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DSS.Data.Models.Entities.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SkyWeb.DatVM.Mvc.Autofac;



namespace DSS.Controllers
{
    public class UserMngController : Controller
    {
        IAccountService accountService = DependencyUtils.Resolve<IAccountService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();

        //
        // GET: UserMng/Index
        public ActionResult Index()
        {
            Wisky.ApplicationUserManager _userManager = HttpContext.GetOwinContext().GetUserManager<Wisky.ApplicationUserManager>();
            var users = _userManager.Users.ToList();
            var userVMs = new List<Models.UserMngVM>();
            foreach (var item in users)
            {
                var u = new Models.UserMngVM
                {
                    UserName = item.UserName,
                    Id = item.Id,
                    Email = item.Email,
                };
                userVMs.Add(u);
            }

            ViewBag.userList = userVMs;
            return View();
        }


        // GET: UserMng/Form/:id
        public ActionResult Form(String id)
        {
            Models.UserMngVM model = null;

            //if (id != null)
            //{
            //    var brand = this.brandService.Get(id);
            //    if (brand != null)
            //    {
            //        model = new Models.BrandDetailVM
            //        {
            //            Name = brand.BrandName,
            //            Description = brand.Description,
            //            Id = brand.BrandID,
            //        };
            //    }
            //}
            return View(model);
        }

    }
}