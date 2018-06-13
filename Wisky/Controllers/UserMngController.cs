using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using AutoMapper;
using DSS.Data.Models.Entities.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SkyWeb.DatVM.Mvc.Autofac;
using Wisky;
using Wisky.Data.Utility;

namespace DSS.Controllers
{
    public class UserMngController : Controller
    {
        Wisky.ApplicationUserManager _userManager;
        IBrandService brandService = DependencyUtils.Resolve<IBrandService>();
        IAspNetUserService aspNetUserService = DependencyUtils.Resolve<IAspNetUserService>();
        IAspNetRoleService aspNetRoleService = DependencyUtils.Resolve<IAspNetRoleService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();

        //
        // GET: UserMng/Index
        public ActionResult Index()
        {
            var users = aspNetUserService.Get().ToList();
            var userVMs = new List<Models.UserMngVM>();

            foreach (var item in users)
            {
                var u = new Models.UserMngVM
                {
                    UserName = item.UserName,
                    Id = item.Id,
                    Email = item.Email,
                    FullName = item.FullName,
                    isActive = item.isActive,
                    BrandName = brandService.GetBrandNameByID(item.BrandID),
                };
                userVMs.Add(u);
            }
            ViewBag.userList = userVMs;
            return View();
        }

        //
        // GET: UserMng/Form/:id
        public ActionResult Form(string id)
        {
            Models.UserDetailVM model = null;
            if (id != null)
            {
                var user = this.aspNetUserService.Get(id);
                if (user != null)
                {
                    var userRoles = UserManager.GetRoles(user.Id).ToArray();
                    ViewBag.userRoles = userRoles;
                    model = new Models.UserDetailVM
                    {
                        UserName = user.UserName,
                        FullName = user.FullName,
                        isActive = user.isActive,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        BrandId = user.BrandID,
                        Role = userRoles[0],
                    };
                }
            }
            ViewBag.brandList = BrandController.GetBrandList();
            var roles = aspNetRoleService.Get().ToList();
            var roleList = new List<Models.RoleVM>();
            foreach (var item in roles)
            {
                var r = new Models.RoleVM
                {
                    RoleId = item.Id,
                    RoleName = item.Name,
                };
                roleList.Add(r);
            }
            ViewBag.roleList = roleList;
            return View(model);
        }


        // POST: UserMng/Add
        public async System.Threading.Tasks.Task<ActionResult> Add(Models.UserDetailVM model)
        {
            //if (ModelState.IsValid)
            {
                var user = new Wisky.Models.ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    BrandId = model.BrandId,
                    isActive = model.isActive,
                    //Roles = model.Role,
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //List<string> roles = new List<string>();
                    //roles.Add(model.Role);
                    UserManager.AddToRoles(user.Id, new string[] { model.Role });
                    return RedirectToAction("Index", "UserMng");
                }
            }
            // If we got this far, something failed, redisplay form
            return View("Form", model);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    _userManager.PasswordHasher = new MP5Hasher(FormsAuthPasswordFormat.MD5);
                }
                return _userManager;
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}