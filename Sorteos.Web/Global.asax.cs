using Newtonsoft.Json;
using Sorteos.Services;
using System;
using System.IO;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sorteos.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //load app settings
            AppSingleton.Init(LoadSettings());
        }

        private AppSettings LoadSettings()
        {
            var configFileString = File.ReadAllText(Server.MapPath("~/config.json"));
            return JsonConvert.DeserializeObject<AppSettings>(configFileString);
        }
    }
}