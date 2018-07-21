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
    public class LayoutController : Controller
    {
        //[Authorize]

        ILayoutService layoutService = DependencyUtils.Resolve<ILayoutService>();
        IMapper mapper = DependencyUtils.Resolve<IMapper>();

        //GET: Layout/Index
        public ActionResult Index()
        {
            var layouts = this.layoutService.Get().ToList();
            var layoutVMs = new List<Models.LayoutDetailVM>();

            foreach (var item in layouts)
            {
                var b = new Models.LayoutDetailVM
                {
                    Id = item.LayoutID,
                    Description = item.Description,
                    IsHorizontal = item.isHorizontal,
                    IsPublic =  item.isPublic,
                    Layoutsrc = item.LayoutSrc,
                    Title = item.Title,
                    Url = item.URL
                };
                layoutVMs.Add(b);
            }
            ViewBag.layoutsList = layoutVMs;
            return View();
        }
    }     
    }
