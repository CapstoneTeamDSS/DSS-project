﻿using DSS.Data.Models.Entities.Services;
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
                    DeviceName = deviceService.GetDeviceNameByID(item.DeviceID),
                    ScenarioTitle = scenarioService.GetScenarioNameById(item.ScenarioID),
                };
                ScheduleVMs.Add(s);
            }
            ViewBag.ScheduleList = ScheduleVMs;
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
                        DayFilterPoint = item.DayFilter
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
                };
                await scheduleService.CreateAsync(schedule);
                return new ContentResult
                {
                    Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "AdvancedScheduling")),
                    ContentType = "text/html"
                };
            }
            return View("Form", model);
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
                    await scheduleService.UpdateAsync(schedule);
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