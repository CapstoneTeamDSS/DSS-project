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
    [Authorize]
    public class PlaylistController : Controller
    {
        IPlaylistService playlistService = DependencyUtils.Resolve<IPlaylistService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();
        // GET: Playlist
        public ActionResult Index()
        {
            var playlists = this.playlistService.Get().ToList();
            var playlistVMs = new List<Models.PlaylistDetailVM>();

            foreach (var item in playlists)
            {
                var p = new Models.PlaylistDetailVM
                {
                    Title = item.Title,
                    Description = item.Description,
                    Id = item.PlaylistID,
                };
                playlistVMs.Add(p);
            }
            ViewBag.playlistList = playlistVMs;
            return View("Index");
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
        public ActionResult Form(int? id)
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
            return View("Form", model);
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
                var playlist = new Data.Models.Entities.Playlist
                {
                    Title = model.Title,
                    Description = model.Description,
                };
                await this.playlistService.CreateAsync(playlist);

                /* Add item to playlist*/
                IPlaylistItemService playlistItemService = DependencyUtils.Resolve<IPlaylistItemService>();
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
                            Duration = "00:00",
                        };
                        await playlistItemService.CreateAsync(playlistItem);
                    }
                }

                return this.RedirectToAction("Index");
            }
            return View("Form", model);
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