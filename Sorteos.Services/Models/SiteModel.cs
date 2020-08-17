using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteos.Services.Models
{
    public class SiteModel
    {
        public int Id { get; set; }
        public string PageTitle { get; set; }
        public string TOS { get; set; }
        public string POP { get; set; }
        public string FacebookClientId { get; set; }
        public string FacebookSecretKey { get; set; }
        public string FacebookRedirectUri { get; set; }
        public string SendGridApiKey { get; set; }
        public string Company { get; set; }
        public string BaseUrl { get; set; }
        public string LogoSrc { get; set; }
        public string WhatsappLink { get; set; }
        public string InstagramLink { get; set; }
        public string FacebookLink { get; set; }
        public string EmailAccount { get; set; }
        public string SupportUrl { get; set; }
        public EmailTemplates EmailTemplates { get; set; }

    }

    public struct EmailTemplates {
        public string ActivationTemplateId;
        public string RecoverPasswordTemplateId;
        public string WelcomeTemplateId;
    }
}
