using DSS.Data.Models.Entities.Services;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSS.Controllers
{
    [Authorize(Roles = "Admin, Active User")]
    public class SchedulingController : Controller
    {
        // GET: Scheduling
        public ActionResult Index()
        {
            ViewBag.ScheduleList = GetSchedulesByBrandId();
            return View();
        }

        public static List<Models.ScheduleVM> GetSchedulesByBrandId()
        {
            IDeviceScenarioService deviceScenarioService = DependencyUtils.Resolve<IDeviceScenarioService>();
            var scheduleVMs = new List<Models.ScheduleVM>();
            IBrandService brandService = DependencyUtils.Resolve<IBrandService>();
            var user = Helper.GetCurrentUser();
            var scheduleList = deviceScenarioService.GetSchedulesByBrandID(user.BrandID);
            IDeviceService deviceService = DependencyUtils.Resolve<IDeviceService>();
            IScenarioService scenarioService = DependencyUtils.Resolve<IScenarioService>();
            foreach (var item in scheduleList)
            {
                var s = new Models.ScheduleVM
                {
                    ScenarioID = item.ScenarioID,
                    DeviceScenarioId = item.DeviceScenationID,
                    DeviceID = item.DeviceID,
                    DeviceName = deviceService.GetDeviceNameByID(item.DeviceID),
                    ScenarioTitle = scenarioService.GetScenarioNameById(item.ScenarioID),
                    StartTime = item.StartTime,
                    TimeToPlay = item.TimesToPlay,
                    EndTime = item.EndTime,
            };
                scheduleVMs.Add(s);
            }
            return scheduleVMs;
        }

        // GET: Scheduling/Form/:id
        public ActionResult Form(int? id)
        {
            Models.ScheduleAddVM model = null;
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
                        isHorizontal = scenarioService.GetScenarioOrientationById(item.ScenarioID)??true,
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

        bool CheckTimeValid(int? deviceID, DateTime startTime, DateTime? endTime)
        {
            var result = true;
            if (startTime > endTime)
            {
                result = false;
            } else 
            {
                IDeviceScenarioService deviceScenarioService = DependencyUtils.Resolve<IDeviceScenarioService>();
                result = deviceScenarioService.CheckTimeValid(deviceID ?? -1, startTime, endTime ?? DateTime.MinValue);
            }            
            return result;
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Update(Models.ScheduleAddVM model)
        {
            IDeviceScenarioService deviceScenarioService = DependencyUtils.Resolve<IDeviceScenarioService>();
            if (ModelState.IsValid && CheckTimeValid(model.DeviceID, model.StartTime, model.EndTime))
            {
                if (model.isFixed)
                {
                    model.TimeToPlay = null;
                }
                else
                {
                    model.EndTime = null;
                }
                var schedule = deviceScenarioService.Get(model.DeviceScenarioId);
                if (schedule != null)
                {
                    schedule.ScenarioID = model.ScenarioID;
                    schedule.DeviceID = model.DeviceID;
                    schedule.EndTime = model.EndTime;
                    schedule.StartTime = model.StartTime;
                    schedule.TimesToPlay = model.TimeToPlay;
                }
                await deviceScenarioService.UpdateAsync(schedule);
                return this.RedirectToAction("Index");
            }
            return View("Form", model);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Add(Models.ScheduleAddVM model)
        {
            IDeviceScenarioService deviceScenarioService = DependencyUtils.Resolve<IDeviceScenarioService>();
            if (ModelState.IsValid && CheckTimeValid(model.DeviceID, model.StartTime, model.EndTime))
            {
                if (model.isFixed)
                {
                    model.TimeToPlay = null;
                } else
                {
                    model.EndTime = null;
                }
                IScenarioService scenarioService = DependencyUtils.Resolve<IScenarioService>();
                var schedule = new Data.Models.Entities.DeviceScenario
                {
                    ScenarioID = model.ScenarioID,
                    DeviceID = model.DeviceID,
                    TimesToPlay = model.TimeToPlay,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    LayoutID = scenarioService.GetLayoutIDById(model.ScenarioID),
                };
                await deviceScenarioService.CreateAsync(schedule);
                return this.RedirectToAction("Index");
            }
            return View("Form", model);
        }

        // GET: Schedule/Delete/:id
        public ActionResult Delete(int id)
        {
            IDeviceScenarioService deviceScenarioService = DependencyUtils.Resolve<IDeviceScenarioService>();
            var schedule = deviceScenarioService.Get(id);
            if (schedule != null)
            {
                deviceScenarioService.Delete(schedule);
            }
            return this.RedirectToAction("Index");
        }
    }
}