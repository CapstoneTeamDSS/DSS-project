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
    [Authorize(Roles = "System Admin")]
    public class UserMngController : Controller
    {
        Wisky.ApplicationUserManager _userManager;
        IBrandService brandService = DependencyUtils.Resolve<IBrandService>();
        IAspNetUserService aspNetUserService = DependencyUtils.Resolve<IAspNetUserService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();

        [Authorize(Roles = "System Admin")]
        // GET: UserMng/Index
        public ActionResult Index()
        {  
            ViewBag.userList = GetAllUser();
            return View();
        }

        public List<Models.UserMngVM> GetAllUser()
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
            return userVMs;
        }
       
        // GET: UserMng/Form/:id
        public ActionResult Form(string id)
        {
            Models.UserDetailVM model = null;
            if (id != null)
            {
                var user = this.aspNetUserService.Get(id);
                if (user != null)
                {
                    model = new Models.UserDetailVM
                    {
                        UserName = user.UserName,
                        FullName = user.FullName,
                        isActive = user.isActive,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        BrandId = user.BrandID,
                        Id = user.Id,
                    };
                    var userRoles = UserManager.GetRoles(user.Id).ToArray();
                    ViewBag.userRoles = userRoles;
                    if (userRoles.Length > 0)
                    {
                        model.Role = userRoles[0];
                    }
                }
            }
            ViewBag.brandList = BrandController.GetBrandList();
            ViewBag.roleList = UserMngController.GetRoleList();
            return View(model);
        }

        public static List<Models.RoleVM> GetRoleList()
        {
            IAspNetRoleService aspNetRoleService = DependencyUtils.Resolve<IAspNetRoleService>();
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
            return roleList;
        }


        // POST: UserMng/Add
        public async System.Threading.Tasks.Task<ActionResult> Add(Models.UserDetailVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Role.CompareTo("System Admin") == 0)
                {
                    model.BrandId = 39;
                }
                var user = new Wisky.Models.ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    BrandId = model.BrandId,
                    isActive = model.isActive,
                };
                var result = await UserManager.CreateAsync(user, model.Password);                 
                if (result.Succeeded)
                {
                    UserManager.AddToRoles(user.Id, new string[] { model.Role });
                    return new ContentResult
                    {
                        Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "UserMng")),
                        ContentType = "text/html"
                    };
                }
                
            }
            // If we got this far, something failed, redisplay form
            ViewBag.brandList = BrandController.GetBrandList();
            ViewBag.roleList = UserMngController.GetRoleList();
            return View("Form", model);
        }


        // POST: UserMng/Update
        public async System.Threading.Tasks.Task<ActionResult> Update(Models.UserDetailVM model)
        {
            if (ModelState.IsValid)
            {
                /*Update custom fields*/
                var user = aspNetUserService
                .Get(a => a.Id == model.Id)
                .FirstOrDefault();
                if (model.Role.CompareTo("System Admin") == 0)
                {
                    model.BrandId = 39;
                }
                if (user != null)
                {
                    user.BrandID = model.BrandId;
                    user.FullName = model.FullName;
                    user.isActive = model.isActive;
                    await this.aspNetUserService.UpdateAsync(user);

                    /*Update user role*/
                    //Remove from other Roles
                    var userRoles = UserManager.GetRoles(user.Id).ToArray();
                    if (userRoles.Length > 0)
                    {
                        UserManager.RemoveFromRole(user.Id, userRoles[0]);
                    }
                    //Add to new Role
                    UserManager.AddToRoles(user.Id, new string[] { model.Role });
                    return new ContentResult
                    {
                        Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "UserMng")),
                        ContentType = "text/html"
                    };
                };
            }
            // If we got this far, something failed, redisplay form
            ViewBag.brandList = BrandController.GetBrandList();
            ViewBag.roleList = UserMngController.GetRoleList();
            return View("Form", model);
        }


        // GET: UserMng/Delete/:id
        public ActionResult Delete(string id)
        {
            var user = this.aspNetUserService.Get(id);
            if (user != null)
            {
                this.aspNetUserService.Delete(user);
            }
            return this.RedirectToAction("Index");
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