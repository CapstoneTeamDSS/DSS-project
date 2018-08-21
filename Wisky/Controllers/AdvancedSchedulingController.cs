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
        IScheduleService scheduleService = DependencyUtils.Resolve<IScheduleService>();

        // GET: AdvancedScheduling
        public ActionResult Index()
        {
            IDeviceService deviceService = DependencyUtils.Resolve<IDeviceService>();
            IScenarioService scenarioService = DependencyUtils.Resolve<IScenarioService>();
            var ScheduleList = scheduleService.Get().ToList();
            var ScheduleVMs = new List<DSS.Models.AdvancedScheduleVM>();
            foreach (var item in ScheduleList)
            {
                var s = new DSS.Models.AdvancedScheduleVM
                {
                    ScenarioID = item.ScenarioID,
                    ScheduleID = item.ScheduleID,
                    DeviceID = item.DeviceID,
                    isEnable = item.isEnable,
                    DeviceName = deviceService.GetDeviceNameByID(item.DeviceID),
                    ScenarioTitle = scenarioService.GetScenarioNameById(item.ScenarioID),
                };
                ScheduleVMs.Add(s);
            }
            ViewBag.ScheduleList = ScheduleVMs;
            ViewBag.addSuccess = Session["ADD_RESULT"] ?? false;
            ViewBag.updateSuccess = Session["UPDATE_RESULT"] ?? false;
            Session.Clear();
            return View();
        }

        // GET: Scheduling/Form/:id
        public ActionResult Form(int? id)
        {
            Models.AdvancedScheduleAddVM model = null;
            ViewBag.TimeSlotList = this.GetTimeSlots();
            if (id != null)
            {
                IScheduleService scheduleService = DependencyUtils.Resolve<IScheduleService>();
                IDeviceService deviceService = DependencyUtils.Resolve<IDeviceService>();
                IScenarioService scenarioService = DependencyUtils.Resolve<IScenarioService>();
                var item = scheduleService.Get(id);
                if (item != null)
                {
                    model = new Models.AdvancedScheduleAddVM
                    {
                        ScenarioID = item.ScenarioID,
                        ScheduleID = item.ScheduleID,
                        DeviceID = item.DeviceID,
                        isHorizontal = scenarioService.GetScenarioOrientationById(item.ScenarioID) ?? true,
                        LayoutID = item.LayoutID,
                        Priority = item.Priority,
                        TimeFilterPoint = item.TimeFilter,
                        DayFilterPoint = item.DayFilter,
                        isEnable = item.isEnable,
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
                        StartTime = item.StartTime.ToString(),
                        EndTime = item.EndTime.ToString(),
                    };
                    TimeSlotVMs.Add(timeSlotVM);
                }
            }
            return TimeSlotVMs;
        }
        //TOANTXSE62006
        //GET: Playlist/UpdateStatus
        public ActionResult UpdateStatus(int dataId)
        {
            bool result = false;
            var schedule = this.scheduleService
                .Get(a => a.ScheduleID == dataId)
                .FirstOrDefault();
            if (schedule != null)
            {
                schedule.isEnable = !schedule.isEnable;
                this.scheduleService.Update(schedule);
                result = true;
            }
            return Json(new
            {
                success = result,
            }, JsonRequestBehavior.AllowGet);

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

        //AdvancedScheduling/Add
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Add(Models.AdvancedScheduleAddVM model)
        {
            IScheduleService scheduleService = DependencyUtils.Resolve<IScheduleService>();
            if (ModelState.IsValid)
            {
                int dayFilter = 0;
                int timeFilter = 0;
                foreach (var item in model.DayFilter)
                {
                    dayFilter += item;
                }
                foreach (var item in model.TimeFilter)
                {
                    timeFilter += item;
                }
                IScenarioService scenarioService = DependencyUtils.Resolve<IScenarioService>();
                var schedule = new Data.Models.Entities.Schedule
                {
                    ScenarioID = model.ScenarioID,
                    DeviceID = model.DeviceID,
                    LayoutID = scenarioService.GetLayoutIDById(model.ScenarioID),
                    DayFilter = dayFilter,
                    TimeFilter = timeFilter,
                    Priority = model.Priority,
                    isEnable = model.isEnable,
                };
                await scheduleService.CreateAsync(schedule);
                Session.Clear();
                Session["ADD_RESULT"] = true;
                return new ContentResult
                {
                    Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "AdvancedScheduling")),
                    ContentType = "text/html"
                };
            }
            return View("Form", model);
        }
        // GET: AdvancedScheduling/Delete/:id
        public ActionResult Delete(int id)
        {
            var scheduling = this.scheduleService.Get(id);
            if (scheduling != null)
            {
                this.scheduleService.Delete(scheduling);
            }
            return this.RedirectToAction("Index");
        }
        //AdvancedScheduling/Update
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Update(Models.AdvancedScheduleAddVM model)
        {
            IScheduleService scheduleService = DependencyUtils.Resolve<IScheduleService>();
            IScenarioService scenarioService = DependencyUtils.Resolve<IScenarioService>();
            if (ModelState.IsValid)
            {
                var schedule = scheduleService.Get(model.ScheduleID);
                int dayFilter = 0;
                int timeFilter = 0;
                foreach (var item in model.DayFilter)
                {
                    dayFilter += item;
                }
                foreach (var item in model.TimeFilter)
                {
                    timeFilter += item;
                }
                if (schedule != null)
                {
                    schedule.ScenarioID = model.ScenarioID;
                    schedule.DeviceID = model.DeviceID;
                    schedule.LayoutID = scenarioService.GetLayoutIDById(model.ScenarioID);
                    schedule.DayFilter = dayFilter;
                    schedule.TimeFilter = timeFilter;
                    schedule.Priority = model.Priority;
                    schedule.isEnable = model.isEnable;
                    await scheduleService.UpdateAsync(schedule);
                    Session.Clear();
                    Session["UPDATE_RESULT"] = true;
                }
                return new ContentResult
                {
                    Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "AdvancedScheduling")),
                    ContentType = "text/html"
                };
            }
            return View("Form", model);
        }

    }
}