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
            return View("Form", model);
        }

        // GET: Playlist/Detail/:id
        public ActionResult Detail()
        {

            return View("Detail");
        }

        // POST: Playlist/Add
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Add(Models.PlaylistDetailVM model)
        {
            if (ModelState.IsValid)
            {
                var playlist = new Data.Models.Entities.Playlist
                {
                    Title = model.Title,
                    Description = model.Description,
                };
                await this.playlistService.CreateAsync(playlist);
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