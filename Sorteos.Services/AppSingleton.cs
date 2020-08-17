using Sorteos.Services;
using Sorteos.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sorteos.Services
{
    public class AppSingleton
    {
        public static AppSingleton Instance { get; private set; }
        public string TokenSecret { get; private set; }
        public string LogoSrc { get; set; }
        public string PatternSrc { get; set; }
        public SiteModel Sitio { get; set; }
        private AppSingleton(AppSettings settings)
        {
            TokenSecret = settings.TokenSecret;
            Sitio = new SiteService().GetSiteByClientId(settings.ClientId);
            LogoSrc = settings.LogoSrc;
            PatternSrc = settings.PatternSrc;
        }

        public static void Init(AppSettings settings)
        {
            Instance = new AppSingleton(settings);
        }
    }
}