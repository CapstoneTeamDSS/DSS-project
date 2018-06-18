using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DSS.Data.Models.Entities.Services;
using System.IO;

namespace DSS.Controllers
{
    public class MediaSrcController : Controller
    {
        IMediaSrcService mediaSrcService = DependencyUtils.Resolve<IMediaSrcService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();

        // GET: Media/Index
        public ActionResult Index() 
        {
            DateTime dateCreate = DateTime.Now;
            DateTime dateUpdate = DateTime.Now;
            var mediaSrcs = this.mediaSrcService.Get().ToList();
            var mediaSrcVMs = new List<Models.MediaSrcVM>();

            foreach (var item in mediaSrcs)
            {
                var b = new Models.MediaSrcVM
                {
                    MediaSrcId = item.MediaSrcID,
                    Description = item.Description,
                    Title = item.Title,
                    Status = true,
                    TypeId = item.TypeID,
                    URL = item.URL,
                    CreateDatetime = dateCreate,
                    UpdateDatetime = dateUpdate,
                };
                mediaSrcVMs.Add(b);
            }
            ViewBag.mediaSrcList = mediaSrcVMs;
            return View();

        }
        // Media/Form
        public ActionResult Form()
        {
            return View();
        }
        // Media/Add resource
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Add(Models.MediaSrcVM model, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0 && ModelState.IsValid)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("/Resource/Image/"), fileName);
                file.SaveAs(path); //Save file to project folder
                DateTime time = DateTime.Now;
                var media = new Data.Models.Entities.MediaSrc
                {
                    BrandID = 1,
                    Title = model.Title,
                    Status = model.Status,
                    TypeID = 1,
                    URL = "/Resource/Image/" + fileName,
                    Description = model.Description,
                    CreateDatetime = time.ToShortTimeString(),
                    UpdateDatetime = time.ToShortTimeString(),
                };

                await this.mediaSrcService.CreateAsync(media);
                return this.RedirectToAction("Index");
            }
            return View("Form", model);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Resource/Images/"), fileName);
                file.SaveAs(path);
            }

            return RedirectToAction("UploadDocument");
        }

        // GET: Media/Delete/:id
        public ActionResult Delete(int id)
        {
            var mediaSrc = this.mediaSrcService.Get(id);
            if (mediaSrc != null)
            {
                this.mediaSrcService.Delete(mediaSrc);
            }
            return this.RedirectToAction("Index");
        }
    }
    
}