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
    public class ScreenController : Controller
    {
        IScreenService screenService = DependencyUtils.Resolve<IScreenService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();
        // GET: Screen
        public ActionResult Index()
        {
            var screens = this.screenService.Get().ToList();
            var screenVMs = new List<Models.ScreenVM>();

            foreach (var item in screens)
            {
                var b = new Models.ScreenVM
                {
                    Name = item.ScreenName,
                    Description =item.Description,                  
                    ResolutionId = item.ResolutionID,
                    ScreenId = item.ScreenID,
                    LocationId = item.LocationID,

                };
                screenVMs.Add(b);
            }
            ViewBag.screensList = screenVMs;
            return View();
        }
        // GET: Screen/Delete/:id
        public ActionResult Delete(int id)
        {
            var screen = this.screenService.Get(id);
            if (screen != null)
            {
                this.screenService.Delete(screen);
            }
            return this.RedirectToAction("Index");
        }
        // GET: Screen/Form/:id
        public ActionResult Form(int? id)
        {
            Models.ScreenVM model = null;

            if (id != null)
            {
                var screen = this.screenService.Get(id);
                if (screen != null)
                {
                    model = new Models.ScreenVM
                    {
                        Name = screen.ScreenName,
                        Description = screen.Description,
                        ResolutionId = screen.ResolutionID,
                        LocationId = screen.LocationID,
                    };
                }
            }
            return View(model);
        }
        // POST: Screen/Add
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Add(Models.ScreenVM model)
        {
            if (ModelState.IsValid)
            {
                var screen = new Data.Models.Entities.Screen
                {
                    ScreenName = model.Name,
                    Description = model.Description,
                    LocationID = model.LocationId,
                    ResolutionID= model.ResolutionId,
                };
                await this.screenService.CreateAsync(screen);
                return this.RedirectToAction("Index");
            }
            return View("Form", model);
        }
        // POST: Brand/Update
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Update(Models.ScreenVM model)
        {
            if (ModelState.IsValid)
            {
                var screen = this.screenService.Get(model.ScreenId);
                if (screen != null)
                {
                    screen.ScreenName = model.Name;
                    screen.Description = model.Description;
                    screen.LocationID = model.LocationId;
                    screen.ResolutionID = model.ResolutionId;
                }
                await this.screenService.UpdateAsync(screen);
                return this.RedirectToAction("Index");
            }
            return View("Form", model);
        }
    }
}