using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DSS.Data.Models.Entities.Services;
using System.Web.Script.Services;
using System.Web.Services;

namespace DSS.Controllers
{
    [Authorize]
    public class MatchingDeviceController : Controller
    {
        IDeviceService deviceService = DependencyUtils.Resolve<IDeviceService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();


        // GET: MatchingDevice
        public ActionResult Index()
        {
            var devices = this.deviceService.Get().ToList();
            var deviceVMs = new List<Models.MatchingDeviceVM>();
            foreach (var item in devices)
            {
                var b = new Models.MatchingDeviceVM
                {
                    DeviceId = item.DeviceID,
                    Description = item.Description,
                    BoxId = item.BoxID,
                    ScreenId = item.ScreenID,

                };
                deviceVMs.Add(b);
            }
            ViewBag.devicesList = deviceVMs;
            ViewBag.locationStringList = GetLocationIdByBrandId();
            return View();
        }
        public static List<Models.LocationStringVM> GetLocationIdByBrandId()
        {
            ILocationService locationService = DependencyUtils.Resolve<ILocationService>();
            var locationStringList = new List<Models.LocationStringVM>();
            IBrandService brandService = DependencyUtils.Resolve<IBrandService>();
            Models.CurrentUserVM currUser = (Models.CurrentUserVM)System.Web.HttpContext.Current.Session["currentUser"];
            var locationList = locationService.GetLocationIdByBrandId(currUser.BrandId);
            foreach (var item in locationList)
            {
                var m = new Models.LocationStringVM
                {
                    LocationId = item.LocationID,
                    LocationStringList = item.Address + " " + item.District + " " + item.Province,

                };
                locationStringList.Add(m);
            }
            return locationStringList;
        }
        // GET: Home/JqAJAX  
        [HttpPost]
        public JsonResult receiveLocationId(string id)
        {
            int locationId = Int32.Parse(id);
            try
            {
                //get box list by location ID
                IBoxService boxService = DependencyUtils.Resolve<IBoxService>();
                IMapper mapper = DependencyUtils.Resolve<IMapper>();
                var boxs = boxService.Get().ToList();
                var boxVMs = new List<Models.AndroidBoxVM>();

                foreach (var item in boxs)
                {
                    if (item.LocationID == locationId)
                    {
                        var b = new Models.AndroidBoxVM
                        {
                            Name = item.BoxName,
                            Description = item.Description,
                            BoxId = item.BoxID,
                            LocationId = item.LocationID,

                        };
                        boxVMs.Add(b);
                    }
                }       
                //get Screen list By Location ID
                IScreenService screenService = DependencyUtils.Resolve<IScreenService>();
                var screens = screenService.Get().ToList();
                var screenVMs = new List<Models.ScreenVM>();

                foreach (var item in screens)
                {
                    if (item.LocationID == locationId)
                    {
                        var b = new Models.ScreenVM
                        {
                            Name = item.ScreenName,
                            Description = item.Description,
                            ResolutionId = item.ResolutionID,
                            ScreenId = item.ScreenID,
                            LocationId = item.LocationID,
                        };
                        screenVMs.Add(b);
                    }                                   
                }
                return Json(new
                {
                    boxListByLocationId = boxVMs,
                    screenListByLocationId = screenVMs
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }

    }
}