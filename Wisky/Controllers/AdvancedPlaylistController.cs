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
            ViewBag.addSuccess = Session["ADD_RESULT"] ?? false;
            ViewBag.updateSuccess = Session["UPDATE_RESULT"] ?? false;
            Session.Clear();
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
        public ActionResult Form(int? id)
        {
            Models.PlaylistCRUDVM model = null;
            if (id != null)
            {
                var playlist = this.playlistService.Get(id);
                if (playlist != null)
                {
                    model = new Models.PlaylistCRUDVM
                    {
                        Title = playlist.Title,
                        Description = playlist.Description,
                        VisualTypeID = playlist.VisualTypeID ?? 1,
                        VisualTypeName = playlist.VisualType.TypeName,
                        Id = playlist.PlaylistID,
                        isPublic = (bool)playlist.isPublic,
                    };
                }
            }
            //ViewBag.mediaSrcList = MediaSrcController.GetMediaSrcListByBrandIdAndStatus();
            ViewBag.visualTypeList = GetVisualTypes();
            return View("Form", model);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Update(Models.PlaylistCRUDVM model)
        {
            if (ModelState.IsValid)
            {
                var playlist = this.playlistService.Get(model.Id);
                if (playlist != null)
                {
                    playlist.Title = model.Title;
                    playlist.Description = model.Description;
                    playlist.isPublic = model.isPublic;
                    playlist.UpdateDateTime = DateTime.Now;
                }
                await this.playlistService.UpdateAsync(playlist);
                IPlaylistItemService playlistItemService = DependencyUtils.Resolve<IPlaylistItemService>();
                /* Remove old items of playlist*/
                List<DSS.Data.Models.Entities.PlaylistItem> items = playlistItemService.Get(a => a.PlaylistID == model.Id).ToList();
                foreach (var item in items)
                {
                    await playlistItemService.DeleteAsync(item);
                }
                /* Add item to playlist*/
                IMediaSrcService mediaSrcService = DependencyUtils.Resolve<IMediaSrcService>();
                if (model.AddedElements.Length > 0)
                {
                    var i = 0;
                    foreach (var item in model.AddedElements)
                    {
                        var playlistItem = new Data.Models.Entities.PlaylistItem
                        {
                            PlaylistID = playlist.PlaylistID,
                            MediaSrcID = item.ItemId,
                            DisplayOrder = i++,
                        };
                        var mediaSrcType = mediaSrcService.GetById(item.ItemId).MediaType.TypeID;
                        if (mediaSrcType != 1)
                        {
                            //playlistItem.Duration = GetVideoDuration(mediaSrcService.GetById(item.ItemId).URL);
                            playlistItem.Duration = 0;
                        }
                        else
                        {
                            var duration = TimeSpan.Parse(item.ItemDuration);
                            playlistItem.Duration = Convert.ToInt32(duration.TotalSeconds);
                        }
                        await playlistItemService.CreateAsync(playlistItem);
                        Session.Clear();
                        Session["UPDATE_RESULT"] = true;
                    }
                }
                return Json(new
                {
                    success = true,
                    url = "/AdvancedPlaylist/Index",
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                success = false,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LoadPlaylistItemIds(int playlistId)
        {
            IPlaylistItemService playlistItemService = DependencyUtils.Resolve<IPlaylistItemService>();
            IPlaylistService playlistService = DependencyUtils.Resolve<IPlaylistService>();
            var playlistItems = playlistItemService.GetPlaylistItemByPlaylistId(playlistId);
            var playlistItemIds = new List<int>();
            if (playlistItems != null)
            {
                foreach (var item in playlistItems)
                {
                    playlistItemIds.Add(item.MediaSrcID);
                }
            }
            return Json(new
            {
                PlaylistItemIds = playlistItemIds,
            }, JsonRequestBehavior.AllowGet);
        }

        private List<Models.VisualTypeVM> GetVisualTypes()
        {
            List<Models.VisualTypeVM> visualTypeVMs = new List<Models.VisualTypeVM>();
            IVisualTypeService visualTypeService = DependencyUtils.Resolve<IVisualTypeService>();
            var visualTypes = visualTypeService.Get().ToList();
            if (visualTypes != null)
            {
                foreach (var item in visualTypes)
                {
                    var v = new Models.VisualTypeVM
                    {
                        VisualTypeID = item.VisualTypeID,
                        VisualTypeName = item.TypeName,
                    };
                    visualTypeVMs.Add(v);
                }
            }
            return visualTypeVMs;
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

        //TrinhNNP
        // POST: Playlist/Add
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Add(Models.PlaylistCRUDVM model)
        {
            if (ModelState.IsValid)
            {
                var user = Helper.GetCurrentUser();
                var playlist = new Data.Models.Entities.Playlist
                {
                    Title = model.Title,
                    Description = model.Description,
                    BrandID = user.BrandID,
                    isPublic = model.isPublic,
                    VisualTypeID = model.VisualTypeID,
                    UpdateDateTime = DateTime.Now,
                };
                await this.playlistService.CreateAsync(playlist);
                /* Add item to playlist*/
                IPlaylistItemService playlistItemService = DependencyUtils.Resolve<IPlaylistItemService>();
                IMediaSrcService mediaSrcService = DependencyUtils.Resolve<IMediaSrcService>();
                if (model.AddedElements.Length > 0)
                {
                    var i = 0;
                    foreach (var item in model.AddedElements)
                    {
                        var playlistItem = new Data.Models.Entities.PlaylistItem
                        {
                            PlaylistID = playlist.PlaylistID,
                            MediaSrcID = item.ItemId,
                            DisplayOrder = i++,
                        };
                        var mediaSrcType = mediaSrcService.GetById(item.ItemId).MediaType.TypeID;
                        if (mediaSrcType != 1)
                        {
                            //playlistItem.Duration = GetVideoDuration(mediaSrcService.GetById(item.ItemId).URL);
                            playlistItem.Duration = 0;
                        }
                        else
                        {
                            var duration = TimeSpan.Parse(item.ItemDuration);
                            playlistItem.Duration = Convert.ToInt32(duration.TotalSeconds);
                        }
                        await playlistItemService.CreateAsync(playlistItem);
                        Session.Clear();
                        Session["ADD_RESULT"] = true;
                    }
                }
                return Json(new
                {
                    success = true,
                    url = "/AdvancedPlaylist/Index",
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                success = false,
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