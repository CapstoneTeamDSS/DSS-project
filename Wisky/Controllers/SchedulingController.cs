using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSS.Controllers
{
    public class SchedulingController : Controller
    {
        // GET: Scheduling
        public ActionResult Index()
        {
            return View();
        }

        // GET: Scheduling/Form/:id
        public ActionResult Form()
        {
            return View();
        }
    }
}