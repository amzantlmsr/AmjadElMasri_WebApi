using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Database.SetInitializer(new DatabaseInitializercontect());

            Database.SetInitializer(new DropCreateDatabaseAlways<CustomerCustomerContactContext>());
            using (var db = new CustomerCustomerContactContext())
            {
                db.Database.Initialize(true);      
            }
            
            log4net.Config.XmlConfigurator.Configure( new FileInfo(Server.MapPath("/Web.config")));
        }
    }
}
