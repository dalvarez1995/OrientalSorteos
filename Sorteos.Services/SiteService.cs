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
        public SiteModel GetSite() {
            using (var context = new SorteosDbEntities()) {
                return context.Sitio.Select(s => new SiteModel
                {
                    Id = s.Id,
                    TOS = s.TOS,
                    POP = s.POP
                }).FirstOrDefault();
            }
        }

        public void UpdateSite(string tos, string pop)
        {
            using (var context = new SorteosDbEntities())
            {
                var site = context.Sitio.FirstOrDefault();
                site.TOS = tos;
                site.POP = pop;

                context.SaveChanges();
            }
        }
    }
}
