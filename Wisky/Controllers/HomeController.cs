using AutoMapper.QueryableExtensions;
using SkyWeb.DatVM.Mvc;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using DSS.Data.Models.Entities.Services;
using DSS.Controllers;

namespace Wisky.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            var user = System.Web.HttpContext.Current.User;
            ViewBag.Title = "Digital Signage System";
            ViewBag.FullName = user.Identity.Name;
            Session["username"] = user.Identity.Name;
            Session["userID"] = user.Identity.GetUserId(); //GetUserId() only can be used when using Microsoft.AspNet.Identity;
            //Get Size Account
            IAspNetUserService aspNetUserService = DependencyUtils.Resolve<IAspNetUserService>();
            var users = Helper.GetCurrentUser();
            var accountList = aspNetUserService.GetAccountsByBrandId(users.BrandID);
            var size = accountList.Count;
            ViewBag.accountList = size;
            //ScreenList
            IScreenService screenSevice = DependencyUtils.Resolve<IScreenService>();
            var screenList = screenSevice.GetScreenIdByBrandId(users.BrandID);
            var screenSize = screenList.Count;
            ViewBag.screenList = screenSize;

            //BoxList
            IBoxService boxService = DependencyUtils.Resolve<IBoxService>();
            var boxList = boxService.GetBoxIdByBrandId(users.BrandID);
            var boxSize = boxList.Count;
            ViewBag.boxList = boxSize;

            //DeviceList
            IDeviceService deviceService = DependencyUtils.Resolve<IDeviceService>();
            var deviceList = deviceService.GetDeviceByBrandId(users.BrandID);
            var deviceSize = deviceList.Count;
            ViewBag.deviceList = deviceSize;

            //LocationList
            ILocationService locationService = DependencyUtils.Resolve<ILocationService>();
            var locationList = locationService.GetLocationIdByBrandId(users.BrandID);
            var locationSize = locationList.Count;
            ViewBag.locationList = locationSize;

            //MediaSrcList
            IMediaSrcService mediaSrcService = DependencyUtils.Resolve<IMediaSrcService>();
            var mediasrcList = mediaSrcService.GetMediaSrcByBrand(users.BrandID);
            var mediasrcSize = mediasrcList.Count;
            ViewBag.mediarsList = mediasrcSize;

            //PlaylistList
            IPlaylistService playlistService = DependencyUtils.Resolve<IPlaylistService>();
            var playlistList = playlistService.GetPlaylistIdByBrandId(users.BrandID);
            var playlistSize = playlistList.Count;
            ViewBag.playlistList = playlistSize;

            //ScenarioList
            IScenarioService  scenarioService= DependencyUtils.Resolve<IScenarioService>();
            var scenarioList = scenarioService.GetScenarioIdByBrandId(users.BrandID);
            var scenarioSize = scenarioList.Count;
            ViewBag.scenarioList = scenarioSize;
            return View();
            
           
          
        }


    }
}
