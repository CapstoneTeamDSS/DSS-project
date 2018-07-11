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
    [Authorize(Roles = "Admin")]
    public class ScreenController : Controller
    {
        IScreenService screenService = DependencyUtils.Resolve<IScreenService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();
        // GET: Screen
        public ActionResult Index()
        {
            ViewBag.screensList = GetScreenIdByBrandId();
            return View();
        }
        //ToanTXSE
        //Get screen List by location ID
        public static List<Models.ScreenVM> GetScreenIdByBrandId()
        {
            IScreenService screenService = DependencyUtils.Resolve<IScreenService>();
            var ScreenVM = new List<Models.ScreenVM>();
            var user = Helper.GetCurrentUser();
            var screenList = screenService.GetScreenIdByBrandId(user.BrandID);
            foreach (var item in screenList)
            {
                var m = new Models.ScreenVM
                {
                    Name = item.ScreenName,
                    Description = item.Description,
                    ResolutionId = item.ResolutionID,
                    ScreenId = item.ScreenID,
                    isHorizontal = item.isHorizontal,
                    LocationId = item.LocationID,
                };
                ScreenVM.Add(m);
            }
            return ScreenVM;
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
        // GET: Screen/GetValueRadioButton
        public bool GetValueRadioButton(string name)
        {
            if (name.ToString().Equals("Landscape"))
            {
                return true;
            }
            return false;
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
                        ScreenId = screen.ScreenID, //truyen screen Id qua view de phan biet update hay addnew
                        Description = screen.Description,
                        ResolutionId = screen.ResolutionID,
                        LocationId = screen.LocationID,
                        isHorizontal = screen.isHorizontal,
                    };
                }
            }
            ViewBag.locationList = LocationController.GetLocationList();
            ViewBag.resolutionList = ResolutionController.GetResolutionList();
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
                    isHorizontal = model.isHorizontal,

                };
                await this.screenService.CreateAsync(screen);
                //return this.RedirectToAction("Index");
                return new ContentResult
                {
                    Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "Screen")),
                    ContentType = "text/html"
                };
            }
            return View("Form", model);
        }
        // POST: Screen/Update
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