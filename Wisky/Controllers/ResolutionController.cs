using AutoMapper;
using DSS.Data.Models.Entities.Services;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSS.Controllers
{
    //[Authorize]
    public class ResolutionController : Controller
    {
        IResolutionService resolutionService = DependencyUtils.Resolve<IResolutionService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();

        //GET: Brand/Index
        public ActionResult Index()
        {
            var resolutions = this.resolutionService.Get().ToList();
            var resolutionVMs = new List<Models.ResolutionDetailVM>();

            foreach (var item in resolutions)
            {
                var b = new Models.ResolutionDetailVM
                {
                    Note = item.Note,
                    Width = item.Width,
                    Height = item.Height,
                    Id = item.ResolutionID,
                };
                resolutionVMs.Add(b);
            }
            ViewBag.brandList = resolutionVMs;
            return View();
        }
        public static List<Models.ResolutionDetailVM> GetResolutionList()
        {
            IResolutionService resolutionService = DependencyUtils.Resolve<IResolutionService>();
            var resolutions = resolutionService.Get().ToList();
            var resolutionVMs = new List<Models.ResolutionDetailVM>();

            foreach (var item in resolutions)
            {
                var b = new Models.ResolutionDetailVM
                {
                   Height=item.Height,
                   Id=item.ResolutionID,
                   Width=item.Width,
                   Note=item.Note,
                };
                resolutionVMs.Add(b);
            }
            return resolutionVMs;
        }

        // GET: Brand/Form/:id
        public ActionResult Form(int? id)
        {
            Models.ResolutionDetailVM model = null;

            if (id != null)
            {
                var resolution = this.resolutionService.Get(id);
                if (resolution != null)
                {
                    model = new Models.ResolutionDetailVM
                    {
                        Note = resolution.Note,
                        Width = resolution.Width,
                        Height = resolution.Height,
                        Id = resolution.ResolutionID,
                    };
                }
            }
            return View("Form", model);
        }

        // POST: Resolution/Add
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Add(Models.ResolutionDetailVM model)
        {
            if (ModelState.IsValid)
            {
                var resolution = new Data.Models.Entities.Resolution
                {
                    Width = model.Width,
                    Height = model.Height,
                    Note = model.Note,
                };
                await this.resolutionService.CreateAsync(resolution);
                return new ContentResult
                {
                    Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "Resolution")),
                    ContentType = "text/html"
                };
            }
            return View("Form", model);
        }

        // POST: Resolution/Update
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Update(Models.ResolutionDetailVM model)
        {
            if (ModelState.IsValid)
            {
                var resolution = this.resolutionService.Get(model.Id);
                if (resolution != null)
                {
                    resolution.Width = model.Width;
                    resolution.Height = model.Height;
                    resolution.Note = model.Note;
                }
                await this.resolutionService.UpdateAsync(resolution);
                return new ContentResult
                {
                    Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "Resolution")),
                    ContentType = "text/html"
                };
            }
            return View("Form", model);
        }

        // GET: Resolution/Delete/:id
        public ActionResult Delete(int id)
        {
            var resolution = this.resolutionService.Get(id);
            if (resolution != null)
            {
                this.resolutionService.Delete(resolution);
            }
            return this.RedirectToAction("Index");
        }

    }
}