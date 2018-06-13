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
    public class ScenarioController : Controller
    {

        IScenarioService scenarioService = DependencyUtils.Resolve<IScenarioService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();
        // GET: Scenario
        public ActionResult Index()
        {
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
            ViewBag.scenariosList =scenariosVMs ;
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
        public ActionResult Form(int? id)
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
            return View(model);
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
        // POST: Scenario/Update
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Update(Models.ScenarioVM model)
        {
            if (ModelState.IsValid)
            {
                var scenario = this.scenarioService.Get(model.ScenarioId);
                if (scenario != null)
                {
                    scenario.Title = model.Title;
                    scenario.Description = model.Description;
                    scenario.LayoutID = model.LayoutId;

                }
                await this.scenarioService.UpdateAsync(scenario);
                return this.RedirectToAction("Index");
            }
            return View("Form", model);
        }
    }
}