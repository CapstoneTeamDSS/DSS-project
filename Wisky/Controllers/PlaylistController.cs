using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSS.Controllers
{
    public class PlaylistController : Controller
    {
        // GET: Playlist
        public ActionResult Index()
        {
            return View();
        }

        // GET: Playlist/Form/:id
        public ActionResult Form()
        {

            return View("Form");
        }
    }

    
}