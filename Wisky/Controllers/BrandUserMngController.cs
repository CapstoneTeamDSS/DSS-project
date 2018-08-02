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
    [Authorize(Roles = "Admin")]
    public class BrandUserMngController : Controller
    {
        Wisky.ApplicationUserManager _userManager;
        IAspNetUserService aspNetUserService = DependencyUtils.Resolve<IAspNetUserService>();
        IBrandService brandService = DependencyUtils.Resolve<IBrandService>();

        // GET: BrandUserMng
        public ActionResult Index()
        {
            ViewBag.userList = GetBrandAccounts();
            return View();
        }

        public List<Models.BrandUserMngVM> GetBrandAccounts()
        {
            var user = Helper.GetCurrentUser();
            var users = aspNetUserService.GetAccountsByBrandId(user.BrandID);
            var userVMs = new List<Models.BrandUserMngVM>();
            foreach (var item in users)
            {
                var u = new Models.BrandUserMngVM
                {
                    UserName = item.UserName,
                    Id = item.Id,
                    Email = item.Email,
                    FullName = item.FullName,
                    IsActive = item.isActive,
                    BrandName = brandService.GetBrandNameByID(item.BrandID),
                };
                userVMs.Add(u);
            }
            return userVMs;
        }
        public ActionResult GetAccountInformation()
        {
            var user = Helper.GetCurrentUser();
            Models.BrandUserDetailVM model = null;
            var myuser = aspNetUserService.GetAccountsByUserName(user.UserName);
            if (myuser != null)
            {
                model = new Models.BrandUserDetailVM
                {
                    UserName = myuser.UserName,
                    Id = myuser.Id,
                    Email = myuser.Email,
                    FullName = myuser.FullName,
                    isActive = myuser.isActive,
                    Password = myuser.PasswordHash,
                };
                var userRoles = UserManager.GetRoles(myuser.Id).ToArray();
                ViewBag.userRoles = userRoles;
                if (userRoles.Length > 0)
                {
                    model.Role = userRoles[0];
                }
            }

            return View(model);
        }
        // GET: BrandUserMng/Form/:id
        public ActionResult Form(string id)
        {
            Models.BrandUserDetailVM model = null;
            if (id != null)
            {
                var user = this.aspNetUserService.Get(id);
                if (user != null)
                {
                    model = new Models.BrandUserDetailVM
                    {
                        UserName = user.UserName,
                        FullName = user.FullName,
                        isActive = user.isActive,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
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
            List<Models.RoleVM> roleList = UserMngController.GetRoleList();
            roleList.RemoveAt(2);
            ViewBag.roleList = roleList;
            return View(model);
        }
        // POST: BrandUserMng/Add
        public async System.Threading.Tasks.Task<ActionResult> Add(Models.BrandUserDetailVM model)
        {
            if (ModelState.IsValid)
            {
                var currUser = Helper.GetCurrentUser();
                var user = new Wisky.Models.ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    BrandId = currUser.BrandID,
                    isActive = model.isActive,
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.Role.CompareTo("System Admin") == 0)
                    {
                        model.Role = "Active User";
                    }
                    UserManager.AddToRoles(user.Id, new string[] { model.Role });
                    return new ContentResult
                    {
                        Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "BrandUserMng")),
                        ContentType = "text/html"
                    };
                }

            }
            // If we got this far, something failed, redisplay form
            ViewBag.brandList = BrandController.GetBrandList();
            ViewBag.roleList = UserMngController.GetRoleList();
            return View("Form", model);
        }

        // POST: BrandUserMng/Update
        public async System.Threading.Tasks.Task<ActionResult> Update(Models.BrandUserDetailVM model)
        {
            if (ModelState.IsValid)
            {
                /*Update custom fields*/
                var user = aspNetUserService
                .Get(a => a.Id == model.Id)
                .FirstOrDefault();
                var currUser = Helper.GetCurrentUser();
                if (user != null)
                {
                    user.BrandID = currUser.BrandID;
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
                    if (model.Role.CompareTo("System Admin") == 0)
                    {
                        model.Role = "Active User";
                    }
                    UserManager.AddToRoles(user.Id, new string[] { model.Role });
                    return new ContentResult
                    {
                        Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "BrandUserMng")),
                        ContentType = "text/html"
                    };
                };
            }
            // If we got this far, something failed, redisplay form
            ViewBag.brandList = BrandController.GetBrandList();
            ViewBag.roleList = UserMngController.GetRoleList();
            return View("Form", model);
        }

        // POST: BrandUserMng/Update
        public async System.Threading.Tasks.Task<ActionResult> UpdateMyAccount(Models.BrandAccountInformation model)
        {
            var currUser = Helper.GetCurrentUser();
            if (ModelState.IsValid)
            {
                /*Update custom fields*/
                var user = aspNetUserService
                .Get(a => a.Id == model.Id)
                .FirstOrDefault();
                if (user != null)
                {
                    user.BrandID = currUser.BrandID;
                    user.FullName = model.FullName;
                    user.AspNetRoles = currUser.AspNetRoles;
                };
                await this.aspNetUserService.UpdateAsync(user);
                return new ContentResult
                {
                    Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "Home")),
                    ContentType = "text/html"
                };

            }
            return View("GetAccountInformation", model);
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
        public ActionResult UpdateStatus(string dataId)
        {
            bool result = false;
            var user = this.aspNetUserService
                .Get(a => a.UserName.Equals(dataId))
                .FirstOrDefault();
            if (user != null)
            {
                user.isActive = !user.isActive;
                this.aspNetUserService.Update(user);
                result = true;
            }
            return Json(new
            {
                success = result,
            }, JsonRequestBehavior.AllowGet);

        }
        // GET: BrandUserMng/Delete/:id
        public ActionResult Delete(int id)
        {
            var brandUserMng = this.aspNetUserService.Get(id);
            if (brandUserMng != null)
            {
                this.aspNetUserService.Delete(brandUserMng);
            }
            return this.RedirectToAction("Index");
        }

    }
}
