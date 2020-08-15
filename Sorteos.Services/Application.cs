using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sorteos.Web
{
    public class Application
    {
        public static Application Instance { get; private set; }
        public string ClientId { get; private set; }
        public string TokenSecret { get; set; }
        private Application(Settings settings)
        {
            ClientId = settings.ClientId;
            TokenSecret = settings.TokenSecret;
        }

        public static void Init(Settings settings)
        {
            Instance = new Application(settings);
        }
    }
}