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
    public class AndroidBoxController : Controller
    {
        IBoxService boxService = DependencyUtils.Resolve<IBoxService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();
        // GET: AndroidBox
        public ActionResult Index()
        {
            //var boxs = this.boxService.Get().ToList();
            //var boxVMs = new List<Models.AndroidBoxVM>();

            //foreach (var item in boxs)
            //{
            //    var b = new Models.AndroidBoxVM
            //    {
            //        Name = item.BoxName,
            //        Description = item.Description,
            //        BoxId = item.BoxID,
            //        LocationId = item.LocationID

            //    };
            //    boxVMs.Add(b);
            //}
            ViewBag.boxsList = GetBoxIdByBrandId();
            return View();
        }
        //ToanTXSE
        //Get box List by location ID
        public static List<Models.AndroidBoxVM> GetBoxIdByBrandId()
        {
            IBoxService boxService = DependencyUtils.Resolve<IBoxService>();
            var AndroidBoxVM = new List<Models.AndroidBoxVM>();
            IBrandService brandService = DependencyUtils.Resolve<IBrandService>();
            var user = Helper.GetCurrentUser();
            var boxList = boxService.GetBoxIdByBrandId(user.BrandID);
            foreach (var item in boxList)
            {
                var m = new Models.AndroidBoxVM
                {
                    Name = item.BoxName,
                    Description = item.Description,
                    BoxId = item.BoxID,
                    LocationId = item.LocationID
                };
                AndroidBoxVM.Add(m);
            }
            return AndroidBoxVM;
        }
        // GET: AndroidBox/Delete/:id
        public ActionResult Delete(int id)
        {
            var box = this.boxService.Get(id);
            if (box != null)
            {
                this.boxService.Delete(box);
            }
            return this.RedirectToAction("Index");
        }
        // GET: AndroidBox/Form/:id
        public ActionResult Form(int? id)
        {
            Models.AndroidBoxVM model = null;

            if (id != null)
            {
                var box = this.boxService.Get(id);
                if (box != null)
                {
                    model = new Models.AndroidBoxVM
                    {
                        Name = box.BoxName,
                        Description = box.Description,
                        BoxId = box.BoxID,
                        LocationId = box.LocationID
                    };
                }
            }
            ViewBag.locationList = LocationController.GetLocationIdByBrandId();
            return View(model);
        }
        // POST: AndroidBox/Add
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Add(Models.AndroidBoxVM model)
        {
            if (ModelState.IsValid)
            {
                var box = new Data.Models.Entities.Box
                {
                    BoxName = model.Name,
                    Description = model.Description,
                    LocationID = model.LocationId
                };
                await this.boxService.CreateAsync(box);
                //return this.RedirectToAction("Index");
                return new ContentResult
                {
                    Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "AndroidBox")),
                    ContentType = "text/html"
                };
            }
            return View("Form", model);
        }
        // POST: Anrdoid Box/CheckAndroidBoxIdIsMatching  
        [HttpPost]
        public JsonResult CheckAndroidBoxIdIsMatching(int id)
        {
            try
            {
                //Get device by screen Id
                IDeviceService deviceService = DependencyUtils.Resolve<IDeviceService>();
                var device = deviceService.Get(a => a.BoxID == id).FirstOrDefault();
                //bool isUsing = true;
                //if (device == null)
                //{
                //    isUsing = false;
                //}
                DSS.Models.MatchingDeviceVM deviceVM = null;
                if (device != null)
                {
                    deviceVM = new DSS.Models.MatchingDeviceVM
                    {
                        Title = device.Title,
                    };
                }
                return Json(new
                {
                    isUsing = device != null,
                    deviceVM = deviceVM,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // POST: AndroidBox/Update
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Update(Models.AndroidBoxVM model)
        {
            if (ModelState.IsValid)
            {
                var box = this.boxService.Get(model.BoxId);
                if (box != null)
                {
                    box.BoxName = model.Name;
                    box.Description = model.Description;
                    box.LocationID = model.LocationId;
                    
                }
                await this.boxService.UpdateAsync(box);
                return new ContentResult
                {
                    Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "AndroidBox")),
                    ContentType = "text/html"
                };
            }
            return View("Form", model);
        }
    }
}