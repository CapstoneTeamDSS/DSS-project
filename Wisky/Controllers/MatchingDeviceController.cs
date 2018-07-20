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
    [Authorize(Roles = "Admin")]
    public class MatchingDeviceController : Controller
    {
        IDeviceService deviceService = DependencyUtils.Resolve<IDeviceService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();
        IScreenService screenService = DependencyUtils.Resolve<IScreenService>();
        IBoxService boxService = DependencyUtils.Resolve<IBoxService>();


        // GET: MatchingDevice/index
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
                    BoxName = boxService.GetBoxNameByID(item.BoxID),
                    ScreenName = screenService.GetScreenNameByID(item.ScreenID),
                    Title = item.Title
                };
                deviceVMs.Add(b);
            }
            ViewBag.devicesList = deviceVMs;
            ViewBag.locationStringList = GetLocationIdByBrandId();
            return View();
        }

        public static List<Models.DeviceRefVM> GetDeviceReferenceByBrandId(bool isHorizontal)
        {
            IDeviceService deviceService = DependencyUtils.Resolve<IDeviceService>();
            IBrandService brandService = DependencyUtils.Resolve<IBrandService>();
            var user = Helper.GetCurrentUser();
            var deviceVMs = new List<Models.DeviceRefVM>();
            var deviceList = deviceService.GetDeviceByBrandIdAndScreenType(user.BrandID, isHorizontal);
            foreach (var item in deviceList)
            {
                var s = new Models.DeviceRefVM
                {
                    DeviceId = item.DeviceID,
                    Title = item.Title,
                };
                deviceVMs.Add(s);
            }
            return deviceVMs;
        }

        // GET: Matching/Form/:id
        public ActionResult Form(int? id, string boxId, string screenId)
        {
            Models.MatchingDeviceVM model = null;
            if (id != null)
            {
                var device = this.deviceService.Get(id);
                if (device != null)
                {
                    model = new Models.MatchingDeviceVM
                    {
                        DeviceId = device.DeviceID,
                        Title = device.Title,
                        ScreenId = device.ScreenID,
                        BoxId = device.BoxID,
                        ScreenName = screenService.GetScreenNameByID(device.ScreenID),
                        BoxName = boxService.GetBoxNameByID(device.BoxID),
                        Description = device.Description
                    };
                }
            }
            else
            {
                int boxID = Int32.Parse(boxId);
                int screenID = Int32.Parse(screenId);
                model = new Models.MatchingDeviceVM
                {
                    ScreenName = screenService.GetScreenNameByID(screenID),
                    BoxName = boxService.GetBoxNameByID(boxID),
                    ScreenId = screenID,
                    BoxId = boxID,
                };
            }
            return View(model);
        }

        public static List<Models.LocationStringVM> GetLocationIdByBrandId()
        {
            ILocationService locationService = DependencyUtils.Resolve<ILocationService>();
            var locationStringList = new List<Models.LocationStringVM>();
            IBrandService brandService = DependencyUtils.Resolve<IBrandService>();
            var user = Helper.GetCurrentUser();
            var locationList = locationService.GetLocationIdByBrandId(user.BrandID);
            foreach (var item in locationList)
            {
                var m = new Models.LocationStringVM
                {
                    LocationId = item.LocationID,
                    LocationStringList = item.Address + ", Quận " + item.District + ", TP." + item.Province,

                };
                locationStringList.Add(m);
            }
            return locationStringList;
        }
        // GET: MatchingDevice/ReceiveLocationId  
        [HttpPost]
        public JsonResult ReceiveLocationId(string id)
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
        }
        // POST: MatchingDevice/Add
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Add(Models.MatchingDeviceVM model)
        {
            DateTime aDateTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                var device = new Data.Models.Entities.Device
                {
                    CreateDatetime = aDateTime,
                    BoxID = model.BoxId,
                    ScreenID = model.ScreenId,
                    Title = model.Title,
                    Description = model.Description,
                    BrandID = Helper.GetCurrentUser().BrandID,
                };
                await this.deviceService.CreateAsync(device);
                //return this.RedirectToAction("Index");
                return new ContentResult
                {
                    Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "MatchingDevice")),
                    ContentType = "text/html"
                };
            }
            return View("Form", model);
        }

        // POST: MatchingDevice/Update
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Update(Models.MatchingDeviceVM model)
        {
            if (ModelState.IsValid)
            {
                var device = this.deviceService.Get(model.DeviceId);
                if (device != null)
                {
                    device.DeviceID = (int)model.DeviceId;
                    device.BoxID = model.BoxId;
                    device.ScreenID = model.ScreenId;
                    device.Title = model.Title;
                    device.Description = model.Description;

                }
                await this.deviceService.UpdateAsync(device);
                return new ContentResult
                {
                    Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "MatchingDevice")),
                    ContentType = "text/html"
                };
            }
            return View("Form", model);
        }

        // GET: MatchingDevice/Delete/:id
        public ActionResult Delete(int id)
        {
            var device = this.deviceService.Get(id);
            if (device != null)
            {
                this.deviceService.Delete(device);
            }
            return this.RedirectToAction("Index");
        }

    }
}