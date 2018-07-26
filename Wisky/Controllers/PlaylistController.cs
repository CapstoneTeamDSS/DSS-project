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
    public class PlaylistController : Controller
    {
        IPlaylistService playlistService = DependencyUtils.Resolve<IPlaylistService>();
        IMediaSrcService mediaSrcService = DependencyUtils.Resolve<IMediaSrcService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();
        // GET: Playlist
        public ActionResult Index()
        {
            ViewBag.playlistList = GetPlaylistIdByBrandId();
            return View("Index");
        }
        //ToanTXSE
        //Get location List by location ID
        public static List<Models.PlaylistDetailVM> GetPlaylistIdByBrandId()
        {
            IPlaylistService playlistService = DependencyUtils.Resolve<IPlaylistService>();
            IPlaylistItemService playlistItemService = DependencyUtils.Resolve<IPlaylistItemService>();
            IBrandService brandService = DependencyUtils.Resolve<IBrandService>();
            var mapper= DependencyUtils.Resolve<IMapper>();
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
                    Duration = playlistItemService.GetTotalDuration(item.PlaylistID),
                };
                playlistDetailVM.Add(m);
            }
            //playlistDetailVM = playlistList.Select(mapper.Map<Playlist, Models.PlaylistDetailVM>).ToList();
            return playlistDetailVM;
        }

        //Update Playlist Item
        public async System.Threading.Tasks.Task<ActionResult> UpdateDetail(int[] playlistItemIds)
        {
            IPlaylistItemService playlistItemService = DependencyUtils.Resolve<IPlaylistItemService>();
            if (playlistItemIds.Length > 0)
            {
                var i = 0;
                foreach (var item in playlistItemIds)
                {
                    var playlistItem = playlistItemService.GetPlaylistItemById(item);
                    if (playlistItem != null)
                    {
                        playlistItem.DisplayOrder = i++;
                        await playlistItemService.UpdateAsync(playlistItem);
                    }
                }
                return this.RedirectToAction("Index");
            }
            return View();
        }

        // GET: Playlist/Form/:id
        public ActionResult Form(int? id, string imageId)
        {
            Models.PlaylistDetailVM model = null;
            if (id != null)
            {
                var playlist = this.playlistService.Get(id);
                if (playlist != null)
                {
                    model = new Models.PlaylistDetailVM
                    {
                        Title = playlist.Title,
                        Description = playlist.Description,
                        Id = playlist.PlaylistID,
                        isPublic = (bool)playlist.isPublic,
                    };
                }
            }
            ViewBag.mediaSrcList = MediaSrcController.GetMediaSrcListByBrandId();
            ViewBag.itemList = GetMediaSrcListByPlaylistId(id ?? default(int));
            return View("Form", model);
        }
        //TOANTXSE62006
        //GET: Playlist/UpdateStatus
        public ActionResult UpdateStatus(int dataId)
        {
            bool result = false;
            var playlist = this.playlistService
                .Get(a => a.PlaylistID == dataId)
                .FirstOrDefault();
            if (playlist != null)
            {
                playlist.isPublic = !playlist.isPublic;
                this.playlistService.Update(playlist);
                result = true;
            }
            return Json(new
            {
                success = result,
            }, JsonRequestBehavior.AllowGet);

        }
        //TrinhNNP
        //Get media Src List by playlist ID
        public static List<Models.PlaylistItemVM> GetMediaSrcListByPlaylistId(int playlistId)
        {
            IPlaylistItemService playlistItemService = DependencyUtils.Resolve<IPlaylistItemService>();
            IMediaSrcService mediaSrcService = DependencyUtils.Resolve<IMediaSrcService>();
            var itemList = new List<Models.PlaylistItemVM>();
            var playlistItems = playlistItemService.GetMediaSrcByPlaylistId(playlistId);
            foreach (var item in playlistItems)
            {
                var p = new Models.PlaylistItemVM();
                var pObj = mediaSrcService.GetById(item.MediaSrcID);
                p.mediaSrcTitle = pObj.Title;
                p.mediaSrcId = pObj.MediaSrcID;
                p.URL = pObj.URL;
                itemList.Add(p);
            }
            return itemList;
        }

        // GET: Playlist/Detail/:id
        public ActionResult Detail(int id)
        {
            IPlaylistItemService playlistItemService = DependencyUtils.Resolve<IPlaylistItemService>();
            IMediaSrcService mediaSrcService = DependencyUtils.Resolve<IMediaSrcService>();
            var playlistItems = playlistItemService.GetMediaSrcByPlaylistId(id);
            var playlistItemVMs = new List<Models.PlaylistItemVM>();
            if (playlistItems != null)
            {
                foreach (var item in playlistItems)
                {
                    var p = new Models.PlaylistItemVM
                    {
                        playlistId = item.PlaylistID,
                        mediaSrcId = item.MediaSrcID,
                        displayOrder = item.DisplayOrder,
                        duration = item.Duration,
                        playlistItemId = item.PlaylistItemID,
                    };
                    var mediaSrc = mediaSrcService.GetById(item.MediaSrcID);
                    if (mediaSrc != null)
                    {
                        p.mediaSrcTitle = mediaSrc.Title;
                        p.URL = mediaSrc.URL;
                    }
                    playlistItemVMs.Add(p);
                }
            }
            ViewBag.playlistItemList = playlistItemVMs;
            return View("Detail");
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
                        } else
                        {
                            var duration = TimeSpan.Parse(item.ItemDuration);
                            playlistItem.Duration = Convert.ToInt32(duration.TotalSeconds);
                        }
                        await playlistItemService.CreateAsync(playlistItem);
                    }
                }    
                return Json(new
                {
                    success = true,
                    url = "/Playlist/Index",
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                success = false,
            }, JsonRequestBehavior.AllowGet);
        }

        private static string GetVideoDuration(string filePath)
        {
            string applicationPath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            string appPath = applicationPath.Replace("\\", "/");
            ShellFile so = ShellFile.FromFilePath(filePath);
            IShellProperty prop = so.Properties.System.Media.Duration;
            var u = (ulong)prop.ValueAsObject;
            return TimeSpan.FromTicks((long)u).ToString();
        }
        //Playlist/Get Image Id to input duration
        public ActionResult ImageDuration(string imageId)
        {
            Models.MediaSrcUseVM model = null;
            int Id = Int32.Parse(imageId);
            var mediaSrc = this.mediaSrcService.Get(Id);
            if (mediaSrc != null)
            {
                model = new Models.MediaSrcUseVM
                {
                    Title = mediaSrc.Title,
                    Description = mediaSrc.Description,
                    MediaSrcId = mediaSrc.MediaSrcID,
                    isPublic = (bool)mediaSrc.isPublic,
                    
                };
            }
            return View(model);
        }
        // POST: Playlist/Update
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
                }
                await this.playlistService.UpdateAsync(playlist);
                return Json(new
                {
                    success = true,
                    url = "/Playlist/Index",
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                success = false,
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: Playlist/Delete/:id
        public ActionResult Delete(int id)
        {
            var playlist = this.playlistService.Get(id);
            if (playlist != null)
            {
                this.playlistService.Delete(playlist);
            }
            return this.RedirectToAction("Index");
        }
    }
}