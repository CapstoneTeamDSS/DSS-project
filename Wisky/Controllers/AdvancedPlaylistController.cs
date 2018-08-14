using AutoMapper;
using DSS.Data.Models.Entities;
using DSS.Data.Models.Entities.Services;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace DSS.Controllers
{
    [Authorize(Roles = "Admin, Active User")]
    public class AdvancedPlaylistController : Controller
    {
        IPlaylistService playlistService = DependencyUtils.Resolve<IPlaylistService>();
        IMediaSrcService mediaSrcService = DependencyUtils.Resolve<IMediaSrcService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();
        // GET: AdvancedPlaylist
        public ActionResult Index()
        {
            ViewBag.playlistList = GetPlaylistIdByBrandId();
            return View("Index");
        }
        public static List<Models.PlaylistDetailVM> GetPlaylistIdByBrandId()
        {
            IPlaylistService playlistService = DependencyUtils.Resolve<IPlaylistService>();
            IPlaylistItemService playlistItemService = DependencyUtils.Resolve<IPlaylistItemService>();
            IVisualTypeService visualTypeService = DependencyUtils.Resolve<IVisualTypeService>();
            IBrandService brandService = DependencyUtils.Resolve<IBrandService>();
            var mapper = DependencyUtils.Resolve<IMapper>();
            var user = Helper.GetCurrentUser();
            var playlistList = playlistService.GetPlaylistIdByBrandId(user.BrandID);
            var playlistDetailVM = new List<Models.PlaylistDetailVM>();
            foreach (var item in playlistList)
            {
                var m = new Models.PlaylistDetailVM
                {
                    Title = item.Title,
                    Description = item.Description,
                    Id = item.PlaylistID,
                    isPublic = (bool)item.isPublic,
                    VisualTypeName = visualTypeService.Get(item.VisualTypeID)?.TypeName,
                    Duration = playlistItemService.GetTotalDuration(item.PlaylistID),
                };
                playlistDetailVM.Add(m);
            }
            //playlistDetailVM = playlistList.Select(mapper.Map<Playlist, Models.PlaylistDetailVM>).ToList();
            return playlistDetailVM;
        }
        public ActionResult Form()
        {
            return View("Form");
        }
        [HttpPost]
        public JsonResult LoadResourceList()
        {
            var ResourceList = MediaSrcController.GetMediaSrcListByBrandIdAndStatus();
            return Json(new
            {
                ResourceList = ResourceList,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckTypeMediaSrcInPlaylist(int id)
        {
            try
            {
                IMediaSrcService mediaSrcService = DependencyUtils.Resolve<IMediaSrcService>();
                var mediaType = mediaSrcService.Get(id).TypeID;
                var mediaURL = mediaSrcService.Get(id).URL;
                return Json(new
                {
                    mediaURL = mediaURL,
                    mediaType = mediaType,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult LoadMediaSrcInfo(int mediaSrcId)
        {
            IMediaSrcService mediaSrcService = DependencyUtils.Resolve<IMediaSrcService>();
            var mediaSrc = mediaSrcService.GetById(mediaSrcId);
            return Json(new
            {
                Id = mediaSrc.MediaSrcID,
                Description = mediaSrc.Description,
                Title = mediaSrc.Title,
                Duration = "00:00:00",
            }, JsonRequestBehavior.AllowGet);
        }
    }

}