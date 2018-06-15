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
    public class MediaController : Controller
    {
        IMediaSrcService mediaService = DependencyUtils.Resolve<IMediaSrcService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();

        // GET: Media/Index
        public ActionResult Index() 
        {
            DateTime dateCreate = DateTime.Now;
            DateTime dateUpdate = DateTime.Now;
            var medias = this.mediaService.Get().ToList();
            var mediaVMs = new List<Models.MediaSrcVM>();

            foreach (var item in medias)
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
                mediaVMs.Add(b);
            }
            ViewBag.locationList = mediaVMs;
            return View();

        }
        // Media/Form
        public ActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {

                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~Resource/Images/"), fileName);
                file.SaveAs(path);
            }

            return RedirectToAction("UploadDocument");
        }
    }
    
}