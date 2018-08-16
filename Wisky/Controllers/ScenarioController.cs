using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using DSS.Data.Models.Entities.Services;

namespace DSS.Controllers
{
    [Authorize(Roles = "Admin, Active User")]
    public class ScenarioController : Controller
    {
        IScenarioService scenarioService = DependencyUtils.Resolve<IScenarioService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();
        // GET: Scenario
        public ActionResult Index()
        {
            ViewBag.scenariosList = GetScenariosByBrandId();
            return View();
        }

        public static List<Models.ScenarioVM> GetScenariosByBrandId()
        {
            IScenarioService scenarioService = DependencyUtils.Resolve<IScenarioService>();
            var scenarioVMs = new List<Models.ScenarioVM>();
            IBrandService brandService = DependencyUtils.Resolve<IBrandService>();
            var user = Helper.GetCurrentUser();
            var scenarioList = scenarioService.GetScenarioIdByBrandId(user.BrandID);
            foreach (var item in scenarioList)
            {
                var s = new Models.ScenarioVM
                {
                    ScenarioId = item.ScenarioID,
                    Description = item.Description,
                    LayoutId = item.LayoutID,
                    Title = item.Title,
                    IsPublic = (bool) item.isPublic
                };
                scenarioVMs.Add(s);
            }
            return scenarioVMs;
        }

        public static List<Models.ScenarioRefVM> GetScenarioReferenceByBrandId(bool isHorizontal)
        {
            IScenarioService scenarioService = DependencyUtils.Resolve<IScenarioService>();
            IBrandService brandService = DependencyUtils.Resolve<IBrandService>();
            var user = Helper.GetCurrentUser();
            var scenarioRefVMs = new List<Models.ScenarioRefVM>();
            var scenarioList = scenarioService.GetScenarioIdByBrandIdAndLayoutType(user.BrandID, isHorizontal);
            foreach (var item in scenarioList)
            {
                var s = new Models.ScenarioRefVM
                {
                    ScenarioId = item.ScenarioID,
                    Title = item.Title,
                };
                scenarioRefVMs.Add(s);
            }
            return scenarioRefVMs;
        }

        // GET: Scenario/Delete/:id
        public ActionResult Delete(int id)
        {
            IScenarioItemService scerarioItemService = DependencyUtils.Resolve<IScenarioItemService>();
            var scenario = this.scenarioService.FirstOrDefault(a => a.ScenarioID == id);
            var user = Helper.GetCurrentUser();
            var scenarioItem = scerarioItemService.GetItemListByScenarioId(id);
            if (scenarioItem != null && scenario != null && scenario.BrandID == user.BrandID)
            {
                foreach (var i in scenarioItem)
                {
                    scerarioItemService.Delete(i);
                }
                this.scenarioService.Delete(scenario);
            }
            return this.RedirectToAction("Index");
        }

        // GET: Scenario/Form/:id
        public ActionResult Form(int? id)
        {
            Models.ScenarioDetailVM model = null;
            if (id != null)
            {
                var scenario = this.scenarioService.GetById(id??-1);
                if (scenario != null)
                {
                    model = new Models.ScenarioDetailVM
                    {
                        ScenarioId = scenario.ScenarioID,
                        LayoutId = scenario.LayoutID,
                        IsPublic = scenario.isPublic??true,
                        AudioArea = scenario.AudioArea,
                        Title = scenario.Title,
                        Description = scenario.Description,
                    };
                }
            }
            ViewBag.playlistList = PlaylistController.GetPlaylistIdByBrandId();
            return View(model);
        }
        //TOANTXSE
        // POST: Scenario/CheckScenarioIdIsUsed  
        [HttpPost]
        public JsonResult CheckScenarioIdIsUsed(int id)
        {
            try
            {
                IDeviceScenarioService deviceScenarioService = DependencyUtils.Resolve<IDeviceScenarioService>();
                var deviceScenario = deviceScenarioService.Get(a => a.ScenarioID == id).FirstOrDefault();
                return Json(new
                {
                    isUsing = deviceScenario != null,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //POST: Scenario/Add
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Add(Models.ScenarioDetailVM model)
        {
            IScenarioItemService scenarioItemService = DependencyUtils.Resolve<IScenarioItemService>();
            //TrinhNNP
            if (ModelState.IsValid)
            {
                /*Add scenario*/
                var user = Helper.GetCurrentUser();
                var scenario = new Data.Models.Entities.Scenario
                {
                    Title = model.Title,
                    Description = model.Description,
                    LayoutID = model.LayoutId,
                    BrandID = user.BrandID,
                    isPublic = model.IsPublic,
                    AudioArea = model.AudioArea,
                };
                await this.scenarioService.CreateAsync(scenario);
                /*Add scenario items*/
                if (model.PlaylistAreaArr != null)
                {
                    foreach (var item in model.PlaylistAreaArr)
                    {
                        var i = 0;
                        if (item.PlaylistIds != null)
                        {
                            foreach (var playlist in item.PlaylistIds)
                            {
                                var scenarioItem = new Data.Models.Entities.ScenarioItem
                                {
                                    AreaID = item.AreaId,
                                    PlaylistID = playlist,
                                    DisplayOrder = i++,
                                    ScenarioID = scenario.ScenarioID,
                                    LayoutID = scenario.LayoutID,
                                };
                                await scenarioItemService.CreateAsync(scenarioItem);
                            }
                        }
                    }
                }
                return Json(new
                {
                    success = true,
                    url = "/Scenario/Index",
                }, JsonRequestBehavior.AllowGet);

            }
            return Json(new
            {
                success = false,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LoadLayoutList(bool isHorizontal)
        {
            ILayoutService layoutService = DependencyUtils.Resolve<ILayoutService>();
            var layoutList = layoutService.Get(a=>a.isHorizontal==isHorizontal).ToList();
            var layoutVMs = new List<Models.LayoutVM>();
            if (layoutList != null)
            {
                foreach (var item in layoutList)
                {
                    var l = new Models.LayoutVM
                    {
                        LayoutID = item.LayoutID,
                        Title = item.Title,
                        isHorizontal = item.isHorizontal,
                        Description = item.Description,
                        URL = item.URL,
                    };
                    layoutVMs.Add(l);
                }
            }
            //return layoutVMs;
            return Json(new
            {
                LayoutVMs = layoutVMs,
            }, JsonRequestBehavior.AllowGet);
        }

        //GET
        //Scenario/UpdateForm/Id
        public ActionResult UpdateForm(int? id)
        {
            Models.ScenarioVM model = null;
            if (id != null)
            {
                var scenario = this.scenarioService.FirstOrDefault(a => a.ScenarioID == id);
                if (scenario != null)
                {
                    model = new Models.ScenarioVM
                    {   
                        
                        ScenarioId = scenario.ScenarioID,
                        Description = scenario.Description,
                        LayoutId = scenario.LayoutID,
                        Title = scenario.Title,
                        IsPublic = (bool) scenario.isPublic
                    };
                }
            }
            return View("UpdateForm", model);
        }

        // POST: Scenario/Update/Model
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Update(Models.ScenarioVM model)
        {
            if (ModelState.IsValid)
            {
                var scenario = this.scenarioService.FirstOrDefault(a => a.ScenarioID == model.ScenarioId);
                if (scenario != null)
                {
                    scenario.LayoutID = model.LayoutId;
                    scenario.Description = model.Description;
                    scenario.Title = model.Title;
                    scenario.isPublic = model.IsPublic;
                    scenario.UpdateDateTime = DateTime.Now;
                }
                await this.scenarioService.UpdateAsync(scenario);
                return this.RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult LoadLayout(int layoutId)
        {
            ILayoutService layoutService = DependencyUtils.Resolve<ILayoutService>();
            string layoutSrc = "";
            layoutSrc = layoutService.Get(a => a.LayoutID == layoutId).FirstOrDefault()?.LayoutSrc;
            return PartialView(layoutSrc);
        }

        [HttpPost]
        public ActionResult LoadPlaylistInfo(int playlistId)
        {
            IPlaylistService playlistService = DependencyUtils.Resolve<IPlaylistService>();
            var playlist = playlistService.GetById(playlistId);
            return Json(new
            {
                Id = playlist.PlaylistID,
                Description = playlist.Description,
                Title = playlist.Title,
                Duration = "00:00:00",
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LoadAreaPlaylist(int areaId, int scenarioId)
        {
            IScenarioItemService scenarioItemService = DependencyUtils.Resolve<IScenarioItemService>();
            IPlaylistService playlistService = DependencyUtils.Resolve<IPlaylistService>();
            var ScenarioItems = scenarioItemService.GetItemListByAreaScenarioId(areaId, scenarioId);
            var ScenarioItemVMs = new List<Models.ScenarioItemVM>();
            var ScenarioItemIds = new List<int>();
            if (scenarioItemService != null)
            {
                foreach (var item in ScenarioItems)
                {
                    ScenarioItemIds.Add(item.PlaylistID);
                }
            }
            return Json(new
            {
                ScenarioItemIds = ScenarioItemIds,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LoadPlaylistList()
        {
            var PlaylistList = PlaylistController.GetPlaylistIdByBrandIdAndStatus() as List<Models.PlaylistDetailVM>;
            return Json(new
            {
                PlaylistList = PlaylistList,
            }, JsonRequestBehavior.AllowGet);
        }

        //GET
        //Scenario/UpdateDetail/Id
        public ActionResult UpdateDetails(int? id)
        {
            Models.ScenarioVM model = null;
            if (id != null)
            {
                var scenario = this.scenarioService.FirstOrDefault(a => a.ScenarioID == id);
                if (scenario != null)
                {
                    model = new Models.ScenarioVM
                    {
                        ScenarioId = scenario.ScenarioID,
                        Description = scenario.Description,
                        LayoutId = scenario.LayoutID,
                        Title = scenario.Title,
                    };
                }
            }
            return View("UpdateDetails", model);
        }

        //POST
        //Scenario/UpdateDetail/Id
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> UpdateDetails(Models.PlaylistAreaObj model)
        {
            IScenarioItemService scenarioItemService = DependencyUtils.Resolve<IScenarioItemService>();
            //TrinhNNP
            if (ModelState.IsValid)
            {
                /*Delete items scenario*/
                var ScenarioItems = scenarioItemService.GetItemListByScenarioId(model.ScenarioId);
                if (ScenarioItems != null)
                {
                    foreach (var item in ScenarioItems)
                    {
                        await scenarioItemService.DeleteAsync(item);
                    }
                }
                if (model.PlaylistAreaArr != null)
                {
                    foreach (var item in model.PlaylistAreaArr)
                    {
                        var i = 0;
                        if (item.PlaylistIds != null)
                        {
                            foreach (var playlist in item.PlaylistIds)
                            {
                                var scenarioItem = new Data.Models.Entities.ScenarioItem
                                {
                                    AreaID = item.AreaId,
                                    PlaylistID = playlist,
                                    DisplayOrder = i++,
                                    ScenarioID = model.ScenarioId,
                                };
                                await scenarioItemService.CreateAsync(scenarioItem);
                            }
                        }
                    }
                }
                return Json(new
                {
                    success = true,
                    url = "/Scenario/Index",
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                success = false,
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateStatus(int dataId)
        {
            bool result = false;
            var scenario = this.scenarioService
                .Get(a => a.ScenarioID == dataId)
                .FirstOrDefault();
            if (scenario != null)
            {
                scenario.isPublic = !scenario.isPublic;
                this.scenarioService.Update(scenario);
                result = true;
            }
            return Json(new
            {
                success = result,
            }, JsonRequestBehavior.AllowGet);

        }
    }
}