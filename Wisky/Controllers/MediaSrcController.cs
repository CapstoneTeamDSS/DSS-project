﻿using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DSS.Data.Models.Entities.Services;
using System.IO;
using Microsoft.AspNet.Identity;

namespace DSS.Controllers
{
    [Authorize(Roles = "Admin, Active User")]
    public class MediaSrcController : Controller
    {
        IMediaSrcService mediaSrcService = DependencyUtils.Resolve<IMediaSrcService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();

        // GET: Media/Index
        public ActionResult Index()
        {
            ViewBag.mediaSrcList = GetMediaSrcListByBrandId();
            ViewBag.addSuccess = Session["ADD_RESULT"] ?? false;
            ViewBag.updateSuccess = Session["UPDATE_RESULT"] ?? false;
            Session.Clear();
            return View();
        }

        // Media/Form
        public ActionResult Form(int? id)
        {
            DateTime aDateTime = DateTime.Now;
            Models.MediaSrcVM model = null;

            if (id != null)
            {
                var mediaSrc = this.mediaSrcService.Get(id);
                if (mediaSrc != null)
                {
                    model = new Models.MediaSrcVM
                    {
                        MediaSrcId = mediaSrc.MediaSrcID,
                        BrandId = mediaSrc.BrandID,
                        Title = mediaSrc.Title,
                        isPublic = (bool)mediaSrc.isPublic,
                        URL = mediaSrc.URL,
                        Description = mediaSrc.Description,
                        UpdateDatetime = aDateTime
                    };
                }
            }
            return View(model);
        }
        //TOANTXSE62006
        //GET: MediaSrc/UpdateStatus
        public ActionResult UpdateStatus(int dataId)
        {
            bool result = false;
            var mediasrc = this.mediaSrcService
                .Get(a => a.MediaSrcID == dataId)
                .FirstOrDefault();
            if (mediasrc != null)
            {
                mediasrc.isPublic = !mediasrc.isPublic;
                this.mediaSrcService.Update(mediasrc);
                result = true;
            }
            return Json(new
            {
                success = result,
            }, JsonRequestBehavior.AllowGet);

        }
        //TrinhNNP
        //Get media Src List by Brand ID
        public static List<Models.MediaSrcUseVM> GetMediaSrcListByBrandId()
        {
            IMediaSrcService mediaSrcService = DependencyUtils.Resolve<IMediaSrcService>();
            var mediaSrcUseVMs = new List<Models.MediaSrcUseVM>();
            var user = Helper.GetCurrentUser();
            var mediaSrcList = mediaSrcService.GetMediaSrcByBrand(user.BrandID);
            foreach (var item in mediaSrcList)
            {
                var m = new Models.MediaSrcUseVM
                {
                    Title = item.Title,
                    Description = item.Description,
                    URL = item.URL,
                    isPublic = (bool)item.isPublic,
                    MediaSrcId = item.MediaSrcID,
                    TypeId = item.TypeID,
                };
                mediaSrcUseVMs.Add(m);
            }
            return mediaSrcUseVMs;
        }
        //TOANTXSE
        //Get media Src List by Brand ID
        public static List<Models.MediaSrcUseVM> GetMediaSrcListByBrandIdAndStatus()
        {
            IMediaSrcService mediaSrcService = DependencyUtils.Resolve<IMediaSrcService>();
            var mediaSrcUseVMs = new List<Models.MediaSrcUseVM>();
            var user = Helper.GetCurrentUser();
            var mediaSrcList = mediaSrcService.GetMediaSrcByBrand(user.BrandID);
            foreach (var item in mediaSrcList)
            {
                if(item.isPublic == true)
                {
                    var m = new Models.MediaSrcUseVM
                    {
                        Title = item.Title,
                        Description = item.Description,
                        URL = item.URL,
                        isPublic = (bool)item.isPublic,
                        MediaSrcId = item.MediaSrcID,
                        TypeId = item.TypeID,
                    };
                    mediaSrcUseVMs.Add(m);
                }
            }
            return mediaSrcUseVMs;
        }
        // Media/Add resource
        //[HttpPost]
        //public async System.Threading.Tasks.Task<ActionResult> Add(Models.MediaSrcVM model, HttpPostedFileBase file)
        //{
        //    if (file != null && file.ContentLength > 0 && ModelState.IsValid)
        //    {
        //        var urlCheck = "";
        //        var tyleIdCheck = 0;
        //        var fileName = Path.GetFileName(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-FFF") + "-File-" + file.FileName);
        //        var resultCheck = CheckFileType(fileName);
        //        // checkt entension file
        //        string ext = Path.GetExtension(fileName);
        //        if (resultCheck == 1)
        //        {
        //            var path = Path.Combine(Server.MapPath("/Resource/Image/"), fileName);
        //            file.SaveAs(path); //Save file to project folder
        //            urlCheck = "/Resource/Image/";
        //            tyleIdCheck = 1;
        //        }
        //        else if (resultCheck == 2)
        //        {
        //            var path = Path.Combine(Server.MapPath("/Resource/Video/"), fileName);
        //            file.SaveAs(path); //Save file to project folder
        //            urlCheck = "/Resource/Video/";
        //            tyleIdCheck = 2;
        //        }
        //        else if (resultCheck == 3)
        //        {
        //            var path = Path.Combine(Server.MapPath("/Resource/Audio/"), fileName);
        //            file.SaveAs(path); //Save file to project folder
        //            urlCheck = "/Resource/Audio/";
        //            tyleIdCheck = 3;
        //        }
        //        else if (resultCheck == 4)
        //        {
        //            var path = Path.Combine(Server.MapPath("/Resource/OrtherFile/"), fileName);
        //            file.SaveAs(path); //Save file to project folder
        //            urlCheck = "/Resource/OrtherFile/";
        //            tyleIdCheck = 4;
        //        }
        //        DateTime time = DateTime.Now;
        //        var media = new Data.Models.Entities.MediaSrc
        //        {
        //            BrandID = 1,
        //            Title = model.Title,
        //            isPublic = model.isActive,
        //            TypeID = tyleIdCheck,
        //            URL = urlCheck + fileName,
        //            Description = model.Description,
        //            Extension = ext,
        //            CreateDatetime = time,
        //        };
        //        await this.mediaSrcService.CreateAsync(media);
        //        //return this.RedirectToAction("Index");
        //        return new ContentResult
        //        {
        //            Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "MediaSrc")),
        //            ContentType = "text/html"
        //        };
        //    }
        //    return View("Form", model);
        //}

        //Media/Add resource
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Add(Models.MediaSrcVM model)
        {
            if (ModelState.IsValid)
            {
                string ext = Path.GetExtension(model.Filename);
                int typeCheck = this.CheckFileType(ext);
                DateTime time = DateTime.Now;
                var user = Helper.GetCurrentUser();
                var currUser = Helper.GetCurrentUser();
                var media = new Data.Models.Entities.MediaSrc
                {
                    BrandID = currUser.BrandID,
                    Title = model.Title,
                    isPublic = model.isPublic,
                    TypeID = typeCheck,
                    URL = model.URL,
                    Description = model.Description,
                    Extension = ext,
                    CreateDatetime = time,
                    SecurityHash = model.SecurityHash
                };
                await this.mediaSrcService.CreateAsync(media);
                Session.Clear();
                Session["ADD_RESULT"] = true;
                return new ContentResult
                {
                    Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "MediaSrc")),
                    ContentType = "text/html"
                };
            }
            return Json(new
            {
                success = false,
            }, JsonRequestBehavior.AllowGet);
        }

        /*Check mime type*/
        //private int CheckMimetype(string ext)
        //{
        //    if (ext.Equals("image/png") || ext.Equals("image/jpeg") || ext.Equals("image/bmp"))
        //    {
        //        return 1;
        //    }
        //    else if (ext.Equals("video/mp4") || ext.Equals("video/mov") || ext.Equals("video/mkv"))
        //    {
        //        return 2;
        //    }
        //    else if (ext.Equals("audio/mpeg") || ext.Equals("audio/wav"))
        //    {
        //        return 3;
        //    }
        //    else if (ext.Equals("text/plain"))
        //    {
        //        return 4;
        //    }
        //    return 0;
        //}

        // Check type file
        private int CheckFileType(string FileName)
        {
            string ext = Path.GetExtension(FileName.ToLower());
            if (ext.Equals(".png") || ext.Equals(".jpg") || ext.Equals(".jpeg"))
            {
                return 1;
            }
            else if (ext.Equals(".mp4") || ext.Equals(".mov"))
            {
                return 2;
            }
            else if (ext.Equals(".mp3") || ext.Equals(".midi"))
            {
                return 3;
            }
            else if (ext.Equals(".txt"))
            {
                return 4;
            }
            return 0;
        }

        //TOANTXSE
        // POST: Media/CheckMediaIdIsUsed  
        [HttpPost]
        public JsonResult CheckMediaIdIsUsed(int id)
        {
            try
            {
                //Get device by screen Id
                IPlaylistItemService playlistItemService = DependencyUtils.Resolve<IPlaylistItemService>();
                IPlaylistService playlistService = DependencyUtils.Resolve<IPlaylistService>();
                var playlistItem = playlistItemService.Get().ToList();
                var playlist = playlistService.Get().ToList();
                var playlistItemVMs = new List<Models.PlaylistItemVM>();
                var playlistVMs = new List<Models.PlaylistDetailVM>();
                //check mediaSrcId have in playlistItem
                foreach (var item in playlistItem)
                {
                    if (item.MediaSrcID == id)
                    {
                        var b = new Models.PlaylistItemVM
                        {
                            playlistId = item.PlaylistID,

                        };
                        playlistItemVMs.Add(b);
                    }
                }
                // if playlistItemVMs != null, get Playlist Title by playlistID
                if (playlistItemVMs.Count != 0)
                {
                    foreach (var item in playlistItemVMs)
                    {
                        foreach (var itemPlaylist in playlist)
                        {
                            if (item.playlistId == itemPlaylist.PlaylistID)
                            {
                                var b = new Models.PlaylistDetailVM
                                {
                                    Title = itemPlaylist.Title,
                                };
                                playlistVMs.Add(b);
                            }
                        }
                    }
                }
                return Json(new
                {
                    isUsing = playlistItemVMs.Count != 0,
                    playlistVMlist = playlistVMs,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // GET: Media/Delete/:id
        public ActionResult Delete(int id)
        {
            var user = Helper.GetCurrentUser();
            var mediaSrc = this.mediaSrcService.Get(id);
            if (mediaSrc != null && mediaSrc.BrandID == user.BrandID)
            {
                this.mediaSrcService.Delete(mediaSrc);
            }
            return this.RedirectToAction("Index");
        }
        // Media/Update resource
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Update(Models.MediaSrcVM model)
        {
            if (ModelState.IsValid)
            {
                DateTime time = DateTime.Now;
                var mediaSrc = this.mediaSrcService.GetById((int)model.MediaSrcId);
                if (mediaSrc != null)
                {
                    mediaSrc.MediaSrcID = (int)model.MediaSrcId;
                    mediaSrc.Title = model.Title;
                    mediaSrc.isPublic = model.isPublic;
                    mediaSrc.Description = model.Description;
                    mediaSrc.UpdateDatetime = time;
                    mediaSrc.SecurityHash = mediaSrc.SecurityHash;
                };
                await this.mediaSrcService.UpdateAsync(mediaSrc);
                Session.Clear();
                Session["UPDATE_RESULT"] = true;
                return new ContentResult
                {
                    Content = string.Format("<script type='text/javascript'>window.parent.location.href = '{0}';</script>", Url.Action("Index", "MediaSrc")),
                    ContentType = "text/html"
                };
            }
            return View("Form", model);
        }
    }
}