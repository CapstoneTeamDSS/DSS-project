using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DSS.Data.Models.Entities.Services;

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



            return View();
        }
    }
}