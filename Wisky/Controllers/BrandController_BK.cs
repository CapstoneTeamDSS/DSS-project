using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wisky.Models.LoginMenu;
using AutoMapper;
using DSS.Data.Models.Entities.Services;

namespace DSS.Controllers
{
    public class BrandController_BK : Controller
    {
        IBrandService brandService = DependencyUtils.Resolve<IBrandService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();

        // GET: Brand
        public ActionResult Index()
        {
            var intValue = 100;
            ViewBag.intValues = new List<int> { 1, 2, 3, 4 };
            ViewBag.brandList = new List<Models.BrandDetailVM>();
            return View(intValue);
        }

        public JsonResult GetInt()
        {
            var intValues = new List<int> { 1, 2, 3, 4, 6 };
            return Json(intValues);
        }

        public JsonResult GetBrands()
        {
            var brands = this.brandService.Get().ToList();
            var brandVMs = new List<Models.BrandDetailVM>();

            foreach (var item in brands)
            {
                var b = new Models.BrandDetailVM
                {
                    Name = item.BrandName,
                    Description = item.Description,
                };
                brandVMs.Add(b);
            }
            return Json(brandVMs);
        }

        public ActionResult Product(int productId)
        {
            return View(productId);
        }

        public JsonResult GetGroupItem(GroupItem groupItem)
        {
            return Json(groupItem);
        }

        // GET: Brand/Form
        public ActionResult Form(string id)
        {
            return View();
        }

        // GET: Brand/Form
        public ActionResult Delete(int id)
        {
            var brand = this.brandService.Get(id);
            if (brand != null)
            {
                this.brandService.Delete(brand);
            }
            return View();
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
                };
                await this.brandService.CreateAsync(brand);
                return this.RedirectToAction("Index");
            }
            return View("Form", model);
        }
    }
}