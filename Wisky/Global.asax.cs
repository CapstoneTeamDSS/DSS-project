using AutoMapper;
using DSS.Data.Models.Entities;
using SkyWeb.DatVM.Data;
using SkyWeb.DatVM.Mvc.Autofac;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using Wisky.Areas.Publisher.Models;

namespace Wisky
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutofacInitializer.Initialize(
                typeof(DSSEntities).Assembly,
                typeof(DSSEntities),
                new MapperConfiguration(this.AutoMapperConfig));
        }

        private void AutoMapperConfig(IMapperConfiguration config)
        {
            var assembly = typeof(DSSEntities).Assembly;
            var entityInterface = typeof(IEntity);

            var entityTypes = assembly.DefinedTypes.Where(q => entityInterface.IsAssignableFrom(q));
            foreach (var entityType in entityTypes)
            {
                var name = entityType.Name;
                var viewModelType = assembly.DefinedTypes.FirstOrDefault(q => q.Name == (name + "ViewModel"));

                if (viewModelType != null)
                {
                    config.CreateMap(entityType, viewModelType);
                    config.CreateMap(viewModelType, entityType);

                }
            }

            //config.CreateMap<PublisherUser, PublisherUserEditViewModel>()
            //    .ForMember(q => q.AspNetUser, opt => opt.MapFrom(q => q.AspNetUser))
            //    .ForMember(q => q.Brand, opt => opt.MapFrom(q => q.Brand));

            //config.CreateMap<PublisherUserEditViewModel, PublisherUser>()
            //    .ForMember(q => q.AspNetUser, opt => opt.MapFrom(q => q.AspNetUser))
            //    .ForMember(q => q.Brand, opt => opt.MapFrom(q => q.Brand));

            //config.CreateMap<Location, LocationEditViewModel>()
            //    .ForMember(q => q.LandingPagePlaces, opt => opt.MapFrom(q => q.LandingPagePlaces));

            //config.CreateMap<AspNetUser, AspNetUserEditViewModel>()
            //   .ForMember(q => q.AspNetRoles, opt => opt.MapFrom(q => q.AspNetRoles));

            //config.CreateMap<UserAccount, UserAccountViewModel>();

            //config.CreateMap<UserAccountCreateViewModel, UserAccount>();

            //config.CreateMap<UserAccount, UserAccountCreateViewModel>();

        }
        protected void Application_DesignRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];
            if(cookie != null && cookie.Value != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie.Value);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie.Value);
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("vi");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("vi");
            }
        }

    }
}
