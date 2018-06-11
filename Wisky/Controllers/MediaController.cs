using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSS.Controllers
{
    public class MediaController : Controller
    {
        // GET: Media/Index
        public ActionResult Index()
        {
            return View();
        }
        // Media/Form
        public ActionResult Form()
        {
            return View();
        }
    }
    
}