using AutoMapper;
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
    [Authorize]
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
            var playlistDetailVM = new List<Models.PlaylistDetailVM>();
            IBrandService brandService = DependencyUtils.Resolve<IBrandService>();
            Models.CurrentUserVM currUser = (Models.CurrentUserVM)System.Web.HttpContext.Current.Session["currentUser"];
            var playlistList = playlistService.GetPlaylistIdByBrandId(currUser.BrandId);
            foreach (var item in playlistList)
            {
                var m = new Models.PlaylistDetailVM
                {
                    Title = item.Title,
                    Description = item.Description,
                    Id = item.PlaylistID,
                    Duration = "00:00:00",
                };
                playlistDetailVM.Add(m);
            }
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
                    };
                }
            }
            ViewBag.mediaSrcList = MediaSrcController.GetMediaSrcListByBrandId();
            ViewBag.itemList = GetMediaSrcListByPlaylistId(id ?? default(int));
            return View("Form", model);
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
        public async System.Threading.Tasks.Task<ActionResult> Add(Models.PlaylistDetailVM model, int[] to)
        {
            if (ModelState.IsValid)
            {
                Models.CurrentUserVM currUser = (Models.CurrentUserVM)System.Web.HttpContext.Current.Session["currentUser"];
                var playlist = new Data.Models.Entities.Playlist
                {
                    Title = model.Title,
                    Description = model.Description,
                    BrandID = currUser.BrandId,
                };
                await this.playlistService.CreateAsync(playlist);

                /* Add item to playlist*/
                IPlaylistItemService playlistItemService = DependencyUtils.Resolve<IPlaylistItemService>();
                IMediaSrcService mediaSrcService = DependencyUtils.Resolve<IMediaSrcService>();
                if (to.Length > 0)
                {
                    var i = 0;
                    foreach (var item in to)
                    {

                        var playlistItem = new Data.Models.Entities.PlaylistItem
                        {
                            PlaylistID = playlist.PlaylistID,
                            MediaSrcID = item,
                            DisplayOrder = i++,
                            Duration = GetVideoDuration(mediaSrcService.GetById(item).URL),
                        };
                        await playlistItemService.CreateAsync(playlistItem);
                    }
                }
                return this.RedirectToAction("Index");
            }
            return View("Form", model);
        }

        private static string GetVideoDuration(string filePath)
        {
            string applicationPath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            string appPath = applicationPath.Replace("\\", "/");
            ShellFile so = ShellFile.FromFilePath(appPath + filePath);
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
                    isActive = (bool)mediaSrc.Status,
                    
                };
            }
            return View(model);
        }
        // POST: Playlist/Update
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Update(Models.PlaylistDetailVM model)
        {
            if (ModelState.IsValid)
            {
                var playlist = this.playlistService.Get(model.Id);
                if (playlist != null)
                {
                    playlist.Title = model.Title;
                    playlist.Description = model.Description;
                }
                await this.playlistService.UpdateAsync(playlist);
                return this.RedirectToAction("Index");
            }
            return View("Form", model);
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