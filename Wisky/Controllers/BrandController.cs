﻿using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using DSS.Data.Models.Entities.Services;

namespace DSS.Controllers
{
    [Authorize(Roles = "System Admin")]
    public class BrandController : Controller
    {
        IMapper mapper = DependencyUtils.Resolve<IMapper>();
        IBrandService brandService = DependencyUtils.Resolve<IBrandService>();

        //GET: Brand/Index
        public ActionResult Index()
        {
            var brandVMs = new List<Models.BrandDetailVM>();
            brandVMs = BrandController.GetBrandList();
            ViewBag.brandList = brandVMs;
            ViewBag.addSuccess = Session["ADD_RESULT"] ?? false;
            ViewBag.updateSuccess = Session["UPDATE_RESULT"] ?? false;
            Session.Clear();
            return View();
        }

        //GET: Brand/Index
        public ActionResult UpdateStatus(int dataId)
        {
            bool result = false;
            var brand = this.brandService
                .Get(a => a.BrandID == dataId)
                .FirstOrDefault();
            if (brand != null)
            {
                brand.isActive = !brand.isActive;
                this.brandService.Update(brand);
                result = true;
            }
            return Json(new
            {
                success = result,
            }, JsonRequestBehavior.AllowGet);

        }

        public static List<Models.BrandDetailVM> GetBrandList()
        {
            IBrandService brandService = DependencyUtils.Resolve<IBrandService>();
            var brands = brandService.Get().ToList();
            var brandVMs = new List<Models.BrandDetailVM>();
            foreach (var item in brands)
            {
                var b = new Models.BrandDetailVM
                {
                    Name = item.BrandName,
                    Description = item.Description,
                    Id = item.BrandID,
                    isActive = item.isActive,
                };
                brandVMs.Add(b);
            }
            return brandVMs;
        }
        //TOANTXSE
        // POST: Brand/CheckBrandIdIsUsed  
        [HttpPost]
        public JsonResult CheckBrandIdIsUsed(int id)
        {
            bool check = false;
            try
            {
                IAspNetUserService userService = DependencyUtils.Resolve<IAspNetUserService>();
                var user = userService
                .Get(a => a.BrandID == id)
                .FirstOrDefault();
                if(user != null)
                {
                    check = true;
                }
                return Json(new
                {
                    isUsing = check
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // GET: Brand/Form/:id
        public ActionResult Form(int? id)
        {
            Models.BrandDetailVM model = null;
            if (id != null)
            {
                var brand = this.brandService.Get(id);
                if (brand != null)
                {
                    model = new Models.BrandDetailVM
                    {
                        Name = brand.BrandName,
                        Description = brand.Description,
                        Id = brand.BrandID,
                        isActive = brand.isActive,
                    };
                }
            }
            return View(model);
        }

        // POST: Brand/Add
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Add(Models.BrandDetailVM model)
        {
            if (ModelState.IsValid)
            {
                var brand = new Data.Models.Entities.Brand
                {
                    BrandName = model.Name,
                    Description = model.Description,
                    isActive = model.isActive
                };
                await this.brandService.CreateAsync(brand);
                //return this.RedirectToAction("Index");
                Session.Clear();
                Session["ADD_RESULT"] = true;
                return new ContentResult
                {
                    Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "Brand")),
                    ContentType = "text/html"
                };
            }
            return View("Form", model);
        }

        // POST: Brand/Update
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Update(Models.BrandDetailVM model)
        {
            if (ModelState.IsValid)
            {
                var brand = this.brandService.Get(model.Id);
                if (brand != null)
                {
                    brand.BrandName = model.Name;
                    brand.Description = model.Description;
                    brand.isActive = model.isActive;
                }
                await this.brandService.UpdateAsync(brand);
                Session.Clear();
                Session["UPDATE_RESULT"] = true;
                return new ContentResult
                {
                    Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "Brand")),
                    ContentType = "text/html"
                };
            }
            return View("Form", model);
        }

        // GET: Brand/Delete/:id
        public ActionResult Delete(int id)
        {
            var brand = this.brandService.Get(id);
            if (brand != null)
            {
                this.brandService.Delete(brand);
            }
            return this.RedirectToAction("Index");
        }

    }
}