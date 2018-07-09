using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using DSS.Data.Models.Entities.Services;

namespace DSS.Controllers
{
    [Authorize]
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
            var userService = DependencyUtils.Resolve<IAspNetUserService>();
            var username = System.Web.HttpContext.Current.User.Identity.Name;
            var user = userService.FirstOrDefault(a => a.UserName == username);
            var scenarioList = scenarioService.GetScenarioIdByBrandId(user.BrandID);
            foreach (var item in scenarioList)
            {
                var s = new Models.ScenarioVM
                {
                    ScenarioId = item.ScenarioID,
                    Description = item.Description,
                    LayoutId = item.LayoutID,
                    Title = item.Title
                };
                scenarioVMs.Add(s);
            }
            return scenarioVMs;
        }

        public static List<Models.ScenarioRefVM> GetScenarioReferenceByBrandId(bool isHorizontal)
        {
            IScenarioService scenarioService = DependencyUtils.Resolve<IScenarioService>();
            IBrandService brandService = DependencyUtils.Resolve<IBrandService>();
            Models.CurrentUserVM currUser = (Models.CurrentUserVM)System.Web.HttpContext.Current.Session["currentUser"];
            var scenarioRefVMs = new List<Models.ScenarioRefVM>();
            var scenarioList = scenarioService.GetScenarioIdByBrandIdAndLayoutType(currUser.BrandId, isHorizontal);
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
            var scenario = this.scenarioService.Get(id);
            if (scenario != null)
            {
                this.scenarioService.Delete(scenario);
            }
            return this.RedirectToAction("Index");
        }

        // GET: AndroidBox/Form/:id
        public ActionResult Form()
        {
            ViewBag.playlistList = PlaylistController.GetPlaylistIdByBrandId();
            ViewBag.layoutList = GetLayoutList();
            return View();
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
                var scenario = new Data.Models.Entities.Scenario
                {
                    Title = model.Title,
                    Description = model.Description,
                    LayoutID = model.LayoutId,
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

        public List<Models.LayoutVM> GetLayoutList()
        {
            ILayoutService layoutService = DependencyUtils.Resolve<ILayoutService>();
            var layoutList = layoutService.Get().ToList();
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
            return layoutVMs;
        }

        //GET
        //Scenario/UpdateForm/Id
        public ActionResult UpdateForm(int? id)
        {
            Models.ScenarioVM model = null;
            if (id != null)
            {
                var scenario = this.scenarioService.Get(id);
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
            return View("UpdateForm", model);
        }

        // POST: Scenario/Update/Model
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Update(Models.ScenarioVM model)
        {
            if (ModelState.IsValid)
            {
                var scenario = this.scenarioService.Get(model.ScenarioId);
                if (scenario != null)
                {
                    scenario.LayoutID = model.LayoutId;
                    scenario.Description = model.Description;
                    scenario.Title = model.Title;
                }
                await this.scenarioService.UpdateAsync(scenario);
                return this.RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult LoadLayout(int layoutId)
        {
            var url = "/Resource/Layout/Layout_" + layoutId + ".cshtml";
            return PartialView(url);
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
            IScenarioItemService scenarioItemService = DependencyUtils.Resolve<IScenarioItemService>();
            IPlaylistService playlistService = DependencyUtils.Resolve<IPlaylistService>();
            var PlaylistList = PlaylistController.GetPlaylistIdByBrandId() as List<Models.PlaylistDetailVM>;
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
                var scenario = this.scenarioService.Get(id);
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
    }
}