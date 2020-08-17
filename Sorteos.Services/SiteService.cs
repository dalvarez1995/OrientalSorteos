using Newtonsoft.Json;
using Sorteos.Data;
using Sorteos.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteos.Services
{
    public class SiteService
    {
        public SiteModel GetSiteByClientId(string clientId)
        {
            using (var context = new SorteosDbEntities())
            {
                return context.Sitio.Where(s => s.ClientId == clientId).ToList().Select(s => new SiteModel
                {
                    Id = s.Id,
                    PageTitle = s.PageTitle,
                    TOS = s.TOS,
                    POP = s.POP,
                    BaseUrl = s.BaseUrl,
                    Company = s.Company,
                    EmailAccount = s.EmailAccount,
                    FacebookClientId = s.FacebookClientId,
                    FacebookLink = s.FacebookLink,
                    FacebookRedirectUri = s.FacebookRedirectUri,
                    FacebookSecretKey = s.FacebookSecretKey,
                    InstagramLink = s.InstagramLink,
                    SendGridApiKey = s.SendGridApiKey,
                    WhatsappLink = s.WhatsappLink,
                    SupportUrl = s.SupportUrl,
                    EmailTemplates = JsonConvert.DeserializeObject<EmailTemplates>(s.EmailTemplates)
                }).FirstOrDefault();
            }
        }

        public SiteModel GetSiteById(int id)
        {
            using (var context = new SorteosDbEntities())
            {
                return context.Sitio.Where(s => s.Id == id).ToList().Select(s => new SiteModel
                {
                    Id = s.Id,
                    PageTitle = s.PageTitle,
                    TOS = s.TOS,
                    POP = s.POP,
                    BaseUrl = s.BaseUrl,
                    Company = s.Company,
                    EmailAccount = s.EmailAccount,
                    FacebookClientId = s.FacebookClientId,
                    FacebookLink = s.FacebookLink,
                    FacebookRedirectUri = s.FacebookRedirectUri,
                    FacebookSecretKey = s.FacebookSecretKey,
                    InstagramLink = s.InstagramLink,
                    SendGridApiKey = s.SendGridApiKey,
                    WhatsappLink = s.WhatsappLink,
                    SupportUrl = s.SupportUrl,
                    EmailTemplates = JsonConvert.DeserializeObject<EmailTemplates>(s.EmailTemplates)
                }).FirstOrDefault();
            }
        }

        public void UpdateSite(int id, string tos, string pop)
        {
            using (var context = new SorteosDbEntities())
            {
                var site = context.Sitio.Where(s => s.Id == id).FirstOrDefault();
                site.TOS = tos;
                site.POP = pop;

                context.SaveChanges();
            }
        }


        public List<SiteModel> GetAllSites(bool showNonActive = false)
        {
            using (var context = new SorteosDbEntities())
            {
                var query = context.Sitio.AsQueryable();
                if (!showNonActive)
                    query = query.Where(s => s.Activo == true);

                return query.ToList().Select(s => new SiteModel
                {
                    Id = s.Id,
                    PageTitle = s.PageTitle,
                    TOS = s.TOS,
                    POP = s.POP,
                    BaseUrl = s.BaseUrl,
                    Company = s.Company,
                    EmailAccount = s.EmailAccount,
                    FacebookClientId = s.FacebookClientId,
                    FacebookLink = s.FacebookLink,
                    FacebookRedirectUri = s.FacebookRedirectUri,
                    FacebookSecretKey = s.FacebookSecretKey,
                    InstagramLink = s.InstagramLink,
                    SendGridApiKey = s.SendGridApiKey,
                    WhatsappLink = s.WhatsappLink,
                    SupportUrl = s.SupportUrl,
                    EmailTemplates = JsonConvert.DeserializeObject<EmailTemplates>(s.EmailTemplates)
                }).ToList();
            }
        }
    }
}
