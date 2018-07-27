using DSS.Data.Models.Entities.Services;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSS.Controllers
{
    public class AdvancedSchedulingController : Controller
    {
        // GET: AdvancedScheduling
        public ActionResult Index()
        {
            return View();
        }

        // GET: Scheduling/Form/:id
        public ActionResult Form(int? id)
        {
            Models.ScheduleAddVM model = null;
            ViewBag.TimeSlotList = this.GetTimeSlots();
            if (id != null)
            {
                IDeviceScenarioService deviceScenarioService = DependencyUtils.Resolve<IDeviceScenarioService>();
                IDeviceService deviceService = DependencyUtils.Resolve<IDeviceService>();
                IScenarioService scenarioService = DependencyUtils.Resolve<IScenarioService>();
                var item = deviceScenarioService.Get(id);
                if (item != null)
                {
                    model = new Models.ScheduleAddVM
                    {
                        ScenarioID = item.ScenarioID,
                        DeviceScenarioId = item.DeviceScenationID,
                        DeviceID = item.DeviceID,
                        isHorizontal = scenarioService.GetScenarioOrientationById(item.ScenarioID) ?? true,
                        isFixed = item.TimesToPlay == null,
                        StartTime = item.StartTime,
                        TimeToPlay = item.TimesToPlay,
                        EndTime = item.EndTime,
                        LayoutID = item.LayoutID,
                    };
                }
            }
            return View(model);
        }

        private List<DSS.Models.TimeSlotVM> GetTimeSlots()
        {
            ITimeSlotService timeSlotService = DependencyUtils.Resolve<ITimeSlotService>();
            var TimeSlotList = timeSlotService.Get().ToList();
            var TimeSlotVMs = new List<DSS.Models.TimeSlotVM>();
            if (TimeSlotList != null)
            {
                foreach (var item in TimeSlotList)
                {
                    var timeSlotVM = new DSS.Models.TimeSlotVM
                    {
                        SlotID = item.SlotID,
                        SlotDetail = item.SlotDetail,
                    };
                    TimeSlotVMs.Add(timeSlotVM);
                }
            }
            return TimeSlotVMs;
        }

        //GET: Scheduling/LoadReference?isHorizontal=
        public ActionResult LoadReference(bool isHorizontal)
        {
            var DeviceList = MatchingDeviceController.GetDeviceReferenceByBrandId(isHorizontal);
            var ScenarioList = ScenarioController.GetScenarioReferenceByBrandId(isHorizontal);
            return Json(new
            {
                DeviceList = DeviceList,
                ScenarioList = ScenarioList,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}