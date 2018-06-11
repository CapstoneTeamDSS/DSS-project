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
        Wisky.ApplicationUserManager _userManager;
        IBrandService brandService = DependencyUtils.Resolve<IBrandService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();

        //
        // GET: UserMng/Index
        public ActionResult Index()
        {
            _userManager = HttpContext.GetOwinContext().GetUserManager<Wisky.ApplicationUserManager>();
            var users = _userManager.Users.ToList();
            var userVMs = new List<Models.UserMngVM>();

            foreach (var item in users)
            {
                var u = new Models.UserMngVM
                {
                    UserName = item.UserName,
                    Id = item.Id,
                    Email = item.Email,
                    BrandName = brandService.GetBrandNameByID(item.BrandId),
                };
                userVMs.Add(u);
            }

            ViewBag.userList = userVMs;
            return View();
        }


        // GET: UserMng/Form/:id
        public ActionResult Form(String id)
        {
            Models.UserDetailVM model = null;
            if (id != null)
            {
                _userManager = HttpContext.GetOwinContext().GetUserManager<Wisky.ApplicationUserManager>();
                var user = _userManager.FindById(id);
                if (user != null)
                {
                    model = new Models.UserDetailVM
                    {
                        UserName = user.UserName,
                        FullName = user.FullName,
                        Id = user.Id,
                        BrandId = user.BrandId,
                        PhoneNumber = user.PhoneNumber,
                        isActive = user.isActive,
                        Email = user.Email,
                    };
                }
            }
            return View(model);
        }

    }
}