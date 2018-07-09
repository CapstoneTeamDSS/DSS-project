using AutoMapper;
using DSS.Data.Models.Entities.Services;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSS.Controllers
{
    public class LocationController : Controller
    {
        ILocationService locationService = DependencyUtils.Resolve<ILocationService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();        
        //GET: Location/Index
        public ActionResult Index()
        {
            //DateTime aDateTime = DateTime.Now;
            //var locations = this.locationService.Get().ToList();
            //var locationVMs = new List<Models.LocationDetailVM>();

            //foreach (var item in locations)
            //{
            //    var b = new Models.LocationDetailVM
            //    {
            //        LocationId = item.LocationID,
            //        BrandId = item.BrandID,
            //        Province = item.Province,
            //        District = item.District,
            //        Address = item.Address,
            //        Description = item.Description,
            //        Time = aDateTime
            //    };
            //    locationVMs.Add(b);
            //}
            //ViewBag.locationList = locationVMs;
            ViewBag.locationList = GetLocationIdByBrandId();
            return View();
        }

        //ToanTXSE
        //Get location List by Brand ID
        public static List<Models.LocationAdditionalVM> GetLocationIdByBrandId()
        {
            IBrandService brandService = DependencyUtils.Resolve<IBrandService>();
            ILocationService locationService = DependencyUtils.Resolve<ILocationService>();
            var LocationAdditionalVM = new List<Models.LocationAdditionalVM>();           
            Models.CurrentUserVM currUser = (Models.CurrentUserVM)System.Web.HttpContext.Current.Session["currentUser"];
            var locationList = locationService.GetLocationIdByBrandId(currUser.BrandId);
            foreach (var item in locationList)
            {
                var m = new Models.LocationAdditionalVM
                {
                    BrandName = brandService.GetBrandNameByID(item.BrandID),
                    LocationId = item.LocationID,
                    Description = item.Description,
                    Province = item.Province,
                    District = item.District,
                    Address = item.Address,
                };
                LocationAdditionalVM.Add(m);
            }
            return LocationAdditionalVM;
        }
        // GET: Location/ReceiveLocationIdToDelete
        [HttpPost]
        public JsonResult ReceiveLocationId(string id)
        {
            int locationId = Int32.Parse(id);
            try
            {
                var locations = this.locationService.Get().ToList();
                var locationVMs = new List<Models.LocationDetailVM>();
                IMapper mapper = DependencyUtils.Resolve<IMapper>();
                foreach (var item in locations)
                {
                    if (item.LocationID == locationId)
                    {
                        var b = new Models.LocationDetailVM
                        {
                            LocationId = item.LocationID,
                            BrandId = item.BrandID,
                            Province = item.Province,
                            District = item.District,
                            Address = item.Address,
                            Description = item.Description
                        };
                        locationVMs.Add(b);
                    }
                }               
                return Json(new
                {
                    locationIsDelete = locationVMs,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<Models.LocationAdditionalVM> GetLocationList()
        {
            ILocationService locationService = DependencyUtils.Resolve<ILocationService>();
            var locations = locationService.Get().ToList();
            var locationVMs = new List<Models.LocationAdditionalVM>();

            foreach (var item in locations)
            {
                var b = new Models.LocationAdditionalVM
                {
                    Address = item.Address,
                    Description = item.Description,
                    BrandId = item.BrandID,
                    District = item.District,
                    Province = item.Province,
                    LocationId = item.LocationID,                
                };
                locationVMs.Add(b);
            }
            return locationVMs;
        }

        // GET: Location/Form/:id
        public ActionResult Form(int? id)
        {
            DateTime aDateTime = DateTime.Now;
            Models.LocationDetailVM model = null;
            IBrandService brandService = DependencyUtils.Resolve<IBrandService>();
            if (id != null)
            {
                var location = this.locationService.Get(id);
                if (location != null)
                {
                    model = new Models.LocationDetailVM
                    {
                        BrandId = location.BrandID,
                        LocationId = location.LocationID,
                        BrandName = brandService.GetBrandNameByID(location.BrandID),
                        Province = location.Province,
                        District = location.District,
                        Address = location.Address,
                        Description = location.Description,
                        Time = aDateTime
                    };
                }
            }
            return View(model);
        }

        // POST: Location/Add
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Add(Models.LocationDetailVM model)
        {
            DateTime aDateTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                var location = new Data.Models.Entities.Location
                {
                    LocationID = model.LocationId,
                    BrandID = model.BrandId,
                    Province = model.Province,
                    District = model.District,
                    Address = model.Address,
                    CreateDatetime = aDateTime,
                    Description = model.Description
                };
                await this.locationService.CreateAsync(location);
                return this.RedirectToAction("Index");
            }
            return View("Form", model);
        }

        // POST: Location/Update
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Update(Models.LocationDetailVM model)
        {
            if (ModelState.IsValid)
            {
                var location = this.locationService.Get(model.LocationId);
                if (location != null)
                {
                    location.LocationID = model.LocationId;
                    location.BrandID = model.BrandId;
                    location.Province = model.Province;
                    location.District = model.District;
                    location.Address = model.Address;
                    location.Description = model.Description;

                }
                await this.locationService.UpdateAsync(location);
                return this.RedirectToAction("Index");
            }
            return View("Form", model);
        }

        // GET: Location/Delete/:id
        public ActionResult Delete(int id)
        {
            var location = this.locationService.Get(id);
            if (location != null)
            {
                this.locationService.Delete(location);
            }
            return this.RedirectToAction("Index");
        }

    }
}