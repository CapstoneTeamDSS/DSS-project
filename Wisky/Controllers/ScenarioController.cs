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
            IPlaylistService playlistService = DependencyUtils.Resolve<IPlaylistService>();
            var scenarios = this.scenarioService.Get().ToList();
            var scenariosVMs = new List<Models.ScenarioVM>();

            foreach (var item in scenarios)
            {
                var b = new Models.ScenarioVM
                {
                    ScenarioId = item.ScenarioID,
                    Description = item.Description,
                    LayoutId = item.LayoutID,
                    Title = item.Title
                };
                scenariosVMs.Add(b);
            }
            ViewBag.scenariosList = scenariosVMs;
            return View();
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

        // POST: Scenario/Add
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Add(Models.ScenarioVM model)
        {
            if (ModelState.IsValid)
            {
                var scenario = new Data.Models.Entities.Scenario
                {
                    Title = model.Title,
                    Description = model.Description,
                    LayoutID = model.LayoutId
                };
                await this.scenarioService.CreateAsync(scenario);
                return this.RedirectToAction("Index");
            }
            return View("Form", model);
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

        // POST: Scenario/Update
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
        public JsonResult LoadAreaPlaylist(string areaIdStr, string scenarioIdStr)
        {
            int AreaId = Int32.Parse(areaIdStr);
            int ScenarioId = Int32.Parse(scenarioIdStr);
            IScenarioItemService scenarioItemService = DependencyUtils.Resolve<IScenarioItemService>();
            IPlaylistService playlistService = DependencyUtils.Resolve<IPlaylistService>();
            var ScenarioItems = scenarioItemService.GetItemListByAreaScenarioId(AreaId, ScenarioId);
            var ScenarioItemVMs = new List<Models.ScenarioItemVM>();
            var PlaylistList = PlaylistController.GetPlaylistIdByBrandId();
            if (scenarioItemService != null)
            {
                foreach (var item in ScenarioItems)
                {
                    var p = playlistService.GetById(item.PlaylistID);
                    var s = new Models.ScenarioItemVM
                    {
                        ScenarioItemId = item.ScenarioItemID,
                        ScenarioId = item.ScenarioID,
                        PlaylistId = item.PlaylistID,
                        Note = item.Note,
                        AreaId = item.AreaID,
                        DisplayOrder = item.DisplayOrder,
                        Title = p.Title,
                        Duration = "Not Yet",
                    };
                    ScenarioItemVMs.Add(s);
                }
            }
            if (scenarioItemService != null)
            {
                foreach (var item in ScenarioItems)
                {
                    var p = playlistService.GetById(item.PlaylistID);
                    var s = new Models.ScenarioItemVM
                    {
                        ScenarioItemId = item.ScenarioItemID,
                        ScenarioId = item.ScenarioID,
                        PlaylistId = item.PlaylistID,
                        Note = item.Note,
                        AreaId = item.AreaID,
                        DisplayOrder = item.DisplayOrder,
                        Title = p.Title,
                        Duration = "Not Yet",
                    };
                    ScenarioItemVMs.Add(s);
                }
            }
            return Json(new
            {
                ScenarioItems = ScenarioItemVMs,
                PlaylistList = PlaylistList
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateDetails()
        {
            return View("UpdateDetails");
        }
    }
}